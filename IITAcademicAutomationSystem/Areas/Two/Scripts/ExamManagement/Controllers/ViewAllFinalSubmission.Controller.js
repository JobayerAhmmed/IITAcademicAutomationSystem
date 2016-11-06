(
    function(){
        angular.module("examinationManagement_module").controller("viewAllFinalSubmission_controller", ["$scope", "Result_Service", "Utility_Service", function ($scope, ResultService, UtilityService) {
            $scope.selected = {
                program: {
                },
                semester: {
                },
                batch: {
                }
            }

            $scope.selection = {
                programs: [],
                semesters: [],
                courseFinalSubmissionInfo:[]
            }

            $scope.message = {
                content: "",
                color: ""
            }

            $scope.flag={
                isAllCoursesFinallySubmitted:false
            }

            $scope.whenProgramIsSelected = function () {
                getSemesters();

                $scope.selected.semester = {};
                $scope.selected.batch = {};

            }

            $scope.whenSemesterIsSelected = function () {
                getBatch();
                
                
            }



            var checkIfAllCoursesAreFinallySubmitted=function () {
                var count=0;
                for(var i=0;i<$scope.selection.courseFinalSubmissionInfo.length;i++){
                    if($scope.selection.courseFinalSubmissionInfo[i].isFinallySubmitted===false){
                        $scope.flag.isAllCoursesFinallySubmitted=false;
                        break;
                    }
                    else{
                        count++;
                    }
                }
                if(count == $scope.selection.courseFinalSubmissionInfo.length){
                    $scope.flag.isAllCoursesFinallySubmitted=true;
                }
            }

            $scope.goToProcessResult = function () {
                console.log("ssss");
                window.location = '/two/ResultManagement/ViewResult';
            }

            var clearData=function () {
                $scope.selected = {
                    program: {
                    },
                    semester: {
                    },
                    batch: {
                    }
                }

                $scope.selection = {
                    programs:$scope.selection.programs,
                    semesters: []
                }

                $scope.message = {
                    content: "",
                    color: ""
                }

                $scope.flag={
                    ifMarksOfAllHeadsAreGiven:false
                }
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
                                  getFinalSubmissionInfoOfAllCourses();
                                  console.log(d.Data)
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

            var getFinalSubmissionInfoOfAllCourses = function () {
                ResultService.getFinalSubmissionInfoOfAllCourses($scope.selected.program.id, $scope.selected.semester.id,$scope.selected.batch.id)
                   .then(
                          function (d) {
                              if (d.Status == "OK") {
                                  $scope.selection.courseFinalSubmissionInfo = d.Data.submissionInfos;
                                  checkIfAllCoursesAreFinallySubmitted();
                                  console.log(d.Data)
                              }
                              else if (d.Status == "ERROR") {
                                  showNotification(d.Message, d.Status);
                              }
                          },
                           function (errResponse) {
                               showNotification('Error While Fetching Info', 'ERROR');
                           }
                   );
            }


            var inatializeThePage = function () {
                getPrograms();
            }
            inatializeThePage();
        }])
    }()
)



