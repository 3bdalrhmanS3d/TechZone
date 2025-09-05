using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechZone.Core.models
{
    public class Laptop
    {
        public int Id { get; set; }
        public string ModelName { get; set; }   // زي: ZBook G5 Studio
        public string Processor { get; set; }   // i7-8850H مثلاً
        public string GPU { get; set; }         // Nvidia Quadro
        public string ScreenSize { get; set; }  // 15.6" FHD
        public bool HasCamera { get; set; }
        public bool HasKeyboard { get; set; }
        public bool HasTouchScreen { get; set; }
        public string Ports { get; set; }       // USB-C, HDMI ...

        // Navigation
        public ICollection<LaptopVariant> Variants { get; set; } // عشان الرامات والهارد
    }

}
