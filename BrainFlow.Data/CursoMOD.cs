using System.ComponentModel.DataAnnotations;

namespace BrainFlow.Data.Models;

/// <summary>
/// Armazena os dados dos cursos criados pelos afiliados.
/// </summary>
public partial class CursoMOD
{
    /// <summary>
    /// Código único do curso. PK.
    /// </summary>
    public int CdCurso { get; set; }

    /// <summary>
    /// FK para a tabela AFILIADO, indicando o autor do curso.
    /// </summary>
    public int CdAfiliado { get; set; }

    /// <summary>
    /// Nome do curso.
    /// </summary>
    [Display(Name = "Título do Curso")]
    public string NoCurso { get; set; } = null!;

    /// <summary>
    /// Descrição completa e detalhada do curso.
    /// </summary>
    [Display(Name = "Descrição do Curso")]
    public string TxDescricao { get; set; } = null!;

    /// <summary>
    /// Valor de venda do curso. 0 para cursos gratuitos.
    /// </summary>
    [Display(Name = "Preço do Curso")]
    public decimal DcValor { get; set; }

    /// <summary>
    /// Caminho para a imagem de capa do curso.
    /// </summary>
    [StringLength(500, ErrorMessage = "O caminho da imagem não pode exceder 500 caracteres.")]
    public string? TxCaminhoImagem { get; set; }

    /// <summary>
    /// Data de criação do registro do curso.
    /// </summary>
    public DateTime DtCadastro { get; set; }

    /// <summary>
    /// Data da última alteração do curso.
    /// </summary>
    public DateTime? DtAlteracao { get; set; }

    /// <summary>
    /// Flag que indica se o curso está ativo (1) ou inativo (0).
    /// </summary>
    public bool SnAtivo { get; set; }

    /// <summary>
    /// Flag que indica se o curso foi aprovado (1) pelo admin para publicação.
    /// </summary>
    public bool SnAprovado { get; set; }

    /// <summary>
    /// Data em que o admin avaliou o curso.
    /// </summary>
    public DateTime? DtAvaliacaoAdmin { get; set; }

    public virtual AfiliadoMOD CdAfiliadoNavigation { get; set; } = null!;

    public virtual ICollection<ModuloMOD> Modulos { get; set; } = new List<ModuloMOD>();

    public virtual ICollection<PedidoItemMOD> PedidoItems { get; set; } = new List<PedidoItemMOD>();
}
