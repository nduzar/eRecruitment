(function () {
    'use strict';

    var verifyEmailRecoveryService = function ($q, $http, ngConfigSettings) {

        var serviceBase = ngConfigSettings.apiServiceBaseUri;//url to webapi

        var generateCaptcha = function () {
            var tmpString = "";
            var i;
            for (i = 0; i < 6; i++) {
                var randNum = Math.round(Math.random() * 9);
                tmpString = tmpString.concat(randNum);
            }
            return tmpString;
        };

        var searchAccount = function (IdentityNumber) {
            var deferred = $q.defer();
            var searchAccountRequest = {
                IdentityNumber: IdentityNumber
            };

            $http.post(serviceBase + 'api/verifyemail/searchAccount', searchAccountRequest).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var checkEmailVerification = function (code) {
            var deferred = $q.defer();

            $http.post(serviceBase + 'api/verifyemail/checkEmailVerification', code).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        return {
            generateCaptcha: generateCaptcha,
            searchAccount: searchAccount,
            checkEmailVerification: checkEmailVerification
        };

    };

    angular.module('eRecruitmentApp').factory('verifyEmailRecoveryService', verifyEmailRecoveryService);
    verifyEmailRecoveryService.$inject = ['$q', '$http', 'ngConfigSettings'];


})();