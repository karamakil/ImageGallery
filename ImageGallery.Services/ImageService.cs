using ImageGallery.Data.DAL;
using ImageGallery.Data.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGallery.Services
{
    public class ImageService : IImage
    {
        private readonly ImageGalleryDbContext _dbContext;
        public ImageService(ImageGalleryDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IEnumerable<GalleryImage> GetAll()
        {
            return this._dbContext.GalleryImages.Include(x => x.Tags);
        }
        public IEnumerable<GalleryImage> GetByTag(string tag)
        {
            return this.GetAll().Where(x => x.Tags.Any(y => y.Description == tag));
        }

        public GalleryImage GetById(int id)
        {
            return this._dbContext.GalleryImages.Include(x => x.Tags).FirstOrDefault(x => x.Id == id);
        }

        public CloudBlobContainer GetBlobContainer(string azureConnectionString, string containerName)
        {
            var storageAccount = CloudStorageAccount.Parse(azureConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference(containerName);
        }

        public async Task SetImage(string title, string tags, Uri uri)
        {
            var image = new GalleryImage()
            {
                Title = title,
                Tags = ParseTags(tags),
                Url = uri.AbsoluteUri,
                Created = DateTime.Now,
            };

            _dbContext.Add(image);
           await _dbContext.SaveChangesAsync();
        }


        public List<ImageTag> ParseTags(string tags)
        {
            return tags.Split(",").Select(tag => new ImageTag()
            {
                Description = tag,
            }).ToList();
        }


    }
}
