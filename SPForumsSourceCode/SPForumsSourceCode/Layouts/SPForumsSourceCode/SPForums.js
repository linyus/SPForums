function LMenuChoose(obj) {
    var menus = document.getElementById("LeftMenuTableZone").getElementsByTagName("a");
    for (var i = 0; i < menus.length; i++) {
        menus[i].style.color = "#0072c6";
        menus[i].style.backgroundColor = "white";
    }
    obj.style.color = "black";
    obj.style.backgroundColor = "#8EC1E5";
}

function InitLookUpField(Id) {
    var aElements = document.getElementById("Td2").getElementsByTagName("a");
    for (var i = 0; i < aElements.length; i++) {
        if (aElements[i].href.indexOf(Id) > 0) {
            var aText = aElements[i].innerText;
            aElements[i].outerHTML = aText;
        }
    }
}

function getUnReadMessage(link) {
    document.getElementById("welcomeMenuBox").innerHTML = link + document.getElementById("welcomeMenuBox").innerHTML;
}
