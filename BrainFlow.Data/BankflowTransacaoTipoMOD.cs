namespace BrainFlow.Data.Models;

/// <summary>
/// Tabela de domínio para os tipos de transação (ex: Cartão de Crédito, PIX).
/// </summary>
public partial class BankflowTransacaoTipoMOD
{
    /// <summary>
    /// Código único do tipo de transação. PK.
    /// </summary>
    public int CdTipoTransacao { get; set; }

    /// <summary>
    /// Nome do tipo de transação.
    /// </summary>
    public string NoTipoTransacao { get; set; } = null!;

    public virtual ICollection<BankflowTransacaoMOD> BankflowTransacaos { get; set; } = new List<BankflowTransacaoMOD>();
}
