tiles = (function() {

    $(document).ready(function() {
        tiles.onDocumentReady();
    });

    return {
        onDocumentReady: function() {
            tiles.wireUpTiles();
        },

        wireUpTiles: function() {

            $(".tile").mouseover(function() {
                var element = $(this).find(".labelLowerLeft");
                if (element != null) {
                    element.addClass("underline");
                }
            });

            $(".tile").mouseout(function() {
                var element = $(this).find(".labelLowerLeft");
                if (element != null) {
                    element.removeClass("underline");
                }
            });
            
        }

    };
})()