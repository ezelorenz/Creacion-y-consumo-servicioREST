document.addEventListener("DOMContentLoaded", init);

const URL_API = 'https://localhost:7143/api/'

var customers = []

function init() {
    search()
}

function agregar() {
    limpiar()
    abrirFormulario()
}

function abrirFormulario() {
    htmlModal = document.getElementById("modal");
    htmlModal.setAttribute("class", "modale opened");
}

function cerrarModal() {
    htmlModal = document.getElementById("modal");
    htmlModal.setAttribute("class", "modale");
}


async function search() {
    
    var url = URL_API + 'customer'
    var resultado = await fetch(url, {
        "method": 'GET',
        "headers": {
            "Content-Type": 'application/json'
        }
    })
    customers = await resultado.json();   
    var html = ''

    for (customer of customers) {
        var row = `<tr>
            <td>${customer.nombre}</td>
            <td>${customer.apellido}</td>
            <td>${customer.email}</td>
            <td>${customer.telefono}</td>
            <td>${customer.direccion}</td>
            <td>
                <a href="#" onclick="edit(${customer.id})" class="Editar">Editar</a>
                <a href="#" onclick="remove(${customer.id})" class="Eliminar">Eliminar</a>
            </td>
        </tr>`
        html = html + row;
    }
    document.querySelector('#customers > tbody').outerHTML = html
}


function edit(id) {
    abrirFormulario()
    customers.find(x => x.id == id)
    document.getElementById('txtId').value = customer.id
    document.getElementById('txtDireccion').value = customer.direccion
    document.getElementById('txtEmail').value = customer.email
    document.getElementById('txtNombre').value = customer.nombre
    document.getElementById('txtApellido').value = customer.apellido
    document.getElementById('txtTelefono').value = customer.telefono
}


async function guardar(id) {

    var data = {
        "direccion": document.getElementById('txtDireccion').value,
        "email": document.getElementById('txtEmail').value,
        "nombre": document.getElementById('txtNombre').value,
        "apellido": document.getElementById('txtApellido').value,
        "telefono": document.getElementById('txtTelefono').value
        }

    var id = document.getElementById('txtId').value
    if (id != '') {
        data.id = id
    }
        var url = URL_API + 'customer'
        await fetch(url, {
            "method": id != '' ? 'PUT' : 'POST',
            "body": JSON.stringify(data),
            "headers": {
                "Content-Type": 'application/json'
            }
        })
    window.location.reload();
}

async function remove(id) {
    respuesta = confirm('¿Está seguro de eliminarlo?');
    if (respuesta) {
        var url = URL_API + 'customer/' + id
        await fetch(url, {
            "method": 'DELETE',
            "headers": {
                "Content-Type": 'application/json'
            }
        })
        window.location.reload();
    }
}

function limpiar() {
    document.getElementById('txtId').value = ''
    document.getElementById('txtNombre').value = ''
    document.getElementById('txtApellido').value = ''
    document.getElementById('txtTelefono').value = ''
    document.getElementById('txtDireccion').value = ''
    document.getElementById('txtEmail').value = ''
}