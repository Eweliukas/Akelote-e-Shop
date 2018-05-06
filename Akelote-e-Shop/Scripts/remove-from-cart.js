$(function () {
    // Document.ready -> link up remove event handler
    $(".RemoveLink").click(function () {
        // Get the id from the link
        var recordToDelete = $(this).attr("data-id");
        if (recordToDelete != '') {
            // Perform the ajax post
            $.post("/ShoppingCart/RemoveFromCart", { "id": recordToDelete },
                function (data) {
                    // Successful requests get here
                    // Update the page elements
                    if (data.ItemCount == 0) {
                        $('#row-' + data.DeleteId).fadeOut('slow');
                    } else {
                        $('#item-count-' + data.DeleteId).text(data.ItemCount);
                    }
                    if (data.CartTotal == 0) {
                        $('.data-view').remove();
                        $('.no-data').show();
                    } else {
                        $('#cart-total').text(data.CartTotal);
                    }
                    $('#update-message').text(data.Message);
                    $('#cart-status').text('Cart (' + data.CartCount + ')');
                    $.getJSON('/ShoppingCart/Count', function (data) {
                        $('#num-items-in-cart').text(data.count);
                    });
                });
        }
    });
});