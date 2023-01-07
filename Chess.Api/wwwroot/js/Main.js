let token;
let connection;

let currentVector = {
    currentX: null,
    currentY: null,
};
let newVector = {
    newX: null,
    newY: null
};
let moveResponse = document.getElementById("move-response");

let selectedPiece = null;
function PieceClick(x, y) {
    if (selectedPiece === null) {
        selectedPiece = {
            x,
            y
        }
        console.log('selectedPiece');
        return;
    }
    let currentVector = selectedPiece;
    let newVector = {
        x: x,
        y: y
    };
    let selectedOptionIndex = 0; 
    connection.send('Move', currentVector, newVector, selectedOptionIndex);
    selectedPiece = null;
    console.log('MovePiece');
}

let initBoard = [
    ['R', 'N', 'B', 'Q', 'K', 'B', 'N', 'R'],
    ['P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'],
    [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
    [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
    [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
    [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
    ['p', 'p', 'p', 'p', 'p', 'p', 'p', 'p'],
    ['r', 'n', 'b', 'q', 'k', 'b', 'n', 'r']
];

/**
 * 
 * @param {HTMLElement} rootElement
 * @param {any} board
 */

function generateChessBoard(rootElement, board) {
    let aChar = 'a'.charCodeAt(0);
    rootElement.innerHTML = '';
    for (let i = 7; i >= 0; i--) {
        for (let z = 0; z < 8; z++) {
            let currentFile = String.fromCharCode(aChar + z);
            let element = document.createElement('div');
            element.classList.add('field');
            element.classList.add('field-' + currentFile + (i + 1));
            if ((i + z) % 2 === 0)
                element.classList.add('field-black');
            else
                element.classList.add('field-white');
            let local = board[i][z];
            if (local !== " ") {
                let div = charToPIece(local);
                element.appendChild(div);
            }
            element.addEventListener('click', () => {
                PieceClick(z, i);
                console.log('klik ' + currentFile + (i + 1));
                element.classList.add('field-selected');
            })

            rootElement.appendChild(element);
        }
    }
}
/**
 * 
 * @param {String} char
 */
function charToPIece(char) {
    let keysDiv = document.createElement('div');
    if (char === char.toUpperCase())
        keysDiv.classList.add('piece-white');
    else
        keysDiv.classList.add('piece-black');
    keysDiv.innerText = char;
    keysDiv.classList.add('piece');
    return keysDiv;
}

token = localStorage.getItem('token');
document.getElementById('token').value = token;

generateChessBoard(document.getElementById('chess-board'), initBoard);

let submitButton = document.getElementById('submit-button');
submitButton.addEventListener('click', setToken);

function setToken() {
    token = document.getElementById('token').value;
    localStorage.setItem('token', token)
}

const connectionOnButton = document.getElementById("connection-on");
connectionOnButton.addEventListener("click", () => connect());

const startGameButton = document.getElementById("start-game-button");
startGameButton.addEventListener("click", () => {
    connection.send('CreateRoom');
});

const joinGameButton = document.getElementById("join-to-the-room");
joinGameButton.addEventListener("click", () => {
    let kod = document.getElementById('kodPokoju');
    connection.send('JoinRoom', kod.value);
});

function login(email, password) {
    fetch('/EnglishApi/account/user/login', {
        method: "POST",
        headers: new Headers({
            'content-type': 'application/json'
        }),
        body: JSON.stringify({
            email: email,
            password: password
        })
    })
        .then(async response => {
            if (!response.ok) {
                throw response.status + " , " + await response.text();
            }
            return response;
        })
        .then(response => response.text())
        .then(response => {
            token = response;
            document.getElementById('login-form').style.display = 'none';
            console.log("essa", email);
            connect();
        })       
        .catch(err => console.error("mój tekst", err));          
}

function loginAsXXXX() {
    login('xxxx', 'xxxx');
    document.getElementById('login-xxxx').style.backgroundColor = 'green';
}
function loginAsYYYY() {
    login('yyyy', 'yyyy');
    document.getElementById('login-yyyy').style.backgroundColor = 'red';
}

function connect() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl('/Hubs/ChessHub', {
            accessTokenFactory: () => token
        })
        .configureLogging(signalR.LogLevel.Trace)
        .build();

    connection.on("onConnected", (message, playerList) => {
        moveResponse.value = message;
        console.log(message);
        console.log(playerList);
    });

    connection.on("AddRoom", (player) => {
        moveResponse.value = "connectionId =  " + player.ConnectionId;
        console.log("connectionId =  ", player.ConnectionId);
    })

    connection.on("StartGame", (message) => {
        moveResponse.value = message;
        console.log(message);
    })

    connection.on("ErrorMove", (message) => {
        moveResponse.value = message;
        console.log('error move', message);
        document.querySelectorAll('.field-selected').forEach(element => {
            element.classList.remove('.field-selected');
        })
    })

    connection.on("MoveResponse", (message, boardStatus) => {
        let localBoard = [
            [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
            [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
            [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
            [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
            [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
            [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
            [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
            [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ']
        ];
        console.log(message);
        moveResponse.value = message;
        boardStatus.Figures.map(x => {
            let localChar = x.FigureChar;
            if (x.WhiteColor)
                localChar = localChar.toUpperCase();
            localBoard[x.Vector2.Y][x.Vector2.X] = `${localChar}`;
        })
        generateChessBoard(document.getElementById('chess-board'), localBoard);
    })

    connection.start()
        .then(() => console.log('Połączenie zostało nawiązane'))
        .catch(err => console.error(err.toString()));
}