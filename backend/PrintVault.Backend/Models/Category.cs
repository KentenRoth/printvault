using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrintVault.Backend.Models
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public List<PrintFile> PrintFiles { get; set; } = new();
    }
}