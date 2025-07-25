using be.Models.Enums;

namespace be.DTOs.Role
{
    public class CreateRoleDTO
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public RoleLevel Level { get; set; }
    }
}