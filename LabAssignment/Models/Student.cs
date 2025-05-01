using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LabAssignment.Models
{
    public class Student
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required field")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "Name is required field")]
        public required string Address { get; set; }
        [Required(ErrorMessage = "Name is required field")]
        public required string Faculty { get; set; }
    }
}
