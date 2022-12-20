const initBoard = [
    ['R', 'N', 'B', 'Q', 'K', 'B', 'N', 'R'],
    ['P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'],
    [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
    [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
    [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
    [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '],
    ['p', 'p', 'p', 'p', 'p', 'p', 'p', 'p'],
    ['r', 'n', 'b', 'q', 'k', 'b', 'n', 'r']
];

function generateChessBoard(rootElement) {
    let aChar = 'a'.charCodeAt(0);
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
            let local = initBoard[i][z];
            if (local !== " ") {
                let div = charToPIece(local);
                element.appendChild(div);
            }
            
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
//dodać klase piece-nazwa figury

generateChessBoard(document.getElementById('chess-board'));
