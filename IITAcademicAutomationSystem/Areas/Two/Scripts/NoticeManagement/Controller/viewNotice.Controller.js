(
    function () {
        angular.module("noticeManagement_module").controller("viewNotice_controller", ["$scope", "Notice_Service", "Utility_Service", function ($scope, NoticeService, UtilityService) {

            $scope.selected = {
                fileFath:"/Areas/Two/AcademicFiles/Notice/",
            }


            function getParameterByName(name, url) {
                if (!url) {
                    url = window.location.href;
                }
                name = name.replace(/[\[\]]/g, "\\$&");
                var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                    results = regex.exec(url);
                if (!results) return null;
                if (!results[2]) return '';
                return decodeURIComponent(results[2].replace(/\+/g, " "));
            }

            

            var inatializeThePage = function () {
                $scope.selected.fileFath = $scope.selected.fileFath+getParameterByName('filePath');
            }


            inatializeThePage();
        }])
    }()
)



