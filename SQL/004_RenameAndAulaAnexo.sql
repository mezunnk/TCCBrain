-- Script Manual: Renomear tabelas e criar AULA_ANEXO
-- Pré-condição: executar em banco MySQL já contendo as tabelas `pagamentos` e `progresso` originais.
-- Ajuste conforme necessário se já tiver sido aplicado.

-- Renomear tabelas
ALTER TABLE `pagamentos` RENAME TO `PEDIDO`;
ALTER TABLE `progresso` RENAME TO `USUARIO_AULA`;

-- Criar tabela de anexos (idempotente: só cria se não existir)
CREATE TABLE IF NOT EXISTS `AULA_ANEXO` (
  `CD_AULA_ANEXO` int NOT NULL AUTO_INCREMENT COMMENT 'Chave primária do anexo. PK.',
  `CD_AULA` int NOT NULL COMMENT 'FK para a tabela AULA.',
  `NO_ARQUIVO_ORIGINAL` varchar(255) NOT NULL COMMENT 'Nome original do arquivo enviado.',
  `TX_CAMINHO_ARQUIVO` varchar(500) NOT NULL COMMENT 'Caminho onde o arquivo foi armazenado.',
  `DT_UPLOAD` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Data/hora do upload.',
  PRIMARY KEY (`CD_AULA_ANEXO`),
  CONSTRAINT `FK_ANEXO_AULA` FOREIGN KEY (`CD_AULA`) REFERENCES `aulas`(`CD_AULA`)
) CHARACTER SET=utf8mb4 COMMENT='Armazena anexos (materiais) associados a uma aula.';

-- Fim do script
