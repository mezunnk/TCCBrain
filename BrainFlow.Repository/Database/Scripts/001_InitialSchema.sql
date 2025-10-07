-- Banco de dados: BrainFlow
CREATE DATABASE brainflow;
USE brainflow;

-- ==========================
-- TABELA: usuarios
-- ==========================
CREATE TABLE usuarios (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    senha VARCHAR(255) NOT NULL,
    tipo ENUM('comum','afiliado','admin') DEFAULT 'comum',
    data_criacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- ==========================
-- TABELA: afiliados (dados adicionais de quem é afiliado)
-- ==========================
CREATE TABLE afiliados (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuario_id INT NOT NULL,
    cnpj VARCHAR(20),
    razao_social VARCHAR(120),
    aprovado BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (usuario_id) REFERENCES usuarios(id)
);

-- ==========================
-- TABELA: cursos
-- ==========================
CREATE TABLE cursos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    afiliado_id INT NOT NULL,
    titulo VARCHAR(150) NOT NULL,
    descricao TEXT,
    preco DECIMAL(10,2) DEFAULT 0.00,
    status ENUM('rascunho','publicado') DEFAULT 'rascunho',
    imagem_capa VARCHAR(255),
    data_criacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (afiliado_id) REFERENCES afiliados(id)
);

-- ==========================
-- TABELA: modulos
-- ==========================
CREATE TABLE modulos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    curso_id INT NOT NULL,
    titulo VARCHAR(150) NOT NULL,
    ordem INT DEFAULT 0,
    FOREIGN KEY (curso_id) REFERENCES cursos(id)
);

-- ==========================
-- TABELA: aulas
-- ==========================
CREATE TABLE aulas (
    id INT AUTO_INCREMENT PRIMARY KEY,
    modulo_id INT NOT NULL,
    titulo VARCHAR(150) NOT NULL,
    descricao TEXT,
    link_video VARCHAR(255),
    is_free BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (modulo_id) REFERENCES modulos(id)
);

-- ==========================
-- TABELA: pagamentos
-- ==========================
CREATE TABLE pagamentos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuario_id INT NOT NULL,
    curso_id INT NOT NULL,
    valor DECIMAL(10,2) NOT NULL,
    status ENUM('pendente','confirmado','cancelado') DEFAULT 'pendente',
    data_pagamento TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (usuario_id) REFERENCES usuarios(id),
    FOREIGN KEY (curso_id) REFERENCES cursos(id)
);

-- ==========================
-- TABELA: comissoes
-- ==========================
CREATE TABLE comissoes (
    id INT AUTO_INCREMENT PRIMARY KEY,
    pagamento_id INT NOT NULL,
    afiliado_id INT NOT NULL,
    valor_comissao DECIMAL(10,2) NOT NULL,
    status ENUM('pendente','pago') DEFAULT 'pendente',
    data_calculo TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (pagamento_id) REFERENCES pagamentos(id),
    FOREIGN KEY (afiliado_id) REFERENCES afiliados(id)
);

-- ==========================
-- TABELA: progresso (rastreamento de aulas assistidas)
-- ==========================
CREATE TABLE progresso (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuario_id INT NOT NULL,
    aula_id INT NOT NULL,
    concluida BOOLEAN DEFAULT FALSE,
    data_atualizacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (usuario_id) REFERENCES usuarios(id),
    FOREIGN KEY (aula_id) REFERENCES aulas(id)
);

-- ==========================
-- TABELA: forum (discussões e mensagens)
-- ==========================
CREATE TABLE forum_topicos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    curso_id INT NOT NULL,
    usuario_id INT NOT NULL,
    titulo VARCHAR(200) NOT NULL,
    data_criacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (curso_id) REFERENCES cursos(id),
    FOREIGN KEY (usuario_id) REFERENCES usuarios(id)
);

CREATE TABLE forum_mensagens (
    id INT AUTO_INCREMENT PRIMARY KEY,
    topico_id INT NOT NULL,
    usuario_id INT NOT NULL,
    mensagem TEXT NOT NULL,
    data_envio TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (topico_id) REFERENCES forum_topicos(id),
    FOREIGN KEY (usuario_id) REFERENCES usuarios(id)
);

-- ==========================
-- TABELA: indicadores (para o painel admin)
-- ==========================
CREATE VIEW vw_indicadores AS
SELECT
    (SELECT COUNT(*) FROM usuarios) AS total_usuarios,
    (SELECT COUNT(*) FROM cursos) AS total_cursos,
    (SELECT COUNT(*) FROM pagamentos WHERE status='confirmado') AS total_vendas,
    (SELECT SUM(valor) FROM pagamentos WHERE status='confirmado') AS faturamento_total,
    (SELECT SUM(valor_comissao) FROM comissoes WHERE status='pago') AS comissoes_pagas;
