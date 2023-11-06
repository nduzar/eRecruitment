(function () {
    'use strict';

    var applyCareerController = function ($localStorage, applyService, browseJobsService, createProfileService, Upload, $timeout, $filter, $location, profileService, $mdDialog) {
        var vm = this;
        vm.vacancy = undefined;
        vm.userprofile = undefined;
        vm.file = {};
        vm.fileTwo = null;
        vm.errorMessageShow = false;
        vm.errorMessage = null;
        vm.savingMessageShow = false;
        vm.successMessageShow = false;
        vm.isBusy = false;
        vm.average = 0;
        vm.ShowError = false;
        vm.identitynumber = 0;
        vm.questionsCount = 0;

        vm.application = {

        }

        vm.genericQuestions = [];
        vm.answersGenericQuestions = [];
        vm.answersThreshold = null;
        vm.selectedAnswersGenericQuestions = {
            question: undefined,
            answer: undefined
        };

        vm.listOtherGenericAnswers = [];

        vm.listSelectedAnswersGenericQuestions = [];


        vm.specificQuestions = [];
        vm.answersSpecificQuestions = [];
        vm.selectedAnswersSpecificQuestions = {
            question: undefined,
            answer: undefined
        };

        vm.isProfileComplete = false;

        vm.listSelectedAnswersSpecificQuestions = [];

        vm.reference = undefined;

        vm.applicationRequirement = {};
        vm.applicantApplications = {};

        vm.experience = [
            "0 - 3 years",
            "3 - 5 years",
            "5 - 7 years",
            "7 - 9 years",
            "10 years or more"];

        vm.mangExperience = [
            "0 - 3 years",
            "3 - 5 years",
            "5 - 7 years",
            "7 - 9 years",
            "10 years or more"];

        vm.qualification = [
            "No Matric",
            "Matric",
            "Post-matric Certificate",
            "National Diploma",
            "B-Degree",
            "Post-graduate"];

        vm.southAfricanCitizen = [
            "No",
            "Yes"];

        vm.training = [
            "No",
            "Yes"];

        vm.knowledge = [
            "No",
            "Yes"];

        vm.getKey = function (key) {
            return parseInt(key, 10);
        };

        vm.applicantDeclaration = {
            applicantProfileId: undefined,
            acceptedDeclaration: false,
            acceptedDeclarationOn: new Date()
        };

        vm.initialisePage = function () {
            vm.isBusy = false;
            var authData = $localStorage.authorizationData; //  JSON.parse($sessionStorage.get('authorizationData'));
            if (authData == null) {
                $location.path('/login');
            }

            vm.vacancy = browseJobsService.getCurrentVacancy();


            if (vm.vacancy == undefined)
                $location.path('/browseJobs');

            applyService.getAllQuestionsAndAnswers(vm.vacancy).then(function (data) {

                //vm.questionsCount = data.data.genericQuestionsAnswers.length + data.data.specificQuestionsAnswers;
                vm.genericQuestions = data.data.genericQuestionsAnswers;
                vm.specificQuestions = data.data.specificQuestionsAnswers;
                vm.answersThreshold = data.data.answersThreshold;
            });

            profileService.getProfileInitialData().then(function (response) {
                if (!response.applicantDeclaration) {

                    vm.errorMessage = "It seems that your profile is incomplete, please complete your profile before submitting your application. You will be redirected to your profile shortly.";
                    vm.errorMessageShow = true;
                    $timeout(function () {

                        $location.path('/viewProfile');
                    }, 10000);
                    return;
                }

                if (response.applicantLangProficiencies.length == 0) {

                    vm.errorMessage = "Please provide atleast 1 language proficiency. You will be redirected to your profile shortly.";
                    vm.errorMessageShow = true;
                    $timeout(function () {

                        $location.path('/viewProfile');
                    }, 10000);
                    return;
                }

                if (response.applicantQualifications.length == 0) {

                    vm.errorMessage = "Please complete your qualifications information.";
                    vm.errorMessageShow = true;
                    $timeout(function () {

                        $location.path('/viewProfile');
                    }, 10000);
                    return;
                }

                //if (response.applicantExperiences.length == 0) {

                //    vm.errorMessage = "Please complete your work experience information.";
                //    vm.errorMessageShow = true;
                //    $timeout(function () {

                //        $location.path('/viewProfile');
                //    }, 10000);
                //    return;
                //}

                if (response.applicantReferences.length == 0) {

                    vm.errorMessage = "Please complete your reference information.";
                    vm.errorMessageShow = true;
                    $timeout(function () {

                        $location.path('/viewProfile');
                    }, 10000);
                    return;
                }

                if (response.attachments == null) {

                    vm.errorMessage = "Please attach required documents.";
                    vm.errorMessageShow = true;
                    $timeout(function () {

                        $location.path('/viewProfile');
                    }, 10000);
                    return;
                } else {
                    if (response.attachments.length < 4) {
                        vm.errorMessage = "Please attach required documents.";
                        vm.errorMessageShow = true;
                        $timeout(function () {

                            $location.path('/viewProfile');
                        }, 10000);
                        return;
                    }
                }

                

                vm.documentTypes = response.documentTypes;
                vm.applicantAttachments = response.attachments;
                vm.applicantApplications = response.applicantApplications;
                vm.userprofile = response.profile;
                vm.identityNumber = response.profile.identityNumber;

                if (vm.appliedForVacancy(vm.vacancy.vacancyId, vm.applicantApplications)) {
                    vm.errorMessage = "You have already applied for this vacancy. You will be redirected to the \"My Applications\" function.";
                    vm.errorMessageShow = true;
                    $timeout(function () {
                        $location.path('/viewApplications');
                    }, 5000);

                }
                vm.ShowError = true;

            }, function (error) {

            });
        };
        vm.appliedForVacancy = function (vacancyId, applicantApplications) {
            var found = false;
            angular.forEach(applicantApplications, function (application, key) {
                if (application.vacancyId === vacancyId) {
                    found = true;
                }
            });
            return found;
        };

        vm.addDeclaration = function () {
            var declaration = vm.applicantDeclaration;

            if (vm.frmDeclaration.$invalid) {
                vm.showError("Please accept the declaration by clicking the \"I DECLARE\" box.", true);
                return false;
            }
            var d = new Date(declaration.acceptedDeclarationOn);
            d.setHours(0, -d.getTimezoneOffset(), 0, 0);
            declaration.acceptedDeclarationOn = d;

            declaration.applicantProfileId = vm.userprofile.applicantProfileId;
            profileService.addDeclaration(declaration).then(function (response) {
            }, function (error) {

                });

            return true;
        };

        vm.clearError = function () {
            vm.errorMessageShow = false;
            vm.errorMessage = null;
        };
        vm.showError = function (message, autoHide) {
            vm.errorMessageShow = true;
            vm.errorMessage = message;
            if (autoHide) {
                $timeout(function () {
                    vm.clearError();
                }, 10000);
            }

        };

        vm.genericQuestionAnswerChange = function (question, answer, index) {

            var obj = {
                question: question.genericQuestion.question,
                AnswersGenericQuestionsId: answer,
                AnswersSpecificQuestionsId: 0,
                ProgrammeId: 0,
                SpecifyAnswer: ''
            };

            var exists = false;

            for (var i = 0; i < vm.listSelectedAnswersGenericQuestions.length; i++) {
                if (vm.listSelectedAnswersGenericQuestions[i].question == obj.question) {
                    exists = true;
                    vm.listSelectedAnswersGenericQuestions[i].AnswersGenericQuestionsId = answer;
                }
            }

            if (!exists) {
                vm.listSelectedAnswersGenericQuestions.push(obj);
            }

            var selectedAnswer = vm.getAnswerTitle(answer, question);
            var otherObj = {
                question: question.genericQuestion.question,
                index: index,
                answer: undefined,
                showOther: false,
                specifyAnswer: ''
            };

            if (selectedAnswer == 'Other') {

                if (vm.checkIfExist(index)) {
                    vm.listOtherGenericAnswers[index].answer = selectedAnswer;
                    vm.listOtherGenericAnswers[index].index = index;
                    vm.listOtherGenericAnswers[index].showOther = true;
                }
                else {
                    otherObj.answer = selectedAnswer;
                    otherObj.showOther = true;

                    vm.listOtherGenericAnswers.push(otherObj);
                    console.log(vm.listOtherGenericAnswers);
                }
            } else {

                for (var i = 0; i < vm.listOtherGenericAnswers.length; i++) {

                    if (index == vm.listOtherGenericAnswers[i].index) {
                        vm.listOtherGenericAnswers[i].answer = selectedAnswer;
                        vm.listOtherGenericAnswers[i].showOther = false;
                    }
                }
                console.log(vm.listOtherGenericAnswers);

                //var index = vm.listOtherGenericAnswers.indexOf(question.genericQuestion.id)
                //vm.listOtherGenericAnswers.splice(index);
                //vm.showOtherControl = false;
                //vm.genericQuestionId = question.genericQuestion.id;
            }
        }

        vm.checkIfExist = function (index) {

            var exists = false;

            for (var i = 0; i < vm.listOtherGenericAnswers.length; i++) {
                if (vm.listOtherGenericAnswers[i].index == index) {
                    exists = true;
                    break;
                }
            }
            return exists;
        }

        vm.specificQuestionAnswerChange = function (question, answer) {

            var obj = {
                question: question.specificQuestion.question,
                AnswersGenericQuestionsId: 0,
                AnswersSpecificQuestionsId: answer,
                ProgrammeId: 0,
                SpecifyAnswer: ''
            };

            var exists = false;

            for (var i = 0; i < vm.listSelectedAnswersSpecificQuestions.length; i++) {
                if (vm.listSelectedAnswersSpecificQuestions[i].question == obj.question) {
                    exists = true;
                    vm.listSelectedAnswersSpecificQuestions[i].AnswersSpecificQuestionsId = answer;
                }
            }

            if (!exists) {
                vm.listSelectedAnswersSpecificQuestions.push(obj);
            }
        }

        vm.remove = function (item, items) {
            // console.log(items);
            items.splice(items.indexOf(item), 1);
            items.forEach(function (elem) {
                elem.id = items.indexOf(elem) + 1;
            });
        };


        //vm.remove = function (item) {
        //    var index = vm.listSelectedAnswersGenericQuestions.indexOf(item)
        //    vm.listSelectedAnswersGenericQuestions.splice(index, 1);
        //}

        vm.getAnswerTitle = function (id, question) {

            var answers = question.listAnswersGenericQuestions;
            for (var i = 0; i < answers.length; i++) {

                if (answers[i].id == id) {
                    return answers[i].answer;
                }
            }
        }      

        vm.sendApplication = function () {
            if (vm.isBusy == true) {
                return;
            }

            if (!vm.addDeclaration()) {
                return;
            }

            vm.isBusy = true;
            vm.savingMessageShow = true;
            vm.successMessageShow = false;

            var idNumber = vm.identityNumber;

            var tempDate = new Date(idNumber.substring(0, 2), idNumber.substring(2, 4) - 1, idNumber.substring(4, 6));

            var age = getAge(tempDate);

            //vm.requirement.age = age;
            for (var i = 0; i < vm.listOtherGenericAnswers.length; i++) {

                for (var j = 0; j < vm.listSelectedAnswersGenericQuestions.length; j++) {

                    if (vm.listOtherGenericAnswers[i].question == vm.listSelectedAnswersGenericQuestions[j].question) {
                        vm.listSelectedAnswersGenericQuestions[j].SpecifyAnswer = vm.listOtherGenericAnswers[i].specifyAnswer;
                    }
                }

            }

            var combinedQuestions = [].concat(vm.listSelectedAnswersGenericQuestions, vm.listSelectedAnswersSpecificQuestions);

            var application = {
                VacancyId: vm.vacancy.vacancyId,
                ApplicantProfileId: vm.userprofile.applicantProfileId,
                applicationRequirement: vm.applicationRequirement,
                requirements: combinedQuestions
            };

            //application.requirement.overallAverage = 0;
            //application.requirement.programmeId = 28;

            var applicationModels = {
                genericQuestionsAnswers: vm.genericQuestions,
                specificQuestionsAnswers: vm.specificQuestions,
                application: application,
                answersThreshold: vm.answersThreshold
            };            

            applyService.sendApplication(applicationModels).then(function (response) {
                vm.savingMessageShow = false;
                vm.successMessageShow = true;
                $timeout(function () {

                    $location.path('/browseJobs');
                }, 10000);
            }, function (error) {
                vm.isBusy = false;
            });
        };

        vm.onLoad = function (e, reader, file, fileList, fileOjects, fileObj) {
            vm.file = fileObj;
            vm.fileTwo = file;
        };

        vm.showAlert = function (ev) {

            $mdDialog.show({
                contentElement: '#myDialog',
                parent: angular.element(document.querySelector('body')),
                targetEvent: ev,
                clickOutsideToClose: true
            });
        };

        var getAge = function (birthDate) {
            var today = new Date();
            var age = today.getFullYear() - birthDate.getFullYear();
            var m = today.getMonth() - birthDate.getMonth();
            if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
                age--;
            }

            if (age > 100)
                age = age - 100;

            return age;
        };


        vm.SaveFile = function (type) {

            var r = new FileReader();
            r.onloadend = function (e) {

                var filedata = new Uint8Array(e.target.result);
                var applicantAttachments = {
                    applicantProfileId: vm.userprofile.applicantProfileId,
                    documentTypeId: type.documentTypeId,
                    //documentFormat: undefined,
                    documentData: arrayBufferToBase64String(filedata),
                    documentFormat: vm.file.filetype,
                    filePath: undefined,
                    localFileName: undefined,
                    originalFileName: vm.file.filename,
                    applicantProfile: undefined,
                    attachmentId: undefined
                };
                var fileToReplace = $filter('filter')(vm.applicantAttachments, { documentTypeId: type.documentTypeId })[0];
                if (fileToReplace) {
                    applicantAttachments.attachmentId = fileToReplace.attachmentId;
                }
                profileService.SaveFile(applicantAttachments).then(function (response) {
                    vm.applicantAttachments = response;
                    vm.initialisePage();
                }, function (error) {

                });
            }
            r.readAsArrayBuffer(vm.fileTwo);
        };

        vm.convertToBoolean = function (bool) {

            return bool === 'Yes' ? true : false;
        }

        function arrayBufferToBase64String(buffer) {
            let binaryString = ''
            var bytes = new Uint8Array(buffer);
            for (var i = 0; i < bytes.byteLength; i++) {
                binaryString += String.fromCharCode(bytes[i]);
            }

            return window.btoa(binaryString);
        }

        vm.viewFile = function (type) {
            var fileToView = $filter('filter')(vm.applicantAttachments, { documentTypeId: type.documentTypeId })[0];
            applyService.getAttachment(fileToView);
        };
        vm.getFileName = function (type) {
            var fileToView = $filter('filter')(vm.applicantAttachments, { documentTypeId: type.documentTypeId })[0];
            if (fileToView) {
                return fileToView.originalFileName;
            }
            return "";
        };
    };

    angular.module('eRecruitmentApp').controller('applyCareerController', applyCareerController);
    applyCareerController.$inject = ['$localStorage', 'applyService', 'browseJobsService', 'createProfileService', 'Upload', '$timeout', '$filter', '$location', 'profileService', '$mdDialog'];
})();