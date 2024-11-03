
function restrictToLetters(input) {
    input.value = input.value.replace(/[^ a-zA-Z]/g, '');
}