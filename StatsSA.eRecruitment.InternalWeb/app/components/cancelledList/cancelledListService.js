﻿(function () {
    'use strict';

    var cancelledListService = function ($http, $q, ngConfigSettings, $location) {

        var serviceBase = ngConfigSettings.apiServiceBaseUri;
        var currentPost = null;
        var currentApplication = null;
        var getcancelledList = function () {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/vacancy/getcancelledList').then(function (response) {
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
            getcancelledList: getcancelledList,           
            generateReport: generateReport
        };
    };

    angular.module('eRecruitmentApp').factory('cancelledListService', cancelledListService);
    cancelledListService.$inject = ['$http', '$q', 'ngConfigSettings', '$location'];
})();