using System;
using System.Collections.Generic;

namespace ImageGallery.Models
{
    public class GalleryDetailModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> Tags { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Url { get; set; }
    }
}
