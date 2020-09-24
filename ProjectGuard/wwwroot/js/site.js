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

$(function () {
    $(":checkbox").change(function () {
        $(this).children(':checkbox').attr('checked', this.checked);
    });
});

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

