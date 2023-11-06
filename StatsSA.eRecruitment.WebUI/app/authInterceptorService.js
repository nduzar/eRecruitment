﻿(function () { 
'use strict';
angular.module('eRecruitmentApp').factory('authInterceptorService', ['$q', '$injector', '$location', function ($q, $injector, $location) {

    var authInterceptorServiceFactory = {};
    //var sessionStorage = $injector.get('$sessionStorage');
    var localStorage = $injector.get('$localStorage');
    var _request = function (config) {

        if (config.url.indexOf('app/component') !== -1 && config.url.indexOf('.html') !== -1) {
            config.url = config.url + '?t=' + Number(Date.now());
        }

        config.headers = config.headers || {};

       // var authData = JSON.parse(sessionStorage.get('authorizationData'));
        var authData = localStorage.authorizationData;


        if (authData) {

            config.headers.Authorization = 'Bearer ' + authData.token;
            //var apiPattern = /\/api\//;

            //config.params = config.params || {};

            //if (authData.token && apiPattern.test(config.url)) {
            //    config.params.access_token = authData.token;
            //}
            if (config.url.indexOf('uploadfiles') > -1) {
                config.headers = config.headers || {};
            }
            else {
                config.headers['Content-Type'] = 'application/json';
            }            
        }

        return config;
    };

    var _responseError = function (rejection) {
        console.log(rejection);
        return $q.reject(rejection);
    };

    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
}]);
})();