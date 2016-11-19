(
    function () {
        angular.module("academicCalendarManagement_module").controller("editOrDeleteAcademicCalendar_controller", ["$scope", "AcademicCalendar_Service", "Utility_Service", "$window", function ($scope, AcademicCalendarService, UtilityService, $window) {

    	    

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
    			programs: [],
    			semesters: [],
				files:[]
    		}

    		$scope.flag = {
    		    disableSubmitButton: true,
    		    isEditButtonClicked: false,
    		}

    		$scope.viewAcademicCalendarFile = function (filePath) {
		        var completeFilePath = 'ViewAcademicCalendar?filePath=' + filePath;
    		    $window.open(completeFilePath, "_blank");
    		}

    		$scope.constant = {
    		    rootPath: "/Areas/Two/AcademicFiles/AcademicCalendar/"
    		}

    		$scope.message = {
    			content: "",
    			color: ""
    		}

    		


    		$scope.whenProgramIsSelected = function () {
    			getSemesters();

    			$scope.selection.files = [];
    			$scope.selected.semester = {};
    			$scope.selected.batch = {};


    		}

    		$scope.whenSemesterIsSelected = function () {
    		    getBatch();
    		    getAcademicCalendars();
    		}


    		var createPath = function () {
    		    if ($scope.selection.files.academicCalendar == null || $scope.selection.files.academicCalendar == undefined)
    		        return;
    		    $scope.selected.path = $scope.constant.rootPath + $scope.selection.files.academicCalendar.path;
    		}

    		

    		$scope.whenDeleteButtonClicked = function (index) {
    		    var academicCalendar = $scope.selection.files.academicCalendar;
    		    var r = confirm("Are You Sure to Delete ");
    		    if (r == true) {
    		        deleteAcademicCalendar(academicCalendar.id);
    		    } else {
    		        return;
    		    }
    		}


    		var clearData = function () {
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
    		        semesters: [],
    		        files: []
    		    }

    		    $scope.flag = {
    		        disableSubmitButton: true,
    		        isEditButtonClicked: false,
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
                                      $scope.selection.files = [];
                                      $scope.flag.isAcademicCalendarUploaded = false;
                                      $scope.selected.path = "";
                                  }
                          	}
                          	else if (d.Status == "ERROR") {
                          		showNotification(d.Message, d.Status);
                          	}
                          },
                           function (errResponse) {
                           	showNotification('Error While Fetching AcademicCalendars', 'ERROR');
                           }
                   );
    		}

    		var saveEditedAcademicCalendar = function (EditedInfo) {
    		    AcademicCalendarService.saveEditedAcademicCalendar(EditedInfo)
	              .then(
                       function (d) {
                           if (d.Status == "OK") {
                               getAcademicCalendars();
                               showNotification(d.Message, d.Status);
                           }
                           else if (d.Status == "ERROR") {
                               showNotification(d.Message, d.Status);
                           }
                       },
			            function (errResponse) {
			                showNotification('Error while Editing AcademicCalendar', 'ERROR');
			            });
    		}


    		var deleteAcademicCalendar = function (academicCalendarId) {
    		    AcademicCalendarService.deleteAcademicCalendar(academicCalendarId)
	              .then(
                       function (d) {
                           if (d.Status == "OK") {
                               clearData();
                               showNotification(d.Message, d.Status);
                           }
                           else if (d.Status == "ERROR") {
                               showNotification(d.Message, d.Status);
                           }
                       },
			            function (errResponse) {
			                showNotification('Error while Editing AcademicCalendar', 'ERROR');
			            });
    		}

    		var inatializeThePage = function () {

    			getPrograms();

    		}


    		inatializeThePage();
    	}])
    }()
)



