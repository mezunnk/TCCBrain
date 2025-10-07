-- ========================================
-- DADOS DE TESTE - COMPATÍVEL COM EF
-- ========================================

USE brainflow;

-- ==========================
-- USUÁRIOS
-- ==========================
INSERT INTO usuarios (CD_USUARIO, CD_TIPO_USUARIO, NO_USUARIO, TX_EMAIL, SN_ATIVO) VALUES 
(1, 3, 'Administrador', 'admin@brainflow.com', TRUE),
(2, 2, 'Maria Silva', 'maria@exemplo.com', TRUE),
(3, 1, 'João Santos', 'joao@exemplo.com', TRUE),
(4, 2, 'Ana Costa', 'ana@exemplo.com', TRUE),
(5, 1, 'Pedro Oliveira', 'pedro@exemplo.com', TRUE),
(6, 2, 'Carla Ferreira', 'carla@exemplo.com', TRUE),
(7, 1, 'Lucas Mendes', 'lucas@exemplo.com', TRUE),
(8, 1, 'Sofia Lima', 'sofia@exemplo.com', TRUE);

-- ==========================
-- LOGINS (SENHAS)
-- ==========================
-- Senha: password para todos
INSERT INTO usuario_login (CD_USUARIO, TX_SENHA_HASH) VALUES 
(1, '$2a$10$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi'),
(2, '$2a$10$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi'),
(3, '$2a$10$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi'),
(4, '$2a$10$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi'),
(5, '$2a$10$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi'),
(6, '$2a$10$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi'),
(7, '$2a$10$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi'),
(8, '$2a$10$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi');

-- ==========================
-- AFILIADOS
-- ==========================
INSERT INTO afiliados (CD_AFILIADO, CD_USUARIO, TX_CNPJ, TX_RAZAO_SOCIAL, SN_APROVADO) VALUES 
(1, 2, '12.345.678/0001-90', 'Maria Silva ME', TRUE),
(2, 4, '98.765.432/0001-10', 'Ana Costa Cursos LTDA', TRUE),
(3, 6, '11.222.333/0001-44', 'Carla Ferreira Educação LTDA', TRUE);

-- ==========================
-- CURSOS
-- ==========================
INSERT INTO cursos (CD_CURSO, CD_AFILIADO, NO_CURSO, DS_CURSO, VL_PRECO, TX_STATUS) VALUES 
(1, 1, 'Introdução ao JavaScript', 'Aprenda os fundamentos da linguagem mais popular da web', 49.90, 'publicado'),
(2, 1, 'HTML & CSS Básico', 'Construa suas primeiras páginas web', 39.90, 'publicado'),
(3, 2, 'React para Iniciantes', 'Desenvolva aplicações modernas com React', 89.90, 'publicado'),
(4, 2, 'Node.js Completo', 'Backend completo com Node.js e Express', 120.00, 'publicado'),
(5, 3, 'Python para Data Science', 'Análise de dados com Python e Pandas', 79.90, 'publicado'),
(6, 3, 'Machine Learning Prático', 'Algoritmos de ML aplicados', 150.00, 'rascunho'),
(7, 1, 'CSS Grid e Flexbox', 'Layout moderno com CSS', 29.90, 'publicado');

-- ==========================
-- MÓDULOS
-- ==========================
INSERT INTO modulos (CD_MODULO, CD_CURSO, NO_MODULO, NU_ORDEM) VALUES 
-- JavaScript
(1, 1, 'Fundamentos', 1),
(2, 1, 'Manipulação do DOM', 2),
(3, 1, 'Projetos Práticos', 3),
-- HTML & CSS
(4, 2, 'HTML Estrutural', 1),
(5, 2, 'CSS Estilização', 2),
(6, 2, 'Layout Responsivo', 3),
-- React
(7, 3, 'Conceitos Básicos', 1),
(8, 3, 'Componentes e Props', 2),
(9, 3, 'Estado e Eventos', 3),
-- Python
(10, 5, 'Python Básico', 1),
(11, 5, 'Pandas e NumPy', 2),
(12, 5, 'Visualização de Dados', 3);

-- ==========================
-- AULAS
-- ==========================
INSERT INTO aulas (CD_AULA, CD_MODULO, NO_AULA, DS_AULA, SN_GRATUITA) VALUES 
-- JavaScript - Fundamentos
(1, 1, 'O que é JavaScript?', 'Introdução à linguagem JavaScript', TRUE),
(2, 1, 'Variáveis e Tipos', 'Aprendendo sobre variáveis e tipos de dados', FALSE),
(3, 1, 'Funções', 'Como criar e usar funções', FALSE),
(4, 1, 'Arrays e Objetos', 'Estruturas de dados em JavaScript', FALSE),
-- JavaScript - DOM
(5, 2, 'Selecionando Elementos', 'Como selecionar elementos HTML', TRUE),
(6, 2, 'Eventos', 'Trabalhando com eventos do usuário', FALSE),
(7, 2, 'Manipulando CSS', 'Alterando estilos com JavaScript', FALSE),
-- HTML
(8, 4, 'Estrutura básica do HTML', 'Tags fundamentais do HTML5', TRUE),
(9, 4, 'Formulários', 'Criando formulários interativos', FALSE),
(10, 4, 'Semântica HTML5', 'Tags semânticas modernas', FALSE),
-- CSS
(11, 5, 'Seletores CSS', 'Como selecionar elementos para estilizar', FALSE),
(12, 5, 'Box Model', 'Entendendo o modelo de caixa', FALSE),
(13, 5, 'Cores e Tipografia', 'Trabalhando com cores e fontes', FALSE),
-- React
(14, 7, 'O que é React?', 'Introdução ao React', TRUE),
(15, 7, 'JSX', 'Sintaxe JSX explicada', FALSE),
(16, 7, 'Criando o primeiro componente', 'Seu primeiro componente React', FALSE);

-- ==========================
-- PAGAMENTOS
-- ==========================
INSERT INTO pagamentos (CD_PEDIDO, CD_USUARIO, CD_CURSO, VL_TOTAL, TX_STATUS, DT_PEDIDO) VALUES 
(1, 3, 1, 49.90, 'confirmado', '2024-10-01 10:30:00'),  -- João comprou JavaScript
(2, 5, 1, 49.90, 'confirmado', '2024-10-02 14:15:00'),  -- Pedro comprou JavaScript
(3, 3, 2, 39.90, 'confirmado', '2024-10-03 09:45:00'),  -- João comprou HTML & CSS
(4, 7, 3, 89.90, 'confirmado', '2024-10-04 16:20:00'),  -- Lucas comprou React
(5, 8, 5, 79.90, 'confirmado', '2024-10-05 11:30:00'),  -- Sofia comprou Python
(6, 5, 2, 39.90, 'pendente', '2024-10-06 08:15:00');   -- Pedro tentou comprar HTML & CSS

-- ==========================
-- COMISSÕES
-- ==========================
INSERT INTO comissoes (CD_COMISSAO, CD_PEDIDO, CD_AFILIADO, VL_COMISSAO, TX_STATUS, DT_CALCULO) VALUES 
(1, 1, 1, 14.97, 'pago', '2024-10-01 10:31:00'),       -- 30% de 49.90
(2, 2, 1, 14.97, 'pago', '2024-10-02 14:16:00'),       -- 30% de 49.90
(3, 3, 1, 11.97, 'pago', '2024-10-03 09:46:00'),       -- 30% de 39.90
(4, 4, 2, 26.97, 'pendente', '2024-10-04 16:21:00'),   -- 30% de 89.90
(5, 5, 3, 23.97, 'pendente', '2024-10-05 11:31:00');   -- 30% de 79.90

-- ==========================
-- PROGRESSO DOS ESTUDANTES
-- ==========================
INSERT INTO progresso (CD_USUARIO, CD_AULA, SN_CONCLUIDA, DT_INICIO, DT_CONCLUSAO) VALUES 
-- João progresso no JavaScript
(3, 1, TRUE, '2024-10-01 11:00:00', '2024-10-01 11:15:00'),   -- O que é JavaScript?
(3, 2, TRUE, '2024-10-01 12:00:00', '2024-10-01 12:30:00'),   -- Variáveis e Tipos
(3, 3, FALSE, '2024-10-01 13:00:00', NULL),  -- Funções (iniciada)
(3, 5, TRUE, '2024-10-03 10:00:00', '2024-10-03 10:20:00'),   -- Selecionando Elementos

-- Pedro progresso no JavaScript
(5, 1, TRUE, '2024-10-02 15:00:00', '2024-10-02 15:15:00'),   -- O que é JavaScript?
(5, 2, FALSE, '2024-10-02 16:00:00', NULL),  -- Variáveis e Tipos (iniciada)

-- Lucas progresso no React
(7, 14, TRUE, '2024-10-04 17:00:00', '2024-10-04 17:15:00'),  -- O que é React?
(7, 15, FALSE, '2024-10-04 18:00:00', NULL), -- JSX (iniciada)

-- João progresso no HTML & CSS
(3, 8, TRUE, '2024-10-03 11:00:00', '2024-10-03 11:20:00');   -- Estrutura básica do HTML