var form = document.forms['add-article-form'];
var titleErrorMessage;
var contentErrorMessage;


document.getElementById('add-article-submit').onclick = function () {
    clearErrors();
    var title = form['article-title'].value;
    var content = form['article-content'].value;

    if (isModelValid(title, content)) {
        document.forms['add-article-form'].submit();
    }
    
}

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



document.getElementById('add-article-upload-img').onclick = function (e) {
    document.getElementById('Article_Title').value = document.getElementById('article-title').value;
    document.getElementById('Article_Content').value = document.getElementById('article-content').value;

    e.preventDefault();
    document.forms['pass-article-to-photo-library-form'].submit();
    //var title = document.getElementById('article-title').value;
    //var content = document.getElementById('article-content').value;

    //var articleJSON = JSON.stringify({ ArticleId: 0, Title: title, Content: content });

    //var xhttp = new XMLHttpRequest();

    //xhttp.onreadystatechange = function () {
    //    if (this.readyState == 4 && this.status == 200) {
    //        window.location.href = '/Gallery/PhotoLibrary'
    //    }
    //};

    //xhttp.open("POST", '/Gallery/PhotoLibrary', true);
    //xhttp.setRequestHeader("Content-Type", "application/json");
    //xhttp.send(articleJSON);
};
