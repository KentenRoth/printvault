using PrintVault.Backend.Configuration;

namespace PrintVault.Tests.Configuration;

public class StorageOptionsTests
{
    [Fact]
    public void StorageOptions_DefaultValues_AreEmptyStrings()
    {
        var options = new StorageOptions();

        Assert.Equal(string.Empty, options.IncomingPath);
        Assert.Equal(string.Empty, options.StorageRoot);
    }

    [Fact]
    public void StorageOptions_CanSetIncomingPath()
    {
        var options = new StorageOptions
        {
            IncomingPath = "/app/incoming"
        };

        Assert.Equal("/app/incoming", options.IncomingPath);
    }

    [Fact]
    public void StorageOptions_CanSetStorageRoot()
    {
        var options = new StorageOptions
        {
            StorageRoot = "/app/storage"
        };

        Assert.Equal("/app/storage", options.StorageRoot);
    }
}