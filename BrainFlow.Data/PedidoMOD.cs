namespace BrainFlow.Data.Models;

/// <summary>
/// Armazena os cabeçalhos dos pedidos de compra de cursos.
/// </summary>
public partial class PedidoMOD
{
    /// <summary>
    /// Código único do pedido. PK.
    /// </summary>
    public int CdPedido { get; set; }

    /// <summary>
    /// FK para a tabela USUARIO, indicando o comprador.
    /// </summary>
    public int CdUsuario { get; set; }

    /// <summary>
    /// Soma total dos valores dos itens do pedido.
    /// </summary>
    public decimal DcValorTotal { get; set; }

    /// <summary>
    /// Data em que o pedido foi criado.
    /// </summary>
    public DateTime DtPedido { get; set; }

    /// <summary>
    /// Flag que indica se o pedido foi finalizado (1) (pagamento confirmado).
    /// </summary>
    public bool SnFinalizado { get; set; }

    /// <summary>
    /// Data em que o pedido foi finalizado.
    /// </summary>
    public DateTime? DtFinalizacao { get; set; }

    public virtual ICollection<BankflowTransacaoMOD> BankflowTransacaos { get; set; } = new List<BankflowTransacaoMOD>();

    public virtual UsuarioMOD CdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<PedidoItemMOD> PedidoItems { get; set; } = new List<PedidoItemMOD>();
}
