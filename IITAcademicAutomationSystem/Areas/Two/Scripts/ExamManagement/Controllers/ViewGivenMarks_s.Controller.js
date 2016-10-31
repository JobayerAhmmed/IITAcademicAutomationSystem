(
    function () {
        angular.module("examinationManagement_module").controller("viewGivenMarks_controller", ["$scope", "Result_Service", "Utility_Service", function ($scope, ResultService, UtilityService) {
            $scope.name = "Ishmnam";



            $scope.selected = {
                program: {
                },
                semester: {
                },
                batch: {
                },
                course: {
                },

            }

            $scope.selection = {
                programs: [],
                semesters: [],
                courses: [],
                marksInfo: []
            }

            $scope.message = {
                content: "",
                color: ""
            }

            $scope.whenCourseIsSelected = function () {
                getGivenMarksOfAllStudentOfACourse();
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


            

            var getCourses = function () {
                UtilityService.getAllCoursesOfAStudent()
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

            var getGivenMarksOfAllStudentOfACourse = function () {
                ResultService.getGivenMarksOfAllStudentOfACourse_s($scope.selected.course.id)
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
                               showNotification('Error While Fetching Courses', 'ERROR');
                           }
                   );
            }


            var inatializeThePagfunction = function () {
                getCourses();
            }


            inatializeThePagfunction();
        }])

    }()

)





