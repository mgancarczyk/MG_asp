"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build(); // łączenie się z hubem pod adresem chathub

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) { // oczekujemy na wiadomość ReciveMessage
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li); // doklejanie kolejnych wiadomości
});

connection.start().then(function () { //metoda startujaca aplikacje
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString()); //obsługa wyjątków 
});

document.getElementById("sendButton").addEventListener("click", function (event) { // zdazenie klik
    var user = document.getElementById("userInput").value; //sczytywanie wartoiści user
    var message = document.getElementById("messageInput").value; //sczytywanie wartoiści message
    connection.invoke("SendMessage", user, message).catch(function (err) { // przekazanie nazwy urzytkownika i tresci do sendmessage
        return console.error(err.toString()); //wywołanie metody a nastepnie zostanie wszystko rozesłane do kilentów 
    });
    event.preventDefault();
});