using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrintVault.Backend.Models
{
    public class Tag
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public List<PrintFileTag> PrintFileTags { get; set; } = new();
    }
}