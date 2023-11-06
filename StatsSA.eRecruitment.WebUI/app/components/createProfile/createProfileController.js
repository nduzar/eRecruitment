(function () {
    'use strict';

    var createProfileController = function ($localStorage, createProfileService, authService, WizardHandler, Upload, $timeout, $filter, $location) {
        var vm = this;
        vm.hideIndicators = true;
        vm.appCount = 0;
        vm.roleCount = 0;
        vm.declarationDate = new Date();
        vm.files = [];
        vm.races = [];
        vm.genders = [];
        vm.contactMethods = [];
        vm.languages = [];
        vm.hasLanguages = true;
        vm.hasExperience = true;
        vm.hasReferences = true;
        vm.hasQualifications = true;
        vm.languageProficiencies = [];
        vm.progress = 0;
        vm.langProfStaticModel= {
            language : null,
            read : null,
            write : null,
            speak: null
        }
        vm.newQualificationModel = {
            applicationProfileId: undefined,
            institution: null,
            qualificationTypeId: null,
            nameOfQualification: null,
            yearObtained: null,
            currentStudies: false
        };
        vm.newExperienceModel = {
            applicantProfileId: null,
            employer: null,
            postHeld: null,
            dateFrom: null,
            dateTo: null,
            reasonForLeaving: null,
            maintUser: null,
            maintDate: null
        };
        vm.newReferencesModel = {
            applicantProfileId: null,
            referenceName: null,
            referenceRelationship: null,
            referenceContactNumber: null,
            maintUser: null,
            maintDate: null
        };
        vm.qualificationTypes = [];
        vm.applicantProfile = [];
        vm.loggedIn = false;
        vm.ErrorMessage = null;
        vm.ShowError = false;
        vm.SuccessMessage = null;
        vm.ShowSuccess = false;
        var authData = null;
        vm.loginData = {
            userName: "",
            password: "",
            useRefreshTokens: false
        };
        vm.id = null;
        vm.seniorCertificate = null;
        vm.tertiaryQualification = null;
        vm.cv = null;

        vm.dateControl = [];
        vm.dateControl.inlineOptions = {
            showWeeks: false
        };
        vm.dateControl.dateOptions = {
            formatYear: 'yyyy',
            startingDay: 1
        };
        vm.dateControl.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        vm.dateControl.format = vm.dateControl.formats[1];
        vm.dateControl.state = [{opened: false},{opened: false},{opened: false},{opened: false}];
        vm.dateControl.open = function (i) {
            vm.dateControl.state[i].opened = true;
        };

        vm.initialisePage = function () {
            authData = $localStorage.authorizationData;  //JSON.parse($sessionStorage.get('authorizationData'));
            createProfileService.getRaces().then(function (response) {
                vm.races = response.data;
            }, function () { console.log(JSON.stringify(error)); });
            createProfileService.getGenders().then(function (response) {
                vm.genders = response.data;
            }, function () { console.log(JSON.stringify(error)); });
            createProfileService.getContactMethods().then(function (response) {
                vm.contactMethods = response.data;
            }, function () { console.log(JSON.stringify(error)); });
            createProfileService.getLanguages().then(function (response) {
                vm.languages = response.data;
            }, function () { console.log(JSON.stringify(error)); });
            createProfileService.getLanguageProficiencies().then(function (response) {
                vm.languageProficiencies = response.data;
            }, function () { console.log(JSON.stringify(error)); });
            createProfileService.getQualificationTypes().then(function (response) {
                vm.qualificationTypes = response.data;
            }, function () { console.log(JSON.stringify(error)); });
            createProfileService.getUser().then(function (response) {
                vm.applicantProfile = response.data;
                var currentFiles = vm.applicant.applicantProfiles[0].applicantAttachments;
                vm.id = $filter('filter')(currentFiles, { documentTypeId: 1, isNew: true })[0];
                vm.seniorCertificate = $filter('filter')(currentFiles, { documentTypeId: 2 })[0];
                vm.tertiaryQualification = $filter('filter')(currentFiles, { documentTypeId: 3 })[0];
                vm.cv = $filter('filter')(currentFiles, { documentTypeId: 5 })[0];
                if (response.data != null) {
                    vm.applicantProfile.applicantProfiles[0].dateOfBirth = new Date(vm.applicantProfile.applicantProfiles[0].dateOfBirth);
                    vm.applicantProfile.applicantProfiles[0].occupationRegistrationDate = new Date(vm.applicantProfile.applicantProfiles[0].occupationRegistrationDate);
                }
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        };

        vm.saveUser = function (newProfile) {
            newProfile.createdBy = newProfile.userName;
            newProfile.ModifiedBy = newProfile.userName;
            
            createProfileService.saveNewUser(newProfile).then(function (response) {
                vm.loginData.userName = newProfile.email == null ? newProfile.cellphone : newProfile.email;
                vm.loginData.password = newProfile.password;
                vm.applicantProfile.id = response.data.id;
                vm.applicantProfile.applicantProfiles = response.data.applicantProfiles;
                authService.login(vm.loginData).then(function (response) {
                    vm.loggedIn = true;
                    vm.ErrorMessage = null;
                    vm.ShowError = false;
                    WizardHandler.wizard().next();
                }, function (response) {
                    vm.ErrorMessage = response.data.error_description;
                    vm.ShowError = true;
                });
            }, function (error) {
                //console.log("SAVE USER ERROR: " + JSON.stringify(error));
                vm.ErrorMessage = error.data.exceptionMessage;
                vm.ShowError = true;
            });
        };
        vm.saveProfile = function (user) {
            var profile = user.applicantProfiles[0];
            profile.userId = user.id;
            profile.maintUser = user.userName;
            createProfileService.saveNewProfile(profile).then(function (response) {
                vm.applicantProfile.applicantProfiles[0] = response.data;
                if (response.data != null) {
                    vm.applicantProfile.applicantProfiles[0].dateOfBirth = new Date(vm.applicantProfile.applicantProfiles[0].dateOfBirth);
                    vm.applicantProfile.applicantProfiles[0].occupationRegistrationDate = new Date(vm.applicantProfile.applicantProfiles[0].occupationRegistrationDate);
                }
                vm.ShowError = false;
                WizardHandler.wizard().next();
            }, function (error) {
                vm.ErrorMessage = error.data.exceptionMessage;
                vm.ShowError = true;
            });
        };

        vm.addQualification = function (qualification) {
            qualification.applicantProfileId = vm.applicantProfile.applicantProfiles[0].applicantProfileId;
            createProfileService.addQualifications(qualification).then(function (response) {
                vm.hasQualifications = true;
                vm.applicantProfile.applicantProfiles[0].applicantQualifications = response.data;
                vm.newQualificationModel.applicationProfileId= undefined;
                vm.newQualificationModel.institution = null;
                vm.newQualificationModel.qualificationTypeId = 0;
                vm.newQualificationModel.nameOfQualification = null;
                vm.newQualificationModel.yearObtained = null;
                vm.newQualificationModel.currentStudies = false;
            }, function (error) {
                console.log("SAVE QUALIFICATION ERROR: " + JSON.stringify(error));
            });
        };
        vm.removeQualification = function (qualification) {
            createProfileService.removeQualifications(qualification).then(function (response) {
                vm.hasQualifications = (response.data != null && response.data.length > 0);
                vm.applicantProfile.applicantProfiles[0].applicantQualifications = response.data;
            }, function (error) {
                console.log("SAVE QUALIFICATION ERROR: " + JSON.stringify(error));
            });
        };

        vm.addExperience = function (experience) {
            experience.applicantProfileId = vm.applicantProfile.applicantProfiles[0].applicantProfileId;
            createProfileService.addExperience(experience).then(function (response) {
                vm.hasExperience = true;
                console.log(response.data);
                vm.applicantProfile.applicantProfiles[0].applicantExperience = response.data;
                vm.newExperienceModel.employer = null;
                vm.newExperienceModel.postHeld = null;
                vm.newExperienceModel.dateFrom = null;
                vm.newExperienceModel.dateTo = null;
                vm.newExperienceModel.reasonForLeaving = null;
            }, function (error) {
                console.log("SAVE EXPERIENCE ERROR: " + JSON.stringify(error));
            });
        };
        vm.removeExperience = function (experience) {
            createProfileService.removeExperience(experience).then(function (response) {
                vm.hasExperience = (response.data.length >0);
                vm.applicantProfile.applicantProfiles[0].applicantExperience = response.data;
            }, function (error) {
                console.log("SAVE EXPERIENCE ERROR: " + JSON.stringify(error));
            });
        };

        vm.addReference = function (reference) {
            reference.applicantProfileId = vm.applicantProfile.applicantProfiles[0].applicantProfileId;
            createProfileService.addReference(reference).then(function (response) {
                vm.applicantProfile.applicantProfiles[0].applicantReferences = response.data;
                vm.hasReferences = true;
            }, function (error) {
                console.log("SAVE REFERENCE ERROR: " + JSON.stringify(error));
            });
        };
        vm.removeReference = function (reference) {
            createProfileService.removeReference(reference).then(function (response) {
                vm.applicantProfile.applicantProfiles[0].applicantReferences = response.data;
            }, function (error) {
            });
        };

        vm.addLangProficiency = function (proficiency) {
            var actualObject =
                  {
                      "applicantProfileId": vm.applicantProfile.applicantProfiles[0].applicantProfileId,
                      "languageId": proficiency.language,
                      "readProficiencyId": proficiency.read,
                      "speakProficiencyId": proficiency.speak,
                      "writeReadProficiencyId": proficiency.write
                  };
            createProfileService.addLangProficiency(actualObject).then(function (response) {
                vm.applicantProfile.applicantProfiles[0].applicantLangProficiencies = response.data;
                vm.hasLanguages = true;
                vm.langProfStaticModel.language = null;
                vm.langProfStaticModel.read = null;
                vm.langProfStaticModel.write = null;
                vm.langProfStaticModel.speak = null;
            }, function (error) {
            });
        };
        vm.removeLangProficiency = function (proficiency) {
            createProfileService.removeLangProficiency(proficiency).then(function (response) {
                vm.applicantProfile.applicantProfiles[0].applicantLangProficiencies = response.data;
            }, function (error) {
            });
        };

        vm.addProhibition = function (prohibition) {
            var newProhib = {
                applicantProfileId: vm.applicantProfile.applicantProfiles[0].applicantProfileId,
                publicServiceProhibition: prohibition.publicServiceProhibition,
                departmentName: prohibition.departmentName,
                maintUser: null,
                mainDate: new Date()
            };
            createProfileService.addProhibition(newProhib).then(function (response) {
                vm.applicantProfile.applicantProfiles[0].applicantPubServProhibition = response.data;
                WizardHandler.wizard().next();
            }, function (error) {
                console.log("SAVE PROHIBITION ERROR: " + JSON.stringify(error));
            });
        };
        vm.addDeclaration = function (declaration) {
            declaration.applicantProfileId = vm.applicantProfile.applicantProfiles[0].applicantProfileId;
            createProfileService.addDeclaration(declaration).then(function (response) {
                vm.applicantProfile.applicantProfiles[0].applicantDeclaration = response.data;
                vm.SuccessMessage = "Your profile was successfully saved. You'll be redirected to the landing pages shortly."
                vm.ShowSuccess = true;
                vm.redirect();

            }, function (error) {
                console.log("SAVE DECLARATION ERROR: " + JSON.stringify(error));
            });
        };
        vm.saveContactInfo = function (contactInfo) {
            contactInfo.applicantProfileId = vm.applicantProfile.applicantProfiles[0].applicantProfileId;
            createProfileService.addContactDetails(contactInfo).then(function (response) {
                vm.applicantProfile.applicantProfiles[0].applicantContactDetails[0] = response.data;
                if (vm.applicantProfile.applicantProfiles[0].applicantLangProficiencies == undefined || vm.applicantProfile.applicantProfiles[0].applicantLangProficiencies == null) {
                    //vm.ErrorMessage = "Please complete the Language Proficiency portion of this form.";
                    // vm.ShowError = true;
                    
                }
                else {
                    if (vm.applicantProfile.applicantProfiles[0].applicantLangProficiencies.length > 0) {
                       // vm.ErrorMessage = "Please complete the Language Proficiency portion of this form.";
                        // vm.ShowError = true;
                        WizardHandler.wizard().next();
                    }
                    else {
                        console.log("Proficiency Lenght Error: "+ vm.applicantProfile.applicantProfiles[0].applicantLangProficiencies.length)
                    }
                }
                
            }, function (error) {
                console.log("SAVE CONTACT INFO ERROR: " + JSON.stringify(error));
            });
        };

        vm.addAttachment = function (attachment) {
            //attachment.applicantProfileId = vm.applicantProfile.applicantProfiles[0].applicantProfileId;
            //createProfileService.addAttachment(attachment).then(function (response) {
            //    console.log("RESULTS FOR ATTACHMENT ADD " + JSON.stringify(response));
            //    vm.applicantProfile.applicantProfiles[0].applicantAttachments = response.data;
            //}, function (error) {
            //    console.log("SAVE ATTACHMENT ERROR: " + JSON.stringify(error));
            //});


        };
        vm.removeAttachment = function (attachment) {
            //attachment.applicantProfileId = vm.applicantProfile.applicantProfiles[0].applicantProfileId;
            createProfileService.removeAttachment(attachment).then(function (response) {
                console.log("RESULTS FOR ATTACHMENT REMOVE " + JSON.stringify(response));
                vm.applicantProfile.applicantProfiles[0].applicantAttachments = response.data;
            }, function (error) {
                console.log("REMOVE ATTACHMENT ERROR: " + JSON.stringify(error));
            });
        };

        vm.uploadFileClick = function (files, currentFile, documentTypeId) {
            if (files && files.length) {

                var uploadDataModel = {
                    applicantProfileId: vm.userprofile.applicantProfileId,
                    applicantAttachmentId: (currentFile) ? currentFile.applicantAttachmentId : 0,
                    documentTypeId: documentTypeId
                };

                Upload.upload({
                    url: 'http://10.120.50.210:9003/api/fileupload/uploadfiles',
                    data: { uploadDataModel: uploadDataModel, files: files }
                }).then(function (response) {
                    $timeout(function () {
                        vm.result = response.data;
                        vm.initialisePage();
                    });
                }, function (response) {
                    if (response.status > 0) {
                        vm.errorMsg = response.status + ': ' + response.data;
                    }
                }, function (evt) {
                    vm.progress =
                        Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                    console.log(vm.progress);
                });
            }
        };

        vm.citizenChange = function () {
            if (vm.applicantProfile.applicantProfiles[0].citizen == "true") {
                vm.applicantProfile.applicantProfiles[0].nationality = "South African";
            }
            else {
                vm.applicantProfile.applicantProfiles[0].nationality = null;
            }
        }
        vm.showHideList = function (list) {
            return true; 
        }
        
        vm.redirect = function () {
            $timeout(function () {
                $location.path('/browseJobs');
            }, 8000);
        }
        vm.getKey = function (key) {
            // 10 is the radix, which is the base (assumed to be base 10 here) 
            return parseInt(key, 10);
        }

        var authData = $localStorage.authorizationData; //  JSON.parse($sessionStorage.get('authorizationData'));
        if (authData != null) {
            vm.loggedIn = true;
        }
        else {
            vm.loggedIn = false;
            console.log("User not logged in");
        }
    }

    angular.module('eRecruitmentApp').controller('createProfileController', createProfileController);
    createProfileController.$inject = ['$localStorage', 'createProfileService', 'authService', 'WizardHandler', 'Upload', '$timeout', '$filter','$location'];
})();