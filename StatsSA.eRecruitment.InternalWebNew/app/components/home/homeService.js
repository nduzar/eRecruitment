(function () {
    'use strict';

    var homeService = function ($http, $q, ngConfigSettings) {

        var serviceBase = ngConfigSettings.apiServiceBaseUri;

        var getHomeData = function () {
            var deferred = $q.defer();

            $http.post(serviceBase + 'api/vacancy/getcaptredvacancies').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };
        return {
            getHomeData: getHomeData
        };
    };

    angular.module('eRecruitmentApp').factory('homeService', homeService);
    homeService.$inject = ['$http', '$q', 'ngConfigSettings'];
})();