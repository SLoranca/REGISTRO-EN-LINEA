btnRegistro.onclick = function (event) { registro_rapido(); }

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

function registro_rapido() {
    if (!$("#frmRegistroRap").valid()) { return false }

    if (document.getElementById("cboGeneroRap").value === "SEL") {
        tools.mensajeError("Seleccione su Género");
        return false;
    }

    if (document.getElementById("cboIdentifiqueRap").value === "SEL") {
        tools.mensajeError("Seleccione la opción a convenir.");
        return false;
    }

    ajax.async = false;
    ajax.serialize = false;
    ajax.parametros = {
        id: 0,
        nombre_completo: document.getElementById("txtNombreFormRap").value,
        correo: document.getElementById("txtCorreoFormRap").value,
        telefono: document.getElementById("txtTelefonoFormRap").value,
        genero: document.getElementById("cboGeneroRap").value,
        tipo: document.getElementById("cboIdentifiqueRap").value,
        no_evento: _app.no_evento,
        nombre_evento: _app.nombre_evento
    };
    ajax.send(urlRegistroRapido, function (response) {
        if (response.status === "OK") {
            limpiarForm(".inputClear");
            tools.mensajeOK(response.message);
        }
        else { tools.mensajeError(response.message); }
    });
}

$(function () {
    $("#frmRegistroRap").validate({
        rules: {
            nombre_completo_form_rap: { required: true },
            correo_form_rap: { required: true },
            telefono_form_rap: { required: true },
            CaptchaInputText: { required: true }
        },
        messages: {
            nombre_completo_form_rap: { required: "*Este campo es obligatorio" },
            correo_form_rap: { required: "*Este campo es obligatorio" },
            telefono_form_rap: { required: "*Este campo es obligatorio", minlength: "*El mínimo de caracteres debe ser de 10", maxlength: "* El máximo de caracteres debe ser de 10" },
            CaptchaInputText: { required: "*Capture el captcha que se indica" }
        }
    });
});