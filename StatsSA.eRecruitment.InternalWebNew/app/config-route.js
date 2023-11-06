(function () {

    'use strict';

    var routeProvider = function ($routeProvider, $locationProvider, authorisationService) {
        $locationProvider.hashPrefix('');

        var viewBase = 'app/components/';
        $routeProvider
        .when('/home', {
            controller: 'homeController',
            templateUrl: viewBase + 'home/home.html',
            controllerAs: 'vm',
            // Commented out to show home page to everyone
            //resolve: {
            //    permission: function (authorisationService, $route) {
            //        return authorisationService.permissionsCheck([
            //            authorisationService.roles.Capturer,
            //            authorisationService.roles.Approver,
            //            authorisationService.roles.Authoriser,
            //            authorisationService.roles.Capturer,
            //            authorisationService.roles.SystemAdmin])
            //    }
            //}
        })
        .when('/salaries', {
            controller: 'salariesController',
            templateUrl: viewBase + '/salaries/salaries.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.Capturer])
                }
            }
        });
        $routeProvider.when('/users', {
            controller: 'usersController',
            templateUrl: viewBase + 'users/users.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.SystemAdmin])
                }
            }
        });
        $routeProvider.when('/approveVacancy', {
            controller: 'approveVacancyController',
            templateUrl: viewBase + 'approveVacancy/approveVacancy.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.Approver])
                }
            }
        });
        $routeProvider.when('/authoriseVacancy', {
            controller: 'authoriseVacancyController',
            templateUrl: viewBase + 'authoriseVacancy/authoriseVacancy.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.Authoriser])
                }
            }
        });

        $routeProvider.when('/hrShortlistedApplications', {
            controller: 'hrShortlistedApplicationsController',
            templateUrl: viewBase + 'shortlistedApplications/hrShortlistedApplications.html',
            controllerAs: 'vm',
            //resolve: {
            //    permission: function (authorisationService, $route) {
            //        return authorisationService.permissionsCheck([authorisationService.roles.HiringManager])
            //    }
            //}
        });

        $routeProvider.when('/hiringManagerShortlisting', {
            controller: 'hiringManagerShortlistedApplicationsController',
            templateUrl: viewBase + 'shortlistedApplications/hiringManagerShortlisting.html',
            controllerAs: 'vm',
            //resolve: {
            //    permission: function (authorisationService, $route) {
            //        return authorisationService.permissionsCheck([authorisationService.roles.HiringManager])
            //    }
            //}
        });

        $routeProvider.when('/cancelVacancy', {
            controller: 'cancelVacancyController',
            templateUrl: viewBase + 'cancelVacancy/cancelVacancy.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.Authoriser])
                }
            }
        });
        $routeProvider.when('/screeningList', {
            controller: 'listScreeningController',
            templateUrl: viewBase + 'listScreening/listScreening.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.Capturer])
                }
            }
        });
        $routeProvider.when('/rankedScreening', {
            controller: 'rankedScreeningController',
            templateUrl: viewBase + 'rankedScreening/rankedScreening.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.Capturer])
                }
            }
        });
        $routeProvider.when('/verifyScreening', {
            controller: 'verifyScreeningController',
            templateUrl: viewBase + 'verifyScreening/verifyScreening.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.Capturer])
                }
            }
        });
        $routeProvider.when('/vacancy', {
            controller: 'vacancyController',
            templateUrl: viewBase + 'vacancy/vacancy.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.Capturer, authorisationService.roles.Approver])
                }
            }
        });     
        $routeProvider.when('/editVacancy', {
            controller: 'editVacancyController',
            templateUrl: viewBase + 'editVacancy/editVacancy.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.Capturer])
                }
            }
        });
        $routeProvider.when('/approvedList', {
            controller: 'approvedListController',
            templateUrl: viewBase + 'approvedList/approvedList.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.Authoriser, authorisationService.roles.Approver])
                }
            }
        });

        $routeProvider.when('/authorisedList', {
            controller: 'authorisedListController',
            templateUrl: viewBase + 'authorisedList/authorisedList.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.Authoriser, authorisationService.roles.Approver])
                }
            }
        });

        $routeProvider.when('/cancelledList', {
            controller: 'cancelledListController',
            templateUrl: viewBase + 'cancelledList/cancelledList.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.Capturer, authorisationService.roles.Authoriser, authorisationService.roles.Approver])
                }
            }
        });


        $routeProvider.when('/reports', {
            controller: 'reportsController',
            templateUrl: viewBase + 'reports/reports.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.Authoriser, authorisationService.roles.Approver, authorisationService.roles.SystemAdmin])
                }
            }
        });
        $routeProvider.when('/unauthorizedaccess', {
            templateUrl: viewBase + 'home/unauthorizedAccess.html'
        });
       
        $routeProvider.otherwise({ redirectTo: '/home' });

    };
    angular.module('eRecruitmentApp').config(['$routeProvider', '$locationProvider', routeProvider]);
})();