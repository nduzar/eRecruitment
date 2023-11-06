(function () {
    'use strict';

    var salariesService = function ($http, $q, ngConfigSettings) {

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
        var saveSalary = function (salary) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/lookups/savesalary', salary).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;

        }

        return {
            getSalaries: getSalaries,
            saveSalary: saveSalary
        };
    };

    angular.module('eRecruitmentApp').factory('salariesService', salariesService);
    salariesService.$inject = ['$http', '$q', 'ngConfigSettings'];
})();