
errorMessage = {
    unAuthorized: "شما مجوز لازم را برای این درخواست ندارید!",
},
gridAuthHeader = function (req) {
    var tokenKey = 'accessToken',
        token = $.cookie("token");
    req.setRequestHeader('Authorization', 'Bearer ' + token);
    req.setRequestHeader('OwnerKey', privateOwnerId);
};


toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": true,
    "progressBar": true,
    "positionClass": "toast-bottom-left",
    "preventDuplicates": true,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};
var ajaxCount = 0;

var showError = function (title, message) {
    toastr["error"](title, message);
    unfreezUI();
};
var showSuccess = function (title, message) {
    toastr["success"](title, message);
};

var onRequestEnd = function(e) {
    if (e.type == "update" && !e.response.Errors)
        showSuccess('', 'اطلاعات ثبت شد.');

    if (e.type == "create" && !e.response.Errors)
        showSuccess('', 'اطلاعات ثبت شد.');
};

var rowNumber = 0;
function resetRowNumber(e) {
    rowNumber = 0;
}
function renderNumber(data) {
    return ++rowNumber;
}

var onDataBinding = function() {
    rowNumber = (this.dataSource.page() - 1) * this.dataSource.pageSize();
};
//******************************************************************//
var freezUI = function () {
    $.blockUI.defaults.css.border = '2px solid black';
    $.blockUI.defaults.css.padding = '5px 5px';
    $.blockUI.defaults.css.height = '40px';
    $.blockUI({ message: '<img src="/Content/img/loading-blue-bak.gif" height="28" width="28" />     لطفا کمی صبر نمایید...' });
    //$('#myDiv').block({ message: 'Processing...' });
};

var unfreezUI = function () {
    ajaxCount = 0;
    $.unblockUI({ fadeOut: 200 });
};
//******************************************************************//

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
};

//******************************************************************//
function shouldShowLogout(show) {
    if (show == true)
        $("#menuExit").show();
    else
        $("#menuExit").hide();
};

//******************************************************************//

function accountManagerViewModel() {
    var self = this;

    var tokenKey = 'accessToken';

    self.result = ko.observable();
    self.user = ko.observable();
    self.loginEmail = ko.observable();
    self.loginPassword = ko.observable();
    self.requestAppObject = ko.observable();

    function showAjaxError(jqXHR) {
        var title = jqXHR.status,
            message = jqXHR.statusText;

        if (jqXHR.responseJSON) {
            title = jqXHR.responseJSON.error;
            message = jqXHR.responseJSON.error_description;
        }

        if (jqXHR.status == 401) {
            title = '401';
            message = errorMessage.unAuthorized;
        }
        if (jqXHR.status == 403) {
            title = '403';
            message = errorMessage.unAuthorized;
        }
        if (jqXHR.status == 400 && jqXHR.responseJSON.modelState != undefined) {
            title = 'خطا';
            var modelState = jqXHR.responseJSON.modelState;
            var errorsString = "";
            var errors = [];
            for (var key in modelState) {
                if (modelState.hasOwnProperty(key)) {
                    errorsString = (errorsString == "" ? "" : errorsString + "<br/>") + modelState[key];
                    errors.push(modelState[key]);//list of error messages in an array
                }
            }
            message = errorsString;
        }
        showError(title, message);

        console.log(title + ': ' + message);
    }
    
    self.callApi = function(url, callType, dataParams, callBackFunc, async) {
        ajaxCount++;
        freezUI();
        if (async == undefined)  async = true;
        
        if (url == undefined || url == '')
            return;

        self.result('');

        var token = $.cookie("token"),
            headers = { ownerKey: privateOwnerId };

        if (token)
            headers.Authorization = 'Bearer ' + token;
        else {
            self.requestAppObject({ url: url, callType: callType, callBackFunc: callBackFunc });
            self.openLogin();

            return;
        }

        shouldShowLogout(true);
        //console.log(url);
        $.ajax({
            type: callType,
            url: url,
            headers: headers,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(dataParams),
            async: async
        }).done(function(data) {
            self.result(data);
            callBackFunc(data);

            ajaxCount--;
            if (ajaxCount <= 0)
                unfreezUI();

        }).fail(function(jqXHR) {
            showAjaxError(jqXHR);

            if (jqXHR.status == 401) {
                self.requestAppObject({ url: url, callType: callType, callBackFunc: callBackFunc });
                self.openLogin();
            }
            ajaxCount--;
            if (ajaxCount <= 0)
                unfreezUI();
        });
    };

    var $loginForm = $(".login-form");
    self.login = function() {
        self.result('');

        if (self.loginEmail() == undefined || self.loginEmail() == '' || self.loginPassword() == undefined || self.loginPassword() == '') {
            self.result('لطفا اطلاعات کاربری خود را وارد نمایید');
            return;
        }

        var loginData = {
            grant_type: 'password',
            username: self.loginEmail(),
            password: self.loginPassword(),
            scope: privateOwnerId + ',' + dataOwnerId
        };

        freezUI();
        $.ajax({
            type: 'POST',
            crossOrigin: true,
            //beforeSend: function (request) {
            //    request.setRequestHeader("OwnerKey", privateOwnerId);
            //    request.setRequestHeader("Access-Control-Allow-Origin", "*");
            //},
            url: urls.loginUrl,
            data: loginData,
        }).done(function(data) {
            self.user(data.userName);
            //debugger
            $.cookie("token", data.access_token, { path: '/' });
            $loginForm.data("kendoWindow").close();

            var retUrlObject = self.requestAppObject();
            if (retUrlObject)
                self.callApi(retUrlObject.url, retUrlObject.callType, {}, retUrlObject.callBackFunc);

            shouldShowLogout(true);
            unfreezUI();

        }).fail(showAjaxError);
    };

    self.logout = function() {

        self.user('');
        shouldShowLogout(false);

        $.removeCookie('token');
        //window.location = '/';
        self.openLogin();
        self.result('لطفا اطلاعات کاربری خود را وارد نمایید');
    };

    self.openLogin = function() {
        unfreezUI();
        $loginForm.data("kendoWindow").center();
        $loginForm.data("kendoWindow").open();
    };

    $(document).on("click", ".btn-login", function () {
        self.login();
    });

    self.initLoginWindow = function () {
        $loginForm.removeClass('hide');
        if (!$loginForm.data("kendoWindow")) {
            $loginForm.kendoWindow({
                width: "450px",
                title: "ورود",
                actions: [],
                modal: true,
                visible: false,
                resizable: false
            });
        }
    };

    self.initLoginWindow();
};
var accountManagerApp = new accountManagerViewModel();
//******************************************************************//

function changePasswordViewModel() {
    // Data
    var self = this;

    self.oldPass = ko.observable('');
    self.newPass = ko.observable('');
    self.confirmNewPass = ko.observable('');

    self.onChangePass = function () {
        if (self.oldPass() === '' || self.newPass() === '' || self.confirmNewPass() === '') {
            showError('', 'لطفا اطلاعات درخواستی را تکمیل نمایید');
            $(".old-pass").focus();
            return;
        }
        if (self.newPass() !== self.confirmNewPass()) {
            showError('', 'رمز عبور جدید با تکرار آن برابر نیست');
            $(".new-pass").focus();
            return;
        }

        accountManagerApp.callApi(urls.changePasswordUrl, "POST", {
            oldPassword: self.oldPass(), newPassword: self.newPass(), confirmPassword: self.confirmNewPass
        }, function (data) {
            showSuccess('', 'رمز عبور شما تغییر کرد');
            self.clearForm();
        });
    };

    self.clearForm = function () {
        self.oldPass('');
        self.newPass('');
        self.confirmNewPass('');
    };
};
