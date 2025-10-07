-- Script para ajustar a tabela cursos para ser compatível com o modelo CursoMOD

USE brainflow;

-- Adicionar as colunas que estão faltando e ajustar as existentes
ALTER TABLE cursos 
DROP COLUMN TX_STATUS;

ALTER TABLE cursos 
ADD COLUMN SN_ATIVO BOOLEAN DEFAULT TRUE COMMENT 'Flag que indica se o curso está ativo (1) ou inativo (0).';

ALTER TABLE cursos 
ADD COLUMN SN_APROVADO BOOLEAN DEFAULT FALSE COMMENT 'Flag que indica se o curso foi aprovado (1) pelo admin para publicação.';

ALTER TABLE cursos 
ADD COLUMN DT_AVALIACAO_ADMIN DATETIME NULL COMMENT 'Data em que o admin avaliou o curso.';

-- Renomear colunas para corresponder ao modelo
ALTER TABLE cursos 
CHANGE COLUMN VL_PRECO DC_VALOR DECIMAL(10,2) DEFAULT 0.00 COMMENT 'Valor de venda do curso. 0 para cursos gratuitos.';

ALTER TABLE cursos 
CHANGE COLUMN DS_CURSO TX_DESCRICAO TEXT COMMENT 'Descrição completa e detalhada do curso.';

ALTER TABLE cursos 
CHANGE COLUMN TX_IMAGEM_CAPA TX_CAMINHO_IMAGEM VARCHAR(500) NULL COMMENT 'Caminho para a imagem de capa do curso.';

-- Atualizar todos os cursos existentes para serem ativos e aprovados (para aparecerem na listagem)
UPDATE cursos SET 
    SN_ATIVO = TRUE,
    SN_APROVADO = TRUE;