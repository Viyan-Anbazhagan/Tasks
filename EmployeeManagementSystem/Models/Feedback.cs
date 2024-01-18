using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Models{
    public class Feedback{
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID{get;set;}
        [Required]
        public string? Useranme{get;set;}
        [Required]
        public string? Text{get;set;}
        
    }
}