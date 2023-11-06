(function () {

    'use strict';

    var routeProvider = function ($routeProvider, $locationProvider, authorisationService) {
        $locationProvider.hashPrefix('');

        var viewBase = 'app/components/';
        $routeProvider.when('/home', {
            controller: 'homeController',
            disableCache: true,
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
            disableCache: true,
            templateUrl: viewBase + '/salaries/salaries.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.Capturer, authorisationService.roles.Authoriser, authorisationService.roles.Approver])
                }
            }
        });
        $routeProvider.when('/users', {
            controller: 'usersController',
            disableCache: true,
            templateUrl: viewBase + 'users/users.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.SystemAdmin])
                }
            }
        });

        $routeProvider.when('/vacancy', {
            controller: 'vacancyController',
            disableCache: true,
            templateUrl: viewBase + 'vacancy/vacancy.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.Capturer])
                }
            }
        });

        $routeProvider.when('/approveVacancy', {
            controller: 'approveVacancyController',
            disableCache: true,
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
            disableCache: true,
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
            disableCache: true,
            templateUrl: viewBase + 'shortlistedApplications/hrShortlistedApplications.html',
            controllerAs: 'vm',
            //resolve: {
            //    permission: function (authorisationService, $route) {
            //        return authorisationService.permissionsCheck([authorisationService.roles.HiringManager])
            //    }
            //}
        });

        $routeProvider.when('/manageGenericQuestions', {
            controller: 'manageGenericQuestionsController',
            disableCache: true,
            templateUrl: viewBase + 'manageGenericQuestions/manageGenericQuestions.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.Capturer, authorisationService.roles.Authoriser, authorisationService.roles.Approver])
                }
            }
        });

        $routeProvider.when('/hiringManagerShortlisting', {
            controller: 'hiringManagerShortlistedApplicationsController',
            disableCache: true,
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
            disableCache: true,
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
            disableCache: true,
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
            disableCache: true,
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
            disableCache: true,
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
            disableCache: true,
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
            disableCache: true,
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
            disableCache: true,
            templateUrl: viewBase + 'approvedList/approvedList.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.Capturer, authorisationService.roles.Authoriser, authorisationService.roles.Approver])
                }
            }
        });

        $routeProvider.when('/listApprovedVacancy', {
            controller: 'listApprovedVacancyController',
            disableCache: true,
            templateUrl: viewBase + 'listApprovedVacancy/listApprovedVacancy.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.Capturer, authorisationService.roles.Authoriser, authorisationService.roles.Approver])
                }
            }
        });

        $routeProvider.when('/listAuthorisedVacancy', {
            controller: 'listAuthorisedVacancyController',
            disableCache: true,
            templateUrl: viewBase + 'listAuthorisedVacancy/listAuthorisedVacancy.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.Capturer, authorisationService.roles.Authoriser, authorisationService.roles.Approver])
                }
            }
        });

        $routeProvider.when('/listCancelledVacancy', {
            controller: 'listCancelledVacancyController',
            disableCache: true,
            templateUrl: viewBase + 'listCancelledVacancy/listCancelledVacancy.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.Capturer, authorisationService.roles.Authoriser, authorisationService.roles.Approver])
                }
            }
        });

        $routeProvider.when('/authorisedList', {
            controller: 'authorisedListController',
            disableCache: true,
            templateUrl: viewBase + 'authorisedList/authorisedList.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.Capturer, authorisationService.roles.Authoriser, authorisationService.roles.Approver])
                }
            }
        });

        $routeProvider.when('/cancelledList', {
            controller: 'cancelledListController',
            disableCache: true,
            templateUrl: viewBase + 'cancelledList/cancelledList.html',
            controllerAs: 'vm',
            resolve: {
                permission: function (authorisationService, $route) {
                    return authorisationService.permissionsCheck([authorisationService.roles.Capturer, authorisationService.roles.Capturer, authorisationService.roles.Authoriser, authorisationService.roles.Approver])
                }
            }
        });


        $routeProvider.when('/reports', {
            controller: 'reportsController',
            disableCache: true,
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