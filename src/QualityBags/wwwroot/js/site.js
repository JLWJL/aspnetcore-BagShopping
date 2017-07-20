// Write your Javascript code.

/*Used for admin to preview when adding or editing product image*/
if (document.querySelector('#uploadFile')) {
    var fileInput = document.querySelector('#uploadFile');
    var previewFrame = document.querySelector('#img-preview');

    fileInput.addEventListener('change', function () {

        //Remove current image 
        previewFrame.setAttribute("src", "");

        if (!fileInput.value) {
            return;
        }

        //Get the image
        var file = fileInput.files[0];
        //Check file type 
        if (file.type !== 'image/jpeg' && file.type !== 'image/png' && file.type !== 'image/gif' && file.type !== 'image/bmp') {
            alert("Please upload image file");
            return;
        }

        //Read and load image to preview
        var reader = new FileReader();
        reader.addEventListener('load', function (e) {
            var data = e.target.result;
            previewFrame.setAttribute('src', data);
        });
        reader.readAsDataURL(file);
    });
}

/*Used for all users to sort product according to price or created date*/

document.querySelector(".sort-option").onchange = function () {
    var curUrl = window.location.href;
    window.location.href = "?srtStr=" + this.value;

    console.log(this.value);
}


