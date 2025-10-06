namespace BrainFlow.Data.Models;

/// <summary>
/// Tabela de associação que registra o progresso de um usuário em um curso.
/// </summary>
public partial class UsuarioAulaMOD
{
    /// <summary>
    /// Código único do registro de progresso. PK.
    /// </summary>
    public int CdUsuarioAula { get; set; }

    /// <summary>
    /// FK para a tabela AULA.
    /// </summary>
    public int CdAula { get; set; }

    /// <summary>
    /// FK para a tabela USUARIO.
    /// </summary>
    public int CdUsuario { get; set; }

    /// <summary>
    /// Flag que indica se o usuário concluiu (1) a aula.
    /// </summary>
    public bool SnConcluida { get; set; }

    /// <summary>
    /// Data em que o usuário marcou a aula como concluída.
    /// </summary>
    public DateTime? DtConclusao { get; set; }

    public virtual AulaMOD CdAulaNavigation { get; set; } = null!;

    public virtual UsuarioMOD CdUsuarioNavigation { get; set; } = null!;
}
