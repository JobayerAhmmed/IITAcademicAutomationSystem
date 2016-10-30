(
    function () {
        angular.module("examinationManagement_module").controller("editDistributedMarks_controller", ["$scope", "Result_Service", "Utility_Service", function ($scope, ResultService, UtilityService) {
            $scope.name = "Ishmnam";

            var EditMarksDistribution = {
                programId: "",
                semesterId: "",
                batchId: "",
                courseId:"",
                distributions: []
            }

            $scope.selected = {
                program: {
                },
                semester: {
                },
                batch: {
                },
                course: {
                },
                distribution: {
                    number: {
                        original: 0,
                        changed:0
                    }
                        ,
                    head: []
                }
            }

            $scope.selection = {
                programs: [],
                semesters: [],
                courses: [],
                distributedMarks: [],
                heads: []
                
            }

            $scope.message = {
                content: "",
                color: ""
            }

            $scope.flag = {
                hideProgramSemesterCourse: false,
                isNumberOfHeadSelected: false,
                isWeightIsDistributedInHundred: false,
                disableSaveButton: true,
                isSameHeadSelectedMultipleTimes: {}
            }

            $scope.constant={
                newlyAddedMarksDistributionId: -1,
                isVisibleToStudent: ["Yes", "No"],
                avarageOrBestCount: ["Avarage", "Best Count"]
            }

            

            $scope.whenProgramIsSelected = function () {
                if($scope.selected.program.id)
                    getSemesters();

                $scope.selected.semester = {};
                $scope.selected.batch = {};
                $scope.selected.course = {};
                $scope.selected.distribution = {};
                $scope.selection.courses = [];
                $scope.selection.distributedMarks = [];
                $scope.selection.heads = [];

                $scope.flag = {
                    hideProgramSemesterCourse: false,
                    isNumberOfHeadSelected: false,
                    isWeightIsDistributedInHundred: false,
                    disableSaveButton: true,
                    isSameHeadSelectedMultipleTimes: {}
                }

            }


            $scope.whenSemesterIsSelected = function () {
                if ($scope.selected.program.id && $scope.selected.semester.id) {
                    getCourses();
                    getBatch();
                }
                

                $scope.selected.batch = {};
                $scope.selected.course = {};
                $scope.selected.distribution = {};
                $scope.selection.distributedMarks = [];
                $scope.selection.heads = [];

                $scope.flag = {
                    hideProgramSemesterCourse: false,
                    isNumberOfHeadSelected: false,
                    isWeightIsDistributedInHundred: false,
                    disableSaveButton: true,
                    isSameHeadSelectedMultipleTimes: {},
                }
            }
            $scope.whenCourseIsSelected = function () {
                if ($scope.selected.program.id && $scope.selected.semester.id && $scope.selected.course.id)
                    getDistributedMarks();

                
               
            }

            $scope.whenHeadNumberChanged = function () {
                if ($scope.flag.isMarksAlreadyDistributed == true)
                    nextOfGettingMarksDistribution();
            }

            var nextOfGettingMarksDistribution = function () {
                $scope.flag.showDistributedMark = true;
                getAvailableHeads();
                inatializeAllMultipleHeadSelectedErrorToFalse();
                $scope.checkIfHeadAndWeightAreDistributed();
                formateDistributedMarksToSelected();
                $scope.whenHeadIsSelected();
            }
            var formateDistributedMarksToSelected = function () {
                $scope.selected.distribution.head = [];
                for (var i = 0; i < $scope.selected.distribution.number.changed; i++) {
                    if ($scope.selection.distributedMarks[i]) {
                        $scope.selected.distribution.head[i] = {
                            id: $scope.selection.distributedMarks[i].id,
                            head: $scope.selection.distributedMarks[i].head,                            
                            weight: $scope.selection.distributedMarks[i].weight,
                            isVisibleToStudent: $scope.selection.distributedMarks[i].isVisibleToStudent,
                            avarageOrBestCount: $scope.selection.distributedMarks[i].avarageOrBestCount
                        }
                    }

                    else {
                        $scope.selected.distribution.head[i] = {
                            id: $scope.constant.newlyAddedMarksDistributionId,
                            head: {
                                id: "",
                                name:"",
                            },
                            weight: "",
                            isVisibleToStudent: $scope.constant.isVisibleToStudent[0],//0 index ontains "Yes"
                            avarageOrBestCount: $scope.constant.avarageOrBestCount[0]//0 index contains "Avarage"
                        }
                        console.log($scope.selected.distribution.head[i]);
                    }

                }
            }


            $scope.whenHeadIsSelected = function () {
                $scope.checkIfHeadAndWeightAreDistributed();
                checkIfSameHeadIsSelectedMultipleTimes();
            }

            $scope.getTimes = function (n) {
                return new Array(n);
            }

            $scope.checkIfHeadAndWeightAreDistributed = function () {
                var headFlag = checkIfALlHeadsAreSelected();
                var weightGlag = checkIfWeightAreDistributedInHundred();
                if (headFlag && weightGlag) {
                    $scope.flag.disableSaveButton = false;
                }
                else
                    $scope.flag.disableSaveButton = true;
            }


            var checkIfALlHeadsAreSelected = function () {
                var numberOfSelectedHeads = 0;
                for (var i = 0; i < $scope.selected.distribution.number.changed; i++) {
                    if ($scope.selected.distribution.head[i] == undefined)
                        continue;
                    if ($scope.selected.distribution.head[i].id == undefined)
                        continue;
                    if ($scope.selected.distribution.head[i].head.id != "")
                        numberOfSelectedHeads++;

                    console.log(numberOfSelectedHeads);
                }
                if (numberOfSelectedHeads == $scope.selected.distribution.number.changed)
                    return true;
                else
                    return false;
            }

            var checkIfWeightAreDistributedInHundred = function () {
                var totalWeight = 0;
                for (var i = 0; i < $scope.selected.distribution.number.changed; i++) {
                    if ($scope.selected.distribution.head[i] == undefined)
                        continue;
                    if ($scope.selected.distribution.head[i].weight == undefined)
                        continue;
                    if ($scope.selected.distribution.head[i].weight != "")
                        totalWeight = totalWeight + $scope.selected.distribution.head[i].weight;
                }
                if (totalWeight == 100) {
                    $scope.flag.isWeightIsDistributedInHundred = true;
                    return true;
                }
                else {
                    $scope.flag.isWeightIsDistributedInHundred = false;
                    return false;
                }
            }



            var checkIfSameHeadIsSelectedMultipleTimes = function () {
                inatializeAllMultipleHeadSelectedErrorToFalse();
                var flag = false;
                var index = []
                for (var i = 0; i < $scope.selected.distribution.number.changed; i++) {
                    if ($scope.selected.distribution.head[i].head.id == undefined || $scope.selected.distribution.head[i].head.id == "") {
                        continue;
                    }
                    for (var j = i + 1; j < $scope.selected.distribution.head.length; j++) {
                        if ($scope.selected.distribution.head[j].head.id == undefined || $scope.selected.distribution.head[j].head.id == "") {
                            continue;
                        }
                        if ($scope.selected.distribution.head[i].head.id == $scope.selected.distribution.head[j].head.id) {
                            $scope.flag.isSameHeadSelectedMultipleTimes[i] = true;
                            $scope.flag.isSameHeadSelectedMultipleTimes[j] = true;
                            $scope.flag.disableSaveButton = true;
                        }
                    }
                }
            }

            var inatializeAllMultipleHeadSelectedErrorToFalse = function () {
                for (var i = 0; i < $scope.selected.distribution.number.changed; i++) {
                    $scope.flag.isSameHeadSelectedMultipleTimes[i] = false;
                }
            }


            $scope.cancelDistribution = function () {
                clearData();
            }


            $scope.save = function () {
                processDistributedMarksToEdit();
                saveEditedDistributedMarks(EditMarksDistribution);
                console.log("Final");
                console.log(EditMarksDistribution);
            }

            var processDistributedMarksToEdit = function () {
                EditMarksDistribution.distributions = [];
                EditMarksDistribution.programId=$scope.selected.program.id;
                EditMarksDistribution.semesterId=$scope.selected.semester.id;
                EditMarksDistribution.batchId=$scope.selected.batch.id;
                EditMarksDistribution.courseId=$scope.selected.course.id;

                for (var i = 0; i < $scope.selected.distribution.head.length; i++) {
                    EditMarksDistribution.distributions[i] = {
                        id:$scope.selected.distribution.head[i].id,
                        weight:$scope.selected.distribution.head[i].weight,
                        avarageOrBestCount:$scope.selected.distribution.head[i].avarageOrBestCount,
                        isVisibleToStudent: $scope.selected.distribution.head[i].isVisibleToStudent,
                        headId: $scope.selected.distribution.head[i].head.id
                    }
                }
            }

            var clearData = function () {
                var EditMarksDistribution = {
                    programId: "",
                    semesterId: "",
                    batchId: "",
                    courseId: "",
                    distributions: []
                }

                $scope.selected = {
                    program: {
                    },
                    semester: {
                    },
                    batch: {
                    },
                    course: {
                    },
                    distribution: {
                        number: {
                            original: 0,
                            changed: 0
                        }
                            ,
                        head: []
                    }
                }

                $scope.selection = {
                    programs: $scope.selection.programs,
                    semesters: [],
                    courses: [],
                    distributedMarks: [],
                    heads: []

                }

                $scope.message = {
                    content: "",
                    color: ""
                }

                $scope.flag = {
                    hideProgramSemesterCourse: false,
                    isMarksAlreadyDistributed: true,
                    isNumberOfHeadSelected: false,
                    isWeightIsDistributedInHundred: false,
                    disableSaveButton: true,
                    isSameHeadSelectedMultipleTimes: {},
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


            var inatializeThePage = function () {
                getPrograms();
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
                               showNotification('Error While Fetching Programs','ERROR');
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
                               showNotification('Error While Fetching Semesters','ERROR');
                           }
                   );

            }

            var getBatch = function () {
                UtilityService.getBatch($scope.selected.program.id, $scope.selected.semester.id)
                   .then(
                          function (d) {
                              if (d.Status == "OK") {
                                  $scope.selected.batch = d.Data;
                                  console.log(d.Data)
                              }
                              else if (d.Status == "ERROR") {
                                  showNotification(d.Message, d.Status);
                              }
                          },
                           function (errResponse) {
                               showNotification('Error While Fetching Batch','ERROR');
                           }
                   );
            }

            var getCourses = function () {
                UtilityService.getCourses($scope.selected.program.id, $scope.selected.semester.id)
                   .then(
                          function (d) {
                              if (d.Status == "OK") {
                                  $scope.selection.courses = d.Data.courses;
                              }
                              else if (d.Status == "ERROR") {
                                  showNotification(d.Message, d.Status);
                              }
                          },
                           function (errResponse) {
                               showNotification('Error While Fetching Courses','ERROR');
                           }
                   );
            }

            var saveEditedDistributedMarks = function (editedMarksDistribution) {
                ResultService.saveEditedMarksDistribution(editedMarksDistribution)
	              .then(
                       function (d) {
                           if (d.Status == "OK") {
                               showNotification(d.Message, d.Status);
                           }
                           else if (d.Status == "ERROR") {
                               showNotification(d.Message, d.Status);
                           }
                       },
			            function (errResponse) {
			                showNotification('Error while Adding Subhead','ERROR');
			            });
                clearData();
            }


            var getAvailableHeads = function () {
                ResultService.getAllHeads()
                    .then(
			               function (d) {
			                   if (d.Status == "OK") {
			                       $scope.selection.heads = d.Data.heads;
			                   }
			                   else if (d.Status == "ERROR") {
			                       showNotification(d.Message, d.Status);
			                   }
			               },
    				        function (errResponse) {
    				            showNotification('Error While Fetching Heads','ERROR');
    				        }
	                );

            }

            var getDistributedMarks = function () {
                ResultService.getDistributedMarks($scope.selected.program.id, $scope.selected.semester.id,$scope.selected.batch.id, $scope.selected.course.id)
                    .then(
			               function (d) {
			                   console.log(d);
			                   if (d.Status == "OK") {
			                       $scope.selection.distributedMarks = d.Data.distributedMarks;
			                       var distributionLength = $scope.selection.distributedMarks.length;		                       
			                             
			                       if (distributionLength != 0) {
			                               $scope.flag.isMarksAlreadyDistributed = true;
			                               $scope.selected.distribution.number = {
			                                   original: distributionLength,
			                                   changed: distributionLength
			                               }
			                               formateDistributedMarksToSelected();
			                               nextOfGettingMarksDistribution();
			                           }
			                           else {
			                               $scope.flag.isMarksAlreadyDistributed = false;
			                           }			                       
			                   }
			                   else if (d.Status == "ERROR") {
			                       showNotification(d.Message, d.Status);
			                   }
			               },
    				        function (errResponse) {
    				            showNotification('Error While Checking','ERROR');
    				        }
	                );
            }
            inatializeThePage();
        }])

    }()

)















