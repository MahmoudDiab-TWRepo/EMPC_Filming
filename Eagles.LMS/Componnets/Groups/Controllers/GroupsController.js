angular.module("LmsApp_Controllers").
    controller("groupsCtrl", ["$scope", "NotificationServices", "groupsFactory", "DataTableServices", "CulutrServices", (s, n, f, dT, cl) => {
    let _location = location.href,
        groupIdMode = _location.split('/')[_location.split('/').length - 1],
        groupId = isNaN(groupIdMode) ? null : groupIdMode;
    s.txt_groupName = "";

    // get all groups
    f.GetAllGroups(groupId).then(function success(response) {
        s.Groups = response.data;
        //add data table
        //dT.pushDataTable("#table_groups");

    }, function error(error) {


            let errorMessage = manageAjaxError(error);
            n.pushNotification("danger", errorMessage);
    });
    // Remove groups

    s.Remove = (groupId, index, e) => {
        e.preventDefault();
        
        bootbox.confirm(cl.GetToken('ConfirmDelete'), function (flag) {
            if (flag) {
                f.RemoveGroup(groupId).then(function success(response) {
                    if (response.data.Code == 200) {
                        s.Groups.splice(index, 1);
                        n.pushNotification("success", cl.GetToken('SuccessfullyDeleted'));
                    }
                    else {
                        n.pushNotification("danger", response.data.Message);

                    }
                }, function error(error) {

     let errorMessage = manageAjaxError(error);
                            n.pushNotification("danger", errorMessage);
                });
            }
        });
    };

    // get all privilages
    f.getAllPrivilages(groupId).then(function success(response) {

        let privilageArr = [],
            privilagesTree = response.data.Tree;
        s.txt_groupName = response.data.GroupName;
        s.groupId = response.data.GroupId;
        for (var i = 0; i < privilagesTree.length; i++) {
            privilageArr.push({ "id": "" + privilagesTree[i].Id + "", "parent": "#", "text": "" + privilagesTree[i].ParentName + "", "state": { "selected": false } });
            for (var j = 0; j < privilagesTree[i].Privilages.length; j++) {


                privilageArr.push({ "id": "" + privilagesTree[i].Privilages[j].Id + "", "parent": "" + privilagesTree[i].Id + "", "text": "" + privilagesTree[i].Privilages[j].MenueName + "", "state": { "selected": privilagesTree[i].Privilages[j].IsChecked } });
            }
        }
        $('#jstree').jstree({
            "types": {
                "default": { "icon": "fa fa-folder kt-font-warning" }
            },
            "plugins": ["checkbox", "types"],
            'core': {
                'data': privilageArr
            }
        });


    }, function error(error) {


            let errorMessage = manageAjaxError(error);
            n.pushNotification("danger", errorMessage);
    });
    // create group
    s.Create = () => {
        if (validateForm()) {
            var privliagesArray = $('#jstree').jstree("get_selected");
            f.CreateGroup({ GroupName: s.txt_groupName, privilages: privliagesArray }).then(function success(response) {
     
                clear();
                n.pushNotification("success", cl.GetToken('AddedSuccessfully'));
            }, function error(error) {
               

                    let errorMessage = manageAjaxError(error);
                    n.pushNotification("danger", errorMessage);
            });
        }
    };
    //update
    s.Update = () => {
        if (validateForm()) {
            var privliagesArray = $('#jstree').jstree("get_selected");
            f.EditGroup({ GroupId: s.groupId, GroupName: s.txt_groupName, privilages: privliagesArray }).then(function success(response) {
                n.pushNotification("success", cl.GetToken('UpdatedSuccessfully'));
            }, function error(error) {

                    let errorMessage = manageAjaxError(error);
                    n.pushNotification("danger", errorMessage);
            });
        }
    };

    // validate form
    function validateForm() {
        let flag = false;
        if (s.txt_groupName == "" || s.txt_groupName == null) {
            n.pushNotification("danger", cl.GetToken('GroupNameIsRequired'));
        } else if ($('#jstree').jstree("get_selected").length == 0) {
            n.pushNotification("danger", cl.GetToken('PleaseSpecifyPermissions'));
        }
        else
            flag = true;
        return flag;
    }
    function clear() {
        s.txt_groupName = "";
        $('#jstree').jstree(true).deselect_all();
    }
}]);