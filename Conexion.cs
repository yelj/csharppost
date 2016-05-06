using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LoginDB
{
    public abstract class Conexion
    {
        public string cadenaConexion;
        protected string sentenciaSql;
        protected int resultado;
        protected SqlConnection conexionMsSql;
        protected SqlCommand comandoSql;
        protected string mensaje;
        protected SqlDataReader registros;

        public Conexion()
        {
            this.cadenaConexion = (@"Data Source = GLENN-PC; Initial Catalog = Clinica; Integrated Security = True");
            this.conexionMsSql = new SqlConnection(this.cadenaConexion);
            this.registros = null;
            this.sentenciaSql = string.Empty;
        }

        public string Mensaje
        {
            get
            {
                return this.mensaje;
            }
        }


    }
}
