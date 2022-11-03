using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Semester7.PBL6.Website_Ecommerce.API.Controllers
{
    public class MainController : ControllerBase
    { 
        protected ICollection<string> Errors = new List<string>();

        protected ActionResult CustomResponse(object result = null){
            if (ValidOperation()){
                return Ok(result);
            }
            return BadRequest( new ValidationProblemDetails(new Dictionary<string, string[]>{
                {"Message", Errors.ToArray()}
            }));

        }
        protected bool ValidOperation()
        {
            return !Errors.Any();
        }
    }
}