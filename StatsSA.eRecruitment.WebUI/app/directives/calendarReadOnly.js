
(function () {
    'use strict';
    angular.module('eRecruitmentApp').directive('calendarReadOnly', function () {

        return {
            restrict: 'EAC',
            link: function (scope, element, attrs) {
                document.querySelectorAll("#datePicker input")[0].setAttribute("readonly", "readonly");
                angular.element(".md-datepicker-button").each(function () {
                    var el = this;
                    var ip = angular.element(el).parent().find("input").bind('click', function (e) {
                        angular.element(el).click();
                    });
                    angular.element(this).css('visibility', 'hidden');
                });
            }
        };
    });
})();
