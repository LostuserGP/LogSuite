using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskSuite.Shared.Models
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code is required")]
        public int Code { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Short Name is required")]
        public string ShortName { get; set; }
    }
}
