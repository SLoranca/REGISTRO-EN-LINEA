btnRegistrar.onclick = function (event) { registrar(); }

addEventListener('resize', () => {
    if (innerWidth < 900) {
        let col = document.getElementsByClassName("resize");
        for (var i = 0; i < col.length; i++) {
            col[i].classList.add("col-12");
        }
    }
    else {
        let col = document.getElementsByClassName("resize");
        for (var i = 0; i < col.length; i++) {
            col[i].classList.remove("col-12");
        }
    }
});

function initForms() {
    array_conf = [];
    array_talleres = [];
    _app.select.folio = "";
    _app.select.mode = "INSERT";
    get_municipios();
}

function get_programas(folio) {
    ajax.async = false;
    ajax.serialize = false; 
    ajax.parametros = {
        folio: folio
    };
    ajax.send(urlObtInfoParticipante, function (response) {
        if (response.status === "OK") {
            let data = JSON.parse(response.data);
            if (data.status === "OK") {
                document.getElementById("lblNombreCompleto").innerText = data.nombre_completo;
                document.getElementById("lblTelefono").innerText = data.telefono;
                document.getElementById("lblTipo").innerText = data.tipo;
                document.getElementById("txtParticipanteId").value = data.id;
                document.getElementById("txtBuscador").value = "";
                get_conferencias();
                get_talleres();
                get_conferencias_participante();
                get_talleres_participante();
            }
            else { tools.mensajeError("No se encontro ninguna información con el folio propocionado."); }
        }
        else { tools.mensajeError(response.message); }
    });
}

function checkConferencia(data) {
    let info = JSON.parse(atob(data));
    ajax.async = false;
    ajax.serialize = false;
    ajax.parametros = {
        participante_id: document.getElementById("txtParticipanteId").value,
        conferencia_id: info.id
    };
    ajax.send(urlCheckConferencia, function (response) {
        if (response.status === "OK") {
            get_conferencias_participante();
        }
        else { tools.mensajeError(response.message); }
    });
}

function checkTaller(data) {
    let info = JSON.parse(atob(data));
    ajax.async = false;
    ajax.serialize = false;
    ajax.parametros = {
        participante_id: document.getElementById("txtParticipanteId").value,
        taller_id: info.id
    };
    ajax.send(urlCheckTaller, function (response) {
        if (response.status === "OK") {
            get_talleres_participante();
        }
        else { tools.mensajeError(response.message); }
    });
}

function init_registro() {
    if (!$("#frmRegistroCompleto").valid()) { return false; }

    //if (!curp_valido) {
    //    tools.mensajeError("La CURP es invalida, revisela cuidadosamente");
    //    return false;
    //}

    //if (!rfc_valido) {
    //    tools.mensajeError("El RFC es invalido, reviselo cuidadosamente");
    //    return false;
    //}

    if (document.getElementById("cboMunicipioFormComp").value === "0") {
        tools.mensajeError("Seleccione el municipio al que pertenece");
        return false;
    }

    if (document.getElementById("cboGeneroFormComp").value === "SEL") {
        tools.mensajeError("Seleccione su Género");
        return false;
    }

    if (document.getElementById("cboIdentifiqueFormComp").value === "SEL") {
        tools.mensajeError("Seleccione la opción a convenir");
        return false;
    }

    if (document.getElementById("cboDiscapacidadFormComp").value === "SEL") {
        tools.mensajeError("Seleccione si cuenta con alguna discapacidad");
        return false;
    }

    ajax.async = false;
    ajax.serialize = false;
    ajax.parametros = {
        id: 0,
        muncipio_id: document.getElementById("cboMunicipioFormComp").value,
        nombre_completo: document.getElementById("txtNombreFormComp").value,
        edad: document.getElementById("txtEdadFormComp").value,
        curp: document.getElementById("txtCurpFormComp").value,
        correo: document.getElementById("txtCorreoFormComp").value,
        telefono: document.getElementById("txtTelefonoFormComp").value,
        genero: document.getElementById("cboGeneroFormComp").value,
        tipo: document.getElementById("cboIdentifiqueFormComp").value,
        discapacidad: document.getElementById("cboDiscapacidadFormComp").value,
        nombre_empresa: document.getElementById("txtNombreComercial").value,
        actividad: document.getElementById("txtActividad").value,
        rfc: document.getElementById("txtRFC").value,
        comentarios: document.getElementById("txtComentario").value,
        folio: _app.select.folio,
        no_evento: _app.no_evento,
        nombre_evento: _app.nombre_evento,
        mode: _app.select.mode
    };
    ajax.send(urlRegistroCompleto, function (response) {
        if (response.status === "OK") {
            _app.select.folio = response.data;
            _app.select.mode = "UPDATE";
            get_programas(_app.select.folio);
            document.getElementById("btnRegresar").classList.remove("hide");
            document.getElementById("btnRegistrar").classList.remove("hide");
            document.getElementById("btnContinuar").classList.add("hide");
            document.getElementById("content-registro").classList.add("hide");
            document.getElementById("content-programas").classList.remove("hide");
        }
        else { tools.mensajeError(response.message); }
    });
}

function registrar() {
    if (!$("#frmRegistroCompleto").valid()) { return false; }

    //if (curp_valido.length > 0) {
    //    if (!curp_valido) {
    //        tools.mensajeError("La CURP es invalida, revisela cuidadosamente");
    //        return false;
    //    }
    //} else {
    //    document.getElementById("txtCurpFormComp").value = 'N/D';
    //}
    

    //if (!rfc_valido) {
    //    tools.mensajeError("El RFC es invalido, reviselo cuidadosamente");
    //    return false;
    //}

    if (document.getElementById("cboMunicipioFormComp").value === "0") {
        tools.mensajeError("Seleccione el municipio al que pertenece");
        return false;
    }

    if (document.getElementById("cboGeneroFormComp").value === "SEL") {
        tools.mensajeError("Seleccione su Género");
        return false;
    }

    if (document.getElementById("cboIdentifiqueFormComp").value === "SEL") {
        tools.mensajeError("Seleccione la opción a convenir");
        return false;
    }

    if (document.getElementById("cboDiscapacidadFormComp").value === "SEL") {
        tools.mensajeError("Seleccione si cuenta con alguna discapacidad");
        return false;
    }

    ajax.async = false;
    ajax.serialize = false;
    ajax.parametros = {
        id: 0,
        muncipio_id: document.getElementById("cboMunicipioFormComp").value,
        nombre_completo: document.getElementById("txtNombreFormComp").value,
        edad: document.getElementById("txtEdadFormComp").value,
        curp: document.getElementById("txtCurpFormComp_").value,
        correo: document.getElementById("txtCorreoFormComp").value,
        telefono: document.getElementById("txtTelefonoFormComp").value,
        genero: document.getElementById("cboGeneroFormComp").value,
        tipo: document.getElementById("cboIdentifiqueFormComp").value,
        discapacidad: document.getElementById("cboDiscapacidadFormComp").value,
        nombre_empresa: document.getElementById("txtNombreComercial").value,
        actividad: document.getElementById("txtActividad").value,
        rfc: document.getElementById("txtRFC").value,
        comentarios: document.getElementById("txtComentario").value,
        folio: _app.select.folio,
        no_evento: _app.no_evento,
        nombre_evento: _app.nombre_evento,
        mode: _app.select.mode
    };
    ajax.send(urlRegistroCompleto, function (response) {
        if (response.status === "OK") {
            _app.select.folio = response.data;
            _app.select.mode = "UPDATE";
            get_programas(_app.select.folio);
            concluir_registro();
        }
        else { tools.mensajeError(response.message); }
    });
}

function concluir_registro() {
    swal.fire({
        title: '¿Toda su información esta correcta?, Se enviará un mensaje a su correo al finalizar su registro.',
        showDenyButton: true,
        confirmButtonText: 'SI',
        denyButtonText: 'NO'
    }).then((result) => {
        if (result.isConfirmed) {

            let htmlTblTalleres = $("#tbl1").html();

            ajax.async = false;
            ajax.serialize = false;
            ajax.parametros = {
                folio: _app.select.folio,
                emailDestino: document.getElementById("txtCorreoFormComp").value,
                tblTalleres: htmlTblTalleres
            };
            ajax.send(urlEnviarCorreo, function (response) {
                if (response.status === "OK") {
                    array_conf = [];
                    array_talleres = [];
                    _app.select.folio = "";
                    _app.select.mode = "INSERT";

                    limpiarForm(".inputClear");
                    resetCombosByClassName("cboRegCompleto");
                    document.getElementById("resultado-curp").innerText = "";
                    document.getElementById("resultado-rfc").innerText = "";
                    document.getElementById("resultado-curp").classList.remove("ok");
                    document.getElementById("resultado-rfc").classList.remove("ok");

                    document.getElementById("btnRegresar").classList.add("hide");
                    document.getElementById("btnRegistrar").classList.add("hide");
                    document.getElementById("btnContinuar").classList.remove("hide");
                    document.getElementById("content-registro").classList.remove("hide");
                    document.getElementById("content-programas").classList.add("hide");
                    tools.mensajeOK(response.message);
                }
                else { tools.mensajeError(response.message); }
            });
        }
    });
}

$(function () {
    $("#frmRegistroCompleto").validate({
        rules: {
            nombre_completo_form_comp: { required: true },
            edad_form_comp: { required: true },
            correo_form_comp: { required: true },
            telefono_form_comp: { required: true }
        },
        messages: {
            nombre_completo_form_comp: { required: "*Este campo es obligatorio" },
            edad_form_comp: { required: "*Este campo es obligatorio" },
            correo_form_comp: { required: "*Este campo es obligatorio" },
            telefono_form_comp: { required: "*Este campo es obligatorio", minlength: "*El mínimo de caracteres debe ser de 10", maxlength: "* El máximo de caracteres debe ser de 10" }
        }
    });
});