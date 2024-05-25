using System.ComponentModel.DataAnnotations;

namespace SEyTEvent.Models
{
    public class ModelParticipante : Response
    {
        public int id { get; set; }

        public int muncipio_id { get; set; }

        public string nombre_completo { get; set; }

        public string edad { get; set; }

        public string curp { get; set; }

        public string correo { get; set; }

        public string telefono { get; set; }

        public string genero { get; set; }

        public string tipo { get; set; }

        public string discapacidad { get; set; }

        public string nombre_empresa { get; set; }

        public string actividad { get; set; }

        public string rfc { get; set; }

        public string comentarios { get; set; }
    }
}