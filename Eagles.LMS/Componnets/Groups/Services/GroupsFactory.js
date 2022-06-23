

angular.module("LmsApp_Services").factory("groupsFactory", ($http) => {
    var getGroups = (groupId) => {
        return $http({
            method: "GET",
            url: '/api/web/Groups/GetAll'
        });
    };
    var deleteGroups = (groupId) => {
        return $http({
            method: "POST",
            url: '/Admission/Groups/Remove/' + groupId + ''
        });
    };
    var getPrivilages = (groupId) => {
        return $http({
            method: "GET",
            url: '/api/web/Privilages/GetAll/Group/' + groupId + ''
        });
    };
    var addGroup = (obj) => {
        return $http({
            method: "POST",
            url: '/Admission/Groups/Create',
            data: JSON.stringify(obj),
        });
    };
    var editGroup = (obj) => {
        return $http({
            method: "POST",
            url: '/Admission/Groups/Edit',
            data: JSON.stringify(obj),
        });
    };
    return {
        getAllPrivilages: getPrivilages,
        CreateGroup: addGroup,
        EditGroup: editGroup,
        GetAllGroups: getGroups,
        RemoveGroup: deleteGroups
    };
});