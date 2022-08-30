using System.ComponentModel.DataAnnotations;

namespace ClientesAPI.Dto
{
    public class CreateCustomerDTO
    {
        [Required (ErrorMessage = "El nombre tiene que estar especificado")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido tiene que estar especificado")]
        public string Apellido { get; set; }
        [RegularExpression ("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", ErrorMessage = "El email no es correcto")]
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

    }
}
