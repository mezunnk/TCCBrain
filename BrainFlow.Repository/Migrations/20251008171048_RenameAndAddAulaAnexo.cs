using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrainFlow.Repository.Migrations
{
    public partial class RenameAndAddAulaAnexo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE `pagamentos` RENAME TO `PEDIDO`;");
            migrationBuilder.Sql("ALTER TABLE `progresso` RENAME TO `USUARIO_AULA`;");

            migrationBuilder.Sql(@"CREATE TABLE IF NOT EXISTS `AULA_ANEXO` (
  `CD_AULA_ANEXO` int NOT NULL AUTO_INCREMENT COMMENT 'Chave primária do anexo. PK.',
  `CD_AULA` int NOT NULL COMMENT 'FK para a tabela AULA.',
  `NO_ARQUIVO_ORIGINAL` varchar(255) NOT NULL COMMENT 'Nome original do arquivo enviado.',
  `TX_CAMINHO_ARQUIVO` varchar(500) NOT NULL COMMENT 'Caminho onde o arquivo foi armazenado.',
  `DT_UPLOAD` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Data/hora do upload.',
  PRIMARY KEY (`CD_AULA_ANEXO`),
  CONSTRAINT `FK_ANEXO_AULA` FOREIGN KEY (`CD_AULA`) REFERENCES `aulas`(`CD_AULA`)
) CHARACTER SET=utf8mb4 COMMENT='Armazena anexos (materiais) associados a uma aula.';");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TABLE IF EXISTS `AULA_ANEXO`;");
            migrationBuilder.Sql("ALTER TABLE `PEDIDO` RENAME TO `pagamentos`;");
            migrationBuilder.Sql("ALTER TABLE `USUARIO_AULA` RENAME TO `progresso`;");
        }
    }
}
