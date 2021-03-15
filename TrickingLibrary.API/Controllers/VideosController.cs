using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using TrickingLibrary.API.BackgroundServices;
using TrickingLibrary.API.BackgroundServices.VideoEditing;

namespace TrickingLibrary.API.Controllers
{
    [Route("api/videos")]
    public class VideosController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly VideoManager _videoManager;

        public VideosController(IWebHostEnvironment env, VideoManager videoManager)
        {
            _env = env;
            _videoManager = videoManager;
        }

        [HttpGet("{video}")]
        public IActionResult GetVideo(string video)
        {
            var savePath = _videoManager.DevVideoPath(video);
            if (string.IsNullOrEmpty(savePath))
            {
                return BadRequest();
            }
            
            return new FileStreamResult(new FileStream(savePath, FileMode.Open, FileAccess.Read), "video/*");
        }
        
        [HttpPost]
        public Task<string> UploadVideo(IFormFile video)
        {
            return _videoManager.SaveTemporaryFile(video);
        }

        [HttpDelete("{fileName}")]
        public IActionResult DeleteTemporaryVideo(string fileName)
        {
            if (!_videoManager.Temporary(fileName))
            {
                return BadRequest();
            }

            if (!_videoManager.TemporaryFileExists(fileName))
            {
                return NoContent();
            }

            _videoManager.DeleteTemporaryFile(fileName);
            
            return Ok();
        }
    }
}