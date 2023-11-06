(function () {
    'use strict';

    var manageGenericQuestionsController = function (manageGenericQuestionsService, WizardHandler, uiGridConstants, $filter, $timeout, $location) {
        var vm = this;
        vm.isLoading = true;
        vm.questions = [];
        vm.ShowError = false;
        vm.ErrorMessage = false;
        vm.ShowSuccess = false;
        vm.SuccessMessage = '';

        vm.initialisePage = function () {
            vm.isLoading = true;
            manageGenericQuestionsService.getAllGenericQuestions().then(function (response) {
                vm.isLoading = false;
                vm.questions = response.data;
                vm.ShowError = false;
                vm.ShowSuccess = false;
                if (response.data != null) { }
            }, function (error) {
               
                vm.ShowError = true;
                vm.ShowSuccess = false;
                vm.isLoading = false;
            });
        };

        vm.saveGenericQuestions = function () {

            var questions = vm.questions.filter(function (item) {
                return item.question !== ''
            });

            manageGenericQuestionsService.saveAllGenericQuestions(questions).then(function (response) {
                vm.isLoading = false;
                vm.SuccessMessage = "Generic questions successfully saved";
                vm.ShowError = false;
                vm.ShowSuccess = true;

                $timeout(function () {

                    $location.path('/home');
                }, 10000);

                if (response.data != null) { }
            }, function (error) {
                vm.ShowError = false;
                vm.ErrorMessage = false;
                vm.ErrorMessage = "An error occured while saving the data";
                vm.ShowSuccess = false;
                console.log(JSON.stringify(error));
                vm.isLoading = false;
            });
        }

        vm.cancel = function () {
            vm.initialisePage();
        };

        //vm.deleteQuestion = function (question) {
        //    manageGenericQuestionsService.deleteQuestion(question).then(function (response) {
        //        vm.isLoading = false;
        //        vm.SuccessMessage  = "Question successfully deleted";
        //        vm.ShowError = false;
        //        vm.ShowSuccess = true;
        //        if (response.data != null) { }
        //    }, function (error) {
        //        vm.ShowError = false;
        //        vm.ErrorMessage = false;
        //        vm.ErrorMessage = "An error occured while deleting question";
        //        vm.ShowSuccess = false;
        //        console.log(JSON.stringify(error));
        //        vm.isLoading = false;
        //    });
        //}

        vm.addNewGeneric = function () {
            var newQuestion = { 'id': 0, 'description': '', 'question': '' };
            vm.questions.push(newQuestion);
        };

        vm.remove = function (item, items) {
            // console.log(items);            
            items.splice(items.indexOf(item), 1);
            items.forEach(function (elem) {
                elem.id = items.indexOf(elem) + 1;
            });
        };

        vm.getKey = function (key) {
            // 10 is the radix, which is the base (assumed to be base 10 here) 
            return parseInt(key, 10);
        }
    };
    angular.module('eRecruitmentApp').controller('manageGenericQuestionsController', manageGenericQuestionsController);
    manageGenericQuestionsController.$inject = ['manageGenericQuestionsService', 'WizardHandler', 'uiGridConstants', '$filter', '$timeout', '$location'];
})();