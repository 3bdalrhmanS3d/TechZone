using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechZone.Core.DTOs.Auth
{
    public class VerificationCodeStatsDto
    {
        public int TotalCodes { get; set; }
        public int ActiveCodes { get; set; }
        public int ExpiredCodes { get; set; }
        public int UsedCodes { get; set; }
        public Dictionary<string, int> CodesByType { get; set; } = new();
        public Dictionary<string, int> CodesByDestination { get; set; } = new();
    }
}
