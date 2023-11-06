(function () { 
'use strict';
angular.module('eRecruitmentApp').factory('authInterceptorService', ['$q', '$injector', '$location', '$sessionStorage', function ($q, $injector, $location, localStorageService) {

    var authInterceptorServiceFactory = {};
    var sessionStorage = $injector.get('$sessionStorage');
    var _request = function (config) {

        config.headers = config.headers || {};

        var authData = JSON.parse(sessionStorage.get('authorizationData'));
        if (authData) {
            config.headers.Authorization = 'Bearer ' + authData.token;
            if (config.url.includes('uploadfiles')) {
                config.headers = config.headers || {};
            }
            else {
                config.headers['Content-Type'] = 'application/json';
            }            
        }

        return config;
    };

    //var _responseError = function (rejection) {
    //    var r = rejection;
    //};

    authInterceptorServiceFactory.request = _request;
   // authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
}]);
})();