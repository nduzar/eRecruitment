(function () {
    'use strict';

    var usersService = function ($http, $q, ngConfigSettings) {

        var serviceBase = ngConfigSettings.apiServiceBaseUri;

        var getUsersList = function () {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/users/getallusers').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        var getAllRoles = function () {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/lookups/gethrroles').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        var searchUser = function (partialString) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/users/searchUser', { partialString: partialString }).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        var updateUser = function (user) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/users/updateUser', user).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        var insertUser = function (user) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/users/insertUser', user).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        return {
            getUsersList: getUsersList,
            getAllRoles: getAllRoles,
            updateUser: updateUser,
            searchUser: searchUser,
            insertUser: insertUser
        };
    };

    angular.module('eRecruitmentApp').factory('usersService', usersService);
    usersService.$inject = ['$http', '$q', 'ngConfigSettings'];
})();