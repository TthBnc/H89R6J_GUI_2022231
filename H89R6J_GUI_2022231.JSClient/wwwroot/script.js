let characters = [];

getCharData()


async function getCharData() {
    await fetch('http://localhost:8160/character')
        .then(x => x.json())
        .then(y => {
            characters = y;
            console.log(characters);
            displayChar();
        });
}

function displayChar() {
    document.getElementById('resultarea').innerHTML = "";

    characters.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" +
            t.id + "</td><td>" +
            t.name + "</td><td>" +
            t.element + "</td><td>" +
            `<button type="button" onclick="removeChar(${t.id})">Delete</button>`+ "</td></tr>";
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