(function () {
    'use strict';

    var authService = function ($http, $q, ngConfigSettings, $localStorage) {
        var serviceBase = ngConfigSettings.apiServiceBaseUri;

        var _authentication = {
            isAuth: false,
            userName: "",
            useRefreshTokens: false,
            fullname: ""
        };


        var _login = function (loginData) {

            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password + "&client_id=" + ngConfigSettings.clientId;
            var deferred = $q.defer();

            $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).then(function (response) {

                var token = {
                    token: response.data.access_token,
                    userName: loginData.userName,
                    refreshToken: response.data.refresh_token,
                    useRefreshTokens: true,
                    fullname: response.data.fullname,
                };
                //$sessionStorage.put('authorizationData', JSON.stringify(token));
                $localStorage.authorizationData = token;
                $localStorage.NewWindow = '1';

                _authentication.isAuth = true;
                _authentication.userName = loginData.userName;
                _authentication.useRefreshTokens = loginData.useRefreshTokens;
                _authentication.fullname = response.data.fullname;
                deferred.resolve(response.data);

            }).catch(function (response) {
                _logOut();
                deferred.reject(response);
            });

            return deferred.promise;

        };

        var _logOut = function () {

            //$sessionStorage.remove('authorizationData');
            //$sessionStorage.remove('selectedUserData');
            $localStorage.$reset({
                authorizationData: undefined,
                selectedUserData: undefined
            });

            _authentication.isAuth = false;
            _authentication.userName = "";
            _authentication.useRefreshTokens = false;
            _authentication.fullname = "";
            // $location.path('/login').replace();

        };

        console.log('logoutonreload', $localStorage)
        _logOut();

        var _fillAuthData = function () {

            //var authData = JSON.parse($sessionStorage.get('authorizationData'));
            var authData = $localStorage.authorizationData;
            if (authData) {
                _authentication.isAuth = true;
                _authentication.userName = authData.userName;
                _authentication.fullname = authData.fullname;
            }

        };

        //var _refreshToken = function () {
        //    var deferred = $q.defer();

        //    var authData = $sessionStorage.get('authorizationData');

        //    if (authData) {

        //        var data = "grant_type=refresh_token&refresh_token=" + authData.refreshToken + "&client_id=" + ngConfigSettings.clientId;

        //        $sessionStorage.remove('authorizationData');

        //        $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded', 'Accept': 'application/json' } }).then(function (response) {

        //            $sessionStorage.put('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: response.refresh_token, useRefreshTokens: true });

        //            deferred.resolve(response);

        //        }).catch(function (err, status) {
        //            _logOut();
        //            deferred.reject(err);
        //        });
        //    }

        //    return deferred.promise;
        //};

        return {
            login: _login,
            logOut: _logOut,            
            fillAuthData: _fillAuthData,
            authentication: _authentication,
            //refreshToken : _refreshToken
        };

    };


    angular.module('eRecruitmentApp').factory('authService', authService);

    authService.$inject = ['$http', '$q', 'ngConfigSettings', '$localStorage'];

})();