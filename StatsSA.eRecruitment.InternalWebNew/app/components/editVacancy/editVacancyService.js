(function () {
    'use strict';

    var editVacancyService = function ($http, $q, ngConfigSettings) {

        var serviceBase = ngConfigSettings.apiServiceBaseUri;

        var getEditableVacancies = function () {
            var deferred = $q.defer();

            $http.post(serviceBase + 'api/vacancy/geteditablevacancies').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };
        var getSalaries = function () {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/lookups/getsalaries').then(function (response) {
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);
            });

            return deferred.promise;
        };

        var updateAnswersThreshold = function (answersThreshold) {
            var deferred = $q.defer();
            var httpPath = serviceBase + 'api/vacancy/updateAnswersThreshold';
            // console.log('SERVICE SIDE <<save(vacancy)>> httpPath: ' + httpPath);

            $http.post(httpPath, answersThreshold)
                .then(function (response) {
                    // console.log("SERVICE SIDE <<save>>: passed >> ");
                    deferred.resolve(response);
                })

                .catch(function (error) {
                    // console.log("SERVICE SIDE <<save>>: failed >> " + JSON.stringify(error));
                    deferred.reject(error);
                });

            return deferred.promise;
        };

        var updateVacancy = function (vacancy) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/vacancy/updatevacancies', vacancy).then(function (response) {
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

        var getAnswersThreshold = function (vacancy) {
            var deferred = $q.defer();
            $http.post(serviceBase + 'api/vacancy/getAnswersThreshold', vacancy).then(function (response) {
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
            getEditableVacancies: getEditableVacancies,
            updateVacancy: updateVacancy,
            getSalaries: getSalaries,
            getAllQuestionsAndAnswers: getAllQuestionsAndAnswers,
            getGenericQuestions: getGenericQuestions,
            updateAnswerGenericQuestion: updateAnswerGenericQuestion,
            updateSpecificQuestions: updateSpecificQuestions,
            deleteAnswerSpecificQuestion: deleteAnswerSpecificQuestion,
            deleteAnswerGenericQuestion: deleteAnswerGenericQuestion,
            deleteGenericQuestion: deleteGenericQuestion,
            deleteSpecificQuestion: deleteSpecificQuestion,
            updateAnswersThreshold: updateAnswersThreshold,
            getAnswersThreshold: getAnswersThreshold

        };
    };

    angular.module('eRecruitmentApp').factory('editVacancyService', editVacancyService);
    editVacancyService.$inject = ['$http', '$q', 'ngConfigSettings'];
})();