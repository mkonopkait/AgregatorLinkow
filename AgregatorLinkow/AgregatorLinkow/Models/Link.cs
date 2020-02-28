using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgregatorLinkow.Models
{
    public class Link
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Url { get; set; }

        public int PlusesNumber { get; set; }

        public string UserId { get; set; }

        public DateTime Date { get; set; }

        public User User { get; set; }

        public List<Plus> Pluses { get; set; }
    }
}
