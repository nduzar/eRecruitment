(function () {
    'use strict';

    var authorisedListService = function ($http, $q, ngConfigSettings, $location) {

        var serviceBase = ngConfigSettings.apiServiceBaseUri;
        var currentPost = null;
        var currentApplication = null;
        var getauthorisedList = function () {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/vacancy/getauthorisedList').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };
        
        var generateReport = function () {
            var deferred = $q.defer();
            var httpPath = serviceBase + 'api/vacancy/generatereport';
            console.log('service >> generateReport: ');            
            // read from file            
            $http.post(httpPath)
                .then(function (response) {
                    console.log("SERVICE SIDE >> generatereport: passed");
                    deferred.resolve(response);
                })
                .catch(function (error) {
                    console.log("SERVICE SIDE >> generatereport: failed");
                    deferred.reject(error); 
                });

            return deferred.promise;
        };
        

        return {
            getauthorisedList: getauthorisedList,           
            generateReport: generateReport
        };
    };

    angular.module('eRecruitmentApp').factory('authorisedListService', authorisedListService);
    authorisedListService.$inject = ['$http', '$q', 'ngConfigSettings', '$location'];
})();