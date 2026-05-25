using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesLike
{
    public class Imagerepository
    {
        private readonly string _connectionstring;
        public Imagerepository(string conectionstring)
        {
            _connectionstring = conectionstring;
        }
        public List<Image> GetImages()
        {
            using var ctx = new ImageDataContext(_connectionstring);
            return ctx.Images.ToList();

        }
        public void Addimage(string filepath, string title)
        {
            var image = new Image
            {
                imagePath = filepath,
                Title = title,
                uploadDate = DateTime.Now,
                likes = 0
            };
            using var ctx = new ImageDataContext(_connectionstring);
            ctx.Images.Add(image);
            ctx.SaveChanges();
        }
        public Image GetImage(int id)
        {
            using var ctx = new ImageDataContext(_connectionstring);
            return ctx.Images.FirstOrDefault(i => i.Id == id);
        }
        public void PostLike(int id)
        {
            using var ctx = new ImageDataContext(_connectionstring);
            ctx.Database.ExecuteSqlInterpolated($" update images set likes = likes + 1 where id = {id}");
        }
        public int GetLikesById(int id)
        {
            using var ctx = new ImageDataContext (_connectionstring);
            Image x = ctx.Images.FirstOrDefault(i => i.Id == id);
            return x.likes;


        }
    }
}
