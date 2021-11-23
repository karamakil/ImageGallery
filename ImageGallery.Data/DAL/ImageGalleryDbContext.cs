using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageGallery.Data.DAL
{
    public class ImageGalleryDbContext : DbContext
    {
        public ImageGalleryDbContext(DbContextOptions options):base(options)
        {
        }

        public DbSet<GalleryImage> GalleryImages { get; set; }
        public DbSet<ImageTag> ImageTags{ get; set; }
    }
}
