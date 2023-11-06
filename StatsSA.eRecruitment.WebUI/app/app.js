(function () {
    'use strict';
    var app = angular.module('eRecruitmentApp', ['ngRoute',
        //'ngSessionStorage',
        'ngStorage',
        'angular-loading-bar', 'mgo-angular-wizard', 'ngSanitize', 'ui.bootstrap', 'ngFileUpload', 'naif.base64', 'toastr', 'StatssaCoreModule', 'ngMaterial']);

    var serviceBase = 'http://localhost:4810/';
  //  var serviceBase = 'https://erecruitment.treasury.gov.za/service/';
  //  var serviceBase = 'http://10.121.144.44:9002/';
  //  var serviceBase = 'http://bmapps.statssa.gov.za/eRecruitment.WebAPI/';
    app.constant('ngConfigSettings', {
        apiServiceBaseUri: serviceBase,
        clientId: 'erectruitmentDev'
    });

    app.config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptorService');
    });

    app.run(['authService', '$location', '$rootScope', function (authService, $location, $rootScope) {
        //authService.logOut();
        authService.fillAuthData();

        var postLogInRoute;

        $rootScope.$on(' ', function (event, nextRoute, currentRoute) {

            //if login required and you're logged out, capture the current path
            if (nextRoute.loginRequired && !authService.authentication.isAuth) {
                postLogInRoute = $location.path();
                $location.path('/login').replace();
            }
            if ($location.path() === "/signup") {
                postLogInRoute = null;
            }
            else if (postLogInRoute && authService.authentication.isAuth) {
                //once logged in, redirect to the last route and reset it
                $location.path(postLogInRoute).replace();
                postLogInRoute = null;
            }
        });
    }]);
})();