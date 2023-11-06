(function () {
    'use strict';

    var emailChangedController = function (emailhangeService, WizardHandler, $location) {
        var vm = this;
        vm.message = "";

        vm.emailChangeData = {
            email: undefined,
            //email: undefined
        }

        vm.searchTerms = {
            email: undefined,
            captcha: undefined
        };

        vm.newEmailData = {
            email: undefined,
            otp: undefined
        }

        vm.userAccount = {};
        vm.message = undefined;
        vm.maskedCellphone = '';

        vm.searchAccount = function (searchTerms) {
            console.log(searchTerms);
            if (searchTerms.captcha != vm.emailChangeData.captcha) {
                vm.message = 'Capture valid email or Captcha';
                vm.invalidCaptcha = true;
                return;
            }

            emailhangeService.searchAccount(searchTerms.email).then(function (response) {
                vm.userAccount = response;
                vm.message = "";
                WizardHandler.wizard().next();
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
                    digits[i] = captcha.substring(i, i + 1);
                }
            }
        };
        vm.initialisePage = function () {
            vm.emailChangeData.captcha = emailhangeService.generateCaptcha();
            angular.element(document).ready(function () {
                vm.generateImage(vm.emailChangeData.captcha);
            });

        };

        vm.generateCaptcha = function () {
            vm.emailChangeData.captcha = emailhangeService.generateCaptcha();
            vm.generateImage(vm.emailChangeData.captcha);
        };

        vm.send = function () {
            vm.message = undefined;
            console.log(vm.userAccount, 'controller')
            emailhangeService.send(vm.userAccount)
                .then(function (response) {
                    //vm.message = "OTP send to your email address";
                    WizardHandler.wizard().next();
                    console.log("succeded to send Email: " + vm.userAccount.secondaryEmail);
                }, function (reseponse) {
                    vm.message = "the Email does not exist";
                    console.log(JSON.stringify(reseponse));
                });
        }

        vm.emailResetRequest = function () {
            emailhangeService.emailResetRequest(vm.userAccount, 'email').then(function (response) {
                console.log('sent', response);
            }, function (error) {
                console.log(JSON.stringify(error));
                vm.message = error.data.exceptionMessage;
            });
        };

        vm.changeEmailNow = function (data) {
            vm.message = undefined;
            emailhangeService.emailResetRequest(vm.userAccount, vm.newEmailData).then(function (response) {
                console.log('sent', response);
                WizardHandler.wizard().next();
            }, function (error) {
                console.log(JSON.stringify(error));
                vm.message = error.data.exceptionMessage;
            });

        }
    }
        angular.module('eRecruitmentApp').controller('emailChangedController', emailChangedController);
    emailChangedController.$inject = ['emailhangeService', 'WizardHandler', '$location'];

})();
