using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgregatorLinkow.Helpers
{
    public class LinkList<T> : List<T>
    {
        public int CurrentPage { get; set; }

        public int PagesNumber { get; set; }

        public LinkList(List<T> source, int currentPage, int pageSize)
        {
            this.CurrentPage = currentPage;
            var count = source.Count();
            if(count != 0)
            {
                var size = count / pageSize;
                this.PagesNumber = count % pageSize == 0 ? size : size + 1;
                var pagedItems = source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                this.AddRange(pagedItems);
            }
            
        }

        public bool HasPrevious
        {
            get
            {
                return (CurrentPage > 1);
            }
        }

        public bool HasNext
        {
            get
            {
                return (CurrentPage < PagesNumber);
            }
        }
    }
}
