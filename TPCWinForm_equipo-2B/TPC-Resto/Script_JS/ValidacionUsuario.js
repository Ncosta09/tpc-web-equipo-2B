function validarRegistro() {
    const txtNombre = document.getElementById("txtNombre");
    const txtApellido = document.getElementById("txtApellido");
    const txtDni = document.getElementById("txtDni");
    const txtEmail = document.getElementById("txtEmail");
    const txtContrasenia = document.getElementById("txtContrasenia");

    const campos = [txtNombre, txtApellido, txtDni, txtEmail, txtContrasenia];

    let isValid = true;

    campos.forEach(campo => {
       
        if (campo.value == "") {
            campo.classList.add("invalido");

            let errorMessage = document.getElementById(`error${campo.id}`);
            if (!errorMessage) {
                errorMessage = document.createElement("p");
                errorMessage.id = `error${campo.id}`;
                errorMessage.textContent = "El campo es requerido.";
                errorMessage.style.color = "red";
                errorMessage.style.fontSize = "12px";
                errorMessage.style.marginTop = "5px";

                campo.parentNode.appendChild(errorMessage);
            }

            isValid = false;
        } else {
            campo.classList.remove("invalido");

            let errorMessage = document.getElementById(`error${campo.id}`);
            if (errorMessage) {
                errorMessage.remove();
            }
        }
    });

    return isValid;
}