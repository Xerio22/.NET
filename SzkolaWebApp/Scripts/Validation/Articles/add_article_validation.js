var form = document.forms['add-article-form'];
var titleErrorMessage;
var contentErrorMessage;

form.onsubmit = function () {
    clearErrors();
    var title = form['article-title'].value;
    var content = form['article-content'].value;

    return isModelValid(title, content);
};

function isModelValid(title, content) {
    var validationResult = true;

    if (isNullOrEmpty(title)) {
        titleErrorMessage = "~ Tytuł nie może być pusty";
        validationResult = false;
    }
    else if (title.length > 60) {
        titleErrorMessage = "~ Tytuł jest za długi. Maksymalna liczba znaków to 60";
        validationResult = false;
    }

    if (isNullOrEmpty(content)) {
        contentErrorMessage = "~ Treść nie może być pusta";
        validationResult = false;
    }
    else if (content.length > 4000) {
        contentErrorMessage = "~ Treść jest za długa. Maksymalna liczba znaków to 4000";
        validationResult = false;
    }

    showArticleErrors();
    return validationResult;
}

function isNullOrEmpty(val) {
    return val == null || val.trim().length === 0;
}

function clearErrors() {
    titleErrorMessage = '';
    contentErrorMessage = '';
}

function showArticleErrors() {
    document.getElementById('title-error-message').innerHTML = titleErrorMessage;
    document.getElementById('content-error-message').innerHTML = contentErrorMessage;
}


document.getElementById('add-article-upload-img').onclick = function () {
    document.getElementById('tit').value = document.getElementById('article-title').value;
    document.getElementById('cont').value = document.getElementById('article-content').value;
}



/*
var form = document.forms['add-article-form'];

form.onsubmit = function () {
    var title = form['article-title'].value;
    var content = form['article-content'].value;

    return isModelValid(title, content);
};

function isModelValid(title, content) {
    var validationResult = true;

    if (isNullOrEmpty(title)) {
        showTitleError("~ Tytuł nie może być pusty");
        validationResult = false;
    }
    else if (title.length > 60) {
        showTitleError("~ Tytuł jest za długi. Maksymalna liczba znaków to 60");
        validationResult = false;
    }

    if (isNullOrEmpty(content)) {
        showContentError("~ Treść nie może być pusta");
        validationResult = false;
    }
    else if (content.length > 4000) {
        showContentError("~ Treść jest za długa. Maksymalna liczba znaków to 4000");
        validationResult = false;
    }

    return validationResult;
}

function isNullOrEmpty(val) {
    return val == null || val.trim().length === 0;
}

function showTitleError(titleErrorMessage) {
    document.getElementById('title-error-message').innerHTML = titleErrorMessage;
}

function showContentError(contentErrorMessage) {
    document.getElementById('content-error-message').innerHTML = contentErrorMessage;
}
*/