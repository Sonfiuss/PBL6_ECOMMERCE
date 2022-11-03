using AutoMapper;
using Semester7.PBL6.Website_Ecommerce.API.ModelDtos;
using Website_Ecommerce.API.Data.Entities;

namespace Semester7.PBL6.Website_Ecommerce.API.Mapping
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            // Source -> Target
            CreateMap<ImageProductDto, ProductImage>();
  
        }
    }
}