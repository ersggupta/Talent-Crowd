function MoveInfo(lista, modo, rowid) {

    var listaModal = [""];
    var listaDetail = [""];


    for (var i = 0; i < lista.length; i++) {
        listaModal[i] = "modal_" + lista[i];
        listaDetail[i] = "detail_" + lista[i] + rowid;
    }

    if (modo == 'DM') {
        lista1 = listaDetail;
        lista2 = listaModal;
        document.getElementById("modal_rowid").value = rowid.toString();
    }
    else {
        lista1 = listaModal;
        lista2 = listaDetail;
    }

    for (var i = 0; i < lista1.length; i++) {
        //alert(lista1[i]);
//        alert(lista2[i]);
        document.getElementById(lista2[i].toString()).value = document.getElementById(lista1[i].toString()).value;
    }
}

function NewLine(listafield, listatype, rowid) {
    var s = '<tr id="' + rowid.toString() + '">';
    for (var i = 0; i < listafield.length; i++) {
        if (listatype[i] == "hidden") {
            s += '<input type="hidden" value="" id="detail_' + listafield[i] + rowid.toString() + '">';
        }
        else {
            s += '<td><input type="text" style="width:100%"  value="" class="disab" id="detail_' + listafield[i] + rowid.toString() + '"></td>';
        }
    }
    s += '<td class="text-right"><a href="#" onclick="editItem(' + rowid.toString() + ');">Edit</a>&nbsp;&nbsp;<a href="#" onclick="deleteItem(this);">Delete</a></td>';
    s += '</tr>';
    return s;

}

function ChangeName(lista, rowid, s) {

    for (var i = 0; i < lista.length; i++) {
        document.getElementById("detail_" + lista[i] + rowid.toString()).setAttribute("name", s + '.' + lista[i]);
    }

}

function TodayString() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();
    if (dd < 10) { dd = '0' + dd; }
    if (mm < 10) { mm = '0' + mm; }
    today = dd + '/' + mm + '/' + yyyy;
    return today;
}


