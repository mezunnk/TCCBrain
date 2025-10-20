using BrainFlow.UI.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BrainFlow.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Dados mock para demonstração (sem dependência do banco)
            ViewBag.CursosDestaque = GetMockCursos().Take(6).ToList();
            ViewBag.Categorias = new List<string> { "Programação", "Design", "Marketing", "Negócios", "Fotografia", "Música" };

            return View();
        }

        public IActionResult Cursos()
        {
            // Dados mock para demonstração
            ViewBag.Cursos = GetMockCursos();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<MockCursoMOD> GetMockCursos()
        {
            return new List<MockCursoMOD>
            {
                new MockCursoMOD
                {
                    CdCurso = 1,
                    NoCurso = "Introdução ao Desenvolvimento Web",
                    TxDescricao = "Aprenda os fundamentos do desenvolvimento web com HTML, CSS e JavaScript. Este curso é perfeito para iniciantes que desejam entrar no mundo da programação.",
                    DcValor = 99.90m,
                    TxCaminhoImagem = "/img/curso-web.jpg",
                    NoAfiliado = "João Silva"
                },
                new MockCursoMOD
                {
                    CdCurso = 2,
                    NoCurso = "Design Gráfico com Adobe Creative Suite",
                    TxDescricao = "Domine as ferramentas do Adobe para criar designs incríveis. Photoshop, Illustrator e InDesign serão seus aliados neste curso completo.",
                    DcValor = 149.90m,
                    TxCaminhoImagem = "/img/curso-design.jpg",
                    NoAfiliado = "Maria Santos"
                },
                new MockCursoMOD
                {
                    CdCurso = 3,
                    NoCurso = "Marketing Digital para Empreendedores",
                    TxDescricao = "Estratégias comprovadas de marketing digital para alavancar seu negócio. SEO, redes sociais, anúncios pagos e muito mais.",
                    DcValor = 79.90m,
                    TxCaminhoImagem = "/img/curso-marketing.jpg",
                    NoAfiliado = "Carlos Oliveira"
                },
                new MockCursoMOD
                {
                    CdCurso = 4,
                    NoCurso = "Fotografia Profissional com DSLR",
                    TxDescricao = "Aprenda a fotografar como um profissional. Técnicas de composição, iluminação, edição e equipamentos serão abordados.",
                    DcValor = 129.90m,
                    TxCaminhoImagem = "/img/curso-foto.jpg",
                    NoAfiliado = "Ana Costa"
                },
                new MockCursoMOD
                {
                    CdCurso = 5,
                    NoCurso = "Música Eletrônica: Produção com DAW",
                    TxDescricao = "Crie suas próprias músicas eletrônicas usando software profissional. Aprenda síntese, mixagem e masterização.",
                    DcValor = 199.90m,
                    TxCaminhoImagem = "/img/curso-musica.jpg",
                    NoAfiliado = "Pedro Lima"
                },
                new MockCursoMOD
                {
                    CdCurso = 6,
                    NoCurso = "Empreendedorismo Digital",
                    TxDescricao = "Como criar e gerenciar um negócio online de sucesso. Estratégias, ferramentas e cases reais de empreendedores digitais.",
                    DcValor = 89.90m,
                    TxCaminhoImagem = "/img/curso-negocio.jpg",
                    NoAfiliado = "Fernanda Rocha"
                },
                new MockCursoMOD
                {
                    CdCurso = 7,
                    NoCurso = "Programação em Python do Zero",
                    TxDescricao = "Curso completo de Python para iniciantes. Desde variáveis até projetos avançados como automação e análise de dados.",
                    DcValor = 0m, // Gratuito
                    TxCaminhoImagem = "/img/curso-python.jpg",
                    NoAfiliado = "Lucas Mendes"
                },
                new MockCursoMOD
                {
                    CdCurso = 8,
                    NoCurso = "UX/UI Design para Apps Mobile",
                    TxDescricao = "Crie interfaces incríveis para aplicativos móveis. Princípios de UX, prototipagem e design systems serão seus guias.",
                    DcValor = 159.90m,
                    TxCaminhoImagem = "/img/curso-ux.jpg",
                    NoAfiliado = "Beatriz Almeida"
                }
            };
        }
    }

    // Classe mock para simular os dados do curso
    public class MockCursoMOD
    {
        public int CdCurso { get; set; }
        public string NoCurso { get; set; } = string.Empty;
        public string TxDescricao { get; set; } = string.Empty;
        public decimal DcValor { get; set; }
        public string? TxCaminhoImagem { get; set; }
        public string NoAfiliado { get; set; } = string.Empty;
    }
}
