using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Semester7.PBL6.Website_Ecommerce.API.ModelDtos;
using Website_Ecommerce.API.Data;
using Website_Ecommerce.API.Data.Entities;
using Website_Ecommerce.API.Repositories;

namespace Semester7.PBL6.Website_Ecommerce.API.Repositories
{
    public class ProductImageRepository : IImageProduct
    {
        private readonly DataContext _dataContext;

        public IUnitOfWork UnitOfWork => _dataContext;

        public ProductImageRepository(DataContext dataContext){
            _dataContext = dataContext;
        }
        public void Add(ProductImage image)
        {
            _dataContext.ProductImages.Add(image);   
        }

        public void Delete(int id)
        {
            var Image =  _dataContext.ProductImages.Where(i => i.Id == id).FirstOrDefault();
            _dataContext.ProductImages.Remove(Image);
        }

        public async Task<IEnumerable<ProductImage>> GetAllImagebyIdProduct(int id)
        {
            var pdetails = _dataContext.ProductDetails.Where(p => p.ProductId == id);
            return await _dataContext.ProductImages.
                                    Join(pdetails, imp => imp.ProductDetailId, pd => pd.Id,(img, pd) 
                                    => new  ProductImage(){
                                        Id = img.Id,
                                        UrlImage = img.UrlImage,
                                        ProductDetailId = pd.Id,
                                    }).ToListAsync();

        }

        public async Task<IEnumerable<ProductImage>> GetAllImagebyIdProductDetail(int id)
        {
            var ListImage = _dataContext.ProductImages.Where(p => p.ProductDetailId == id);
            return await ListImage.ToListAsync();
        }
        public ImageProductDto MapImagetoDto(ProductImage imgProduct){
            return new ImageProductDto{
                Id = imgProduct.Id,
                UrlImage = imgProduct.UrlImage,
                ProductDetailId = imgProduct.ProductDetailId
            };
        }

        public async Task<ProductImage> GetImagebyId(int id)
        {
            return await _dataContext.ProductImages.Where(x => x.Id == id).FirstAsync();
        }

        public void Update(ProductImage image)
        {
            _dataContext.ProductImages.Update(image);
        }

    }
}