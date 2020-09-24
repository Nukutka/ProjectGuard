$(document).ready(function () {
    $(".caret").each(function () {
        var liId = "#" + $(this).prop('id').replace('caret', 'li');
        $(this).click(function () {
            $(liId).find(".nested:first").each(function () {
                $(this).toggleClass("active");
            })

            $(this).toggleClass("caret-down");
        });
    });

    $(".nested").each(function () {
        var array = [];
        var checkboxId = $(this).prop('id').replace('nested', '');

        $(this).find(':checkbox').each(function () {
            if ($(this).prop('class') == 'not-directory') {
                array.push($(this).prop('checked'));
            }
        });

        if (array.every(a => a == true)) {
            $("#" + checkboxId).prop('checked', true);
        }
    });
});

function setChildCheckboxes(id, checked) {
    var nestedId = "#nested" + id;
    var array = [];

    $(nestedId).find(":checkbox").each(function () {
        $(this).prop('checked', checked);
        if ($(this).prop('class') == 'not-directory') {
            array.push({ fileId: parseInt($(this).prop('value')), needHash: $(this).prop('checked') });
        }
    });

    changeFilesNeedHash(array);
}

function hashFiles() {
    var projectId = $("#filesForm").children('#projectId').prop('value');

    $.ajax({
        url: "/Hash/HashFiles",
        type: "POST",
        dataType: 'text',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { projectId: projectId },

        success: function (partialView) {
            $('#projectFilesPart').html(partialView);
            $('#projectFilesPart').show();
        }
    });
};

function checkFiles() {
    var projectId = $("#filesForm").children('#projectId').prop('value');

    $.ajax({
        url: "/Hash/CheckFiles",
        type: "POST",
        dataType: 'text',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { projectId: projectId },

        success: function (partialView) {
            $('#projectFilesPart').html(partialView);
            $('#projectFilesPart').show();
        }
    });
};

function deleteProject() {
    var projectId = $("#filesForm").children('#projectId').prop('value');

    $.ajax({
        url: "/Project/DeleteProject",
        type: "POST",
        dataType: 'text',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { projectId: projectId },

        success: function () {
            window.location.href = "/";
        }
    });
};

function changeFileNeedHash(fileId, needHash) {
    $.ajax({
        url: "/Hash/ChangeNeedHash",
        type: "POST",
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: { fileId, needHash }
    });
}

function changeFilesNeedHash(model) {
    $.ajax({
        url: "/Hash/ChangeNeedHashs",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(model),
        dataType: "json",
    });
}