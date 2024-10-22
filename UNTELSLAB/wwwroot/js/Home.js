const laboratorioDiv = document.getElementById('Laboratorio');
laboratorioDiv.addEventListener('click', () => {
    const url = laboratorioDiv.getAttribute('data-url'); // Obtiene la URL desde el atributo data-url
    window.location.href = url; // Redirige a la URL
});