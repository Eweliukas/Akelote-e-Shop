﻿$(function () {
    $('.add-to-cart').click(function () {
        var row = $($(this).data('animation-target') || this);
        $.post('/ShoppingCart/AddToCart/' + $(this).data('id'))
            .done(function () {
                row
                    .clone()
                    .css({
                        position: 'fixed',
                        left: row.offset().left,
                        top: row.offset().top
                    })
                    .appendTo($('body'))
                    .animate({
                        left: 0,
                        top: 0,
                        opacity: 0
                    }, 2000, function () {
                        $(this).detach();
                    });
                $.getJSON('/ShoppingCart/Count', function (data) {
                    $('#num-items-in-cart').text(data.count);
                });
            })
            .fail(function () {
                window.alert('An error occurred. Please try again.');
            });
        return false;
    });
});