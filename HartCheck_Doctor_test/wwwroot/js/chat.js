"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var currentUser = null;
document.getElementById("sendButton").disabled = true;

document.getElementById("userInput").addEventListener("input", function () {
    if (currentUser === null) {
        currentUser = 'doctor';
    }
});

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    let isCurrentUserMessage = user === currentUser;
    li.className = isCurrentUserMessage ? 'd-flex justify-content-end mb-4':'d-flex justify-content-start mb-4';

    var divElement = document.createElement('div');
    divElement.className = isCurrentUserMessage ? 'msg_cotainer_send' : 'msg_cotainer';
    divElement.innerHTML = `${message}`;

    var spanElement = document.createElement('span');
    spanElement.className = isCurrentUserMessage ? 'msg_time_send' : 'msg_time';
    spanElement.innerHTML = getCurrentDateTime();

    divElement.appendChild(spanElement);
    li.appendChild(divElement);
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {

    var user = document.getElementById("userInput").value;
    if (user === null){
        user = currentUser
    }
    var message = document.getElementById("messageInput").value;


    connection.invoke("sendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    message.value = "";
    event.preventDefault();
});

function getCurrentDateTime() {
    var currentDate = new Date();

    var monthNames = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    var month = monthNames[currentDate.getMonth()];

    var day = currentDate.getDate();

    var hour = currentDate.getHours() % 12;
    if (hour === 0) {
        hour = 12;
    }

    var minutes = currentDate.getMinutes();

    var amOrPm = currentDate.getHours() < 12 ? 'AM' : 'PM';

    var formattedDateTime = month + ' ' + day + ', ' + hour + ':' + minutes.toString().padStart(2, '0') + amOrPm;

    return formattedDateTime;
}