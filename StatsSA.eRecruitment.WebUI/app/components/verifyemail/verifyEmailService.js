(function () {
    'use strict';

    var verifyEmailService = function ($q, $http, ngConfigSettings) {

        var serviceBase = ngConfigSettings.apiServiceBaseUri;//url to webapi

        var verifyEmail = function (idnumber) {
            var deferred = $q.defer();
            
            var httpPath = serviceBase + 'api/verifyemail/checkEmailVerification/';
            console.log('checkEmailVerification httpPath: ' + httpPath);
                        
            $http.post(httpPath, idnumber)
                .then(function (response)
                {
                    console.log("SERVICE SIDE >>: passed >> " + idnumber);
                    deferred.resolve(response);
                })

                .catch(function (error)
                {
                    console.log("SERVICE SIDE >>: failed >> " + idnumber);
                    deferred.reject(error);
                });

            return deferred.promise;
        };

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

        var updateEmailVerification = function (userAccount) {
            var deferred = $q.defer();
            var verifyEmail = {
                userId: userAccount.userId
            };
            $http.post(serviceBase + 'api/verifyemail/updateEmailVerification', verifyEmail).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        return {
            generateCaptcha: generateCaptcha,
            searchAccount: searchAccount,
            verifyEmail: verifyEmail,
            updateEmailVerification: updateEmailVerification
        };

    };

    angular.module('eRecruitmentApp').factory('verifyEmailService', verifyEmailService);
    verifyEmailService.$inject = ['$q', '$http', 'ngConfigSettings'];


})();