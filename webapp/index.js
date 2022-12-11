/// <reference path="angular.js"/>

var app = angular.module("MyModule", []);

app.controller("myController", function ($scope) {
    $scope.message = "This from angular!!";
    friends = [{ name: 'John', phone: '555-1276', like: 0 },
    { name: 'Mary', phone: '800-BIG-MARY', like: 0 },
    { name: 'Mike', phone: '555-4321', like: 0 },
    { name: 'Adam', phone: '555-5678', like: 0 },
    { name: 'Julie', phone: '555-8765', like: 0 },
    { name: 'Juliette', phone: '555-5678', like: 0 }]
    $scope.friends = friends;
    $scope.rowlimit = 3;

    var increase = function (friend) {
        friend.like++;

    }

    $scope.increase = increase;
    $scope.sortColumn = 'name';
    $scope.reverseSort = false;

    $scope.sortData = function (column) {
        $scope.reverseSort = ($scope.sortColumn == column) ? !$scope.reverseSort : false;
        $scope.sortColumn = column;
    }

    $scope.getSortClass = function (column) {
        if ($scope.sortColumn == column) {
            return $scope.reverseSort ? 'arrow-down' : 'arrow-up';
        }
        return '';
    }
});

