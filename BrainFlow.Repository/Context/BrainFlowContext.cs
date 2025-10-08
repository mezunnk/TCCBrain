using BrainFlow.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BrainFlow.Repository.Context;

public partial class BrainFlowContext : DbContext
{
    public BrainFlowContext()
    {
    }

    public BrainFlowContext(DbContextOptions<BrainFlowContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AfiliadoMOD> Afiliados { get; set; }

    public virtual DbSet<AfiliadoPaginaMOD> AfiliadoPaginas { get; set; }

    public virtual DbSet<AulaMOD> Aulas { get; set; }

    public virtual DbSet<AulaAnexoMOD> AulaAnexos { get; set; }

    public virtual DbSet<BankflowTransacaoMOD> BankflowTransacaos { get; set; }

    public virtual DbSet<BankflowTransacaoTipoMOD> BankflowTransacaoTipos { get; set; }

    public virtual DbSet<ComissaoMOD> Comissaos { get; set; }

    public virtual DbSet<CursoMOD> Cursos { get; set; }

    public virtual DbSet<ModuloMOD> Modulos { get; set; }

    public virtual DbSet<PedidoMOD> Pedidos { get; set; }

    public virtual DbSet<PedidoItemMOD> PedidoItems { get; set; }

    public virtual DbSet<UsuarioMOD> Usuarios { get; set; }

    public virtual DbSet<UsuarioAulaMOD> UsuarioAulas { get; set; }

    public virtual DbSet<UsuarioLoginMOD> UsuarioLogins { get; set; }

    public virtual DbSet<UsuarioTipoMOD> UsuarioTipos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Configuração padrão para MySQL (será sobrescrita pela injeção de dependência)
            optionsBuilder.UseMySql(
                "Server=localhost;Database=brainflow;User=root;Password=;Port=3306;",
                ServerVersion.AutoDetect("Server=localhost;Database=brainflow;User=root;Password=;Port=3306;")
            );
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AfiliadoMOD>(entity =>
        {
            entity.HasKey(e => e.CdAfiliado);

            entity.ToTable("afiliados", tb => tb.HasComment("Armazena dados específicos dos usuários que são afiliados (instrutores)."));

            entity.Property(e => e.CdAfiliado)
                .HasComment("Código único do afiliado. PK.")
                .HasColumnName("CD_AFILIADO");
            entity.Property(e => e.CdUsuario)
                .HasComment("FK para a tabela USUARIO.")
                .HasColumnName("CD_USUARIO");
            entity.Property(e => e.DtAprovacao)
                .HasComment("Data em que a afiliação foi aprovada.")
                .HasColumnType("datetime")
                .HasColumnName("DT_APROVACAO");
            entity.Property(e => e.DtSolicitacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("Data em que o usuário solicitou a afiliação.")
                .HasColumnType("datetime")
                .HasColumnName("DT_SOLICITACAO");
            entity.Property(e => e.NoInstitucional)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("Nome da instituição ou empresa do afiliado, se aplicável.")
                .HasColumnName("NO_INSTITUCIONAL");
            entity.Property(e => e.NrCpf)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasComment("CPF do afiliado, para fins de pagamento e fiscais.")
                .HasColumnName("NR_CPF");
            entity.Property(e => e.SnAprovado)
                .HasDefaultValue(false)
                .HasComment("Flag que indica se a solicitação de afiliação foi aprovada (1) pelo admin.")
                .HasColumnName("SN_APROVADO");
            entity.Property(e => e.SnAtivo)
                .HasDefaultValue(true)
                .HasComment("Flag que indica se o afiliado está ativo (1) ou inativo (0).")
                .HasColumnName("SN_ATIVO");

            entity.HasOne(d => d.CdUsuarioNavigation).WithMany(p => p.Afiliados)
                .HasForeignKey(d => d.CdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AFILIADO_USUARIO");
        });

        modelBuilder.Entity<AfiliadoPaginaMOD>(entity =>
        {
            entity.HasKey(e => e.CdPagina);

            entity.ToTable("AFILIADO_PAGINA", tb => tb.HasComment("Armazena as informações da página pública personalizável de cada afiliado."));

            entity.Property(e => e.CdPagina)
                .HasComment("Código único da página. PK.")
                .HasColumnName("CD_PAGINA");
            entity.Property(e => e.CdAfiliado)
                .HasComment("FK para a tabela AFILIADO.")
                .HasColumnName("CD_AFILIADO");
            entity.Property(e => e.TxCaminhoBanner)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasComment("Caminho para a imagem do banner da página.")
                .HasColumnName("TX_CAMINHO_BANNER");
            entity.Property(e => e.TxCaminhoLogo)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasComment("Caminho para a imagem do logo do afiliado.")
                .HasColumnName("TX_CAMINHO_LOGO");
            entity.Property(e => e.TxLinkPagina)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("URL amigável da página (ex: /afiliado/nome-do-afiliado).")
                .HasColumnName("TX_LINK_PAGINA");
            entity.Property(e => e.TxTitulo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("Título principal exibido na página do afiliado.")
                .HasColumnName("TX_TITULO");
            entity.Property(e => e.TxUrlInstagram)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("URL para o perfil do Instagram do afiliado.")
                .HasColumnName("TX_URL_INSTAGRAM");
            entity.Property(e => e.TxUrlLinkedin)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("URL para o perfil do LinkedIn do afiliado.")
                .HasColumnName("TX_URL_LINKEDIN");
            entity.Property(e => e.TxUrlYoutube)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("URL para o canal do YouTube do afiliado.")
                .HasColumnName("TX_URL_YOUTUBE");

            entity.HasOne(d => d.CdAfiliadoNavigation).WithMany(p => p.AfiliadoPaginas)
                .HasForeignKey(d => d.CdAfiliado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PAGINA_AFILIADO");
        });

        modelBuilder.Entity<AulaMOD>(entity =>
        {
            entity.HasKey(e => e.CdAula);

            entity.ToTable("aulas", tb => tb.HasComment("Armazena os dados de cada aula individualmente."));

            entity.Property(e => e.CdAula)
                .HasComment("Código único da aula. PK.")
                .HasColumnName("CD_AULA");
            entity.Property(e => e.CdModulo)
                .HasComment("FK para a tabela MODULO.")
                .HasColumnName("CD_MODULO");
            entity.Property(e => e.DtAlteracao)
                .HasComment("Data da última alteração da aula.")
                .HasColumnType("datetime")
                .HasColumnName("DT_ALTERACAO");
            entity.Property(e => e.DtCadastro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("Data de criação do registro da aula.")
                .HasColumnType("datetime")
                .HasColumnName("DT_CADASTRO");
            entity.Property(e => e.NoAula)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("Nome da aula (Ex: \"Variáveis e Tipos de Dados\").")
                .HasColumnName("NO_AULA");
            entity.Property(e => e.NrOrdem)
                .HasComment("Número que define a ordem de exibição das aulas dentro de um módulo.")
                .HasColumnName("NR_ORDEM");
            entity.Property(e => e.SnAtivo)
                .HasDefaultValue(true)
                .HasComment("Flag que indica se a aula está ativa (1) ou inativa (0).")
                .HasColumnName("SN_ATIVO");
            entity.Property(e => e.SnAulaGratuita)
                .HasDefaultValue(false)
                .HasComment("Flag que indica se a aula pode ser visualizada gratuitamente (1) por não-alunos.")
                .HasColumnName("SN_AULA_GRATUITA");
            entity.Property(e => e.TxCaminhoVideo)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasComment("URL ou caminho para o arquivo de vídeo da aula.")
                .HasColumnName("TX_CAMINHO_VIDEO");
            entity.Property(e => e.TxConteudo)
                .IsUnicode(false)
                .HasComment("Conteúdo textual complementar da aula (ex: código, links).")
                .HasColumnName("TX_CONTEUDO");
            entity.Property(e => e.TxDescricao)
                .IsUnicode(false)
                .HasComment("Descrição do conteúdo abordado na aula.")
                .HasColumnName("TX_DESCRICAO");

            entity.HasOne(d => d.CdModuloNavigation).WithMany(p => p.Aulas)
                .HasForeignKey(d => d.CdModulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AULA_MODULO");
        });

        modelBuilder.Entity<BankflowTransacaoMOD>(entity =>
        {
            entity.HasKey(e => e.CdTransacao);

            entity.ToTable("BANKFLOW_TRANSACAO", tb => tb.HasComment("Registra as transações financeiras com o gateway de pagamento."));

            entity.Property(e => e.CdTransacao)
                .HasComment("Código único da transação. PK.")
                .HasColumnName("CD_TRANSACAO");
            entity.Property(e => e.CdPedido)
                .HasComment("FK para a tabela PEDIDO.")
                .HasColumnName("CD_PEDIDO");
            entity.Property(e => e.CdTipoTransacao)
                .HasComment("FK para a tabela BANKFLOW_TRANSACAO_TIPO.")
                .HasColumnName("CD_TIPO_TRANSACAO");
            entity.Property(e => e.DcValorTransacao)
                .HasComment("Valor efetivamente transacionado.")
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("DC_VALOR_TRANSACAO");
            entity.Property(e => e.DtTransacao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("Data e hora em que a transação ocorreu.")
                .HasColumnType("datetime")
                .HasColumnName("DT_TRANSACAO");
            entity.Property(e => e.TxTokenGateway)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasComment("Código ou token de identificação da transação no gateway de pagamento (ex: PayPal).")
                .HasColumnName("TX_TOKEN_GATEWAY");

            entity.HasOne(d => d.CdPedidoNavigation).WithMany(p => p.BankflowTransacaos)
                .HasForeignKey(d => d.CdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRANSACAO_PEDIDO");

            entity.HasOne(d => d.CdTipoTransacaoNavigation).WithMany(p => p.BankflowTransacaos)
                .HasForeignKey(d => d.CdTipoTransacao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TRANSACAO_TIPO");
        });

        modelBuilder.Entity<BankflowTransacaoTipoMOD>(entity =>
        {
            entity.HasKey(e => e.CdTipoTransacao);

            entity.ToTable("BANKFLOW_TRANSACAO_TIPO", tb => tb.HasComment("Tabela de domínio para os tipos de transação (ex: Cartão de Crédito, PIX)."));

            entity.Property(e => e.CdTipoTransacao)
                .HasComment("Código único do tipo de transação. PK.")
                .HasColumnName("CD_TIPO_TRANSACAO");
            entity.Property(e => e.NoTipoTransacao)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Nome do tipo de transação.")
                .HasColumnName("NO_TIPO_TRANSACAO");
        });

        modelBuilder.Entity<ComissaoMOD>(entity =>
        {
            entity.HasKey(e => e.CdComissao);

            entity.ToTable("comissoes", tb => tb.HasComment("Registra o cálculo e o status das comissões geradas por venda."));

            entity.Property(e => e.CdComissao)
                .HasComment("Código único da comissão. PK.")
                .HasColumnName("CD_COMISSAO");
            entity.Property(e => e.CdAfiliado)
                .HasComment("FK para a tabela AFILIADO, que receberá a comissão.")
                .HasColumnName("CD_AFILIADO");
            entity.Property(e => e.CdPedidoItem)
                .HasComment("FK para a tabela PEDIDO_ITEM, que originou a comissão.")
                .HasColumnName("CD_PEDIDO_ITEM");
            entity.Property(e => e.DcComissaoAfiliado)
                .HasComment("Valor da comissão a ser paga ao afiliado.")
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("DC_COMISSAO_AFILIADO");
            entity.Property(e => e.DcComissaoPlataforma)
                .HasComment("Valor da comissão retido pela plataforma.")
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("DC_COMISSAO_PLATAFORMA");
            entity.Property(e => e.DcValorBrutoVenda)
                .HasComment("Valor total da venda do item.")
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("DC_VALOR_BRUTO_VENDA");
            entity.Property(e => e.DtCalculo)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("Data em que a comissão foi calculada e registrada.")
                .HasColumnType("datetime")
                .HasColumnName("DT_CALCULO");
            entity.Property(e => e.DtRepasse)
                .HasComment("Data em que o repasse foi efetuado.")
                .HasColumnType("datetime")
                .HasColumnName("DT_REPASSE");
            entity.Property(e => e.SnRepassado)
                .HasDefaultValue(false)
                .HasComment("Flag que indica se a comissão já foi repassada (1) ao afiliado.")
                .HasColumnName("SN_REPASSADO");

            entity.HasOne(d => d.CdAfiliadoNavigation).WithMany(p => p.Comissaos)
                .HasForeignKey(d => d.CdAfiliado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COMISSAO_AFILIADO");

            entity.HasOne(d => d.CdPedidoItemNavigation).WithMany(p => p.Comissaos)
                .HasForeignKey(d => d.CdPedidoItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COMISSAO_PEDIDOITEM");
        });

        modelBuilder.Entity<CursoMOD>(entity =>
        {
            entity.HasKey(e => e.CdCurso);

            entity.ToTable("cursos", tb => tb.HasComment("Armazena os dados dos cursos criados pelos afiliados."));

            entity.Property(e => e.CdCurso)
                .HasComment("Código único do curso. PK.")
                .HasColumnName("CD_CURSO");
            entity.Property(e => e.CdAfiliado)
                .HasComment("FK para a tabela AFILIADO, indicando o autor do curso.")
                .HasColumnName("CD_AFILIADO");
            entity.Property(e => e.DcValor)
                .HasComment("Valor de venda do curso. 0 para cursos gratuitos.")
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("DC_VALOR");
            entity.Property(e => e.DtAlteracao)
                .HasComment("Data da última alteração do curso.")
                .HasColumnType("datetime")
                .HasColumnName("DT_ALTERACAO");
            entity.Property(e => e.DtAvaliacaoAdmin)
                .HasComment("Data em que o admin avaliou o curso.")
                .HasColumnType("datetime")
                .HasColumnName("DT_AVALIACAO_ADMIN");
            entity.Property(e => e.DtCadastro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("Data de criação do registro do curso.")
                .HasColumnType("datetime")
                .HasColumnName("DT_CADASTRO");
            entity.Property(e => e.NoCurso)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("Nome do curso.")
                .HasColumnName("NO_CURSO");
            entity.Property(e => e.SnAprovado)
                .HasDefaultValue(false)
                .HasComment("Flag que indica se o curso foi aprovado (1) pelo admin para publicação.")
                .HasColumnName("SN_APROVADO");
            entity.Property(e => e.SnAtivo)
                .HasDefaultValue(true)
                .HasComment("Flag que indica se o curso está ativo (1) ou inativo (0).")
                .HasColumnName("SN_ATIVO");
            entity.Property(e => e.TxCaminhoImagem)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasComment("Caminho para a imagem de capa do curso.")
                .HasColumnName("TX_CAMINHO_IMAGEM");
            entity.Property(e => e.TxDescricao)
                .IsUnicode(false)
                .HasComment("Descrição completa e detalhada do curso.")
                .HasColumnName("TX_DESCRICAO");

            entity.HasOne(d => d.CdAfiliadoNavigation).WithMany(p => p.Cursos)
                .HasForeignKey(d => d.CdAfiliado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CURSO_AFILIADO");
        });

        modelBuilder.Entity<ModuloMOD>(entity =>
        {
            entity.HasKey(e => e.CdModulo);

            entity.ToTable("modulos", tb => tb.HasComment("Organiza o conteúdo de um curso em módulos ou seções."));

            entity.Property(e => e.CdModulo)
                .HasComment("Código único do módulo. PK.")
                .HasColumnName("CD_MODULO");
            entity.Property(e => e.CdCurso)
                .HasComment("FK para a tabela CURSO, indicando a qual curso o módulo pertence.")
                .HasColumnName("CD_CURSO");
            entity.Property(e => e.DtAlteracao)
                .HasComment("Data da última alteração do módulo.")
                .HasColumnType("datetime")
                .HasColumnName("DT_ALTERACAO");
            entity.Property(e => e.DtCadastro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("Data de criação do registro do módulo.")
                .HasColumnType("datetime")
                .HasColumnName("DT_CADASTRO");
            entity.Property(e => e.NoModulo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("Nome do módulo (Ex: \"Introdução ao C#\").")
                .HasColumnName("NO_MODULO");
            entity.Property(e => e.NrOrdem)
                .HasComment("Número que define a ordem de exibição dos módulos.")
                .HasColumnName("NR_ORDEM");
            entity.Property(e => e.SnAtivo)
                .HasDefaultValue(true)
                .HasComment("Flag que indica se o módulo está ativo (1) ou inativo (0).")
                .HasColumnName("SN_ATIVO");
            entity.Property(e => e.TxDescricao)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasComment("Breve descrição sobre o conteúdo do módulo.")
                .HasColumnName("TX_DESCRICAO");

            entity.HasOne(d => d.CdCursoNavigation).WithMany(p => p.Modulos)
                .HasForeignKey(d => d.CdCurso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MODULO_CURSO");
        });

        modelBuilder.Entity<PedidoMOD>(entity =>
        {
            entity.HasKey(e => e.CdPedido);

            entity.ToTable("PEDIDO", tb => tb.HasComment("Armazena os cabeçalhos dos pedidos de compra de cursos."));

            entity.Property(e => e.CdPedido)
                .HasComment("Código único do pedido. PK.")
                .HasColumnName("CD_PEDIDO");
            entity.Property(e => e.CdUsuario)
                .HasComment("FK para a tabela USUARIO, indicando o comprador.")
                .HasColumnName("CD_USUARIO");
            entity.Property(e => e.DcValorTotal)
                .HasComment("Soma total dos valores dos itens do pedido.")
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("DC_VALOR_TOTAL");
            entity.Property(e => e.DtFinalizacao)
                .HasComment("Data em que o pedido foi finalizado.")
                .HasColumnType("datetime")
                .HasColumnName("DT_FINALIZACAO");
            entity.Property(e => e.DtPedido)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("Data em que o pedido foi criado.")
                .HasColumnType("datetime")
                .HasColumnName("DT_PEDIDO");
            entity.Property(e => e.SnFinalizado)
                .HasDefaultValue(false)
                .HasComment("Flag que indica se o pedido foi finalizado (1) (pagamento confirmado).")
                .HasColumnName("SN_FINALIZADO");

            entity.HasOne(d => d.CdUsuarioNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.CdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PEDIDO_USUARIO");
        });

        modelBuilder.Entity<PedidoItemMOD>(entity =>
        {
            entity.HasKey(e => e.CdPedidoItem);

            entity.ToTable("PEDIDO_ITEM", tb => tb.HasComment("Armazena os itens (cursos) de um pedido."));

            entity.Property(e => e.CdPedidoItem)
                .HasComment("Código único do item do pedido. PK.")
                .HasColumnName("CD_PEDIDO_ITEM");
            entity.Property(e => e.CdCurso)
                .HasComment("FK para a tabela CURSO, indicando o curso comprado.")
                .HasColumnName("CD_CURSO");
            entity.Property(e => e.CdPedido)
                .HasComment("FK para a tabela PEDIDO.")
                .HasColumnName("CD_PEDIDO");
            entity.Property(e => e.DcValorItem)
                .HasComment("Valor do curso no momento da compra.")
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("DC_VALOR_ITEM");

            entity.HasOne(d => d.CdCursoNavigation).WithMany(p => p.PedidoItems)
                .HasForeignKey(d => d.CdCurso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PEDIDOITEM_CURSO");

            entity.HasOne(d => d.CdPedidoNavigation).WithMany(p => p.PedidoItems)
                .HasForeignKey(d => d.CdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PEDIDOITEM_PEDIDO");
        });

        modelBuilder.Entity<UsuarioMOD>(entity =>
        {
            entity.HasKey(e => e.CdUsuario);

            entity.ToTable("usuarios", tb => tb.HasComment("Armazena os dados cadastrais de todos os usuários da plataforma."));

            entity.Property(e => e.CdUsuario)
                .HasComment("Código único do usuário. PK.")
                .HasColumnName("CD_USUARIO");
            entity.Property(e => e.CdTipoUsuario)
                .HasComment("FK para a tabela USUARIO_TIPO.")
                .HasColumnName("CD_TIPO_USUARIO");
            entity.Property(e => e.DtAlteracao)
                .HasComment("Data da última alteração do registro.")
                .HasColumnType("datetime")
                .HasColumnName("DT_ALTERACAO");
            entity.Property(e => e.DtCadastro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("Data de criação do registro.")
                .HasColumnType("datetime")
                .HasColumnName("DT_CADASTRO");
            entity.Property(e => e.NoUsuario)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("Nome completo do usuário.")
                .HasColumnName("NO_USUARIO");
            entity.Property(e => e.SnAtivo)
                .HasDefaultValue(true)
                .HasComment("Flag que indica se o usuário está ativo (1) ou inativo (0).")
                .HasColumnName("SN_ATIVO");
            entity.Property(e => e.TxEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("Email do usuário. Deve ser único.")
                .HasColumnName("TX_EMAIL");
            entity.Property(e => e.TxTelefone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("Telefone de contato do usuário.")
                .HasColumnName("TX_TELEFONE");

            entity.HasOne(d => d.CdTipoUsuarioNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.CdTipoUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USUARIO_TIPO");
        });

        modelBuilder.Entity<UsuarioAulaMOD>(entity =>
        {
            entity.HasKey(e => e.CdUsuarioAula);

            entity.ToTable("USUARIO_AULA", tb => tb.HasComment("Tabela de associação que registra o progresso de um usuário em um curso."));

            entity.Property(e => e.CdUsuarioAula)
                .HasComment("Código único do registro de progresso. PK.")
                .HasColumnName("CD_USUARIO_AULA");
            entity.Property(e => e.CdAula)
                .HasComment("FK para a tabela AULA.")
                .HasColumnName("CD_AULA");
            entity.Property(e => e.CdUsuario)
                .HasComment("FK para a tabela USUARIO.")
                .HasColumnName("CD_USUARIO");
            entity.Property(e => e.DtConclusao)
                .HasComment("Data em que o usuário marcou a aula como concluída.")
                .HasColumnType("datetime")
                .HasColumnName("DT_CONCLUSAO");
            entity.Property(e => e.SnConcluida)
                .HasDefaultValue(false)
                .HasComment("Flag que indica se o usuário concluiu (1) a aula.")
                .HasColumnName("SN_CONCLUIDA");

            entity.HasOne(d => d.CdAulaNavigation).WithMany(p => p.UsuarioAulas)
                .HasForeignKey(d => d.CdAula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USUARIOAULA_AULA");

            entity.HasOne(d => d.CdUsuarioNavigation).WithMany(p => p.UsuarioAulas)
                .HasForeignKey(d => d.CdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USUARIOAULA_USUARIO");
        });

        modelBuilder.Entity<UsuarioLoginMOD>(entity =>
        {
            entity.HasKey(e => e.CdLogin);

            entity.ToTable("usuario_login", tb => tb.HasComment("Armazena as credenciais de acesso e informações de segurança do usuário."));

            entity.Property(e => e.CdLogin)
                .HasComment("Código único do registro de login. PK.")
                .HasColumnName("CD_LOGIN");
            entity.Property(e => e.CdUsuario)
                .HasComment("FK para a tabela USUARIO, estabelecendo a relação um-para-um.")
                .HasColumnName("CD_USUARIO");
            entity.Property(e => e.DtAlteracao)
                .HasComment("Data da última alteração do registro (ex: troca de senha).")
                .HasColumnType("datetime")
                .HasColumnName("DT_ALTERACAO");
            entity.Property(e => e.DtCadastro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("Data de criação do registro.")
                .HasColumnType("datetime")
                .HasColumnName("DT_CADASTRO");
            entity.Property(e => e.DtValidadeToken)
                .HasComment("Data e hora em que o token de recuperação de senha expira.")
                .HasColumnType("datetime")
                .HasColumnName("DT_VALIDADE_TOKEN");
            entity.Property(e => e.TxSenhaHash)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasComment("Senha do usuário armazenada em formato hash.")
                .HasColumnName("TX_SENHA_HASH");
            entity.Property(e => e.TxTokenRecuperacao)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasComment("Token gerado para o processo de recuperação de senha.")
                .HasColumnName("TX_TOKEN_RECUPERACAO");

            entity.HasOne(d => d.CdUsuarioNavigation).WithMany(p => p.UsuarioLogins)
                .HasForeignKey(d => d.CdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LOGIN_USUARIO");
        });

        // AulaAnexo
        modelBuilder.Entity<AulaAnexoMOD>(entity =>
        {
            entity.HasKey(e => e.CdAulaAnexo);

            entity.ToTable("AULA_ANEXO", tb => tb.HasComment("Armazena anexos (materiais) associados a uma aula."));

            entity.Property(e => e.CdAulaAnexo)
                .HasComment("Chave primária do anexo. PK.")
                .HasColumnName("CD_AULA_ANEXO");
            entity.Property(e => e.CdAula)
                .HasComment("FK para a tabela AULA.")
                .HasColumnName("CD_AULA");
            entity.Property(e => e.NoArquivoOriginal)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasComment("Nome original do arquivo enviado.")
                .HasColumnName("NO_ARQUIVO_ORIGINAL");
            entity.Property(e => e.TxCaminhoArquivo)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasComment("Caminho onde o arquivo foi armazenado.")
                .HasColumnName("TX_CAMINHO_ARQUIVO");
            entity.Property(e => e.DtUpload)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("Data/hora do upload.")
                .HasColumnType("datetime")
                .HasColumnName("DT_UPLOAD");

            entity.HasOne(d => d.CdAulaNavigation).WithMany(p => p.AulaAnexos)
                .HasForeignKey(d => d.CdAula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ANEXO_AULA");
        });

        // UsuarioTipo
        modelBuilder.Entity<UsuarioTipoMOD>(entity =>
        {
            entity.HasKey(e => e.CdTipoUsuario);
            entity.ToTable("USUARIO_TIPO");
            entity.Property(e => e.CdTipoUsuario).HasColumnName("CD_TIPO_USUARIO");
            entity.Property(e => e.NoTipoUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NO_TIPO_USUARIO");
        });

        // Índice único de email em usuarios (garante unicidade de TX_EMAIL)
        modelBuilder.Entity<UsuarioMOD>()
            .HasIndex(u => u.TxEmail)
            .IsUnique()
            .HasDatabaseName("UX_USUARIO_EMAIL");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
