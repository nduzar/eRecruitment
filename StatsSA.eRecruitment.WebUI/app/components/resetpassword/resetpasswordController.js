(function () {
    'use strict';
    
    var resetpasswordController = function (resetpasswordService) {
        var vm = this;

        vm.resetpasswordData = {
            idNumber: undefined,
            tempPassword: undefined,
            newPassword: undefined
        }  

        vm.reset = function()
        {
            var objData = {
                IDNo: vm.resetpasswordData.idNumber,
                tempPass: vm.resetpasswordData.tempPassword,
                newPass: vm.resetpasswordData.newPassword
            };

            console.log("CONTROLLER values: " + objData.IDNo +"::"+objData.tempPass +"::"+objData.newPass);

            resetpasswordService.reset(objData)
                .then(function () {
                    console.log("succeded to reset password: " + objData.idNumber + "::" + objData.tempPass + "::" + objData.newPass);
                },
                function (reseponse) {
                    console.log("failed to reset password: " + objData.idNumber);
                });
        }
    };

    angular.module('eRecruitmentApp').controller('resetpasswordController', resetpasswordController);
    resetpasswordController.$inject = ['resetpasswordService'];

})();
