(
    function(){
        angular.module("examinationManagement_module").controller("editGivenMarks_controller", ["$scope", "Result_Service", "Utility_Service", function ($scope, ResultService, UtilityService) {
            $scope.name="Ishmnam";

            var EditMarks={
                marksToEdit: []
            }

            $scope.selected={
                program:{
                },
                semester:{
                },
                batch:{
                },
                course:{
                },
                head:{
                },
                subHead:{

                },
                marksInfo:{

               }
            }

            $scope.selection={
                programs:[],
                semesters:[],
                courses:[],
                heads:[],
                subHeads:[],
            }

            $scope.flag={
                disableSaveButton:false
            }

            $scope.message = {
                content: "",
                color: ""
            }

            $scope.whenProgramIsSelected=function() {
                getSemesters();

                $scope.selected.semester={};
                $scope.selected.batch={};
                $scope.selected.course={};
                $scope.selected.head={};
                $scope.selected.subHead={};
                $scope.selected.marksInfo={};


                $scope.selection.courses=[];
                $scope.selection.heads=[];
                $scope.selection.subHeads = [];
                $scope.flag = {
                    disableSaveButton: false
                }
            }


            $scope.whenSemesterIsSelected = function () {
                getBatch();
                getCourses();

                $scope.selected.batch={};
                $scope.selected.course={};
                $scope.selected.head={};
                $scope.selected.subHead={};
                $scope.selected.marksInfo={};

                $scope.selection.heads=[];
                $scope.selection.subHeads=[];
                $scope.flag = {
                    disableSaveButton: false
                }
            }


            $scope.whenCourseIsSelected=function(){
                getIdAndHeadsOfADistribution();
                $scope.selected.head={};
                $scope.selected.subHead={};
                $scope.selected.marksInfo={};
                $scope.selection.subHeads = [];

                $scope.flag = {
                    disableSaveButton: false
                }
            }

            $scope.whenHeadIsSelected = function () {
                $scope.selected.head = {
                    id: $scope.selected.distributedMarks.head.id,
                    name: $scope.selected.distributedMarks.head.name,
                }
                getSubHeadOfAHead();
                $scope.selected.subHead={};
                $scope.selected.marksInfo = {};

                $scope.flag = {
                    disableSaveButton: false,
                    isMarksAlreadyDistributed: $scope.flag.isMarksAlreadyDistributed
                }

            }


            $scope.whenSubHeadIsSelected = function () {
                $scope.flag = {
                    disableSaveButton: false,
                    isMarksAlreadyDistributed: $scope.flag.isMarksAlreadyDistributed
                }
                getGivenMarks();
                $scope.flag.hideProgramSemesterCourse=true;
            }      


            $scope.whenMarksAreGiven=function () {
                checkWheatherToDisableSaveButton();
            }

            var checkWheatherToDisableSaveButton=function () {
                var temp=checkIfAllMarksAreGiven();
                if(temp==false){
                    $scope.flag.disableSaveButton=true;
                }
                else{
                    $scope.flag.disableSaveButton=false;
                }
            }

            var checkIfAllMarksAreGiven=function () {
                for(var i=0;i<$scope.selected.marksInfo.obtainedMarks.length;i++){
                    if($scope.selected.marksInfo.obtainedMarks[i].marks=="" || $scope.selected.marksInfo.obtainedMarks[i].marks==null)
                        return false;
                }
                return true;
            }


            $scope.cancel=function(){
                clearData();

            }

            $scope.save=function(){
                EditMarks.marksToEdit = $scope.selected.marksInfo.obtainedMarks;

                saveEditedMarks(EditMarks);
                console.log(EditMarks);
            }

            var inatializeThePage=function(){
                getPrograms();
            }



            var clearData = function () {
                var EditMarks = {
                    marksToEdit: []
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
                    head: {
                    },
                    subHead: {

                    },
                    marksInfo: {

                    }
                }

                $scope.selection = {
                    programs:$scope.selection.programs,
                    semesters: [],
                    courses: [],
                    heads: [],
                    subHeads: [],
                }

                $scope.flag = {
                    hideProgramSemesterCourse: false,
                    disableSaveButton: false
                }

                $scope.message = {
                    content: "",
                    color: ""
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

            var getIdAndHeadsOfADistribution = function () {
                ResultService.getIdAndHeadsOfADistribution($scope.selected.program.id, $scope.selected.semester.id, $scope.selected.batch.id, $scope.selected.course.id)
                   .then(
                          function (d) {
                              if (d.Status == "OK") {
                                  $scope.selection.distributedMarks = d.Data.distributedMarks;
                                  if ($scope.selection.distributedMarks.length > 0) {
                                      $scope.flag.isMarksAlreadyDistributed = true;
                                      getAllStudentOfACourse();
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
                               showNotification('Error While Fetching Courses','ERROR');
                           }
                   );
            }

            var getSubHeadOfAHead = function () {
                ResultService.getSubHead($scope.selected.distributedMarks.head.id)
                    .then(
			               function (d) {
			                   if (d.Status == "OK") {
			                       $scope.selection.subHeads = d.Data.subHeads;
			                   }
			                   else if (d.Status == "ERROR") {
			                       showNotification(d.Message, d.Status);
			                   }
			               },
    				        function (errResponse) {
    				            showNotification('Error While Fetching Sub Heads','ERROR');
    				        }
	                );
            }

            var getAllStudentOfACourse = function () {
                UtilityService.getAllStudentOfACourse($scope.selected.program.id, $scope.selected.semester.id, $scope.selected.batch.id, $scope.selected.course.id)
                    .then(
			               function (d) {
			                   if (d.Status == "OK") {			                       
			                       $scope.selection.students = d.Data.students;
			                   }
			                   else if (d.Status == "ERROR") {
			                       showNotification(d.Message, d.Status);
			                   }
			               },
    				        function (errResponse) {
    				            showNotification('Error While Fetching Sub Heads','ERROR');
    				        }
	                );
            }

            var getGivenMarks = function () {
                ResultService.getGivenMarksOfAllStudentForEditing($scope.selected.program.id, $scope.selected.semester.id, $scope.selected.batch.id, $scope.selected.course.id, $scope.selected.head.id, $scope.selected.subHead.id)
                    .then(
			               function (d) {
			                   if (d.Status == "OK") {
			                       $scope.selected.marksInfo = d.Data;

			                       if ($scope.selected.marksInfo.obtainedMarks != null && $scope.selected.marksInfo.examMarks!=0) {
                                       $scope.flag.isMarksGiven=true;
			                       }
			                       else {
			                           $scope.flag.isMarksGiven = false;
			                       }
			                   }
			                   else if (d.Status == "ERROR") {
			                       showNotification(d.Message, d.Status);
			                   }
			               },
    				        function (errResponse) {
    				            showNotification('Error While Fetching Sub Heads','ERROR');
    				        }
	                );
            }

            var saveEditedMarks = function (editedMarks) {
                ResultService.saveEditedMarks(editedMarks)
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
            
            inatializeThePage();
        }])
    }()
)














