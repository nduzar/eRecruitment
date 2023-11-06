(function () {
    'use strict';

    var vacancyService = function ($q, $http, ngConfigSettings) {

        var serviceBase = ngConfigSettings.apiServiceBaseUri;//url to webapi

        var save = function (vacancy) {
            var deferred = $q.defer();
            var httpPath = serviceBase + 'api/vacancy/vacancy';
           // console.log('SERVICE SIDE <<save(vacancy)>> httpPath: ' + httpPath);

            $http.post(httpPath, vacancy)
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

        var saveGenericQuestionAnswers = function (answers) {
            var deferred = $q.defer();
            var httpPath = serviceBase + 'api/vacancy/addQuestion';
            // console.log('SERVICE SIDE <<save(vacancy)>> httpPath: ' + httpPath);

            $http.post(httpPath, answers)
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

        var saveSpecificQuestions = function (question) {
            var deferred = $q.defer();
            var httpPath = serviceBase + 'api/vacancy/addSpecificQuestions';
            // console.log('SERVICE SIDE <<save(vacancy)>> httpPath: ' + httpPath);

            $http.post(httpPath, question)
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

        var saveQuestionsAnswers = function (questionsAnswers) {
            var deferred = $q.defer();
            var httpPath = serviceBase + 'api/vacancy/addQuestionsAnswers';
            // console.log('SERVICE SIDE <<save(vacancy)>> httpPath: ' + httpPath);

            $http.post(httpPath, questionsAnswers)
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

        var addAnswersThreshold = function (answersThreshold) {
            var deferred = $q.defer();
            var httpPath = serviceBase + 'api/vacancy/addAnswersThreshold';
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

        // -- LOOKUP FUNCTIONS START -- //  
        var getSalaryLevels = function () {
            var deferred = $q.defer();            
            //var httpPath = serviceBase + 'api/lookups/getsalarylevels';
            var httpPath = serviceBase + 'api/lookups/getsalaries';
           // console.log('getsalarylevels >> httpPath: ' +httpPath);

            $http.post(httpPath).then(function (response) {            
                deferred.resolve(response);
                //console.log("getsalarylevels successful >> " + response)
            }).catch(function (response) {
                //console.log('getsalarylevels failed: ' + response.data + '<<-->>' + response.data.error_description);
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

        var getVacancyStatuses = function () {
            var deferred = $q.defer();
            var httpPath = serviceBase + 'api/vacancy/getgenericquestions';
            //console.log('getvacancystatuses >> httpPath: ' + httpPath);

            $http.get(httpPath).then(function (response) {
                deferred.resolve(response);
                //console.log("getvacancystatuses successful >> " + response)
            }).catch(function (response) {
                //console.log('getvacancystatuses failed >>: ' + response.data + '<<-->>' + response.data.error_description);
                deferred.reject(response);
            });
            return deferred.promise;
        };
        // -- LOOKUP FUNCTIONS END -- //

        return {
            save: save,          
            getSalaryLevels: getSalaryLevels,
            getVacancyStatuses: getVacancyStatuses,
            getGenericQuestions: getGenericQuestions,
            saveGenericQuestionAnswers: saveGenericQuestionAnswers,
            saveSpecificQuestions: saveSpecificQuestions,
            addAnswersThreshold: addAnswersThreshold,
            saveQuestionsAnswers: saveQuestionsAnswers
        };

    };

    angular.module('eRecruitmentApp').factory('vacancyService', vacancyService);
    vacancyService.$inject = ['$q', '$http', 'ngConfigSettings'];
})();