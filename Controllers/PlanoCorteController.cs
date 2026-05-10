using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;
using Otimizador_PC_Web.Dados;
using Otimizador_PC_Web.Models;
using System.Data;
using System.Text;

namespace Otimizador_PC_Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;

    [Authorize]

    public class PlanoCorteController : Controller
    {
        Da_PlanoCorte _daplanocorte = new Da_PlanoCorte();// GET: MotivoParada

       



        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnv;

        public PlanoCorteController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnv)
        {
            _logger = logger;
            _webHostEnv = webHostEnv;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }



        public IActionResult PlanoCorte()
        {


            return View();
        }

        public IActionResult ManPlanoCorte()
        {


            return View();
        }
        public IActionResult SubstituicaoChapa()
        {


            return View();
        }
        public IActionResult ImplosaoPlano()
        {


            return View();
        }
       


        



       
       

        [HttpGet]
        public JsonResult ListaPlanoCorte()
        {
            List<PlanoCorte> oLista = new List<PlanoCorte>();
            oLista = _daplanocorte.ListarPlanoCorte();
            return Json(new { data = oLista });

        }
       

        

        [HttpGet]
        public JsonResult ListaPlanoCorteMan(string codigo)
        {
            List<PlanoCorte> oLista = new List<PlanoCorte>();
            oLista = _daplanocorte.ListarPlanoCorteMan(codigo);
            return Json(new { data = oLista });

        }

       

        //função para salvar novos turno ou editar
        [HttpPost]
        public JsonResult GuardarPlano([FromBody] PlanoCorte obj)
        {
            //string operacion = Request.Headers["operacion"];
            bool respuesta;


            {
                respuesta = _daplanocorte.SalvarPlano(obj);
            }



            return Json(new { respuesta = respuesta });
        }

        //função para salvar novos turno ou editar
        [HttpPost]
        public JsonResult GuardarPlanoCorte([FromBody] PlanoCorte obj)
        {

            bool respuesta;




            {
                respuesta = _daplanocorte.EditarPlano(obj);
            }



            return Json(new { respuesta = respuesta });
        }

        //função para salvar novos turno ou editar
        [HttpPost]
        public JsonResult ExluirPlano([FromBody] PlanoCorte model)
        {
            if (Request.Headers["X-HTTP-Method-Override"] == "DELETE")
            {
                try
                {
                    // Lógica para excluir o plano usando o codigoplano
                    bool respuesta = _daplanocorte.ExcluirPlano(model.Codplano);

                    return Json(new { respuesta });
                }
                catch (Exception ex)
                {
                    // Trate exceções aqui
                    return Json(new { respuesta = false });
                }
            }

            return Json(new { respuesta = false });
        }



        public IActionResult BuscarImagem(string codigo)
        {
            if (string.IsNullOrEmpty(codigo))
            {
                return Json(new { mensagem = "Código não informado" });
            }

            var cadastro = _daplanocorte.ListarImagemPlano(codigo).FirstOrDefault();

            if (cadastro == null || cadastro.Imgplano == null)
            {
                return Json(new { mensagem = "Imagem não encontrada" });
            }

            // Converte a imagem para base64
            string imagemBase64 = Convert.ToBase64String(cadastro.Imgplano);

            return Json(new { imagemBase64 });
        }

        [HttpGet]
        public JsonResult ProcurarItensFocco(string CODIGO)
        {
            List<PlanoCorte> oLista = new List<PlanoCorte>();
            oLista = _daplanocorte.ListarItensFocco(CODIGO);
            return Json(new { data = oLista });

        }


        [HttpPost]
        public JsonResult GuardarItensPlano([FromBody] PlanoCorte obj)
        {
            string operacion = Request.Headers["operacion"];
            bool respuesta;

            if (operacion == "crear")
            {

                respuesta = _daplanocorte.SalvarItens(obj);

            }
            else
            {
                respuesta = _daplanocorte.EditarItens(obj);
            }

            return Json(new { respuesta = respuesta });
        }
        [HttpPost]
        public JsonResult ExcluirItemPlano(int id)


        {
            bool respuesta = _daplanocorte.ExcluirItemPlano(id);
            return Json(new { respuesta = respuesta });
        }

        [HttpDelete]
        public JsonResult Limpar()
        {
            bool respuesta;
            respuesta = _daplanocorte.Limpar();
            return Json(new { respuesta = respuesta });
        }
       


       





        [HttpGet]
        public IActionResult GerarRelatorio(string id)
        {
            // Chama o método para gerar o DataSet (com 3 tabelas: Dtset , Dtset1 e Dtset2)
            DataSet ds = _daplanocorte.GerarRelatorioPlano(id);

            // Caminho do arquivo RDLC
            string path = $"{_webHostEnv.WebRootPath}\\reports\\RptPlanoCorte.rdlc";

            // Criação do dicionário de parâmetros (caso necessário)
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            // Criação do objeto LocalReport
            LocalReport localReport = new LocalReport(path);

            // Adiciona as duas tabelas do DataSet como fontes de dados
            localReport.AddDataSource("Dtset", ds.Tables["Dtset"]); // Adiciona a tabela "Dtset"
            localReport.AddDataSource("Dtset1", ds.Tables["Dtset1"]); // Adiciona a tabela "Dtset1"
            localReport.AddDataSource("Dtset2", ds.Tables["Dtset2"]);
            // Renderiza o relatório em PDF
            string mimeType = "";
            int extension = 1;
            var res = localReport.Execute(RenderType.Pdf, extension, parameters, mimeType);

            // Retorna o arquivo PDF gerado
            return File(res.MainStream, "application/pdf");
        }

        [HttpGet]
        public JsonResult ListaItens(string Codigo)
        {
            List<PlanoCorte> oLista = new List<PlanoCorte>();
            oLista = _daplanocorte.ListarItens(Codigo);
            return Json(new { data = oLista });




        }

        [HttpGet]
        public JsonResult GerarArquivo(int CODPLANO)
        {
            List<PlanoCorte> oLista = _daplanocorte.ListarArquivo(CODPLANO);

            string folderPath = @"C:\Engenharia\dados\ENGENHARIA\Planos de Corte - Produção\Teste Arquivos";
            string filePath = Path.Combine(folderPath, $"{CODPLANO}.AC");

            // Garante que a pasta existe
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Cria o conteúdo do arquivo
            StringBuilder sb = new StringBuilder();
            foreach (var item in oLista)
            {
                sb.AppendLine(item.Linha); // Adapte se precisar de mais colunas
            }

            // Escreve o arquivo
            System.IO.File.WriteAllText(filePath, sb.ToString());

            return Json(new { sucesso = true, mensagem = "Arquivo gerado com sucesso." });
        }

        
  

        [HttpGet]
        public JsonResult ExluirLinhaArquivo(int id)
        {
            bool respuesta;
            respuesta = _daplanocorte.ExcluirLinhaArquivo(id);
            return Json(new { respuesta = respuesta });
        }

       

        [HttpGet]
        public JsonResult ListaDados(string CODIGO,string ITENS)
        {
            List<PlanoCorte> oLista = new List<PlanoCorte>();
            oLista = _daplanocorte.ListarDados(CODIGO,ITENS);
            return Json(new { data = oLista });

        }

        [HttpPost]
        public JsonResult SubstituirChapa(string CODIGOATUAL,string CODIGONOVO,string PLANOS)
        {
            bool respuesta;
            respuesta = _daplanocorte.SubstituirChapa(CODIGOATUAL,CODIGONOVO,PLANOS);
            return Json(new { respuesta = respuesta });
        }

       

    }



}
