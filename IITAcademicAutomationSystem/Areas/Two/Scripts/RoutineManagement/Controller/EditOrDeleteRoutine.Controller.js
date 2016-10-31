(
    function () {
    	angular.module("routineManagement_module").controller("editOrDeleteRoutine_controller", ["$scope", "Routine_Service", "Utility_Service", function ($scope, RoutineService, UtilityService) {

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

    		$scope.constant = {
    		    rootPath: "/Areas/Two/AcademicFiles/Routine/"
    		}

    		$scope.message = {
    			content: "",
    			color: ""
    		}

    		$scope.flag = {
    		    disableSubmitButton: true,
    		    isEditButtonClicked:false,
    		}


    		$scope.whenProgramIsSelected = function () {
    			getSemesters();

    			$scope.selection.files = [];
    			$scope.selected.semester = {};
    			$scope.selected.batch = {};

    			$scope.flag = {
    			    disableSubmitButton: true,
    			    isEditButtonClicked: false,
    			}
    		}

    		$scope.whenSemesterIsSelected = function () {
    		    $scope.flag = {
    		        disableSubmitButton: true,
    		        isEditButtonClicked: false,
    		    }
    		    getBatch();
    		    getRoutines();
    		}


    		var createPath = function () {
    		    if ($scope.selection.files.routine == null || $scope.selection.files.routine == undefined)
    		        return;
    		    $scope.selected.path = $scope.constant.rootPath + $scope.selection.files.routine.path;
    		}

    		

    		$scope.whenDeleteButtonClicked = function (index) {
    		    var routine = $scope.selection.files.routine;
    		    var r = confirm("Are You Sure to Delete ");
    		    if (r == true) {
    		        deleteRoutine(routine.id);
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

    		var getRoutines = function () {
    		    RoutineService.getRoutines_tp($scope.selected.program.id, $scope.selected.semester.id)
                   .then(
                          function (d) {
                              if (d.Status == "OK") {
                                  if (d.Data.routine != null) {
                                      $scope.selection.files = d.Data;
                                      createPath();
                                      $scope.flag.isRoutineUploaded = true;
                                  }
                                  else {
                                      $scope.selection.files = [];
                                      $scope.flag.isRoutineUploaded = false;
                                      $scope.selected.path = "";
                                  }
                          	}
                          	else if (d.Status == "ERROR") {
                          		showNotification(d.Message, d.Status);
                          	}
                          },
                           function (errResponse) {
                           	showNotification('Error While Fetching Routines', 'ERROR');
                           }
                   );
    		}

    		var saveEditedRoutine = function (EditedInfo) {
    		    RoutineService.saveEditedRoutine(EditedInfo)
	              .then(
                       function (d) {
                           if (d.Status == "OK") {
                               getRoutines();
                               showNotification(d.Message, d.Status);
                           }
                           else if (d.Status == "ERROR") {
                               showNotification(d.Message, d.Status);
                           }
                       },
			            function (errResponse) {
			                showNotification('Error while Editing Routine', 'ERROR');
			            });
    		}


    		var deleteRoutine = function (routineId) {
    		    RoutineService.deleteRoutine(routineId)
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
			                showNotification('Error while Editing Routine', 'ERROR');
			            });
    		}

    		var inatializeThePage = function () {

    			getPrograms();

    		}


    		inatializeThePage();
    	}])
    }()
)



