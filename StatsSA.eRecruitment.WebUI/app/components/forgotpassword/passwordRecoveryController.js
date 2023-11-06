(function () {
    'use strict';

    var passwordRecoveryController = function (passwordRecoveryService, WizardHandler, authService, $location) {
        var vm = this;
        vm.resetCodeData = {
            resetCode: undefined
        };

        vm.codeVerificationMessage = undefined;

        var loginData = {
            userName: "",
            password: "",
            useRefreshTokens: false
        };

        vm.passwordData = {
            newPassword: undefined,
            cofirmPassword: undefined,
            userId: undefined,
            userName: undefined
        };

        vm.inputType = "password";
        

        vm.passwordResetRequest = {};

        vm.message = undefined;

        vm.initialisePage = function () {
            vm.message = '';
            vm.showMessage = false;
        };

        vm.hideShowPassword = function () {
            if (vm.inputType == 'password')
                vm.inputType = 'text';
            else
                vm.inputType = 'password';
        };

        vm.tryAgain = function () {
            vm.codeVerificationMessage = false;
        }

        vm.codeVerifivation = function (resetCodeData) {
            vm.codeVerificationMessage = undefined;
            passwordRecoveryService.codeVerifivation(resetCodeData).then(function (response) {
                vm.passwordResetRequest = response.passwordResetRequest;
                if ((vm.passwordResetRequest !== null)) {
                    vm.passwordData.userId = response.passwordResetRequest.userId;
                    vm.passwordData.userName = response.username;
                    WizardHandler.wizard().next();
                }
            }, function (error) {
                vm.codeVerificationMessage = error.data.exceptionMessage;
            });
        };

        vm.resetPassword = function (passwordData) {
            passwordRecoveryService.resetPassword(passwordData).then(function (response) {
                console.log(JSON.stringify(passwordData));
                loginData.password = passwordData.newPassword;
                loginData.userName = passwordData.userName;
                $location.path('/browseJobs');
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        };

        var login = function (loginData) {
            authService.login(loginData).then(function (response) {
                $location.path('/browseJobs').replace();
            }, function (response) {
                vm.message = response.data.error_description;
            });
        };

    };

    angular.module('eRecruitmentApp').controller('passwordRecoveryController', passwordRecoveryController);
    passwordRecoveryController.$inject = ['passwordRecoveryService', 'WizardHandler', 'authService', '$location'];
})();