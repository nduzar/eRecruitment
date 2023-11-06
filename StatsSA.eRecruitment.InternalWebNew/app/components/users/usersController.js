(function () {
    'use strict';

    var usersController = function (usersService, WizardHandler, $filter) {
        var vm = this;
        vm.editMode = true;
        vm.isCollapsed = true;
        vm.users = [];
        vm.sortBy = null;
        vm.viewedUser = [];
        vm.allRoles = [];
        vm.availableRoles = [];
        vm.setSorting = function (sortColumn) {
            if (vm.sortBy == sortColumn) {
                vm.sortBy = '-' + sortColumn
            } else {
                vm.sortBy = sortColumn;
            }
            console.log("SORTING: " + sortColumn);
        };
        vm.searchFilter = undefined;
        vm.searchString = null;
        vm.setFilter = function () {
            vm.searchFilter = vm.searchString;
            console.log(vm.searchString);
        };
        vm.clearFilter = function () {
            vm.searchFilter = undefined;
            vm.searchString = null;
        };
        vm.initialisePage = function () {
            usersService.getAllRoles().then(function (response) {
                vm.allRoles = response.data;
                usersService.getUsersList().then(function (response) {
                    vm.users = response.data;
                    
                    if (response.data != null) { }
                }, function (error) {
                    console.log(JSON.stringify(error));
                });
            }, function (error) {
                console.log(JSON.stringify(error));
            });
            
        };
        vm.thisUsersRoles = [];
        vm.viewUser = function (selectedUser) {
            vm.error = {
                thrown: false,
                header: "",
                message: ""
            };
            vm.editMode = true;
            vm.viewedUser = angular.copy(selectedUser);
            vm.viewedUser.searchName = selectedUser.hrUserName;
            vm.thisUsersRoles = [];
            vm.availableRoles = angular.copy(vm.allRoles);
            angular.forEach(vm.viewedUser.hrUserRoles, function (value, key) {
                var role = $filter('filter')(vm.availableRoles, { hrUserRoleId: value.hrRoleId })[0];
                var indexOfRole = vm.availableRoles.indexOf(role);
                if (indexOfRole >= 0) {
                        console.log("FOUND ROLE:" + JSON.stringify(role) + " at " + indexOfRole);
                        vm.availableRoles.splice(indexOfRole, 1);
                        vm.thisUsersRoles.push(role);
                    }
                    else {
                        console.log("DID NOT FIND ROLE:" + JSON.stringify(role) + " " + indexOfRole);
                    }
                });
            
            WizardHandler.wizard().next();
        };
        vm.addUser = function () {
            vm.error = {
                thrown: false,
                header: "",
                message: ""
            };
            vm.editMode = false;
            vm.viewedUser.searchName = null;
            vm.viewedUser = [];
            vm.thisUsersRoles = [];
            vm.availableRoles = angular.copy(vm.allRoles);
            WizardHandler.wizard().next();
        };
        vm.searchList = [];
        vm.searchUser = function (searchString) {
            vm.isCollapsed = true;
            usersService.searchUser(searchString).then(function (response) {
                console.log(JSON.stringify(response.data));
                vm.isCollapsed = false;
                vm.searchList = response.data;
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        };
        vm.searchSelected = function (selectedUser) {
            vm.viewedUser.searchName = null;
            vm.viewedUser.hrUserName = selectedUser.samAccountname;
            vm.viewedUser.email = selectedUser.emailAddress;
            vm.viewedUser.fullName = selectedUser.fullNames;
            
            vm.isCollapsed = true;
        };
        vm.userIsActive = function(status){
            vm.viewedUser.isActive = status;
        }
        vm.backToList = function () {
            vm.viewedUser = [];
            WizardHandler.wizard().previous();
        }
        vm.addRole = function (role, index) {
            
            vm.thisUsersRoles.push(role);
            var indexOfRole = vm.availableRoles.indexOf(role);
            vm.availableRoles.splice(indexOfRole, 1);
        };
        vm.removeRole = function (role, index) {
            
            vm.availableRoles.push(role);
            var indexOfRole = vm.thisUsersRoles.indexOf(role);
            vm.thisUsersRoles.splice(indexOfRole, 1);
        };
        vm.error = {
            thrown: false,
            header: "",
            message: ""
        }
        vm.dismiss = function () {
            vm.error = {
                thrown: false,
                header: "",
                message: ""
            };
        };
        vm.updateUser = function (user) {
            if (vm.viewedUser.hrUserName == null || vm.viewedUser.hrUserName == "") {
                vm.error = {
                    thrown: true,
                    header: "Validation error",
                    message: "Please select a network user."
                };
                return false;
            }
            if (vm.thisUsersRoles.length == 0) {
                vm.error = {
                    thrown: true,
                    header: "Validation error",
                    message: "Please select user role(s)."
                };
                return false;
            }
            var editedRoles = [];
            angular.forEach(vm.thisUsersRoles, function (value, key) {
                var tempRole = {
                    hrRole: {},
                    hrUser: null,
                    hrUserId: user.hrUserId,
                    hrRoleId: value.hrUserRoleId
                };
                editedRoles.push(tempRole);
            });
            user.hrUserRoles = editedRoles;
            usersService.updateUser(user).then(function (response) {
                if (response.data.status) {
                    vm.viewedUser = [];
                    vm.thisUsersRoles = [];
                    WizardHandler.wizard().previous();
                    vm.initialisePage();
                }
                else {
                    vm.error = {
                        thrown: true,
                        header: "Error updating user",
                        message: response.data.statusMessage
                    };
                    return false;
                }
                
            }, function (error) {
                console.log(JSON.stringify(error));
            });
        }
        vm.insertUser = function (user) {
            if (vm.viewedUser.hrUserName == null || vm.viewedUser.hrUserName == "") {
                vm.error = {
                    thrown: true,
                    header: "Validation error",
                    message: "Please select a network user."
                };
                return false;
            }
            var userRoles = [];
            if (vm.thisUsersRoles.length == 0) {
                vm.error = {
                    thrown: true,
                    header: "Validation error",
                    message: "Please select user role(s)."
                };
            } else {
                angular.forEach(vm.thisUsersRoles, function (value, key) {
                    var tempRole = {
                        hrRole: {},
                        hrUser: null,
                        hrUserId: null,
                        hrRoleId: value.hrUserRoleId
                    };
                    userRoles.push(tempRole);
                });
                var newUser = {
                    hrUserName: vm.viewedUser.hrUserName,
                    hrUserRoles: userRoles,
                    fullName: undefined,
                    email: undefined,
                    createdDate: undefined,
                    createdBy: undefined,
                    hrUserRoleId: 1,
                    lastLogin: undefined,
                    isActive: vm.viewedUser.isActive,
                    maintUser: undefined,
                    maintDate: undefined
                };
                usersService.insertUser(newUser).then(function (response) {
                    if (response.data.status) {
                        vm.viewedUser = [];
                        vm.thisUsersRoles = [];
                        WizardHandler.wizard().previous();
                        vm.initialisePage();
                    }
                    else {
                        vm.error = {
                            thrown: true,
                            header: "Error adding user",
                            message: response.data.statusMessage
                        };
                        return false;
                    }
                }, function (error) {
                    console.log(JSON.stringify(error));
                });
            }
            
        };
    };

    angular.module('eRecruitmentApp').controller('usersController', usersController);
    usersController.$inject = ['usersService', 'WizardHandler', '$filter'];
})();