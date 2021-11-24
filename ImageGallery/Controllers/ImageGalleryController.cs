using ImageGallery.Data.DAL;
using ImageGallery.Data.Interface;
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
        private readonly IImage _imageService;

        public ImageGalleryController(IImage imageService)
        {
            this._imageService = imageService;
        }

        public IActionResult Index()
        {
            var imgList = this._imageService.GetAll();
            var model = new GalleryIndexModel()
            {
                Images = imgList,
                SearchQuery = string.Empty,
            };
            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var image = this._imageService.GetById(id);
            var model = new GalleryDetailModel()
            {
                Id = image.Id,
                Title = image.Title,
                CreatedOn = image.Created,
                Url = image.Url,
                Tags = image.Tags.Select(t => t.Description).ToList(),
            };
            return View(model);
        }


    }
}
