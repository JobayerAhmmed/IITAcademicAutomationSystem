(
    function(){
        angular.module("examinationManagement_module").controller("viewGivenMarks_controller", ["$scope", "Result_Service", "Utility_Service", function ($scope, ResultService, UtilityService) {
            $scope.name="Ishmnam";



            $scope.selected={
                program:{
                },
                semester:{
                },
                batch:{
                },
                course:{
                },

            }

            $scope.selection={
                programs:[],
                semesters:[],
                courses:[],
                marksInfo:[]
            }
            $scope.flag= {
                
            }

            $scope.message = {
                content: "",
                color: ""
            }

            $scope.whenProgramIsSelected = function () {
                $scope.selected = {
                    program: $scope.selected.program,
                    semester: {
                    },
                    batch: {
                    },
                    course: {
                    },

                }

                $scope.selection = {
                    programs: $scope.selection.programs,
                    semesters: [],
                    courses: [],
                    marksInfo: []
                }
                $scope.flag = {

                }
                getSemesters();
                
            }

            

            $scope.whenSemesterIsSelected = function () {
                $scope.selected = {
                    program: $scope.selected.program,
                    semester: $scope.selected.semester,
                    batch: $scope.selected.batch,
                    course: {
                    },

                }

                $scope.selection = {
                    programs: $scope.selection.programs,
                    semesters: $scope.selection.semesters,
                    courses: [],
                    marksInfo: []
                }
                $scope.flag = {
                }
                if ($scope.selected.program.id && $scope.selected.semester.id) {
                    getBatch();
                    getCourses();
                }
                
               
            }

           
            var checkIfAllMarksOfAllSubHeadsAreGiven = function () {
                var count = 0;
                for (var i = 0; i < $scope.selection.courseMarksInfo.headMarksInfo.length; i++) {
                    if ($scope.selection.courseMarksInfo.headMarksInfo[i].isMarksGiven === false) {
                        $scope.flag.ifMarksOfAllHeadsAreGiven = false;
                        break;
                    }
                    else {
                        count++;
                    }
                }
                if (count == $scope.selection.courseMarksInfo.headMarksInfo.length) {
                    $scope.flag.ifMarksOfAllHeadsAreGiven = true;
                }
            }

            $scope.whenFinalSubmissionBauttonIsClicked = function () {
                var r = confirm("Sure ?..After confirming you can not enter/edit any marks in this course");
                if (r == true) {
                    finallySubmitMarks();
                }
            }

            $scope.whenCourseIsSelected = function () {
                $scope.selected = {
                    program: $scope.selected.program,
                    semester: $scope.selected.semester,
                    batch: $scope.selected.batch,
                    course: $scope.selected.course,

                }

                $scope.selection = {
                    programs: $scope.selection.programs,
                    semesters: $scope.selection.semesters,
                    courses: $scope.selection.courses,
                    marksInfo: []
                }
                $scope.flag = {

                }
                getGivenMarksOfAllStudentOfACourse();
                getCourseMarksInfo();
            }

            var showNotification = function (message, status) {
                $scope.message.content = message;

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
                        getCourseMarksInfo();
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
                               showNotification('Error While Fetching Programs','ERROR');
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
                               showNotification('Error While Fetching Semesters','ERROR');
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
                               showNotification('Error While Fetching Batch','ERROR');
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
                               showNotification('Error While Fetching Courses','ERROR');
                           }
                   );
            }

            var getGivenMarksOfAllStudentOfACourse = function () {
                ResultService.getGivenMarksOfAllStudentOfACourse_t($scope.selected.program.id, $scope.selected.semester.id, $scope.selected.batch.id, $scope.selected.course.id)
                   .then(
                          function (d) {
                              console.log(d);
                              if (d.Status == "OK") {
                                  $scope.selection.marksInfo = d.Data;
                              }
                              else if (d.Status == "ERROR") {
                                  showNotification(d.Message, d.Status);
                              }
                          },
                           function (errResponse) {
                               showNotification('Error While Fetching Courses','ERROR');
                           }
                   );
            }

            var getCourseMarksInfo = function () {
                ResultService.getCourseMarksInfo($scope.selected.program.id, $scope.selected.semester.id, $scope.selected.batch.id, $scope.selected.course.id)
                   .then(
                          function (d) {
                              if (d.Status == "OK") {
                                  $scope.selection.courseMarksInfo = d.Data;
                                  console.log(d.Data);
                                  checkIfAllMarksOfAllSubHeadsAreGiven();
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

            var finallySubmitMarks = function () {
                ResultService.finallySubmitOfACourse($scope.selected.program.id, $scope.selected.semester.id, $scope.selected.batch.id, $scope.selected.course.id)
	              .then(
                       function (d) {
                           if (d.Status == "OK") {
                               showNotification(d.Message, d.Status);
                               $scope.flag = {};
                           }
                           else if (d.Status == "ERROR") {
                               showNotification(d.Message, d.Status);
                           }
                       },
			            function (errResponse) {
			                showNotification('Error while Adding Subhead', 'ERROR');
			            });
            }


            var inatializeThePagfunction=function(){
                getPrograms();
            }


            inatializeThePagfunction();
        }])

    }()

)





