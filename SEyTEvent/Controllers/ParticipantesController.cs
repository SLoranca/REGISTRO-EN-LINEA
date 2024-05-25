using SEyTEvent.Models;
using SEyTEvent.Services;
using System;
using System.Web.Mvc;

namespace SEyTEvent.Controllers
{
    public class ParticipantesController : Controller
    {
        public JsonResult GetParticipanteInfo(string folio)
        {
            Participantes participantes = new Participantes();
            Response result = participantes.participante_get_info(folio);
            return Json(result);
        }

        public JsonResult GetPanelAdmin()
        {
            Participantes participantes = new Participantes();
            Response result = participantes.panel_get_info();
            return Json(result);
        }

        public JsonResult RegistoCompleto(ModelParticipante model, string folio, string mode, string no_evento, string nombre_evento)
        {
            Response response = new Response();

            try
            {
                if (model.muncipio_id == 0) {
                    response.status = "ERROR";
                    response.data = null;
                    response.message = "El valor del municipio debe ser mayor a 0";
                    return Json(response);
                }

                if (model.genero == "SEL")
                {
                    response.status = "ERROR";
                    response.data = null;
                    response.message = "El valor del genero debe ser diferente de [SELECCIONE]";
                    return Json(response);
                }

                if (model.tipo == "SEL")
                {
                    response.status = "ERROR";
                    response.data = null;
                    response.message = "El valor del tipo de persona debe ser diferente de [SELECCIONE]";
                    return Json(response);
                }

                if (model.discapacidad == "SEL")
                {
                    response.status = "ERROR";
                    response.data = null;
                    response.message = "El valor de discapacidad debe ser diferente de [SELECCIONE]";
                    return Json(response);
                }

                if (nombre_evento == "" || nombre_evento == null)
                {
                    response.status = "ERROR";
                    response.data = null;
                    response.message = "Debe proporcionar el nombre del evento.";
                    return Json(response);
                }

                if (no_evento == "" || no_evento == null)
                {
                    response.status = "ERROR";
                    response.data = null;
                    response.message = "Debe proporcionar el número del evento.";
                    return Json(response);
                }

                if (ModelState.IsValid)
                {
                    Participantes participante = new Participantes();

                    if (mode == "INSERT")
                    {
                        string userIP = Request.UserHostAddress;
                        string useAgent = Request.UserAgent.ToString().ToLower();
                        string useDNS = Request.UserHostName.ToString();

                        response = participante.participante_ins_completo(model, nombre_evento, no_evento, userIP, useDNS, useAgent);
                    }
                    else
                    {
                        response = participante.participante_upd_completo(model, folio);
                    }
                }
                else
                {
                    response.status = "ERROR";
                    response.data = null;
                    response.message = "Modelo invalido, algún campo no esta correctamente capturado, porfavor verifique su captura";
                }
            }
            catch (Exception ex)
            {
                response.status = "ERROR";
                response.data = null;
                response.message = ex.Message.ToString();
            }

            return Json(response);
        }

        public JsonResult RegistoRapido(ModelParticipante model, string no_evento, string nombre_evento)
        {
            Response response = new Response();

            try
            {
                if (model.genero == "SEL")
                {
                    response.status = "ERROR";
                    response.data = null;
                    response.message = "El valor del genero debe ser diferente de [SELECCIONE]";
                    return Json(response);
                }

                if (model.tipo == "SEL")
                {
                    response.status = "ERROR";
                    response.data = null;
                    response.message = "El valor del tipo de persona debe ser diferente de [SELECCIONE]";
                    return Json(response);
                }

                if (nombre_evento == "" || nombre_evento == null)
                {
                    response.status = "ERROR";
                    response.data = null;
                    response.message = "Debe proporcionar el nombre del evento.";
                    return Json(response);
                }

                if (no_evento == "" || no_evento == null)
                {
                    response.status = "ERROR";
                    response.data = null;
                    response.message = "Debe proporcionar el número del evento.";
                    return Json(response);
                }

                if (ModelState.IsValid)
                {
                    Participantes participante = new Participantes();

                    string userIP = Request.UserHostAddress;
                    string useAgent = Request.UserAgent.ToString().ToLower();
                    string useDNS = Request.UserHostName.ToString();

                    response = participante.participante_ins_rapido(model, nombre_evento, no_evento, userIP, useDNS, useAgent);

                    if (response.status == "OK")
                    {
                        ModelEmail email = new ModelEmail(response.data, "");
                        SendEmail sendEmail = new SendEmail();
                        response = sendEmail._SendEmail(model.correo, email.asunto, email.body, response.data);
                    }
                }
                else
                {
                    response.status = "ERROR";
                    response.data = null;
                    response.message = "Modelo invalido, algún campo no esta correctamente capturado, porfavor verifique su captura";
                }
            }
            catch (Exception ex)
            {
                response.status = "ERROR";
                response.data = null;
                response.message = ex.Message.ToString();
            }

            return Json(response);
        }

        public JsonResult EnviarCorreo(string folio, string emailDestino, string tblTalleres)
        {
            Response response = new Response();

            try
            {
                if (tblTalleres == "" || tblTalleres == null)
                {
                    //response.status = "ERROR";
                    //response.data = null;
                    //response.message = "No se encontro el body de la tabla talleres";
                    //return Json(response);
                    tblTalleres = " ";
                }

                if (folio == "" || folio == null)
                {
                    response.status = "ERROR";
                    response.data = null;
                    response.message = "El folio llego vacio, porfavor reviselo";
                    return Json(response);
                }

                if (emailDestino == "" || emailDestino == null)
                {
                    response.status = "ERROR";
                    response.data = null;
                    response.message = "El correo llego vacio, porfavor reviselo";
                    return Json(response);
                }

                ModelEmail email = new ModelEmail(folio, tblTalleres);
                SendEmail sendEmail = new SendEmail();
                response = sendEmail._SendEmail(emailDestino, email.asunto, email.body, folio);
            }
            catch (Exception ex)
            {
                response.status = "ERROR";
                response.data = null;
                response.message = ex.Message.ToString();
            }

            return Json(response);
        }

    }
}