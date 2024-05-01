using Azure.AI.OpenAI;
using brainX.Areas.Student.Models;
using brainX.Data;
using brainX.Infrastructure.Domains;
using brainX.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Text;

namespace brainX.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = "Student")]
    public class AIController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICourseRepository _courseRepository;

        public AIController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ICourseRepository courseRepository)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _courseRepository = courseRepository;
        }
        public IActionResult Index()
        {
            var model = new AIModel();
            return View();
        }

        static async Task<string> GenerateResponse(string apiKey, string prompt, int maxTokens, string model)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                string url = "https://api.openai.com/v1/completions";
                string requestBody = $"{{\"prompt\": \"{prompt}\", \"max_tokens\": {maxTokens}, \"model\": \"{model}\"}}";

                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else
                {
                    Console.WriteLine($"Failed to generate response. Status code: {response.StatusCode}");
                    return null;
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(AIModel model)
        {
            var courseId = await _courseRepository.CreateCourseAsync(model);
            return RedirectToAction("Enroll", "Course", new { area = "", id = Guid.NewGuid() });
        }
    }
}
