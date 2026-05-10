

namespace Otimizador_PC_Web.Dados
{
    public class Conexao

    {


        private string ConexaoSQL = string.Empty;
        private string ConexaoSQL1 = string.Empty;
        private string ConexaoFocco = string.Empty;
        private string ConexaoFoccoOtimiza = string.Empty;

        public Conexao()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            ConexaoSQL = builder.GetSection("ConnectionStrings:ConexaoSQL").Value;

            var builder3 = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            ConexaoSQL1 = builder.GetSection("ConnectionStrings3:ConexaoSQL1").Value;

            var builder1 = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            ConexaoFocco = builder.GetSection("ConnectionStrings1:ConexaoFocco").Value;

            var builder2 = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            ConexaoFoccoOtimiza = builder.GetSection("ConnectionStrings2:ConexaoFoccoOtimiza").Value;

        }

        public string getConexaoSQL()
        {
            return ConexaoSQL;
        }

        public string getConexaoFocco()
        {
            return ConexaoFocco;
        }

        public string getConexaoFoccoOtimiza()
        {
            return ConexaoFoccoOtimiza;
        }

        public string getConexaoSQL1()
        {
            return ConexaoSQL1;
        }



    }










}
