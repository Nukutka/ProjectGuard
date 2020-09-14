$(document).ready(function () {
    $('.list-group-item').click(function () {
        $.ajax(
            {
                url: 'Project/SelectProject', // Как сделать тут переменную шарпа с путем ????!
                type: 'GET',
                data: { projectId: $(this).val() },

                success: function (partialView) {
                    $('#projectFilesPart').html(partialView);
                    $('#projectFilesPart').show();
                }
            });
    });
});

function hashFile() {
    var formdata = $("#filesForm").serialize();

    $.ajax({
        url: "/Hash/HashFiles",
        type: "POST",
        dataType: 'text',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: formdata,

        success: function (partialView) {
            $('#projectFilesPart').html(partialView);
            $('#projectFilesPart').show();
        }
    });
};

