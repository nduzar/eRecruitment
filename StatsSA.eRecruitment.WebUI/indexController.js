(function () {
    'use strict';
    angular.module('eRecruitmentApp').controller('indexController', ['$scope', 'authService', '$location', 'statssaCoreService', '$window', function ($scope, authService, $location, statssaCoreService, $window) {

        $scope.navbarExpanded = false;
        //var result = statssaCoreService.validateSAIdNumber('8212026599087');
        //console.log(result);

        $scope.download = function(link) {
            window.location = link;
            return false;
        };

        $scope.logOut = function () {
            authService.logOut();
            $location.path('/browseJobs');
        }
        $scope.viewApplications = function () {
            $location.path('/viewApplications');
        }

        $scope.copyrightYear = new Date().getFullYear();

        $scope.authentication = authService.authentication;
        if (!authService.authentication.isAuth) {
            if ($location.url() !== '/passwordrecovery') {
                $location.path('/browseJobs');
            }            
        }

        //$window.onbeforeunload = function (event) {
        //    authService.logOut();
        //};

    }]);
})();