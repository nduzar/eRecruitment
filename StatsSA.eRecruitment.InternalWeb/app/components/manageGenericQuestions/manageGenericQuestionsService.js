(function () {
    'use strict';

    var manageGenericQuestionsService = function ($http, $q, ngConfigSettings) {

        var serviceBase = ngConfigSettings.apiServiceBaseUri;

        var getAllGenericQuestions = function () {
            var deferred = $q.defer();
            $http.get(serviceBase + 'api/vacancy/getgenericquestions').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

        var getAllGenericQuestions = function () {
            var deferred = $q.defer();
            $http.get(serviceBase + 'api/vacancy/getgenericquestions').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

        var deleteQuestion = function (question) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/vacancy/deleteQuestion', question).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

        var saveAllGenericQuestions = function (questions) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/vacancy/saveAllGenericQuestions', questions).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

        return {
            getAllGenericQuestions: getAllGenericQuestions,
            deleteQuestion: deleteQuestion,
            saveAllGenericQuestions: saveAllGenericQuestions
        };
    };

    angular.module('eRecruitmentApp').factory('manageGenericQuestionsService', manageGenericQuestionsService);
    manageGenericQuestionsService.$inject = ['$http', '$q', 'ngConfigSettings'];
})();