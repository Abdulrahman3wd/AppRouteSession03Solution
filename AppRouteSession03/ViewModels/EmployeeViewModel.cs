using App.DAL.Models;
using AppRouteSession03.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace AppRouteSession03.PL.ViewModels
{
    public class EmployeeViewModel
    {
        #region Data
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(50, ErrorMessage = "MaxLength Of Name is 50 Chars")]
        [MinLength(5, ErrorMessage = "MinLength of Name is 5 Chars")]
        public string Name { get; set; }
        [Range(22, 30)]
        public int? Age { get; set; }
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Address Must be Like 123-street-City-Country")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string Phone { get; set; }
        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }
        public Gender gender { get; set; }
        public EmpType empType { get; set; }
      
      
        #endregion

        public int? DepartmentId { get; set; } // ForignKey Column


        // Navigational Property [One] [Related Data]
        public virtual Department Department { get; set; }


    }
}
