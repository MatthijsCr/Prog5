
function restrictToLetters(input) {
    input.value = input.value.replace(/[^ a-zA-Z]/g, '');
}

function validateForm() {
    const nameInput = document.querySelector('.inputname');
    const errorMessage = document.querySelector('.error-message');
    const nameValue = nameInput.value;

    // Foutmelding verbergen bij elke nieuwe validatie
    errorMessage.style.display = 'none';
    errorMessage.textContent = '';

    // Controleren of het invoerveld leeg is
    if (!nameValue) {
        errorMessage.textContent = "Naam is vereist."; // Zet de foutmelding
        errorMessage.style.display = 'block'; // Toon de foutmelding
        return false; // Voorkom het indienen van het formulier
    }

    // Controleren op meerdere spaties
    if (nameValue.includes('  ')) {
        errorMessage.textContent = "Max. 1 spatie."; // Zet de foutmelding
        errorMessage.style.display = 'block'; // Toon de foutmelding
        return false; // Voorkom het indienen van het formulier
    }

    // Controleren op leidende of achtervolgende spaties
    const trimmedNameValue = nameValue.value.trim();
    if (nameValue.length !== trimmedNameValue.length) {
        errorMessage.textContent = "Begint/Eindigt met spatie."; // Zet de foutmelding
        errorMessage.style.display = 'block'; // Toon de foutmelding
        return false; // Voorkom het indienen van het formulier
    }

    // Controleren of de invoer alleen letters en een enkele spatie bevat
    const regex = /^[A-Za-z]+( [A-Za-z]+)?$/;
    if (!regex.test(nameValue)) {
        errorMessage.textContent = "Max 1 spatie."; // Zet de foutmelding
        errorMessage.style.display = 'block'; // Toon de foutmelding
        return false; // Voorkom het indienen van het formulier
    }

    if (nameValue.length > 10) {
        errorMessage.textContent = "Lengte max. 10"
        errorMessage.style.display = 'block';
        return false;
    }

    return true; // Sta indienen van het formulier toe als alle controles slagen
}