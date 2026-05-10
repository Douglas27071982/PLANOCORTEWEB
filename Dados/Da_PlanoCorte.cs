using Oracle.ManagedDataAccess.Client;
using Otimizador_PC_Web.Models;
using System.Data;
using System.Data.SqlClient;


namespace Otimizador_PC_Web.Dados
{
    public class Da_PlanoCorte
    {

        private static Da_PlanoCorte instancia = null;


        public Da_PlanoCorte()
        {
        }

        public static Da_PlanoCorte Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new Da_PlanoCorte();
                }
                return instancia;
            }

        }


      

        public bool Limpar()
        {
            bool respuesta = true;
            var cn = new Conexao();

            using (var conexion = new SqlConnection(cn.getConexaoSQL()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_EXCLUIR_TEMP_ARQUIVO", conexion);
                    
                   
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();

                    cmd.ExecuteNonQuery();

                        //respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }
       

        public bool SalvarPlano(PlanoCorte obj)
        {
            bool respuesta = true;
            var cn = new Conexao();

            using (var conexion = new SqlConnection(cn.getConexaoSQL()))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("SP_REGISTRAR_PLANO", conexion);
                    cmd.Parameters.AddWithValue("CODIGO_PLANO", obj.Codplano);
                    cmd.Parameters.AddWithValue("SEQUENCIA", obj.Seq);
                    cmd.Parameters.AddWithValue("DESCRIÇÃO_PLANO", obj.Descplano);
                    cmd.Parameters.AddWithValue("CHAPA", obj.Chapa);
                    cmd.Parameters.AddWithValue("MAQUINA", obj.Maquina);
                    cmd.Parameters.AddWithValue("TIPO", obj.Tipo);
                    cmd.Parameters.AddWithValue("NUMERO_PEÇAS", obj.Numpecas);
                    cmd.Parameters.AddWithValue("M3", obj.M3);
                    cmd.Parameters.AddWithValue("CICLOS_H", obj.Ciclosh);
                    cmd.Parameters.AddWithValue("CICLOS_V", obj.Ciclosv);
                    cmd.Parameters.AddWithValue("APROVEITAMENTO", obj.Aproveitamento);
                    cmd.Parameters.AddWithValue("SITUAÇÃO", obj.Situacao);
                    cmd.Parameters.AddWithValue("CODIGO_CHAPA", obj.Codchapa);
                    cmd.Parameters.AddWithValue("MULTIPLICADOR", obj.Multiplicador);

                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

       
        public List<PlanoCorte> ListarPlanoCorte()

        {

            List<PlanoCorte> lista = new List<PlanoCorte>();
            var cn = new Conexao();


            using (var conexion = new SqlConnection(cn.getConexaoSQL()))
            {

                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_MOSTRAR_PLANO_CORTE", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new PlanoCorte()
                        {
                            Codplano = Convert.ToInt32(dr["CODIGO_PLANO"]),
                            Descplano = dr["DESCRIÇÃO_PLANO"].ToString(),
                            Tipo = dr["TIPO"].ToString(),
                            Chapa = dr["CHAPA"].ToString(),
                           
                            Situacao = Convert.ToInt32(dr["SITUAÇÃO"]),
                         
                        });
                    }
                }



                return lista;
            }

        }

        public List<PlanoCorte> ListarPlanoCorteMan(string codigo)

        {

            List<PlanoCorte> lista = new List<PlanoCorte>();
            var cn = new Conexao();


            using (var conexion = new SqlConnection(cn.getConexaoSQL()))
            {

                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_MOSTRAR_PLANO_CORTE_MAN", conexion);
                cmd.Parameters.AddWithValue("@CODIGOPLANO", codigo);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new PlanoCorte()
                        {
                            Codplano = Convert.ToInt32(dr["CODIGO_PLANO"]),
                            Descplano = dr["DESCRIÇÃO_PLANO"].ToString(),
                            Tipo = dr["TIPO"].ToString(),
                            Chapa = dr["CHAPA"].ToString(),
                            Seq = Convert.ToInt32(dr["SEQUENCIA"]),
                            Codmaquina = Convert.ToInt32(dr["COD_MAQUINA"]),
                            Maquina = dr["MAQUINA"].ToString(),
                            Numpecas = Convert.ToInt32(dr["NUMERO_PEÇAS"]),
                            M3 = dr["M3"].ToString(),
                            Ciclosh = Convert.ToInt32(dr["CICLOS_H"]),
                            Ciclosv = Convert.ToInt32(dr["CICLOS_V"]),
                            Aproveitamento = dr["APROVEITAMENTO"].ToString(),
                            Tempototal = dr["TEMPO_TOTAL"].ToString(),
                            Qtdechapaciclo = Convert.ToInt32(dr["QTDE_CHAPA_CICLO"]),
                            Codchapa = Convert.ToInt32(dr["CODIGO_CHAPA"]),
                            Situacao = Convert.ToInt32(dr["SITUAÇÃO"]),
                            Multiplicador = Convert.ToInt32(dr["MULTIPLICADOR"]),

                        });
                    }
                }



                return lista;
            }

        }
        public bool ExcluirPlano(int codplano)
        {
            bool respuesta = true;
            var cn = new Conexao();

            using (var conexion = new SqlConnection(cn.getConexaoSQL()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_EXCLUIR_PLANO_CORTE", conexion);
                    cmd.Parameters.AddWithValue("@CODIGOPLANO", codplano);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }


        public List<PlanoCorte> ListarImagemPlano(string codigo = null)
        {
            var oLista = new List<PlanoCorte>();
            var cn = new Conexao();

            using (var conexion = new SqlConnection(cn.getConexaoSQL()))
            {
                conexion.Open();
                string query = "select CODIGO_PLANO,DESCRIÇÃO_PLANO,IMAGEM_PLANO from TB_CADASTRO_PLANO";
                if (!string.IsNullOrEmpty(codigo))
                {
                    query += " WHERE CODIGO_PLANO = @codigo";
                }

                SqlCommand cmd = new SqlCommand(query, conexion);

                if (!string.IsNullOrEmpty(codigo))
                {
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                }

                cmd.CommandType = CommandType.Text;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new PlanoCorte()
                        {
                            Codplano = Convert.ToInt32(dr["CODIGO_PLANO"]),
                            Descplano = dr["DESCRIÇÃO_PLANO"].ToString(),
                            Imgplano = dr["IMAGEM_PLANO"] as byte[],
                        });
                    }
                }
            }

            return oLista;
        }



        public bool EditarPlano(PlanoCorte obj)
        {
            bool respuesta = true;
            var cn = new Conexao();

            using (var conexion = new SqlConnection(cn.getConexaoSQL()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_MODIFICA_PLANO_CORTE", conexion);
                    cmd.Parameters.AddWithValue("CODIGOPLANO", obj.Codplano);
                    cmd.Parameters.AddWithValue("DESCRIÇÃO_PLANO", obj.Descplano);
                    cmd.Parameters.AddWithValue("TIPO", obj.Tipo);
                    cmd.Parameters.AddWithValue("SEQ", obj.Seq);
                    cmd.Parameters.AddWithValue("CICLOSH", obj.Ciclosh);
                    cmd.Parameters.AddWithValue("CICLOSV", obj.Ciclosv);
                    cmd.Parameters.AddWithValue("SITUACAO", obj.Situacao);
                    cmd.Parameters.AddWithValue("MULTIPLICADOR", obj.Multiplicador);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }
        public List<PlanoCorte> ListarItensFocco(string CODIGO)

        {

            List<PlanoCorte> lista = new List<PlanoCorte>();
            var cn = new Conexao();




            using (var conexion = new OracleConnection(cn.getConexaoFoccoOtimiza()))
            {

                conexion.Open();
                OracleCommand cmd = new OracleCommand("SELECT ID, DESC_OPER FROM JFL_DADOS_ROT_ESP WHERE ID ='" + CODIGO + "'", conexion);
                cmd.CommandType = CommandType.Text;


                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new PlanoCorte()
                        {
                            Codigoitem = Convert.ToInt32(dr["ID"]),
                            Descricao = dr["DESC_OPER"].ToString(),



                        });
                    }

                }
                return lista;
            }

        }

        public bool SalvarItens(PlanoCorte obj)
        {
            bool respuesta = true;
            var cn = new Conexao();

            using (var conexion = new SqlConnection(cn.getConexaoSQL()))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("SP_REGISTRAR_ITEM_PLANO", conexion);
                    cmd.Parameters.AddWithValue("CODIGO_PLANO", obj.Codplano);
                    cmd.Parameters.AddWithValue("CODIGO_ITEM", obj.Codigoitem);
                    cmd.Parameters.AddWithValue("DESCRIÇÃO_ITEM", obj.Descricao);
                    cmd.Parameters.AddWithValue("QTDE_ITEM", obj.Qtdeitem);
                    cmd.Parameters.AddWithValue("S", obj.Simples);
                    cmd.Parameters.AddWithValue("D", obj.Dupla);
                   
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool ExcluirItemPlano(int id)
        {
            bool respuesta = true;
            var cn = new Conexao();

            using (var conexion = new SqlConnection(cn.getConexaoSQL()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_EXCLUIR_ITENS_PLANO", conexion);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }

        public bool EditarItens(PlanoCorte obj)
        {
            bool respuesta = true;
            var cn = new Conexao();

            using (var conexion = new SqlConnection(cn.getConexaoSQL()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_MODIFICA_ITENS_PLANO", conexion);
                    cmd.Parameters.AddWithValue("CODIGO_PLANO", obj.Codplano);
                    cmd.Parameters.AddWithValue("CODIGO_ITEM", obj.Codigoitem);

                    cmd.Parameters.AddWithValue("QTDE", obj.Qtdeitem);
                    cmd.Parameters.AddWithValue("S", obj.Simples);
                    cmd.Parameters.AddWithValue("D", obj.Dupla);
                    
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }





        public DataSet GerarRelatorioPlano(string id)
        {
            var cn = new Conexao();
            var ds = new DataSet(); // Cria um DataSet para armazenar múltiplas tabelas
            using (var con = new SqlConnection(cn.getConexaoSQL()))
            {
                // Comando para o primeiro procedimento armazenado (tabela 1)
                SqlCommand cmd1 = new SqlCommand("SP_MOSTRAR_PLANO_CORTE_MAN", con);
                cmd1.Parameters.AddWithValue("@CODIGOPLANO", id);
                cmd1.CommandType = CommandType.StoredProcedure;

                // Preenche o primeiro DataTable (para Dtset)
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                da1.Fill(ds, "Dtset"); // "Dtset" é o nome da tabela no DataSet

                // Comando para o segundo procedimento armazenado (tabela 2)
                SqlCommand cmd2 = new SqlCommand("SP_MOSTRAR_ITENS_PLANO", con);
                cmd2.Parameters.AddWithValue("@CODIGOPLANO", id);
                cmd2.CommandType = CommandType.StoredProcedure;

                // Preenche o segundo DataTable (para Dtset1)
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(ds, "Dtset1"); // "Dtset1" é o nome da tabela no DataSet

                // Comando para o terceiro procedimento armazenado (tabela 3)
                SqlCommand cmd3 = new SqlCommand("SP_MOSTRAR_RETALHO_PLANO", con);
                cmd3.Parameters.AddWithValue("@CODIGOPLANO", id);
                cmd3.CommandType = CommandType.StoredProcedure;

                // Preenche o segundo DataTable (para Dtset1)
                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                da3.Fill(ds, "Dtset2"); // "Dtset1" é o nome da tabela no DataSet
            }

            return ds; // Retorna o DataSet preenchido com as duas tabelas
        }


        public List<PlanoCorte> ListarItens(string Codigo)

        {

            List<PlanoCorte> lista = new List<PlanoCorte>();
            var cn = new Conexao();


            using (var conexion = new SqlConnection(cn.getConexaoSQL()))
            {

                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_MOSTRAR_IMPLOSAO_PLANOS", conexion);
                cmd.Parameters.AddWithValue("CODITEM", Codigo);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new PlanoCorte()
                        {
                            Codigoitem = Convert.ToInt32(dr["CODIGO_ITEM"]),
                            Descricao = dr["DESCRIÇÃO_ITEM"].ToString(),
                            Codplano = Convert.ToInt32(dr["CODIGO_PLANO"]),
                            Tipo = dr["TIPO"].ToString(),
                            Chapa = dr["CHAPA"].ToString(),

                        });
                    }
                }



                return lista;
            }

        }


        public List<PlanoCorte> ListarArquivo(int CODPLANO)

        {

            List<PlanoCorte> lista = new List<PlanoCorte>();
            var cn = new Conexao();


            using (var conexion = new SqlConnection(cn.getConexaoSQL()))
            {

                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_TESTAR_ARQUIVO_PLANO", conexion);
                cmd.Parameters.AddWithValue("CODPLANO", CODPLANO);
              
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new PlanoCorte()
                        {
                           
                            Linha  = dr["LINHA"].ToString(),
                            

                        });
                    }
                }



                return lista;
            }

        }

        

       

        public bool ExcluirLinhaArquivo(int id)
        {
            bool respuesta = true;
            var cn = new Conexao();

            using (var conexion = new SqlConnection(cn.getConexaoSQL()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_EXCLUIR_LINHA_ARQUIVO", conexion);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }

        public List<PlanoCorte> ListarDados(string CODIGO, string ITENS)

        {

            List< PlanoCorte> lista = new List<PlanoCorte>();
            var cn = new Conexao();


            using (var conexion = new SqlConnection(cn.getConexaoSQL()))
            {

                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_MOSTRAR_CHAPA_PLANO", conexion);
                cmd.Parameters.AddWithValue("@CODIGO", CODIGO);
                cmd.Parameters.AddWithValue("@ITENS", ITENS);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new PlanoCorte()
                        {
                            Codplano = Convert.ToInt32(dr["CODIGO_PLANO"]),
                            Descplano = dr["DESCRIÇÃO_PLANO"].ToString(),
                           
                            Tipo = dr["TIPO"].ToString(),
                            

                        });
                    }
                }



                return lista;
            }

        }
        public bool SubstituirChapa(string CODIGOATUAL, string CODIGONOVO, string PLANOS)
        {
            bool respuesta = true;
            var cn = new Conexao();

            using (var conexion = new SqlConnection(cn.getConexaoSQL()))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("SP_MODIFICA_CHAPA_PLANO", conexion);
                    cmd.Parameters.AddWithValue("CODIGO_ATUAL", CODIGOATUAL);
                    cmd.Parameters.AddWithValue("CODIGO_NOVO", CODIGONOVO);
                    cmd.Parameters.AddWithValue("PLANOS", PLANOS);
                    

                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

    }
}
