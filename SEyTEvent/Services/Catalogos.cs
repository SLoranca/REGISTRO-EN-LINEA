using Newtonsoft.Json;
using SEyTEvent.Models;
using System;
using System.Data;
using System.Linq;

namespace SEyTEvent.Services
{
    public class Catalogos
    {
        public Response get_municipios()
        {
            Response result = new Response();
            DbContext ctx = new DbContext();
            IDbConnection cnn = ctx.Get();

            try
            {
                var registro = Dapper.SqlMapper.Query<dynamic>(cnn, "cat_municipios_get", null, commandType: CommandType.StoredProcedure).ToList();
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

        public Response get_conferencias()
        {
            Response result = new Response();
            DbContext ctx = new DbContext();
            IDbConnection cnn = ctx.Get();

            try
            {
                var registro = Dapper.SqlMapper.Query<dynamic>(cnn, "cat_conferencias_get", null, commandType: CommandType.StoredProcedure).ToList();
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

        public Response get_talleres()
        {
            Response result = new Response();
            DbContext ctx = new DbContext();
            IDbConnection cnn = ctx.Get();

            try
            {
                var registro = Dapper.SqlMapper.Query<dynamic>(cnn, "cat_talleres_get", null, commandType: CommandType.StoredProcedure).ToList();
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