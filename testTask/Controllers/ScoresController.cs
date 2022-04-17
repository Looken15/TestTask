using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using testTask.Models.Simple;
using testTask.Models.Context;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web;
using System;
using System.IO;
using testTask.Interfaces;

namespace testTask.Controllers
{
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly IScoresRepository v_repository;
        private readonly IScoresService v_service;
        public ScoresController(IScoresService service, IScoresRepository repository)
        {
            v_repository = repository;
            v_service = service;
        }

        [HttpGet("api/scores")]
        public ActionResult Index()
        {
            return new ViewResult();
        }

        // POST 
        [HttpPost("api/addFromFile")]
        public HttpResponseMessage Post()
        {
            var fileStream = HttpContext.Request.Form.Files.FirstOrDefault().OpenReadStream();
            var path = Directory.GetCurrentDirectory() + "/excel.xlsx";

            using (var stream = System.IO.File.Create(path))
            {
                fileStream.Seek(0, SeekOrigin.Begin);
                fileStream.CopyTo(stream);
            }

            v_service.ReadExcelWithAdding(path);

            return null;
        }

        [HttpGet("api/test")]
        public List<SpecialtyRequirements> GetAllRequirements()
        {
            return v_repository.GetAllRequirements().ToList();
        }
    }
}