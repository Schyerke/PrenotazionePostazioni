let date = new Date;
let date1 = new Date;
let month = date.getMonth();
let month1 = date1.getMonth();

function loadCalendar() {
    date.setDate(1);

    $('#month-year').text(translateMonth(date.getMonth()) + " " + date.getFullYear());
    month = date.getMonth();

    for (let i = 0; i<7; i++)
        for (let j = 0; j<6; j++) {
            $('#'.concat(i).concat('-').concat(j)).text("");
            $('#'.concat(i).concat('-').concat(j)).css("color", "black");
        }

    let firstLineLoad = true;

    for (let i = 0; i<6; i++) {
        for (let j = 0; j<7; j++) {
            if (firstLineLoad === true) {
                date.setDate(date.getDate()-((date.getDay() === 0 ? +6 : date.getDay()-1)));
                firstLineLoad = false;
            }
            $('#'.concat(j).concat('-').concat(i)).text(date.getDate());
            if (date.getMonth() !== month)
                $('#'.concat(j).concat('-').concat(i)).css("color", "#a6a6a6");
            date.setDate(date.getDate()+1);
        }
    }

    loadFeste();
    checkSelected();
}

function loadFeste() {
    for (let i = 0; i < festivita.length; i++) {
        let month_temp = month + 1; let yr_temp = date.getFullYear();
        if (month_temp === 12) {
            month_temp = 0;
        }
        if (festivita[i].date.getMonth() === month_temp && festivita[i].date.getFullYear() === yr_temp)
            $('#'.concat(festivita[i].id.replace('f', ''))).css("color", "darkorange");
    }
}

function loadCalendar1() {
    date1.setDate(1);

    $('#month-year-1').text(translateMonth(date1.getMonth()) + " " + date1.getFullYear());
    month1 = date1.getMonth();

    for (let i = 0; i < 7; i++)
        for (let j = 0; j < 6; j++) {
            $('#'.concat(i).concat('-').concat(j).concat('1')).text("");
            $('#'.concat(i).concat('-').concat(j).concat('1')).css("color", "black");
        }

    let firstLineLoad = true;

    for (let i = 0; i < 6; i++) {
        for (let j = 0; j < 7; j++) {
            if (firstLineLoad === true) {
                date1.setDate(date1.getDate() - ((date1.getDay() === 0 ? +6 : date1.getDay() - 1)));
                firstLineLoad = false;
            }
            $('#'.concat(j).concat('-').concat(i).concat('1')).text(date1.getDate());
            if (date1.getMonth() !== month1)
                $('#'.concat(j).concat('-').concat(i).concat('1')).css("color", "#a6a6a6");
            date1.setDate(date1.getDate() + 1);
        }
    }

    loadFeste1();
    checkSelected1()
}

function loadFeste1() {
    for (let i = 0; i < festivita.length; i++) {
        let month_temp = month1 + 1; let yr_temp = date1.getFullYear();
        if (month_temp === 12) {
            month_temp = 0;
        }
        if ((festivita[i].date.getMonth()) === month_temp && festivita[i].date.getFullYear() === yr_temp) {
            $('#'.concat(festivita[i].id.replace('f', '')).concat('1')).css("color", "darkorange");
        }
    }
}

function prevMonth() {
    date.setMonth(date.getMonth() - 2);
    loadCalendar();
}

function prevMonth1() {
    date1.setMonth(date1.getMonth() - 2);
    loadCalendar1();
}

function nextMonth() {
    date.setMonth(date.getMonth());
    loadCalendar();
}

function nextMonth1() {
    date1.setMonth(date1.getMonth());
    loadCalendar1();
}

function translateMonth(number) {
    switch (number) {
        case 0:
            return "Gennaio";
        case 1:
            return "Febbraio";
        case 2:
            return "Marzo";
        case 3:
            return "Aprile";
        case 4:
            return "Maggio";
        case 5:
            return "Giugno";
        case 6:
            return "Luglio";
        case 7:
            return "Agosto";
        case 8:
            return "Settembre";
        case 9:
            return "Ottobre";
        case 10:
            return "Novembre";
        case 11:
            return "Dicembre";
    }
    return "";
}

function daysInMonth (month) {
    return new Date(1970, month, 0).getDate();
}

function checkSelected() {
    if (daySelected.getMonth() !== date.getMonth() || daySelected.getFullYear() !== date.getFullYear()) {
        $('#'.concat(dayIdSelected)).css("color","black");
        $('#'.concat(dayIdSelected)).css("background-color","transparent");
        $('#'.concat(dayIdSelected)).css("font-weight","normal");
    } else {
        $('#'.concat(dayIdSelected)).css("color","white");
        $('#'.concat(dayIdSelected)).css("background-color","darkorange");
        $('#'.concat(dayIdSelected)).css("font-weight","bold");
    }
}

function checkSelected1() {
    if (daySelected.getMonth() !== date1.getMonth()) {
        $('#'.concat(dayIdSelected)).css("color", "black");
        $('#'.concat(dayIdSelected)).css("background-color", "transparent");
    } else {
        $('#'.concat(dayIdSelected)).css("color", "white");
        $('#'.concat(dayIdSelected)).css("background-color", "darkorange");
    }
}