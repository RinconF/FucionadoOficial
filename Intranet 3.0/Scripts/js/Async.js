const foto = document.querySelector('#foto_back_user');
const foto_menu = document.querySelector('#foto_back_user_aside');

console.log('hola');

const file = async (ubicacion, imagen) => {
    fetch(`http://localhost:51482/api/values/archivos_usuarios/3664/Perfil/3664.png`, {
        method: 'GET',
    })
        .then(res => res.blob())
        .then(datos => {
            const url = URL.createObjectURL(datos);

            foto.style.backgroundImage = `url('${url}')`;
            foto_menu.style.backgroundImage = `url('${url}')`;
        })
        .catch(error => {
            console.log(error);
        })
};

file();