(
    function(){

        angular.module("attendanceManagement_module").controller("viewAttendenceIndividual_controller", ["$scope", "Attendance_Service", function ($scope, AttendanceService) {
            $scope.selection={
                attendanceInfo:{}
            }

           var getAttendanceOfAStudentOfAllCourses=function () {
               getAttendanceIndividual();
           }


            var inatializeAllStudentPresencePercentage=function () {
                var attendanceInfoLength = $scope.selection.attendanceInfo.attendanceOfAllCourses.length;                
                for (var i = 0; i < attendanceInfoLength; i++) {
                    if ($scope.selection.attendanceInfo.attendanceOfAllCourses[i].attendances == undefined || $scope.selection.attendanceInfo.attendanceOfAllCourses[i].attendances == null)
                        continue;
                    $scope.selection.attendanceInfo.attendanceOfAllCourses[i].presencePercentage=getPresencePercentage($scope.selection.attendanceInfo.attendanceOfAllCourses[i].attendances)

                }
            }

            var getPresencePercentage = function (array) {
                if (array == undefined)
                    return;
                present=0;
                arrayLength=array.length
                for(var i=0;i<arrayLength;i++){
                    if(array[i].isPresent==true)
                        present++;
                }

                var percentage=(present/arrayLength)*100.00;

                return percentage.toFixed(2);
            }



           var getAttendanceIndividual = function () {
               AttendanceService.getAttendanceIndividual()
                  .then(
                         function (d) {
                             if (d.Status == "OK") {
                                 $scope.selection.attendanceInfo = d.Data;
                                 inatializeAllStudentPresencePercentage();
                             }
                             else if (d.Status == "ERROR") {
                                 showNotification(d.Message, d.Status);
                             }
                         },
                          function (errResponse) {
                              showNotification('Error While Fetching Courses', 'ERROR');
                          }
                  );
           }

            var inatializeThePagfunction=function(){
                getAttendanceOfAStudentOfAllCourses();
            }
            inatializeThePagfunction();
        }])
    }()
)


