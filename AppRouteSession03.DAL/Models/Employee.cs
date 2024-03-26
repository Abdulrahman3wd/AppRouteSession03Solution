using App.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AppRouteSession03.DAL.Models
{
    public enum Gender
    {
        [EnumMember(Value ="Male")]
        Male = 1,
        [EnumMember(Value = "Female")]

        Female = 2
    }
    public enum EmpType
    {
        FullTIme = 1,
        
        PartTime = 2
    }
    public class Employee : ModelBase
    {

        #region Data

        public string Name { get; set; }
  
        public int? Age { get; set; }
       
        public string Address { get; set; }


        public decimal Salary { get; set; }
    
        public bool IsActive { get; set; }
  
        public string Email { get; set; }

    

        public string Phone { get; set; }

        public DateTime HiringDate { get; set; }
        public Gender gender { get; set; }
        public EmpType empType { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        #endregion

        public int? DepartmentId { get; set; } // ForignKey Column

        
        // Navigational Property [One] [Related Data]
        public virtual Department Department { get; set; }





    }
}
