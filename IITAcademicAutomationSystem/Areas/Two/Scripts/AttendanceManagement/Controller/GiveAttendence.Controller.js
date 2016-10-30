(
    function(){
        angular.module("attendanceManagement_module").controller("giveAttendence_controller", ["$scope", "Attendance_Service", "Utility_Service", function ($scope, AttendanceService, UtilityService) {
            var AddAttendance={
                programId:"",
                semesterId:"",
                batchId:"",
                courseId:"",
                classNo:"",
                classDate:"",
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
                currentClass:{
                    date:getTodayDate(),
                    classNo:"",
                },

            }
            
            $scope.selection={
                programs:[],
                semesters:[],
                courses:[],
                attendanceInfo:[],//stu id,name,class,roll,isPresent
            }



            $scope.flag={
                ifMultipleAttendenceGiven: false,

            }

            $scope.message = {
                content: "",
                color: ""
            }

            $scope.whenProgramIsSelected=function() {
                getSemesters();
            }


            $scope.whenSemesterIsSelected=function(){
                getCourses();
                getBatch();
            }


            $scope.whenCourseIsSelected=function(){
                getClassNumber();
                getAllStudentOfACourse();
               
            }


            $scope.absentAllStudent=function () {
                absentAllStudent();
            }

            var absentAllStudent=function () {
                for(var i=0;i<$scope.selection.attendanceInfo.length;i++){
                    $scope.selection.attendanceInfo[i].isPresent=false;
                }
            }


            $scope.presentAllStudent=function () {
                presentAllStudent();
            }

            var presentAllStudent=function () {
                for(var i=0;i<$scope.selection.attendanceInfo.length;i++){
                    $scope.selection.attendanceInfo[i].isPresent = true;
                }
            }

            $scope.inputAttendanceIndividual=function (index) {
                $scope.selection.attendanceInfo[index].isPresent = !$scope.selection.attendanceInfo[index].isPresent;
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
                console.log(AddAttendance);
                saveAttendance(AddAttendance);
                
            }

            var processFinalObject = function () {
                AddAttendance.programId = $scope.selected.program.id;
                AddAttendance.semesterId = $scope.selected.semester.id;
                AddAttendance.batchId = $scope.selected.batch.id;
                AddAttendance.courseId = $scope.selected.course.id;
                AddAttendance.classNo = $scope.selected.currentClass.classNo;
                AddAttendance.classDate = $scope.selected.currentClass.date;
                AddAttendance.attendances = processAttendanceWithStudentId();
                       
            }

            var processAttendanceWithStudentId = function () {
                var tempArray = [];
                for (var i = 0; i < $scope.selection.attendanceInfo.length; i++) {
                    tempArray[i] = {
                        studentId: $scope.selection.attendanceInfo[i].id,
                        isPresent: $scope.selection.attendanceInfo[i].isPresent
                    }
                }

                return tempArray;
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
                    programs:$scope.selection.programs,
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

            var getClassNumber = function () {
                AttendanceService.getLastClassNumber($scope.selected.program.id, $scope.selected.semester.id,$scope.selected.batch.id, $scope.selected.course.id)
                   .then(
                          function (d) {
                              if (d.Status == "OK") {
                                  $scope.selected.currentClass.classNo = d.Data+1;
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

            var getAllStudentOfACourse = function () {
                UtilityService.getAllStudentOfACourse($scope.selected.program.id, $scope.selected.semester.id, $scope.selected.batch.id, $scope.selected.course.id)
                    .then(
			               function (d) {
			                   if (d.Status == "OK") {
			                       console.log(d);
			                       $scope.selection.attendanceInfo = d.Data.students;
			                       absentAllStudent();
			                   }
			                   else if (d.Status == "ERROR") {
			                       showNotification(d.Message, d.Status);
			                   }
			               },
    				        function (errResponse) {
    				            showNotification('Error While Fetching Sub Heads','ERROR');
    				        }
	                );
            }

            var saveAttendance = function (Attendance) {
                AttendanceService.saveAttendance(Attendance)
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
			                showNotification('Error while Adding Subhead','ERROR');
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



