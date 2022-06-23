angular.module("LmsApp_Controllers").controller("usersCtrl",
    ["$scope", "NotificationServices", "groupsFactory", "usersFactory", "DataTableServices",
        (s, n, f, usersFactory, dT) => {
      
                  // remove users
            s.Remove = (userId, index, e) => {
                e.preventDefault();
                bootbox.confirm('Confirm Delete Operation', function (flag) {
                    if (flag) {
                        usersFactory.RemoveUser(userId).then(function success(response) {
                            location.reload();

                        }, function error(error) {

                            let errorMessage = manageAjaxError(error);
                            n.pushNotification("danger", errorMessage);
                        });
                    }
                });
            };
            s.ActiveAccount = (userId, index, e) => {
                e.preventDefault();
                bootbox.confirm('Confirm activation account', function (flag) {
                    if (flag) {
                        usersFactory.ActiveAccount(userId).then(function success(response) {
                            location.reload();
                           

                        }, function error(error) {

                            let errorMessage = manageAjaxError(error);
                                n.pushNotification("danger", errorMessage);
                        });
                    }
                });
            };

            s.BlockAccount = (userId, index, e) => {
                e.preventDefault();
                bootbox.confirm('Confirm block account', function (flag) {
                    if (flag) {
                        usersFactory.BlockAccount(userId).then(function success(response) {
                            location.reload();
                          
                        }, function error(error) {
                               
                            let errorMessage = manageAjaxError(error);
                                n.pushNotification("danger", errorMessage);
                        });
                    }
                });
            };

            function getGroupId() {
             return $("#groupId").val();
    
            }
        }]);