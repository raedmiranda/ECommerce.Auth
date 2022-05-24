using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace ECommerce.Auth.Models
{
    public class UsuarioDAO
    {
        #region singleton
        private static readonly UsuarioDAO _instancia = new UsuarioDAO();
        public static UsuarioDAO Instancia { get { return _instancia; } }
        #endregion singleton

        internal bool Login(UsuarioModel model)
        {
            SqlCommand cmd = null;
            bool valido = false;
            try
            {
                SqlConnection cn = new SqlConnection("Data Source=IBLAPELIMC00703\\SQLEXPRESS;Initial Catalog=ECommerce;Integrated Security=True");
                cmd = new SqlCommand("DevolverUsuario", cn);
                cmd.Parameters.AddWithValue("@usuario", model.NombreUsuario);
                cmd.Parameters.AddWithValue("@contrasena", model.Clave);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    valido = true;
                }
            }
            catch (Exception ex)
            {
                valido = false;
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return valido;
        }

    }
}
