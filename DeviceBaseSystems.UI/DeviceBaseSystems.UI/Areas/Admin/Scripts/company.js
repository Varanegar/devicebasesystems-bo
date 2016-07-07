"use strict";

$(document).ready(function() {
    $("#grd_company").kendoGrid({
        autoBind: true,
        dataSource: {
            transport: {
                read: loadCompanies
            },
            pageSize: 30,
            serverPaging: false,
            //serverFiltering: true,
            //serverSorting: true
        },

        sortable: false,
        editable: false,
        selectable: "row",
        pageable: false,
        scrollable: true,
        height: 450,
        columns: [
        { field: "companyCode", title: 'کد' , width:100},
        { field: "companyName", title: 'عنوان' },
        { field: "uniqueId", title: "&nbsp; &nbsp;", width:100,
            attributes: { style: "width:50px;" },
            template:
            "<button  type='button' class='btn-link btn-grid' onclick=openCompanyModelDialog('#=uniqueId#');><span class='glyphicon glyphicon-pencil color-gray span-btn-grid'></span ></button>" +
            "<button  type='button' class='btn-link btn-grid' onclick=removeCompanyModel('#=uniqueId#');><span class='glyphicon glyphicon-trash color-gray span-btn-grid'></span ></button>"
        }
        ]
    });


    $("#btn_company_add").on("click", function (e) {
        openCompanyModelDialog(null);
    });

    $("#btn_save_company").on("click", function (e) {
        var desc = '';
        if ($("#txt_code").val() == '')
            desc = 'لطفا کد را وارد کنید. ';
        else if ($("#txt_title").val() == '')
            desc += 'لطفا عنوان را وارد کنید. ';            

        if (desc != '') {
            alert(desc);
        } else {
            accountManagerApp.callApi(urls.savecompany, 'POST',
             getData(),
            function (result) {
                $('#grd_company').data('kendoGrid').dataSource.read();
                $('#grd_company').data('kendoGrid').refresh();
                closeCompanyModelDialog();
            });                     
        }       

    });

    $("#btn_cancel_company").on("click", function (e) {
        $("#dlg_company").modal('hide');

    });

});

//-------------------------------------------------------------------------------------------------
//
//-------------------------------------------------------------------------------------------------
function loadCompanies(options) {
    accountManagerApp.callApi(urls.loadcompany, 'POST',
        {},
        function (result) {
            options.success(result);
        });
}

function getData() {
    return {
        companyData: {
            uniqueId: $("#hdn_id").val(),
            companyCode: $("#txt_code").val(),
            companyName: $("#txt_title").val()
        }
    };
}

function openCompanyModelDialog(id) {
    if (id != null) {
        $("#hdn_id").val(id);
        accountManagerApp.callApi(urls.getcompany, 'POST',
            {uniqueId : id},
            function (result) {
                if (result != null) {
                    $("#txt_code").val(result.companyCode);
                    $("#txt_title").val(result.companyName);
                }
            });
    }
    else $("#hdn_id").val('');
    $("#dlg_company").modal('show');
}
function closeCompanyModelDialog() {
    $("#hdn_id").val('');
    $("#txt_code").val('');
    $("#txt_title").val('');
    $("#dlg_company").modal('hide');
}


function removeCompanyModel(id) {
    showQuestion("ایا اطلاعات موردنظر حذف شود؟","حذف شرکت",
    function () {
        accountManagerApp.callApi(urls.removecompany, 'POST',
        { companyData: { uniqueId: id } },
        function (result) {
            $('#grd_company').data('kendoGrid').dataSource.read();
            $('#grd_company').data('kendoGrid').refresh();
        });
    });
}
