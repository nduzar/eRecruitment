(function () {
    'use strict';

    var cancelledListController = function (cancelledListService, WizardHandler, $filter) {
        var vm = this;
        vm.canEdit = false;
        vm.sortBy = null;
        
        vm.setSorting = function (sortColumn) {
            if (vm.sortBy == sortColumn) {
                vm.sortBy = '-' + sortColumn
            } else {
                vm.sortBy = sortColumn;
            }
            console.log("SORTING: " + sortColumn);
        };
        vm.searchFilter = undefined;
        vm.searchString = null;
        vm.setFilter = function () {
            vm.searchFilter = vm.searchString;
            console.log(vm.searchString);
        };
        vm.clearFilter = function () {
            vm.searchFilter = undefined;
            vm.searchString = null;
        };
        vm.vacancies = [];
        vm.selectedVacancy = [];

        vm.initialisePage = function () {
            cancelledListService.getcancelledList().then(function (response) {
                vm.vacancies = response.data;
                if (response.data != null) { }
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        };
        
        vm.generateReport = function () {
            cancelledListService.generateReport()
            .then(function (response) {               
                console.log("succeded to pass data to service: ");
            }, function (reseponse) {
                console.log("failed to pass data to service: ");
            });
        };

        vm.getKey = function (key) {
            // 10 is the radix, which is the base (assumed to be base 10 here) 
            return parseInt(key, 10);
        }
    };
    angular.module('eRecruitmentApp').controller('cancelledListController', cancelledListController);
    cancelledListController.$inject = ['cancelledListService', 'WizardHandler', '$filter'];
})();