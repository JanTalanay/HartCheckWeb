"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var currentUser = 0;
var originalTitle = document.title;
document.getElementById("sendButton").disabled = true;

document.addEventListener('visibilitychange', function() {
    if (document.visibilityState === 'hidden') {
        console.log('Tab is not active');
        currentUser = 0;
        
        
    } else if (document.visibilityState === 'visible') {
        console.log('Tab is active');
        changeTitle(originalTitle);
        currentUser = 1;
        
    }
});

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    let isCurrentUserMessage = currentUser===1;
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
    
    let audio = new Audio('~/sound/ping.mp3')
    
    if (document.visibilityState === 'hidden'){
        setTimeout(function() {

            changeTitle('New Message from Patient ');
            audio.play();
        },  5000);
        
    }
    toggleNav();
   
    console.log(user,message)
    
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {

    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var txtBox = document.getElementById("messageInput");


    connection.invoke("sendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    txtBox.value = '';
    event.preventDefault();
});

function changeTitle(newTitle) {
    document.title = newTitle;
}

function toggleNotif(){
    let notif_box = document.getElementById("notif_box");
    notif_box.classList.toggle("open-box");
}


function toggleNav(){
    let subMenu = document.getElementById("submenu");
    subMenu.classList.toggle("open-menu");

    var notifs = document.getElementById("notifications");
    notifs.innerHTML = "Patient X has sent you a message"//alter here
    console.log("toggled!")
}

// function showNotificationBubble() {
//     // Check if the notification bubble already exists
//     var bubble = document.getElementById('notificationBubble');
//     if (!bubble) {
//         // Create the notification bubble element
//         bubble = document.createElement('div');
//         bubble.id = 'notificationBubble';
//         bubble.className = 'notification-bubble';
//         bubble.textContent = 'A Patient has sent a message';
//         bubble.style.overflowY = 'visible';
//
//         // Append the bubble to the notifications list item
//         var notifx = document.getElementById('notifx');
//         notifx.appendChild(bubble);
//     }
//
//     // Toggle the visibility of the bubble
//     bubble.style.display = bubble.style.display === 'none' ? 'block' : 'none';
//
// }
// document.getElementById('notifx').addEventListener('click', showNotificationBubble);

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