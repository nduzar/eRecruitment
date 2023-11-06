(function () {
    'use strict';

    var homeController = function (homeService) {
        var vm = this;

        vm.userCount = 0;
        vm.appCount = 0;
        vm.roleCount = 0;

        vm.initialisePage = function () {
            homeService.getHomeData().then(function (response) {
                vm.userCount = response.data.userCount;
                vm.appCount = response.data.clientCount;
                vm.roleCount = response.data.roleCount;
            }, function (error) {
                console.log(error);
            });
        };
    };

    angular.module('eRecruitmentApp').controller('homeController', homeController);
    homeController.$inject = ['homeService'];
})();