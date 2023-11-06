(function () {
    'use strict';

    var viewProfileController = function (profileService, authService, $location, $filter) {
        var vm = this;
        vm.authentication = authService.authentication;
        vm.loading = true;
        vm.profile = {};
        vm.selectedProficiencies = [];
        vm.applicantCurrentQualifications = [];
        vm.attachments = [];
        vm.user = {};

        vm.YesNo = function (isTrue) {
            if (isTrue === true) {
                return 'Yes'
            }
            if (isTrue === false) {
                return 'No'
            }
            return undefined;
        };

        vm.initialisePage = function () {
            vm.selectedProficiencies.length = 0;
            vm.applicantCurrentQualifications.length = 0;
            vm.applicantQualification = {
                applicantQualificationId: undefined,
                qualificationType: undefined,
                qualificationTypeId: undefined,
                applicantProfileId: undefined,
                institution: undefined,
                nameOfQualification: undefined,
                yearObtained: undefined,
                currentStudies: undefined
            };
            if (vm.authentication.isAuth) {
                profileService.getProfileInitialData().then(function (response) {
                    vm.profile = response.profile;
                    vm.user = response.user;
                    vm.races = response.races;
                    vm.genders = response.genders;
                    vm.institution = response.institutions;
                    vm.contactMethods = response.contactMethods;
                    vm.languageProficiencies = response.languageProficiencies;
                    vm.languages = response.languages;
                    vm.Preferredlanguages = response.languages;
                    vm.qualificationTypes = response.qualificationTypes;                    
                    vm.languageProficienciesRead = vm.languageProficiencies;
                    vm.languageProficienciesWrite = vm.languageProficiencies;
                    vm.languageProficienciesSpeak = vm.languageProficiencies;                    
                    vm.documentTypes = response.documentTypes;                 

                    if (vm.profile) {
                        vm.firstName = vm.profile.firstNames;
                        vm.surname = vm.profile.surname;

                        var selectedRace = $filter('filter')(vm.races, { raceId: vm.profile.raceId })[0];
                        var selectedGender = $filter('filter')(vm.genders, { genderId: vm.profile.genderId })[0];
                        

                        vm.profile.race = selectedRace;
                        vm.profile.gender = selectedGender;
                        vm.profile.dateOfBirth = new Date(response.profile.dateOfBirth);
                        vm.profile.occupationRegistrationDate =(response.profile.occupationRegistrationDate) ? new Date(response.profile.occupationRegistrationDate) : undefined;

                        vm.applicantDeclaration = response.applicantDeclaration;
                        vm.applicantLangProficiencies = response.applicantLangProficiencies;
                        vm.applicantQualifications = response.applicantQualifications;
                        vm.applicantContactDetail = response.applicantContactDetail;
                        vm.applicantExperiences = response.applicantExperiences;
                        vm.applicantReferences = response.applicantReferences;
                        vm.ApplicantPubServProhibition = response.applicantPubServProhibition;

                        //Loop through all the document types
                       
                        for (var i = 0; i < vm.documentTypes.length; i++) {

                            for (var j = 0; j < response.attachments.length; j++) {
                                if (response.documentTypes[i].documentTypeId == response.attachments[j].documentTypeId) {
                                    var attachment = {
                                        documentType: undefined,
                                        documentName: undefined
                                    };
                                    attachment.documentName = response.attachments[j].originalFileName;
                                    attachment.documentType = response.documentTypes[i].documentTypeDesc;
                                    vm.attachments.push(attachment);
                                    console.log(vm.attachments);
                                }
                            }
                        }
                    }
                    vm.loading = false;
                }, function (error) {

                });
            }
        };

    };

    angular.module('eRecruitmentApp').controller('viewProfileController', viewProfileController);
    viewProfileController.$inject = ['profileService', 'authService', '$location', '$filter'];
})();