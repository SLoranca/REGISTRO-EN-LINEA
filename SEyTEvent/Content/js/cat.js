function get_municipios() {
    ajax.async = true;
    ajax.tipo = "POST";
    ajax.send(urlCatMunicipio, function (response) {
        if (response.status === "OK") {
            let data = JSON.parse(response.data);
            let sel = document.getElementById("cboMunicipioFormComp");
            sel.innerHTML = "";

            let opt = document.createElement("option");
            opt.value = 0;
            opt.text = "[SELECCIONAR]";
            sel.add(opt, null);

            data.map(function (e) {
                let opt = document.createElement("option");
                opt.value = e.id;
                opt.text = e.municipio;
                sel.add(opt, null);
            });
        }
    });
}

function get_conferencias() {
    ajax.async = true;
    ajax.tipo = "POST";
    ajax.send(urlCatConferencias, function (response) {
        if (response.status === "OK") {
            let data = JSON.parse(response.data);
            let html = "", acciones = "";

            if (data.length > 0) {
                data.map(function (item, index) {
                    acciones = '<input type="checkbox" id="chkConferencia-' + item.id + '" value="" onclick="checkConferencia(\'' + btoa(JSON.stringify(item)) + '\');">';

                    html += "<tr>";
                    html += "   <td style='width:90%;' class='text-left'>" + item.tema + "</td>";
                    html += "   <td style='width:10%;' class='text-center'>" + acciones + "</td>";
                    html += "</tr>";
                });
            }

            document.getElementById("tblConferencias").innerHTML = html;
        }
    });
}

function get_conferencias_participante() {
    ajax.async = true;
    ajax.tipo = "POST";
    ajax.parametros = {
        participante_id: document.getElementById("txtParticipanteId").value
    };
    ajax.send(urlGetRegistroParticipanteConferencia, function (response) {
        if (response.status === "OK") {
            let data = JSON.parse(response.data);
            let html = "";
            array_conf = [];

            if (data.length > 0) {
                data.map(function (item, index) {
                    html += "<li>" + item.tema + "</li>";
                    array_conf.push({
                        programa: item.tema
                    });
                });
            }
            else {
                html = "<li>Por el momento no tiene ninguna conferencia registrada.</li>";
            }

            document.getElementById("listConferencias").innerHTML = html;
            draw_tbl_conferencias();
        }
    });
}

function get_talleres() {
    ajax.async = true;
    ajax.tipo = "POST";
    ajax.send(urlCatTalleres, function (response) {
        if (response.status === "OK") {
            let data = JSON.parse(response.data);
            let html = "", acciones = "";

            if (data.length > 0) {
                data.map(function (item, index) {
                    acciones = '<input type="checkbox" id="chkTalleres-' + item.id + '" value="" onclick="checkTaller(\'' + btoa(JSON.stringify(item)) + '\');">';

                    html += "<tr>";
                    html += "   <td style='width:90%;' class='text-left'>" + item.tema + "</td>";
                    html += "   <td style='width:10%;' class='text-center'>" + acciones + "</td>";
                    html += "</tr>";
                });
            }

            document.getElementById("tblTalleres").innerHTML = html;
        }
    });
}

function get_talleres_participante() {
    ajax.async = true;
    ajax.tipo = "POST";
    ajax.parametros = {
        participante_id: document.getElementById("txtParticipanteId").value
    };
    ajax.send(urlGetRegistroParticipanteTaller, function (response) {
        if (response.status === "OK") {
            let data = JSON.parse(response.data);
            let html = "";
            array_talleres = [];

            if (data.length > 0) {
                data.map(function (item, index) {
                    html += "<li>" + item.tema + "</li>";
                    array_talleres.push({
                        programa: item.tema
                    });
                });
            }
            else {
                html = "<li>Por el momento no tiene ningun taller registrado.</li>";
            }

            document.getElementById("listTalleres").innerHTML = html;
            draw_tbl_talleres();
        }
    });
}
