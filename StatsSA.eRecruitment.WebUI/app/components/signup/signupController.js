(function () {
    'use strict';

    var signupController = function (signupService, $location, authService, $sessionStorage, statssaCoreService) {

        var vm = this;
        vm.confirmPassword;
        vm.errorMessageShow = false;
        vm.errorMessage = null;
        vm.popup = {
            opened: false
        };

        vm.citizen = true;

        vm.open = function () {
            vm.popup.opened = true;
        };

        var todayDate = new Date();
        var day = todayDate.getDate();
        var month = todayDate.getMonth();
        var year = todayDate.getFullYear();
        var minYear = year - 65;
        var maxYear = year + 1;

        vm.dateOptions = {
            //dateDisabled: disabled,
            formatYear: 'yy',
            maxDate: new Date(maxYear, 0, 1),
            minDate: new Date(minYear, 0, 1),
            startingDay: 1
        };

        vm.user = {
            firstName:undefined,
            surname:undefined,
            userName:undefined,
            email:undefined,
            cellphone:undefined,
            password:undefined,
            isActive: undefined,
            identityNumber: undefined,
            dateOfBirth: undefined,
            inputEmail: undefined,
            inputConfirmPrimaryEmail: undefined,
            inputSecondaryEmail: undefined,
            inputSecondaryEmailConfirm: undefined
        };

        //var loginUser = function (username, password, identityNumber) {
        //    authService.login({ userName: username, password: password, identityNumber: identityNumber }).then(function (response) {
        //        $location.path('/profile/0');
        //    }, function (response) {
        //        $location.path('/signupsuccess');
        //        //vm.message = response.data.error_description;
        //    });
        //};


        var formErrors = function (form) {
            var count = 0,
                errors = form.$error,
                fieldNames = '';

            angular.forEach(errors, function (val) { if (angular.isArray(val)) { count += val.length; } });

            angular.forEach(errors.pattern, function (field) {
                if (field.$invalid) {
                    fieldNames += field.$name + ', ';
                }
            });

            angular.forEach(errors.required, function (field) {
                if (field.$invalid) {
                    fieldNames += field.$name + ', ';
                }
            });

            return {
                errorCount: count, errorFields: fieldNames
            };
        };

        vm.saveUser = function (user) {

            vm.errorMessageShow = false;
            console.log(vm.user, user);
            if (user.userName == user.inputSecondaryEmail) {
                vm.errorMessageShow = false;
                vm.errorMessage = 'Email and secondary email cannot be same.';
                return;
            }
            return;

          /*  if (vm.frmUser.$invalid) {
                var error = 'error'
                var errorObject = formErrors(vm.frmUser);
                vm.errorMessageShow = true;

                if (errorObject.errorCount > 1) {
                    error = 'errors';
                }
                vm.errorMessage = errorObject.errorCount + ' ' + error + ' have been found: ' + errorObject.errorFields;
                return;
            }*/

            angular.forEach(vm.frmUser, function (value, key) {
                if (typeof value === 'object' && value.hasOwnProperty('$modelValue')) {
                    if (value.$error.required) {
                        vm.errorMessageShow = true;
                        vm.errorMessage = 'Complete required information ';
                        return;
                    }
                    if (value.$error.email) {
                        vm.errorMessageShow = true;
                        vm.errorMessage = 'Input valid email address ';
                        return;
                    }

                    if (value.$error.idNumber) {
                        vm.errorMessageShow = true;
                        vm.errorMessage = 'Input valid ID Number';
                        return;
                    }

                    if (value.$error.compareTo) {
                        vm.errorMessageShow = true;
                        vm.errorMessage = 'Password not matching';
                        return;
                    }

                    if (value.$invalid) {
                        vm.errorMessageShow = true;
                        vm.errorMessage = key + ' is not valid: ';
                        return;
                    }
                }
            });

            if (!vm.citizen) {

                var dbo = new Date(vm.user.dateOfBirth);

                if (dbo > todayDate) {
                    vm.errorMessageShow = true;
                    vm.errorMessage = 'date cannot be in the future';
                    return;
                }
            } else {
                var dateBirth = new Date(vm.user.identityNumber.substring(0, 2), vm.user.identityNumber.substring(2, 4) - 1, vm.user.identityNumber.substring(4, 6));
                vm.user.dateOfBirth = dateBirth;
            }

            var d = new Date(user.dateOfBirth);
            d.setHours(0, -d.getTimezoneOffset(), 0, 0);
            user.dateOfBirth = d;

            if (!vm.errorMessageShow) {

                user.email = user.userName;
                user.citizen = vm.citizen;

                signupService.saveUser(user).then(function (response) {
                    //$sessionStorage.put('dobData', JSON.stringify({ dob: user.dateOfBirth, idNumber: user.identityNumber }));
                    $sessionStorage.dobData = JSON.stringify({ dob: user.dateOfBirth, idNumber: user.identityNumber });
                    vm.errorMessageShow = false;
                    vm.errorMessage = null;
                    //loginUser(user.userName, user.password);
                    $location.path('/signupsuccess');
                }, function (error) {
                    if (error.data.exceptionMessage) {
                        vm.errorMessageShow = true;
                        vm.errorMessage = error.data.exceptionMessage;
                    }
                });
            }

        };
    };

    angular.module('eRecruitmentApp').controller('signupController', signupController);
    signupController.$inject = ['signupService', '$location', 'authService', '$sessionStorage', 'statssaCoreService'];
})();