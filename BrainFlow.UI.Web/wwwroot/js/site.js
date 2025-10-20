// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function () {
    // Header scroll effect
    const nav = document.querySelector('.nav');
    if (nav) {
        window.addEventListener('scroll', function() {
            if (window.scrollY > 50) {
                nav.classList.add('scrolled');
            } else {
                nav.classList.remove('scrolled');
            }
        });
    }

    // Busca em tempo real
    const searchInputs = document.querySelectorAll('.search');
    searchInputs.forEach(input => {
        let searchTimeout;
        input.addEventListener('input', function() {
            clearTimeout(searchTimeout);
            searchTimeout = setTimeout(() => {
                performSearch(this.value);
            }, 300);
        });
    });

    // Toast notifications
    initializeToasts();

    // Push notifications (mantido do original)
    if ('Notification' in window && 'serviceWorker' in navigator) {
        Notification.requestPermission().then(function (permission) {
            if (permission === 'granted') {
                navigator.serviceWorker.ready.then(function (registration) {
                    registration.pushManager.subscribe({
                        userVisibleOnly: true,
                        applicationServerKey: urlBase64ToUint8Array('SUA_CHAVE_PUBLICA')
                    }).then(function (subscription) {
                        // Enviar a inscrição para o backend
                        fetch('/api/pushNotification/subscribe', {
                            method: 'POST',
                            body: JSON.stringify(subscription),
                            headers: {
                                'Content-Type': 'application/json'
                            }
                        });
                    });
                });
            }
        });
    }
});

function performSearch(query) {
    const cursosGrid = document.getElementById('cursosGrid');
    if (!cursosGrid) return;

    // Mostrar loading
    cursosGrid.innerHTML = '<div class="loading">Buscando cursos...</div>';

    fetch(`/Conta/BuscarCursos?termo=${encodeURIComponent(query)}`)
        .then(response => response.text())
        .then(html => {
            cursosGrid.innerHTML = html;
        })
        .catch(error => {
            console.error('Erro na busca:', error);
            cursosGrid.innerHTML = '<p class="error">Erro ao buscar cursos. Tente novamente.</p>';
        });
}

function initializeToasts() {
    // Implementar sistema de toasts baseado em TempData
    const successMessage = document.querySelector('[data-toast="success"]');
    const errorMessage = document.querySelector('[data-toast="error"]');

    if (successMessage) {
        showToast(successMessage.textContent, 'success');
    }
    if (errorMessage) {
        showToast(errorMessage.textContent, 'error');
    }
}

function showToast(message, type = 'info') {
    // Criar elemento toast
    const toast = document.createElement('div');
    toast.className = `toast toast-${type}`;
    toast.innerHTML = `
        <div class="toast-message">${message}</div>
        <button class="toast-close" onclick="this.parentElement.remove()">×</button>
    `;

    // Adicionar ao container
    let container = document.querySelector('.toast-container');
    if (!container) {
        container = document.createElement('div');
        container.className = 'toast-container';
        document.body.appendChild(container);
    }
    container.appendChild(toast);

    // Auto-remover após 5 segundos
    setTimeout(() => {
        if (toast.parentElement) {
            toast.remove();
        }
    }, 5000);
}

function urlBase64ToUint8Array(base64String) {
    var padding = '='.repeat((4 - base64String.length % 4) % 4);
    var base64 = (base64String + padding)
        .replace(/\-/g, '+')
        .replace(/_/g, '/');
    var rawData = window.atob(base64);
    var outputArray = new Uint8Array(rawData.length);
    for (var i = 0; i < rawData.length; ++i) {
        outputArray[i] = rawData.charCodeAt(i);
    }
    return outputArray;
}
