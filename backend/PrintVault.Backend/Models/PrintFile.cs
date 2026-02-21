using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PrintVault.Backend.Models
{
    public class PrintFile
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public string? PrinterProfile { get; set; }
        public string? Material { get; set; }
        public TimeSpan? EstimatedPrintTime { get; set; }
        public double? EstimatedFilamentUsedGrams { get; set; }

        public string? ThumbnailPath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<PrintFileTag> PrintFileTags { get; set; } = new();

    }
}