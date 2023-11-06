(function () {
    'use strict';

    var resetpasswordService = function ($q, $http, ngConfigSettings) {

        var serviceBase = ngConfigSettings.apiServiceBaseUri;//url to webapi

        var reset = function (resetpasswordData) {
            
            console.log("IDNo: " + resetpasswordData.IDNo + ", tempPass: " + resetpasswordData.tempPass + ", newPass: " + resetpasswordData.newPass);
            var deferred = $q.defer();
            var httpPath = serviceBase + 'api/managepassword/resetpassword/';
            
            //post data to a webapi
            $http.post(httpPath, resetpasswordData)
                .then(function (response)
                {
                    console.log("SERVICE SIDE >>: passed >> " + resetpasswordData.IDNo);
                    deferred.resolve(response);
                })
                .catch(function (error)
                {
                    console.log("SERVICE SIDE >>: failed >> " + resetpasswordData.IDNo);
                    deferred.reject(error);
                });

            return deferred.promise;
        };

        return {
            reset: reset
        };
    };

    angular.module('eRecruitmentApp').factory('resetpasswordService', resetpasswordService);
    resetpasswordService.$inject = ['$q', '$http', 'ngConfigSettings'];

})();