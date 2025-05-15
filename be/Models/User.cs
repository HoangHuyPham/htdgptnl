using System.ComponentModel.DataAnnotations.Schema;

namespace be.Models
{
    public class User
    {
        public Guid Id {get; set;}
        public string? Username {get; set;} = null!;
        public string? Password {get; set;} = null!;
        public string? Email {get; set;} = null!;
        public string? Phone {get; set;} = null!;
        public Guid? RoleId {get; set;} = null!;
        public Guid? EmployeeId {get; set;}
    }
}