var $loadingTable = '<img class="preloader__table" src="../img/loader.gif" id="loaderTable">';
var $loadingModal = '<img class="preloader__modal" src="../img/loader.gif" id="loaderModal">';
var $loadingSave = '<img class="preloader__save" src="../img/loader.gif" id="loaderSave">';
var $modalConstructor = '<div id="{{modal}}" class="modal" role="dialog"></div>';

//table

function fetchTable(model) {
    $(model.element).prepend($loadingTable);

    $.ajax({
        url: model.url,
        type: model.method,
        data: model.data,
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
        $($loadingTable).remove();
    })
}

function tablePaging(element, fetchTableFn, params = {}) {
    $(element).on('click', '.pager a', function (e) {
        e.preventDefault();
        params.page = parseInt($(this).html());
        fetchTableFn(params);
    })
}

//crud

function save(model) {
    $(model.modalName + ' .modal-footer').prepend($loadingSave);
    var formData = model.data ?? $(model.form).serialize();

    $.ajax({
        url: model.url,
        type: 'POST',
        dataType: 'json',
        data: formData,
        success: function (data) {
            if (data.result == true) {
                messagesShow('Сохранено успешно.');
                modalClose(model.modalName);
                model.fetchTableFn(model.params);
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
        $($loadingSave).remove();
    })
}

function deleteData(model) {
    $(model.element).prepend($loadingTable);

    $.ajax({
        url: model.url,
        type: 'POST',
        dataType: 'json',
        data: { Id: model.id },
        success: function (data) {
            if (data.result == true) {
                messagesShow('Успешно удалено.');
                model.fetchTableFn(model.params);
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
        $($loadingTable).remove();
    })
}

//modal

async function createModal(model) {
    $('body').append($modalConstructor.replace('{{modal}}', model.modalId));

    $('#' + model.modalId).modal('show').prepend($loadingModal);

    fetchModal(model);
}

function fetchModal(model) {
    $.ajax({
        url: model.url,
        type: model.method,
        dataType: 'html',
        data: model.data,
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
    }).done(function () {
        $($loadingModal).remove();
        if (model.onModalLoad != null) {
            model.onModalLoad();
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

//validate

function validateNum(e) {
    var charCode = (e.which) ? e.which : e.keyCode;
    if (charCode != 46 && charCode != 48 && charCode != 49 && charCode != 50 && charCode != 51 && charCode != 52 && charCode != 53 && charCode != 54 && charCode != 55 && charCode != 56 && charCode != 57 && charCode != 8)
        return false;

    var elem = $(e.target);

    var elemValue = elem.val().replace(/\s+/g, '');
    if (elemValue.indexOf('.') > 0 && charCode == 46)
        return false;

    if (elemValue == '' && charCode == 46) {
        elem.val('0');
        return true;
    }
}

//mask

function maskNum(e) {
    var elem = $(e.target);

    var elemValue = elem.val().replace(/\s+/g, '');

    var index = elemValue.indexOf('.');

    if ((elemValue.length - 1) == index)
        return;

    if ((elemValue.length - index) > 3) {
        var val = '';
        for (var i = 0; elemValue.length > i; i++) {
            if (elemValue[i] == '.') {
                val += elemValue[i + 1];
                val += '.';
                i++;
            } else {
                val += elemValue[i];
            }
        }
        elemValue = val;
    }

    var floatValue = parseFloat((elemValue));

    elem.val(floatValue.toLocaleString('ru').replace(',', '.'));
    return;
}