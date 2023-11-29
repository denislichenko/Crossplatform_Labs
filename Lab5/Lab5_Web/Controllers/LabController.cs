using Lab5_Labs;
using Lab5_Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab5_Web.Controllers
{
    [Authorize]
    public class LabController : Controller
    {
        public IActionResult Lab1()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Lab1([FromForm]IFormFile file)
        {
            string inputText = await file.ReadAsStringAsync();
            var response = LabRunner.ExecuteLab1(inputText);

            return View(response);
        }

        public IActionResult Lab2()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Lab2([FromForm] IFormFile file)
        {
            string inputText = await file.ReadAsStringAsync();
            var response = LabRunner.ExecuteLab2(inputText);

            return View(response);
        }

        public IActionResult Lab3()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Lab3([FromForm] IFormFile file)
        {
            string inputText = await file.ReadAsStringAsync();
            var response = LabRunner.ExecuteLab3(inputText);

            return View(response);
        }
    }
}
