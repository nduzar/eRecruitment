(function () {
    'use strict';

    var cancelVacancyService = function ($http, $q, ngConfigSettings) {

        var serviceBase = ngConfigSettings.apiServiceBaseUri;
        var getSalaries = function () {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/lookups/getsalaries').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };
        var getCancelableVacancies = function () {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/vacancy/getcancelable').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };
        var cancelVacancy = function (vacancy) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/vacancy/cancelvacancy', vacancy).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };
        return {
            getCancelableVacancies: getCancelableVacancies,
            cancelVacancy: cancelVacancy,
            getSalaries: getSalaries
        };
    };

    angular.module('eRecruitmentApp').factory('cancelVacancyService', cancelVacancyService);
    cancelVacancyService.$inject = ['$http', '$q', 'ngConfigSettings'];
})();