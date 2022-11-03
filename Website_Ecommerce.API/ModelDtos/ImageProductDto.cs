using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Semester7.PBL6.Website_Ecommerce.API.ModelDtos
{
    public class ImageProductDto
    {
        public int Id { get; set; }
        public int ProductDetailId { get; set; }
        public string UrlImage { get; set; }
    }
}