using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Semester7.PBL6.Website_Ecommerce.API.ModelDtos;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.Repositories;

namespace Semester7.PBL6.Website_Ecommerce.API.Repositories
{
    public interface IImageProduct : IRepository<ProductImage>
    {
        void Add(ProductImage image);
        void Update(ProductImage image);
        void Delete(int id);
        Task<IEnumerable<ProductImage>> GetAllImagebyIdProduct(int id);
        Task<IEnumerable<ProductImage>> GetAllImagebyIdProductDetail(int id);
        Task<ProductImage> GetImagebyId(int id);

    }
}