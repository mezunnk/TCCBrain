namespace BrainFlow.Data.Models;

/// <summary>
/// Armazena os dados cadastrais de todos os usuários da plataforma.
/// </summary>
public partial class UsuarioMOD
{
    /// <summary>
    /// Código único do usuário. PK.
    /// </summary>
    public int CdUsuario { get; set; }

    /// <summary>
    /// FK para a tabela USUARIO_TIPO.
    /// </summary>
    public int CdTipoUsuario { get; set; }

    /// <summary>
    /// Nome completo do usuário.
    /// </summary>
    public string NoUsuario { get; set; } = null!;

    /// <summary>
    /// Email do usuário. Deve ser único.
    /// </summary>
    public string TxEmail { get; set; } = null!;

    /// <summary>
    /// Telefone de contato do usuário.
    /// </summary>
    public string? TxTelefone { get; set; }

    /// <summary>
    /// Data de criação do registro.
    /// </summary>
    public DateTime DtCadastro { get; set; }

    /// <summary>
    /// Data da última alteração do registro.
    /// </summary>
    public DateTime? DtAlteracao { get; set; }

    /// <summary>
    /// Flag que indica se o usuário está ativo (1) ou inativo (0).
    /// </summary>
    public bool SnAtivo { get; set; }

    public virtual ICollection<AfiliadoMOD> Afiliados { get; set; } = new List<AfiliadoMOD>();

    public virtual UsuarioTipoMOD CdTipoUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<PedidoMOD> Pedidos { get; set; } = new List<PedidoMOD>();

    public virtual ICollection<UsuarioAulaMOD> UsuarioAulas { get; set; } = new List<UsuarioAulaMOD>();

    public virtual ICollection<UsuarioLoginMOD> UsuarioLogins { get; set; } = new List<UsuarioLoginMOD>();
}
