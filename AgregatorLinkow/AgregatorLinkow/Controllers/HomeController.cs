using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AgregatorLinkow.Models;
using AgregatorLinkow.Data;
using Microsoft.EntityFrameworkCore;
using AgregatorLinkow.Helpers;

namespace AgregatorLinkow.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            this._logger = logger;
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            var links = await this._context.Links.ToListAsync();
            var actualLinks = links?.Where(x => (DateTime.Now - x.Date) < TimeSpan.FromDays(5))
                .OrderByDescending(x => x.PlusesNumber); 
            var loggedUser = await this._context.Users
                    .FirstOrDefaultAsync(x => x.UserName == HttpContext.User.Identity.Name);

            List<LinkListItem> listItems = new List<LinkListItem>();
            foreach (var link in actualLinks)
            {
                listItems.Add(new LinkListItem(loggedUser, link));
            }
            var ll = new LinkList<LinkListItem>(listItems);
            return View(ll);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
