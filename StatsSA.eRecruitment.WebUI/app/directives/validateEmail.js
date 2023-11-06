(function () {
    'use strict';
    angular.module('eRecruitmentApp').directive('validateEmail', function () {
        var EMAIL_REGEXP = /^[_a-zA-Z0-9]+(\.[_a-zA-Z0-9]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,4})$/;

        return {
            require: '?ngModel',
            restrict: "A",
            link: function (scope, elm, attrs, ctrl) {
                // only apply the validator if ngModel is present and Angular has added the email validator
                //if (ctrl && ctrl.$validators.email) {

                    // this will overwrite the default Angular email validator      
                    ctrl.$validators.email = function (modelValue) {
                        return ctrl.$isEmpty(modelValue) || EMAIL_REGEXP.test(modelValue);
                    };
                //}
            }
        };
    });
})();