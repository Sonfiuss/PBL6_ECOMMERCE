
using Microsoft.AspNetCore.Mvc;
using Website_Ecommerce.API.Response;
using Website_Ecommerce.API.services;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Website_Ecommerce.API.ModelDtos;
using Website_Ecommerce.API.Repositories;
using Microsoft.EntityFrameworkCore;
using Website_Ecommerce.API.Data.Entities;
using AutoMapper;

namespace Website_Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHostEnvironment _environment;
        private readonly IProductRepository _productRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public HomeController(
            IHostEnvironment environment,
            IProductRepository productRepository,
            IShopRepository shopRepository,
            IUserRepository userRepository,
            IMapper mapper
            ){
            _environment = environment;
            _productRepository = productRepository;
            _shopRepository = shopRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult> UpFile(List<IFormFile> files){
            const bool AllowLimitFileSize = true;
            
            var baseUrl = "https://localhost:7220";
            var listFileError = new List<FileUploadInfo>();
            var limitFileSize = 8388608;
            string result = "";
            if(files.Count <= 0){
                return BadRequest( new Response<ResponseDefault>(){
                    State = false,
                    Message = "Please select file to upload",
                    Result = new ResponseDefault()
                    {
                        Data = result
                    }
                });
            }
            // var listFileTypeAllow = "jpg|png|gif|xls|xlsx";   

            if(listFileError.Count() > 0){

                return BadRequest( new Response<ResponseDefault>(){
                    State = false,
                    Message = ErrorCode.NotFound,
                    Result = new ResponseDefault()
                    {
                        Data = JsonConvert.SerializeObject(listFileError)
                    }
                });
            }
            if(AllowLimitFileSize){
                foreach(var i in files){
                    if(i.Length > limitFileSize){
                        listFileError.Add(new FileUploadInfo(){
                            filename = i.FileName,
                            filesize = i.Length
                        });
                    }
                }
            }
            var listLinkUploaded = new List<string>();
            if(listFileError.Count() > 0){
                return BadRequest( new Response<ResponseDefault>(){
                    State = false,
                    Message = ErrorCode.NotFound,
                    Result = new ResponseDefault()
                    {
                        Data = JsonConvert.SerializeObject(listFileError)
                    }
                });
            }
            foreach(var i in files){
                if(i.Length > 0){
                    var templateUrl = i.FileName;
                    string filePath = Path.Combine($"{_environment.ContentRootPath}/wwwroot/", templateUrl);
                    string fileName = Path.GetFileName(filePath);
                    using(var stream = System.IO.File.Create(filePath)){
                        await i.CopyToAsync(stream);
                    }
                    listLinkUploaded.Add($"{baseUrl}/wwwroot/{i.FileName}");
                }

            }
            return Ok( new Response<ResponseDefault>(){
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = JsonConvert.SerializeObject(listLinkUploaded)
                    }
            });
        }

        [HttpGet("get-list-product")]
        public async Task<IActionResult> GetListProduct()
        {
            
            var products = await _productRepository.GetAllProduct();
                                                
            if(products == null)
            {
                return BadRequest( new Response<ResponseDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    Result = new ResponseDefault()
                    {
                        Data = "NotFound Product"
                    }
                });
            }

            return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = products
                    }
                });
        }
            [HttpGet("get-product-detail-by/{productId}")]
        public async Task<IActionResult> GetProductDetailByProductId(int productId)
        {
            List<ProductDetail> productDetails = await _productRepository.ProductDetails.Where(x => x.ProductId == productId).ToListAsync();

            List<ProductDetailDto> pds = _mapper.Map<List<ProductDetailDto>>(productDetails);

            return Ok( new Response<ResponseDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    Result = new ResponseDefault()
                    {
                        Data = pds
                    }
                });
        }



        [HttpGet("get-productdetail-by/{productDetailId}")]
        public async Task<IActionResult> GetProductDetailById(int productDetailId){
            ProductDetail productDetail = await _productRepository.ProductDetails.Where(p => p.Id == productDetailId).FirstOrDefaultAsync();
            ProductDetailDto productDetailDto= _mapper.Map<ProductDetailDto>(productDetail);
            return Ok( new Response<ResponseDefault>()
            {
                State = true,
                Message = ErrorCode.Success,
                Result = new ResponseDefault()
                {
                    Data = productDetailDto
                }
            });
        }
        [HttpGet("get-image-by-product-detail-id/{productDetailId}")]
        public async Task<IActionResult> getImageByProductDetailId(int productDetailId){
            ProductImage productImage = await _productRepository.ProductImages.Where(i => i.ProductDetailId == productDetailId).FirstOrDefaultAsync();
            ProductImageDto productimageDto = new ProductImageDto(){
                Id = productImage.Id,
                ProductDetailId = productImage.ProductDetailId,
                UrlImage = productImage.UrlImage
            };
            
            return Ok( new Response<ResponseDefault>()
            {
                State = true,
                Message = ErrorCode.Success,
                Result = new ResponseDefault()
                {
                    Data = productimageDto
                }
            });
        }
    }
    // [HttpPost("UploadImage")]
    // public async Task<ActionResult> UploadImage(List<IFormFile> _uploadedfiles)
    // {
    //     bool Results = false;
    //     try
    //     {
    //         foreach (IFormFile source in _uploadedfiles)
    //         {
    //             string Filename = source.FileName;
    //             string Filepath = GetFilePath(Filename);

    //             if (!System.IO.Directory.Exists(Filepath))
    //             {
    //                 System.IO.Directory.CreateDirectory(Filepath);
    //             }

    //             string imagepath = Filepath;

    //             if (System.IO.File.Exists(imagepath))
    //             {
    //                 System.IO.File.Delete(imagepath);
    //             }
    //             using (FileStream stream = System.IO.File.Create(imagepath))
    //             {
    //                 await source.CopyToAsync(stream);
    //                 Results = true;
    //             }


    //         }
    //     }
    //     catch (Exception ex)
    //     {

    //     }
    //     return Ok(Results);
    // }
        
        
    // [NonAction]
    // private string GetFilePath(string ProductCode)
    // {
    //     return this._environment.ContentRootPath + "\\Uploads\\Product\\" + ProductCode;
    // }
    // }

    
}