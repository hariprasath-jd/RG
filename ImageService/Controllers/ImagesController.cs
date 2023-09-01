using ImageService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using static System.Net.Mime.MediaTypeNames;

namespace ImageService.Controllers
{
    [ApiController]
    public class ImagesController : ControllerBase
    {

        Models.AppContext appContext = new Models.AppContext();

        [Route("LoadAll")]
        [HttpGet]
        public async Task<IActionResult> LoadImages()
        {
            var images = await appContext.Images.ToListAsync();
            return (images != null) ? Ok(images) : NotFound();
        }

        [Route("LoadImg/{id}")]
        [HttpGet]
        public async Task<IActionResult> LoadImage(int id)
        {
            return (await appContext.Images.Where(x => x.Id == id).ToListAsync() != null) ? Ok(await appContext.Images.Where(x => x.Id == id).FirstAsync()) : NotFound("Not Found");
        }


        [Route("Load/{id}")]
        [HttpGet]
        public async Task<IActionResult> Load(int id)
        {
            return (await appContext.Images.Where(x => x.Description == id).ToListAsync() != null) ? Ok(await appContext.Images.Where(x => x.Description == id).ToListAsync()) : NotFound("Not Found");
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Save(IFormFile file)
        {
            var id = Convert.ToInt32(Request.Form["id"]);
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var imageEntity = new Images
            {
                Filename = file.FileName,
                Description = id
            };

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                imageEntity.Photos = memoryStream.ToArray();
            }

            appContext.Images.Add(imageEntity);
            await appContext.SaveChangesAsync();

            return Ok(imageEntity.Id);
        }

        [Route("Update")]
        [HttpPut]
        public IActionResult Update()
        {
            var filename = Request.Form["filename"];
            var id = Convert.ToInt32(Request.Form["id"]);

            var table = new Images() { Id = id, Filename = filename };

            appContext.Images.Attach(table);
            appContext.Entry(table).Property(x => x.Filename).IsModified = true;
            int x = appContext.SaveChanges();
            return (x > 0)? Ok():NoContent();
        }

        [Route("Delete/{id}")]
        [HttpDelete]
        public IActionResult DeelteImage(int id)
        {
            var record = appContext.Images.Find(id);
            appContext.Images.Remove(record);
            int x = appContext.SaveChanges();
            return (x > 0) ? Ok() : NoContent();
        }
    }
}
