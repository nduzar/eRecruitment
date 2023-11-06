(function () {

    'use strict';

    const newLocal = 'emailChanged/emailChanged.html';
    var routeProvider = function ($routeProvider, $locationProvider) {$locationProvider.hashPrefix('');
        var viewBase = 'app/components/';

        $routeProvider.when('/browseJobs', {
            controller: 'browseJobsController',
            templateUrl: viewBase + 'browseJobs/browseJobs.html',
            controllerAs: 'vm'
        });
        $routeProvider.when('/emailChanged', {
            controller: 'emailChangedController',
            templateUrl: viewBase + newLocal,
            controllerAs: 'vm'
        });

        $routeProvider.when('/signupsuccess', {
            //controller: 'browseJobsController',
            templateUrl: viewBase + 'success/signupsuccess.html',
            //controllerAs: 'vm'
        });

        $routeProvider.when('/viewVacancy', {
            controller: 'viewVacancyController',
            templateUrl: viewBase + 'viewVacancy/viewVacancy.html',
            controllerAs: 'vm'
        });

        $routeProvider.when('/viewInternshipVacancy', {
            controller: 'viewInternshipVacancyController',
            templateUrl: viewBase + 'viewInternshipVacancy/viewInternshipVacancy.html',
            controllerAs: 'vm'
        });

        $routeProvider.when('/applyCareer', {
            controller: 'applyCareerController',
            templateUrl: viewBase + 'applyCareer/applyCareer.html',
            controllerAs: 'vm',
            loginRequired: true
        });

        $routeProvider.when('/apply', {
            controller: 'applyController',
            templateUrl: viewBase + 'apply/apply.html',
            controllerAs: 'vm',
            loginRequired: true //
        });

        $routeProvider.when('/createProfile', {
            controller: 'createProfileController',
            templateUrl: viewBase + 'emailChanged/changedEmail.html',
            controllerAs: 'vm',
            loginRequired: true
        });

        $routeProvider.when('/login', {
            controller: 'loginController',
            templateUrl: viewBase + 'login/login.html',
            controllerAs: 'vm'
        });

        $routeProvider.when('/verifyemailrecovery', {
            controller: 'verifyEmailRecoveryController',
            templateUrl: viewBase + 'verifyemail/verifyEmailRecovery.html',
            controllerAs: 'vm'
        });

        $routeProvider.when('/verifyemail', {
            controller: 'verifyEmailController',
            templateUrl: viewBase + 'verifyemail/verifyEmail.html',
            controllerAs: 'vm'
        });

        $routeProvider.when('/manageuser', {
            controller: 'manageuserController',
            templateUrl: viewBase + 'users/manageuser.html',
            controllerAs: 'vm',
            loginRequired: true
        });

        $routeProvider.when('/manageclient', {
            controller: 'manageclientController',
            templateUrl: viewBase + 'clients/manageclient.html',
            controllerAs: 'vm',
            loginRequired: true
        });

        $routeProvider.when('/userclient', {
            controller: 'userclientcontroller',
            templateUrl: viewBase + 'userclient/userclient.html',
            controllerAs: 'vm',
            loginRequired: true
        });

        $routeProvider.when('/roles', {
            controller: 'rolesController',
            templateUrl: viewBase + 'roles/roles.html',
            controllerAs: 'vm'
        });

        $routeProvider.when('/clientroles', {
            controller: 'clientrolecontroller',
            templateUrl: viewBase + 'clientroles/clientrole.html',
            controllerAs: 'vm'
        });
        $routeProvider.when('/passwordrecovery', {
            controller: 'passwordRecoveryController',
            templateUrl: viewBase + 'forgotpassword/passwordRecovery.html',
            controllerAs: 'vm'
        });
        $routeProvider.when('/forgotpassword', {
            controller: 'forgotpasswordController',
            templateUrl: viewBase + 'forgotpassword/forgotpassword.html',
            controllerAs: 'vm'
        });
        $routeProvider.when('/resetpassword', {
            controller: 'resetpasswordController',
            templateUrl: viewBase + 'resetpassword/resetpassword.html',
            controllerAs: 'vm'
        });
        $routeProvider.when('/viewApplications', {
            controller: 'viewApplicationsController',
            templateUrl: viewBase + 'viewApplications/viewApplications.html',
            controllerAs: 'vm',
            loginRequired: true
        });

        $routeProvider.when('/signup', {
            controller: 'signupController',
            templateUrl: viewBase + 'signup/signup.html',
            controllerAs: 'vm'
        });

        $routeProvider.when('/profile/:stepIndex', {
            controller: 'profileController',
            templateUrl: viewBase + 'profile/profile.html',
            controllerAs: 'vm',
            loginRequired: true

        });

        $routeProvider.when('/viewProfile', {
            controller: 'viewProfileController',
            templateUrl: viewBase + 'viewProfile/viewProfile.html',
            controllerAs: 'vm',
            loginRequired: true
        });

        $routeProvider.when('/legal', {
            controller: 'legalController',
            templateUrl: viewBase + 'legal/legal.html',            
            controllerAs: 'vm'
        });

        $routeProvider.otherwise({ redirectTo: '/browseJobs' });

    };
    angular.module('eRecruitmentApp').config(['$routeProvider', '$locationProvider', routeProvider]);
})();