(function () {
    'use strict';

    angular.module('eRecruitmentApp').directive("validateSaIdNumber", function ($rootScope) {
        return {
            restrict: "A",
            require: "?ngModel",
            link: function (scope, element, attributes, ngModel) {

                ngModel.$validators.idnumber = function (modelValue)
                {
                    var idNumber = modelValue;
                    if (!idNumber) {
                        return;
                    }

                    var correct = true;
                    var isnumber = !isNaN(parseFloat(idNumber)) && isFinite(idNumber);

                    if (idNumber.length != 13 || !isnumber) {
                        correct = false;
                    }

                    var tempDate = new Date(idNumber.substring(0, 2), idNumber.substring(2, 4) - 1, idNumber.substring(4, 6));

                    var id_date = tempDate.getDate();
                    var id_month = tempDate.getMonth();
                    var id_year = tempDate.getFullYear();

                    var fullDate = id_date + "-" + (parseInt(id_month) + parseInt(1)).toString() + "-" + id_year;

                    if (!((tempDate.getYear() == idNumber.substring(0, 2)) && (id_month == idNumber.substring(2, 4) - 1) && (id_date == idNumber.substring(4, 6)))) {
                        //ID number does not appear to be authentic - date part not valid.
                        correct = false;
                    }

                    // get the gender
                    var genderCode = idNumber.substring(6, 10);
                    var gender = parseInt(genderCode) < 5000 ? "Female" : "Male";

                    // get country ID for citzenship
                    var citzenship = parseInt(idNumber.substring(10, 11)) == 0 ? true : false;                   

                    // apply Luhn formula for check-digits
                    var tempTotal = 0;
                    var checkSum = 0;
                    var multiplier = 1;
                    for (var i = 0; i < 13; ++i) {
                        tempTotal = parseInt(idNumber.charAt(i)) * multiplier;
                        if (tempTotal > 9) {
                            tempTotal = parseInt(tempTotal.toString().charAt(0)) + parseInt(tempTotal.toString().charAt(1));
                        }
                        checkSum = checkSum + tempTotal;
                        multiplier = (multiplier % 2 == 0) ? 1 : 2;
                    }
                    if ((checkSum % 10) != 0) {
                        //ID number does not appear to be authentic - check digit is not valid.
                        correct = false;
                    };

                    return correct;
                };
            }
        };
    });

})();