using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace N5NowTT.Domain
{
    public class Permissions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public string? NombreEmpleado { get; set; }
            
        public string? ApellidoEmpleado { get; set; }
        public DateTime FechaPermiso { get; set; }


        public PermissionType? TipoPermiso { get; set; }
    }
}