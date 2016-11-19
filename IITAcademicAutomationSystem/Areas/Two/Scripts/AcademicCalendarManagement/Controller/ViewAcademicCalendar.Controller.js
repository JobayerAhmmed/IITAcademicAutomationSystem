(
    function () {
        angular.module("academicCalendarManagement_module").controller("viewAcademicCalendar_controller", ["$scope", "AcademicCalendar_Service", "Utility_Service", function ($scope, AcademicCalendarService, UtilityService) {

            $scope.selected = {
                fileFath:"/Areas/Two/AcademicFiles/AcademicCalendar/",
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



