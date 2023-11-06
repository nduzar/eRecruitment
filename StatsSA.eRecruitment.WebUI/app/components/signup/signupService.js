(function () {
    'use strict';

    var signupService = function ($http, $q, ngConfigSettings) {

        var serviceBase = ngConfigSettings.apiServiceBaseUri;

        var saveUser = function (user) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/user/adduser', user).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        return {
            saveUser: saveUser
        };
    };

    angular.module('eRecruitmentApp').factory('signupService', signupService);
    signupService.$inject = ['$http', '$q', 'ngConfigSettings'];

})();