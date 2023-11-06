(function () {
    'use strict';
    var app = angular.module('eRecruitmentApp', ['ngRoute', 'ngSessionStorage', 'mgo-angular-wizard', 'textAngular', 'ui.bootstrap', 'ui.grid', 'ngTable']);

    //var serviceBase = 'http://10.121.144.44:9002/';
    var serviceBase = '/';
    app.constant('ngConfigSettings', {
        apiServiceBaseUri: serviceBase,
        clientId: 'eRecruitmentInternalApp'
    });

   // app.config(['$httpProvider', function ($httpProvider) {
     //   $httpProvider.interceptors.push('authInterceptorService');
  //  }]);


    app.run(['authorisationService', '$location', '$rootScope', function (authorisationService, $location, $rootScope) {
        $rootScope.$on("$routeChangeSuccess", function (event) {
            //Do nothing for now;
            authorisationService.fillAuthData()
        });

        $rootScope.$on("$routeChangeError", function (event, current, previous, eventObj) {
            if (eventObj.authenticated === false) {
                $location.path("/unauthorizedaccess");
            }
        });
    }]);
})();