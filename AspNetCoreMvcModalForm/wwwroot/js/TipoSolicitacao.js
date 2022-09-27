var tableTipoSolicitacao;

$(function () {
    LoadTableTipoSolicitacao();
});

function jQueryModalPostExecute_modalTipoSolicitacao(res, modalId) {
    tableTipoSolicitacao.ajax.reload();
    var element = $("#" + modalId).attr("data-element-reload");
    if (element !== undefined) {
        var elementType = $(element).prop('nodeName');
        if (elementType == "SELECT") {
            $(element).append('<option value="' + res.model.id + '">' + res.model.descricao + '</option>');
        }
    }
}
function jQueryModalDeleteExecute_modalTipoSolicitacao(res, modalId) {
    tableTipoSolicitacao.ajax.reload();
    var element = $("#" + modalId).attr("data-element-reload");
    if (element !== undefined) {
        var elementType = $(element).prop('nodeName');
        if (elementType == "SELECT") {
            $(element).find("option[value='" + res.model.id + "']").remove();
        }
    }
}

function LoadTableTipoSolicitacao() {
    tableTipoSolicitacao = $("#tableTipoSolicitacao").DataTable({
        processing: false,
        serverSide: true,
        filter: true,
        destroy: true,
        responsive: true,
        ajax: {
            url: "/TipoSolicitacao/GetDataTable",
            type: "POST",
            datatype: "json",
            data: {
                '__RequestVerificationToken': $("#requestToken").val()
            }
        },
        columns: [
            { data: "descricao", autoWidth: true },
            {
                data: null, className: "text-center",
                render: function (data, row) {
                    var modalDetail = "jQueryModalGet(this, '/TipoSolicitacao/Details/" + data.id + "', 'Delathes do tipo da solicitação', 'modalTipoSolicitacao')";
                    var modalEdit = "jQueryModalGet(this, '/TipoSolicitacao/Edit/" + data.id + "', 'Atualizar tipo de solicitação', 'modalTipoSolicitacao')";
                    var modalDelete = "jQueryModalGet(this, '/TipoSolicitacao/Delete/" + data.id + "', 'Excluir tipo de solicitação', 'modalTipoSolicitacao')";
                    return '<a onclick="' + modalDetail + '" class="ml-1 mr-1"><i class="far fa-sticky-note"></i></a>' +
                        '<a onclick="' + modalEdit + '" class="ml-1 mr-1"><i class="far fa-edit"></i></a>' +
                        '<a onclick="' + modalDelete + '" class="ml-1 mr-1"><i class="far fa-trash-alt"></i></a>';
                }
            }
        ],
        createdRow: function (row, data, dataIndex) {
            $(row).attr('data-id', data.id);
        }
    });
}