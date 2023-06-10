let dipendenti=60;

function carica() {
    for(let i=0; i<dipendenti; i++) {
        let newLine = document.createElement("li");
        newLine.innerHTML="<div class='container-fluid mt-4'><div class='p-3 border bg-light'><div class='row'><div class='col-sm-1 ml-3 align-self-center'><img class='item icon-img' src='img/user_icon.png' alt='user_icon'></div><div class='col-sm-7 align-self-center'>Persona "+i+"</div><div class='col-sm-1.5 align-self-center'><input type='radio' name='rumorosita"+i+"' id='rumoroso"+i+"' value='rumoroso'><label for='rumoroso"+i+"'>Rumoroso</label></div><div class='col-sm-1 align-self-center'><input type='radio' name='rumorosita"+i+"' id='neutro"+i+"' value='neutro' checked><label for='neutro"+i+"'>Neutro</label></div><div class='col align-self-center'><input type='radio' name='rumorosita"+i+"' id='non_rumoroso"+i+"' value='non_rumoroso'><label for='non_rumoroso"+i+"'>Non rumoroso</label></div></div></div></div>"
        document.getElementById("votazioni").appendChild(newLine);
    }
}