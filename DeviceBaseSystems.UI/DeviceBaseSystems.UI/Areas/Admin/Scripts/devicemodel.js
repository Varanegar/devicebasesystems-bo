"use strict";

$(document).ready(function() {
    $("#grd_device_model").kendoGrid({
        autoBind: true,
        dataSource: {
            transport: {
                read: loadDeviceModes
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
        { field: "brandName", title: 'برند' },
        { field: "deviceCode", title: 'کد' },
        { field: "deviceName", title: 'عنوان' },
        { field: "uniqueId", title: "&nbsp; &nbsp;",
            attributes: { style: "width:50px;" },
            template:
            "<button  type='button' class='btn-link btn-grid' onclick=openDeviceModelDialog('#=uniqueId#');><span class='glyphicon glyphicon-pencil color-gray span-btn-grid'></span ></button>" +
            "<button  type='button' class='btn-link btn-grid' onclick=removeDeviceModel('#=uniqueId#');><span class='glyphicon glyphicon-trash color-gray span-btn-grid'></span ></button>"
        }
        ]
    });


    $("#btn_device_mode_add").on("click", function (e) {
        openDeviceModelDialog(null);
    });

    $("#btn_save_device_model").on("click", function (e) {
        var desc = '';
        if ($("#txt_code").val() == '')
            desc = 'لطفا کد را وارد کنید. ';
        else if ($("#txt_title").val() == '')
            desc += 'لطفا عنوان را وارد کنید. ';            
        else if ($("#ddl_brand").val() == '')
            desc += 'لطفا برند را وارد کنید. ';

        if (desc != '') {
            alert(desc);
        } else {
            accountManagerApp.callApi(urls.savedevicemodel, 'POST',
             getData(),
            function (result) {
                $('#grd_device_model').data('kendoGrid').dataSource.read();
                $('#grd_device_model').data('kendoGrid').refresh();
                closeDeviceModelDialog();
            });                     
        }       

    });

    $("#btn_cancel_device_model").on("click", function (e) {
        $("#dlg_device_model").modal('hide');

    });

});

//-------------------------------------------------------------------------------------------------
//
//-------------------------------------------------------------------------------------------------
function loadDeviceModes(options) {
    accountManagerApp.callApi(urls.loaddevicemodel, 'POST',
        {},
        function (result) {
            options.success(result);
        });
}

function getData() {
    return {
        deviceModelData: {
            uniqueId: $("#hdn_id").val(),
            deviceCode: $("#txt_code").val(),
            deviceName: $("#txt_title").val(),
            brandId: $("#ddl_brand").val()
        }
    };
}

function openDeviceModelDialog(id) {
    loadDdlBrand();
    if (id != null) {
        $("#hdn_id").val(id);
        accountManagerApp.callApi(urls.getdevicemodel, 'POST',
            {uniqueId : id},
            function (result) {
                if (result != null) {
                    $("#txt_code").val(result.deviceCode);
                    $("#txt_title").val(result.deviceName);
                    $("#ddl_brand").data("kendoComboBox").value(result.brandId);
                }
            });
    }
    else $("#hdn_id").val('');


    $("#dlg_device_model").modal('show');
}
function closeDeviceModelDialog() {
    $("#hdn_id").val('');
    $("#txt_code").val('');
    $("#txt_title").val('');
    $("#ddl_brand").val('');
    $("#dlg_device_model").modal('hide');
}


function removeDeviceModel(id) {
    showQuestion("ایا اطلاعات موردنظر حذف شود؟","حذف مدل دستگاه",
    function () {
        accountManagerApp.callApi(urls.removedevicemodel, 'POST',
        { deviceModelData: { uniqueId: id } },
        function (result) {
            $('#grd_device_model').data('kendoGrid').dataSource.read();
            $('#grd_device_model').data('kendoGrid').refresh();
        });
    });
}

function loadDdlBrand() {
    $("#ddl_brand").kendoComboBox({
        placeholder: "انتخاب کنید ..",
        dataTextField: "brandName",
        dataValueField: "uniqueId",
        filter: "contains",
        autoBind: true,
        minLength: 3,
        dataSource: {
            serverFiltering: false,
            transport: { read: loadBrand }
        }
    });
}

function loadBrand(options) {
    accountManagerApp.callApi(urls.loadbrand, 'POST', { },
        function (result) {
            options.success(result);
        });
}
