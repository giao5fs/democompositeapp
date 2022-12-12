app.controller("loginController", function ($scope, $http, $q, $timeout) {
    $scope.message = "This from Login.js";

    var login = function () {
        var deferred = $q.defer();
        $http({
            method: 'POST',
            url: "http://localhost:5007/api/authenication/Login",
            data: {
                UserName: $scope.myusername,
                Password: $scope.mypassword
            },
            headers: {
                'Content-Type': 'application/json'
            }
        }).success(function (data, status, headers, config) {
            console.log(data);
            deferred.resolve(data);
        }).error(function (data, status, headers, config) {
            console.log(data);
            deferred.resolve(data);
        });
        return deferred.promise;
    }

    $scope.login = login;

});