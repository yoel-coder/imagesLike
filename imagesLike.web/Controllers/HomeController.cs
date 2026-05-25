using imagesLike.web.Models;
using ImagesLike;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace imagesLike.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _connectionString;


        public HomeController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
            _webHostEnviorment = webHostEnvironment;
        }
        private IWebHostEnvironment _webHostEnviorment;
        public IActionResult Index()
        {
            var repo = new Imagerepository(_connectionString);
            var ivm = new ImageViewModel();
            ivm.images = repo.GetImages();
            return View(ivm);
        }
        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Upload(string title, IFormFile imageFile)
        {

            string filename = $"{Guid.NewGuid()} - {imageFile.FileName}";

            var fulpath = Path.Combine(_webHostEnviorment.WebRootPath, "image", filename);
            using FileStream fs = new FileStream(fulpath, FileMode.Create);

            imageFile.CopyTo(fs);
            var repo = new Imagerepository(_connectionString);
            repo.Addimage(filename, title);
            return Redirect("/Home/index");
        }
        public IActionResult SingleImageView(int id)
        {
            var repo = new Imagerepository(_connectionString);
            var ivm = new SinglePictureViewModel
            {
                image = repo.GetImage(id),
                likedImages = HttpContext.Session.Get<List<int>>("pictureIds")
            };
            return View(ivm);
        }
        [HttpPost("/LikePost")]
      public IActionResult LikePost(int id)
        {
            List<int> likedIds = HttpContext.Session.Get<List<int>>("pictureIds");
            if (likedIds==null)
            {
                likedIds = new List<int>();

            }
         
            
                var repo = new Imagerepository(_connectionString);
                repo.PostLike(id);
                likedIds.Add(id);
                HttpContext.Session.Set<List<int>>("pictureIds", likedIds);
            


            return Redirect($"/home/SingleImageView?id={id}");
        }
        public IActionResult GetLikesById(int id)
        {
            var repo = new Imagerepository(_connectionString);
            return Json(repo.GetLikesById(id));
        }



        }

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            return value == null ? default(T) :
                JsonSerializer.Deserialize<T>(value);
        }
    }


}
