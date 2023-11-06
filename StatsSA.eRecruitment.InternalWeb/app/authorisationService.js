(function () {
    'use strict';

    var authorisationService = function ($http, $q, ngConfigSettings, $sessionStorage, $rootScope) {

        var serviceBase = ngConfigSettings.apiServiceBaseUri;
        var _roles = {
            "Capturer": 1, "Approver": 2, "Authoriser": 3, "SystemAdmin": 4, "HiringManager": 5};
        var _enableRouteProtection = false;
        var _authorisation = {
            isAuth: false,
            authMessage: null,
            userName: "",
            userRoles: [],
            showCaptMenu: false,
            showApprMenu: false,
            showAuthMenu: false,
            showSysAMenu: false,
            showHiringManagerMenu: false
        };
        var _createMenu = function () {
            _authorisation.showCaptMenu = false;
            _authorisation.showApprMenu = false;
            _authorisation.showAuthMenu = false;
            _authorisation.showSysAMenu = false;
            _authorisation.showHiringManagerMenu = false;

            angular.forEach(_roles, function (role) {
                angular.forEach(_authorisation.userRoles, function (userRole) {
                    if (role == userRole.hrRoleId) {
                        switch(role) {
                            case 1:
                                _authorisation.showCaptMenu = true;
                                break;
                            case 2:
                                _authorisation.showApprMenu = true;
                                break;
                            case 3:
                                _authorisation.showAuthMenu = true;
                                break;
                            case 4:
                                _authorisation.showSysAMenu = true;
                                break;
                            case 5:
                                _authorisation.showHiringManagerMenu = true;
                                break;
                        }
                    }
                });
            });
        };
        var _fillAuthData = function () {
            var deferred = $q.defer();
            //var authData = null;
            //authData = JSON.parse($sessionStorage.get('authorizationData'));
            
            //if (authData != null) {
                
            //    _authorisation.isAuth = authData.isAuth;
            //    _authorisation.userName = authData.userName;
            //    _authorisation.userRoles = authData.userRoles;
            //    _createMenu();
            //    deferred.resolve(_authorisation);
            //} 
            //else
            //{
                $http.post(serviceBase + "api/users/getauthorisation").then(function (result) {
                    if (result.data) {
                        if (result.data.loggedIn) {
                            _authorisation.isAuth = result.data.user.isActive;
                            _authorisation.userName = result.data.user.hrUserName;
                            _authorisation.userRoles = result.data.user.hrUserRoles;
                            _createMenu();
                            $sessionStorage.put('authorizationData', JSON.stringify(_authorisation));
                        }
                        else {
                            _authorisation.isAuth = result.data.loggedIn;
                            _authorisation.authMessage = result.data.loggedInMessage;
                        }
                       
                    }
                    deferred.resolve(_authorisation);
                }, function (error) {
                    deferred.reject(error);
                });
            //}
            
            return deferred.promise;
        };
        var _returnAuth = function () {
            var authData = JSON.parse($sessionStorage.get('authorizationData'));
            if (authData) {
                return authData;
            }
            else {
                return null;
            }
        };
        var _recheckAuth = function () {
            var deferred = $q.defer();
            $http.post(serviceBase + "api/users/getauthorisation").then(function (result) {
                console.log(JSON.stringify(result.data));
                if (result.data) {
                    if (result.data.loggedIn) {
                        _authorisation.isAuth = result.data.user.isActive;
                        _authorisation.userName = result.data.user.hrUserName;
                        _authorisation.userRoles = result.data.user.hrUserRoles;
                        $sessionStorage.put('authorizationData', JSON.stringify(_authorisation));
                    }
                    else {
                        _authorisation.isAuth = result.data.loggedIn;
                        _authorisation.userRoles = [];
                        _authorisation.authMessage = result.data.loggedInMessage;
                    }
                    _createMenu();
                }
                deferred.resolve(_authorisation);
            }, function (error) {
                deferred.reject(error);
            });

            return deferred.promise;
        }

        var _permissionsCheck = function (roles) {
            if (_authorisation.isAuth) {
                _enableRouteProtection = true;
                
                return _verifyRouteRolePermission(_authorisation, roles, $q);
            }
            else {
                if (_enableRouteProtection) {
                    
                    return $q.reject({ authenticated: false });
                }
                else {
                    deferred.resolve();
                }                
            }
        };
        var _verifyRouteRolePermission = function (authorisation, roles, $q) {
            var hasRoleAccess = false;
            angular.forEach(roles, function (role) {
                angular.forEach(authorisation.userRoles, function (userRole) {
                    if (role == userRole.hrRoleId) {
                        hasRoleAccess = true;
                    }
                });
            });
            if (hasRoleAccess) {
                return $q.when(hasRoleAccess);
            }
            else {
                return $q.reject({ authenticated: false });
            }
        };

        return {
            createMenu: _createMenu,
            fillAuthData: _fillAuthData,
            authorisation: _authorisation,
            recheckAuth: _recheckAuth,
            roles: _roles,
            permissionsCheck: _permissionsCheck
        };

    };


    angular.module('eRecruitmentApp').factory('authorisationService', authorisationService);

    authorisationService.$inject = ['$http', '$q', 'ngConfigSettings', '$sessionStorage'];

})();