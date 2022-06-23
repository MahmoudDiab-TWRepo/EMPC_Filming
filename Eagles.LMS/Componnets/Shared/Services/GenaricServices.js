angular.module("LmsApp_Services").factory("GenaricServices", () => {

    isValidDate = (dateString, controller = "") => {
        return true;

        var date_regex = /^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$/;
        if (!(date_regex.test(dateString))) {

            controller.css("border", "1px solid red").focus();


            return false;
        }
        controller.css("border", "1px solid green");

        return true;
    };
    return {
        IsValidDate: isValidDate
    };
});
$(function () {
    pushNotification = (type, message) => {
        $.notifyClose();
        $.notify(message, {
            type: type,
            placement: {
                from: "top",
                align: "left"
            },
            offset: 20,
            spacing: 10,
            z_index: 99999999,
            delay: 5000,
            timer: 1000,
            url_target: '_blank',
            mouse_over: null,
            animate: {
                enter: 'animated bounceInDown',
                exit: 'animated zoomOutUp'
            }
        });
        //if (type == "danger")
        //    $.playSound("/assets/whistle_short.mp3");
        //else
        //    $.playSound("/assets/ios_notification.mp3");

    }
    manageAjaxError = (response) => {
        console.log(response);
        $('#loadingDiv').hide();
        if (response.status === 500) {
            response.responseText = "Server-Error";
        }

        else if (response.status === 401) {
            response.responseText = "you don't have permission to access";
        }
        else if (response.status === 404) {
            response.responseText = "Not-Found";
        }
        else if (response.status === 400 && response.responseJSON != null && response.responseJSON.Message != null && response.responseJSON.Message != "" && !response.responseJSON.Message.includes("<!DOCTYPE ")) {

            response.responseText = response.responseJSON.Message;

        }
        else {
            try {
                if (response.data != null && response.data != "" && !response.data.includes("<!DOCTYPE "))
                    response.responseText = response.data;
                else if (response.statusText != null && response.statusText != "" && !response.statusText.includes("<!DOCTYPE "))
                    response.responseText = response.statusText;

                else if (response.responseText == null || response.responseText == "")
                    response.responseText = "Error";
            }
            catch {
                response.responseText = response.data.Message;

            }
            if (typeof (response.responseText) == 'object') {

                response.responseText = response.data.Message


            }
            if (response.responseText.includes("Method Not Allowed") || response.responseText.includes("Unauthorized")) {

                return "you don't have permission to access";
            }
        }
        return response.responseText;
    };
    $("input[data-type='numberOnly'").keypress(s => {

        if (s.which < 48 || s.which > 57) {
            s.preventDefault();
        }
    });
    $("input[data-type='numberDotOnly'").keypress(s => {

        if ((s.which < 48 || s.which > 57) && s.which != 46) {
            s.preventDefault();
        }
    });

});