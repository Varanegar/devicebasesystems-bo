$(document).ready(function () {
    accountManagerApp.callApi(urls.loadcompany, 'POST',
    {},
    function (data) {
        $.each(data, function (i, item) {
            $("#ddl_company").append
                ('<option value="' + item.uniqueId + '">' + item.companyName + '</option>');
        });
        $("#ddl_company").val($.cookie("selectedCompanyId"));
    },false);

    $("#ddl_company").on("change", function (e) {
        $.cookie("selectedCompanyId", $("#ddl_company").val());
    });
});