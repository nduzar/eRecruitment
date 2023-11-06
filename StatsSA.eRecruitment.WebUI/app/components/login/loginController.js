(function () {
    'use strict';

    var loginController = function ($scope, authService, ngConfigSettings, $location) {
        var vm = this;
        vm.message = "";
        vm.loginData = {
            userName: "",
            password: "",
            useRefreshTokens: false
        };
        vm.errorMessageShow = false;
        vm.errorMessage = null;

        vm.login = function () {
            if (vm.frmLogin.$invalid) {
                vm.errorMessageShow = true;
                if (vm.loginData.userName === '') {                    
                    vm.errorMessage = "Username or Password cannot be blank";
                    return;
                }
                if (vm.loginData.password === '') {
                    vm.errorMessage = "Username or Password cannot be blank";
                    return;
                }
                return;
            }
            authService.login(vm.loginData).then(function (response) {
                $location.path('/browseJobs').replace;
            }, function (response) {
                vm.errorMessageShow = true;
                    vm.errorMessage = response.data.error_description;
           });
        };

        vm.singUp = function () {
            $location.path('/signup').replace();
        };
    };

    angular.module('eRecruitmentApp').controller('loginController', loginController);

    loginController.$inject = ['$scope', 'authService', 'ngConfigSettings', '$location'];
})();