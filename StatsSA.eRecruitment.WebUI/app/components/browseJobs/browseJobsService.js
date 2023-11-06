(function () {
    'use strict';

    var browseJobsService = function ($http, $q, ngConfigSettings) {

        var serviceBase = ngConfigSettings.apiServiceBaseUri;
        var currentVacancy = undefined;
        var viewOnly = undefined;

        var setCurrentVacancy = function (vacancy) {
            currentVacancy = vacancy;
        };

        var getCurrentVacancy = function () {
            return currentVacancy;
        };

        var setViewOnly = function (isViewOnly) {
            viewOnly = isViewOnly;
        };

        var getViewOnly = function () {
            return viewOnly;
        };

        var getAllActiveVacancies = function () {
            var deferred = $q.defer();

            $http.post(serviceBase + 'api/vacancy/getAllActiveVacancies').then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };
        return {
            getAllActiveVacancies: getAllActiveVacancies,
            setCurrentVacancy: setCurrentVacancy,
            getCurrentVacancy: getCurrentVacancy,
            setViewOnly: setViewOnly,
            getViewOnly: getViewOnly
        };
    };

    angular.module('eRecruitmentApp').factory('browseJobsService', browseJobsService);
    browseJobsService.$inject = ['$http', '$q', 'ngConfigSettings'];
})();