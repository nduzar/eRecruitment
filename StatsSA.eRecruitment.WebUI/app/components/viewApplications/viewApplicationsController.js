(function () {
    'use strict';

    var viewApplicationsController = function (viewApplicationsService, WizardHandler, $timeout, $filter) {
        var vm = this;
        vm.hasRecords = false;
        vm.showDetails = false;
        vm.applications = [];
        
        vm.reference = undefined;

        vm.applicationRequirement = {};
        vm.programmes = {};
        vm.programme = null;
        vm.applicantApplications = {};
        vm.average = 0;


        vm.qualificationAnswersDislay = null;

        vm.qualificationAnswers = [
                    { answerScore: 0, answerDesc: "No Matric" },
                    { answerScore: 1, answerDesc: "Matric" },
                    { answerScore: 2, answerDesc: "Post-matric Certificate" },
                    { answerScore: 3, answerDesc: "National Diploma" },
                    { answerScore: 4, answerDesc: "B-Degree" },
                    { answerScore: 5, answerDesc: "Post-graduate" }];

        vm.southAfricanCitizenScoreAnswersDisplay = null;
        vm.southAfricanCitizenScoreAnswers = [
              { answerScore: 1, answerDesc: "No" },
               { answerScore: 3, answerDesc: "Yes" }];

        vm.ageScoreAnswersDisplay = null;
        vm.ageScoreAnswers = [
                { answerScore: 1, answerDesc: "No" },
               { answerScore: 3, answerDesc: "Yes" }];

        vm.trainingAnswers = [
            { answerScore: 1, answerDesc: "No" },
            { answerScore: 3, answerDesc: "Yes" }];

        vm.knowledgeAnswers = [
            { answerScore: 1, answerDesc: "No" },
            { answerScore: 2, answerDesc: "Yes" }];

        vm.internshipScoreAnswersDisplay = null;
        vm.internshipScoreAnswers = [
            { answerScore: 1, answerDesc: "No" },
            { answerScore: 3, answerDesc: "Yes" }];

        vm.showInfoMessage = true;
        vm.savingMessageShow = false;
        vm.successMessageShow = false;
        vm.getKey = function (key) {
            return parseInt(key, 10);
        };

        vm.currentApplication = undefined;
        vm.initialisePage = function () {
            viewApplicationsService.getListOfApplications().then(function (response) {
                if (response.data.length > 0) {
                    vm.hasRecords = true;
                }
                vm.applications = response.data;
                for (var i = 0; i < vm.applications.length; i++) {
                    var status = vm.applications[i].applicationStatus.applicationStatusDesc;

                    if (status === 'HRShortlisted' || status === 'HiringManagerShortlisted') {
                        vm.applications[i].applicationStatus.applicationStatusDesc = 'In Progress';
                    }

                    if (status === 'ScheduledInterview') {
                        vm.applications[i].applicationStatus.applicationStatusDesc = 'In Progress';
                    }

                    if (status === 'HRRejected' || status === 'LineManagerRejected') {
                        vm.applications[i].applicationStatus.applicationStatusDesc = 'In Progress';
                    }                    
                }
            }, function (error) { });
        };
        vm.editApplication = function (application) {
            vm.savingMessageShow = false;
            vm.successMessageShow = false;
            vm.showInfoMessage = true;
            vm.currentApplication = application;

            vm.qualificationAnswersDislay = $filter('filter')(vm.qualificationAnswers, { answerScore: vm.currentApplication.applicationRequirement.qualificationScore })[0].answerDesc;
            vm.southAfricanCitizenScoreAnswersDisplay = $filter('filter')(vm.southAfricanCitizenScoreAnswers, { answerScore: vm.currentApplication.applicationRequirement.southAfricanCitizenScore })[0].answerDesc;
            vm.ageScoreAnswersDisplay = $filter('filter')(vm.ageScoreAnswers, { answerScore: vm.currentApplication.applicationRequirement.ageScore })[0].answerDesc;
            vm.internshipScoreAnswersDisplay = $filter('filter')(vm.internshipScoreAnswers, { answerScore: vm.currentApplication.applicationRequirement.internshipScore })[0].answerDesc;
            vm.average = vm.currentApplication.applicationRequirement.overallAverage;

        };
        vm.saveApplication = function (application) {
            vm.savingMessageShow = true;
            vm.showInfoMessage = false;
            viewApplicationsService.saveApplication(application).then(function (response) {
                vm.savingMessageShow = false;
                vm.successMessageShow = true;
                $timeout(function () {
                    WizardHandler.wizard().previous();
                }, 10000);
            }, function (error) { console.log("Error saving application: " + JSON.stringify(error)); vm.savingMessageShow = false; });
        }
        vm.showDetail = function () {
            vm.showDetails = true;
        };
        vm.hideDetail = function () {
            vm.showDetails = false;
        };
        vm.validateClosingDate = function (closingDate) {
            var d = new Date(closingDate);
            var now = new Date();
            return d < now;
        };
    };

    angular.module('eRecruitmentApp').controller('viewApplicationsController', viewApplicationsController);
    viewApplicationsController.$inject = ['viewApplicationsService', 'WizardHandler', '$timeout', '$filter'];
})();