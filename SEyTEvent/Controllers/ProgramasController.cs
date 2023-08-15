using SEyTEvent.Models;
using SEyTEvent.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEyTEvent.Controllers
{
    public class ProgramasController : Controller
    {
        public JsonResult ParticipanteConferenciaAdd(int participante_id, int conferencia_id)
        {
            Participantes participantes = new Participantes();
            Response result = participantes.participante_conferencia_add(participante_id, conferencia_id);
            return Json(result);
        }

        public JsonResult ParticipanteConferenciaRegistroGet(int participante_id)
        {
            Participantes participantes = new Participantes();
            Response result = participantes.participante_conferencia_reg_get(participante_id);
            return Json(result);
        }

        public JsonResult ParticipanteTallerAdd(int participante_id, int taller_id)
        {
            Participantes participantes = new Participantes();
            Response result = participantes.participante_talleres_add(participante_id, taller_id);
            return Json(result);
        }

        public JsonResult ParticipanteTallerRegistroGet(int participante_id)
        {
            Participantes participantes = new Participantes();
            Response result = participantes.participante_taller_reg_get(participante_id);
            return Json(result);
        }
    }
}