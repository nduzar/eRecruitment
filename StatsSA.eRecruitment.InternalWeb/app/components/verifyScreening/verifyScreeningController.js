(function () {
    'use strict';

    var verifyScreeningController = function (screeningService, WizardHandler, $filter, $timeout, $location) {
        var vm = this;
        vm.canEdit = false;
        vm.application = null;
        vm.applicant = [];
        vm.allApplicationQuestionAnswers = [];
        vm.ErrorMessage = null;
        vm.ShowError = false;
        vm.SuccessMessage = null;
        vm.ShowSuccess = false;
       
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

                vm.applicant.profile.numberOfExperiencePrivate = vm.applicant.profile.numberOfExperiencePrivate == '' ? '0' : vm.applicant.profile.numberOfExperiencePrivate;
                vm.applicant.profile.numberOfExperiencePublic = vm.applicant.profile.numberOfExperiencePublic == '' ? '0' : vm.applicant.profile.numberOfExperiencePublic;

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
                vm.ShowSuccess = true;
                vm.SuccessMessage = "Application status set to shortlisted!";
                //WizardHandler.wizard().previous();

                $timeout(function () {
                    screeningService.backToApplications(path);
                }, 10000);
                
                if (response.data != null) { }
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        };

        vm.rejectAtVerification = function (application) {
            screeningService.rejectAtVerification(application).then(function (response) {
                vm.ShowSuccess = true;
                vm.SuccessMessage = "Application status set to rejected!";
                //WizardHandler.wizard().previous();

                $timeout(function () {
                    screeningService.backToApplications(path);
                }, 10000);

                if (response.data != null) { }
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        };

        vm.inProcess = function (application) {
            screeningService.inProcess(application).then(function (response) {
                vm.ShowSuccess = true;
                vm.SuccessMessage = "Application status set to InProgress!";
                //WizardHandler.wizard().previous();

                $timeout(function () {
                    screeningService.backToApplications(path);
                }, 10000);
                if (response.data != null) { }
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        };

        vm.successful = function (application) {
            screeningService.successful(application).then(function (response) {
                vm.ShowSuccess = true;
                vm.SuccessMessage = "Application status set to Successful!";
                //WizardHandler.wizard().previous();

                $timeout(function () {
                    screeningService.backToApplications(path);
                }, 10000);
                if (response.data != null) { }
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        };

        vm.unSuccessful = function (application) {
            screeningService.unSuccessful(application).then(function (response) {
                vm.ShowSuccess = true;
                vm.SuccessMessage = "Application status set to unSuccessful!";
                //WizardHandler.wizard().previous();

                $timeout(function () {
                    screeningService.backToApplications(path);
                }, 10000);
                if (response.data != null) { }
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        };

        vm.hrRejected = function (application) {
            screeningService.hrRejected(application).then(function (response) {
                vm.ShowSuccess = true;
                vm.SuccessMessage = "Application status set to hrRejected!";
                //WizardHandler.wizard().previous();

                $timeout(function () {
                    screeningService.backToApplications(path);
                }, 10000);
                if (response.data != null) { }
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        };

        

        vm.interview = function (application) {
            screeningService.interview(application).then(function (response) {
                vm.ShowSuccess = true;
                vm.SuccessMessage = "Application status set to schedule for interview!";
                //WizardHandler.wizard().previous();

                $timeout(function () {
                    screeningService.backToApplications(path);
                }, 10000);
                if (response.data != null) { }
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        }

        //vm.reject = function (application) {

        //    application.applicantApplication.applicationStatusId = '12';
        //    screeningService.unSuccessful(application).then(function (response) {
        //        vm.ShowSuccess = true;
        //        vm.SuccessMessage = "Application status set to UnSuccessful!";
        //        //WizardHandler.wizard().previous();

        //        $timeout(function () {
        //            screeningService.backToApplications(path);
        //        }, 3000);
        //        if (response.data != null) { }
        //    }, function (error) {
        //        console.log(JSON.stringify(error));
        //    });
        //};

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
    verifyScreeningController.$inject = ['screeningService', 'WizardHandler', '$filter', '$timeout', '$location'];
})();