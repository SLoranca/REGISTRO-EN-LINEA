using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SEyTEvent.Services
{
    public class DbContext
    {
        private string conexion = ConfigurationManager.ConnectionStrings["BDConn"].ToString();

        /// <summary>
        /// Obtiene la conexión a la base de datos.
        /// </summary>
        /// <returns></returns>
        public IDbConnection Get()
        {
            IDbConnection conn = new SqlConnection(conexion);
            conn.Open();
            return conn;
        }
    }
}