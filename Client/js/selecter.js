// noinspection JSJQueryEfficiency

let roomSelected = null;
let daySelected = new Date();
let startHoursSelected = 9;
let startMinutesSelected = 0;
let finishHoursSelected = 13;
let finishMinutesSelected = 0;

let dayIdSelected = null;

function selectRoom(room) {
    roomSelected = room;
    $('#room-sel').text(roomSelected);
}

function selectDay(date) {
    daySelected = date;
    if(dayIdSelected !== null)
        $('#day-sel-2').text("Giorno selezionato: " + date.getDate() + "/" + date.getMonth() + "/" +  date.getFullYear());
    loadPresenti();
}

function selectDayFest(date) {
    daySelected = date;
    $('#day-sel-1').text("Giorno selezionato: " + date.getDate() + "/" + date.getMonth() + "/" +  date.getFullYear());
}

//Link api google https://developers.google.com/people/api/rest/v1/people/get

function defaultSelecter() {
    $('#room-sel').text("Seleziona una Stanza");
    selectDay(daySelected);
}

function clickCalendar(id) {
    let selector = $('#'.concat(id));

    if (selector.css("color") !== "rgb(0, 0, 0)")
        return;

    if (dayIdSelected !== null) {
        $('#'.concat(dayIdSelected)).css("color","black");
        $('#'.concat(dayIdSelected)).css("background-color","transparent");
        $('#'.concat(dayIdSelected)).css("font-weight","normal");
    }

    dayIdSelected = id;

    selector.css("color","white");
    selector.css("background-color","darkorange");
    selector.css("font-weight","bold");

    selectDay(new Date(date.getFullYear(), date.getMonth(), selector.text()));
}

function clickCalendarFest(id) {
    
    let selector = $('#'.concat(id));
    
    if (selector.css("color") !== "rgb(0, 0, 0)" && selector.css("color") !== "rgb(255, 140, 0)")
        return;

    if (dayIdSelected !== null) {
        if(document.getElementById("festabutton").value === "Imposta come festività")
            $('#'.concat(dayIdSelected)).css("color","black");
        else if(dayIdSelected.length === 4)
            $('#'.concat(dayIdSelected)).css("color","black");
        else $('#'.concat(dayIdSelected)).css("color","darkorange");
        $('#'.concat(dayIdSelected)).css("background-color","transparent");
        $('#'.concat(dayIdSelected)).css("font-weight","normal");
    }

    dayIdSelected = id;

    if(selector.css("color") === "rgb(0, 0, 0)") {
        document.getElementById("festabutton").value = "Imposta come festività";
        document.getElementById("festabutton").innerText = "Imposta come festività";
    }
    else {
        document.getElementById("festabutton").value = "Rimuovi festività";
        document.getElementById("festabutton").innerText = "Rimuovi festività";
    }
    selector.css("font-weight","bold");
    selector.css("color","white");
    selector.css("background-color","darkorange");

    selectDayFest(new Date(date.getFullYear(), date.getMonth(), selector.text()));
}

function clickCalendarPres(id) {
    let selector = $('#'.concat(id));

    if (selector.css("color") !== "rgb(0, 0, 0)")
        return;

    if (dayIdSelected !== null) {
        if(dayIdSelected.length === 3 && document.getElementById("festabutton").value === "Rimuovi festività")
            $('#'.concat(dayIdSelected)).css("color","darkorange");
        else
            $('#'.concat(dayIdSelected)).css("color","black");
    
        $('#'.concat(dayIdSelected)).css("background-color","transparent");
        $('#'.concat(dayIdSelected)).css("font-weight","normal");
    }

    dayIdSelected = id;

    selector.css("color","white");
    selector.css("background-color","darkorange");
    selector.css("font-weight","bold");

    selectDay(new Date(date1.getFullYear(), date1.getMonth(), selector.text()));
}

function festaButton() {
    if(dayIdSelected === null) return;
    let selector = $('#'.concat(dayIdSelected));
    let btn = document.getElementById("festabutton");
    if(btn.value === "Imposta come festività") {
        btn.value = "Rimuovi festività"
        btn.innerText = "Rimuovi festività"
        selector.css("color", "darkorange");
        selector.css("background-color", "transparent");
        selector.css("font-weight", "normal");
        impostaFesta(daySelected, dayIdSelected);
        loadFeste();
        loadFeste1();
        dayIdSelected = null;
    }
    else {
        btn.value = "Imposta come festività"
        btn.innerText = "Imposta come festività"
        selector.css("color", "black");
        selector.css("background-color", "transparent");
        selector.css("font-weight", "normal");
        rimuoviFesta(daySelected, dayIdSelected);
        loadFeste();
        loadFeste1();
        dayIdSelected = null;
    }
}