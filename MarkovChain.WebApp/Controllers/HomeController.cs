using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using MarkovChain.WebApp.Models;
using MarkovChain.Core;

namespace MarkovChain.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment _env;
        private FileReader _fileReader;
        private ChainWorker _worker;
        public HomeController(IHostingEnvironment environment)
        {
            _env = environment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Valid(IFormFile file)
        {
            var listViews = new List<DataViewModel>();
            if (file.Length > 0)
            {
                ReadFile(file);
                var list = _fileReader.Read();
                RemoveFile(file);
                BreakdownEngine engine = new BreakdownEngine(list.ToList());
                var breakdowns = engine.GetBreakdowns();

                for (int j = 0;j<list.Count();j++)
                {
                    for (int i = 0; i < breakdowns.Count; i++)
                    {
                        if (breakdowns[i].Position == j)
                        {
                            listViews.Add(new DataViewModel { Value = list.ElementAt(j), IsBreakdown = true });
                        }
                        else
                        {
                            listViews.Add(new DataViewModel { Value = list.ElementAt(j), IsBreakdown = false });
                        }
                    }
                }
            }
            return Json(listViews);
        }


        private void RemoveFile(IFormFile file)
        {
            string directoryPath = Path.Combine(_env.WebRootPath, "Data");
            string filePath = Path.Combine(directoryPath, file.FileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        private void ReadFile(IFormFile file)
        {
            string directoryPath = Path.Combine(_env.WebRootPath, "Data");
            try
            {
                string filePath = Path.Combine(directoryPath, file.FileName);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fs);
                }
                _fileReader = new FileReader(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
