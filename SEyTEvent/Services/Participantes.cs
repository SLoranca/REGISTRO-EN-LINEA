using Dapper;
using Newtonsoft.Json;
using SEyTEvent.Models;
using System;
using System.Data;
using System.Linq;

namespace SEyTEvent.Services
{
    public class Participantes
    {
        public Response participante_ins_completo(ModelParticipante model, string nombre_evento, string no_evento, string ipCliente, string dnsCLiente, string webCliente)
        {
            Response result = new Response();
            DbContext ctx = new DbContext();
            IDbConnection cnn = ctx.Get();

            try
            {
                var _params = new DynamicParameters();

                _params.Add("@municipio_id", model.muncipio_id);
                _params.Add("@nombre_completo", model.nombre_completo);
                _params.Add("@edad", model.edad);
                _params.Add("@curp", model.curp);
                _params.Add("@correo", model.correo);
                _params.Add("@telefono", model.telefono);
                _params.Add("@genero", model.genero);
                _params.Add("@tipo", model.tipo);
                _params.Add("@discapacidad", model.discapacidad);
                _params.Add("@nombre_emp", model.nombre_empresa);
                _params.Add("@actividad", model.actividad);
                _params.Add("@rfc", model.rfc);
                _params.Add("@comentarios", model.comentarios);
                _params.Add("@ip_client", ipCliente);
                _params.Add("@dns_client", dnsCLiente);
                _params.Add("@web_client", webCliente);
                _params.Add("@nom_evento", nombre_evento);
                _params.Add("@no_evento", no_evento);

                var registro = Dapper.SqlMapper.Query<Response>(cnn, "participantes_ins_complete", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
                result.status = registro.status;
                result.message = registro.message;
                result.data = registro.data;
            }
            catch (Exception e)
            {
                result.status = "ERROR";
                result.message = e.Message.ToString();
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }

        public Response participante_upd_completo(ModelParticipante model, string folio)
        {
            Response result = new Response();
            DbContext ctx = new DbContext();
            IDbConnection cnn = ctx.Get();

            try
            {
                var _params = new DynamicParameters();

                _params.Add("@folio", folio);
                _params.Add("@municipio_id", model.muncipio_id);
                _params.Add("@nombre_completo", model.nombre_completo);
                _params.Add("@edad", model.edad);
                _params.Add("@curp", model.curp);
                _params.Add("@correo", model.correo);
                _params.Add("@telefono", model.telefono);
                _params.Add("@genero", model.genero);
                _params.Add("@tipo", model.tipo);
                _params.Add("@discapacidad", model.discapacidad);
                _params.Add("@nombre_emp", model.nombre_empresa);
                _params.Add("@actividad", model.actividad);
                _params.Add("@rfc", model.rfc);
                _params.Add("@comentarios", model.comentarios);

                var registro = Dapper.SqlMapper.Query<Response>(cnn, "participantes_upd_complete", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
                result.status = registro.status;
                result.message = registro.message;
                result.data = registro.data;
            }
            catch (Exception e)
            {
                result.status = "ERROR";
                result.message = e.Message.ToString();
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }

        public Response participante_ins_rapido(ModelParticipante model,string nombre_evento, string no_evento, string ipCliente, string dnsCLiente, string webCliente)
        {
            Response result = new Response();
            DbContext ctx = new DbContext();
            IDbConnection cnn = ctx.Get();

            try
            {
                var _params = new DynamicParameters();

                _params.Add("@nombre_completo", model.nombre_completo);
                _params.Add("@correo", model.correo);
                _params.Add("@telefono", model.telefono);
                _params.Add("@genero", model.genero);
                _params.Add("@tipo", model.tipo);
                _params.Add("@ip_client", ipCliente);
                _params.Add("@dns_client", dnsCLiente);
                _params.Add("@web_client", webCliente);
                _params.Add("@nom_evento", nombre_evento);
                _params.Add("@no_evento", no_evento);

                var registro = Dapper.SqlMapper.Query<Response>(cnn, "participantes_ins_rapido", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
                result.status = registro.status;
                result.message = registro.message;
                result.data = registro.data;
            }
            catch (Exception e)
            {
                result.status = "ERROR";
                result.message = e.Message.ToString();
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }

        public Response participante_get_info(string folio)
        {
            Response result = new Response();
            DbContext ctx = new DbContext();
            IDbConnection cnn = ctx.Get();

            try
            {
                var _params = new DynamicParameters();
                
                _params.Add("@folio", folio);

                var registro = Dapper.SqlMapper.Query<dynamic>(cnn, "participante_get_info", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
                result.status = "OK";
                result.data = JsonConvert.SerializeObject(registro);
            }
            catch (Exception e)
            {
                result.status = "ERROR";
                result.message = e.Message.ToString();
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }

        public Response participante_conferencia_add(int participante_id , int conferencia_id)
        {
            Response result = new Response();
            DbContext ctx = new DbContext();
            IDbConnection cnn = ctx.Get();

            try
            {
                var _params = new DynamicParameters();

                _params.Add("@participante_id", participante_id);
                _params.Add("@conferencia_id", conferencia_id);
                var registro = Dapper.SqlMapper.Query<Response>(cnn, "participante_conferencia_add", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
                result.status = registro.status;
                result.message = registro.message;
                result.data = JsonConvert.SerializeObject(registro);
            }
            catch (Exception e)
            {
                result.status = "ERROR";
                result.message = e.Message.ToString();
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }

        public Response participante_conferencia_reg_get(int participante_id)
        {
            Response result = new Response();
            DbContext ctx = new DbContext();
            IDbConnection cnn = ctx.Get();

            try
            {
                var _params = new DynamicParameters();

                _params.Add("@participante_id", participante_id);

                var registro = Dapper.SqlMapper.Query<dynamic>(cnn, "participante_conferencia_reg_get", _params, commandType: CommandType.StoredProcedure).ToList();
                result.status = "OK";
                result.data = JsonConvert.SerializeObject(registro);
            }
            catch (Exception e)
            {
                result.status = "ERROR";
                result.message = e.Message.ToString();
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }

        public Response participante_talleres_add(int participante_id, int taller_id)
        {
            Response result = new Response();
            DbContext ctx = new DbContext();
            IDbConnection cnn = ctx.Get();

            try
            {
                var _params = new DynamicParameters();

                _params.Add("@participante_id", participante_id);
                _params.Add("@taller_id", taller_id);
                var registro = Dapper.SqlMapper.Query<Response>(cnn, "participante_taller_add", _params, commandType: CommandType.StoredProcedure).FirstOrDefault();
                result.status = registro.status;
                result.message = registro.message;
                result.data = JsonConvert.SerializeObject(registro);
            }
            catch (Exception e)
            {
                result.status = "ERROR";
                result.message = e.Message.ToString();
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }

        public Response participante_taller_reg_get(int participante_id)
        {
            Response result = new Response();
            DbContext ctx = new DbContext();
            IDbConnection cnn = ctx.Get();

            try
            {
                var _params = new DynamicParameters();

                _params.Add("@participante_id", participante_id);

                var registro = Dapper.SqlMapper.Query<dynamic>(cnn, "participante_taller_reg_get", _params, commandType: CommandType.StoredProcedure).ToList();
                result.status = "OK";
                result.data = JsonConvert.SerializeObject(registro);
            }
            catch (Exception e)
            {
                result.status = "ERROR";
                result.message = e.Message.ToString();
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }

        public Response panel_get_info()
        {
            Response result = new Response();
            DbContext ctx = new DbContext();
            IDbConnection cnn = ctx.Get();

            try
            {
                var registro = Dapper.SqlMapper.Query<dynamic>(cnn, "panel_admin", null, commandType: CommandType.StoredProcedure).FirstOrDefault();
                result.status = "OK";
                result.data = JsonConvert.SerializeObject(registro);
            }
            catch (Exception e)
            {
                result.status = "ERROR";
                result.message = e.Message.ToString();
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }
    }
}