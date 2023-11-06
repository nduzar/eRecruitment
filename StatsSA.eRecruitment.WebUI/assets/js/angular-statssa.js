/**
 * An Angular module that gives you access to the core statssa functionality
 * @version v0.0.1 - 2017-04-16
 * @author Dumisane Kubayi
 * @license MIT License, http://www.treasury.gov.za/licenses/MIT
 */
(function (window, angular) {
    var isDefined = angular.isDefined,
      isUndefined = angular.isUndefined,
      isNumber = angular.isNumber,
      isObject = angular.isObject,
      isArray = angular.isArray,
      isString = angular.isString,
      extend = angular.extend,
      toJson = angular.toJson;

    angular.module('StatssaCoreModule', []).provider('statssaCoreService', function () {          

          var statssaCoreService = function ($rootScope, $window, $document, $parse, $timeout, $filter) {
              var self = this;

              // When Angular's $document is not available
              if (!$document) {
                  $document = document;
              } else if ($document[0]) {
                  $document = $document[0];
              }

              var getAge = function (birthDate) {
                  var today = new Date();
                  var age = today.getFullYear() - birthDate.getFullYear();
                  var m = today.getMonth() - birthDate.getMonth();
                  if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
                      age--;
                  }
                  return age;
              };

              var validateSAIdNumber = function (idNumber) {


                  // Let's convert undefined values to null to get the value consistent
                  if (isUndefined(idNumber)) {
                      return {
                          idNumber: undefined,
                          isValid: false,
                          message: 'ID number is undefined.'
                      };
                  }

                  var isnumber = !isNaN(parseFloat(idNumber)) && isFinite(idNumber);

                  if (!isnumber) {
                      return {
                          idNumber: idNumber,
                          isValid: false,
                          message: 'Only numeric characters are allowed.'
                      };
                  }

                  if (idNumber.length != 13) {
                      return {
                          idNumber: idNumber,
                          isValid: false,
                          message: 'ID number must be 13 characters long.'
                      };
                  }

                  var tempDate = new Date(idNumber.substring(0, 2), idNumber.substring(2, 4) - 1, idNumber.substring(4, 6));

                  var id_date = tempDate.getDate();
                  var id_month = tempDate.getMonth();
                  var id_year = tempDate.getFullYear();

                  // var fullDate = id_date + "/" + (parseInt(id_month) + parseInt(1)).toString() + "/" + id_year;
                  var dob = $filter('date')(tempDate, "dd/MM/yyyy");
                  if (!((tempDate.getYear() == idNumber.substring(0, 2)) && (id_month == idNumber.substring(2, 4) - 1) && (id_date == idNumber.substring(4, 6)))) {
                      return {
                          idNumber: idNumber,
                          isValid: false,
                          message: 'ID number does not appear to be authentic - date part not valid.'
                      };
                  }

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
                      return {
                          idNumber: idNumber,
                          isValid: false,
                          message: 'ID number does not appear to be authentic - check digit is not valid.'
                      };
                  }

                  var genderCode = idNumber.substring(6, 10);
                  var gender = parseInt(genderCode) < 5000 ? "Female" : "Male";

                  // get country ID for citzenship
                  var citzenship = parseInt(idNumber.substring(10, 11)) == 0 ? true : false;

                  var age = getAge(tempDate);

                  return {
                      idNumber: idNumber,
                      isValid: true,
                      gender: gender,
                      isSouthAfrican: citzenship,
                      dateOfBirth_text: dob,
                      dateOfBirth: tempDate,
                      age: age
                  };

              }

              return {
                  validateSAIdNumber: validateSAIdNumber,
                  getAgeFromDOB: getAge
              };
          };

          this.$get = ['$rootScope', '$window', '$document', '$parse', '$timeout', '$filter', statssaCoreService];
      });
})(window, window.angular);