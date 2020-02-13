const ADM = "ADMIN",
    DEPTHEAD = "DEPTHEAD",
    SUP = "SUP",
    USER = "USER";

const Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,

    timer: 3000,
    customClass: {
        title: 'title-class',
        icon: 'icon-class'
    }
});

const $success = function (message, confirm = true, title = "Success!") {
    if (!confirm)
    {
        Swal.fire(
            title,
            message,
            'success'
        );
    }
    else
    {
        Toast.fire({
            title: message,
            type: 'success'
        });
    }
    
};


const $error = function (message, confirm = true,title = "Error!" ) {
    if (!confirm) {
        Swal.fire(
            title,
            message,
            'error'
        );
    }
    else {
        Toast.fire({
            title: message,
            type: 'error'
        });
    }
};

const $warning = function (message, confirm = true, title = "Warning!") {
    if (!confirm) {
        Swal.fire(
            title,
            message,
            'warning'
        );
    }
    else {
        Toast.fire({
            title: message,
            type: 'warning'
        });
    }
};
const $info = function (message, confirm = true, title = "Info!") {
    if (!confirm) {
        Swal.fire(
            title,
            message,
            'info'
        );
    }
    else {
        Toast.fire({
            title: message,
            type: 'info'
        });
    }
};
const $question = function (message, confirm = true, title = "Question!") {
    if (!confirm) {
        Swal.fire(
            title,
            message,
            'question'
        );
    }
    else {
        Toast.fire({
            title: message,
            type: 'question'
        });
    }
};
///
////http
const $post = function (url, data) {
    return new Promise(function (res, rej) {
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            contentType: "application/json;charset=utf-8",
            dataType: "json"
        })
            .done(function (data) {
                res(data);
            }).fail(function (jqXHR, textStatus, errorThrown) {
                rej(textStatus);
            });
    });
};
const $get = function (url, data) {
    return new Promise(function (res, rej) {
        $.ajax({
            type: 'GET',
            url: url,
            data: data,
            contentType: "application/json;charset=utf-8",
            dataType: "json"
        })
            .done(function (data) {
                res(data);

            }).fail(function (jqXHR, textStatus, errorThrown) {
                rej(textStatus);
            });
    });
};