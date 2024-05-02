using brainX.Areas.Instructor.Models;
using brainX.Data;
using brainX.Infrastructure.Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace brainX.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    [Authorize(Roles = "Instructor")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public DashboardController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

       

        public async Task<IActionResult> Index()
        {
            var model = new DashboardModel();
            var accounts = await _dbContext.Accounts.ToListAsync();
            var applicationUser = await _userManager.GetUserAsync(HttpContext.User);
            var acount = new Account();
            foreach (var account in accounts)
            {
                if(account.InstructorId == Guid.Parse(applicationUser.Id))
                {
                    acount = account;
                    break;
                }
            }
            if (acount == null)
            {
                model.CurrentBalance = 0;
                model.TotalEarning= 0;
            }
            else
            {
                model.CurrentBalance = (double)acount.CurrentBalance;
                model.TotalEarning = (double)acount.TotalRevenue;
            }
            return View(model);
        }
    }
}
