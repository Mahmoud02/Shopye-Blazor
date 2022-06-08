using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopye.Models.Dtos
{
    public class TestDto
    {
        [Required]
        public string Name { get; set; }
        [Required(ErrorMessage ="Description custom message")]
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
