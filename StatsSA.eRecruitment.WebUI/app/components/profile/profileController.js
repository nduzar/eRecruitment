(function () {
    'use strict';

    var profileController = function ($scope, profileService, WizardHandler, authService, $filter, $location, $routeParams, $localStorage, $timeout, Upload) {

        var vm = this;
        vm.isSavingProfile = false;
        var stepIndex = parseInt($routeParams.stepIndex);
        vm.selecteWorkPermit = undefined;
        vm.currenEmployment = false;
        vm.workPermitOptions = [
         { desc: "Yes", value: true },
         { desc: "No", value: false }
        ];

        vm.showOtherRace = false;
        vm.loading = false; 
        vm.contactEmail = "";
        vm.contactTelephone = "";
        vm.otherInstitution = "";
        vm.emailSameError = "";

        var wizardInit = true;
        $scope.$on('wizard:stepChanged', function () {
            if (wizardInit) {
                wizardInit = false;
                setTimeout(function () {
                    WizardHandler.wizard().goTo(stepIndex);
                    $scope.$apply();
                });
            }
        });


        vm.errorMessageShow = false;
        vm.errorMessage = null;

        vm.file = {};

        vm.popup = {
            opened: false
        };

        vm.open = function () {
            vm.popup.opened = true;
        };
        
        var todayDate = new Date();
        var day = todayDate.getDate();
        var month = todayDate.getMonth();
        var year = todayDate.getFullYear();
        var minYear = year - 65;
        vm.MaxQualDate = year;
        vm.dateOptions = {
            //dateDisabled: disabled,
            formatYear: 'yy',
            maxDate: new Date(year, month, day),
            minDate: new Date(minYear, 0, 1),
            startingDay: 1
        };

        vm.popupRegDate = {
            opened: false
        };

        vm.openRegDate = function () {
            vm.popupRegDate.opened = true;
        };

        vm.popupWorkExpDateTo = {
            opened: false
        };

        vm.openWorkExpDateTo = function () {
            vm.popupWorkExpDateTo.opened = true;
        };

        vm.popupWorkExpDateFrom = {
            opened: false
        };

        vm.openWorkExpDateFrom = function () {
            vm.popupWorkExpDateFrom.opened = true;
        };

        vm.popupDeclarationDate = {
            opened: false
        };

        vm.openDeclarationDate = function () {
            vm.popupDeclarationDate.opened = true;
        };

        var profileEditMode = false;

        var editWorkExperience = false;
        var editReference = false;
        vm.hideIndicators = false;
        vm.authentication = authService.authentication;
        vm.editApplicantProficiency = false;
        vm.editApplicantQualificationMode = false;
        vm.editWorkExperienceMode = false;
        vm.editReferenceMode = false;

        vm.contactMethods = [];
        vm.languages = [];
        vm.races = [];
        vm.genders = [];

        vm.languageProficiencies = [];
        vm.applicantCurrentQualifications = [];
        vm.applicantLangProficiencies = [];
        vm.applicantExperiences = [];
        vm.applicantReferences = [];
        vm.selectedProficienciesSpeak = undefined;
        vm.selectedProficienciesRead = undefined;
        vm.selectedProficienciesWrite = undefined;
        vm.selectedPreferredLanguage = undefined;
        vm.selectedContactMethod = undefined;
        vm.selectedProficiencies = [];
        vm.applicantCurrentQualifications = [];
        vm.institutions = {};

        vm.profile = {
            applicantProfileId: undefined,
            userId: undefined,
            surname: undefined,
            firstNames: undefined,
            race: undefined,
            gender: undefined,
            contactMethod: undefined,
            occupationRegistrationDate: undefined,
        };

        vm.user = {
            dateOfBirth: undefined,
            identityNumber: undefined,
            email: undefined,
            SecondaryEmail: undefined,
            userName: undefined
        };

        vm.frmAccount = {
            email: undefined,
            SecondaryEmail: undefined,
            userName: undefined
        }

        vm.proficiency = {};
        vm.qualification = {};
        vm.experience = {};
        vm.reference = {};
        vm.isGraduateNoExperienceClicked = false;
        vm.isCurrentEmploymentClicked = false;

        $scope.$watch('vm.editApplicantProficiency', function (newValue, oldValue) {
            if (newValue) {
                vm.profinciencyButtonText = "UPDATE";
                return;
            }
            vm.profinciencyButtonText = "ADD";
        });

        $scope.$watch('vm.editApplicantQualificationMode', function (newValue, oldValue) {
            if (newValue) {
                vm.qualificationButtonText = "UPDATE";
                return;
            }
            vm.qualificationButtonText = "ADD";
        });

        $scope.$watch('vm.editWorkExperienceMode', function (newValue, oldValue) {
            if (newValue) {
                vm.workExperienceButtonText = "UPDATE";
                return;
            }
            vm.workExperienceButtonText = "ADD";
        });

        $scope.$watch('vm.editReferenceMode', function (newValue, oldValue) {
            if (newValue) {
                vm.referenceButtonText = "UPDATE";
                return;
            }
            vm.referenceButtonText = "ADD";
        });

        var reset = function () {
            vm.frmLangProficiency.$submitted = false;
            vm.proficiency = {
                applicantLangProficiencyId: undefined,
                applicantProfileId: undefined,
                languageId: undefined,
                readProficiencyId: undefined,
                speakProficiencyId: undefined,
                writeReadProficiencyId: undefined
            };

            vm.qualification = {
                applicantQualificationId: undefined,
                applicantProfileId: undefined,
                institution: undefined,
                qualificationTypeId: undefined,
                nameOfQualification: undefined,
                yearObtained: undefined,
                currentStudies: undefined
            };

            vm.experience = {
                applicantExperienceId: undefined,
                applicantProfileId: undefined,
                employer: undefined,
                postHeld: undefined,
                dateFrom: undefined,
                dateTo: undefined,
                reasonForLeaving: undefined,
            };

            vm.reference = {
                applicantReferenceId: undefined,
                applicantProfileId: undefined,
                referenceName: undefined,
                referenceRelationship: undefined,
                referenceContactNumber: undefined
            }
        };


        vm.applicantContactDetail = {
            applicantContactDetailsId: undefined,
            applicantProfileId: vm.profile.applicantProfileId,
            languageId: undefined,
            language: undefined,
            cellNumber: undefined,
            contactMethodId: undefined,
            contactMethod: undefined,
            contactMethogDetails: undefined
        };

        vm.ApplicantPubServProhibition = {
            applicantProfileId: undefined,
            publicServiceProhibition: undefined,
            departmentName: undefined,
        };

        vm.applicantQualification = {
            applicantQualificationId: undefined,
            qualificationType: undefined,
            qualificationTypeId: undefined,
            applicantProfileId: undefined,
            institution: undefined,
            nameOfQualification: undefined,
            yearObtained: undefined,
            currentStudies: undefined
        }

        vm.applicantExperience = {
            applicantExperienceId: undefined,
            applicantProfileId: vm.profile.applicantProfileId,
            employer: undefined,
            postHeld: undefined,
            dateFrom: undefined,
            dateTo: undefined,
            reasonForLeaving: undefined,
        };

        vm.reference = {
            applicantReferenceId: undefined,
            applicantProfileId: undefined,
            referenceName: undefined,
            referenceRelationship: undefined,
            referenceContactNumber: undefined
        }

        vm.applicantDeclaration = {
            applicantProfileId: undefined,
            acceptedDeclaration: false,
            acceptedDeclarationOn: new Date()
        };

        vm.updateAccount = function () {
            if (vm.frmAccount.secondaryEmail == vm.frmAccount.email) {
                vm.emailSameError = "Primary email and Secondary email cannot be same."
                return;
            }
            vm.emailSameError = "";
            vm.loading = true;

            console.log(vm.frmAccount);
            let data = vm.frmAccount;
            data.Id = vm.frmAccount.userId;
            profileService.updateAccount(data, data.Id).then(function (response) {
                console.log('update', response)
                vm.loading = false;
                vm.initialisePage();
            });
        };

        vm.initialisePage = function () {
            vm.currenEmployment = false;
            vm.toDay = new Date();
            var d;
            vm.clearError();
            vm.selectedProficiencies.length = 0;
            vm.applicantCurrentQualifications.length = 0;
            if (vm.authentication.isAuth) {
                profileService.getProfileInitialData().then(function (response) {
                    if (response.profile) {
                        vm.profile = response.profile;
                        vm.user = response.user;
                        vm.frmAccount = response.user;
                        vm.frmAccount.userId = vm.profile.userId;
                        console.log(vm.frmAccount);
                        profileEditMode = true;
                        d = new Date(vm.profile.dateOfBirth);
                        //d.setHours(d.getHours(), d.getTimezoneOffset(), 0, 0);
                        vm.profile.dateOfBirth = d;

                    }
                    else {
                        vm.profile.identityNumber = response.user.identityNumber;
                        d = new Date(response.user.dateOfBirth);
                        // d.setHours(d.getHours(), d.getTimezoneOffset(), 0, 0);
                        vm.profile.dateOfBirth = d;

                    }
                    vm.races = response.races;
                    vm.genders = response.genders;
                    vm.institutions = response.institutions;
                    vm.contactMethods = response.contactMethods;
                    vm.languageProficiencies = response.languageProficiencies;
                    vm.languages = response.languages;
                    vm.profile.citizen = response.user.citizen == 1 ? true : false;
                    vm.Preferredlanguages = response.languages;
                    vm.qualificationTypes = response.qualificationTypes;
                    vm.applicantExperiences = response.applicantExperiences;
                    

                    if (response.profile != null) {

                        vm.profile.disabilityDesc = vm.profile.disability ? response.disabilityDesc : '';
                        vm.publicServiceDismiss = response.profile.publicServiceDismiss;
                        vm.profile.previousDismissedReason = response.profile.previousDismissedReason;
                        vm.profile.criminalOffenceDesc = vm.profile.criminalOffence ? response.profile.criminalOffenceDesc : '';
                        vm.profile.pendingCriminalDesc = vm.profile.pendingCriminalCase ? response.profile.pendingCriminalDesc : '';
                        vm.profile.publicServiceDismissDesc = vm.profile.publicServiceDismiss ? response.profile.publicServiceDismissDesc : '';
                        vm.profile.pendingDisciplinaryDesc = vm.profile.pendingDisciplinary ? response.profile.pendingDisciplinaryDesc : '';
                        vm.profile.resignedPendingDisciplinaryDesc = vm.profile.resignedPendingDisciplinary ? response.profile.resignedPendingDisciplinaryDesc : '';
                        vm.profile.businessWithStateDesc = vm.profile.businessWithState ? response.profile.businessWithStateDesc : '';


                        if (vm.profile.applicantProfileId) {
                            vm.firstName = vm.profile.firstNames;
                            vm.surname = vm.profile.surname;

                            var selectedRace = $filter('filter')(vm.races, { raceId: vm.profile.raceId })[0];
                            var selectedGender = $filter('filter')(vm.genders, { genderId: vm.profile.genderId })[0];
                            //var selectedInstitution = $filter('filter')(vm.institutions, { institutionId: vm.applicantCurrentQualifications.institution1 })[0];
                            vm.selecteWorkPermit = $filter('filter')(vm.workPermitOptions, { value: vm.profile.workPermit })[0];

                            vm.profile.race = selectedRace;
                            vm.profile.gender = selectedGender;
                            //  vm.institution = selectedInstitution.institutionName;
                            //console.log(vm.profile.occupationRegistrationDate);
                            if (vm.profile.occupationRegistrationDate) {
                                d = new Date(vm.profile.occupationRegistrationDate);
                                //d.setHours(d.getHours(), d.getTimezoneOffset(), 0, 0);
                                vm.profile.occupationRegistrationDate = d;
                            }
                            vm.proficiency.applicantProfileId = vm.profile.applicantProfileId;
                            vm.qualification.applicantProfileId = vm.profile.applicantProfileId;
                            vm.experience.applicantProfileId = vm.profile.applicantProfileId;
                        }
                    }                                       

                    if (response.applicantExperiences) {
                        angular.forEach(vm.applicantExperiences, function (experience) {
                            var df = new Date(experience.dateFrom);
                            //df.setHours(df.getHours(), df.getTimezoneOffset(), 0, 0);
                            experience.dateFrom = df;

                            if (experience.reasonForLeaving === "Current Employment") {
                                experience.dateTo = null;
                            } else {
                                var dt = new Date(experience.dateTo);
                                //dt.setHours(dt.getHours(), dt.getTimezoneOffset(), 0, 0);
                                experience.dateTo = dt;
                            }

                        });
                    }

                    vm.applicantReferences = response.applicantReferences;
                    vm.languageProficienciesRead = vm.languageProficiencies;
                    vm.languageProficienciesWrite = vm.languageProficiencies;
                    vm.languageProficienciesSpeak = vm.languageProficiencies;
                    vm.ApplicantPubServProhibition = response.applicantPubServProhibition;
                    vm.documentTypes = response.documentTypes;
                    vm.applicantAttachments = response.attachments;
                    vm.user = response.user;
                    vm.email = response.user.email;
                    vm.SecondaryEmail = response.user.secondaryEmail;
                    vm.pendingCriminalCase = response.pendingCriminalCase;
                    vm.resignedPendingDisciplinary = response.resignedPendingDisciplinary;
                    vm.dischargedOrRetired = response.dischargedOrRetired;
                    vm.businessWithState = response.businessWithState;
                    vm.relinquishBusinessInterest = response.relinquishBusinessInterest;
                    vm.numberOfExperiencePublic = response.numberOfExperiencePublic;
                    vm.numberOfExperiencePrivate = response.numberOfExperiencePrivate;

                    var applicantLangProficiencies = undefined;

                    if (response.applicantDeclaration) {
                        vm.applicantDeclaration = response.applicantDeclaration;
                        vm.applicantDeclaration.acceptedDeclarationOn = new Date();
                    }

                    if (response.applicantLangProficiencies.length) {
                        // console.log(JSON.stringify(vm.applicantLangProficiencies));
                        vm.applicantLangProficiencies = response.applicantLangProficiencies;
                    }

                    if (response.applicantQualifications.length) {
                        vm.applicantCurrentQualifications = response.applicantQualifications;
                    }

                    if (response.applicantContactDetail) {
                        vm.applicantContactDetail = response.applicantContactDetail;
                        vm.applicantContactDetail.language = $filter('filter')(vm.languages, { languageId: vm.applicantContactDetail.languageId })[0];
                        vm.applicantContactDetail.contactMethod = $filter('filter')(vm.contactMethods, { contactMethodId: vm.applicantContactDetail.contactMethodId })[0];
                        if (vm.applicantContactDetail.contactMethod.contactMethodId == 1) {
                            vm.contactEmail = vm.applicantContactDetail.contactMethogDetails;
                        } else {
                            vm.contactTelephone = vm.applicantContactDetail.contactMethogDetails
                        }
                    }                    

                }, function (error) {

                });
            }
        };

        vm.profile = {};
        vm.changeCurrentEmployment = function () {
            if (vm.currenEmployment) {
                vm.experience.reasonForLeaving = "Current Employment";
                vm.experience.dateTo = null;
                vm.isCurrentEmploymentClicked = true;
            }
            else {
                vm.experience.reasonForLeaving = null;
                vm.isCurrentEmploymentClicked = false;
            }
        };

        vm.changeGraduateNoExperience = function () {
            if (vm.graduateNoExperience) {
                vm.isGraduateNoExperienceClicked = true;
                vm.isCurrentEmploymentClicked = true;
            }
            else {
                vm.isGraduateNoExperienceClicked = false;
                vm.isCurrentEmploymentClicked = false;
            }
        };

        vm.saveProfile = function (profile) {
            
            if (vm.frmPersonalInfo.$invalid) {
                vm.isSavingProfile = false;
                return;
            }

            if (vm.isSavingProfile) {
                return;
            }
            vm.isSavingProfile = true;
            profile.raceId = profile.race.raceId;
            profile.genderId = profile.gender.genderId;


            var d = new Date(profile.dateOfBirth);
            d.setHours(0, -d.getTimezoneOffset(), 0, 0);
            profile.dateOfBirth = d;

            if (profile.occupationRegistrationDate) {
                d = new Date(profile.occupationRegistrationDate);
                d.setHours(0, -d.getTimezoneOffset(), 0, 0);
                profile.occupationRegistrationDate = d;


            }
            profile.firstNames = vm.firstName;
            profile.surname = vm.surname;
            profile.email = vm.frmAccount.email;
            profile.SecondaryEmail = vm.frmAccount.SecondaryEmail;
            profile.username = vm.frmAccount.userName;
            profile.workPermit = (vm.selecteWorkPermit) ? vm.selecteWorkPermit.value : undefined;
            profileService.addProfile(profile).then(function (response) {
                profileEditMode = true;
                vm.isSavingProfile = false;

                vm.initialisePage();
                WizardHandler.wizard().next();
            }, function (error) {
                vm.isSavingProfile = false;
                if (error.data.exceptionMessage) {
                    alert(error.data.exceptionMessage);
                }
                console.log(error);
            });
        };

        vm.saveLanguageProficiency = function (languageProficiency) {

            if (vm.frmLangProficiency.$invalid) {
                return;
            }
            if (!vm.editApplicantProficiency) {
                vm.proficiency.applicantProfileId = vm.profile.applicantProfileId;
                profileService.saveLanguageProficiency(languageProficiency).then(function (response) {
                    vm.proficiency = undefined;
                    vm.applicantLangProficiencies.push(response);
                    reset();
                    vm.frmLangProficiency.$setPristine();
                }, function (error) {
                    var innerException = error.data.innerException;
                    var index = innerException.exceptionMessage.indexOf('DUPLICATE_LANGUAGEPROFICIENCIES');
                    if (index > 0) {
                        vm.showError("Cant add a language more than once.", true);
                    }

                });
            }
            if (vm.editApplicantProficiency) {
                profileService.updateLanguageProficiency(languageProficiency).then(function (response) {
                    vm.editApplicantProficiency = false;
                    vm.applicantLangProficiencies[languageProficiency.index] = languageProficiency;
                    reset();
                    vm.frmLangProficiency.$setPristine();
                }, function (error) {

                });
            }

        };

        vm.openDatePopup = {
            opened: false
        };

        vm.openDateShowPopup = function () {
            vm.openDatePopup.opened = true;
        };

        vm.changeRace = function (race) {
            if (race.raceId == '6') {
                vm.showOtherRace = true
            } else {
                vm.showOtherRace = false;
            }
        }

        vm.closeDatePopup = {
            opened: false
        };

        vm.closeDateShowPopup = function () {
            vm.closeDatePopup.opened = true;
        };

        vm.editLanguageProficiency = function (languageProficiency, index) {
            vm.proficiency = languageProficiency;
            vm.proficiency.index = index;
            vm.editApplicantProficiency = true;
        };

        vm.removeLanguageProficiency = function (proficiency, index) {
            profileService.removeLanguageProficiency(proficiency).then(function (response) {
                vm.applicantLangProficiencies.splice(index, 1);
            }, function (error) {

            });
        };

        vm.saveApplicantQualification = function (qualification) {
            if (vm.frmQualifications.$invalid) {
                return;
            }

            //try {
            //    if (qualification.yearObtained <= vm.profile.dateOfBirth.getFullYear()) {
            //        throw "exit";
            //    }
            //}
            //catch (e) {
            //    vm.showError("Year obtained must be greater than your date of birth.", true);
            //    //setTimeout(location.reload.bind(location), 2000);
            //    return;
            //} 

            if (qualification.yearObtained > vm.MaxQualDate && !qualification.currentStudies) {
                vm.showError("Year obtained can not be greater than the current year.", true);
                //setTimeout(location.reload.bind(location), 2000); /* Modisaemang Manthe 18/10/2017 */
                return;
            }


            if (!vm.editApplicantQualificationMode) {
                qualification.applicantProfileId = vm.profile.applicantProfileId;


                profileService.saveApplicantQualification(qualification).then(function (response) {
                    vm.qualification = undefined;

                    var institution = $filter('filter')(vm.institutions, { institutionId: qualification.institutionId })[0];

                    response.institution = institution

                    vm.applicantCurrentQualifications.push(response);
                    reset();
                    vm.frmQualifications.$setPristine();
                }, function (error) {

                });
            }


            if (vm.editApplicantQualificationMode) {
                profileService.updateApplicantQualification(qualification).then(function (response) {
                    vm.editApplicantQualificationMode = false;

                    var institution = $filter('filter')(vm.institutions, { institutionId: qualification.institutionId })[0];
                    
                    vm.applicantCurrentQualifications[qualification.index] = qualification;
                    vm.applicantCurrentQualifications[qualification.index].institution = institution
                    reset();
                    vm.frmQualifications.$setPristine();
                }, function (error) {

                });
            }
        };

        vm.editApplicantQualification = function (qualification, index) {
            vm.qualification = qualification;
            vm.qualification.index = index;
            vm.editApplicantQualificationMode = true;
        };

        vm.removeApplicantQualification = function (qualification, index) {
            profileService.removeApplicantQualification(qualification).then(function (response) {
                vm.applicantCurrentQualifications.splice(index, 1);
            }, function (error) {

            });
        };

        vm.editWorkExperience = function (experience) {
            vm.experience = experience;
            vm.experience.dateFrom = new Date(experience.dateFrom);
            if (vm.experience.reasonForLeaving === "Current Employment") {
                vm.experience.dateTo = null;
                vm.currenEmployment = true;
            }
            else {
                vm.experience.dateTo = new Date(experience.dateTo);
            }

            vm.editWorkExperienceMode = true;

        };

        vm.removeWorkExperience = function (experience, index) {
            profileService.removeWorkExperience(experience).then(function (response) {
                vm.applicantExperiences.splice(index, 1);
            }, function (error) {

            });
        };

        vm.saveContactInfo = function (applicantContactDetail, formContactDetails) {

            if (formContactDetails.$invalid) {
                return;
            }

            if (vm.applicantLangProficiencies.length < 1) {
                vm.showError("Please provide at least one language.", true);
                return;
            }
            vm.applicantContactDetail.contactMethodId = applicantContactDetail.contactMethod.contactMethodId;
            vm.applicantContactDetail.languageId = applicantContactDetail.language.languageId;
            applicantContactDetail.applicantProfileId = vm.profile.applicantProfileId;
            if (vm.applicantContactDetail.contactMethod.contactMethodDesc == 'Email') {
                vm.applicantContactDetail.contactMethogDetails = vm.contactEmail;
            } else {
                vm.applicantContactDetail.contactMethogDetails = vm.contactTelephone;
            }

            profileService.saveContactInfo(vm.applicantContactDetail).then(function (response) {
                WizardHandler.wizard().next();
            }, function (error) {

            });
        };

        vm.addExperience = function (experience) {

            if (vm.frmExperience.$invalid) {
                return;
            }

            experience.applicantProfileId = vm.profile.applicantProfileId;

            var d = new Date(experience.dateFrom);
            d.setHours(0, -d.getTimezoneOffset(), 0, 0);
            experience.dateFrom = d;
            if (!vm.currenEmployment) {
                d = new Date(experience.dateTo);
                d.setHours(0, -d.getTimezoneOffset(), 0, 0);
                experience.dateTo = d;
                if (experience.dateFrom > experience.dateTo) {
                    vm.showError("Experience date from or date to seems to be incorrect.", true);
                    return;
                }
            }

            if (!vm.editWorkExperienceMode) {

                profileService.addExperience(experience).then(function (response) {
                    vm.experience = undefined;
                    vm.currenEmployment = false;
                    vm.applicantExperiences.push(response);
                    reset();
                    vm.frmExperience.$setPristine();

                }, function (error) {

                });
            }


            if (vm.editWorkExperienceMode) {
                profileService.updateExperience(experience).then(function (response) {
                    vm.applicantExperiences[experience.index] = experience;
                    vm.currenEmployment = false;
                    reset();
                    vm.frmExperience.$setPristine();
                    vm.editWorkExperienceMode = false;
                }, function (error) {

                });
            }
        };

        vm.addReference = function (reference) {

            if (vm.frmReference.$invalid) {
                return;
            }
            reference.applicantProfileId = vm.profile.applicantProfileId;
            if (!vm.editReferenceMode) {
                profileService.addReference(reference).then(function (response) {
                    vm.reference = undefined;
                    vm.applicantReferences.push(response);
                    reset();
                    vm.frmReference.$setPristine();
                }, function (error) {

                });
            }

            if (vm.editReferenceMode) {
                profileService.updateReference(reference).then(function (response) {
                    vm.applicantReferences[reference.index] = reference;
                    reset();
                    vm.frmReference.$setPristine();
                    vm.editReferenceMode = false;
                }, function (error) {

                });
            }


        };

        vm.editReference = function (reference, index) {
            vm.reference = reference;
            vm.reference.index = index;
            vm.editReferenceMode = true;
        };

        vm.removeReference = function (reference, index) {
            profileService.removeReference(reference).then(function (response) {
                vm.applicantReferences.splice(index, 1);
            }, function (error) {

            });
        }

        vm.addProhibition = function (prohibition) {    

            if (!(vm.graduateNoExperience) && vm.applicantExperiences.length < 1) {
                vm.showError("Please provide at least one work experience.", true);
                return;
            }

            if (vm.applicantReferences.length < 2) {
                vm.showError("Please provide at least two references.", true);
                return;
            }
            if (vm.frmPSProhibition.$invalid) {
                return;
            }
            prohibition.applicantProfileId = vm.profile.applicantProfileId;
            profileService.addProhibition(prohibition).then(function (response) {
                vm.ApplicantPubServProhibition = response;
                WizardHandler.wizard().next();
            }, function (error) {

            });
        };

        vm.fileTwo = null;
        vm.onLoad = function (e, reader, file, fileList, fileOjects, fileObj) {
            vm.file = fileObj;
            vm.fileTwo = file;
        };

        vm.SaveFile = function (type) {
            if (!vm.file || !vm.file.filesize) {
                return;
            }
            vm.loading = true;
            if (vm.fileTwo != null) {
                var r = new FileReader();
                r.onloadend = function (e) {

                    var filedata = new Uint8Array(e.target.result);
                    var applicantAttachments = {
                        applicantProfileId: vm.profile.applicantProfileId,
                        documentTypeId: type.documentTypeId,
                        //documentFormat: undefined,
                        documentData: arrayBufferToBase64String(filedata),
                        documentFormat: vm.file.filetype,
                        filePath: undefined,
                        localFileName: undefined,
                        originalFileName: vm.file.filename,
                        applicantProfile: vm.profile,
                        attachmentId: undefined
                    };
                    var fileToReplace = $filter('filter')(vm.applicantAttachments, { documentTypeId: type.documentTypeId })[0];
                    if (fileToReplace) {
                        applicantAttachments.attachmentId = fileToReplace.attachmentId;
                    }

                    if (vm.fileTwo != null) {
                        profileService.SaveFile(applicantAttachments).then(function (response) {
                            vm.applicantAttachments.push(applicantAttachments);
                            vm.fileTwo = null;
                            $timeout(function () {
                            vm.loading = false;
                                vm.initialisePage();
                            }, 5000);
                            
                        }, function (error) {

                        });
                    }
                }
                r.readAsArrayBuffer(vm.fileTwo);                
            }           
        };

        function arrayBufferToBase64String(buffer) {
        let binaryString = ''
        var bytes = new Uint8Array(buffer);
        for (var i = 0; i < bytes.byteLength; i++) {
            binaryString += String.fromCharCode(bytes[i]);
        }

        return window.btoa(binaryString);
    }

        vm.getFileBuffer = function (fileInput) {
            var deferred = jQuery.Deferred();
            var reader = new FileReader();
            reader.onloadend = function (e) {
                deferred.resolve(e.target.result);
            }
            reader.onerror = function (e) {
                deferred.reject(e.target.error);
            }
            reader.readAsArrayBuffer(fileInput);
            return deferred.promise();
        }

        function onError(error) {
            alert(error.responseText);
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
            profileService.getAttachment(fileToView.attachmentId);
            console.log(JSON.stringify(fileToView));
        };

        vm.getFileName = function (type) {

            if (vm.applicantAttachments != null) {
                var fileToView = $filter('filter')(vm.applicantAttachments, { documentTypeId: type.documentTypeId })[0];
                if (fileToView) {
                    return fileToView.originalFileName;
                }
            }
            return "";
        };

        vm.deleteFile = function (type) {
            
            if (vm.applicantAttachments != null) {
                var fileToView = $filter('filter')(vm.applicantAttachments, { documentTypeId: type.documentTypeId })[0];

                if (fileToView) {
                    profileService.deleteAttachment(fileToView);
                    vm.remove(fileToView, vm.applicantAttachments)
                }
            }           
        };

        vm.remove = function (item, items) {
            // console.log(items);            
            items.splice(items.indexOf(item), 1);
            items.forEach(function (elem) {
                elem.id = items.indexOf(elem) + 1;
            });
        };

        vm.addDeclaration = function (declaration) {
            if (vm.frmDeclaration.$invalid) {
                vm.showError("Please accept the declaration by clicking the \"I DECLARE\" box.", true);
                return;
            }
            var d = new Date(declaration.acceptedDeclarationOn);
            d.setHours(0, -d.getTimezoneOffset(), 0, 0);
            declaration.acceptedDeclarationOn = d;

            declaration.applicantProfileId = vm.profile.applicantProfileId;
            profileService.addDeclaration(declaration).then(function (response) {
                $location.path('/viewProfile');
                $location.replace();
            }, function (error) {

            });
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
    };

    angular.module('eRecruitmentApp').controller('profileController', profileController).config(function ($mdDateLocaleProvider) {

        $mdDateLocaleProvider.formatDate = function (date) {
            if (date) {
                var day = date.getDate();
                var monthIndex = date.getMonth();
                var year = date.getFullYear();
                var monthString = null;
                var dayString = null;

                if (monthIndex <= 8) {
                    monthString = "0" + (monthIndex + 1);
                }
                else {
                    monthString = (monthIndex + 1);
                }
                if (day < 10) {
                    dayString = "0" + day;
                }
                else {
                    dayString = day;
                }
                return year + '/' + monthString + '/' + dayString;
            }
            else {
                return null;
            }



        };
    });;
    profileController.$inject = ['$scope', 'profileService', 'WizardHandler', 'authService', '$filter', '$location', '$routeParams', '$localStorage', '$timeout', 'Upload'];
})();