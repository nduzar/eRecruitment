(function () {
    'use strict';

    var applyService = function ($http, $q, ngConfigSettings) {

        var serviceBase = ngConfigSettings.apiServiceBaseUri;

        var sendApplication = function (application) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/application/addapplication', application).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };
        var getAttachment = function (attachment) {
            window.location.href = serviceBase + "api/applicantprofile/getattachment/" + attachment.attachmentId;
        };

        var getAllQuestionsAndAnswers = function (vacancy) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/vacancy/getAllQuestionsAndAnswers', vacancy).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;

        }

        var sentAllQuestionsAndAnswers = function (allQuestionsAndAnswers) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/application/sentAllQuestionsAndAnswers', allQuestionsAndAnswers).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;

        }

        return {
            sendApplication: sendApplication,
            getAttachment: getAttachment,
            getAllQuestionsAndAnswers: getAllQuestionsAndAnswers,
            sentAllQuestionsAndAnswers: sentAllQuestionsAndAnswers
        };
    };

    angular.module('eRecruitmentApp').factory('applyService', applyService);
    applyService.$inject = ['$http', '$q', 'ngConfigSettings'];
})();