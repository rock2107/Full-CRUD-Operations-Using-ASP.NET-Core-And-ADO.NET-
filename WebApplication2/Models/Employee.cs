using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Name")]
        [Required]
        
        public string Name { get; set; }
        
        [DisplayName("DOB")]
        [Required]
        public DateTime DOB { get; set; }
        [DisplayName("Email")]
        [Required]
        public string Email { get; set; }
        [Required]

        public double Salary { get; set; }
      
     

       
    }
}
