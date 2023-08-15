using Newtonsoft.Json;
using SEyTEvent.Models;
using SEyTEvent.Services;
using System.Web.Mvc;

namespace SEyTEvent.Controllers
{
    public class SEFIEDController : Controller
    {
        public ActionResult Registro()
        {
            return View();
        }

        public ActionResult RegistroExitoso(string folio)
        {
            Participantes participantes = new Participantes();
            Response result = participantes.participante_get_info(folio);
            ModelParticipanteInfo model = new ModelParticipanteInfo();

            model = JsonConvert.DeserializeObject<ModelParticipanteInfo>(result.data);

            if (model.status == "OK")
            {
                ViewBag.nombre_completo = model.nombre_completo;
                ViewBag.edad = model.edad;
                ViewBag.curp = model.curp;
                ViewBag.correo = model.correo;
                ViewBag.telefono = model.telefono;
                ViewBag.genero = model.genero;
                ViewBag.tipo = model.tipo;
                ViewBag.folio = model.folio;
                ViewBag.status = model.status;
            }
            else
            {
                ViewBag.status = model.status;
                ViewBag.message = model.message;
            }


            return View();
        }
    }
}