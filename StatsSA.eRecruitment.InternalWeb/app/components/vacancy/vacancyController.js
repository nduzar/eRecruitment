(function () {
    'use strict';

    var vacancyController = function (vacancyService, $timeout, $location) {
        var vm = this;
                
        vm.ErrorMessage = null;
        vm.ShowError = false;
        vm.SuccessMessage = null;
        vm.ShowSuccess = false;
        vm.isBusy = false;
        vm.ShowSave = true;
        vm.ShowAnother = false;
        vm.salarylevels = [];
        vm.showGenericQuestions = false;
        vm.showSpecificQuestions = false;
        vm.visibleSpecificQuestions = false;
        vm.visibleGenericQuestions = false;
        vm.sumOfWeights = 0;
        vm.answersThreshold = 0;

        vm.vacancystatuses = [];
        vm.genericQuestionsDB = [];
        vm.genericQuestion;
        vm.selectedGenericQuestion;
        vm.specificQuestions = {
            title: undefined,
            questions: undefined
        };

        vm.genericQuestions = {
            title: undefined,
            questions: undefined
        };

        vm.vacancyId = undefined;

        vm.isWeightReadOnly = false;
        
        vm.listAnswersGenericQuestion = [];
        vm.listAnswersSpecificQuestion = [];

        vm.questionsAnswers = {
            listSpecificQuestionsAnswers: undefined,
            listGenericQuestionsAnswers: undefined,
            answersThreshold: undefined
        }
        
        vm.vacancy = {
            JobTitle: undefined,
            OfficeName: undefined,
            DivisionName: undefined,
            NumberOfPosts: undefined,
            ReferenceNumber: undefined,
            KeyPerformanceAreas: undefined,
            Prerequisites: undefined,
            PersonProfile: undefined,
            SalaryLevel: undefined,
            SalaryNotch: undefined,
            RemunerationDescription: undefined,
            OpeningDate: undefined,
            ClosingDate: undefined,            
            StatusId: undefined,
            StatusComment: undefined,
            IsPermanent: undefined
        }

        vm.openDateOptions = {
            formatYear: 'yy',
            minDate: new Date(),
            startingDay: 1
        };

        vm.questions = [];
        vm.autoRejection = [
            "Yes",
            "No"
        ];

        vm.questionType = [
            "Yes",
            "No"
        ];
        vm.questions = [
            "Yes",
            "No"
        ];

        vm.closeDateOptions = {
            formatYear: 'yy',
            minDate: new Date(),
            startingDay: 1
        };
        vm.updateCloseDateMin = function () {
            vm.closeDateOptions.minDate = vm.vacancy.OpeningDate;
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

        vm.showGenericQuestions = function (value) {
            vm.displayGenericQuestions = value == 'Y';
        }
         

        vm.initialisePage = function ()
        {
           // console.log("page initialised");
            
            vm.ShowSave = true;
            vm.ShowAnother = false;
            vm.ShowSuccess = false;
            vm.ShowError = false;
            vm.myToday = new Date();
            vm.isBusy = false;

            vm.vacancy = {
                JobTitle: undefined,
                OfficeName: undefined,
                DivisionName: undefined,
                NumberOfPosts: 1,
                ReferenceNumber: undefined,
                KeyPerformanceAreas: undefined,
                Prerequisites: undefined,
                PersonProfile: undefined, 
                RemunerationDescription: undefined,
                OpeningDate: undefined,
                ClosingDate: undefined,
                StatusId: undefined,
                StatusComment: undefined,
                IsPermanent: undefined
            }

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
                                'weight': 0
                            }]
                    }]
            };

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
                                'weight': 0

                            }]
                    }]
            };         
            
            vacancyService.getSalaryLevels().then(function (response) {
                //console.log("successfully called: vacancyService.getSalaryLevels()" + response.data);            
                vm.salarylevels = response.data;
            })

            vm.updateNotch = function (selectedSalary) {

                if (selectedSalary.salaryLevel === "To be Advised") {
                    vm.remunerationDescription = "per annum";
                }
                else {

                    if (selectedSalary.salaryLevel >= 11 && selectedSalary.salaryLevel <= 16) {
                        vm.remunerationDescription = "per annum (All-Incl.)";
                    }
                    else {
                        vm.remunerationDescription = "per annum (Excl. benefits)";
                    }
                }

            };

            vacancyService.getVacancyStatuses().then(function (response) {
               // console.log("successfully called: vacancyService.getVacancyStatuses()" + response.data);
                vm.vacancystatuses = response.data;
            })

            vacancyService.getGenericQuestions().then(function (response) {
                // console.log("successfully called: vacancyService.getVacancyStatuses()" + response.data);
                vm.genericQuestionsDB = response.data;
            })

        };

        vm.toggleGenericQuestions = function () {

            vm.visibleGenericQuestions = vm.visibleGenericQuestions === false ? true : false;

            if (!vm.visibleGenericQuestions) {
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
                                    'disable' : false,
                                    'weight': 0

                                }]
                        }]
                };    
            }
        }

        vm.autoRejectChange = function (answer) {
            if (answer.autoReject) {
                answer.weight = 0;
                vm.isWeightReadOnly = true;
            } else {
                answer.weight = '';
                vm.isWeightReadOnly = false;
            }
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

        vm.remove = function (item, items) {
            // console.log(items);            
            items.splice(items.indexOf(item), 1);
            items.forEach(function (elem) {
                elem.id = items.indexOf(elem) + 1;
            });

            vm.calculateSumWeight();
        };

        vm.addNewGenericQuestion = function () {
            var newQuestionNo = vm.genericQuestions.questions.length + 1;
            vm.genericQuestions.questions.push({ 'id': newQuestionNo, 'title': '', 'answers': [{ id: 1, 'title': '', 'autoReject': true, 'disable': false, 'weight': 0 }] });
            vm.calculateSumWeight();
        };

        vm.addNewGenericAnswer = function (question) {
            // console.log(this.question.answers.length);
            var newAnswerNo = question.answers.length + 1;
            vm.remove(questions, vm.genericQuestions.questions);
            var newAnswer = { 'id': newAnswerNo, 'title': '', 'autoReject': true, 'disable': false, 'weight': 0 };
            question.answers.push(newAnswer);

            vm.genericQuestions.questions.push(question);

            vm.calculateSumWeight();
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
            vm.calculateSumWeight();
        };

        vm.addNewSpecificAnswer = function (question) {
            // console.log(this.question.answers.length);
            var newAnswerNo = question.answers.length + 1;
            vm.remove(questions, vm.specificQuestions.questions);
            var newAnswer = { 'id': newAnswerNo, 'title': '', 'autoReject': true, 'disable': false, 'weight': 0 };
            question.answers.push(newAnswer);

            vm.specificQuestions.questions.push(question);
            vm.calculateSumWeight();
        };


        vm.calculateSumWeight = function () {

            var weight = 0;
            for (var i = 0; i < vm.genericQuestions.questions.length; i++) {

                for (var j = 0; j < vm.genericQuestions.questions[i].answers.length; j++) {
                    weight += parseInt(vm.genericQuestions.questions[i].answers[j].weight);
                }
            }

            for (var i = 0; i < vm.specificQuestions.questions.length; i++) {

                for (var j = 0; j < vm.specificQuestions.questions[i].answers.length; j++) {
                    weight += parseInt(vm.specificQuestions.questions[i].answers[j].weight);
                }
            }

            console.log(weight);
            vm.sumOfWeights = weight;
        }

        vm.save = function () {

            if (vm.isBusy) {
                return;
            }          

            vm.isBusy = true;

            if (vm.vacancy.salary.salaryLevel >= 11 && vm.vacancy.salary.salaryLevel <= 16) {
                vm.vacancy.RemunerationDescription = "per annum (All-Incl.)";
            }
            else
            {
                vm.vacancy.RemunerationDescription = "per annum (Excl. benefits)";
            } 
                
            var objVacancy = {
                ReferenceNumber: vm.vacancy.ReferenceNumber,
                JobTitle: vm.vacancy.JobTitle,
                OfficeName: vm.vacancy.OfficeName,
                DivisionName: vm.vacancy.DivisionName,
                NumberOfPosts: vm.vacancy.NumberOfPosts,                
                KeyPerformanceAreas: vm.vacancy.KeyPerformanceAreas,
                Prerequisites: vm.vacancy.Prerequisites,
                PersonProfile: vm.vacancy.PersonProfile,
                salary: vm.vacancy.salary,
                salaryId: vm.vacancy.salary.salaryId,
                SalaryNotch: vm.vacancy.salary.salaryNotch,               
                RemunerationDescription: vm.vacancy.RemunerationDescription,
                OpeningDate: vm.vacancy.OpeningDate,
                ClosingDate: vm.vacancy.ClosingDate,                
                StatusId: undefined,
                StatusComment: undefined,
                IsPermanent: vm.vacancy.IsPermanent
            };

            var d = new Date(objVacancy.OpeningDate);
            d.setHours(0, -d.getTimezoneOffset(), 0, 0);
            objVacancy.OpeningDate = d;
            
            d = new Date(objVacancy.ClosingDate);
            d.setHours(23, -d.getTimezoneOffset(), 0, 0);
            d.setMinutes(59,59,0);
            objVacancy.ClosingDate = d;

            if (objVacancy.OpeningDate > objVacancy.ClosingDate) {
                vm.ErrorMessage = "Opening date should not be creater than closing date";
                vm.ShowError = true;
                vm.ShowSuccess = false;
                return; 
            }

            vacancyService.save(objVacancy)
                .then(function (response) {     

                    vm.vacancyId = response.data.vacancyId;
                    vm.saveQuestionsAnswers(vm.vacancyId);

                    $timeout(function () {

                        $location.path('/home');
                    }, 10000);
                                        
                }, function (response) {
                    vm.ErrorMessage = response.data.error_description;
                    vm.ShowError = true;
                    vm.ShowSuccess = false;
                    vm.ShowAnother = false;
                    vm.ShowSave = true;
                    console.log("failed to send vacancy to a service: ");
                    });
        }

        vm.saveQuestionsAnswers = function (vacancyId) {

            //Persist questions separately this is dependent on the vacancy id
            for (var i = 0; i < vm.genericQuestions.questions.length; i++) {

                for (var j = 0; j < vm.genericQuestions.questions[i].answers.length; j++) {

                    var answerGenericQuestion = {
                        id: vm.genericQuestions.questions[i].title.id,
                        questionId: vm.genericQuestions.questions[i].title.id,
                        answer: vm.genericQuestions.questions[i].answers[j].title,
                        vacancyId: vacancyId,
                        autoReject: vm.genericQuestions.questions[i].answers[j].autoReject === '' ? false : vm.genericQuestions.questions[i].answers[j].autoReject,
                        weight: vm.genericQuestions.questions[i].answers[j].weight,
                        showSpecifyTextbox: vm.genericQuestions.questions[i].answers[j].title == 'Other' ? true : false
                    };
                    if (answerGenericQuestion.answer !== '') {
                        vm.listAnswersGenericQuestion.push(answerGenericQuestion);
                    }
                }
            }

            var specificQuestions = vm.specificQuestions.questions;
            for (var i = 0; i < specificQuestions.length; i++) {

                var listAnswersSpecificQuestions = [];

                var specificQuestion = {
                    id: i,
                    question: specificQuestions[i].title,
                    vacancyId: vacancyId
                }

                for (var j = 0; j < specificQuestions[i].answers.length; j++) {

                    var answerSpecificQuestion = {
                        id: j,
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

                if (specificQuestion.question !== '') {

                    vm.listAnswersSpecificQuestion.push(specificQuestionsAnswers)
                }
            }

            vm.questionsAnswers.listSpecificQuestionsAnswers = vm.listAnswersSpecificQuestion;
            vm.questionsAnswers.listGenericQuestionsAnswers = vm.listAnswersGenericQuestion;

            vm.calculateSumWeight();

            var answersThreshold = {
                threshold: vm.answersThreshold,
                sumOfWeights: vm.sumOfWeights,
                vacancyId: vacancyId
            }

            vm.questionsAnswers.answersThreshold = answersThreshold;

            //console.log(vm.questionsAnswers);

            vacancyService.saveQuestionsAnswers(vm.questionsAnswers).then(function (response) {
                vm.SuccessMessage = "vacancy successfully created!";
                vm.ErrorMessage = null;
                vm.ShowError = false;
                vm.ShowSuccess = true;
                vm.ShowAnother = true;
                vm.ShowSave = false;
                vm.isBusy = false;
            }, function (response) {
                vm.ErrorMessage = response.data.error_description;
                vm.ShowError = true;
                vm.ShowSuccess = false;
                vm.ShowAnother = false;
                vm.ShowSave = true;
                console.log("failed to send vacancy to a service: ");
            });
        }
    };

    angular.module('eRecruitmentApp').controller('vacancyController', vacancyController);
    vacancyController.$inject = ['vacancyService', '$timeout', '$location',];
})();
