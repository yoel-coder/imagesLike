using ImagesLike;

namespace imagesLike.web.Models
{
    public class SinglePictureViewModel
    {
        public Image image { get; set; }
        public List<int> likedImages { get; set; }
    }
}
