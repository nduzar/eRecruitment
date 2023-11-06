(function () {
    'use strict';
    angular.module('eRecruitmentApp').directive('validateCharacters', function () {
        var regExp = /^[a-z ,.'-]+$/;

        return {
            require: '?ngModel',
            restrict: "A",
            link: function (scope, elm, attrs, ctrl) {
                // only apply the validator if ngModel is present and Angular has added the email validator
                //if (ctrl && ctrl.$validators.email) {

                // this will overwrite the default Angular email validator
                ctrl.$validators.name = function (modelValue) {
                    return ctrl.$isEmpty(modelValue) || regExp.test(modelValue);
                };
                //}
            }
        };
    });
})();   