using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgregatorLinkow.Models
{
    public class Plus
    {
        public int Id { get; set; }

        public int LinkId { get; set; }

        public string UserId { get; set; }

        public Link Link { get; set; }

        public User User { get; set; }
    }
}
