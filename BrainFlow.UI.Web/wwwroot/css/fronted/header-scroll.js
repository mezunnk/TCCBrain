// Header scroll effect
document.addEventListener('DOMContentLoaded', function() {
  const nav = document.querySelector('.nav');
  let lastScrollY = window.scrollY;
  let ticking = false;

  function updateHeader() {
    const scrollY = window.scrollY;
    
    // Adiciona classe 'scrolled' quando rola mais de 50px
    if (scrollY > 50) {
      nav.classList.add('scrolled');
    } else {
      nav.classList.remove('scrolled');
    }
    
    lastScrollY = scrollY;
    ticking = false;
  }

  function requestTick() {
    if (!ticking) {
      requestAnimationFrame(updateHeader);
      ticking = true;
    }
  }

  // Listener para scroll com throttling para performance
  window.addEventListener('scroll', requestTick);
  
  // Checa a posição inicial
  updateHeader();
});