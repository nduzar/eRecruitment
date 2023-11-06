(function () {
    'use strict';

    var screeningService = function ($http, $q, ngConfigSettings, $location) {

        var serviceBase = ngConfigSettings.apiServiceBaseUri;
        var currentPost = null;
        var currentApplication = null;
        var getPostsForScreening = function () {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/vacancy/getscreeninglist').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

        var getApplicationsHiringManager = function () {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applications/getApplicationsHiringManager').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };


        var getAllApplications = function (selectedVacancy) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applications/getallapplications', selectedVacancy).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

        var getApplicantProfile = function () {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applications/getapplicantprofile', currentApplication).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        var setCurrentPost = function (post, path) {
            currentPost = post;
            $location.path(path);
        };
        var getCurrentPost = function () {
            return currentPost;
        }
        var setCurrentApplication = function (selectedApplication, path) {
            currentApplication = selectedApplication;       
            $location.path(path);
        }
        var getCurrentApplication = function () {
            return currentApplication;
        }
        var backToPosts = function (path) {
            $location.path(path);
        }
        var backToApplications = function (path) {            
            $location.path(path);
        }


        var verifyApplication = function (selectedapplication) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applications/verifyappliction', selectedapplication).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

        var shortListAppliction = function (selectedapplication) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applications/shortlistappliction', selectedapplication).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };
        var rejectAtVerification = function (selectedapplication) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applications/rejectapplicationatverification', selectedapplication).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };
        var screeningComplete = function () {
            var selectedVacancy = this.getCurrentPost();
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/vacancy/screenedvacancy', selectedVacancy).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;

        };
        var shortlistApplication = function (selectedapplication) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applications/shortlistappliction', selectedapplication).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

        var getAllRoles = function () {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applications/getAllRoles').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

        var sentEmail = function (hiringmanagerVacancyAccess) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applications/sentEmail', hiringmanagerVacancyAccess).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

        var inProcess = function (selectedapplication) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applications/inProcess', selectedapplication).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

        var successful = function (selectedapplication) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applications/successful', selectedapplication).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

        var unSuccessful = function (selectedapplication) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applications/unSuccessful', selectedapplication).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

        var getAttachment = function (attachment) {
            window.location.href = serviceBase + "api/applications/getapplicationattachment/" + attachment.verAttachmentId;
        }
        return {
            getPostsForScreening: getPostsForScreening,
            shortListAppliction: shortListAppliction,
            setCurrentPost: setCurrentPost,
            getAllApplications: getAllApplications,
            getApplicationsHiringManager: getApplicationsHiringManager,
            getCurrentPost: getCurrentPost,
            backToPosts: backToPosts,
            setCurrentApplication: setCurrentApplication,
            getCurrentApplication: getCurrentApplication,
            backToApplications: backToApplications,
            getApplicantProfile: getApplicantProfile,
            //rejectAtShortlisting: rejectAtShortlisting,
            shortlistApplication: shortlistApplication,
            inProcess: inProcess,
            successful: successful,
            unSuccessful: unSuccessful,
            rejectAtVerification: rejectAtVerification,
            verifyApplication: verifyApplication,
            screeningComplete: screeningComplete,
            getAttachment: getAttachment,
            getAllRoles: getAllRoles,
            sentEmail: sentEmail
        };
    };

    angular.module('eRecruitmentApp').factory('screeningService', screeningService);
    screeningService.$inject = ['$http', '$q', 'ngConfigSettings', '$location'];
})();