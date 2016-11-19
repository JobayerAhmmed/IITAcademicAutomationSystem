(
    function(){
        angular.module("academicCalendarManagement_module").controller("uploadAcademicCalendar_controller", ["$scope", "AcademicCalendar_Service", "Utility_Service", function ($scope, AcademicCalendarService, UtilityService) {

            var InformationToSave = {
                title:"",
                description:"",
                programId: "",
                semesterId: "",
                batchId:""
            }

            $scope.selected = {
                program: {
                },
                semester: {
                },
                batch: {
                },
                
            }

            $scope.selection = {
                programs: [],
                semesters: []
            }           

            $scope.message = {
                content: "",
                color: ""
            }

            $scope.flag = {
                disableSubmitButton: true,
                showUploadForm: false
            }

            $scope.constant = {
                
            }


            $scope.whenProgramIsSelected = function () {               
                getSemesters();
                $scope.flag.showUploadForm = false;
                $scope.selected.semester = {};
                $scope.selected.batch = {};
                
                $scope.flag = {
                    disableSubmitButton: true,
                    showUploadForm: false
                }
            }

            $scope.whenSemesterIsSelected = function () {
                $scope.flag = {
                    disableSubmitButton: true,
                    showUploadForm: true
                }
                getBatch();
                checkIfAcademicCalendarAlreadyUploaded();

                
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

            var checkIfAcademicCalendarAlreadyUploaded = function () {
                AcademicCalendarService.checkIfAcademicCalendarAlreadyUploaded($scope.selected.program.id, $scope.selected.semester.id)
                   .then(
                          function (d) {
                              if (d.Status == "OK") {
                                  $scope.flag.isAcademicCalendarAlreadyUploaded = d.Data;
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


            var uploadAcademicCalendar = function () {
                if ($scope.IsFormValid && $scope.IsFileValid) {
                    AcademicCalendarService.UploadFile($scope.SelectedFileForUpload, InformationToSave)
                        .then(function (d) {
                            showNotification(d.Message, d.Status);
                            clearDate();
                        },
                         function (e) {
                             showNotification("Error Has occured while saving Routine", 'ERROR');
                         }
                      );
                }
                else {
                    $scope.Message = "All the fields are required.";
                }
            }

            $scope.Message = "";
            $scope.FileInvalidMessage = "";
            $scope.SelectedFileForUpload = null;
            $scope.IsFormSubmitted = false;
            $scope.IsFileValid = false;
            $scope.IsFormValid = false;

            $scope.$watch("f1.$valid", function (isValid) {
                $scope.IsFormValid = isValid;
            });

            $scope.ChechFileValid = function (file) {
                var isValid = false;
                if ($scope.SelectedFileForUpload != null) {
                    if ((file.type == 'application/pdf') && file.size <= (1024 * 1024 * 5 )) {
                        $scope.FileInvalidMessage = "";
                        isValid = true;
                    }
                    else {
                        $scope.FileInvalidMessage = "Only file type PDF and 5 Mb size allowed";
                    }
                }
                else {
                    $scope.FileInvalidMessage = "Only file type PDF and 5 Mb size allowed";
                }
                $scope.IsFileValid = isValid;
            };

            $scope.selectFileforUpload = function (file) {
                $scope.SelectedFileForUpload = file[0];
            }
            
            $scope.SaveFile = function () {
                processInfoToSave();
                console.log(InformationToSave);
                $scope.ChechFileValid($scope.SelectedFileForUpload);
                uploadAcademicCalendar();
            };

            var processInfoToSave = function () {
                InformationToSave.programId= $scope.selected.program.id;
                InformationToSave.semesterId= $scope.selected.semester.id;
                InformationToSave.batchId= $scope.selected.batch.id;
                
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
                }

                $scope.selection = {
                    programs: $scope.selection.programs,
                    semesters: []
                }

                $scope.flag = {
                   showUploadForm : false
                }


                $scope.IsFormSubmitted = true;
                $scope.Message = "";
                $scope.FileDescription = "";
                angular.forEach(angular.element("input[type='file']"), function (inputElem) {
                    angular.element(inputElem).val(null);
                });
                $scope.f1.$setPristine();
                $scope.IsFormSubmitted = false;
            }

            var inatializeThePage = function () {

                getPrograms();

            }


            inatializeThePage();
        }])
    }()
)



