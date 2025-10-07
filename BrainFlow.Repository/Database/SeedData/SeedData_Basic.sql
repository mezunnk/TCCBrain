-- ========================================
-- DADOS INICIAIS - BrainFlow
-- ========================================
-- Arquivo: SeedData_Basic.sql
-- Descrição: Dados básicos necessários para funcionamento do sistema

USE brainflow;

-- ==========================
-- USUÁRIO ADMINISTRADOR PADRÃO
-- ==========================
INSERT INTO usuarios (nome, email, senha, tipo) VALUES 
('Administrador', 'admin@brainflow.com', '$2a$10$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi', 'admin');
-- Senha padrão: password (deve ser alterada em produção)

-- ==========================
-- USUÁRIOS DE TESTE (DESENVOLVIMENTO)
-- ==========================
INSERT INTO usuarios (nome, email, senha, tipo) VALUES 
('Maria Silva', 'maria@exemplo.com', '$2a$10$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi', 'afiliado'),
('João Santos', 'joao@exemplo.com', '$2a$10$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi', 'comum'),
('Ana Costa', 'ana@exemplo.com', '$2a$10$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi', 'afiliado'),
('Pedro Oliveira', 'pedro@exemplo.com', '$2a$10$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi', 'comum'),
('Carla Ferreira', 'carla@exemplo.com', '$2a$10$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi', 'afiliado'),
('Lucas Mendes', 'lucas@exemplo.com', '$2a$10$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi', 'comum'),
('Sofia Lima', 'sofia@exemplo.com', '$2a$10$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi', 'comum');

-- ==========================
-- DADOS DE AFILIADOS
-- ==========================
INSERT INTO afiliados (usuario_id, cnpj, razao_social, aprovado) VALUES 
(2, '12.345.678/0001-90', 'Maria Silva ME', TRUE),
(4, '98.765.432/0001-10', 'Ana Costa Cursos LTDA', TRUE),
(6, '11.222.333/0001-44', 'Carla Ferreira Educação LTDA', TRUE);

-- ==========================
-- CURSOS DE EXEMPLO
-- ==========================
INSERT INTO cursos (afiliado_id, titulo, descricao, preco, status) VALUES 
(1, 'Introdução ao JavaScript', 'Aprenda os fundamentos da linguagem mais popular da web', 49.90, 'publicado'),
(1, 'HTML & CSS Básico', 'Construa suas primeiras páginas web', 39.90, 'publicado'),
(2, 'React para Iniciantes', 'Desenvolva aplicações modernas com React', 89.90, 'publicado'),
(2, 'Node.js Completo', 'Backend completo com Node.js e Express', 120.00, 'publicado'),
(3, 'Python para Data Science', 'Análise de dados com Python e Pandas', 79.90, 'publicado'),
(3, 'Machine Learning Prático', 'Algoritmos de ML aplicados', 150.00, 'rascunho'),
(1, 'CSS Grid e Flexbox', 'Layout moderno com CSS', 29.90, 'publicado');

-- ==========================
-- MÓDULOS E AULAS DE EXEMPLO
-- ==========================
-- Módulos do curso JavaScript (ID 1)
INSERT INTO modulos (curso_id, titulo, ordem) VALUES 
(1, 'Fundamentos', 1),
(1, 'Manipulação do DOM', 2),
(1, 'Projetos Práticos', 3);

-- Módulos do curso HTML & CSS (ID 2)
INSERT INTO modulos (curso_id, titulo, ordem) VALUES 
(2, 'HTML Estrutural', 1),
(2, 'CSS Estilização', 2),
(2, 'Layout Responsivo', 3);

-- Módulos do curso React (ID 3)
INSERT INTO modulos (curso_id, titulo, ordem) VALUES 
(3, 'Conceitos Básicos', 1),
(3, 'Componentes e Props', 2),
(3, 'Estado e Eventos', 3),
(3, 'Hooks Avançados', 4);

-- Módulos do curso Python Data Science (ID 5)
INSERT INTO modulos (curso_id, titulo, ordem) VALUES 
(5, 'Python Básico', 1),
(5, 'Pandas e NumPy', 2),
(5, 'Visualização de Dados', 3);

-- Aulas do módulo JavaScript - Fundamentos (módulo ID 1)
INSERT INTO aulas (modulo_id, titulo, descricao, is_free) VALUES 
(1, 'O que é JavaScript?', 'Introdução à linguagem JavaScript', TRUE),
(1, 'Variáveis e Tipos', 'Aprendendo sobre variáveis e tipos de dados', FALSE),
(1, 'Funções', 'Como criar e usar funções', FALSE),
(1, 'Arrays e Objetos', 'Estruturas de dados em JavaScript', FALSE);

-- Aulas do módulo JavaScript - DOM (módulo ID 2)
INSERT INTO aulas (modulo_id, titulo, descricao, is_free) VALUES 
(2, 'Selecionando Elementos', 'Como selecionar elementos HTML', TRUE),
(2, 'Eventos', 'Trabalhando com eventos do usuário', FALSE),
(2, 'Manipulando CSS', 'Alterando estilos com JavaScript', FALSE);

-- Aulas do módulo HTML Estrutural (módulo ID 4)
INSERT INTO aulas (modulo_id, titulo, descricao, is_free) VALUES 
(4, 'Estrutura básica do HTML', 'Tags fundamentais do HTML5', TRUE),
(4, 'Formulários', 'Criando formulários interativos', FALSE),
(4, 'Semântica HTML5', 'Tags semânticas modernas', FALSE);

-- Aulas do módulo CSS Estilização (módulo ID 5)
INSERT INTO aulas (modulo_id, titulo, descricao, is_free) VALUES 
(5, 'Seletores CSS', 'Como selecionar elementos para estilizar', FALSE),
(5, 'Box Model', 'Entendendo o modelo de caixa', FALSE),
(5, 'Cores e Tipografia', 'Trabalhando com cores e fontes', FALSE);

-- Aulas do módulo React - Conceitos Básicos (módulo ID 6)
INSERT INTO aulas (modulo_id, titulo, descricao, is_free) VALUES 
(6, 'O que é React?', 'Introdução ao React', TRUE),
(6, 'JSX', 'Sintaxe JSX explicada', FALSE),
(6, 'Criando o primeiro componente', 'Seu primeiro componente React', FALSE);

-- ==========================
-- PAGAMENTOS DE EXEMPLO
-- ==========================
INSERT INTO pagamentos (usuario_id, curso_id, valor, status, data_pagamento) VALUES 
(3, 1, 49.90, 'confirmado', '2024-10-01 10:30:00'),  -- João comprou JavaScript
(5, 1, 49.90, 'confirmado', '2024-10-02 14:15:00'),  -- Pedro comprou JavaScript
(3, 2, 39.90, 'confirmado', '2024-10-03 09:45:00'),  -- João comprou HTML & CSS
(6, 3, 89.90, 'confirmado', '2024-10-04 16:20:00'),  -- Lucas comprou React
(7, 5, 79.90, 'confirmado', '2024-10-05 11:30:00'),  -- Sofia comprou Python
(5, 2, 39.90, 'pendente', '2024-10-06 08:15:00');   -- Pedro tentou comprar HTML & CSS

-- ==========================
-- COMISSÕES GERADAS
-- ==========================
INSERT INTO comissoes (pagamento_id, afiliado_id, valor_comissao, status, data_calculo) VALUES 
(1, 1, 14.97, 'pago', '2024-10-01 10:31:00'),       -- 30% de 49.90
(2, 1, 14.97, 'pago', '2024-10-02 14:16:00'),       -- 30% de 49.90
(3, 1, 11.97, 'pago', '2024-10-03 09:46:00'),       -- 30% de 39.90
(4, 2, 26.97, 'pendente', '2024-10-04 16:21:00'),   -- 30% de 89.90
(5, 3, 23.97, 'pendente', '2024-10-05 11:31:00');   -- 30% de 79.90

-- ==========================
-- PROGRESSO DOS ESTUDANTES
-- ==========================
INSERT INTO progresso (usuario_id, aula_id, concluida, data_atualizacao) VALUES 
-- João progresso no JavaScript
(3, 1, TRUE, '2024-10-01 11:00:00'),   -- O que é JavaScript? (concluída)
(3, 2, TRUE, '2024-10-01 12:00:00'),   -- Variáveis e Tipos (concluída)
(3, 3, FALSE, '2024-10-01 13:00:00'),  -- Funções (iniciada)
(3, 5, TRUE, '2024-10-03 10:00:00'),   -- Selecionando Elementos (concluída)

-- Pedro progresso no JavaScript
(5, 1, TRUE, '2024-10-02 15:00:00'),   -- O que é JavaScript? (concluída)
(5, 2, FALSE, '2024-10-02 16:00:00'),  -- Variáveis e Tipos (iniciada)

-- Lucas progresso no React
(6, 13, TRUE, '2024-10-04 17:00:00'),  -- O que é React? (concluída)
(6, 14, FALSE, '2024-10-04 18:00:00'), -- JSX (iniciada)

-- Sofia progresso no Python (ainda não começou as aulas)

-- João progresso no HTML & CSS
(3, 8, TRUE, '2024-10-03 11:00:00');   -- Estrutura básica do HTML (concluída)

-- ==========================
-- TÓPICOS DO FÓRUM
-- ==========================
INSERT INTO forum_topicos (curso_id, usuario_id, titulo, data_criacao) VALUES 
(1, 3, 'Dúvida sobre closures em JavaScript', '2024-10-01 14:30:00'),
(1, 5, 'Como usar arrow functions?', '2024-10-02 20:15:00'),
(3, 6, 'Diferença entre state e props', '2024-10-04 19:45:00'),
(2, 3, 'Flexbox vs Grid - quando usar?', '2024-10-03 12:30:00');

-- ==========================
-- MENSAGENS DO FÓRUM
-- ==========================
INSERT INTO forum_mensagens (topico_id, usuario_id, mensagem, data_envio) VALUES 
(1, 2, 'Closures são uma das funcionalidades mais poderosas do JavaScript. Elas permitem que funções internas acessem variáveis de funções externas...', '2024-10-01 15:00:00'),
(1, 3, 'Obrigado! Agora entendi melhor o conceito.', '2024-10-01 15:30:00'),
(2, 2, 'Arrow functions são uma forma mais concisa de escrever funções: const sum = (a, b) => a + b;', '2024-10-02 21:00:00'),
(3, 4, 'State é o estado interno do componente, props são propriedades passadas pelo componente pai.', '2024-10-04 20:00:00'),
(4, 2, 'Use Grid para layouts 2D e Flexbox para layouts 1D. Grid é melhor para estruturas complexas.', '2024-10-03 13:00:00');

-- ==========================
-- ESTATÍSTICAS DE TESTE
-- ==========================
-- Com estes dados você pode testar:
-- ✅ 7 usuários (1 admin, 3 afiliados, 3 estudantes)
-- ✅ 7 cursos (6 publicados, 1 rascunho)
-- ✅ 10 módulos com 15 aulas
-- ✅ 6 pagamentos (5 confirmados, 1 pendente)
-- ✅ 5 comissões (3 pagas, 2 pendentes)
-- ✅ Progresso de estudantes em diferentes estágios
-- ✅ 4 tópicos de fórum com 5 mensagens

-- ==========================
-- CREDENCIAIS PARA TESTE
-- ==========================
-- ADMINISTRADOR:
-- Email: admin@brainflow.com | Senha: password

-- AFILIADOS:
-- Email: maria@exemplo.com | Senha: password (3 cursos, comissões pagas)
-- Email: ana@exemplo.com | Senha: password (2 cursos, comissão pendente)
-- Email: carla@exemplo.com | Senha: password (1 curso)

-- ESTUDANTES:
-- Email: joao@exemplo.com | Senha: password (comprou 2 cursos, progresso ativo)
-- Email: pedro@exemplo.com | Senha: password (comprou 1 curso, iniciando)
-- Email: lucas@exemplo.com | Senha: password (comprou React, progresso inicial)
-- Email: sofia@exemplo.com | Senha: password (comprou Python, não iniciou)

-- ==========================
-- NOTA DE SEGURANÇA
-- ==========================
-- IMPORTANTE: Em produção:
-- 1. Altere TODAS as senhas padrão
-- 2. Remove ou altere os usuários/dados de teste
-- 3. Use senhas hash reais com BCrypt
-- 4. Configure properly as permissões de banco
-- 5. Remova dados pessoais de exemplo

-- ==========================
-- CENÁRIOS DE TESTE SUGERIDOS
-- ==========================
-- 🔹 Dashboard Administrativo:
--   Faça login como admin@brainflow.com para gerenciar usuários e cursos

-- 🔹 Dashboard de Afiliado Ativo:
--   Login como maria@exemplo.com - Veja comissões pagas e vendas

-- 🔹 Dashboard de Afiliado Iniciante:
--   Login como carla@exemplo.com - Veja dashboard sem vendas ainda

-- 🔹 Experiência do Estudante:
--   Login como joao@exemplo.com - Acesse cursos comprados e continue aulas

-- 🔹 Teste de Progressão:
--   Login como pedro@exemplo.com - Inicie o curso de JavaScript do zero

-- 🔹 Sistema de Fórum:
--   Qualquer usuário pode participar das discussões criadas

-- 🔹 Simulação de Vendas:
--   Crie novos pedidos para testar o sistema de comissões

-- FIM DO ARQUIVO DE DADOS DE TESTE