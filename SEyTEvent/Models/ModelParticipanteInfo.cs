using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEyTEvent.Models
{
    public class ModelParticipanteInfo : Response
    {
        public int id { get; set; }
        public string nombre_completo { get; set; }
        public string edad { get; set; }
        public string curp { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string genero { get; set; }
        public string tipo { get; set; }
        public string folio { get; set; }
    }
}