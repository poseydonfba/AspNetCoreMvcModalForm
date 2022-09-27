var tableSolicitacao;

$(function () {
    LoadTableSolicitacao();
});

function jQueryModalPostExecute_modalSolicitacao(res, modalId) {
    tableSolicitacao.ajax.reload();
    var element = $("#" + modalId).attr("data-element-reload");
    if (element !== undefined) {
        var elementType = $(element).prop('nodeName');
        if (elementType == "SELECT") {
            $(element).append('<option value="' + res.model.id + '">' + res.model.descricao + '</option>');
        }
    }
}
function jQueryModalDeleteExecute_modalSolicitacao(res, modalId) {
    tableSolicitacao.ajax.reload();
    var element = $("#" + modalId).attr("data-element-reload");
    if (element !== undefined) {
        var elementType = $(element).prop('nodeName');
        if (elementType == "SELECT") {
            $(element).find("option[value='" + res.model.id + "']").remove();
        }
    }
}

function LoadTableSolicitacao() {
    tableSolicitacao = $("#tableSolicitacao").DataTable({
        processing: false,
        serverSide: true,
        filter: true,
        destroy: true,
        responsive: true,
        ajax: {
            url: "/Solicitacao/GetDataTable",
            type: "POST",
            datatype: "json",
            data: {
                '__RequestVerificationToken': $("#requestToken").val()
            }
        },
        columns: [
            { data: "descricao", autoWidth: true },
            {
                data: "data", autoWidth: true, className: "text-center",
                render: function (data, row) {
                    return moment(data).format("L LTS");
                }
            },
            { data: "quantidade", autoWidth: true, className: "text-center" },
            {
                data: "valor", autoWidth: true, className: "text-right",
                render: function (data, row) {
                    return numeral(data).format("$0,0.00");
                }
            },
            { data: "tipoSolicitacao.descricao", autoWidth: true, className: "text-center" },
            {
                data: null, className: "text-center",
                render: function (data, row) {
                    var modalDetail = "jQueryModalGet(this, '/Solicitacao/Details/" + data.id + "', 'Delathes da solicitação', 'modalSolicitacao')";
                    var modalEdit = "jQueryModalGet(this, '/Solicitacao/Edit/" + data.id + "', 'Atualizar solicitação', 'modalSolicitacao')";
                    var modalDelete = "jQueryModalGet(this, '/Solicitacao/Delete/" + data.id + "', 'Excluir solicitação', 'modalSolicitacao')";
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