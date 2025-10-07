# Database Scripts - BrainFlow

Esta pasta contém todos os scripts SQL relacionados ao banco de dados do projeto BrainFlow.

## Arquivos Atuais

### 📁 **Scripts/**
- `001_InitialSchema.sql` - **Schema completo do BrainFlow** (MySQL/MariaDB)
  - Criação do database `brainflow`
  - Todas as tabelas: usuarios, afiliados, cursos, modulos, aulas, pagamentos, comissoes, progresso, forum
  - View de indicadores para dashboard admin
- `002_Indexes.sql` - Índices para otimização de performance

### 📁 **SeedData/**
- `SeedData_Basic.sql` - Dados iniciais essenciais
  - Usuário administrador padrão
  - Usuários de teste para desenvolvimento
  - Cursos, módulos e aulas de exemplo

## Estrutura do Banco

### Tabelas Principais:
- **usuarios** - Dados básicos (comum/afiliado/admin)
- **afiliados** - Dados adicionais de afiliados (CNPJ, aprovação)
- **cursos** - Cursos criados pelos afiliados
- **modulos** / **aulas** - Estrutura hierárquica do conteúdo
- **pagamentos** / **comissoes** - Sistema financeiro
- **progresso** - Rastreamento de conclusão de aulas
- **forum_topicos** / **forum_mensagens** - Sistema de discussões

### Relacionamentos:
```
usuarios (1:1) afiliados (1:N) cursos (1:N) modulos (1:N) aulas
usuarios (1:N) pagamentos (1:N) comissoes
usuarios (1:N) progresso (N:1) aulas
```

## Como Executar

### Para Nova Instalação:
```bash
# 1. Execute o schema inicial
mysql -u root -p < Scripts/001_InitialSchema.sql

# 2. Adicione os índices
mysql -u root -p < Scripts/002_Indexes.sql

# 3. Insira dados básicos
mysql -u root -p < SeedData/SeedData_Basic.sql
```

### Credenciais Padrão:
- **Admin**: admin@brainflow.com / password
- **Teste**: maria@exemplo.com / password

⚠️ **ALTERE AS SENHAS EM PRODUÇÃO!**