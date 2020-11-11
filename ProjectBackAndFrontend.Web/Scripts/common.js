var $loadingTable = '<img class="preloader__table" src="../img/loader.gif" id="loader">';
var $loadingModal = '<img class="preloader__modal" src="../img/loader.gif" id="loader">';
var $loadingSave = '<img class="preloader__save" src="../img/loader.gif" id="loader">';
var $modalConstructor = '<div id="{{modal}}" class="modal" role="dialog"></div>';

//table

function fetchTable(model) {
    $(model.element).prepend($loadingTable);

    $.ajax({
        url: model.url,
        type: model.method,
        data: { page: model.page },
        dataType: 'html',
        success: function (data) {
            if (data != null) {
                $(model.element).html(data);
            }
            else {
                messagesShow('Ошибка!');
            }
        },
        error: function () {
            messagesShow('Ошибка! Пожалуйста попробуйте ещё раз.')
        }
    }).done(function () {
        $('#loader').remove();
    })
}

function tablePaging(element, fetchTableFn) {
    $(element).on('click', '.pager a', function (e) {
        e.preventDefault();
        var page = parseInt($(this).html());
        fetchTableFn(page);
    })
}

//crud

function save(model) {
    $(model.modalName + ' .modal-footer').prepend($loadingSave);
    var formData = $(model.form).serialize();

    $.ajax({
        url: model.url,
        type: 'POST',
        dataType: 'json',
        data: formData,
        success: function (data) {
            if (data.result == true) {
                messagesShow('Сохранено успешно.');
                modalClose(model.modalName);
                model.fetchTableFn(model.pageTable);
            }
            else {
                messagesShow(data.errorMessage);
            }
        },
        error: function (data) {
            if (data.result == false) {
                messagesShow('Ошибка!');
            }
        }
    }).done(function () {
        $('#loader').remove();
    })
}

function deleteData(model) {
    $.ajax({
        url: model.url,
        type: 'POST',
        dataType: 'json',
        data: { Id: model.id },
        success: function (data) {
            if (data.result == true) {
                messagesShow('Успешно удалено.');
                model.fetchTableFn(model.page);
            }
            else {
                messagesShow(data.errorMessage);
            }
        },
        error: function (data) {
            if (data.result == false) {
                messagesShow('Ошибка!');
            }
        }
    }).done(function () {
        $('#loader').remove();
    })
}

//modal

function createModal(model) {
    $('body').append($modalConstructor.replace('{{modal}}', model.modalId));

    $('#' + model.modalId).modal('show').prepend($loadingModal);

    fetchModal(model);
}

function fetchModal(model) {
    var options = {
        url: model.url,
        type: model.method,
        dataType: 'html',
        success: function (data) {
            if (data != null) {
                $('#' + model.modalId).html(data);
            }
            else {
                messagesShow('Ошибка!');
            }
        },
        error: function () {
            messagesShow('Ошибка! Пожалуйста попробуйте ещё раз.')
        }
    };

    if (model.id != null) {
        options.data = { Id: model.id };
    }

    $.ajax(options).done(function () {
        $('#loader').remove();
        if (model.fetchTableFn != null) {
            model.fetchTableFn(model.page ?? 1);
        }
    });
}

function modalClose(modalName) {
    $(modalName).modal('hide');
    $(modalName).remove();
}

//toater

var toasterIndex = 0;

async function toaster(html, index) {
    $('body').append(html);

    var toastHtml = $('#toaster' + index);
    if (index > 1) {
        toastHtml.css('bottom', 40 * index);
    }
    toastHtml.css('display', 'block');
    toastHtml.css('opacity', 1);

    setTimeout(function () {
        toastHtml.css('opacity', 0);
        toastHtml.css('display', 'none');
        $('#toaster' + index).remove();
        toasterIndex--;
    }, 5000);
}

function messagesShow(message) {
        var constructorMessages =
                '<div class="toast" id="toaster{{index}}">' +
                      '<div class="toast-body">' +
                            '{{message}}' +
                      '</div>' +
                '</div>';

    if (typeof message != 'string') {
        message.forEach(function (element, index) {
            toasterIndex++;
            var toasterContent = constructorMessages.replace('{{index}}', toasterIndex).replace('{{message}}', element);
            toaster(toasterContent, toasterIndex);
        });
    }
    else {
        toasterIndex++;
        var toasterContent = constructorMessages.replace('{{index}}', toasterIndex).replace('{{message}}', message != null && message != '' ? message : 'Ошибка');
        toaster(toasterContent, toasterIndex);
    }
}