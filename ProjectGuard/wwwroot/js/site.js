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

    $(function () {
        $(":checkbox").change(function () {
            $(this).children(':checkbox').attr('checked', this.checked);
        });
    });

    $(function () {
        $("input[type='checkbox']").change(function () {
            $(this).siblings('ul')
                .find("input[type='checkbox']")
                .prop('checked', this.checked);
        });
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

function deleteProject() {
    var formdata = $("#filesForm").serialize();

    $.ajax({
        url: "/Project/DeleteProject",
        type: "POST",
        dataType: 'text',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: formdata,

        success: function () {
            window.location.href = "/";
        }
    });
};

function selectCheckboxes(id, checked) {
    //var checkboxes = document.querySelectorAll('input[type="checkbox"]');
    //for (var i = 0; i < checkboxes.length; i++) {
    //    if (checkboxes[i] != checked && checkboxes[i].id == id)
    //        checkboxes[i].checked = checked;
    //}
    var test = $(this).find("input:checkbox");
}

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

function addToggleToCarets() {
    var toggler = document.getElementsByClassName("caret");
    var drops = document.getElementsByClassName("custom-drop");
    var i;
    for (var i = 0; i < toggler.length; i++) {
        toggler[i].addEventListener("click", function () {
            this.parentElement.querySelector(".nested").classList.toggle("active");
            this.classList.toggle("caret-down");
        });
    }
}

