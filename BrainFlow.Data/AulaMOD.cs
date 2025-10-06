namespace BrainFlow.Data.Models;

/// <summary>
/// Armazena os dados de cada aula individualmente.
/// </summary>
public partial class AulaMOD
{
    /// <summary>
    /// Código único da aula. PK.
    /// </summary>
    public int CdAula { get; set; }

    /// <summary>
    /// FK para a tabela MODULO.
    /// </summary>
    public int CdModulo { get; set; }

    /// <summary>
    /// Nome da aula (Ex: &quot;Variáveis e Tipos de Dados&quot;).
    /// </summary>
    public string NoAula { get; set; } = null!;

    /// <summary>
    /// Número que define a ordem de exibição das aulas dentro de um módulo.
    /// </summary>
    public int NrOrdem { get; set; }

    /// <summary>
    /// Descrição do conteúdo abordado na aula.
    /// </summary>
    public string? TxDescricao { get; set; }

    /// <summary>
    /// URL ou caminho para o arquivo de vídeo da aula.
    /// </summary>
    public string? TxCaminhoVideo { get; set; }

    /// <summary>
    /// Conteúdo textual complementar da aula (ex: código, links).
    /// </summary>
    public string? TxConteudo { get; set; }

    /// <summary>
    /// Flag que indica se a aula pode ser visualizada gratuitamente (1) por não-alunos.
    /// </summary>
    public bool SnAulaGratuita { get; set; }

    /// <summary>
    /// Data de criação do registro da aula.
    /// </summary>
    public DateTime DtCadastro { get; set; }

    /// <summary>
    /// Data da última alteração da aula.
    /// </summary>
    public DateTime? DtAlteracao { get; set; }

    /// <summary>
    /// Flag que indica se a aula está ativa (1) ou inativa (0).
    /// </summary>
    public bool SnAtivo { get; set; }

    public virtual ModuloMOD CdModuloNavigation { get; set; } = null!;

    public virtual ICollection<UsuarioAulaMOD> UsuarioAulas { get; set; } = new List<UsuarioAulaMOD>();
}
