(function () {
    'use strict';

    var applyController = function (applyService, browseJobsService, createProfileService, Upload, $timeout, $filter, $location, profileService, $mdDialog) {
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
        vm.calculatedAge = 0;

        vm.application = {

        }

        vm.getGenericQuestions = [];

        vm.reference = undefined;

        vm.applicationRequirement = {};
        vm.programmes = {};
        vm.programme = null;

        vm.applicantApplications = {};
        
        vm.qualification = [
            //{ answerScore: 0, answerDesc: "No Matric" },
            //{ answerScore: 1, answerDesc: "Matric" },
            //{ answerScore: 2, answerDesc: "Post-matric Certificate" },
            "National Diploma" ,
            "B-Degree" ,
            "Post-graduate" ];  

        vm.southAfricanCitizen = [
            "No" ,
            "Yes" ];


        vm.age = [
            "No" ,
            "Yes"];

        vm.training = [
            "No" ,
            "Yes"];

        vm.knowledge = [
             "No" ,
            "Yes" ];

        vm.internship = [
            "No" ,
            "Yes" ];

        vm.getKey = function (key) {
            return parseInt(key, 10);
        };
        vm.initialisePage = function () {
            vm.isBusy = false;
            vm.vacancy = browseJobsService.getCurrentVacancy();
            profileService.getProfileInitialData().then(function (response) {
                if (!response.applicantDeclaration) {

                    vm.errorMessage = "It seems that your profile is incomplete, please complete your profile before submitting your application. You will be redirected to your profile shortly.";
                    vm.errorMessageShow = true;
                    $timeout(function () {

                        $location.path('/viewProfile');
                    }, 10000);
                    return;
                }
                else {
                    var idNumber = response.profile.identityNumber;

                    var tempDate = new Date(idNumber.substring(0, 2), idNumber.substring(2, 4) - 1, idNumber.substring(4, 6));

                    var age = getAge(tempDate);

                    vm.calculatedAge = age;

                    var dateOfBirth = response.profile.identityNumber;

                    if (age < 18) {
                        vm.errorMessage = "Internship programme is for candidate above the age of 18 ";
                        vm.errorMessageShow = true;
                        return;
                    }

                    if (age > 35) {
                        vm.errorMessage = "Internship programme is for candidate below the age of 35 ";
                        vm.errorMessageShow = true;
                        return;
                    }


                    //if(response.profile)
                    //vm.errorMessage = "It seems that your profile is incomplete, please complete your profile before submitting your application. You will be redirected to your profile shortly.";
                    //vm.errorMessageShow = true;
                    //$timeout(function () {

                    //    $location.path('/viewProfile');
                    //}, 10000);
                    //return;
                }

                vm.documentTypes = response.documentTypes;
                vm.applicantAttachments = response.attachments;
                vm.applicantApplications = response.applicantApplications;
                vm.userprofile = response.profile;
                vm.programmes = response.programmes;

                if (vm.appliedForVacancy(vm.vacancy.vacancyId, vm.applicantApplications)) {
                    vm.errorMessage = "You have already applied for this vacancy. You will be redirected to the \"My Applications\" function.";
                    vm.errorMessageShow = true;
                    $timeout(function () {
                        $location.path('/viewApplications');
                    }, 5000);

                }

                //applyService.getGenericQuestions(vm.vacancy.VacancyId).then(function (response) {

                //    console.log(response.data);
                //});

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

        

        vm.updateReference = function (event) {
            vm.reference = $filter('filter')(vm.programmes, { referenceNo: event })[0];
        }
        vm.sendApplication = function () {
            if (vm.isBusy == true) {
                return;
            }

            if (vm.average < 60) {
                vm.errorMessage = "You minimum average required is 60%";
                vm.ShowError = true;
                return;
            }

            vm.isBusy = true;
            vm.savingMessageShow = true;
            vm.successMessageShow = false;
            var application = {
                VacancyId: vm.vacancy.vacancyId,
                ApplicantProfileId: vm.userprofile.applicantProfileId,
                applicationRequirement: null,
                requirement: vm.requirement,
                programme: vm.reference
            };

            vm.requirement.programmeId = vm.reference.programmeId;
            vm.requirement.overallAverage = vm.average;
            vm.requirement.age = vm.calculatedAge;
            vm.requirement.hasKnowledge = false;
            vm.requirement.managementExperience = false;
            vm.requirement.hasTraining = false;

            applyService.sendApplication(application).then(function (response) {
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




        // vm.SaveFile = function (type) {

        //var r = new FileReader();
        //r.onloadend = function (e) {

        //    var filedata = new Uint8Array(e.target.result);
        //    var applicantAttachments = {
        //        applicantProfileId: vm.userprofile.applicantProfileId,
        //        documentTypeId: type.documentTypeId,
        //        //documentFormat: undefined,
        //        documentData: uint8ArrayToArray(filedata),
        //        documentFormat: vm.file.filetype,
        //        filePath: undefined,
        //        localFileName: undefined,
        //        originalFileName: vm.file.filename,
        //    };
        //    var fileToReplace = $filter('filter')(vm.applicantAttachments, { documentTypeId: type.documentTypeId })[0];
        //    if (fileToReplace) {
        //        applicantAttachments.applicantAttachmentId = fileToReplace.applicantAttachmentId;
        //    }
        //    profileService.SaveFile(applicantAttachments).then(function (response) {
        //        vm.initialisePage();
        //    }, function (error) {

        //    });
        //}
        //r.readAsArrayBuffer(vm.fileTwo);

        //    };

        function arrayBufferToBase64String(buffer) {
            let binaryString = ''
            var bytes = new Uint8Array(buffer);
            for (var i = 0; i < bytes.byteLength; i++) {
                binaryString += String.fromCharCode(bytes[i]);
            }

            return window.btoa(binaryString);
        }

        var uint8ArrayToArray = function (uint8Array) {
            var array = [];

            for (var i = 0; i < uint8Array.byteLength; i++) {
                array[i] = uint8Array[i];
            }

            return array;
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

        vm.convertBoolean = function (bool) {
            return bool === 'Yes' ? true : false;
        }

    };



    angular.module('eRecruitmentApp').controller('applyController', applyController);
    applyController.$inject = ['applyService', 'browseJobsService', 'createProfileService', 'Upload', '$timeout', '$filter', '$location', 'profileService', '$mdDialog'];
})();