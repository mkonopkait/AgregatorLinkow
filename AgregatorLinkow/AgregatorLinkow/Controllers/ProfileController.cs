using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgregatorLinkow.Data;
using AgregatorLinkow.Helpers;
using AgregatorLinkow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgregatorLinkow.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [Route("Profile/Index")]
        public async Task<IActionResult> Index(int pageNumber)
        {
            var links = await this._context.Links.ToListAsync();
            var currentUser = await this._context.Users
                    .FirstOrDefaultAsync(x => x.UserName == HttpContext.User.Identity.Name);
            var actualLinks = links?.Where(x => (DateTime.Now - x.Date) < TimeSpan.FromDays(5) && x.UserId == currentUser.Id)
                .OrderByDescending(x => x.PlusesNumber);

            List<LinkListItem> listItems = new List<LinkListItem>();
            foreach (var link in actualLinks)
            {
                listItems.Add(new LinkListItem(currentUser, link));
            }

            return View(new LinkList<LinkListItem>(listItems, pageNumber, 100));
        }

        [Authorize]
        [Route("Profile/AddLink")]
        public IActionResult AddLink()
        {
            TempData["Message"] = "Link added.";
            return View();
        }

        [Authorize]
        [Route("Profile/AddLink")]
        [HttpPost]
        public async Task<IActionResult> AddLink(Link link)
        {
            link.Date = DateTime.UtcNow;
            var currentUser = await this._context.Users
                .FirstOrDefaultAsync(x => x.UserName == HttpContext.User.Identity.Name);
            link.UserId = currentUser.Id;
            this._context.Add(link);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}