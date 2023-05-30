// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function showAlert() {
    alert("Button clicked!");
}

function changetext() {
    document.getelementbyid("text").innerhtml = "text changed!";
}

function changecolor() {
    document.getelementbyid("element").style.color = "red";
}

function addElement() {
    var newDiv = document.createElement("div");
    newDiv.innerHTML = "I am a new div!";
    document.body.appendChild(newDiv);
}

function makeRequest() {
    var xhr = new XMLHttpRequest();
    xhr.onload = function () {
        if (xhr.status === 200) {
            alert(xhr.responseText);
        }
    };
    xhr.open("GET", "/path/to/resource", true);
    xhr.send();
}

document.getElementById("button").addEventListener("click", showAlert);
document.getElementById("button").addEventListener("click", changeText);
document.getElementById("button").addEventListener("click", changeColor);
document.getElementById("button").addEventListener("click", addElement);
document.getElementById("button").addEventListener("click", makeRequest);

document.getElementById("button").addEventListener("click", function () {
    showAlert();
    changeText();
    changeColor();
    addElement();
    makeRequest();
});

window.onload = function () {
    showAlert();
}


