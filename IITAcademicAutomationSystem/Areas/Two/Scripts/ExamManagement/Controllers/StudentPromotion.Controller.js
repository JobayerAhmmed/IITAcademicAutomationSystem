(
    function(){
        angular.module("examinationManagement_module").controller("StudentPromotion_Controller", ["$scope", "Result_Service", "Utility_Service", function ($scope, ResultService, UtilityService) {
            $scope.name="Ishmnam";

            var PromoteOrDemote = {
                program:{},
                semester:{},
                studentPassFailInfo:[]
            };
            

            $scope.selected={
                program:{
                },
                semester:{
                },
                batch:{
                }

            }

            $scope.selection={
                programs:[],
                semesters:[],
                studentInfo:[]
            }

            $scope.flag={
                haveIRead:false
            }

            $scope.flag = {
                hideProgramSemesterCourse: false,
                disableSaveButton: true,
            }

            $scope.whenProgramIsSelected=function() {
                getSemesters();
                
            }


            $scope.whenSemesterIsSelected = function () {
                getBatch();
                
            }

            var processObjectToSave = function () {
                PromoteOrDemote.program = $scope.selected.program;
                PromoteOrDemote.semester = $scope.selected.semester;
                PromoteOrDemote.batch = $scope.selected.batch;
                for (var i = 0; i < $scope.selection.studentInfo.length; i++) {
                    PromoteOrDemote.studentPassFailInfo[i] = {
                        studentId: $scope.selection.studentInfo,
                        isPassedAllCourses:$scope.selection.isPassedAllCourses
                    }
                }
            }

            $scope.whenOkButtonIsClicked=function() {
                processObjectToSave();
                console.log(PromoteOrDemote);
            }

            var clearData = function () {
                var PromoteOrDemote = {
                    program: {},
                    semester: {},
                    studentPassFailInfo: []
                };


                $scope.selected = {
                    program: {
                    },
                    semester: {
                    },
                    batch: {
                    }

                }

                $scope.selection = {
                    programs: $scope.selection.programs,
                    semesters: [],
                    studentInfo: []
                }

                $scope.flag = {
                    haveIRead: false
                }

                $scope.flag = {
                    hideProgramSemesterCourse: false,
                    disableSaveButton: true,
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
                UtilityService.getAllPrograms()
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
                UtilityService.getSemestersOfAProgram($scope.selected.program.id)
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
                                  getPassFailInfoOfASemester();
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

            var getPassFailInfoOfASemester = function () {
                ResultService.getPassFailInfoOfASemester($scope.selected.program.id, $scope.selected.semester.id,$scope.selected.batch.id)
                   .then(
                          function (d) {
                              if (d.Status == "OK") {
                                  $scope.selection.studentInfo = d.Data;
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

            var inatializeThePage = function () {
                getPrograms();
            }


            inatializeThePage();
        }])

    }()

)





