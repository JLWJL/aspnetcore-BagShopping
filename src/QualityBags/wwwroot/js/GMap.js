
//Google Map module
(function (window, google) {
    //Return a constructor function named 'Mapster'
    var Mapster = (function () {

        function Mapster(elem, opts) {
            this.gMap = new google.maps.Map(elem, opts);
        }

        /****************************
         Start setting various methods for Mapster
        ****************************/
        Mapster.prototype = {

            zoom: function (level) {
                if (level) {
                    this.gMap.setZoom(level)
                }
                else {
                    return this.gMap.getZoom();
                }
            },

            on: function (opts) {
                google.maps.event.addListener(opts.obj, opts.event, opts.callback);
            },

            addMarker: function (mkrOpts) {
                var marker;
                mkrOpts.map = this.gMap;
                console.log("mkrOpts.position: ",mkrOpts.position);
                mkrOpts.position = {
                    lat:mkrOpts.position.lat,
                    lng:mkrOpts.position.lng
                }
                marker = new google.maps.Marker(mkrOpts);
                marker.window = null;
                marker.windowIsOpen = false;
                if (mkrOpts.event) {
                    this.on({
                        obj: marker,
                        event: mkrOpts.event.name, 
                        callback: mkrOpts.event.callback
                    });
                }
                if (mkrOpts.content) {
                    this.on({
                        obj: marker,
                        event: 'click',
                        callback: function () {
                            if (marker.window === null) {
                                var infowindow = new google.maps.InfoWindow({ content: mkrOpts.content });
                                infowindow.open(this.gmap, marker);
                                marker.window = infowindow;
                                marker.windowIsOpen = true;
                            } else if(marker.windowIsOpen){
                                marker.window.close();
                                marker.windowIsOpen = false;
                            } else {
                                marker.window.open(this.gmap, marker);
                                marker.windowIsOpen = true;
                            }
                        }
                    })
                }

                return marker;
            },

            addInfoWindow: function (opts, mkr) {
                var infoWindow = new google.maps.InfoWindow(opts);
                //infoWindow.open(this.gMap, mkr)
                return infoWindow;
            }
        };
        /****************************
        Finish setting various methods for Mapster
        ****************************/

        return Mapster;
    }());

    //create new Mapster object
    Mapster.create = function (elem, opts) {
        return new Mapster(elem,opts);
    }

    //Create globally accessible variable
    window.Mapster = Mapster;
}(window, google));


/**
* Google Map options
*/

(function (window, google, mapster) {
    mapster.mapOptions = {
        center: {
            lat: -36.947277,
            lng: 174.718252
        },
        mapTypeId: google.maps.MapTypeId.ROADMAP
    }

}(window, google, window.Mapster || (window.Mapster = {}) ));