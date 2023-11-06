(function () {
    'use strict';

    var viewVacancyController = function (browseJobsService) {
        var vm = this;
        vm.vacancy = undefined;
        vm.viewOnly = undefined;
        vm.salaryLevel = undefined;

        vm.initialisePage = function () {
            vm.vacancy = browseJobsService.getCurrentVacancy();            
            vm.viewOnly = browseJobsService.getViewOnly();
            vm.salaryLevel = vm.vacancy.salary.salaryId == '17' ? 'Internship Programme Stipend' : vm.vacancy.salary.salaryId
            vm.vacancy.remunerationDescription = vm.vacancy.salary.salaryId == '17' ? '' : vm.vacancy.remunerationDescription
        };

        //vm.onViewApply = function (vacancy) {
        //    browseJobsService.setCurrentVacancy(vacancy);
        //}
    };

    angular.module('eRecruitmentApp').controller('viewVacancyController', viewVacancyController);
    viewVacancyController.$inject = ['browseJobsService'];
})();