(function () {
    'use strict';

    var reportsController = function (reportsService, WizardHandler, uiGridConstants, $filter) {
        var vm = this;

        vm.filter = {
            rptType: ''
        }
       
        vm.reports = [
            { type_id: 1, type_desc: "Application History per Candidate" },
            { type_id: 2, type_desc: "List of Candidates per Post" },
            { type_id: 3, type_desc: "No of Candidates per Advert" },
            { type_id: 4, type_desc: "Screening Report" }];

        vm.initialisePage = function () {
            reportsService.getReportServerURL().then(function (url) {
                vm.ReportServerUrl = url.data;
            });
        };

        vm.ViewReport = function () {
            if (vm.filter.rptType == '' || vm.filter.rptType == null) {
                toastr.warning('Please select report');
                return;
            }

            vm.CurrentReport = "";
            var url = vm.ReportServerUrl;
            switch (vm.filter.rptType) {

                case "1":
                    vm.CurrentReport = vm.ReportServerUrl + "?%2feRecruitmentReports%2frptApplicationHistory";
                    window.open(vm.CurrentReport, '_blank');

                    break;

                case "2":
                    vm.CurrentReport = vm.ReportServerUrl + "?%2feRecruitmentReports%2frptListCandidatePerPost";
                    window.open(vm.CurrentReport, '_blank');

                    break;

                case "3":
                    vm.CurrentReport = vm.ReportServerUrl + "?%2feRecruitmentReports%2frptNoCandidatesPerAd";
                    window.open(vm.CurrentReport, '_blank');

                    break;

                case "4":
                    vm.CurrentReport = vm.ReportServerUrl + "?%2feRecruitmentReports%2frptScreening";
                    window.open(vm.CurrentReport, '_blank');

                    break;
            }
        }


    };
    angular.module('eRecruitmentApp').controller('reportsController', reportsController);
    reportsController.$inject = ['reportsService', 'WizardHandler', 'uiGridConstants', '$filter'];
})();