const enteredSigns = document.querySelector(".create-post_form-items-input-content-element-number-of-signs-entered");
const inputForContent = document.querySelector(".create-post_form-items-input-content-element");

inputForContent.addEventListener("input", e => {
    enteredSigns.textContent = inputForContent.value.length;

    if (enteredSigns.textContent > 1700) {
        enteredSigns.classList.add("create-post_form-items-input-content-element-number-of-signs-entered-overflow");
    } else {
        enteredSigns.classList.remove("create-post_form-items-input-content-element-number-of-signs-entered-overflow");
    }
});