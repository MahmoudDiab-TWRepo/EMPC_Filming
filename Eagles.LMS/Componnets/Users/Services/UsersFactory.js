angular.module("LmsApp_Services").factory("usersFactory", ($http) => {
    var addUsers = (obj) => {
        return $http({
            method: "POST",
            url: '/api/User/Add',
            data: JSON.stringify(obj)
        });
    };
    var editusers = (obj) => {
        return $http({
            method: "POST",
            url: '/api/User/Edit',
            data: JSON.stringify(obj)
        });
    };
    var deleteUser = (userId) => {
        return $http({
            method: "POST",
            url: '/Admission/Users/Delete/' + userId + ''
        });
    };
    var activeUser = (userId) => {
        return $http({
            method: "POST",
            url: '/Admission/Users/ActiveAccount/' + userId + ''
        });
    };
    var blockUser = (userId) => {
        return $http({
            method: "POST",
            url: '/Admission/Users/BlockAccount/' + userId + ''
        });
    };
    var getAll = (groupId) => {

        return $http({
            method: "GET",
            url: '/api/ApiUsers/GetAll?GroupId=' + groupId+''
        });
    };
    var binduser = (userid) => {
        return $http({
            method: "GET",
            url: "/api/User/" + userid+"/Get",
        });
    };
    return {
        CreateUser: addUsers,
        GetAllUser: getAll,
        RemoveUser: deleteUser,
        BlockAccount: blockUser,
        ActiveAccount: activeUser,
        Binddata: binduser,
        UpdateUser:editusers
    };
});