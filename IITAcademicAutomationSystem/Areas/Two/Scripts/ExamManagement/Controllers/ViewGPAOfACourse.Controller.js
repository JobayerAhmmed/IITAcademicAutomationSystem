(
    function () {
        angular.module("examinationManagement_module").controller("viewGPAOfACourse_controller", ["$scope", "Result_Service", "Utility_Service", "$window", function ($scope, ResultService, UtilityService, $window) {
            $scope.name = "Ishmnam";
            $scope.selected = {
                program: {
                },
                semester: {
                },
                batch: {
                },
                course: {
                }
            }

            $scope.selection = {
                programs: [],
                semesters: [],
                courses: [],
                GPAOfCourse:{}
            }
            $scope.flag = {}

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
                    GPAOfCourse: {}
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
                    }
                }
                $scope.selection = {
                    programs: $scope.selection.programs,
                    semesters: $scope.selection.semesters,
                    courses: [],
                    GPAOfCourse: {}
                }
                $scope.flag = {
                }
                if ($scope.selected.program.id && $scope.selected.semester.id) {
                    getBatch();
                    getCourses();
                }
            }

            $scope.whenCourseIsSelected = function () {
                $scope.selected = {
                    program: $scope.selected.program,
                    semester: $scope.selected.semester,
                    batch: $scope.selected.batch,
                    course: $scope.selected.course
                }

                $scope.selection = {
                    programs: $scope.selection.programs,
                    semesters: $scope.selection.semesters,
                    courses: $scope.selection.courses,
                    GPAOfCourse: {}
                }
                $scope.flag = {}
                getIdAndHeadsOfADistribution();
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
            $scope.printIt = function () {
                var table = document.getElementById('printArea').innerHTML;
                var myWindow = $window.open('', '', 'width=800, height=600');
                myWindow.document.write(table);
                myWindow.print();
            };

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
            var getIdAndHeadsOfADistribution = function () {
                ResultService.getIdAndHeadsOfADistribution($scope.selected.program.id, $scope.selected.semester.id, $scope.selected.batch.id, $scope.selected.course.id)
                   .then(
                          function (d) {
                              if (d.Status == "OK") {
                                  $scope.selection.distributedMarks = d.Data.distributedMarks;
                                  if ($scope.selection.distributedMarks.length > 0) {
                                      $scope.flag.isMarksAlreadyDistributed = true;
                                      getCourseMarksInfo();
                                      getGPAOfCourse();
                                  }
                                  else {
                                      $scope.flag.isMarksAlreadyDistributed = false;
                                  }
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
            var getCourseMarksInfo = function () {
                ResultService.getCourseMarksInfo($scope.selected.program.id, $scope.selected.semester.id, $scope.selected.batch.id, $scope.selected.course.id)
                   .then(
                          function (d) {
                              if (d.Status == "OK") {
                                  $scope.selection.courseMarksInfo = d.Data;
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
            var getGPAOfCourse = function () {
                ResultService.getResultOfACourse($scope.selected.program.id, $scope.selected.semester.id, $scope.selected.batch.id, $scope.selected.course.id)
                   .then(
                          function (d) {
                              if (d.Status == "OK") {
                                  $scope.selection.GPAOfCourse = d.Data;
                                  console.log($scope.selection.GPAOfCourse);
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
            var inatializeThePagfunction = function () {
                getPrograms();
            }
            inatializeThePagfunction();
        }])

    }()

)





