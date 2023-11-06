(function () {
    'use strict';

    var cancelVacancyController = function (cancelVacancyService, WizardHandler,$filter) {
        var vm = this;
        vm.canEdit = true;
        vm.searchFilter = undefined;
        vm.searchString = null;
        vm.ErrorMessage = null;
        vm.ShowError = false;
        vm.SuccessMessage = null;
        vm.ShowSuccess = false;
        vm.setSorting = function (sortColumn) {
            if (vm.sortBy == sortColumn) {
                vm.sortBy = '-' + sortColumn
            } else {
                vm.sortBy = sortColumn;
            }
            console.log("SORTING: " + sortColumn);
        };
        vm.setFilter = function () {
            vm.searchFilter = vm.searchString;
            console.log(vm.searchString);
        };
        vm.clearFilter = function () {
            vm.searchFilter = undefined;
            vm.searchString = null;
        };
        vm.editMode = function (state) {
            vm.canEdit = state;
        };
        vm.salaries = [];
        vm.vacancies = [];
        vm.selectedVacancy = [];
        vm.hideConfirmReason = true;
        vm.cancelReason = null;
        vm.myToday = null
        vm.initialisePage = function () {
            cancelVacancyService.getSalaries().then(function (response) {
                vm.salaries = response.data;
                if (response.data != null) { }
            }, function (error) {
                console.log(JSON.stringify(error));
            });
            cancelVacancyService.getCancelableVacancies().then(function (response) {
                vm.vacancies = response.data;
                if (response.data != null) { }
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        };
        vm.viewVacancy = function (selectedVacancy) {
            vm.selectedVacancy = selectedVacancy;
            
            var d = new Date();
            vm.myToday = d;

            WizardHandler.wizard().next();
        };
        vm.back = function () {
            vm.hideConfirmReason = true;
            vm.cancelReason = null;
            WizardHandler.wizard().previous();
        };
        vm.confirmReason = function () {
            vm.cancelReason = null;
            vm.hideConfirmReason = false;
        }
        vm.cancel = function (vacancy) {
            vacancy.statusComment = vm.cancelReason;

            var d = new Date(vacancy.openingDate);
            d.setHours(0, -d.getTimezoneOffset(), 0, 0);
            vacancy.openingDate = d;

            var d = new Date(vacancy.closingDate);
            d.setHours(23, -d.getTimezoneOffset(), 0, 0);
            d.setMinutes(59, 59, 0);
            vacancy.closingDate = d;

            cancelVacancyService.cancelVacancy(vacancy).then(function (response) {
                vm.vacancies = response.data;
                vm.ShowSuccess = true;
                vm.SuccessMessage = "vacancy successfully cancelled!";
                //WizardHandler.wizard().previous();
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        };
        vm.updateNotch = function (selectedSalary) {
            console.log();
            vm.selectedVacancy.salaryNotch = selectedSalary.salaryNotch;
            vm.selectedVacancy.salaryId = selectedSalary.salaryId;
            console.log(JSON.stringify(tempObj));
        };
        vm.getKey = function (key) {
            // 10 is the radix, which is the base (assumed to be base 10 here) 
            return parseInt(key, 10);
        }
    };
    angular.module('eRecruitmentApp').controller('cancelVacancyController', cancelVacancyController);
    cancelVacancyController.$inject = ['cancelVacancyService', 'WizardHandler', '$filter'];
})();