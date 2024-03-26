using AppRouteSession03.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Models
{
    public class Department : ModelBase
    {
       

        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
       
        public DateTime DateOFCreation { get; set; } = DateTime.Now;


        [InverseProperty(nameof(Employee.Department))]
        // navigational Property  [Many]
        public ICollection<Employee> Employees { get; set; }= new HashSet<Employee>();
    }
}
