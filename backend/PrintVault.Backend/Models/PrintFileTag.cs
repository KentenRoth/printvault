using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrintVault.Backend.Models
{
    public class PrintFileTag
    {
        public Guid PrintFileId { get; set; }
        public PrintFile PrintFile { get; set; } = null!;
        public Guid TagId { get; set; }
        public Tag Tag { get; set; } = null!;
    }
}