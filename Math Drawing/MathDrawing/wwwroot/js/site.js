// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle ands.

// Write your Javascript code.


function startRoomNotifications(Room,flagNotifications) {
    "use strict";
    var currentRoom = Room.toString();
    var connection = new signalR.HubConnectionBuilder().withUrl("/drawHub").build();

    connection.on("ReceiveMessage", function (user, message) {
        var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        var encodedMsg = user + ": " + msg;
        var line = "------------------";
        var li = document.createElement("li");
        var lineM = document.createElement("lineM");
        li.textContent = encodedMsg;
        lineM.textContent = line;
        document.getElementById("messagesList").appendChild(li);
        document.getElementById("messagesList").appendChild(lineM);
    });


    connection.start().then(function () {
        connection.invoke("Subscribe", currentRoom).catch(function (err) {
            return console.error(err.toString());
        });
        if (flagNotifications==0) { 
        connection.invoke("Ujoin", currentRoom).catch(function (err) {
            return console.error(err.toString());
        });
        connection.invoke("Umay", currentRoom).catch(function (err) {
            return console.error(err.toString());
        });
        }
        if (flagNotifications == 1) {
            connection.invoke("StartGame", currentRoom).catch(function (err) {
                return console.error(err.toString());
            });
        }
        if (flagNotifications == 2) {
            connection.invoke("SendResult", "true",currentRoom).catch(function (err) {
                return console.error(err.toString());
            });
        }
        if (flagNotifications == 3) {
            connection.invoke("SendResult", "false", currentRoom).catch(function (err) {
                return console.error(err.toString());
            });
        }
        if (flagNotifications == 4) {
            connection.invoke("EmptyEq", currentRoom).catch(function (err) {
                return console.error(err.toString());
            });
        }
        if (flagNotifications == 5) {
            connection.invoke("SendResult","skip", currentRoom).catch(function (err) {
                return console.error(err.toString());
            });
        }
    });
}

function endGame(Room, score) {
    var currentRoom = Room.toString();
    var connection = new signalR.HubConnectionBuilder().withUrl("/drawHub").build();
    connection.start().then(function () {
        connection.invoke("endGame", currentRoom, score).catch(function (err) {
            return console.error(err.toString());
        });
    });
}
