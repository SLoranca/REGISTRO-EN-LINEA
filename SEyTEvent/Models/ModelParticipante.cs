using System.ComponentModel.DataAnnotations;

namespace SEyTEvent.Models
{
    public class ModelParticipante : Response
    {
        public int  id { get; set; }

        public int? muncipio_id { get; set; }

        [Required]
        [StringLength(150)]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Solo se permiten letras")]
        public string nombre_completo { get; set; }

        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [Range(18, 100, ErrorMessage = "La edad debe ser entre 18 y 100 años")]
        public string edad { get; set; }

        [StringLength(18)]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Solo se permiten números y letras")]
        public string curp { get; set; }

        [Required]
        [StringLength(250)]
        [EmailAddress]
        public string correo { get; set; }

        [Required]
        [StringLength(10)]
        [Phone(ErrorMessage = "El Máximo de caracteres para el telefono debe ser 10")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        public string telefono { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Solo se permiten letras")]
        public string genero { get; set; }

        [Required]
        [StringLength(30)]
        public string tipo { get; set; }

        public string discapacidad { get; set; }

        public string nombre_empresa { get; set; }

        public string actividad { get; set; }

        [MinLength(12)]
        [MaxLength(13)]
        [RegularExpression("^([A-ZÑ&]{3,4}) ?(?:- ?)?(\\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\\d|3[01])) ?(?:- ?)?([A-Z\\d]{2})([A\\d])$", ErrorMessage = "RFC Invalido")]
        public string rfc { get; set; }

        public string comentarios { get; set; }
    }
}