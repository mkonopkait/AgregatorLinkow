using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgregatorLinkow.Helpers
{
    public class LinkList<T> : List<T>
    {
        public LinkList(List<T> source)
        {
            this.AddRange(source);
        }
    }
}
