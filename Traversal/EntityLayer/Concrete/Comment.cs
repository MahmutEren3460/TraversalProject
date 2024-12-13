




using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Comment
    {
        [Key] 
        public int Id { get; set; }
        public string? User { get; set; }
        public DateTime Date { get; set; }
        public string? Content { get; set; }
        public bool? State { get; set; }
        public int DestinationID { get; set; }
        public Destination? Destination { get; set; }

        public int AppUserID { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
