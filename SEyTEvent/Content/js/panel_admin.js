function load_panel() {
    ajax.async = true;
    ajax.tipo = "POST";
    ajax.send(urlPanelAdmin, function (response) {
        if (response.status === "OK") {
            let data = JSON.parse(response.data);

            document.getElementById("lblTotalParticipantes").innerHTML = data.total_participantes;
            document.getElementById("lblTotalMujeres").innerHTML = data.total_mujeres;
            document.getElementById("lblTotalHombres").innerHTML = data.total_hombres;

            document.getElementById("lblTotalEmprendedores").innerHTML = data.total_emprendedor;
            document.getElementById("lblTotalEmpresarios").innerHTML = data.total_empresario;
            document.getElementById("lblTotalPubGenerar").innerHTML = data.total_pub_general;
        }
    });
}