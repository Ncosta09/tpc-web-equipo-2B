function showModal() {
    var modal = document.getElementById("asignarMeseroModal");
    modal.style.display = "block";
}

function closeModal() {
    var modal = document.getElementById("asignarMeseroModal");
    modal.style.display = "none";
}

document.getElementsByClassName("close")[0].onclick = function () {
    closeModal();
}