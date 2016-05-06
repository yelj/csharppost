using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LoginDB
{
    class Usuarios : Conexion
    {
        private string usuario;
        private string contrasena;

        public Usuarios()
        {
            usuario = string.Empty;
            contrasena = string.Empty;
            this.sentenciaSql = string.Empty;
        }

        public string Usuario
        {
            get { return this.usuario; }
            set { this.usuario = value; }
        }

        public string Contrasena
        {
            get { return this.contrasena; }
            set { this.contrasena = value; }
        }


        public bool buscar()
        {
            bool resultado = false;
            this.sentenciaSql = string.Format(@"SELECT id_usuario from Usuario WHERE Usuario='{0}' AND Contrasena='{1}'", this.usuario,this.contrasena);
            this.comandoSql = new SqlCommand(this.sentenciaSql,this.conexionMsSql);
            this.conexionMsSql.Open();
            SqlDataReader dataReader = null;
            dataReader = this.comandoSql.ExecuteReader();

                if(dataReader.Read())
                {
                    resultado = true;
                    this.mensaje = "Bienvenido!";
                }
                else
                {
                    this.mensaje = "Usuario/Contrasena incorrecta verifique por favor.";
                }

            this.conexionMsSql.Close();
            return resultado;
        }
    }
}
