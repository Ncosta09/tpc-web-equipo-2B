﻿//MODAL PARA MESERO
function showModal() {
    var modal = document.getElementById("asignarMeseroModal");
    modal.style.display = "block";
}

function closeModal() {
    var modal = document.getElementById("asignarMeseroModal");
    modal.style.display = "none";
}
//MODAL PARA INSUMO
function showModalInsumo() {
    var modal = document.getElementById("AgregarInsumoModal");
    modal.style.display = "block";
}

function closeModalInsumo() {
    var modal = document.getElementById("AgregarInsumoModal");
    modal.style.display = "none";
}

//CLOSE MODAL
document.getElementsByClassName("close")[0].onclick = function () {
    closeModal();
}
//DISPLAY FLEX CARD
document.getElementById("btnAsignarMesero").addEventListener("click", function () {
    document.querySelector(".card").style.display = "flex";
})

//MODAL PARA Alerta
function showModalAlert() {
    var modal = document.getElementById("CierreMesa");
    modal.style.display = "block";
}
function closeModalAlert() {
    var modal = document.getElementById("CierreMesa");
    modal.style.display = "none";
}

//MODAL PARA Factura
function showModalFactura() {
    var modal = document.getElementById("enviarFactura");
    modal.style.display = "block";
}
function closeModalFactura() {
    var modal = document.getElementById("enviarFactura");
    modal.style.display = "none";
}