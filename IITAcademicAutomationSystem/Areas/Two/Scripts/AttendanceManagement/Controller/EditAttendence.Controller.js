(
    function(){
        angular.module("attendanceManagement_module").controller("editAttendence_controller", ["$scope", "Attendance_Service", "Utility_Service", function ($scope, AttendanceService, UtilityService) {
            var EditAttendance={
                attendances: []
            }

            $scope.selected={
                program:{
                },
                semester:{
                },
                batch:{
                },
                course:{
                },
                classNoAndDate:{
                }

            }

            $scope.selection={
                programs:[],
                semesters:[],
                courses:[],
                attendanceInfo:[],//stu id,name,class,roll,isPresent
                classNoAndDateInfo:{}
            }



            $scope.flag={
                ifMultipleAttendenceGiven:false
            }

            $scope.message = {
                content: "",
                color: ""
            }


            $scope.checkedOrUnchecked={

            }

            $scope.whenProgramIsSelected = function () {
                if ($scope.selected.program.id)
                    getSemesters();

               
                $scope.selected.semester= {
                };
                $scope.selected.batch= {
                };
                $scope.selected.course= {
                };
                $scope.selected.classNoAndDate= {
                };
               
                $scope.selection.courses= [];
                $scope.selection.attendanceInfo= [];//stu id,name,class,roll,isPresent
                $scope.selection.classNoAndDateInfo = {};
                
                $scope.flag.ifMultipleAttendenceGiven= false;
                
            }


            $scope.whenSemesterIsSelected = function () {
                if ($scope.selected.program.id && $scope.selected.semester.id) {
                    getBatch();
                    getCourses();
                }
               

               
                $scope.selected.course = {
                };
                $scope.selected.classNoAndDate = {
                };
                
                $scope.selection.attendanceInfo = [];//stu id,name,class,roll,isPresent
                $scope.selection.classNoAndDateInfo = {};

                $scope.flag.ifMultipleAttendenceGiven = false;
            }


            $scope.whenCourseIsSelected = function () {
                if ($scope.selected.program.id && $scope.selected.semester.id && $scope.selected.course.id) {
                    getClassNumberAndDate();
                }
                

                $scope.selected.classNoAndDate = {
                };
                $scope.selection.attendanceInfo = [];//stu id,name,class,roll,isPresent
                $scope.selection.classNoAndDateInfo = {};

                $scope.flag.ifMultipleAttendenceGiven = false;
            }

            $scope.whenClassIsSelected = function () {
                if ($scope.selected.program.id && $scope.selected.semester.id && $scope.selected.course.id && $scope.selected.classNoAndDate.classNo) {
                    getAttendance();
                }
               

                $scope.selection.attendanceInfo = [];

                $scope.flag.ifMultipleAttendenceGiven = false;
            }
           

            $scope.inputAttendanceIndividual=function (index) {
                $scope.selection.attendanceInfo.studentInfo[index].isPresent=!$scope.selection.attendanceInfo.studentInfo[index].isPresent;
            }


            function getTodayDate(){
                var today = new Date();
                var dd = today.getDate();
                var mm = today.getMonth()+1; //January is 0!
                var yyyy = today.getFullYear();

                if(dd<10) {
                    dd='0'+dd
                }

                if(mm<10) {
                    mm='0'+mm
                }
                return dd+'-'+mm+'-'+yyyy;
            }


            $scope.submitAttendence=function(){
                 processFinalObject();
                console.info("Data has been saved");
                console.log(EditAttendance);
                saveEditedAttendance(EditAttendance);
            }

            var processFinalObject = function () {
                for (var i = 0; i < $scope.selection.attendanceInfo.studentInfo.length; i++) {
                    EditAttendance.attendances[i] = {
                        attendanceId: $scope.selection.attendanceInfo.studentInfo[i].id,
                        isPresent: $scope.selection.attendanceInfo.studentInfo[i].isPresent
                    }
                }

            }

           

            var clearData = function () {
                var EditAttendance = {
                    attendances: []
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
                    classNoAndDate: {
                    }

                }

                $scope.selection = {
                    programs: $scope.selection.programs,
                    semesters: [],
                    courses: [],
                    attendanceInfo: [],//stu id,name,class,roll,isPresent
                    classNoAndDateInfo: {}
                }



                $scope.flag = {
                    ifMultipleAttendenceGiven: false
                }

                $scope.message = {
                    content: "",
                    color: ""
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

            var getClassNumberAndDate = function () {
                AttendanceService.getClassNumberAndDate($scope.selected.program.id, $scope.selected.semester.id, $scope.selected.batch.id, $scope.selected.course.id)
                   .then(
                          function (d) {
                              if (d.Status == "OK") {
                                  $scope.selection.classNoAndDateInfo = d.Data.classesNubesAndDates;
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

            var getAttendance = function () {
                AttendanceService.getAttendance($scope.selected.program.id, $scope.selected.semester.id, $scope.selected.batch.id, $scope.selected.course.id, $scope.selected.classNoAndDate.classNo)
                   .then(
                          function (d) {
                              if (d.Status == "OK") {
                                  $scope.selection.attendanceInfo.studentInfo = d.Data.attendances;
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
            var saveEditedAttendance = function (EditAttendance) {
                AttendanceService.saveEditedAttendance(EditAttendance)
                  .then(
                       function (d) {
                           if (d.Status == "OK") {
                               showNotification(d.Message, d.Status);
                           }
                           else if (d.Status == "ERROR") {
                               console.log(d.Message);
                               showNotification(d.Message, d.Status);
                           }
                       },
                        function (errResponse) {
                            showNotification('Error while Adding Subhead', 'ERROR');
                        });
                clearData();
            }

            var inatializeThePagfunction=function(){
                getPrograms();
            }


            inatializeThePagfunction();
        }])
    }()
)



