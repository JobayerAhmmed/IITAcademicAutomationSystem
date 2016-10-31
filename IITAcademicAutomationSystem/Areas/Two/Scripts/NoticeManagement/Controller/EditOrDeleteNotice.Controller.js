(
    function () {
    	angular.module("noticeManagement_module").controller("editOrDeleteNotice_controller", ["$scope", "Notice_Service", "Utility_Service", function ($scope, NoticeService, UtilityService) {

    	    var EditedInfo = {
    	        id: "",
    	        title: ""
    	    }

    		$scope.selected = {
    			program: {
    			},
    			semester: {
    			},
    			batch: {
    			},
    			file: {
    				title: ""
                    }
    		}

    		$scope.selection = {
    			programs: [],
    			semesters: [],
				files:[]
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

    			$scope.selected.semester = {};
    			$scope.selected.batch = {};


    		}

    		$scope.whenSemesterIsSelected = function () {
    		    getBatch();
    		    getNotices();
    		}

    		$scope.whenEditButtonIsSelected = function (index) {
    		    $scope.flag.isEditButtonClicked = true;
    		    $scope.selected.file={
    		        id:$scope.selection.files.notices[index].id,
    		        title:$scope.selection.files.notices[index].title
    		    }
    		}

    		$scope.whenSaveEditIsClicked = function () {
    		    processInfoToSave();
    		    saveEditedNotice(EditedInfo);
    		    clearEditData();
    		}

    		var processInfoToSave = function () {
    		    EditedInfo = {
    		        id: $scope.selected.file.id,
    		        title: $scope.selected.file.title
    		    }
    		}

    		$scope.whenCancelEditIsClicked = function () {
    		    clearEditData();
    		}

    		$scope.whenDeleteButtonClicked = function (index) {
    		    var notice = $scope.selection.files.notices[index];
    		    var r = confirm("Are You Sure to Delete " + notice.title);
    		    if (r == true) {
    		        deleteNotice(notice.id);
    		    } else {
    		        return;
    		    }
    		}



    		$scope.whenTitleIsChanged = function () {
    			checkIfAllInfoAreGiven();
    		}

    		var checkIfAllInfoAreGiven = function () {
    			if ($scope.selected.file.title != ""  && $scope.selected.file.title != null ) {
    				$scope.flag.disableSubmitButton = false;
    			}
    			else {
    				$scope.flag.disableSubmitButton = true;
    			}
    		}

    		var clearEditData = function () {
    		    $scope.flag.isEditButtonClicked = false;
    		    $scope.flag.disableSubmitButton = true;
    		    $scope.selected.file = {
    		        title: ""
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

    		var getNotices = function () {
    		    NoticeService.getNotices_tp($scope.selected.program.id, $scope.selected.semester.id)
                   .then(
                          function (d) {
                              console.log("AAA");
                              console.log(d);
                          	if (d.Status == "OK") {
                          		$scope.selection.files = d.Data;
                          	}
                          	else if (d.Status == "ERROR") {
                          		showNotification(d.Message, d.Status);
                          	}
                          },
                           function (errResponse) {
                           	showNotification('Error While Fetching Notices', 'ERROR');
                           }
                   );
    		}

    		var saveEditedNotice = function (EditedInfo) {
    		    NoticeService.saveEditedNotice(EditedInfo)
	              .then(
                       function (d) {
                           if (d.Status == "OK") {
                               getNotices();
                               showNotification(d.Message, d.Status);
                           }
                           else if (d.Status == "ERROR") {
                               showNotification(d.Message, d.Status);
                           }
                       },
			            function (errResponse) {
			                showNotification('Error while Editing Notice', 'ERROR');
			            });
    		}


    		var deleteNotice = function (noticeId) {
    		    NoticeService.deleteNotice(noticeId)
	              .then(
                       function (d) {
                           if (d.Status == "OK") {
                               getNotices();
                               showNotification(d.Message, d.Status);
                           }
                           else if (d.Status == "ERROR") {
                               showNotification(d.Message, d.Status);
                           }
                       },
			            function (errResponse) {
			                showNotification('Error while Editing Notice', 'ERROR');
			            });
    		}

    		var inatializeThePage = function () {

    			getPrograms();

    		}


    		inatializeThePage();
    	}])
    }()
)



