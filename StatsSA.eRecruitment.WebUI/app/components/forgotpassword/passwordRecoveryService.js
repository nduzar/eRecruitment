(function () {
    'use strict';

    var passwordRecoveryService = function ($q, $http, ngConfigSettings) {
        var serviceBase = ngConfigSettings.apiServiceBaseUri;
        var codeVerifivation = function (resetCodeData) {
            var deferred = $q.defer();

            $http.post(serviceBase + 'api/managepassword/codeVerifivation', resetCodeData).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var resetPassword = function (passwordData) {
            var deferred = $q.defer();
            var resetPasswordRequest = {
                newPassword: passwordData.newPassword,
                userId: passwordData.userId
            };
            $http.post(serviceBase + 'api/managepassword/resetpassword', resetPasswordRequest).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        return {
            codeVerifivation: codeVerifivation,
            resetPassword: resetPassword
        };
    };
    angular.module('eRecruitmentApp').factory('passwordRecoveryService', passwordRecoveryService);
    passwordRecoveryService.$inject = ['$q', '$http', 'ngConfigSettings']
})();