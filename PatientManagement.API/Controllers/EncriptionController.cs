using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientManagement.API.Security;

namespace PatientManagement.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EncriptionController : ControllerBase
    {
        [HttpGet("encrypt")]
        public IActionResult Encrypt(string value)
        {
            if (!string.IsNullOrEmpty(value))
                return Content(EncryptionHelper.Encrypt(value));
            else
                return BadRequest();

        }
        [HttpGet("decrypt")]
        public IActionResult Decrypt(string value)
        {
            if (!string.IsNullOrEmpty(value))
                return Content(EncryptionHelper.Decrypt(value));
            else
                return BadRequest();
        }
    }
}
