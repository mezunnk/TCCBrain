# Padrões de Navegação - BrainFlow

## 🎯 **Headers por Contexto de Usuário**

### 📱 **Páginas Públicas** (Visitante não logado)
```html
<nav class="menu">
  <a href="home.html">Home</a>
  <a href="cursos.html">Cursos</a>
  <a href="index.html">Login</a>
  <a class="signup" href="cadastro.html">Cadastro</a>
</nav>
```
**Páginas:** home.html, cursos.html, index.html, cadastro.html, sobre.html, suporte.html, termos.html, etc.

---

### 👤 **Usuário Comum Logado**
```html
<nav class="menu">
  <a href="home.html">Home</a>
  <a href="cursos.html">Cursos</a>
  <a href="perfil_usuario.html">Perfil</a>
  <a class="signup" href="index.html">Sair</a>
</nav>
```
**Páginas:** perfil_usuario.html, curso_exemplo.html (assistindo aula)

---

### 💼 **Afiliado Logado**
```html
<nav class="menu">
  <a href="home.html">Home</a>
  <a href="afiliado_dashboard.html">Dashboard</a>
  <a href="afiliado_pagina_config.html">Meus Links</a>
  <a class="signup" href="index.html">Sair</a>
</nav>
```
**Páginas:** afiliado_dashboard.html, afiliado_pagina_config.html

---

### ⚙️ **Admin Logado**
```html
<nav class="menu">
  <a href="home.html">Home</a>
  <a href="cursos.html">Cursos</a>
  <a href="admin_dashboard.html">Admin</a>
  <a class="signup" href="index.html">Sair</a>
</nav>
```
**Páginas:** admin_dashboard.html, admin_aprovacao_afiliados.html

---

## ✅ **Status de Correção**

### ✅ **Corrigidas**
- ✅ perfil_usuario.html → Usuário comum
- ✅ afiliado_dashboard.html → Afiliado  
- ✅ afiliado_pagina_config.html → Afiliado
- ✅ admin_dashboard.html → Admin
- ✅ admin_aprovacao_afiliados.html → Admin
- ✅ curso_exemplo.html → Usuário comum (assistindo aula)
- ✅ curso_form.html → Afiliado (criando curso)
- ✅ checkout.html → Usuário comum (comprando)
- ✅ forum_novo_topico.html → Usuário comum (criando tópico)
- ✅ forum_topico.html → Usuário comum (visualizando tópico)

### ✅ **Já estavam corretas (públicas)**
- ✅ afiliado_cadastro.html → Público
- ✅ afiliado_pagina_publica.html → Público  
- ✅ home.html → Público
- ✅ cursos.html → Público
- ✅ index.html → Público
- ✅ cadastro.html → Público
- ✅ sobre.html → Público
- ✅ suporte.html → Público  
- ✅ termos.html → Público
- ✅ forum_index.html → Público (lista de tópicos)
- ✅ curso.html → Público (detalhes do curso)
- ✅ curso_detalhes.html → Público (preview)
- ✅ recuperar.html → Público
- ✅ default.html → Template público

---

## 🔄 **Próximos Passos**
1. Revisar e corrigir páginas restantes
2. Implementar lógica dinâmica no backend ASP.NET Core
3. Criar partial views para cada tipo de header
4. Implementar middleware de autenticação/autorização