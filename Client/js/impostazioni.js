let presenti = 10;
var festivita = [];
var nFestivita = 0;

function loadPresenti() {
    document.getElementById('lista-presenti').innerHTML = "";

    for(let i=0; i<presenti; i++) {
        let newLine = document.createElement('li');
        newLine.setAttribute("style", "width:40vw");
        newLine.innerHTML+='<img src="img/example/example-1.png" alt="example">Persona '+i;
        document.getElementById('lista-presenti').appendChild(newLine);
    }
}

function impostaFesta(date, dateId) {
    let festaId = 'f'.concat(dateId);
    let day = date.getDate();
    let month = date.getMonth();
    let year = date.getFullYear();
    festivita.push({
        date: date,
        id: festaId
    });
    nFestivita++;
    let festa = document.createElement('li');
    festa.setAttribute("id", festaId);
    festa.setAttribute("style", "list-style-type:none; margin: 0px 0px 10px 0px");
    if(month === 0) festa.innerText = day + "/12/" + (year-1);
    else festa.innerText = day + "/" + month + "/" + year;
    document.getElementById("lista-feste").appendChild(festa);
}

function rimuoviFesta(date, dateId) {
    let festaId = 'f'.concat(dateId);
    for(let i=0; i<nFestivita; i++) {
        if(festivita[i].id === festaId) {
            festivita.splice(i, 1);
            nFestivita--;
            document.getElementById(festaId).remove();
            $('#'.concat(dateId)).css("color", "black");
            $('#'.concat(dateId).concat('1')).css("color", "black");
            return;
        }
    }
}