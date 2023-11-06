(function () {
    'use strict';

    var salariesController = function (salariesService, WizardHandler, uiGridConstants, $filter, $timeout, $location) {
        var vm = this;
        vm.selectedLevel = [];
        vm.salaryModel = [];
        vm.salaries = [];
        vm.isLoading = true;
        vm.ErrorMessage = null;
        vm.ShowError = false;
        vm.SuccessMessage = null;
        vm.ShowSuccess = false;

        vm.initialisePage = function () {
            vm.isLoading = true;
            salariesService.getSalaries().then(function (response) {
                vm.isLoading = false;
                vm.salaries = response.data;
                if (response.data != null) { }
            }, function (error) {
                console.log(JSON.stringify(error));
                vm.isLoading = false;
            });
          };
        vm.updateNotch = function () {
            vm.salaryModel.salaryNotch = vm.salaryModel.salary.salaryNotch;
         };
        vm.saveNotch = function () {
            var tmpSalary = {};

            angular.copy(vm.salaryModel.salary, tmpSalary);
            tmpSalary.salaryNotch = vm.salaryModel.salaryNotch;

            salariesService.saveSalary(tmpSalary).then(function (response) {

                var arrayValue = $filter('filter')(vm.salaries, { salaryId: tmpSalary.salaryId })[0];
                arrayValue.salaryNotch = tmpSalary.salaryNotch;

                vm.ShowSuccess = true;
                vm.SuccessMessage = "salaries successfully saved!";

                $timeout(function () {

                    $location.path('/home');
                }, 10000);

             }, function (error) {
                 console.log(JSON.stringify(error));
             });
             
        }
        vm.cancel = function () {
            vm.selectedLevel = [];
            vm.salaryModel = [];
            vm.salaries = [];
            vm.initialisePage();
        };
        vm.getKey = function (key) {
            // 10 is the radix, which is the base (assumed to be base 10 here) 
            return parseInt(key, 10);
        }
    };
    angular.module('eRecruitmentApp').controller('salariesController', salariesController);
    salariesController.$inject = ['salariesService', 'WizardHandler', 'uiGridConstants', '$filter', '$timeout', '$location'];
})();