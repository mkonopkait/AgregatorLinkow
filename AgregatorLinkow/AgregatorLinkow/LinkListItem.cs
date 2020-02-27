using AgregatorLinkow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgregatorLinkow
{
    public class LinkListItem
    {
        public int LinkId { get; set; }

        public string UserId { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public int PlusesNumber { get; set; }

        public DateTime Date { get; set; }

        public bool PlusVisibility { get; set; }

        public LinkListItem(User user, Link link)
        {
            UserId = link.UserId;
            LinkId = link.Id;
            Url = link.Url;
            Title = link.Title;
            PlusesNumber = link.PlusesNumber;
            Date = link.Date;
            this.PlusVisibility = true;
        }
    }
}
