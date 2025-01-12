const fileInput = document.querySelector('.settings-elements_profile-information-elements_input-photo-element');
const uploadButton = document.querySelector('.settings-elements_profile-information-elements_input-photo-custom-uploader');

fileInput.addEventListener('change', () => {
    if (fileInput.files.length > 0) {
        uploadButton.textContent = 'File has been uploaded';
    }
});