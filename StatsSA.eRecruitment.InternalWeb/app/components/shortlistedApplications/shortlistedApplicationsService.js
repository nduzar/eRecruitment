(function () {
    'use strict';

    var shortlistedApplicationsService = function ($http, $q, ngConfigSettings, $location) {        

        var serviceBase = ngConfigSettings.apiServiceBaseUri;

        var currentPost = null;
        var currentApplication = null;

        var getApplicationsHiringManager = function () {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/applications/getApplicationsHiringManager').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

      

        var setCurrentPost = function (post, path) {
            var path = '/hrShortlistedApplications'
            currentPost = post;
            $location.path(path);
        };
        var getCurrentPost = function () {
            return currentPost;
        }
        var setCurrentApplication = function (selectedApplication) {
            currentApplication = selectedApplication;
            $location.path('/hiringManagerShortlisting');
        }
        var getCurrentApplication = function () {
            return currentApplication;
        }

        return {
            getApplicationsHiringManager: getApplicationsHiringManager,
            getCurrentPost: getCurrentPost,
            setCurrentApplication: setCurrentApplication,
            getCurrentApplication: getCurrentApplication            
        };
    };

    angular.module('eRecruitmentApp').factory('shortlistedApplicationsService', shortlistedApplicationsService);
    shortlistedApplicationsService.$inject = ['$http', '$q', 'ngConfigSettings', '$location'];
})();