var categorySelect = $('#CategoryId');

$(document).ready(function () {

    categorySelect.change(function() {
        getProperties($(this).val());

    });

});

function getProperties(categoryId) {

    $.ajax({
        type: 'GET',
        url: '/api/properties/' + categorySelect.val(),
        dataType: 'json',
        success: function (data) {
            container.html('');
            data.forEach(function (property) {
                renderRow(property['id'], property['name']);
            });
        }
    });

}

function renderRow(id, name) {
    var row = "<div class='col-6'>";
    row = row + "<label>" + name + "</label>";
    row = row + "<input type='text' id='" + id + "'>";
    row = row + "</div>";
}