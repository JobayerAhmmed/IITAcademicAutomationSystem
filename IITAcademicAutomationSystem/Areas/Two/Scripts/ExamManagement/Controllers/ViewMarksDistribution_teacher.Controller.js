(
    function(){
        angular.module("examinationManagement_module").controller("viewMarksDistribution_controller", ["$scope", "Result_Service", "Utility_Service", function ($scope, ResultService, UtilityService) {
            $scope.name="Ishmnam";



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
                headDistribution:[]
            }

            $scope.flag = {               
            }

            $scope.whenProgramIsSelected=function() {
                getSemesters();
                $scope.selected.semester={};
                $scope.selected.batch={};
                $scope.selected.course={}
                $scope.selected.head={}

                $scope.selection.courses = [];
                $scope.flag = {
                }
            }

            $scope.message = {
                content: "",
                color: ""
            }

            $scope.whenSemesterIsSelected=function(){
                getCourses();
                getBatch();

                $scope.selected.batch={};
                $scope.selected.course = {}
                $scope.flag = {
                }


            }


            $scope.whenCourseIsSelected=function(){                
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
                                  console.log(d.Data)
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
                               showNotification('Error While Fetching Marks Distribution','ERROR');
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












