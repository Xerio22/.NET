var content = document.forms["contact-form"]["Content"].value;
var errorBox = document.getElementById('contact-error-box');

function validateContactForm() {
    if (content == null || content.trim().length == 0) {
        showContactError("Treść nie może być pusta");
        return false;
    }
    if (content.length > 2000) {
        showContactError("Treść nie może być dłuższa niż 2000 znaków");
        return false;
    }
    return true;
}

function showContactError(error) {
    errorBox.innerHTML = error;
}