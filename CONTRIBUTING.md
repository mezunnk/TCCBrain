# 🤝 Guia de Contribuição - Brain Flow

Obrigado por considerar contribuir com o Brain Flow! Este documento fornece diretrizes para contribuições efetivas ao projeto.

## 📋 Antes de Começar

### 🔍 Verifique Issues Existentes
- Procure por issues similares antes de criar uma nova
- Comente na issue se planeja trabalhar nela
- Para grandes mudanças, abra uma issue para discussão primeiro

### 🛠️ Configuração do Ambiente

1. **Fork e Clone**
   ```bash
   git clone https://github.com/seu-usuario/TCCBrain.git
   cd TCCBrain
   ```

2. **Instale Dependências**
   ```bash
   dotnet restore
   ```

3. **Configure o Banco**
   ```bash
   dotnet ef database update --project BrainFlow.Repository
   ```

## 🎯 Tipos de Contribuição

### 🐛 Correção de Bugs
- Descreva o bug claramente
- Inclua passos para reproduzir
- Indique versão/ambiente
- Forneça screenshots se visual

### ✨ Novas Funcionalidades
- Discuta a proposta em uma issue primeiro
- Siga os padrões de design existentes
- Inclua testes quando aplicável
- Documente mudanças significativas

### 📚 Documentação
- Corrija typos e erros
- Melhore exemplos existentes
- Adicione documentação faltante
- Traduções são bem-vindas

### 🎨 Melhorias de UI/UX
- Mantenha consistência com o design system
- Teste em diferentes tamanhos de tela
- Considere acessibilidade
- Inclua screenshots das mudanças

## 📝 Padrões de Código

### 🏗️ Arquitetura
```
- Controllers: Lógica de apresentação
- Repository: Acesso a dados
- Data: Modelos e entidades
- ViewModels: DTOs para views
```

### 🎨 Frontend
```css
/* Use variáveis CSS */
color: var(--navy);

/* Classes semânticas */
.btn-primary { }
.card-course { }
.stats-grid { }
```

### 🔧 Backend
```csharp
// PascalCase para classes e métodos
public class CursoRepository
{
    public async Task<Curso> ObterPorIdAsync(int id)
    {
        // camelCase para variáveis locais
        var curso = await _context.Cursos.FindAsync(id);
        return curso;
    }
}
```

## 🔄 Processo de Pull Request

### 1. 🌿 Crie uma Branch
```bash
git checkout -b feature/nova-funcionalidade
# ou
git checkout -b fix/correcao-bug
# ou
git checkout -b docs/melhoria-readme
```

### 2. 💻 Desenvolva
- Faça commits pequenos e frequentes
- Use mensagens descritivas
- Teste suas mudanças localmente

### 3. 📝 Commit Messages
```bash
# Use o padrão:
git commit -m "tipo: descrição breve

Explicação mais detalhada se necessário.

Fixes #123"
```

**Tipos de commit:**
- `feat:` Nova funcionalidade
- `fix:` Correção de bug
- `docs:` Documentação
- `style:` Formatação, ponto e vírgula
- `refactor:` Refatoração de código
- `test:` Adição/correção de testes
- `chore:` Tarefas de build, configuração

### 4. 🚀 Abra o Pull Request

**Template do PR:**
```markdown
## 📋 Descrição
Breve descrição das mudanças

## 🎯 Tipo de Mudança
- [ ] Bug fix
- [ ] Nova funcionalidade
- [ ] Breaking change
- [ ] Documentação

## 🧪 Como Testar
1. Passo 1
2. Passo 2
3. Resultado esperado

## 📸 Screenshots
(Se aplicável)

## ✅ Checklist
- [ ] Código segue os padrões do projeto
- [ ] Testei as mudanças localmente
- [ ] Documentação atualizada
- [ ] Não quebra funcionalidades existentes
```

## 🧪 Testes

### 🏃‍♂️ Executar Testes
```bash
dotnet test
```

### ✍️ Escrever Testes
```csharp
[Test]
public async Task ObterCurso_DeveRetornarCursoCorreto()
{
    // Arrange
    var cursoId = 1;
    
    // Act
    var resultado = await _repository.ObterPorIdAsync(cursoId);
    
    // Assert
    Assert.NotNull(resultado);
    Assert.Equal(cursoId, resultado.Id);
}
```

## 🎨 Design System

### 🎯 Cores
```css
--navy: #0f2741;        /* Primária */
--accent: #f0b41b;      /* Destaque */
--text: #1f2b3a;        /* Texto */
--muted: #6b7a90;       /* Secundário */
```

### 📏 Espaçamentos
```css
gap: 8px;   /* Pequeno */
gap: 12px;  /* Médio */
gap: 18px;  /* Grande */
gap: 24px;  /* Extra grande */
```

### 🔘 Componentes
```css
.btn { /* Base */ }
.btn.primary { /* Ação principal */ }
.btn.secondary { /* Ação secundária */ }

.card { /* Container base */ }
.card.course { /* Cartão de curso */ }
.card.stats { /* Cartão de estatística */ }
```

## 🚫 O que NÃO Fazer

- ❌ Não faça PRs enormes (quebrem em partes menores)
- ❌ Não altere estilos globais sem discussão
- ❌ Não remova funcionalidades sem deprecation
- ❌ Não commite arquivos de configuração pessoal
- ❌ Não use `!important` no CSS sem justificativa

## 📞 Dúvidas e Suporte

- 📧 **Email**: [email-do-mantenedor]
- 💬 **Issues**: Para discussões técnicas
- 📖 **Wiki**: Para documentação detalhada

## 🎉 Reconhecimento

Todos os contribuidores serão reconhecidos no README principal. Obrigado por ajudar a tornar o Brain Flow melhor!

---

<div align="center">
  <p><strong>Juntos construímos uma plataforma melhor! 🚀</strong></p>
</div>