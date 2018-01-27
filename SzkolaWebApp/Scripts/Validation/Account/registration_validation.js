document.getElementById('register-username').focus();


var form = document.forms['register-form'];
var usernameErrorMessage;
var passwordErrorMessage;

form.onsubmit = function () {
    clearErrors();
    var username = form['register-username'].value;
    var password = form['register-password'].value;

    return areCredentialsValid(username, password);
};

function areCredentialsValid(username, password) {
    var validationResult = true;

    if (isNullOrEmpty(username)) {
        usernameErrorMessage = "Nazwa użytkownika nie może być pusta";
        validationResult = false;
    }
    else if (username.length > 20) {
        usernameErrorMessage = "Wprowadzona nazwa użytkownika jest za długa";
        validationResult = false;
    }

    if (isNullOrEmpty(password) || password.length < 5) {
        passwordErrorMessage = "Hasło musi składać się z przynajmniej 5 znaków";
        validationResult = false;
    }
    else if (password.length > 4000) {
        passwordErrorMessage = "Hasło nie może mieć więcej niż 4000 znaków";
        validationResult = false;
    }

    showLoginErrors();
    return validationResult;
}

function isNullOrEmpty(val) {
    return val == null || val.trim().length === 0;
}

function clearErrors() {
    document.getElementById('register-credentials-error').innerHTML = '';
    usernameErrorMessage = '';
    passwordErrorMessage = '';
}

function showLoginErrors() {
    document.getElementById('register-username-error-message').innerHTML = usernameErrorMessage;
    document.getElementById('register-password-error-message').innerHTML = passwordErrorMessage;
}