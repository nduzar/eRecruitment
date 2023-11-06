(function () {
    'use strict';

    var forgotpasswordController = function (forgotpasswordService, WizardHandler, $location) {
        var vm = this;
        vm.resertPasswordMethod = undefined;
        vm.message = "";

        vm.forgotpasswordData = {
            identityNumber: undefined,
            //identityNumber: undefined
        }

        vm.searchTerms = {
            identityNumber: undefined,
            captcha: undefined
        };

        vm.userAccount = {};
        vm.message = undefined;
        vm.maskedCellphone = '';

        vm.searchAccount = function (searchTerms) {
            console.log(searchTerms);
            if (searchTerms.captcha != vm.forgotpasswordData.captcha) {
                vm.message = 'Capture valid ID or Captcha';
                vm.invalidCaptcha = true;
                return;
            }

            forgotpasswordService.searchAccount(searchTerms.identityNumber).then(function (response) {
                vm.userAccount = response;               

                WizardHandler.wizard().next();
                vm.message = "";
            }, function (error) {
                vm.message = error.data;
            });
        };
        vm.generateImage = function (captcha) {
            var width = 400;
            var height = 120;
            var digits = [];
            var lineColors = ['#939598', '#231f20', '#d1d3d4']; //'#0056a7',
            var textColors = ['#0056a7', '#939598', '#231f20', '#d1d3d4'];
            var fonts = ['30pt Times New Roman', 'italic 25pt Comic Sans MS', '30pt Courier New', '25pt Lucida Console'];
            var backgroundColors = ['#ffffff'];
            var numberOfLines = Math.floor((Math.random() * 5) + 3);
            var canvas = document.getElementById("captcha_image");
            var context = canvas.getContext('2d');
            prepCanvas();
            fillDigits();
            printDigits();

            function prepCanvas() {
                context.canvas.width = width;
                context.canvas.height = height;

                context.fillStyle = backgroundColors[0];
                context.fillRect(0, 0, width, height);

                for (var i = 0; i < numberOfLines; i++) {
                    drawLine(canvas);
                }
            };
            function drawLine() {
                var randomWidth = Math.floor((Math.random() * 4) + 2);
                var randomColor = lineColors[Math.floor((Math.random() * lineColors.length))];
                var startX = Math.floor((Math.random() * width) + 1) - randomWidth;
                var startY = Math.floor((Math.random() * height) + 1) - randomWidth;
                var endX = Math.floor((Math.random() * width) + 1) - randomWidth;
                var endY = Math.floor((Math.random() * height) + 1) - randomWidth;
                context.beginPath();
                context.moveTo(startX, startY);
                context.lineTo(endX, endY);
                context.lineWidth = randomWidth;
                context.strokeStyle = randomColor;
                context.stroke();
            };
            function printDigits() {
                var numDigits = digits.length;
                var spacingX = width / numDigits;
                var spacingY = height / 2;
                var placingX = spacingX / 2;
                var pos = true;
                for (var i = 0; i < digits.length; i++) {
                    
                    var offsetY = 15;
                    if (pos) {
                        offsetY = offsetY * -1;
                        pos = false;
                    }
                    else {
                        pos = true;
                    }
                    
                    context.font = fonts[Math.floor((Math.random() * fonts.length))];
                    context.fillStyle = textColors[Math.floor((Math.random() * textColors.length))];
                    context.fillText(digits[i], placingX, spacingY + offsetY);
                    context.save();
                   
                    placingX += (spacingX - 15);
                }
            }
            function fillDigits() {
                digits = [];
                for (var i = 0; i < captcha.length; i++) {
                    digits[i] = captcha.substring(i, i+1);
                }
            }
        };
        vm.initialisePage = function ()
        {   
            vm.forgotpasswordData.captcha = forgotpasswordService.generateCaptcha();
            angular.element(document).ready(function () {
                vm.generateImage(vm.forgotpasswordData.captcha);
            });
            
        };

        vm.generateCaptcha = function ()
        {
            vm.forgotpasswordData.captcha = forgotpasswordService.generateCaptcha();
            vm.generateImage(vm.forgotpasswordData.captcha);
        };              

        vm.send = function()
        {
            vm.message = undefined;

            if (vm.forgotpasswordData.captcha === '') {
                vm.message = "Captcha field cannot be blank";
                return
            }
            if (vm.forgotpasswordData.identityNumber === '') {
                vm.message = "ID number field cannot be blank";
                return
            }

            var objID = { IDNo: vm.forgotpasswordData.identityNumber };            
            forgotpasswordService.send(objID)
                .then(function (response)
                {
                    vm.message = "password send to your email address";
                    console.log("succeded to send ID Number: " + vm.forgotpasswordData.identityNumber);
                }, function (reseponse) {
                    vm.message = "the ID Number does not exist";                    
                    console.log(JSON.stringify(reseponse));
                });            
        }

        vm.passwordResetRequest = function () {
            vm.resertPasswordMethod = 'email';
            forgotpasswordService.passwordResetRequest(vm.userAccount, vm.resertPasswordMethod).then(function (response) {
                $location.path('/passwordrecovery').replace();
            }, function (error) {
                console.log(JSON.stringify(error));
                vm.message = error.data.exceptionMessage;
            });
        };
    };

    angular.module('eRecruitmentApp').controller('forgotpasswordController', forgotpasswordController);
    forgotpasswordController.$inject = ['forgotpasswordService', 'WizardHandler', '$location'];

})();
