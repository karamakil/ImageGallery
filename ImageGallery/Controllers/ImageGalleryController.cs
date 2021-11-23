using ImageGallery.Data.DAL;
using ImageGallery.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Controllers
{
    public class ImageGalleryController : Controller
    {

        public IActionResult Index()
        {
            var hikingImgTags = new List<ImageTag>();
            var cityImageTags = new List<ImageTag>();

            var imgTag1 = new ImageTag() { Description = "adventure" };
            var imgTag2 = new ImageTag() { Description = "Urban" };
            var imgTag3 = new ImageTag() { Description = "New York" };

            hikingImgTags.Add(imgTag1);
            cityImageTags.AddRange(new List<ImageTag> { imgTag2, imgTag3 });

            var galleryImg = new List<GalleryImage>()
            {
                new GalleryImage()
                {
                    Title = "hikingTrip",
                    Url = "https://images.pexels.com/photos/7026406/pexels-photo-7026406.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940",
                    Created = DateTime.Now,
                    Tags = hikingImgTags
                },
                new GalleryImage()
                {
                    Title = "downtown Trip",
                    Url = "https://images.pexels.com/photos/1070945/pexels-photo-1070945.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940",
                    Created = DateTime.Now,
                    Tags = cityImageTags
                },
                new GalleryImage()
                {
                    Title = "on the trial",
                    Url = "https://images.pexels.com/photos/1448055/pexels-photo-1448055.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500",
                    Created = DateTime.Now,
                    Tags = hikingImgTags
                },
            };

            var model = new GalleryIndexModel()
            {
                Images = galleryImg,
                SearchQuery = string.Empty,

            };
            return View(model);
        }


    }
}
