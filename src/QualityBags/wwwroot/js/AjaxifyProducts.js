$(document).ready(function () {

    /**
    ===================================
    Displaying bags by users' selection

      How it works:
    * 1. Assign 'filter-option' class to all filtering controls used to filter bags
    * 2. Create an object to store the filtering options
    * 3. Bind event handler for all filtering controls
    ===================================
    */

    ;(function () {

        var filter_ctls = $('.filter-option');
        var url = "asp_practical/Home/FilteredBags/"; //For AJAX - published
        //var url = "/FilteredBags/"; //For AJAX - local

        //DOM elements
        var prePageBtn = $('.pg-pre');
        var nextPageBtn = $('.pg-next');

        //To store user's selections
        var filters = {
            catID: "",
            srtStr: "",
            srcStr: "",
            page: ""
        };

        //Binding click handler for all filter controls
        filter_ctls.on('click', function (event) {
            if ($(event.target).attr('type') === 'submit') {
                event.preventDefault(); //prevent request to defautl action
            }
            createFilters(event);
            displayBags();
        });

        //Assign filter option according to which control is clicked
        function createFilters(event) { 
            var elem = $(event.target);
            var metedata = elem.data();

            //Determine filter control by metedata attribute
            if (metedata) {
                if (metedata.catid !== undefined) {
                    resetFilters();
                    filters.catID = metedata.catid;
                    filters.curCat = metedata.catid;
                }
                else if(metedata.page!==undefined){
                    filters.page = metedata.page;
                }
                else if(metedata.srtstr!==undefined){
                    filters.srtStr = metedata.srtstr;
                    filters.page = "";
                }
                else { //otherwise it is search button 
                    var inputT = elem.siblings();
                    var inputV = inputT.val();
                    resetFilters();
                    filters.srcStr = inputV;

                }
            }
        }
    
        function displayBags() {
            $.getJSON(url, filters, function (data) {
                //Parse AJAX data
                var curPage = $.parseJSON(data)['Result'].CurPage;
                var hasNextPage = $.parseJSON(data)['Result'].HasNextPage;
                var hasPrePage = $.parseJSON(data)['Result'].HasPreviousPage;
                var bagData = $.parseJSON(data)['Result'].PagedBagList;
            
                var bags = $('.bag-container');
                var productBlock = bags.eq(0).clone(); //Clone 1 bag container to create more later on
                var iterate = 0;

                //Remove all bags block
                bags.remove();

                $.each(bagData, function (key, val) {
                    AddBag(productBlock, iterate, val);
                    iterate++;
                });

                iterate = 0;

                //Deal with pager
                changePager(hasNextPage, hasPrePage, curPage);

            });
        }

        //Create bag DOM blocks and append
        function AddBag(bagFrame, i, val) {

            var newBag = bagFrame.clone();

            newBag.find('img').attr('src', 'asp_practical/'+val.ImagePath);
            newBag.find('h4').text(val.BagName);
            newBag.find('span').html('$' + val.Price);
            newBag.find('a').attr('src', '/wangj174/asp_practical/ShoppingCarts/AddToCart/' + val.bagID);

            //Four bags per row
            if (i > 3) {
                newBag.appendTo($('.product-area')[1]);
            } else {
                newBag.appendTo($('.product-area')[0]);
            }
        }

        function changePager(hasNextPage, hasPrePage, curPage) {
            if (hasNextPage) {
                nextPageBtn.removeClass('disabled');
                nextPageBtn.data("page", curPage + 1);
            } else {
                nextPageBtn.addClass('disabled');
            }

            if (hasPrePage) {
                prePageBtn.removeClass('disabled');
                prePageBtn.data("page", curPage - 1);
            } else {
                prePageBtn.addClass('disabled');
                prePageBtn.data("page", curPage);
            }
        }

        //Reset filter when choosing a category or do a search
        function resetFilters() {
            for (var option in filters) {
                filters[option] = "";
            }
        }
    })();



});