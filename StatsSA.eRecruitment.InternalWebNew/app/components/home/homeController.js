(function () {
    'use strict';

    var homeController = function (homeService) {
        var vm = this;

        vm.initialisePage = function () {
            
        };
    };

    angular.module('eRecruitmentApp').controller('homeController', homeController);
    homeController.$inject = ['homeService'];
})();