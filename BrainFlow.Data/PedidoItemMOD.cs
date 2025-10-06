namespace BrainFlow.Data.Models;

/// <summary>
/// Armazena os itens (cursos) de um pedido.
/// </summary>
public partial class PedidoItemMOD
{
    /// <summary>
    /// Código único do item do pedido. PK.
    /// </summary>
    public int CdPedidoItem { get; set; }

    /// <summary>
    /// FK para a tabela CURSO, indicando o curso comprado.
    /// </summary>
    public int CdCurso { get; set; }

    /// <summary>
    /// FK para a tabela PEDIDO.
    /// </summary>
    public int CdPedido { get; set; }

    /// <summary>
    /// Valor do curso no momento da compra.
    /// </summary>
    public decimal DcValorItem { get; set; }

    public virtual CursoMOD CdCursoNavigation { get; set; } = null!;

    public virtual PedidoMOD CdPedidoNavigation { get; set; } = null!;

    public virtual ICollection<ComissaoMOD> Comissaos { get; set; } = new List<ComissaoMOD>();
}
