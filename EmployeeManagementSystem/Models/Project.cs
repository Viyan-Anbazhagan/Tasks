using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models{
    public class Project{
        [Required]
        public string? Name{get;set;}
        [DataType(DataType.DateTime)]
        [Required]
        [CustomDeadline(ErrorMessage ="Deadline must be Future date")]
        public DateTime? Deadline{get;set;}
        [Required]
        public string? Manager{get;set;}
        [Required]
        public string? Document{get;set;}
        
    }
    public class CustomDeadlineAttribute:ValidationAttribute{
    public override bool IsValid(object value){

        DateTime dateTime = Convert.ToDateTime(value);
        return dateTime >= DateTime.Now;
    }
   }
}