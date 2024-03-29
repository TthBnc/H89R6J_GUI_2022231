﻿let characters = [];
let connection = null;

let charIdToUpdate = -1;


getCharData()
setupSignalR()

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:8160/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("CharacterCreated", (user, message) => {
        //console.log(user);
        //console.log(message);
        getCharData();
    });

    connection.on("CharacterDeleted", (user, message) => {
        //console.log(user);
        //console.log(message);
        getCharData();
    });

    connection.on("CharacterUpdated", (user, message) => {
        //console.log(user);
        //console.log(message);
        getCharData();
    });

    connection.onclose(async () => {
            await start();
        });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getCharData() {
    await fetch('http://localhost:8160/character')
        .then(x => x.json())
        .then(y => {
            characters = y;
            //console.log(characters);
            displayChar();
        });
}

function displayChar() {
    document.getElementById('resultarea').innerHTML = "";

    characters.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td> " +
            t.id + "</td><td>" +
            t.name + "</td><td>" +
            t.element + "</td><td>" +
        `<button type="button" onclick="removeChar(${t.id})">Delete</button>` +
        `<button type="button" onclick="showUpdateChar(${t.id})">Update</button>` +
             "</td></tr>";
        console.log(t.name);
    })
}

function createChar() {
    let tname = document.getElementById('characterName').value;
    let telement = document.getElementById('characterElement').value;

    fetch('http://localhost:8160/character', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                name: tname,
                element: telement
            }),
        })
        .then(response => response)
        .then(data =>
        {
            console.log('Success:', data);
            getCharData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function removeChar(id) {
    fetch('http://localhost:8160/character/' + id, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getCharData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function showUpdateChar(id) {
    let tname = document.getElementById('characterNameUpdate').value = characters
        .find(t => t['id'] == id)['name'];
    let telement = document.getElementById('characterElementUpdate').value = characters
        .find(t => t['id'] == id)['element'];

    document.getElementById('updateformdiv').style.display = 'flex';

    charIdToUpdate = id;
}

function updateChar() {
    document.getElementById('updateformdiv').style.display = 'none';

    let tname = document.getElementById('characterNameUpdate').value;
    let telement = document.getElementById('characterElementUpdate').value;

    fetch('http://localhost:8160/character', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                id: charIdToUpdate,
                name: tname,
                element: telement
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getCharData();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}
