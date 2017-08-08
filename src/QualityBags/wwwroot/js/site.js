// Write your Javascript code.

/*Used for admin to preview when adding or editing product image*/
(function (window) {
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

}(window));

(function (window, mapster) {
    var
    mapElement = document.getElementById('gMap'),
    options = mapster.mapOptions,
    map = mapster.create(mapElement, options),
    marker,
    infoWindow;

    map.zoom(12);
    marker = map.addMarker({
        position: options.center,
        //event: {
        //    name: 'click',
        //    callback: function () {
        //    }
        //},
        content:'<h4>Quality Bags</h4>'+
                 '139, Carrington Rd, Auckland, NZ'+
                 '<br/>09-12345670'
    });

    

}(window, window.Mapster || (window.Mapster = {})));