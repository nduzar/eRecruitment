(function () {
    'use strict';

    var emailhangeService = function ($q, $http, ngConfigSettings) {

        var serviceBase = ngConfigSettings.apiServiceBaseUri;//url to webapi

        var send = function (userAccount) {
            var deferred = $q.defer();
            console.log(userAccount, 'service')

            var httpPath = serviceBase + 'api/managepassword/sendOtp';
            console.log('forgot password httpPath: ' + httpPath);
            //var otpRequestForEmail = {
               // searchAccountRequestByEmail: userAccount,
            //}
            var searchAccountRequestByEmail = {
                SecondaryEmail: userAccount.secondaryEmail,
                Id: userAccount.id,
            }
            $http.post(httpPath, searchAccountRequestByEmail)
                .then(function (response) {
                    console.log("SERVICE SIDE >>: passed >> " + userAccount.email);
                    deferred.resolve(response);
                })

                .catch(function (error) {
                    console.log("SERVICE SIDE >>: failed >> " + email);
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

        var searchAccount = function (email) {
            var deferred = $q.defer();
            var searchAccountRequestByEmail = {
                email: email
            };

            $http.post(serviceBase + 'api/managepassword/searchAccountByEmail', searchAccountRequestByEmail).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var emailResetRequest = function (userAccount, newEmail) {
            var deferred = $q.defer();
            var emailResetRequest = {
                Id: userAccount.id,
                Otp: newEmail.otp,
                Email: newEmail.email
            };

            $http.post(serviceBase + 'api/managepassword/otpVerifivation', emailResetRequest).then(function (response) {
                debugger
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        return {
            send: send,
            generateCaptcha: generateCaptcha,
            searchAccount: searchAccount,
            emailResetRequest: emailResetRequest
        };

    };

    angular.module('eRecruitmentApp').factory('emailhangeService', emailhangeService);
    emailhangeService.$inject = ['$q', '$http', 'ngConfigSettings'];


})();