(
    function () {
        angular.module("examinationManagement_module").controller("viewMarksDistribution_controller", ["$scope", "Result_Service", "Utility_Service", function ($scope, ResultService, UtilityService) {
            $scope.name = "Ishmnam";


            $scope.selected = {
                program: {
                    
                },
                semester: {
                    
                },
                batch:{
                },
                course: {
                }
            }

            $scope.selection = {                
                courses: [],
                headDistribution: []
            }

            $scope.flag = {
            }     

            $scope.message = {
                content: "",
                color: ""
            }

            $scope.whenCourseIsSelected = function () {
                $scope.flag = {
                }
                getMarksDistribution();
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
                    });
                }, 3000);
            }

            var getProgramSemesterBatchOfLoggedInStudent = function () {
                UtilityService.getProgramSemesterBatchOfAStuent()
                   .then(
                          function (d) {
                              if (d.Status == "OK") {
                                  console.log(d);
                                  $scope.selected.program = d.Data.program;
                                  $scope.selected.semester = d.Data.semester;
                                  $scope.selected.batch = d.Data.batch;
                                  getCourses();
                              }
                              else if (d.Status == "ERROR") {
                                  showNotification(d.Message,d.Status);
                              }
                          },
                           function (errResponse) {
                               showNotification('Error While Fetching Programs','ERROR');
                           }
                   );
            }

            var getCourses = function () {
                UtilityService.getAllCoursesOfAStudent($scope.selected.program.id, $scope.selected.semester.id)
                   .then(
                          function (d) {
                              if (d.Status == "OK") {
                                  $scope.selection.courses = d.Data.courses;
                              }
                              else if (d.Status == "ERROR") {
                                  showNotification(d.Message,d.Status);
                              }
                          },
                           function (errResponse) {
                               showNotification('Error While Fetching Courses','ERROR');
                           }
                   );
            }


            var getMarksDistribution = function () {
                ResultService.getDistributedMarks($scope.selected.program.id, $scope.selected.semester.id, $scope.selected.batch.id, $scope.selected.course.id)
                   .then(
                          function (d) {
                              if (d.Status == "OK") {
                                  $scope.selection.headDistribution = d.Data.distributedMarks;

                                  if ($scope.selection.headDistribution.length != 0) {
                                      $scope.flag.isMarksAlreadyDistributed = true;
                                  }
                                  else {
                                      $scope.flag.isMarksAlreadyDistributed = false;
                                  }

                              }
                              else if (d.Status == "ERROR") {
                                  $scope.flag.isMarksAlreadyDistributed = "";
                                  showNotification(d.Message, d.Status);
                              }
                          },
                           function (errResponse) {
                               $scope.flag.isMarksAlreadyDistributed = "";
                               showNotification('Error While Fetching Marks Distribution', 'ERROR');
                           }
                   );
            }

            var inatializeThePagfunction = function () {
                getProgramSemesterBatchOfLoggedInStudent();
                
            }


            inatializeThePagfunction();

            }])

        }()

)












