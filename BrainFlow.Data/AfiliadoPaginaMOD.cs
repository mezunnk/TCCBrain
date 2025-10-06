namespace BrainFlow.Data.Models;

/// <summary>
/// Armazena as informações da página pública personalizável de cada afiliado.
/// </summary>
public partial class AfiliadoPaginaMOD
{
    /// <summary>
    /// Código único da página. PK.
    /// </summary>
    public int CdPagina { get; set; }

    /// <summary>
    /// FK para a tabela AFILIADO.
    /// </summary>
    public int CdAfiliado { get; set; }

    /// <summary>
    /// URL amigável da página (ex: /afiliado/nome-do-afiliado).
    /// </summary>
    public string TxLinkPagina { get; set; } = null!;

    /// <summary>
    /// Título principal exibido na página do afiliado.
    /// </summary>
    public string TxTitulo { get; set; } = null!;

    /// <summary>
    /// Caminho para a imagem do banner da página.
    /// </summary>
    public string? TxCaminhoBanner { get; set; }

    /// <summary>
    /// Caminho para a imagem do logo do afiliado.
    /// </summary>
    public string? TxCaminhoLogo { get; set; }

    /// <summary>
    /// URL para o perfil do LinkedIn do afiliado.
    /// </summary>
    public string? TxUrlLinkedin { get; set; }

    /// <summary>
    /// URL para o canal do YouTube do afiliado.
    /// </summary>
    public string? TxUrlYoutube { get; set; }

    /// <summary>
    /// URL para o perfil do Instagram do afiliado.
    /// </summary>
    public string? TxUrlInstagram { get; set; }

    public virtual AfiliadoMOD CdAfiliadoNavigation { get; set; } = null!;
}
