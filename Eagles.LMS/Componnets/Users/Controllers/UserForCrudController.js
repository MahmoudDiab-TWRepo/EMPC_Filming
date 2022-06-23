angular.module("LmsApp_Controllers").controller("usersCtrl",
    ["$scope", "NotificationServices", "groupsFactory", "usersFactory", "DataTableServices",
        (s, n, f, usersFactory, dT) => {


            // get all groups
            f.GetAllGroups().then(function success(response) {
                s.Groups = response.data;
                s.Group_dll = response.data[0];


            }, function error(error) {
                let errorMessage = manageAjaxError(error);
                n.pushNotification("danger", errorMessage);
            });


            if (bind() != undefined) {
                usersFactory.Binddata(bind()).then(function success(response) {
                    s.FullName = response.data.FullName;
                    s.EmailAddress = response.data.EmailAddress;
                    s.Phone = response.data.Mobile;
                   
                
                    var group = s.Groups.filter(arr => {

                        if (arr.Id == response.data.GroupId) {

                            return arr;
                        }
                    });
                    s.Group_dll = group[0];
                }, function error(error) {

                    let errorMessage = manageAjaxError(error);
                    n.pushNotification("danger", errorMessage);
                });
            }



            function Clear() {
              
                s.FullName = "";
                s.EmailAddress = "";
                s.Password = "";
                s.Phone = "";
                s.ConfirmPassword = "";
             


            }
            function validateEmail(email) {
                var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;


                return re.test(email);
            }
            // validate form
            function validateForm(isEditMode = false) {

                let flag = false;
                 if (s.FullName == "" || typeof (s.FullName) == 'undefined') {
                    n.pushNotification("danger", "Please Enter The Full Name");
                }
                if (isEditMode == false || isEditMode == true && (s.Password != "" && typeof (s.Password) != 'undefined')) {
                    if (s.Password == "" || typeof (s.Password) == 'undefined') {

                        n.pushNotification("danger", "Please Enter User Password");
                        return;
                    }
                    else if (s.Password.length < 6) {
                        n.pushNotification("danger", "Password Weak << Please Enter At Least 6 Letters Or Numbers");
                        return;

                    }
                    else if (s.Password != s.ConfirmPassword) {
                        n.pushNotification("danger", "Passwords Don't Match");
                        return;

                    }
                }
                if (s.EmailAddress == "" || typeof (s.EmailAddress) == 'undefined') {
                    n.pushNotification("danger", "Please Enter An Email Address");
                }

                else if (!validateEmail(s.EmailAddress)) {
                    n.pushNotification("danger", "Please Enter An Valid Email Address");
                }
                else if (s.Phone == "" || typeof (s.Phone) == 'undefined') {
                    n.pushNotification("danger", "Please Enter Phone Number");
                }


                else
                    flag = true;

                return flag;
            }

            s.CreateUser = () => {

                if (validateForm()) {
                    let user = {
                      
                        FullName: s.FullName,
                        Password: s.Password,
                        EmailAddress: s.EmailAddress,
                        GroupId: s.Group_dll.Id,
                        Mobile: s.Phone,

                    };
                    // get all safes    
                    usersFactory.CreateUser(user).then(function success(response) {
                        n.pushNotification("success", "Added Successfully");
                        Clear();
                    }, function error(error) {

                        let errorMessage = manageAjaxError(error);
                        n.pushNotification("danger", errorMessage);
                    });
                }
            };
            function bind() {
                var usid = $("#Userid").val();
                return usid;
            }
            s.EditUser = () => {

                if (validateForm(isEditMode = true)) {
                    let user = {
                      
                        FullName: s.FullName,
                        Password: s.Password,
                        EmailAddress: s.EmailAddress,
                        GroupId: s.Group_dll.Id,
                        Mobile: s.Phone,
                        ID: bind(),
                    };

                    // get all safes    
                    usersFactory.UpdateUser(user).then(function success(response) {
                        n.pushNotification("success", "Updated Successfully");

                    }, function error(error) {

                        let errorMessage = manageAjaxError(error);
                        n.pushNotification("danger", errorMessage);


                    });
                }
            };
        }]);


$(".kt-input-icon .show-hidden i").click(function () {
    $(this).toggleClass("la-eye la-eye-slash");
    var input = $(".kt-input-icon .pass");
    if (input.attr("type") === "password") {
        input.attr("type", "text");
    } else {
        input.attr("type", "password");
    }
});