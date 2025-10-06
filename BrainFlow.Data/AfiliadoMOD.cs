namespace BrainFlow.Data.Models;

/// <summary>
/// Armazena dados específicos dos usuários que são afiliados (instrutores).
/// </summary>
public partial class AfiliadoMOD
{
    /// <summary>
    /// Código único do afiliado. PK.
    /// </summary>
    public int CdAfiliado { get; set; }

    /// <summary>
    /// FK para a tabela USUARIO.
    /// </summary>
    public int CdUsuario { get; set; }

    /// <summary>
    /// CPF do afiliado, para fins de pagamento e fiscais.
    /// </summary>
    public string NrCpf { get; set; } = null!;

    /// <summary>
    /// Nome da instituição ou empresa do afiliado, se aplicável.
    /// </summary>
    public string? NoInstitucional { get; set; }

    /// <summary>
    /// Data em que o usuário solicitou a afiliação.
    /// </summary>
    public DateTime DtSolicitacao { get; set; }

    /// <summary>
    /// Flag que indica se o afiliado está ativo (1) ou inativo (0).
    /// </summary>
    public bool SnAtivo { get; set; }

    /// <summary>
    /// Flag que indica se a solicitação de afiliação foi aprovada (1) pelo admin.
    /// </summary>
    public bool SnAprovado { get; set; }

    /// <summary>
    /// Data em que a afiliação foi aprovada.
    /// </summary>
    public DateTime? DtAprovacao { get; set; }

    public virtual ICollection<AfiliadoPaginaMOD> AfiliadoPaginas { get; set; } = new List<AfiliadoPaginaMOD>();

    public virtual UsuarioMOD CdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<ComissaoMOD> Comissaos { get; set; } = new List<ComissaoMOD>();

    public virtual ICollection<CursoMOD> Cursos { get; set; } = new List<CursoMOD>();
}
