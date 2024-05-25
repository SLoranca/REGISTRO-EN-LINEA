let curp_valido = true;
let rfc_valido = true;
let array_conf = [];
let array_talleres = [];

var loadervar = jQuery('<div id="loader-wrapper"><div id="loader"></div></div>')
    .css({
        content: '',
        display: "block",
        position: "fixed",
        top: 0,
        left: 0,
        width: "100%",
        height: "100%",
        background: "radial - gradient(rgba(20, 20, 20, .8), rgba(0, 0, 0, .8))",
        background: "-webkit - radial - gradient(rgba(20, 20, 20, .8), rgba(0, 0, 0, .8))",
        background: "#f1eacc",
        opacity: "0.85"
    })
    .appendTo("body")
    .hide();

let tools = {
    loading: {
        start: function () {
            let div = document.createElement('div');
            div.id = "preloader";
            div.innerHTML = '<div id="loader" class="spinner-border" role="status">\
                                <span class="sr-only hide">Loading...</span>\
                            </div>\
                            <span id="loaderText">cargando...</span>';
            document.body.appendChild(div);
        },
        stop: function () {
            let divLoading = document.getElementById("preloader");
            document.body.removeChild(divLoading);
        }
    },
    mensajeOK: function (mensaje) {
        swal.fire({
            title: "Aviso",
            text: mensaje,
            icon: "success",
            buttonsStyling: false,
            confirmButtonText: "Cerrar",
            showConfirmButton: true,
            customClass: {
                confirmButton: "btn btn-primary"
            },
            closeOnConfirm: true
        });
    },
    mensajeInfo: function (mensaje) {
        swal.fire({
            title: "Aviso",
            text: mensaje,
            icon: "info",
            buttonsStyling: false,
            confirmButtonText: "Cerrar",
            showConfirmButton: true,
            customClass: {
                confirmButton: "btn btn-primary"
            },
            closeOnConfirm: true
        });
    },
    mensajeError: function (mensaje) {
        swal.fire({
            title: "Error",
            text: mensaje,
            icon: "error",
            buttonsStyling: false,
            confirmButtonText: "Cerrar",
            showConfirmButton: true,
            customClass: {
                confirmButton: "btn btn-danger"
            },
            closeOnConfirm: true
        });
    },
    alerta: {
        show: function (content, titulo, mensaje) {
            document.querySelector(content).innerHTML = '<div class="alert bg-alert p-2 text-sm text-white" role="alert">\
                                                        <h6 class=" text-white text-sm mb-0">' + titulo + '</h6>\
                                                        <span class="alert-text">' + mensaje + '</span>\
                                                    </div>';
        },
        hide: function (content) {
            document.querySelector(content).innerHTML = "";
        }
    },
    validEmail: function (text) {
        var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        if (text.match(mailformat)) {
            return true;
        }
        else {
            return false;
        }
    },
    validFecha: function (date) {
        return date instanceof Date && !isNaN(date);
    },
    onlyNumbers: function isNumberKey(e) {
        if (e.which !== 8 && e.which !== 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    },
    codificacion: function (texto) {
        texto = texto.replaceAll("Ã", "í");
        texto = texto.replaceAll("í³", "ó");
        texto = texto.replaceAll("Ãº", "ú");
        texto = texto.replaceAll("Ã³", "ó");
        texto = texto.replaceAll("íº", "ú");
        texto = texto.replaceAll("í", "Ó");
        texto = texto.replaceAll("í", "Á");

        return texto;
    },
    exportar: {
        csv: function (table, nameFile) {
            // Variable to store the final csv data
            var csv_data = [];
            // Get each row data
            var rows = document.getElementById(table).getElementsByTagName('tr');
            for (var i = 0; i < rows.length; i++) {
                // Get each column data
                var cols = rows[i].querySelectorAll('td,th');
                // Stores each csv row data
                var csvrow = [];
                for (var j = 0; j < cols.length; j++) {
                    // Get the text data of each cell
                    // of a row and push it to csvrow
                    csvrow.push(cols[j].innerHTML);
                }
                // Combine each column value with comma
                csv_data.push(csvrow.join(","));
            }
            // Combine each row data with new line character
            csv_data = csv_data.join('\n');
            // Call this function to download csv file 
            this.downloadCSV(csv_data, nameFile);
        },
        pdf: function () {
        },
        downloadCSV: function (data, nameFile) {
            // Create CSV file object and feed
            // our csv_data into it
            CSVFile = new Blob(["\ufeff", data], {
                type: "text/csv"
            });

            // Create to temporary link to initiate
            // download process
            var temp_link = document.createElement('a');

            // Download csv file
            temp_link.download = nameFile + ".csv";
            var url = window.URL.createObjectURL(CSVFile);
            //let url = 'data:text/csv;charset=utf-8,%EF%BB%BF' + encodeURIComponent(CSVFile);
            temp_link.href = url;

            // This link should not be displayed
            //temp_link.charset = "utf-8";
            temp_link.style.display = "none";
            document.body.appendChild(temp_link);

            // Automatically click the link to
            // trigger download
            temp_link.click();
            document.body.removeChild(temp_link);
        }
    },
    getUserData: function () {
        ajax.async = false;        
        ajax.send("/Auth/GetUserData", function (response) {
            if (response.status === "OK") {
                document.getElementById("lblUsuario").innerText = response.nombre;
                document.getElementById("lblUsuarioRol").innerText = response.rol;
                document.getElementById("lnkUsuarios").classList.add("hide");
                if (response.id_rol === 1) { document.getElementById("lnkUsuarios").classList.remove("hide"); }
            }
            else { window.location = "/Auth/Login"; }
        });
    },
    logOut: function () {
        localStorage.setItem("data-renovacion", "");
        window.location = "/Auth/Login";
    }
};

let dataTable = {
    table: "",
    buscador: "",
    targets: [],
    destroy: function () {
        if ($.fn.DataTable.isDataTable(this.table)) { $(this.table).DataTable().destroy(); }
    },
    load: function (datos, pageLength = 10) {
        this.destroy();
        let table = $(this.table).DataTable({
            dom: 'lrtip',
            "bLengthChange": false,
            "autoWidth": false,
            data: datos,
            responsive: true,
            lengthMenu: [5, 10, 25, 50, 100],
            pageLength: pageLength,
            order: [[0, "desc"]],
            ordering: false,
            language: {
                "emptyTable": "No hay datos disponibles en la tabla",
                "info": "Mostrando _START_ de _END_ de _TOTAL_ registros",
                "infoEmpty": "Mostrando 0 de 0 de 0 registros",
                "infoFiltered": "(filtrado de _MAX_ registros en total)",
                "infoPostFix": "",
                "thousands": ",",
                "lengthMenu": "Ver _MENU_ Registros",
                "loadingRecords": "Cargando...",
                "processing": "Procesando...",
                "search": "Buscar:",
                "zeroRecords": "No se encontraron registros coincidentes",
                "paginate": {
                    "previous": "",
                    "next": "",
                }
            },
            columnDefs: this.targets
        });
        $('[data-toggle="tooltip"]').tooltip();
        $(this.buscador).on('keyup', function () {
            table.search(this.value).draw();
        });
       
    }
};

let ajax = {
    tipo: "POST",
    async: false,
    serialize: true,
    token: false,
    parametros: {},
    send: function (url, response) {
        let tokenData = this.token ? 'Bearer ' + localStorage.getItem(config.dataToken) : "";
        $.ajax({
            async: this.async,
            type: this.tipo,
            url: url,
            contentType: this.serialize ? 'application/x-www-form-urlencoded; charset=UTF-8' : "application/json; charset=utf-8",
            dataType: "json",
            headers: { 'Authorization': tokenData },
            data: this.serialize ? this.parametros : JSON.stringify(this.parametros),
            beforeSend: function () {
                tools.loading.start();
            },
            success: function (data) {
                if (data.status === "NO_SESSION") { tools.logOut(); }
                else if (data.status === "ERROR") {
                    tools.mensajeError(data.message);
                }
                else {
                    response(data);
                    tools.loading.stop();
                }
            },
            error: function (_error) {
                tools.mensajeError(_error.responseText);
                return null;
            },
            complete: function () {
                tools.loading.stop();
            }
        });
    }
};

//money.format(string);
let money = Intl.NumberFormat("en-MX", {
    style: "currency",
    currency: "USD",
    useGrouping: true,
});

function no_money(value) {
    let format = value.replace(/[$,]/g, "");
    return format;
}

function _toUppercase(e) {
    e.value = e.value.toUpperCase();
}

function limpiarForm(value) {
    let input = document.querySelectorAll(value);
    input.forEach((_input) => {
        _input.value = "";
    });
}

function initValueForm(value) {
    let input = document.querySelectorAll(value);
    input.forEach((_input) => {
        _input.value = "0";
    });
}

function removeActive(nav, tab) {
    navLinkPanel(nav);
    tabPanel(tab);
}

function navLinkPanel(nav) {
    let navLink = document.querySelectorAll(nav);
    navLink.forEach((_navLink) => {
        _navLink.classList.remove("active");
    });
}

function tabPanel(tab) {
    let tabPane = document.querySelectorAll(tab);
    tabPane.forEach((_tabPane) => {
        _tabPane.classList.remove("active");
    });
}

function objectShow(obj, mode) {
    let element = document.querySelectorAll(obj);
    element.forEach((_elem) => {
        if (mode === "hide") {
            _elem.classList.add("hide");
        }
        else {
            _elem.classList.remove("hide");
        }
    });
}

function HideRemoveObj(arrayObj = []) {
    arrayObj.forEach((_elem) => {
        let result = _elem.split(":");
        result[1] === "add" ? document.getElementById(result[0]).classList.add("hide") : document.getElementById(result[0]).classList.remove("hide"); 
    });
}

function resetCombosByClassName(value) {
    let elements = document.getElementsByClassName(value);
    for (var i = 0, l = elements.length; i < l; i++) {
        elements[i].selectedIndex = 0;
    }
}

function resetForm(form) {
    let frm = $(form).validate();
    frm.resetForm();
}

function checkboxUncheck(value) {
    let checkbox = document.querySelectorAll(value);
    checkbox.forEach((_checkbox) => {
        _checkbox.checked = false;
    });
}

//valida numero y letras
$(".letter_and_numbers").keypress(function (key) {
    if (key.key.match(/[a-z0-9ñçáéíóú\s]/i) === null) {
        key.preventDefault();
    }
});

//valida numero y letras
$(".letter_and_numbers_and_guion").keypress(function (key) {
    if (key.key.match(/[a-z0-9ñçáéíóú-\s]/i) === null) {
        key.preventDefault();
    }
});


//valida letras
$(".letter").keypress(function (key) {
    if ((key.charCode < 97 || key.charCode > 122)//letras mayusculas
        && (key.charCode < 65 || key.charCode > 90) //letras minusculas
        && (key.charCode != 45) //retroceso
        && (key.charCode != 44) // ,
        && (key.charCode != 46) // .
        && (key.charCode != 241) //ñ
        && (key.charCode != 209) //Ñ
        && (key.charCode != 32) //espacio
        && (key.charCode != 225) //á
        && (key.charCode != 233) //é
        && (key.charCode != 237) //í
        && (key.charCode != 243) //ó
        && (key.charCode != 250) //ú
        && (key.charCode != 193) //Á
        && (key.charCode != 201) //É
        && (key.charCode != 205) //Í
        && (key.charCode != 211) //Ó
        && (key.charCode != 218) //Ú

    )
        return false;
});

//valida numeros
$(".numbers").keypress(function (e) {
    var key = window.Event ? e.which : e.keyCode
    return (key >= 48 && key <= 57)
});

//valida numeros y caracteres
$(".number_caracter").keypress(function (key) {
    if (key.key.match(/[0-9:]/i) === null) {
        key.preventDefault();
    }
});

//valida numeros con decimal
function number_decimal(evt, input) {
    // Backspace = 8, Enter = 13, ‘0′ = 48, ‘9′ = 57, ‘.’ = 46, ‘-’ = 43
    let key = window.Event ? evt.which : evt.keyCode;
    let chark = String.fromCharCode(key);
    let tempValue = input.value + chark;
    if (key >= 48 && key <= 57) {
        if (filter(tempValue) === false) {
            return false;
        } else {
            return true;
        }
    } else {
        if (key == 8 || key == 13 || key == 0) {
            return true;
        } else if (key == 46) {
            if (filter(tempValue) === false) {
                return false;
            } else {
                return true;
            }
        } else {
            return false;
        }
    }
}

function filter(__val__) {
    let preg = /^([0-9]+\.?[0-9]{0,2})$/;
    if (preg.test(__val__) === true) {
        return true;
    } else {
        return false;
    }
}

function curpValida(curp) {
    var re = /^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/,
        validado = curp.match(re);

    if (!validado)  //Coincide con el formato general?
        return false;

    //Validar que coincida el dígito verificador
    function digitoVerificador(curp17) {
        //Fuente https://consultas.curp.gob.mx/CurpSP/
        var diccionario = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ",
            lngSuma = 0.0,
            lngDigito = 0.0;
        for (var i = 0; i < 17; i++)
            lngSuma = lngSuma + diccionario.indexOf(curp17.charAt(i)) * (18 - i);
        lngDigito = 10 - lngSuma % 10;
        if (lngDigito == 10) return 0;
        return lngDigito;
    }

    if (validado[2] != digitoVerificador(validado[1]))
        return false;

    return true; //Validado
}

function validarInputCURP(input) {
    var curp = input.value.toUpperCase(),
        resultado = document.getElementById("resultado-curp"),
        valido = "No válido";

    if (curpValida(curp)) {
        valido = "Válido";
        curp_valido = true;    
        resultado.classList.add("ok");
    } else {
        curp_valido = false;
        resultado.classList.remove("ok");
    }

    resultado.innerText = "CURP: " + curp + "\nFormato: " + valido 
}


function rfcValido(rfc, aceptarGenerico = true) {
    const re = /^([A-ZÑ&]{3,4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$/;
    var validado = rfc.match(re);

    if (!validado)  //Coincide con el formato general del regex?
        return false;

    //Separar el dígito verificador del resto del RFC
    const digitoVerificador = validado.pop(),
        rfcSinDigito = validado.slice(1).join(''),
        len = rfcSinDigito.length,

        //Obtener el digito esperado
        diccionario = "0123456789ABCDEFGHIJKLMN&OPQRSTUVWXYZ Ñ",
        indice = len + 1;
    var suma,
        digitoEsperado;

    if (len == 12) suma = 0
    else suma = 481; //Ajuste para persona moral

    for (var i = 0; i < len; i++)
        suma += diccionario.indexOf(rfcSinDigito.charAt(i)) * (indice - i);
    digitoEsperado = 11 - suma % 11;
    if (digitoEsperado == 11) digitoEsperado = 0;
    else if (digitoEsperado == 10) digitoEsperado = "A";

    //El dígito verificador coincide con el esperado?
    // o es un RFC Genérico (ventas a público general)?
    if ((digitoVerificador != digitoEsperado)
        && (!aceptarGenerico || rfcSinDigito + digitoVerificador != "XAXX010101000"))
        return false;
    else if (!aceptarGenerico && rfcSinDigito + digitoVerificador == "XEXX010101000")
        return false;
    return rfcSinDigito + digitoVerificador;
}

function validarInputRFC(input) {
    var rfc = input.value.trim().toUpperCase(),
        resultado = document.getElementById("resultado-rfc"),
        valido;

    var rfcCorrecto = rfcValido(rfc);

    if (rfcCorrecto) {
        valido = "Válido";
        rfc_valido = true;
        resultado.classList.add("ok");
    } else {
        valido = "No válido";
        rfc_valido = false;
        resultado.classList.remove("ok");
    }

    resultado.innerText = "RFC: " + rfc + "\nFormato: " + valido;
}