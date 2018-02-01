var form = document.forms['upload-photos-form'];
var fileErrorMessage = document.getElementById('FileErrorMessage');
var extensionErrorMessage = document.getElementById('ExtensionErrorMessage');

form.onsubmit = function () {
    return validateUploadPhotosForm();
}

function validateUploadPhotosForm() {
    clearErrors();
    var photos = document.getElementById('UploadedPhotos').files;

    if (photos.length === 0 || photos == null || photos === '') {
        showUploadPhotosError("Nie wybrano pliku");
        return false;
    }
    var extensions = [".png", ".jpg", ".jpeg", ".gif"];

    var photosArray = Array.from(photos);
    for (var i = 0; i < photosArray.length; i++){
        var ph = photosArray[i];
        var extensionIsValid = extensions.some(ext => { console.log('do ' + ph.name + ' ends with ' + ext + '? -> ' + ph.name.endsWith(ext));return ph.name.endsWith(ext); });

        if (!extensionIsValid) {
            showPhotosExtensionError("Plik ma nieprawidłowe roszerzenie. Dozwolone rozszerzenia to: " + extensions);
            return false;
        }
    };

    return true;
}

function showUploadPhotosError(error) {
    fileErrorMessage.innerHTML = error;
}

function showPhotosExtensionError(error) {
    extensionErrorMessage.innerHTML = error;
}

function clearErrors() {
    fileErrorMessage.innerHTML = '';
    extensionErrorMessage.innerHTML = '';
}


document.getElementsByName('img-container').forEach(c => {
    c.onclick = function () {
        if (c.style.borderColor === "green") {
            c.children["PhotosToInsertIDs"].checked = "";
            c.style.border = "2px solid grey";
        }
        else {
            c.children["PhotosToInsertIDs"].checked = "checked";
            c.style.border = "4px solid green";
        }
    }

    if (c.children["PhotosToInsertIDs"].checked === true) {
        c.style.border = "4px solid green";
        console.log('changed color');
    }
});

document.getElementsByName('delete-photo').forEach(elem => {
    elem.onclick = function (e) {
        return confirm('Usunięcie zdjęcia spowoduje usunięcie go ze wszystkich artykułów, czy jesteś pewien, że chcesz je usunąć?');
    }
});


var photosForm = document.forms['insert-photos-form'];
var buttonInsert = document.getElementById('insert-photos-to-article');

buttonInsert.onclick = function (e) {
    //photosForm['PhotosToInsert'].value = toInsert;
    //e.preventDefault();
//photosForm.submit();
}