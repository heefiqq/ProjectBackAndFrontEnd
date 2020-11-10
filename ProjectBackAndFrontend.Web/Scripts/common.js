var $loading = '<div id="loader">Loading...</div>';

//table

function fetchTable(url, method, page, element) {
    $(element).prepend($loading);

    $.ajax({
        url: url,
        type: method,
        data: { page: page },
        dataType: 'html',
        success: function (data) {
            if (data != null) {
                $(element).html(data);
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

//crud

function save(url, form, modalName, fetchTableFn, element = 'body') {
    $(element).prepend($loading);
    var formData = $(form).serialize();

    $.ajax({
        url: url,
        type: 'POST',
        dataType: 'json',
        data: formData,
        success: function (data) {
            if (data.result == true) {
                messagesShow('Сохранено успешно.');
                modalClose(modalName);
                fetchTableFn(currentPage);
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

function fetchModal(url, method, modalId, id = 0, element = 'body') {
    $(element).prepend($loading);

    var options = {
        url: url,
        type: method,
        dataType: 'html',
        success: function (data) {
            if (data != null) {
                $(element).append(data);
            }
            else {
                messagesShow('Ошибка!');
            }
        },
        error: function () {
            messagesShow('Ошибка! Пожалуйста попробуйте ещё раз.')
        }
    };

    if (id != 0) {
        options.data = { Id: id };
    }

    $.ajax(options).done(function () {
        $('#loader').remove();
        $('#' + modalId).modal('show');
    })
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
    }, 2000);
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