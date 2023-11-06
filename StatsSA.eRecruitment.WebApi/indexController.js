(function () {
    'use strict';
    angular.module('StatsSaAuthServer').controller('indexController', ['$scope', 'authService', '$location', function ($scope, authService, $location) {

        $scope.logOut = function () {
            authService.logOut();
            $location.path('/login');
        }

        $scope.authentication = authService.authentication;
        if (!authService.authentication.isAuth) {
            $location.path('/login');
        }

    }]);
})();