(function () {
    'use strict';

    angular.module('eRecruitmentApp').directive('usernameAvailable', function ($http, $q, ngConfigSettings) {
        return {
            restrict: 'AE',
            require: 'ngModel',
            link: function (scope, elm, attr, model) {

                var serviceBase = ngConfigSettings.apiServiceBaseUri;

                model.$asyncValidators.usernameExists = function () {
                    var username = model.$viewValue;
                    var defer = $q.defer();
                    var request = {
                        Username: username
                    };
                    $http.post(serviceBase + 'api/user/checkUniqueUsername', request, { headers: { 'Content-Type': 'application/json' } }).then(function (response) {
                        model.$setValidity('usernameExists', !!response.data);
                        defer.resolve;
                    }).catch(function (response) {
                        defer.reject(response);
                    });

                    return defer.promise;
                };
            }
        }
    });
})();