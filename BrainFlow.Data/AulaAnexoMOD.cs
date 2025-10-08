namespace BrainFlow.Data.Models;

/// <summary>
/// Anexo (material) associado a uma aula.
/// </summary>
public partial class AulaAnexoMOD
{
    public int CdAulaAnexo { get; set; }
    public int CdAula { get; set; }
    public string NoArquivoOriginal { get; set; } = null!;
    public string TxCaminhoArquivo { get; set; } = null!;
    public DateTime DtUpload { get; set; }

    public virtual AulaMOD CdAulaNavigation { get; set; } = null!;
}
