(function () {
    'use strict';

    var viewApplicationsService = function ($http, $q, ngConfigSettings) {

        var serviceBase = ngConfigSettings.apiServiceBaseUri;

        var getListOfApplications = function () {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/application/getapplicantapplications').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        var saveApplication = function (application) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/application/saveapplication', application).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        }
        return {
            getListOfApplications: getListOfApplications,
            saveApplication: saveApplication
        };
        
    };

    angular.module('eRecruitmentApp').factory('viewApplicationsService', viewApplicationsService);
    viewApplicationsService.$inject = ['$http', '$q', 'ngConfigSettings'];
})();