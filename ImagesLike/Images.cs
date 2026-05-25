using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesLike
{
    public class Image
    {
        public int Id {  get; set; }
        public string Title { get; set; }
        public string imagePath { get; set; }
        public int likes { get; set; }
        public DateTime uploadDate {  get; set; }

    }
}
