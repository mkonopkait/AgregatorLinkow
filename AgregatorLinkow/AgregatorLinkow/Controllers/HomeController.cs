﻿using System;
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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace AgregatorLinkow.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, UserManager<User> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        [Route("")]

        public async Task<IActionResult> Index()
        {
            var links = await this._context.Links.ToListAsync();
            var actualLinks = links.Where(x => (DateTime.Now - x.Date) < TimeSpan.FromDays(5))
                .OrderByDescending(x => x.PlusesNumber); 
            var currentUser = await this._context.Users.Include(x => x.Links).Include(x => x.Pluses)
                    .FirstOrDefaultAsync(x => x.UserName == HttpContext.User.Identity.Name);

            List<LinkListItem> listItems = new List<LinkListItem>();
            foreach (var link in actualLinks)
            {
                listItems.Add(new LinkListItem(currentUser, link));
            }

            return View(new LinkList<LinkListItem>(listItems));
        }

        [Authorize]
        [HttpGet("{linkId:int}")]
        public async Task<IActionResult> Plus(int linkId)
        {
            var link = await this._context.Links.FirstOrDefaultAsync(x => x.Id == linkId);
            
            if(link != null)
            {
                var currentUser = await this._context.Users.Include(x => x.Links).Include(x => x.Pluses)
                .FirstOrDefaultAsync(x => x.UserName == HttpContext.User.Identity.Name);
                if(currentUser != null)
                {
                    this._context.Pluses.Add(new Plus
                    {
                        LinkId = linkId,
                        UserId = currentUser.Id
                    });
                    link.PlusesNumber++;
                    await this._context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
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
