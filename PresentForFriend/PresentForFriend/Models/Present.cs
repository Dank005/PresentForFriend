using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PresentForFriend.Models
{
    public class Present
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        [MaxLength(30, ErrorMessage = "Name should contains max 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(200, ErrorMessage = "Description should contains max 200 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Link is required")]
        public string Link { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public float Price { get; set; }
        public int UserID { get; set; }

    }
}
