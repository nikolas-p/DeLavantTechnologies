using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeLavant.Domain.MediaFiles
{
    public struct MediaFile
    {
        public string Id {  get; set; }
        public string filePath { get; set; }
        public string loadingDate { get; set; }
        public string? signature { get; set; }
    }
}
