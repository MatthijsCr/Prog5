
function updateHiddenInput() {
    const nameInput = document.getElementById('nameInput');
    const hiddenInput = document.getElementById('hiddenNameInput');
    hiddenInput.value = nameInput.value;
}