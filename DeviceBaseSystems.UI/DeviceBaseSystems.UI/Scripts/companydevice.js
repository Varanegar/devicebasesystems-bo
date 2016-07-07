"use strict";

$(document).ready(function() {
    $("#grd_company_device").kendoGrid({
        autoBind: true,
        dataSource: {
            transport: {
                read: loadCompanyDevices
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
        { field: "deviceModelName", title: 'مدل' },
        { field: "imei", title: 'کد' },
        { field: "description", title: 'توضیحات' },
        { field: "uniqueId", title: "&nbsp; &nbsp;",
            attributes: { style: "width:50px;" },
            template:
            "<button  type='button' class='btn-link btn-grid' onclick=openCompanyDeviceDialog('#=uniqueId#');><span class='glyphicon glyphicon-pencil color-gray span-btn-grid'></span ></button>" +
            "<button  type='button' class='btn-link btn-grid' onclick=removeCompanyDevice('#=uniqueId#');><span class='glyphicon glyphicon-trash color-gray span-btn-grid'></span ></button>"
        }
        ]
    });


    $("#btn_company_device_add").on("click", function (e) {
        openCompanyDeviceDialog(null);
    });

    $("#btn_save_company_device").on("click", function (e) {
        var desc = '';
        if ($("#txt_code").val() == '')
            desc = 'لطفا کد را وارد کنید. ';
        else if ($("#ddl_device_model").val() == '')
            desc += 'لطفا مدل را انتخاب کنید. ';

        if (desc != '') {
            alert(desc);
        } else {
            accountManagerApp.callApi(urls.savecompanydevice, 'POST',
            getData(),
            function (result) {
                $('#grd_company_device').data('kendoGrid').dataSource.read();
                $('#grd_company_device').data('kendoGrid').refresh();
                closeCompanyDeviceDialog();
            });                     
        }       

    });

    $("#btn_cancel_company_device").on("click", function (e) {
        $("#dlg_company_device").modal('hide');

    });
    
    $("#ddl_company").on("change", function (e) {
        $.cookie("selectedCompanyId", $("#ddl_company").val());
        $('#grd_company_device').data('kendoGrid').dataSource.read();
        $('#grd_company_device').data('kendoGrid').refresh();
    });
});

//-------------------------------------------------------------------------------------------------
//
//-------------------------------------------------------------------------------------------------
function loadCompanyDevices(options) {
    if ($("#ddl_company").val() == '')
        alert("لطفا شرکت را انتخاب کنید.");
    else
       accountManagerApp.callApi(urls.loadcompanydevice, 'POST',
        { companyDeviceData: { companyId: $("#ddl_company").val() } },
        function (result) {
            options.success(result);
        });
}

function getData() {
    return {
        companyDeviceData: {
            uniqueId: $("#hdn_id").val(),
            companyId: $("#ddl_company").val() ,
            deviceModelId: $("#ddl_device_model").val(),
            iMEI: $("#txt_code").val(),
            description: $("#txt_desc").val()
        }
    };
}

function openCompanyDeviceDialog(id) {
    loadDdlDeviceModel();
    if (id != null) {
        $("#hdn_id").val(id);
        accountManagerApp.callApi(urls.getcompanydevice, 'POST',
            {uniqueId : id},
            function (result) {
                if (result != null) {
                    $("#txt_code").val(result.imei);
                    $("#txt_desc").val(result.description);
                    $("#ddl_device_model").data("kendoComboBox").value(result.deviceModelId);
                }
            });
    }
    else $("#hdn_id").val('');


    $("#dlg_company_device").modal('show');
}
function closeCompanyDeviceDialog() {
    $("#hdn_id").val('');
    $("#txt_code").val('');
    $("#txt_desc").val('');
    $("#ddl_device_model").val('');
    $("#dlg_company_device").modal('hide');
}


function removeCompanyDevice(id) {
    showQuestion("ایا اطلاعات موردنظر حذف شود؟","حذف دستگاه",
    function () {
        accountManagerApp.callApi(urls.removecompanydevice, 'POST',
        { companyDeviceData: { uniqueId: id } },
        function (result) {
            $('#grd_company_device').data('kendoGrid').dataSource.read();
            $('#grd_company_device').data('kendoGrid').refresh();
        });
    });
}

function loadDdlDeviceModel() {
    $("#ddl_device_model").kendoComboBox({
        placeholder: "انتخاب کنید ..",
        dataTextField: "deviceName",
        dataValueField: "uniqueId",
        filter: "contains",
        autoBind: true,
        minLength: 3,
        dataSource: {
            serverFiltering: false,
            transport: { read: loadDeviceModel}
        }
    });
}

function loadDeviceModel(options) {
    accountManagerApp.callApi(urls.loaddevicemodel, 'POST', { },
        function (result) {
            options.success(result);
        });
}
