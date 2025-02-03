const enteredSigns = document.querySelector(".create-book_form-items-input-content-element-number-of-signs-entered");
const inputForContent = document.querySelector(".create-book_form-items-input-content-element");

inputForContent.addEventListener("input", e => {
    enteredSigns.textContent = inputForContent.value.length;

    if (enteredSigns.textContent > 500) {
        enteredSigns.classList.add("create-book_form-items-input-content-element-number-of-signs-entered-overflow");
    } else {
        enteredSigns.classList.remove("create-book_form-items-input-content-element-number-of-signs-entered-overflow");
    }
});