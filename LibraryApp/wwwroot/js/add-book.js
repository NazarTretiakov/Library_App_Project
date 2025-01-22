const enteredSigns = document.querySelector(".create-book_form-items-input-content-element-number-of-signs-entered");
const inputForContent = document.querySelector(".create-book_form-items-input-content-element");
const fileInput = document.querySelector('.create-book_form-items-input-image-element');
const uploadButton = document.querySelector('.create-book_form-items-input-image-custom-uploader');


fileInput.addEventListener('change', () => {
    if (fileInput.files.length > 0) {
        uploadButton.textContent = 'File has been uploaded';
    }
});

inputForContent.addEventListener("input", e => {
    enteredSigns.textContent = inputForContent.value.length;

    if (enteredSigns.textContent > 1000) {
        enteredSigns.classList.add("create-book_form-items-input-content-element-number-of-signs-entered-overflow");
    } else {
        enteredSigns.classList.remove("create-book_form-items-input-content-element-number-of-signs-entered-overflow");
    }
});