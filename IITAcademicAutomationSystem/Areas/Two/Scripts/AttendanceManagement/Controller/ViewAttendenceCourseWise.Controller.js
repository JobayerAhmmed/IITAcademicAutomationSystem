(
    function(){

        angular.module("attendanceManagement_module").controller("viewAttendenceCourseWise_controller", ["$scope", "Attendance_Service", "Utility_Service", function ($scope, AttendanceService, UtilityService) {

            $scope.selected={
                program:{
                },
                semester:{
                },
                batch:{
                },
                course:{
                }
            }


            $scope.selection={
                programs:[],
                semesters:[],
                courses:[],
                attendanceInfo:{}
            }

            $scope.message = {
                content: "",
                color: ""
            }

            $scope.whenProgramIsSelected = function () {
                if($scope.selected.program.id){
                    getSemesters();
                }
               

                $scope.selected.semester = {
                };
                $scope.selected.batch = {
                };
                $scope.selected.course = {
                };
               

                $scope.selection.courses = [];
                $scope.selection.attendanceInfo = {};
            }


            $scope.whenSemesterIsSelected = function () {
                if ($scope.selected.program.id && $scope.selected.semester.id) {
                    getBatch();
                    getCourses();
                }
                

                $scope.selected.course = {
                };
                
                $scope.selection.attendanceInfo = {};
            }

            $scope.whenCourseIsSelected = function () {
                if ($scope.selected.program.id && $scope.selected.semester.id && $scope.selected.course.id) {
                    getAttendanceCourseWise();
                }
            }

            
            var inatializeAllStudentPresencePercentage=function () {
                var attendanceInfoLength=$scope.selection.attendanceInfo.attendanceHistoryAll.length;
                for(var i=0;i<attendanceInfoLength;i++){
                    $scope.selection.attendanceInfo.attendanceHistoryAll[i].presencePercentage=getPresencePercentage($scope.selection.attendanceInfo.attendanceHistoryAll[i].attendanceHistoryIndividual)
                    
                }
            }

            var getPresencePercentage=function (array) {
                present=0;
                arrayLength=array.length
                for(var i=0;i<arrayLength;i++){
                    if(array[i].isPresent==true)
                        present++;
                }

                var percentage=(present/arrayLength)*100.00;

                return percentage.toFixed(2);
            }

            

            var clearData = function () {
                var AddAttendance = {
                    programId: "",
                    semesterId: "",
                    batchId: "",
                    courseId: "",
                    classNo: "",
                    classDate: "",
                    attendance: []
                }

                $scope.selected = {
                    program: {
                    },
                    semester: {
                    },
                    batch: {
                    },
                    course: {
                    },
                    currentClass: {
                        date: getTodayDate(),
                        classNo: "",
                    },

                }

                $scope.selection = {
                    programs: $scope.selection.programs,
                    semesters: [],
                    courses: [],
                    attendanceInfo: [],//stu id,name,class,roll,isPresent
                }



                $scope.flag = {
                    ifMultipleAttendenceGiven: false,
                }
            }


            var showNotification = function (notification, status) {

                $scope.message.content = notification;

                if (status == "OK") {
                    $scope.message.color = "alert alert-success";
                }
                else if (status == "ERROR") {
                    $scope.message.color = "alert alert-danger";
                }
                setTimeout(function () {
                    $scope.$apply(function () {
                        $scope.message = {
                            content: "",
                            color: ""
                        }
                    });
                }, 3000);
            }

            var getPrograms = function () {
                UtilityService.getPrograms()
                   .then(
                          function (d) {
                              if (d.Status == "OK") {
                                  $scope.selection.programs = d.Data.programs;
                              }
                              else if (d.Status == "ERROR") {
                                  showNotification(d.Message, d.Status);
                              }
                          },
                           function (errResponse) {
                               showNotification('Error While Fetching Programs', 'ERROR');
                           }
                   );
            }

            var getSemesters = function () {
                UtilityService.getSemesters($scope.selected.program.id)
                   .then(
                          function (d) {
                              if (d.Status == "OK") {
                                  $scope.selection.semesters = d.Data.semesters;
                              }
                              else if (d.Status == "ERROR") {
                                  showNotification(d.Message, d.Status);
                              }
                          },
                           function (errResponse) {
                               showNotification('Error While Fetching Semesters', 'ERROR');
                           }
                   );

            }

            var getBatch = function () {
                UtilityService.getBatch($scope.selected.program.id, $scope.selected.semester.id)
                   .then(
                          function (d) {
                              if (d.Status == "OK") {
                                  $scope.selected.batch = d.Data;
                              }
                              else if (d.Status == "ERROR") {
                                  showNotification(d.Message, d.Status);
                              }
                          },
                           function (errResponse) {
                               showNotification('Error While Fetching Batch', 'ERROR');
                           }
                   );
            }

            var getCourses = function () {
                UtilityService.getCourses($scope.selected.program.id, $scope.selected.semester.id)
                   .then(
                          function (d) {
                              if (d.Status == "OK") {
                                  $scope.selection.courses = d.Data.courses;
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

            var getAttendanceCourseWise = function () {
                AttendanceService.getAttendanceCourseWise($scope.selected.program.id, $scope.selected.semester.id, $scope.selected.batch.id, $scope.selected.course.id)
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
                getPrograms();
            }


            inatializeThePagfunction();


        }])



    }()
)

