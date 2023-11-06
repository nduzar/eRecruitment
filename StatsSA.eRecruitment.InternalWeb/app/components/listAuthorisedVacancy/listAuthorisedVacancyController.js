(function () {
    'use strict';

    var listAuthorisedVacancyController = function (listAuthorisedVacancyService, WizardHandler, uiGridConstants, $filter, $timeout, $location) {
        var vm = this;
        vm.filterPost = null;
        vm.filterTitle = null;
        vm.isLoading = true;
        vm.noRecords = false;
        vm.salaries = [];
        vm.ErrorMessage = null;
        vm.ShowError = false;
        vm.SuccessMessage = null;
        vm.ShowSuccess = false;
        vm.canEdit = false;
        vm.editMode = function (state) {
            vm.canEdit = true;
        };
        vm.visibleGenericQuestions = false;
        vm.vacancies = [];
        vm.selectedVacancy = [];
        vm.hideConfirmReason = true;
        vm.rejectionReason = null;
        vm.sortBy = null;
        vm.setSorting = function (sortColumn) {
            if (vm.sortBy == sortColumn) {
                vm.sortBy = '-'+sortColumn
            } else {
                vm.sortBy = sortColumn;
            }
            console.log("SORTING: " + sortColumn);
        };

        vm.showSpecificQuestions = false;
        vm.visibleSpecificQuestions = false;
        vm.genericQuestionsDB = [];
        vm.selectedGenericQuestion = [];
        vm.selectedGenericQuestionAnswers = [];
        vm.visibleGenericQuestions = false;

        vm.genericQuestions = {
            title: '',
            questions: []
        };

        vm.listAnswersGenericQuestion = [];

        vm.specificQuestions = {
            title: '',
            questions: []
        };

        vm.allQuestionsAndAnswers = undefined;

        vm.visibleGenericQuestions = false;


        vm.searchFilter = undefined;
        vm.searchString = null;
        vm.setFilter = function () {
            vm.searchFilter = vm.searchString;
            console.log(vm.searchString);
        };
        vm.clearFilter = function () {
            vm.searchFilter = undefined;
            vm.searchString = null;
        };

        vm.openDateOptions = {
            formatYear: 'yy',
            minDate: new Date(),
            startingDay: 1
        };
        vm.closeDateOptions = {
            formatYear: 'yy',
            minDate: new Date(),
            startingDay: 1
        };
        vm.updateCloseDateMin = function () {
            vm.closeDateOptions.minDate = vm.selectedVacancy.openingDate;
        };
        vm.formats = ['yyyy-MM-dd', 'dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        vm.format = vm.formats[0];

        vm.openDatePopup = {
            opened: false
        };
        vm.openDateShowPopup = function () {
            vm.openDatePopup.opened = true;
        };
        vm.closeDatePopup = {
            opened: false
        };
        vm.closeDateShowPopup = function () {
            vm.closeDatePopup.opened = true;
        };

        vm.initialisePage = function () {
            vm.isLoading = true;
            vm.ShowSuccess = false;
            vm.SuccessMessage = "";
            listAuthorisedVacancyService.getSalaries().then(function (response) {
                vm.salaries = response.data;
                if (response.data != null) { }
            }, function (error) {
                console.log(JSON.stringify(error));
            });
            listAuthorisedVacancyService.getauthorisedList().then(function (response) {
                vm.vacancies = response.data;
                vm.isLoading = false;
                if (response.data.length == 0) {
                    vm.noRecords = true;
                } else {
                    vm.noRecords = false
                }
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        };
        vm.myToday = null;
        vm.viewVacancy = function (selectedVacancy) {
            vm.isLoading = true;
            vm.selectedVacancy = angular.copy(selectedVacancy);  
            vm.selectedVacancy.rejectionReason
            vm.selectedVacancy.openingDate = new Date(vm.selectedVacancy.openingDate);
            vm.selectedVacancy.closingDate = new Date(vm.selectedVacancy.closingDate);
            var d = new Date();
            vm.myToday = d;
            vm.getAllQuestionsAndAnswers(selectedVacancy);
            WizardHandler.wizard().next();
        };       

        vm.back = function () {
            vm.hideConfirmReason = true;
            vm.rejectionReason = null;
            WizardHandler.wizard().previous();
        };
        vm.update = function (vacancy) {
            if (vm.isBusy) {
                return;
            }

            vm.isBusy = true;


            var d = new Date(vacancy.openingDate);
            d.setHours(0, -d.getTimezoneOffset(), 0, 0);
            vacancy.openingDate = d;

            d = new Date(vacancy.closingDate);
            d.setHours(23, -d.getTimezoneOffset(), 0, 0);
            d.setMinutes(59, 59, 0);
            vacancy.closingDate = d;

            if (vacancy.openingDate > vacancy.closingDate) {
                vm.ErrorMessage = "Opening date should not be creater than closing date";
                vm.ShowError = true;
                vm.ShowSuccess = false;
                return;
            }

            if (vm.validate(vacancy)) {

                listAuthorisedVacancyService.updateVacancy(vacancy).then(function (response) {
                    vm.ShowSuccess = true;
                    vm.SuccessMessage = "vacancy successfully updated!";
                    vm.isBusy = false;
                    vm.initialisePage();
                    vm.hideConfirmReason = true;
                    //WizardHandler.wizard().previous();

                    $timeout(function () {

                        vm.back();
                    }, 10000);

                }, function (error) {
                    console.log(JSON.stringify(error));
                    vm.isBusy = true;
                });
            }
        };
        vm.confirmReason = function () {
            vm.rejectionReason = null;
            vm.hideConfirmReason = false;
        }
        vm.reject = function (vacancy) {
            var d = new Date(vacancy.openingDate);
            d.setHours(0, -d.getTimezoneOffset(), 0, 0);
            vacancy.openingDate = d;

            d = new Date(vacancy.closingDate);
            d.setHours(23, -d.getTimezoneOffset(), 0, 0);
            d.setMinutes(59, 59, 0);
            vacancy.closingDate = d;
            vacancy.statusComment = vm.rejectionReason;
            listAuthorisedVacancyService.rejectVacancy(vacancy).then(function (response) {
                vm.initialisePage();
                WizardHandler.wizard().previous();
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        };
        vm.updateNotch = function (selectedSalary) {

            vm.selectedVacancy.salaryNotch = selectedSalary.salaryNotch;
            vm.selectedVacancy.salaryId = selectedSalary.salaryId;
            if (selectedSalary.salaryLevel >= 11 && selectedSalary.salaryLevel <= 16) {
                vm.selectedVacancy.remunerationDescription = "all-inclusive remuneration package per annum";
            }
            else {
                vm.selectedVacancy.remunerationDescription = "per annum (Excl. benefits)";
            }
        };

        vm.getKey = function (key) {
            // 10 is the radix, which is the base (assumed to be base 10 here) 
            return parseInt(key, 10);
        }

        vm.toggleGenericQuestions = function () {

            vm.visibleGenericQuestions = vm.visibleGenericQuestions === false ? true : false;

            if (!vm.visibleGenericQuestions) {
                vm.buildGenericQuestionsAnswers();
            }
        }

        vm.getAllQuestionsAndAnswers = function (vacancy) {

            listAuthorisedVacancyService.getGenericQuestions().then(function (response) {
                // console.log("successfully called: vacancyService.getVacancyStatuses()" + response.data);
                vm.genericQuestionsDB = response.data;
            });

            listAuthorisedVacancyService.getAllQuestionsAndAnswers(vacancy).then(function (response) {
                vm.allQuestionsAndAnswers = response.data;
                if (vm.allQuestionsAndAnswers != null) {
                    var generic = vm.allQuestionsAndAnswers.genericQuestionsAnswers;
                    var specific = vm.allQuestionsAndAnswers.specificQuestionsAnswers;
                    if (generic.length > 0) {
                        vm.visibleGenericQuestions = true;

                        for (var i = 0; i < generic.length; i++) {
                            //vm.selectedGenericQuestion = generic[i].genericQuestion;
                            //vm.selectedGenericQuestionAnswers = generic[i].listAnswersGenericQuestions;
                            // vm.buildGenericQuestionsAnswers();

                            var genericAnswers = generic[i].listAnswersGenericQuestions;
                            var listAnswers = [];

                            for (var j = 0; j < genericAnswers.length; j++) {
                                var answers = {
                                    id: genericAnswers[j].id,
                                    title: genericAnswers[j].answer,
                                    autoReject: genericAnswers[j].autoReject,
                                    disable: false,
                                    weight: genericAnswers[j].weight
                                }
                                listAnswers.push(answers);
                            }

                            var genericQuestion = {
                                'id': generic[i].genericQuestion.id,
                                'title': generic[i].genericQuestion.question
                            };

                            vm.genericQuestions.questions.push({ 'genericQuestion': genericQuestion, 'answers': listAnswers });
                            console.log(vm.genericQuestions);

                        }
                    }

                    if (specific.length > 0) {
                        vm.visibleSpecificQuestions = true;

                        for (var i = 0; i < specific.length; i++) {

                            var specificAnswers = specific[i].listAnswersSpecificQuestions;
                            var listSpecificAnswers = [];

                            for (var j = 0; j < specificAnswers.length; j++) {
                                var specificAnswer = {
                                    id: specificAnswers[j].id,
                                    title: specificAnswers[j].answer,
                                    autoReject: specificAnswers[j].autoReject,
                                    disable: false,
                                    weight: specificAnswers[j].weight
                                }
                                listSpecificAnswers.push(specificAnswer);
                            }

                            var specificQuestion = {
                                'id': specific[i].specificQuestion.id,
                                'title': specific[i].specificQuestion.question
                            };

                            vm.specificQuestions.questions.push({ 'specificQuestion': specificQuestion, 'answers': listSpecificAnswers });
                            console.log(vm.specificQuestions);
                        }
                    }
                }
                vm.isLoading = false;
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        }

        vm.toggleSpecificQuestions = function () {

            vm.visibleSpecificQuestions = vm.visibleSpecificQuestions === false ? true : false;

            if (!vm.visibleSpecificQuestions) {
                vm.specificQuestions = {
                    'title': '',
                    'questions':
                        [{
                            id: 1,
                            'title': '',
                            'answers':
                                [{
                                    id: 1,
                                    'title': '',
                                    'autoReject': true,
                                    'disable': false,
                                    'weight': 0
                                }]
                        }]
                };
            }
        }

        vm.buildGenericQuestionsAnswers = function () {

            vm.genericQuestions = {
                'title': '',
                'questions':
                    [{
                        id: 1,
                        'title': '',
                        'answers':
                            [{
                                id: 1,
                                'title': '',
                                'autoReject': true,
                                'disable': false,
                                'weight': 0

                            }]
                    }]
            };
        }

        vm.removeGenericAnswers = function (item, items) {

            vm.remove(item, items);

            listAuthorisedVacancyService.deleteAnswerGenericQuestion(item).then(function (response) {
                console.log(response);
            });
        };

        vm.removeSpecificAnswers = function (item, items) {

            vm.remove(item, items);

            listAuthorisedVacancyService.deleteAnswerSpecificQuestion(item).then(function (response) {
                console.log(response);
            });
        };


        vm.removeGenericQuestion = function (item, items) {

            vm.remove(item, items);

            var question = {
                id: item.genericQuestion.id,
                question: item.genericQuestion.title,
                description: item.genericQuestion.title
            }


            listAuthorisedVacancyService.deleteGenericQuestion(question).then(function (response) {
                console.log(response);
            });
        };

        vm.removeSpecificQuestion = function (item, items) {

            vm.remove(item, items);

            var question = {
                id: item.specificQuestion.id,
                question: item.specificQuestion.title,
                vacancyId: 0
            }


            listAuthorisedVacancyService.deleteSpecificQuestion(question).then(function (response) {
                console.log(response);
            });
        };

        vm.remove = function (item, items) {
            // console.log(items);
            items.splice(items.indexOf(item), 1);
            items.forEach(function (elem) {
                elem.id = items.indexOf(elem) + 1;
            });
        };

        vm.addNewGenericQuestion = function () {
            var newQuestionNo = vm.genericQuestions.questions.length + 1;
            vm.genericQuestions.questions.push({ 'id': newQuestionNo, 'title': '', 'answers': [{ id: 1, 'title': '', 'autoReject': true, 'disable': false, 'weight': 0 }] });
        };

        vm.addNewGenericAnswer = function (question) {
            // console.log(this.question.answers.length);
            var newAnswerNo = question.answers.length + 1;
            vm.remove(questions, vm.genericQuestions.questions);
            var newAnswer = { 'id': newAnswerNo, 'title': '', 'autoReject': true, 'disable': false, 'weight': 0 };
            question.answers.push(newAnswer);

            vm.genericQuestions.questions.push(question);
        };

        vm.addOtherAsNewGenericAnswer = function (question) {

            var exists = false;

            for (var i = 0; i < question.answers.length; i++) {

                if (question.answers[i].title == 'Other') {
                    exists = true
                    break;
                }
            }

            if (!exists) {

                var newAnswerNo = question.answers.length + 1;
                vm.remove(questions, vm.genericQuestions.questions);
                var newAnswer = { 'id': newAnswerNo, 'title': 'Other', 'autoReject': false, 'disable': true, 'weight': 0 };
                question.answers.push(newAnswer);

                vm.genericQuestions.questions.push(question);
            }
        };

        vm.addNewSpecificQuestion = function () {
            var newQuestionNo = vm.specificQuestions.questions.length + 1;
            vm.specificQuestions.questions.push({ 'id': newQuestionNo, 'title': '', 'answers': [{ id: 1, 'title': '', 'autoReject': true, 'disable': false, 'weight': 0 }] });
        };

        vm.addNewSpecificAnswer = function (question) {
            // console.log(this.question.answers.length);
            var newAnswerNo = question.answers.length + 1;
            vm.remove(questions, vm.specificQuestions.questions);
            var newAnswer = { 'id': newAnswerNo, 'title': '', 'autoReject': true, 'disable': false, 'weight': 0 };
            question.answers.push(newAnswer);

            vm.specificQuestions.questions.push(question);
        };

        vm.validate = function (vacancy) {
            if (vacancy.jobTitle === '' || vacancy.jobTitle == undefined) {
                vm.ErrorMessage = 'Job title is required';
                vm.ShowError = true;
                return false;
            }
            if (vacancy.officeName === '' || vacancy.officeName == undefined) {
                vm.ErrorMessage = 'Centre is required';
                vm.ShowError = true;
                return false;
            }
            if (vacancy.divisionName === '' || vacancy.divisionName == undefined) {
                vm.ErrorMessage = 'Division is required';
                vm.ShowError = true;
                return false;
            }
            if (vacancy.numberOfPosts === '' || vacancy.numberOfPosts == undefined) {
                vm.ErrorMessage = 'Number of posts is required';
                vm.ShowError = true;
                return false;
            }
            if (vacancy.keyPerformanceAreas === '' || vacancy.keyPerformanceAreas == undefined) {
                vm.ErrorMessage = 'Key Performance Area is required';
                vm.ShowError = true;
                return false;
            }
            if (vacancy.prerequisites === '' || vacancy.prerequisites == undefined) {
                vm.ErrorMessage = 'Prerequisites is required';
                vm.ShowError = true;
                return false;
            }
            if (vacancy.personProfile === '' || vacancy.personProfile == undefined) {
                vm.ErrorMessage = 'Personal profile is required';
                vm.ShowError = true;
                return false;
            }
            if (vacancy.salaryId === '' || vacancy.salaryId == undefined) {
                vm.ErrorMessage = 'Salary is required';
                vm.ShowError = true;
                return false;
            }

            if (vacancy.openingDate === '' || vacancy.openingDate == undefined) {
                vm.ErrorMessage = 'Opening Date is required';
                vm.ShowError = true;
                return false;
            }
            if (vacancy.closingDate === '' || vacancy.closingDate == undefined) {
                vm.ErrorMessage = 'Closing Date is required';
                vm.ShowError = true;
                return false;
            }
            if (vacancy.referenceNumber === '' || vacancy.referenceNumber == undefined) {
                vm.ErrorMessage = 'Reference Number is required';
                vm.ShowError = true;
                return false;
            }
            return true;
        }

        vm.updateQuestions = function (vacancy) {
            //Persist questions separately this is dependent on the vacancy id
            for (var i = 0; i < vm.genericQuestions.questions.length; i++) {

                for (var j = 0; j < vm.genericQuestions.questions[i].answers.length; j++) {

                    var answerGenericQuestion = {
                        id: vm.genericQuestions.questions[i].answers[j].id,
                        questionId: vm.genericQuestions.questions[i].genericQuestion.id,
                        answer: vm.genericQuestions.questions[i].answers[j].title,
                        vacancyId: vacancy.vacancyId,
                        autoReject: vm.genericQuestions.questions[i].answers[j].autoReject === '' ? false : vm.genericQuestions.questions[i].answers[j].autoReject,
                        weight: vm.genericQuestions.questions[i].answers[j].weight,
                        showSpecifyTextbox: vm.genericQuestions.questions[i].answers[j].title == 'Other' ? true : false
                    };
                    if (answerGenericQuestion.answer !== '') {
                        vm.listAnswersGenericQuestion.push(answerGenericQuestion);
                    }
                }
            }

            if (vm.listAnswersGenericQuestion.length > 0) {
                listAuthorisedVacancyService.updateAnswerGenericQuestion(vm.listAnswersGenericQuestion);
            }

            var specificQuestions = vm.specificQuestions.questions;
            var listSpecificQuestionsAnswers = [];
            for (var i = 0; i < specificQuestions.length; i++) {

                var listAnswersSpecificQuestions = [];

                var specificQuestion = {
                    id: specificQuestions[i].specificQuestion.id,
                    question: specificQuestions[i].specificQuestion.title,
                    vacancyId: vacancy.vacancyId
                }

                for (var j = 0; j < specificQuestions[i].answers.length; j++) {

                    var answerSpecificQuestion = {
                        id: specificQuestions[i].answers[j].id,
                        questionId: specificQuestion.id,
                        answer: specificQuestions[i].answers[j].title,
                        autoReject: specificQuestions[i].answers[j].autoReject === '' ? false : specificQuestions[i].answers[j].autoReject,
                        weight: specificQuestions[i].answers[j].weight
                    }
                    listAnswersSpecificQuestions.push(answerSpecificQuestion);
                }

                var specificQuestionsAnswers = {
                    specificQuestion: specificQuestion,
                    listAnswersSpecificQuestions: listAnswersSpecificQuestions
                }

                listSpecificQuestionsAnswers.push(specificQuestionsAnswers);
            }

            if (listSpecificQuestionsAnswers.length > 0) {

                listAuthorisedVacancyService.updateSpecificQuestions(listSpecificQuestionsAnswers).then(function (response) {
                    console.log(response);
                });
            }
        }
    };
    angular.module('eRecruitmentApp').controller('listAuthorisedVacancyController', listAuthorisedVacancyController);
    listAuthorisedVacancyController.$inject = ['listAuthorisedVacancyService', 'WizardHandler', 'uiGridConstants', '$filter', '$timeout', '$location'];
})();