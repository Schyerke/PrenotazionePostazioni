// noinspection JSJQueryEfficiency

let daySelected = new Date();
let dayIdSelected = null;

function selectDay(date) {
    daySelected = date;
}

//Link api google https://developers.google.com/people/api/rest/v1/people/get

function getIdDay(number) {
    for (let i = 0; i < 6; i++) {
        for (let j = 0; j < 7; j++) {
            if ($('#'.concat(j).concat('-').concat(i)).css("color") === "rgb(0, 0, 0)" &&
                $('#'.concat(j).concat('-').concat(i)).text() == number)
                return j + '-' + i;
        }
    }
    return "null";
}

function getIdDay1(number) {
    for (let i = 0; i < 6; i++) {
        for (let j = 0; j < 7; j++) {
            if ($('#'.concat(j).concat('-').concat(i).concat(1)).css("color") === "rgb(0, 0, 0)" &&
                $('#'.concat(j).concat('-').concat(i).concat(1)).text() == number)
                return j + '-' + i + "" + 1;
        }
    }
    return "null";
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

function isValidDay(id) {
    let selector = $('#'.concat(id));
    if (selector.css("color") !== "rgb(0, 0, 0)")
        return false;
    return true;
}

function goDate(newDate) {
    date = newDate;
    loadCalendar();
}

function goDate1(newDate) {
    date1 = newDate;
    loadCalendar1();
}

function selectDayFest(date) {
    daySelected = date;
}

function clickCalendarFest(id) {

    let selector = $('#'.concat(id));

    if (selector.css("color") !== "rgb(0, 0, 0)" && selector.css("color") !== "rgb(255, 140, 0)")
        return;

    if (dayIdSelected !== null) {
        if (document.getElementById("festabutton").value === "Imposta come festività")
            $('#'.concat(dayIdSelected)).css("color", "black");
        else if (dayIdSelected.length === 4)
            $('#'.concat(dayIdSelected)).css("color", "black");
        else $('#'.concat(dayIdSelected)).css("color", "darkorange");
        $('#'.concat(dayIdSelected)).css("background-color", "transparent");
        $('#'.concat(dayIdSelected)).css("font-weight", "normal");
    }

    dayIdSelected = id;

    if (selector.css("color") === "rgb(0, 0, 0)") {
        document.getElementById("festabutton").value = "Imposta come festività";
        document.getElementById("festabutton").innerText = "Imposta come festività";
    }
    else {
        document.getElementById("festabutton").value = "Rimuovi festività";
        document.getElementById("festabutton").innerText = "Rimuovi festività";
    }
    selector.css("font-weight", "bold");
    selector.css("color", "white");
    selector.css("background-color", "darkorange");

    selectDayFest(new Date(date.getFullYear(), date.getMonth(), selector.text()));
}

function clickCalendarPres(id) {
    let selector = $('#'.concat(id));

    if (selector.css("color") !== "rgb(0, 0, 0)")
        return;

    if (dayIdSelected !== null) {
        if (dayIdSelected.length === 3 && document.getElementById("festabutton").value === "Rimuovi festività")
            $('#'.concat(dayIdSelected)).css("color", "darkorange");
        else
            $('#'.concat(dayIdSelected)).css("color", "black");

        $('#'.concat(dayIdSelected)).css("background-color", "transparent");
        $('#'.concat(dayIdSelected)).css("font-weight", "normal");
    }

    dayIdSelected = id;

    selector.css("color", "white");
    selector.css("background-color", "darkorange");
    selector.css("font-weight", "bold");

    selectDay(new Date(date1.getFullYear(), date1.getMonth(), selector.text()));
}

function festaButton() {
    if (dayIdSelected === null) return;
    let selector = $('#'.concat(dayIdSelected));
    let btn = document.getElementById("festabutton");
    if (btn.value === "Imposta come festività") {
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