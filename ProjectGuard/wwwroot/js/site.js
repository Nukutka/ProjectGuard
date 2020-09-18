$(document).ready(function () {
    $('.li-clickable').click(function () {
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

    $('.dropdown').click(function () {
        $(this).nextUntil('.dropdown').slideToggle('normal');
    });
});

function hashFiles() {
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

function checkFiles() {
    var formdata = $("#filesForm").serialize();

    $.ajax({
        url: "/Hash/CheckFiles",
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

function selectCheckboxes(id, checked) {
    var checkboxes = document.querySelectorAll('input[type="checkbox"]');
    for (var i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i] != checked && checkboxes[i].id == id)
            checkboxes[i].checked = checked;
    }
}

function changeFileNeedHash(fileId, needHash) {
    $.ajax({
        url: "/Hash/ChangeNeedHash",
        type: "POST",
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { fileId, needHash }
    });
}

