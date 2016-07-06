$(document).ready(function () {
    kendo.culture("fa-IR");
    intDate();
});
//------------------------------------------------------
//date time
//------------------------------------------------------
function intDate() {
    $(".mydate").persianDatepicker({
        cellWidth: 48,
        cellHeight: 32,
        fontSize: 16,
        isRTL: true,
        selectedBefore: true,
        formatDate: "YYYY/0M/0D",
    });

}

//-------------------------------------------------
// question
//-------------------------------------------------

function showQuestion(message, title, yescallback, nocallback) {

    var dialog =
        '<div id="dlg_question" class="modal fade" role="dialog" >' +
            '<div class="modal-dialog" style="width:400px">' +
            '<div class="modal-content">' +
            '<div class="modal-header">' +
            '<button type="button" class="close" data-dismiss="modal">&times;</button>' +
            '<h4 class="modal-title">' + title + '</h4>' +
            '</div>' +
            '<div class="modal-body">' +
            '<h6>' + message + '</h6>' +
            '</div>' +
            '<div class="modal-footer">' +
            '<button id="btn_yes" class="btn btn-primary large-btn">بله</button>' +
            '<button id="btn_no" class="btn btn-primary large-btn">خیر</button>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>';

    $('<div></div>').appendTo('body').html(dialog);
    $("#btn_yes").on("click", function () {
        $("#dlg_question").modal("hide");
        $("#dlg_question").remove();
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
        if (yescallback != null)
            yescallback();
    });
    $("#btn_no").on("click", function () {
        $("#dlg_question").modal("hide");
        $("#dlg_question").remove();
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
        if (nocallback != null)
            nocallback();
    });

    $("#dlg_question").modal("show");
}