namespace BrainFlow.Data.Models;

/// <summary>
/// Armazena as credenciais de acesso e informações de segurança do usuário.
/// </summary>
public partial class UsuarioLoginMOD
{
    /// <summary>
    /// Código único do registro de login. PK.
    /// </summary>
    public int CdLogin { get; set; }

    /// <summary>
    /// FK para a tabela USUARIO, estabelecendo a relação um-para-um.
    /// </summary>
    public int CdUsuario { get; set; }

    /// <summary>
    /// Senha do usuário armazenada em formato hash.
    /// </summary>
    public string TxSenhaHash { get; set; } = null!;

    /// <summary>
    /// Token gerado para o processo de recuperação de senha.
    /// </summary>
    public string? TxTokenRecuperacao { get; set; }

    /// <summary>
    /// Data e hora em que o token de recuperação de senha expira.
    /// </summary>
    public DateTime? DtValidadeToken { get; set; }

    /// <summary>
    /// Data de criação do registro.
    /// </summary>
    public DateTime DtCadastro { get; set; }

    /// <summary>
    /// Data da última alteração do registro (ex: troca de senha).
    /// </summary>
    public DateTime? DtAlteracao { get; set; }

    public virtual UsuarioMOD CdUsuarioNavigation { get; set; } = null!;
}
