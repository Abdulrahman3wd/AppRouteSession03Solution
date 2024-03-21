using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code Is Required !!")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name Is Required !!")]

        public string Name { get; set; }
        [Display (Name = "Date Of Creation")]
        public DateTime DateOFCreation { get; set; } = DateTime.Now;
    }
}
