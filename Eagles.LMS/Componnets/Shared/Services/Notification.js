angular.module("LmsApp_Services").factory("NotificationServices", () => {
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
    return {
        pushNotification: pushNotification
    };
});

$(function () {
    addBorder = (type, controller) => {
        if (type == "danger") {
            controller.css("border", "1px solid red");
        } else {
            controller.css("border", "1px solid green");

        }
    };
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
        $.playSound("/assets/whistle_short.mp3");
    };
});