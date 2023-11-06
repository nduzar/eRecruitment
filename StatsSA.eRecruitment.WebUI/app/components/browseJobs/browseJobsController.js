(function () {
    'use strict';

    var browseJobsController = function (browseJobsService, $location, $filter, $mdDialog) {
        var vm = this;
        vm.availableVacancies = [];
        vm.currentApplications = [];
        vm.currentYear = new Date().getFullYear();
        vm.followingYear = new Date().getFullYear() + 1;

        vm.initialisePage = function () {
            browseJobsService.getAllActiveVacancies().then(function (response) {
                vm.availableVacancies = response.vacancies;
                vm.currentApplications = response.currentApplications;
            }, function (error) {
                console.log(error);
            });
        };

        vm.searchFilter = undefined;
        vm.searchString = null;
        vm.setFilter = function () {

            vm.searchFilter = vm.searchString;
            vm.applications = $filter('filter')(vm.availableVacancies, vm.searchFilter);

           // setPager(1);

        };

        vm.cancel = function () {
            $mdDialog.cancel();
        }

        vm.clearFilter = function () {
            vm.searchFilter = undefined;
            vm.searchString = null;
        };

        vm.showAlert = function (ev) {

            $mdDialog.show({
                contentElement: '#myDialog',
                parent: angular.element(document.querySelector('body')),
                targetEvent: ev,
                clickOutsideToClose: true
            });
        };


        vm.showInternshipAlert = function (ev) {

            $mdDialog.show({
                contentElement: '#internshipDialog',
                parent: angular.element(document.querySelector('body')),
                targetEvent: ev,
                clickOutsideToClose: true
            });
        };

        vm.appliedForVacancy = function (vacancyId) {

            var found = false;
            angular.forEach(vm.currentApplications, function (application, key) {
                if (application.vacancyId == vacancyId) {
                    found = true;
                }
            });
            return found;
        };

        vm.viewApplication = function (vacancy) {

        };

        vm.onViewApply = function (vacancy) {
            browseJobsService.setCurrentVacancy(vacancy);
            browseJobsService.setViewOnly(false);
            $location.path('/viewVacancy');
        }

        vm.viewOnly = function (vacancy) {
            browseJobsService.setCurrentVacancy(vacancy);
            browseJobsService.setViewOnly(true);
            $location.path('/viewVacancy');
        }

        vm.onViewInternship = function (vacancy) {
            browseJobsService.setCurrentVacancy(vacancy);
            $location.path('/apply');
        }
    };

    angular.module('eRecruitmentApp').controller('browseJobsController', browseJobsController);
    browseJobsController.$inject = ['browseJobsService', '$location', '$filter', '$mdDialog'];
})();