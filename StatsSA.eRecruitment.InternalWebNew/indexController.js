(function () {
    'use strict';
    angular.module('eRecruitmentApp').controller('indexController', ['$scope', '$location', 'authorisationService', function ($scope, $location, authorisationService) {
        $scope.authorisation = {};
        $scope.loading = true;
        $scope.initialise = function () {
            authorisationService.fillAuthData().then(function (response) {
                $scope.loading = false;
                if (!response.isAuth) {
                    $scope.loading
                    $location.path('/unauthorizedaccess');
                }
                else {
                    $location.path('/home');
                }
                $scope.authorisation = response;
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        }
        
        $scope.recheckAuth = function () {
            $scope.loading = true;
            authorisationService.recheckAuth().then(function (response) {
                $scope.authorisation = response;
                if (response.isAuth != null) {
                    if (!response.isAuth) {
                        $location.path('/unauthorizedaccess');
                    }
                    else {
                        $location.path('/home');
                    }
                }
                $scope.loading = false;
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        }
    }]);
})();