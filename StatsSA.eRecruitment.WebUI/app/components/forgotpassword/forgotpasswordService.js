(function () {
    'use strict';

    var forgotpasswordService = function ($q, $http, ngConfigSettings) {

        var serviceBase = ngConfigSettings.apiServiceBaseUri;//url to webapi

        var send = function (idnumber) {
            var deferred = $q.defer();
            
            var httpPath = serviceBase + 'api/managepassword/forgotpassword/';
            console.log('forgot password httpPath: ' + httpPath);
                        
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

            $http.post(serviceBase + 'api/managepassword/searchAccount', searchAccountRequest).then(function (response) {
                deferred.resolve(response.data);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var passwordResetRequest = function (userAccount, resertPasswordMethod) {
            var deferred = $q.defer();
            var passwordResetRequestModel = {
                SearchAccountResult: userAccount,
                resertPasswordMethod: resertPasswordMethod
            };

            $http.post(serviceBase + 'api/managepassword/passwordReset', passwordResetRequestModel).then(function (response) {
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
            passwordResetRequest: passwordResetRequest
        };

    };

    angular.module('eRecruitmentApp').factory('forgotpasswordService', forgotpasswordService);
    forgotpasswordService.$inject = ['$q', '$http', 'ngConfigSettings'];


})();