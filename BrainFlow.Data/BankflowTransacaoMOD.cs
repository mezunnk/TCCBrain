namespace BrainFlow.Data.Models;

/// <summary>
/// Registra as transações financeiras com o gateway de pagamento.
/// </summary>
public partial class BankflowTransacaoMOD
{
    /// <summary>
    /// Código único da transação. PK.
    /// </summary>
    public int CdTransacao { get; set; }

    /// <summary>
    /// FK para a tabela PEDIDO.
    /// </summary>
    public int CdPedido { get; set; }

    /// <summary>
    /// FK para a tabela BANKFLOW_TRANSACAO_TIPO.
    /// </summary>
    public int CdTipoTransacao { get; set; }

    /// <summary>
    /// Código ou token de identificação da transação no gateway de pagamento (ex: PayPal).
    /// </summary>
    public string TxTokenGateway { get; set; } = null!;

    /// <summary>
    /// Valor efetivamente transacionado.
    /// </summary>
    public decimal DcValorTransacao { get; set; }

    /// <summary>
    /// Data e hora em que a transação ocorreu.
    /// </summary>
    public DateTime DtTransacao { get; set; }

    public virtual PedidoMOD CdPedidoNavigation { get; set; } = null!;

    public virtual BankflowTransacaoTipoMOD CdTipoTransacaoNavigation { get; set; } = null!;
}
