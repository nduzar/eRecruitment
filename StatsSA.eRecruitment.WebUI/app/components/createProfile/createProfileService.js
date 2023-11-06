(function () {
    'use strict';

    var createProfileService = function ($http, $q, ngConfigSettings) {

        var serviceBase = ngConfigSettings.apiServiceBaseUri;

        var getUser = function () {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/user/getUserByUserId').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        var saveNewUser = function (user) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/user/adduser', user).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        var saveNewProfile = function (profile) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/addprofile', profile).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var addQualifications = function (qualification) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/addqualification', qualification).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        var removeQualifications = function (qualification) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/removequalification', qualification).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var addExperience = function (experience) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/addexperience', experience).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        var removeExperience = function (experience) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/addexperience', experience).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var addAttachment = function (attachment) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/addattachments', attachment).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        var removeAttachment = function (attachment) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/removeattachments', attachment).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var addReference = function (reference) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/addreferences', reference).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        var removeReference = function (reference) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/removereferences', reference).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var addLangProficiency = function (proficiency) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/addapplicantlangproficiencies', proficiency).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        var removeLangProficiency = function (proficiency) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/removeapplicantlangproficiencies', proficiency).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var addContactDetails = function (contactDetail) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/addcontactdatails', contactDetail).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        var addDeclaration = function (declaration) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/adddeclaration', declaration).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        var addProhibition = function (prohibition) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applicantprofile/addprohibition', prohibition).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        // -- LOOKUP FUNCTIONS START -- //
        var getRaces = function () {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/lookups/getraces').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        var getGenders = function () {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/lookups/getgenders').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        var getContactMethods = function () {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/lookups/getcontactmetods').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        var getLanguages = function () {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/lookups/getlanguages').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        var getLanguageProficiencies = function () {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/lookups/getlanguageproficiencies').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        var getQualificationTypes = function () {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/lookups/getqualificationtypes').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        // -- LOOKUP FUNCTIONS END -- //

        return {
            getUser: getUser,
            saveNewUser: saveNewUser,
            saveNewProfile: saveNewProfile,
            addQualifications: addQualifications,
            addExperience: addExperience,
            addAttachment: addAttachment,
            removeAttachment: removeAttachment,
            addContactDetails: addContactDetails,
            addDeclaration: addDeclaration,
            addProhibition: addProhibition,
            getRaces: getRaces,
            getGenders: getGenders,
            getContactMethods: getContactMethods,
            getLanguages: getLanguages,
            getLanguageProficiencies: getLanguageProficiencies,
            getQualificationTypes: getQualificationTypes,
            addReference: addReference,
            removeReference: removeReference,
            addLangProficiency: addLangProficiency,
            removeLangProficiency: removeLangProficiency,
            removeQualifications: removeQualifications
        };

    };

    angular.module('eRecruitmentApp').factory('createProfileService', createProfileService);
    createProfileService.$inject = ['$http', '$q', 'ngConfigSettings'];
})();