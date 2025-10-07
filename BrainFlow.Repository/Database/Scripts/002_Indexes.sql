-- ========================================
-- ÍNDICES E OTIMIZAÇÕES - BrainFlow
-- ========================================
-- Arquivo: 002_Indexes.sql
-- Descrição: Criação de índices para otimização de performance

USE brainflow;

-- ==========================
-- ÍNDICES PARA USUARIOS
-- ==========================
CREATE INDEX idx_usuarios_email ON usuarios(email);
CREATE INDEX idx_usuarios_tipo ON usuarios(tipo);
CREATE INDEX idx_usuarios_data_criacao ON usuarios(data_criacao);

-- ==========================
-- ÍNDICES PARA AFILIADOS
-- ==========================
CREATE INDEX idx_afiliados_usuario_id ON afiliados(usuario_id);
CREATE INDEX idx_afiliados_aprovado ON afiliados(aprovado);
CREATE INDEX idx_afiliados_cnpj ON afiliados(cnpj);

-- ==========================
-- ÍNDICES PARA CURSOS
-- ==========================
CREATE INDEX idx_cursos_afiliado_id ON cursos(afiliado_id);
CREATE INDEX idx_cursos_status ON cursos(status);
CREATE INDEX idx_cursos_data_criacao ON cursos(data_criacao);
CREATE INDEX idx_cursos_titulo ON cursos(titulo);

-- ==========================
-- ÍNDICES PARA MÓDULOS E AULAS
-- ==========================
CREATE INDEX idx_modulos_curso_id ON modulos(curso_id);
CREATE INDEX idx_modulos_ordem ON modulos(ordem);
CREATE INDEX idx_aulas_modulo_id ON aulas(modulo_id);
CREATE INDEX idx_aulas_is_free ON aulas(is_free);

-- ==========================
-- ÍNDICES PARA PAGAMENTOS
-- ==========================
CREATE INDEX idx_pagamentos_usuario_id ON pagamentos(usuario_id);
CREATE INDEX idx_pagamentos_curso_id ON pagamentos(curso_id);
CREATE INDEX idx_pagamentos_status ON pagamentos(status);
CREATE INDEX idx_pagamentos_data ON pagamentos(data_pagamento);

-- ==========================
-- ÍNDICES PARA COMISSÕES
-- ==========================
CREATE INDEX idx_comissoes_pagamento_id ON comissoes(pagamento_id);
CREATE INDEX idx_comissoes_afiliado_id ON comissoes(afiliado_id);
CREATE INDEX idx_comissoes_status ON comissoes(status);
CREATE INDEX idx_comissoes_data_calculo ON comissoes(data_calculo);

-- ==========================
-- ÍNDICES PARA PROGRESSO
-- ==========================
CREATE INDEX idx_progresso_usuario_id ON progresso(usuario_id);
CREATE INDEX idx_progresso_aula_id ON progresso(aula_id);
CREATE INDEX idx_progresso_concluida ON progresso(concluida);
CREATE UNIQUE INDEX idx_progresso_unique ON progresso(usuario_id, aula_id);

-- ==========================
-- ÍNDICES PARA FÓRUM
-- ==========================
CREATE INDEX idx_forum_topicos_curso_id ON forum_topicos(curso_id);
CREATE INDEX idx_forum_topicos_usuario_id ON forum_topicos(usuario_id);
CREATE INDEX idx_forum_topicos_data ON forum_topicos(data_criacao);

CREATE INDEX idx_forum_mensagens_topico_id ON forum_mensagens(topico_id);
CREATE INDEX idx_forum_mensagens_usuario_id ON forum_mensagens(usuario_id);
CREATE INDEX idx_forum_mensagens_data ON forum_mensagens(data_envio);