(function () {
    'use strict';

    var verifyScreeningController = function (screeningService, WizardHandler, $filter) {
        var vm = this;
        vm.canEdit = false;
        vm.application = null;
        vm.applicant = [];
        vm.allApplicationQuestionAnswers = [];
       
        vm.average = 0;

        vm.reference = undefined;
        vm.currentApplication = {};

        vm.applicationRequirement = {};
        vm.programmes = {};
        vm.programme = null;
        vm.applicantApplications = {};

        var path = '/rankedScreening';
        
        vm.EmbedUrl = null;
        vm.showDocs = false;
        vm.initialisePage = function () {
            vm.application = screeningService.getCurrentApplication();
            screeningService.getApplicantProfile(vm.application).then(function (response) {
                vm.applicant = response.data;
                console.log(JSON.stringify(vm.applicant));

                vm.currentApplication = vm.application;

                var applicationsQuestionAnswer = vm.applicant.allApplicantQuestionAnswers;
                for (var i = 0; i < applicationsQuestionAnswer.length; i++) {
                    
                    if (applicationsQuestionAnswer[i].genericQuestion != null && applicationsQuestionAnswer[i].genericAnswer != null) {
                        vm.allApplicationQuestionAnswers.push({
                            question: applicationsQuestionAnswer[i].genericQuestion,
                            answer: applicationsQuestionAnswer[i].genericAnswer,
                            otherAnswerDescription: applicationsQuestionAnswer[i].otherAnswerDescription
                        });
                    }

                    if (applicationsQuestionAnswer[i].specificQuestion != null && applicationsQuestionAnswer[i].specificAnswer != null) {
                        vm.allApplicationQuestionAnswers.push({
                            question: applicationsQuestionAnswer[i].specificQuestion,
                            answer: applicationsQuestionAnswer[i].specificAnswer,
                            otherAnswerDescription: applicationsQuestionAnswer[i].otherAnswerDescription
                        });
                    }
                }

                if (response.data != null) { }
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        };
        vm.verifyApplication = function (application) {
            screeningService.verifyApplication(application).then(function (response) {
                screeningService.backToApplications(path);
                if (response.data != null) { }
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        };

        vm.shortlistApplication = function (application) {
            application.applicantApplication.applicationStatus.applicationStatusDesc = 'HRShortlisted';
            screeningService.shortListAppliction(application).then(function (response) {
                screeningService.backToApplications(path);
                if (response.data != null) { }
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        };

        vm.rejectAtVerification = function (application) {
            screeningService.rejectAtVerification(application).then(function (response) {
                screeningService.backToApplications(path);
                if (response.data != null) { }
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        };

        vm.inProcess = function (application) {
            screeningService.inProcess(application).then(function (response) {
                screeningService.backToApplications(path);
                if (response.data != null) { }
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        };

        vm.successful = function (application) {
            screeningService.successful(application).then(function (response) {
                screeningService.backToApplications(path);
                if (response.data != null) { }
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        };

        vm.unSuccessful = function (application) {
            screeningService.unSuccessful(application).then(function (response) {
                screeningService.backToApplications(path);
                if (response.data != null) { }
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        };

        vm.backToApplicants = function () {
            screeningService.backToApplications(path);
        }
        vm.viewAttachment = function (attachment) {
            screeningService.getAttachment(attachment);
        }
        vm.getKey = function (key) {
            // 10 is the radix, which is the base (assumed to be base 10 here) 
            return parseInt(key, 10);
        }
    };
    angular.module('eRecruitmentApp').controller('verifyScreeningController', verifyScreeningController);
    verifyScreeningController.$inject = ['screeningService', 'WizardHandler', '$filter'];
})();