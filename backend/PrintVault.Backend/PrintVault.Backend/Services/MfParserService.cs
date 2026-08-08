using System.IO.Compression;
using System.Xml.Linq;
using PrintVault.Backend.Models;

namespace PrintVault.Backend.Services;

public class MfParserService
{
    public MfData Parse(string filePath)
    {
        using var archive = ZipFile.OpenRead(filePath);

        var modelData = ParseModelFile(archive);
        var sliceData = ParseSliceInfo(archive);

        return new MfData
        {
            Title = modelData.Title,
            Description = modelData.Description,
            CreationDate = modelData.CreationDate,
            ProfileTitle = modelData.ProfileTitle,
            ProfileDescription = modelData.ProfileDescription,
            Plates = sliceData.Plates,
            NumberOfPlates = sliceData.Plates.Count,
            TotalFilamentUsed = sliceData.Plates.Sum(p => p.FilamentUsed),
            TotalPrintTimeSeconds = sliceData.Plates.Sum(p => p.PrintTimeSeconds)
        };
    }

    private ModelFileData ParseModelFile(ZipArchive archive)
    {
        var entry = archive.GetEntry("3D/3dmodel.model");
        if (entry == null)
            return new ModelFileData();

        using var stream = entry.Open();
        var doc = XDocument.Load(stream);
        XNamespace ns = "http://schemas.microsoft.com/3dmanufacturing/core/2015/02";

        string GetMetadata(string name) =>
            doc.Descendants(ns + "metadata")
               .FirstOrDefault(x => (string?)x.Attribute("name") == name)
               ?.Value ?? string.Empty;

        return new ModelFileData
        {
            Title = GetMetadata("Title"),
            Description = GetMetadata("Description"),
            CreationDate = GetMetadata("CreationDate"),
            ProfileTitle = GetMetadata("ProfileTitle"),
            ProfileDescription = GetMetadata("ProfileDescription")
        };
    }

    private SliceInfoData ParseSliceInfo(ZipArchive archive)
    {
        var entry = archive.GetEntry("Metadata/slice_info.config");
        if (entry == null)
            return new SliceInfoData();

        using var stream = entry.Open();
        var doc = XDocument.Load(stream);

        var plates = doc.Descendants("plate").Select(plate =>
        {
            string GetMeta(string key) =>
                plate.Elements("metadata")
                     .FirstOrDefault(x => (string?)x.Attribute("key") == key)
                     ?.Attribute("value")?.Value ?? "0";

            return new PlateData
            {
                PlateNumber = int.Parse(GetMeta("index")),
                FilamentUsed = double.Parse(GetMeta("weight")),
                PrintTimeSeconds = int.Parse(GetMeta("prediction"))
            };
        }).ToList();

        return new SliceInfoData { Plates = plates };
    }

    private class SliceInfoData
    {
        public List<PlateData> Plates { get; set; } = new();
    }
}