const enteredSigns = document.querySelector(".settings-elements_profile-information-elements_input-description-number-of-characters-entered");
const inputForContent = document.querySelector(".settings-elements_profile-information-elements_input-description-item");
const maxNumberOfSigns = 300;

inputForContent.addEventListener("input", e => {
    enteredSigns.textContent = inputForContent.value.length;

    if (enteredSigns.textContent > maxNumberOfSigns) {
        enteredSigns.classList.add("settings-elements_profile-information-elements_input-description-number-of-characters-entered-overflow");
    } else {
        enteredSigns.classList.remove("settings-elements_profile-information-elements_input-description-number-of-characters-entered-overflow");
    }
});