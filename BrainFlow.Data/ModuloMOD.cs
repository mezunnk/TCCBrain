namespace BrainFlow.Data.Models;

/// <summary>
/// Organiza o conteúdo de um curso em módulos ou seções.
/// </summary>
public partial class ModuloMOD
{
    /// <summary>
    /// Código único do módulo. PK.
    /// </summary>
    public int CdModulo { get; set; }

    /// <summary>
    /// FK para a tabela CURSO, indicando a qual curso o módulo pertence.
    /// </summary>
    public int CdCurso { get; set; }

    /// <summary>
    /// Nome do módulo (Ex: &quot;Introdução ao C#&quot;).
    /// </summary>
    public string NoModulo { get; set; } = null!;

    /// <summary>
    /// Breve descrição sobre o conteúdo do módulo.
    /// </summary>
    public string? TxDescricao { get; set; }

    /// <summary>
    /// Número que define a ordem de exibição dos módulos.
    /// </summary>
    public int NrOrdem { get; set; }

    /// <summary>
    /// Data de criação do registro do módulo.
    /// </summary>
    public DateTime DtCadastro { get; set; }

    /// <summary>
    /// Data da última alteração do módulo.
    /// </summary>
    public DateTime? DtAlteracao { get; set; }

    /// <summary>
    /// Flag que indica se o módulo está ativo (1) ou inativo (0).
    /// </summary>
    public bool SnAtivo { get; set; }

    public virtual ICollection<AulaMOD> Aulas { get; set; } = new List<AulaMOD>();

    public virtual CursoMOD CdCursoNavigation { get; set; } = null!;
}
