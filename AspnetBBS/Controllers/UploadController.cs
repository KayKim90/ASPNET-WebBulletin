using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspnetBBS.Controllers
{
    public class UploadController : Controller
    {
        private readonly IHostingEnvironment _envvironment;
        public UploadController(IHostingEnvironment environment)
        {
            _envvironment = environment;
        }
        // http://www.example.com/Upload/ImageUpload 만약 이것이 너무 길면 Route를 통해
        // http://www.example.com/api/upload 간편해짐
        [HttpPost, Route("api/upload")] //라우트 재정의
        public IActionResult ImageUpload(IFormFile fileToUpload)
        {
            var path = Path.Combine(_envvironment.WebRootPath, @"images\upload");
            //var fileName = fileToUpload.FileName;
            var fileName = DateTime.Now.ToString("yyyy_MM_dd") +"_"+ fileToUpload.FileName;
            using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                fileToUpload.CopyTo(fileStream);
            }
            return Ok(new { file = "/images/upload/" + fileName, success = true });
            /* Kay ver
            //1. PATH - 파일 업로드시, 저장 위치 명시 
            var path = Path.Combine(_envvironment.WebRootPath, @"images\upload");
            //2. NAME - DateTime, GUID + GUID
            //3. EXTENSION - jpg, png, txt ...
            var fileName = DateTime.Now.ToString("yyyy_MM_dd")+file.FileName;

            using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return Ok();
            //이를통해 이미지 업로드를 받는 서버쪽 코드 완료!

            //# URL 접근 방식
            //
            */
        }
    }
}
