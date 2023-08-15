function draw_tbl_talleres() {
    let fila = "";
    if (array_talleres.length > 0) {
        array_talleres.map(function (item, index) {
            fila += '<tr>' + item.programa + '</tr>';
        });
    }

    document.getElementById("tblTalleresSendEmail").innerHTML = fila;
}

function draw_tbl_conferencias() {
    let fila = "";
    if (array_conf.length > 0) {
        array_conf.map(function (item, index) {
            fila += '<tr>' + item.programa + '</tr>';
        });
    }

    document.getElementById("tblConferenciaSendEmail").innerHTML = fila;
}