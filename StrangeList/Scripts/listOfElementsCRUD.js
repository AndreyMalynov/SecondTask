
$(document).ready(function () {
    GetAllElementsOfList();
    $("#editElementOfList").click(function (event) {
        event.preventDefault();
        EditElementOfList();
    });

    $("#addElementOfList").click(function (event) {
        event.preventDefault();
        AddElementOfList();
    });
});

function GetAllElementsOfList() {
    $.ajax({
        url: '/api/values',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            WriteResponse(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}


function AddElementOfList() {
    var strName = $('#addName').val();
    var finalName = strName.slice(0, strName.length - 1) + strName[strName.length - 1].toUpperCase();
    var elementOfList = {
        Name: finalName,
        Score: $('#addScore').val()
    };

    $.ajax({
        url: '/api/values/',
        type: 'POST',
        data: JSON.stringify(elementOfList),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllElementsOfList();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function DeleteElementOfList(id) {

    $.ajax({
        url: '/api/values/' + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllElementsOfList();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}


function EditElementOfList() {
    var id = $('#editId').val()
    var strName = $('#editName').val();
    var finalName = strName.slice(0, strName.length - 1) + strName[strName.length - 1].toUpperCase();
    var elementOfList = {
        Id: $('#editId').val(),
        Name: finalName,
        Score: $('#editScore').val()
    };
    $.ajax({
        url: '/api/values/' + id,
        type: 'PUT',
        data: JSON.stringify(elementOfList),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllElementsOfList();
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function WriteResponse(elementsOfList) {
    var strResult = "<table class='table'><th>ID</th><th>Имя</th><th>Оценка</th><th></th><th></th>";
    $.each(elementsOfList, function (index, elementOfList) {
        strResult += "<tr><td>" + elementOfList.Id + "</td><td> " + elementOfList.Name + "</td><td>" + elementOfList.Score +
        "</td><td><div class='edit-icon' id='editItem' data-item='" + elementOfList.Id + "' onclick='EditItem(this);' ></div></td>" +
        "<td><div class='delete-icon' id='delItem' data-item='" + elementOfList.Id + "' onclick='DeleteItem(this);' ></div></td></tr>";
    });
    strResult += "</table>";
    $("#tableBlock").html(strResult);

}


function DeleteItem(el) {
    var id = $(el).attr('data-item');
    DeleteElementOfList(id);
}


function EditItem(el) {
    var id = $(el).attr('data-item');
    GetElementOfList(id);
    (function (e) {
        $('.edit-element .popup, .overlay').css({ 'opacity': 1, 'visibility': 'visible' });
        e.preventDefault();
    }());

}

function ShowElementOfList(elementOfList) {
    if (elementOfList != null) {
        $("#editId").val(elementOfList.Id);
        $("#editName").val(elementOfList.Name);

        $("#editScore").val(elementOfList.Score);
    }
    else {
        alert("Элемент не существует");
    }
}

function GetElementOfList(id) {
    $.ajax({
        url: '/api/values/' + id,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            ShowElementOfList(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

$('.popup .close_window, .overlay, #editElementOfList, #addElementOfList').click(function () {
    $('.popup, .overlay').css({ 'opacity': 0, 'visibility': 'hidden' });
});
$('button.open_window').click(function (e) {
    $('.create-element .popup, .overlay').css({ 'opacity': 1, 'visibility': 'visible' });
    e.preventDefault();
});
