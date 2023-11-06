(function () {
    'use strict';
    var rankedScreeningController = function (screeningService, WizardHandler, $filter) {
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

            vm.applications = $filter('orderBy')(vm.allApplicationsBeforeSlice.applications, vm.sortBy);
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
            vm.sentEmailClicked = false;
            vm.vacancy = screeningService.getCurrentPost();
            vm.vacancy.maintuser = vm.searchString

            var d = new Date(vm.vacancy.openingDate);
            d.setHours(d.getHours(), d.getTimezoneOffset(), 0, 0);
            vm.vacancy.openingDate = d;

            d = new Date(vm.vacancy.closingDate);
            d.setHours(d.getHours(), d.getTimezoneOffset(), 0, 0);
            vm.vacancy.closingDate = d;

            screeningService.getAllApplications(vm.vacancy).then(function (response) {
                vm.applications = response.data;

                //for (var i = 0; i < vm.applications.length; i++) {

                //    try {

                //        var applicationObject = {
                //            firstName: undefined,
                //            surname: undefined,
                //            dateOfBirth: undefined,
                //            programmeDesc: undefined,
                //            referenceNo: undefined,
                //            score: undefined,
                //            average: undefined,
                //            hasAttcahments: false
                //        }

                //        applicationObject.firstName = vm.applications[i].profile.firstNames;
                //        applicationObject.surname = vm.applications[i].profile.surname;
                //        applicationObject.dateOfBirth = vm.applications[i].profile.dateOfBirth;
                //        applicationObject.programmeDesc = vm.applications[i].applicantApplication.applicationRequirement.programme.programmeDesc;
                //        applicationObject.referenceNo = vm.applications[i].applicantApplication.applicationRequirement.programme.referenceNo;
                //        applicationObject.score = vm.applications[i].score;
                //        applicationObject.average = vm.applications[i].applicantApplication.applicationRequirement.overallAverage;

                //        vm.applicationObjectArray.push(applicationObject);
                //    }
                //    catch (ex) {
                //        console.log(ex);
                //    }                    
                //}  

                vm.noOfApplications = response.data.length;
                vm.allApplicationsBeforeSlice = response.data;

                var appStatusToSearch = "1";
                //if (vm.applications.length > 0) {
                //    for (var i = 0; i < vm.applications.length; i++) {
                //        var currStatusId = vm.applications[i].applicantApplication.applicationStatusId;

                //        if (currStatusId == appStatusToSearch) {
                //            i = vm.applications.length; //force loop to break
                //            vm.vacancyscreened = false;                            
                //        }
                //    }
                //    if (vm.vacancy.statusId == 5 && vm.vacancyscreened) {
                //        console.log("the vacancy status must be updated to screened: ");
                //        vm.screeningComplete();
                //    }
                //}                

                vm.applicationObjectArray = [];
                setPager(1);

                if (response.data != null) { }
            }, function (error) {
                console.log(JSON.stringify(error));
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

            if (vm.userList.length == 0) {
                vm.userList.push(vm.selectedUser);
            }
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

        vm.getProgrammes = function () {

            vm.programme = {
                ProgrammeId: null,
                ProgrammeDesc: null
            }

            screeningService.getprogrammes().then(function (response) {
                vm.programmes = response.data;
            });
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
            var path = '/screeningList';
            screeningService.backToPosts(path);
        }
        vm.viewApplication = function (selectedApplication) {
            var path = '/verifyScreening'
            screeningService.setCurrentApplication(selectedApplication, path);
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
                var path = '/screeningList'
                screeningService.backToPosts(path);
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

        //vm.selectedPageSizes = vm.tableParams.settings().counts;
        //vm.availablePageSizes = [5, 10, 15, 20, 25, 30, 40, 50, 100];
        //vm.changePage = changePage;
        //vm.changePageSize = changePageSize;
        //vm.changePageSizes = changePageSizes;


        //vm.setPage = function (pageNo) {
        //    vm.currentPage = pageNo;
        //};

        //vm.pageChanged = function () {
        //    console.log('Page changed to: ' + vm.currentPage);
        //};

        //vm.setItemsPerPage = function (num) {
        //    vm.itemsPerPage = num;
        //    vm.currentPage = 1; //reset to first page
        //}


        //function changePage(nextPage) {
        //    vm.tableParams.page(nextPage);
        //}

        //function changePageSize(newSize) {
        //    vm.tableParams.count(newSize);
        //}

        //function changePageSizes(newSizes) {
        //    // ensure that the current page size is one of the options
        //    if (newSizes.indexOf(vm.tableParams.count()) === -1) {
        //        newSizes.push(vm.tableParams.count());
        //        newSizes.sort();
        //    }
        //    vm.tableParams.settings({ counts: newSizes });
        //}

    };
    angular.module('eRecruitmentApp').controller('rankedScreeningController', rankedScreeningController);
    rankedScreeningController.$inject = ['screeningService', 'WizardHandler', '$filter'];
})();