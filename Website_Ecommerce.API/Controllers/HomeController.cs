
using Microsoft.AspNetCore.Mvc;
using Website_Ecommerce.API.Response;
using Website_Ecommerce.API.services;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Website_Ecommerce.API.ModelDtos;
using Website_Ecommerce.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Website_Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHostEnvironment _environment;
        private readonly IProductRepository _productRepository;
        public HomeController(
            IHostEnvironment environment,
            IProductRepository productRepository
            ){
            _environment = environment;
            _productRepository = productRepository;
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
            
            List<ProductDto> products = await _productRepository.Products
                                                .Select(x => new ProductDto 
                                                {
                                                    Id = x.Id,
                                                    Name = x.Name,
                                                    Material = x.Material,
                                                    Origin = x.Origin,
                                                    Description = x.Description,
                                                    Status = x.Status,
                                                    Categories = x.ProductCategories.Where(y => y.ProductId == x.Id).Select(c => c.CategoryId).ToHashSet()
                                                }).ToListAsync();
            
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