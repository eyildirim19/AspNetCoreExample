function OnSuccessSepet(response) {

    var toplamAdet = 0;
    for (var i = 0; i < response.length; i++) {
        toplamAdet += response[i].Adet;
    }

    document.getElementById("SepetItemCount").innerHTML = toplamAdet;
}

function OnErrorSepet(response) {
    console.log(response);
}