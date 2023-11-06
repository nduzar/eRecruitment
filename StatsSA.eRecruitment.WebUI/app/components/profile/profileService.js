(function () {
    'use strict';

    var profileService = function ($http, $q, ngConfigSettings) {

        var serviceBase = ngConfigSettings.apiServiceBaseUri;

        var addProfile = function (profile) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/addprofile', profile).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var getProfileInitialData = function () {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/getProfileInitialData').then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var saveLanguageProficiency = function (proficiency) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/addapplicantlangproficiencies', proficiency).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var saveContactInfo = function (contactdetail) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/addcontactdatails', contactdetail).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var saveApplicantQualification = function (qualification) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/addqualification', qualification).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var deleteAttachment = function (attachment) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/deleteAttachment', attachment).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var addExperience = function (experience) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/addexperience', experience).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var addReference = function (reference) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/addreferences', reference).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var addProhibition = function (prohibition) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/addprohibition', prohibition).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var SaveFile = function (attachment) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/addattachments', attachment).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var getAttachment = function (attachmentId) {
            window.location.href = serviceBase + "api/applicantprofile/getattachment/" + attachmentId;
        };

        var addDeclaration = function (declaration) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/adddeclaration', declaration).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var updateLanguageProficiency = function (proficiency) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/updateapplicantlangproficiencies', proficiency).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var updateApplicantQualification = function (qualification) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/updateapplicantqualification', qualification).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var updateExperience = function (experience) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/updateexperience', experience).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var updateReference = function (reference) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/updatereferences', reference).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var updateAccount = function (account) { // Amit Kamal
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/updateAccount', account).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var removeLanguageProficiency = function (proficiency) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/removeapplicantlangproficiency', proficiency).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var removeApplicantQualification = function (qualification) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/removeapplicantqualification', qualification).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var removeWorkExperience = function (experience) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/removeworkexperience', experience).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var removeReference = function (reference) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/removereference', reference).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };


        return {
            addProfile: addProfile,
            getProfileInitialData: getProfileInitialData,
            saveLanguageProficiency: saveLanguageProficiency,
            saveContactInfo: saveContactInfo,
            saveApplicantQualification: saveApplicantQualification,
            addExperience: addExperience,
            addReference: addReference,
            addProhibition: addProhibition,
            SaveFile: SaveFile,
            addDeclaration: addDeclaration,
            updateLanguageProficiency: updateLanguageProficiency,
            updateApplicantQualification: updateApplicantQualification,
            updateExperience: updateExperience,
            updateReference: updateReference,
            deleteAttachment: deleteAttachment,
            getAttachment: getAttachment,
            removeLanguageProficiency: removeLanguageProficiency,
            removeApplicantQualification: removeApplicantQualification,
            removeWorkExperience: removeWorkExperience,
            removeReference: removeReference,
            updateAccount: updateAccount // Amit Kamal
        };
    };

    angular.module('eRecruitmentApp').factory('profileService', profileService);
    profileService.$inject = ['$http','$q','ngConfigSettings'];
})();