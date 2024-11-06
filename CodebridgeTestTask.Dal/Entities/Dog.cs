using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodebridgeTestTask.Dal.Entities
{
    public class Dog
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Color { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Tail length must be a non-negative number.")]
        public int TailLength { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Weight must be a non-negative number.")]
        public int Weight { get; set; }
    }
}
