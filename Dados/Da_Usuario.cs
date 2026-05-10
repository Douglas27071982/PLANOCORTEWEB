using Otimizador_PC_Web.Models;
using System.Data;
using System.Data.SqlClient;


namespace Otimizador_PC_Web.Dados
{
    public class Da_Usuario
    {



        private static Da_Usuario instancia = null;

        public Da_Usuario()
        {
        }

        public static Da_Usuario Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new Da_Usuario();
                }
                return instancia;
            }

        }


        public List<Usuario> ListarUsuario()

        {

            List<Usuario> lista = new List<Usuario>();
            var cn = new Conexao();


            using (var conexion = new SqlConnection(cn.getConexaoSQL()))
            {

                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_LISTAR_USUARIO", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Usuario()
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            Nome = dr["Nome"].ToString(),
                            Senha = dr["Senha"].ToString(),
                            Tipo = dr["Tipo"].ToString()


                        });
                    }
                }



                return lista;
            }

        }

        public bool ExcluirUsuario(int idusuario)
        {
            bool respuesta = true;
            var cn = new Conexao();

            using (var conexion = new SqlConnection(cn.getConexaoSQL()))
            {
                // try
                // {
                SqlCommand cmd = new SqlCommand("delete from USUARIO where IdUsuario = @idusuario", conexion);
                cmd.Parameters.AddWithValue("@idusuario", idusuario);
                cmd.CommandType = CommandType.Text;

                conexion.Open();

                cmd.ExecuteNonQuery();

                respuesta = true;

                // }
                //   catch (Exception ex)
                // {
                //   respuesta = false;
                // }

            }

            return respuesta;

        }
    }
}
