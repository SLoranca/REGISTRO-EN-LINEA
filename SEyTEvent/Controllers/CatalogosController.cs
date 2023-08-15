using SEyTEvent.Models;
using SEyTEvent.Services;
using System.Web.Mvc;

namespace SEyTEvent.Controllers
{
    public class CatalogosController : Controller
    {
        public JsonResult GetMunicipios()
        {
            Catalogos catalogos = new Catalogos();
            Response result = catalogos.get_municipios();
            return Json(result);
        }

        public JsonResult GetConferencias()
        {
            Catalogos catalogos = new Catalogos();
            Response result = catalogos.get_conferencias();
            return Json(result);
        }

        public JsonResult GetTalleres()
        {
            Catalogos catalogos = new Catalogos();
            Response result = catalogos.get_talleres();
            return Json(result);
        }
    }
}