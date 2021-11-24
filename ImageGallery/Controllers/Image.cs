using ImageGallery.Data.Interface;
using ImageGallery.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ImageGallery.Controllers
{
    public class Image : Controller
    {
        private IConfiguration _Config;
        private string AzureConnectionString{get;}
        private readonly IImage _imageService;

        public Image(IConfiguration config, IImage imageService)
        {
            this._Config = config;
            this.AzureConnectionString = _Config["AzureConnectionString"];
            this._imageService = imageService;
        }

        public IActionResult Upload()
        {
            var model = new UploadViewModel()
            {

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadViewImage(IFormFile file,string title,string tags)
        {
            var container = _imageService.GetBlobContainer(AzureConnectionString, "images");
            var content = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
            var fileName = content.FileName.Trim('"');

            //Get a reference to block blob
            var blockblob = container.GetBlockBlobReference(fileName);
            await blockblob.UploadFromStreamAsync(file.OpenReadStream());
            await _imageService.SetImage(title,tags,blockblob.Uri);
            return RedirectToAction("Index","ImageGallery");
        }

    }
}
