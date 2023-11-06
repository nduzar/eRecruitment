(function () {
    'use strict';

    var approvedListController = function (approvedListService, WizardHandler, $filter) {
        var vm = this;
        vm.canEdit = false;
        vm.sortBy = null;
        vm.showNoPost = false;
        vm.isLoading = true;
        
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
            vm.showNoPost = false;
            vm.isLoading = true;
            approvedListService.getApprovedList().then(function (response) {
                vm.vacancies = response.data;
                vm.isLoading = false;
                if (response.data != null) { }

                if (vm.vacancies.length == 0) {
                    vm.showNoPost = true;
                }
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        };
        
        vm.generateReport = function () {
            approvedListService.generateReport()
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
    angular.module('eRecruitmentApp').controller('approvedListController', approvedListController);
    approvedListController.$inject = ['approvedListService', 'WizardHandler', '$filter'];
})();