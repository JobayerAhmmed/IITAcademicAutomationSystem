  (
    function () {
        angular.module("academicCalendarManagement_module").controller("viewAcademicCalendar_controller", ["$scope", "AcademicCalendar_Service", "Utility_Service", "$window", function ($scope, AcademicCalendarService, UtilityService,$window) {

           

            $scope.selected = {
                program: {
                },
                semester: {
                },
                file:{
                }
                
            }

            $scope.selection = {
                programs: [],
                semesters: [],
                files:[]
            }

            $scope.flag = {

            }

            /*$scope.viewNoticeFile = function (filePath) {
                console.log(filePath);
                var completeFilePath = 'ViewNotice?filePath=' + filePath;
                $window.open(completeFilePath, "_blank");
            }*/
            $scope.viewAcademicCalendarFile = function (filePath) {
                var completeFilePath = 'ViewAcademicCalendar?filePath=' + filePath;
                $window.open(completeFilePath, "_blank");
            }
            $scope.constant={
                rootPath:"/Areas/Two/AcademicFiles/AcademicCalendar/"
            }

            $scope.message = {
                content: "",
                color: ""
            }


            $scope.whenProgramIsSelected = function () {
                getSemesters();
                
                $scope.selected.semester = {};
                $scope.selected.file = {};
                $scope.selected.path = "";
                $scope.selection.files = [];  
            }

            $scope.whenSemesterIsSelected = function () {
                getAcademicCalendars();
            }

            var createPath = function () {
                if ($scope.selection.files.academicCalendar == null || $scope.selection.files.academicCalendar == undefined)
                    return;
                $scope.selected.path = $scope.constant.rootPath + $scope.selection.files.academicCalendar.path;
            }

            /*$scope.downloadAcademicCalendar=function(filePath) {
                window.location = '/AcademicCalendarManagement/DownloadAcademicCalendar?fileName=' + filePath;
            }*/


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

            var getAcademicCalendars = function () {
                AcademicCalendarService.getAcademicCalendars_tp($scope.selected.program.id, $scope.selected.semester.id)
                   .then(
                          function (d) {
                              if (d.Status == "OK") {
                                  if (d.Data.academicCalendar != null) {
                                      $scope.selection.files = d.Data;
                                      createPath();
                                      $scope.flag.isAcademicCalendarUploaded = true;
                                  }
                                  else {
                                      $scope.flag.isAcademicCalendarUploaded = false;
                                      $scope.selected.path = "";
                                  }
                                  
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
            

            function clearDate() {
                var InformationToSave = {
                    title: "",
                    description: "",
                    programId: "",
                    semesterId: "",
                    batchId: ""
                }

                $scope.selected = {
                    program: {
                    },
                    semester: {
                    },
                    batch: {
                    },
                    file: {
                        title: "",
                        description: ""
                    }
                }

                $scope.selection = {
                    programs: $scope.selection.programs,
                    semesters: []
                }

                $scope.flag = {
                    disableSubmitButton: true
                }
            }

            var inatializeThePage = function () {

                getPrograms();

            }


            inatializeThePage();
        }])
    }()
)



