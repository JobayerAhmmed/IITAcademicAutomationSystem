(
    function () {
    	angular.module("noticeManagement_module").controller("viewNotice_controller", ["$scope", "Notice_Service", "Utility_Service", function ($scope, NoticeService, UtilityService) {



    		$scope.selected = {
    			file: {
    			}

    		}

    		$scope.selection = {
    			files: []
    		}

    		$scope.flag = {
    		    isViewNoticeCliked:false,
    		}

    		$scope.message = {
    			content: "",
    			color: ""
    		}

    		

    		$scope.viewNoticeFile = function (filePath) {
    		    console.log(filePath);
    		    window.location = 'ViewNotice?filePath=' + filePath;
    		}
    		

    		$scope.downloadNotice = function (filePath) {
    			window.location = '/NoticeManagement/DownloadNotice?fileName=' + filePath;
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

    		

    		var getNotices = function () {
    			NoticeService.getNotices_s()
                   .then(
                          function (d) {
                          	if (d.Status == "OK") {
                          		$scope.selection.files = d.Data;
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

    			getNotices();

    		}


    		inatializeThePage();
    	}])
    }()
)



