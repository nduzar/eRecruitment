(function () {
    'use strict';

    var authoriseVacancyService = function ($http, $q, ngConfigSettings) {

        var serviceBase = ngConfigSettings.apiServiceBaseUri;
        var getSalaries = function () {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/lookups/getsalaries').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };
        var getApprovedVacancies = function () {
            var deferred = $q.defer();

            $http.post(serviceBase + 'api/vacancy/getapprovedvacancies').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };
        var authoriseVacancy = function (vacancy) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/vacancy/authorisevacancy', vacancy).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };
        var rejectVacancy = function (vacancy) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/vacancy/authrejectvacancy', vacancy).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

        var updateAnswerGenericQuestion = function (answersGenericQuestion) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/vacancy/updateAnswerGenericQuestion', answersGenericQuestion).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

        var deleteAnswerGenericQuestion = function (answerGenericQuestion) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/vacancy/deleteAnswerGenericQuestion', answerGenericQuestion).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

        var updateSpecificQuestions = function (listSpecificQuestionsAnswers) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/vacancy/updateSpecificQuestions', listSpecificQuestionsAnswers).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

        var deleteAnswerSpecificQuestion = function (answerSpecificQuestion) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/vacancy/deleteAnswerSpecificQuestion', answerSpecificQuestion).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

        var deleteGenericQuestion = function (answerSpecificQuestion) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/vacancy/deleteGenericQuestion', answerSpecificQuestion).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

        var getAllQuestionsAndAnswers = function (vacancy) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/vacancy/getAllQuestionsAndAnswers', vacancy).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

        var getGenericQuestions = function () {
            var deferred = $q.defer();
            var httpPath = serviceBase + 'api/vacancy/getgenericquestions';

            $http.get(httpPath).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        };

        var deleteSpecificQuestion = function (specificQuestion) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/vacancy/deleteSpecificQuestion', specificQuestion).then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

        return {
            getApprovedVacancies: getApprovedVacancies,
            authoriseVacancy: authoriseVacancy,
            rejectVacancy: rejectVacancy,
            getSalaries: getSalaries,
            getAllQuestionsAndAnswers: getAllQuestionsAndAnswers,
            getGenericQuestions: getGenericQuestions,
            updateAnswerGenericQuestion: updateAnswerGenericQuestion,
            updateSpecificQuestions: updateSpecificQuestions,
            deleteAnswerSpecificQuestion: deleteAnswerSpecificQuestion,
            deleteAnswerGenericQuestion: deleteAnswerGenericQuestion,
            deleteGenericQuestion: deleteGenericQuestion,
            deleteSpecificQuestion: deleteSpecificQuestion
        };
    };

    angular.module('eRecruitmentApp').factory('authoriseVacancyService', authoriseVacancyService);
    authoriseVacancyService.$inject = ['$http', '$q', 'ngConfigSettings'];
})();