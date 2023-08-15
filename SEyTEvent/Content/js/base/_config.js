
let _app = {
    mode: "DEV", //[DEV, PROD]
    urlAuth: "",
    org: {
        nombre: "SECRETARÍA DE ECONOMÍA Y DEL TRABAJO",
        url_logo: "https://economiaytrabajo.chiapas.gob.mx/wp-content/uploads/escudogris.png"
    },
    select: {},
    no_evento: "1",
    nombre_evento: "Foro Innovación y Economía Digital 2023"
};

_app.mode === "DEV" ? _app.urlAuth = "https://localhost:7015/" : _app.urlAuth = "https://login.seyt.gob.mx/";