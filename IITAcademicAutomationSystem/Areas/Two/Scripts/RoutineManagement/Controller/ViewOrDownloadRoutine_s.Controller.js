(
    function () {
        angular.module("routineManagement_module").controller("viewRoutine_controller", ["$scope", "Routine_Service", "Utility_Service", "$window", function ($scope, RoutineService, UtilityService,$window) {



    		$scope.selected = {
    			file: {
    			}

    		}

    		$scope.selection = {
    			files: []
    		}

    		$scope.flag = {

    		}

    		$scope.constant = {
    		    rootPath: "/Areas/Two/AcademicFiles/Routine/"
    		}

    		$scope.message = {
    			content: "",
    			color: ""
    		}

    		$scope.viewRoutineFile = function (filePath) {
    		    var completeFilePath = 'ViewRoutine?filePath=' + filePath;
    		    $window.open(completeFilePath, "_blank");
    		}
    		
    		var createPath = function () {
    		    if ($scope.selection.files.routine == null || $scope.selection.files.routine == undefined)
    		        return;
    		    $scope.selected.path = $scope.constant.rootPath + $scope.selection.files.routine.path;
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

    		

    		var getRoutines = function () {
    			RoutineService.getRoutines_s()
                   .then(
                          function (d) {
                          	if (d.Status == "OK") {
                          	    if (d.Data.routine != null) {
                          	        $scope.selection.files = d.Data;
                          	        createPath();
                          	        $scope.flag.isRoutineUploaded = true;
                          	    }
                          	    else {
                          	        $scope.flag.isRoutineUploaded = false;
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

    			getRoutines();

    		}


    		inatializeThePage();
    	}])
    }()
)



