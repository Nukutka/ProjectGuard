$(document).ready(function () {
    $('.li-clickable').click(function () {
        $.ajax(
            {
                url: 'Project/SelectProject',
                type: 'GET',
                data: { projectId: $(this).val() },

                success: function (partialView) {
                    $('#projectFilesPart').html(partialView);
                    $('#projectFilesPart').show();
                }
            });
    });
});



