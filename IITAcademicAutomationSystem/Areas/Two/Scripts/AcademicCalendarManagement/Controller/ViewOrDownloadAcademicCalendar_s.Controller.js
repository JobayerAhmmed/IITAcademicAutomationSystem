(
    function () {
    	angular.module("academicCalendarManagement_module").controller("viewAcademicCalendar_controller", ["$scope", "AcademicCalendar_Service", "Utility_Service", function ($scope, AcademicCalendarService, UtilityService) {



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
    		    rootPath: "/Areas/Two/AcademicFiles/AcademicCalendar/"
    		}

    		$scope.message = {
    			content: "",
    			color: ""
    		}


    		
    		var createPath = function () {
    		    if ($scope.selection.files.academicCalendar == null || $scope.selection.files.academicCalendar == undefined)
    		        return;
    		    $scope.selected.path = $scope.constant.rootPath + $scope.selection.files.academicCalendar.path;
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

    		

    		var getAcademicCalendars = function () {
    			AcademicCalendarService.getAcademicCalendars_s()
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

    			getAcademicCalendars();

    		}


    		inatializeThePage();
    	}])
    }()
)



