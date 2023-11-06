(function () {
    'use strict';

    var viewInternshipVacancyController = function (browseJobsService) {
        var vm = this;
        vm.vacancy = undefined;
        vm.closingDate = undefined;

        vm.availableVacancies = [];
        vm.currentApplications = [];

        vm.initialisePage = function () {

            vm.vacancy = browseJobsService.getCurrentVacancy();
            vm.closingDate = vm.vacancy.closingDate;

            browseJobsService.getAllActiveVacancies().then(function (response) {
                vm.availableVacancies = response.vacancies;
                vm.currentApplications = response.currentApplications;
            }, function (error) {
                console.log(error);
            });
        };

        vm.appliedForVacancy = function () {
            
            var found = false;

            if (vm.currentApplications.length > 0) {

                angular.forEach(vm.currentApplications, function (application, key) {
                    if (application.vacancyId == vm.vacancy) {
                        found = true;
                    }
                });
            }
            return found;
        };

        //vm.onViewApply = function (vacancy) {
        //    browseJobsService.setCurrentVacancy(vacancy);
        //}
    };

    angular.module('eRecruitmentApp').controller('viewInternshipVacancyController', viewInternshipVacancyController);
    viewInternshipVacancyController.$inject = ['browseJobsService'];
})();