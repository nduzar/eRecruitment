(function () {
    'use strict';
    angular.module('eRecruitmentApp').filter('filterByArray', function ($filter) {
        return function(list, arrayFilter, element){
            if(arrayFilter){
                return $filter("filter")(list, function (listItem) {
                    var found = false;
                    angular.forEach(arrayFilter, function (langArray, key) {
                        if (langArray.language[element] == listItem[element]) {
                            found = true;
                        }
                    });
                    return !found;
                });
            }
        };
    });
})();