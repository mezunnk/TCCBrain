namespace BrainFlow.Data.Models;

/// <summary>
/// Registra o cálculo e o status das comissões geradas por venda.
/// </summary>
public partial class ComissaoMOD
{
    /// <summary>
    /// Código único da comissão. PK.
    /// </summary>
    public int CdComissao { get; set; }

    /// <summary>
    /// FK para a tabela PEDIDO_ITEM, que originou a comissão.
    /// </summary>
    public int CdPedidoItem { get; set; }

    /// <summary>
    /// FK para a tabela AFILIADO, que receberá a comissão.
    /// </summary>
    public int CdAfiliado { get; set; }

    /// <summary>
    /// Valor total da venda do item.
    /// </summary>
    public decimal DcValorBrutoVenda { get; set; }

    /// <summary>
    /// Valor da comissão retido pela plataforma.
    /// </summary>
    public decimal DcComissaoPlataforma { get; set; }

    /// <summary>
    /// Valor da comissão a ser paga ao afiliado.
    /// </summary>
    public decimal DcComissaoAfiliado { get; set; }

    /// <summary>
    /// Data em que a comissão foi calculada e registrada.
    /// </summary>
    public DateTime DtCalculo { get; set; }

    /// <summary>
    /// Flag que indica se a comissão já foi repassada (1) ao afiliado.
    /// </summary>
    public bool SnRepassado { get; set; }

    /// <summary>
    /// Data em que o repasse foi efetuado.
    /// </summary>
    public DateTime? DtRepasse { get; set; }

    public virtual AfiliadoMOD CdAfiliadoNavigation { get; set; } = null!;

    public virtual PedidoItemMOD CdPedidoItemNavigation { get; set; } = null!;
}
