(function () {
    'use strict';

    var hrShortlistedApplicationsController = function (screeningService, WizardHandler, $filter) {
        var vm = this;
        vm.canEdit = false;
        vm.vacancyscreened = true;
        vm.totalItems = null;
        vm.currentPage = 1;
        vm.itemsPerPage = 10;
        vm.programmes = null;
        vm.programme = null;
        vm.pager = {};
        vm.allApplications = [];
        vm.noOfApplications = 0;
        vm.allApplicationsBeforeSlice = [];
        vm.hiringManagerEmail = "";
        vm.allUsers = [];
        vm.selectedUser = {};
        vm.userList = [];
        vm.allSelectedUsers = [];

        vm.setSorting = function (sortColumn) {
            if (vm.sortBy == sortColumn) {
                vm.sortBy = '-' + sortColumn;
            } else {
                vm.sortBy = sortColumn;
            }

            vm.applications = $filter('orderBy')(vm.allApplicationsBeforeSlice, vm.sortBy);
            setPager(1);

            console.log("SORTING: " + sortColumn);
        };
        vm.editMode = function (state) {
            vm.canEdit = state;
        };
        vm.vacancy = undefined;
        vm.applications = [];
        vm.allUsers = [];

        vm.setPager = setPager;

        vm.initialisePage = function () {

            //vm.vacancy = screeningService.getCurrentPost();
            //vm.vacancy.maintuser = vm.searchString

            //var d = new Date(vm.vacancy.openingDate);
            //d.setHours(d.getHours(), d.getTimezoneOffset(), 0, 0);
            //vm.vacancy.openingDate = d;

            //d = new Date(vm.vacancy.closingDate);
            //d.setHours(d.getHours(), d.getTimezoneOffset(), 0, 0);
            //vm.vacancy.closingDate = d;

            screeningService.getApplicationsHiringManager().then(function (response) {
            vm.applications = response.data;

            vm.noOfApplications = response.data.length;
            vm.allApplicationsBeforeSlice = response.data;

            var appStatusToSearch = "1";                             

            vm.applicationObjectArray = [];
            setPager(1);

            //if (response.data != null) { }
            //}, function (error) {
            //    console.log(JSON.stringify(error));
            });
        };

        vm.toggleScreen = function () {
            vm.sentEmailClicked = true;
            screeningService.getAllRoles().then(function (response) {
                vm.allUsers = response.data;
                console.log(vm.allUsers);
            });
        }

        vm.addUser = function () {

            vm.userList.push(vm.selectedUser);
            vm.userList = removeDuplicates(vm.userList, "hrUserId");
        }

        vm.sentEmail = function () {

            var hiringmanagerVacancyAccess = {
                users: vm.userList,
                vacancyId: vm.vacancy.vacancyId
            }

            screeningService.sentEmail(hiringmanagerVacancyAccess).then(function (response) {
                console.log(response);
            });
        }

        vm.remove = function (item, items) {
            // console.log(items);            
            items.splice(items.indexOf(item), 1);
            items.forEach(function (elem) {
                elem.id = items.indexOf(elem) + 1;
            });
        };

        function removeDuplicates(originalArray, prop) {
            var newArray = [];
            var lookupObject = {};

            for (var i in originalArray) {
                lookupObject[originalArray[i][prop]] = originalArray[i];
            }

            for (i in lookupObject) {
                newArray.push(lookupObject[i]);
            }
            return newArray;
        }

        vm.backToList = function () {
            vm.sentEmailClicked = false;
        }

        vm.searchFilter = undefined;
        vm.searchString = null;
        vm.setFilter = function () {

            vm.searchFilter = vm.searchString;
            vm.applications = $filter('filter')(vm.allApplicationsBeforeSlice, vm.searchFilter);

            setPager(1);

        };

        vm.clearFilter = function () {
            vm.searchFilter = undefined;
            vm.searchString = null;
        };

        function setPager(page) {

            if (page < 1 || page > vm.pager.totalPages) {
                return;
            }

            vm.pager = vm.getPager(vm.applications.length, page, vm.itemsPerPage);
            vm.allApplications = vm.applications.slice(vm.pager.startIndex, vm.pager.endIndex + 1);
        }

        vm.backToPosts = function () {
            screeningService.backToPosts('/hrShortlistedApplications');
        }

        vm.viewApplication = function (selectedApplication) {
            screeningService.setCurrentApplication(selectedApplication, '/hiringManagerShortlisting');

        };
        vm.screeningComplete = function () {
            var d = new Date(vm.vacancy.openingDate);
            d.setHours(0, -d.getTimezoneOffset(), 0, 0);
            vm.vacancy.openingDate = d;

            d = new Date(vm.vacancy.closingDate);
            d.setHours(23, -d.getTimezoneOffset(), 0, 0);
            d.setMinutes(59, 59, 0);
            vm.vacancy.closingDate = d;
            screeningService.screeningComplete(vm.vacancy).then(function (response) {
                screeningService.backToPosts();
                if (response.data != null) { }
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        }
        vm.back = function () {
            WizardHandler.wizard().previous();
        };
        vm.getKey = function (key) {
            // 10 is the radix, which is the base (assumed to be base 10 here) 
            return parseInt(key, 10);
        }


        vm.getPager = function (totalItems, currentPage, pageSize) {
            // default to first page
            currentPage = currentPage || 1;

            // default page size is 10
            pageSize = pageSize || 10;

            // calculate total pages
            var totalPages = Math.ceil(totalItems / pageSize);

            var startPage, endPage;
            if (totalPages <= 10) {
                // less than 10 total pages so show all
                startPage = 1;
                endPage = totalPages;
            } else {
                // more than 10 total pages so calculate start and end pages
                if (currentPage <= 6) {
                    startPage = 1;
                    endPage = 10;
                } else if (currentPage + 4 >= totalPages) {
                    startPage = totalPages - 9;
                    endPage = totalPages;
                } else {
                    startPage = currentPage - 5;
                    endPage = currentPage + 4;
                }
            }

            // calculate start and end item indexes
            var startIndex = (currentPage - 1) * pageSize;
            var endIndex = Math.min(startIndex + pageSize - 1, totalItems - 1);

            // create an array of pages to ng-repeat in the pager control
            var pages = _.range(startPage, endPage + 1);

            // return object with all pager properties required by the view
            return {
                totalItems: totalItems,
                currentPage: currentPage,
                pageSize: pageSize,
                totalPages: totalPages,
                startPage: startPage,
                endPage: endPage,
                startIndex: startIndex,
                endIndex: endIndex,
                pages: pages
            };
        }

    };
    angular.module('eRecruitmentApp').controller('hrShortlistedApplicationsController', hrShortlistedApplicationsController);
    hrShortlistedApplicationsController.$inject = ['screeningService', 'WizardHandler', '$filter'];
})();