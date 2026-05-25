using ImagesLike;

namespace imagesLike.web.Models
{
    public class ImageViewModel
    {
        public Image image {  get; set; }
        public List<Image> images { get; set; }
        public List<int> likedImages { get; set; }
    }
}
