function vm() {
    let container = document.getElementById('encuesta-container');
    let aside = document.getElementById('aside');
    if (!aside.classList.contains('active')) {
        container.classList.add('class', 'encuesta-container');
        container.classList.remove('class', 'encuesta-container-movil');
        console.log(':)');

    } else {
        container.classList.add('class', 'encuesta-container-movil');
        container.classList.remove('class', 'encuesta-container');
        console.log(":(");
    }
}

let aside2 = document.getElementById('nav__icon-menu');
aside2.addEventListener('click', vm);

document.addEventListener('load', vm);