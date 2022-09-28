function FormataCurrency() {
    $('.price').priceFormat({
        prefix: '',
        centsSeparator: ',',
        thousandsSeparator: '.'
    });
}

function UpdateModalConfig() {

    $(document).on('show.bs.modal', '.modal', function () {
        var zIndex = 1040 + (10 * $('.modal:visible').length);
        $(this).css('z-index', zIndex);
        setTimeout(function () {
            $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
        }, 0);
    });

    $(document).on('hidden.bs.modal', '.modal', function () {
        $('.modal:visible').length && $(document.body).addClass('modal-open');
    });
}

$(document).ready(function () {
    $("#loaderbody").addClass('hide');

    $(document).bind('ajaxStart', function () {
        $("#loaderbody").removeClass('hide');
    }).bind('ajaxStop', function () {
        $("#loaderbody").addClass('hide');
    });

    UpdateModalConfig();

    jQueryModalGet = (e, url, title, modalId) => {
        try {
            var reloadElementId = $(e).attr("data-element-reload");
            if ($("#" + modalId).length == 0)
                $("body").append(ModalFormTemplate(modalId));
            $.ajax({
                type: 'GET',
                url: url,
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#' + modalId + ' .modal-body').html(res);
                    $('#' + modalId + ' .modal-title').html(title);
                    $('#' + modalId + '').modal('show');
                    $('#' + modalId + '').attr("data-element-reload", reloadElementId);
                },
                error: function (err) {
                    console.log("jQueryModalGet");
                    console.log(err);
                }
            });
            return false;
        } catch (ex) {
            console.log("jQueryModalGet");
            console.log(ex);
            return false;
        }
        return false;
    }
    jQueryModalPost = form => {
        try {
            if (!$(form).valid()) return false;
            var modalId = $(form).closest(".modal").attr("id");
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {
                        $('#' + modalId + ' .modal-body').html('');
                        $('#' + modalId + ' .modal-title').html('');
                        $('#' + modalId + '').modal('hide');
                        window['jQueryModalPostExecute_' + modalId](res, modalId);
                    }
                    else
                        $('#' + modalId + ' .modal-body').html(res.html);
                },
                error: function (err) {
                    console.log(err);
                }
            });
            return false;
        } catch (ex) {
            console.log(ex);
        }
    }
    jQueryModalDelete = form => {
        try {
            var modalId = $(form).closest(".modal").attr("id");
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {
                        $('#' + modalId + ' .modal-body').html('');
                        $('#' + modalId + ' .modal-title').html('');
                        $('#' + modalId + '').modal('hide');
                        window['jQueryModalDeleteExecute_' + modalId](res, modalId);
                    }
                    else
                        $('#' + modalId + ' .modal-body').html(res.html);
                },
                error: function (err) {
                    console.log(err);
                }
            })
        } catch (ex) {
            console.log(ex);
        }
        return false;
    }

    function ModalFormTemplate(modalId) {
        var template = `
<div id="[id]" class="modal fade" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
    <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title"></h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
            </div>
        </div>
    </div>
</div>`;
        return template.replace("[id]", modalId);
    }
});