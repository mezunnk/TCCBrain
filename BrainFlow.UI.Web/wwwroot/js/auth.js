// Sistema de Autenticação Dinâmica - BrainFlow
window.BrainFlowAuth = {
    // Estado da autenticação
    isAuthenticated: false,
    userInfo: null,

    // Inicializar sistema
    init: function() {
        this.checkAuthentication();
        this.updateNavigation();
        this.setupLogoutHandlers();
    },

    // Verificar estado de autenticação
    checkAuthentication: function() {
        fetch('/Conta/GetUserInfo')
            .then(response => response.json())
            .then(data => {
                this.isAuthenticated = data.IsAuthenticated;
                this.userInfo = data;
                this.updateNavigation();
            })
            .catch(error => {
                console.log('Erro ao verificar autenticação:', error);
                this.isAuthenticated = false;
                this.userInfo = null;
                this.updateNavigation();
            });
    },

    // Atualizar navegação baseada no estado
    updateNavigation: function() {
        const nav = document.querySelector('.menu');
        if (!nav) return;

        if (this.isAuthenticated && this.userInfo) {
            // Usuário logado - mostrar menu apropriado
            this.showAuthenticatedMenu(nav);
        } else {
            // Usuário não logado - mostrar menu público
            this.showPublicMenu(nav);
        }
    },

    // Menu para usuários autenticados
    showAuthenticatedMenu: function(nav) {
        const userType = this.userInfo.CdTipoUsuario;
        let menuHTML = '';

        // Menu comum para todos os usuários logados
        menuHTML += '<a href="/Home/Index">Home</a>';
        menuHTML += '<a href="/Home/Cursos">Cursos</a>';

        // Menu específico por tipo de usuário
        switch (userType) {
            case 1: // Usuário comum
                menuHTML += '<a href="/Perfil/Index">Perfil</a>';
                break;
            case 2: // Afiliado
                menuHTML += '<a href="/Afiliado/Dashboard">Dashboard</a>';
                menuHTML += '<a href="/Afiliado/MeusLinks">Meus Links</a>';
                break;
            case 3: // Admin
                menuHTML += '<a href="/Admin/Index">Admin</a>';
                break;
        }

        // Botão de logout
        menuHTML += `
            <a href="#" onclick="BrainFlowAuth.logout(); return false;" class="logout">
                Sair (${this.userInfo.Nome})
            </a>
        `;

        nav.innerHTML = menuHTML;
    },

    // Menu para usuários não autenticados
    showPublicMenu: function(nav) {
        nav.innerHTML = `
            <a href="/Home/Index">Home</a>
            <a href="/Home/Cursos">Cursos</a>
            <a href="/Conta/Login">Login</a>
            <a href="/Conta/Cadastro" class="signup">Cadastro</a>
        `;
    },

    // Configurar handlers de logout
    setupLogoutHandlers: function() {
        // Interceptar forms de logout
        document.addEventListener('submit', (e) => {
            if (e.target.action && e.target.action.includes('/Conta/Logout')) {
                e.preventDefault();
                this.logout();
            }
        });
    },

    // Fazer logout
    logout: function() {
        // Criar form temporário para logout
        const form = document.createElement('form');
        form.method = 'POST';
        form.action = '/Conta/Logout';
        
        // Adicionar token antiforgery
        const token = document.querySelector('input[name="__RequestVerificationToken"]');
        if (token) {
            const tokenInput = document.createElement('input');
            tokenInput.type = 'hidden';
            tokenInput.name = '__RequestVerificationToken';
            tokenInput.value = token.value;
            form.appendChild(tokenInput);
        }

        document.body.appendChild(form);
        form.submit();
    },

    // Fazer login programaticamente
    login: function(email, password, rememberMe = false) {
        const formData = new FormData();
        formData.append('Email', email);
        formData.append('Senha', password);
        formData.append('LembrarMe', rememberMe);
        
        const token = document.querySelector('input[name="__RequestVerificationToken"]');
        if (token) {
            formData.append('__RequestVerificationToken', token.value);
        }

        return fetch('/Conta/Login', {
            method: 'POST',
            body: formData
        }).then(response => {
            if (response.ok) {
                this.checkAuthentication();
                return { success: true };
            } else {
                return { success: false, message: 'Erro no login' };
            }
        });
    },

    // Registrar novo usuário
    register: function(nome, email, senha, confirmarSenha, tipoUsuario = 1) {
        const formData = new FormData();
        formData.append('Nome', nome);
        formData.append('Email', email);
        formData.append('Senha', senha);
        formData.append('ConfirmarSenha', confirmarSenha);
        formData.append('TipoUsuario', tipoUsuario);
        formData.append('AceitarTermos', true);
        
        const token = document.querySelector('input[name="__RequestVerificationToken"]');
        if (token) {
            formData.append('__RequestVerificationToken', token.value);
        }

        return fetch('/Conta/Registro', {
            method: 'POST',
            body: formData
        }).then(response => {
            if (response.ok) {
                return { success: true, message: 'Conta criada com sucesso!' };
            } else {
                return { success: false, message: 'Erro ao criar conta' };
            }
        });
    },

    // Verificar se tem acesso a uma página
    hasAccess: function(requiredRole) {
        if (!this.isAuthenticated) return false;
        
        const userRole = this.userInfo.CdTipoUsuario;
        
        switch (requiredRole) {
            case 'admin':
                return userRole === 3;
            case 'afiliado':
                return userRole === 2 || userRole === 3;
            case 'usuario':
                return userRole >= 1;
            default:
                return this.isAuthenticated;
        }
    },

    // Redirecionar baseado no tipo de usuário
    redirectToHome: function() {
        if (!this.isAuthenticated) {
            window.location.href = '/Conta/Login';
            return;
        }

        const userType = this.userInfo.CdTipoUsuario;
        switch (userType) {
            case 2: // Afiliado
                window.location.href = '/Afiliado/Dashboard';
                break;
            case 3: // Admin
                window.location.href = '/Admin/Index';
                break;
            default: // Usuário comum
                window.location.href = '/Dashboard/Index';
                break;
        }
    }
};

// Inicializar quando página carrega
document.addEventListener('DOMContentLoaded', function() {
    BrainFlowAuth.init();
});

// Atualizar a cada 5 minutos para verificar se sessão ainda está ativa
setInterval(function() {
    BrainFlowAuth.checkAuthentication();
}, 5 * 60 * 1000);