(function () {
    'use strict';

    var reportsService = function ($http, $q, ngConfigSettings) {

        var serviceBase = ngConfigSettings.apiServiceBaseUri;

        var getReportServerURL = function () {
            var deferred = $q.defer();
            $http.get(serviceBase + 'api/Report/get').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

        return {
            getReportServerURL: getReportServerURL
        };
    };

    angular.module('eRecruitmentApp').factory('reportsService', reportsService);
    reportsService.$inject = ['$http', '$q', 'ngConfigSettings'];
})();