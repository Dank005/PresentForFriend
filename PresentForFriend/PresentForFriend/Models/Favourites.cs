using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PresentForFriend.Models
{
    namespace PresentForFriend.Models
    {
        public class Favourites
        {
            [Key]
            public int Id { get; set; }
            public string UserID { get; set; }
            public int PresentId { get; set; }
        }
    }
}
