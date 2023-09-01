using LoginService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LoginService.Controllers
{
    [ApiController]
    public class LoginServiceController : ControllerBase
    {
        Models.AppContext appContext = new Models.AppContext();

        [Route("Validate")]
        [HttpGet]
        public IActionResult Validate()
        {
            string s = Request.Query["obj"];
            //var obj_data = JsonConvert.DeserializeObject<UserData>(s);
            var data = (from d in appContext.LoginDatas where d.UserName == s select d).FirstOrDefault();
            //var data = "Hari";
            if (data != null)
                return Ok(data);
            else
                return NotFound("User Not Found! : " + s);
        }

        [Route("Register")]
        [HttpPost]
        public IActionResult RegisterUser()
        {
            string email = Request.Form["email"];
            string passwd = Request.Form["pass"];

            var obj = new UserData()
            {
                UserName = email,
                Password = passwd,
                Roles = "Employee"
            };

            appContext.LoginDatas.Add(obj);
            int x = appContext.SaveChanges();

            if (x > 0) return Ok();
            else return NoContent();
        }

        [Route("GetInfo/{id}")]
        [HttpGet]
        public IActionResult GetUserDetails(int id)
        {
            var info = (from data in appContext.UserInfos where data.UserId == id select data).FirstOrDefault();
            if (info != null)
                return Ok(info);
            else
                return NotFound("User Not Found! : " + id);
        }

        [Route("CreateInfo")]
        [HttpPost]
        public async Task<IActionResult> CreateInfo(IFormFile file)
        {
            var data = Request.Form["Name"];
            var address = Request.Form["Address"];
            var mobileno = Request.Form["MobileNo"];
            var id = Convert.ToInt32(Request.Form["id"]);

            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var imageEntity = new UserInfo
            {
                FileName = file.FileName,
                Name = data,
                Address = address,
                MobileNo = mobileno,
                UserId = id
            };

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                imageEntity.ProfileImage = memoryStream.ToArray();
            }
            appContext.UserInfos.Add(imageEntity);
            int x = appContext.SaveChanges();
            if (x > 0) return Ok();
            else return NoContent();
        }
    }
}
