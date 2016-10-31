(
    function(){
        angular.module("examinationManagement_module").controller("giveMarks_controller", ["$scope", "Result_Service", "Utility_Service", function ($scope, ResultService, UtilityService) {
            $scope.name = "Ishmnam";
           

            var GiveMarks={
                programId:"",
                semesterId:"",
                batchId:"",
                courseId:"",
                subHeadId:"",
                examMarks: "",
                marksDistributionId:"",
                marks: []
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
                distributedMarks:{
                },
                subHead:{

                },
                marks:{
                    examMarks:"",
                    obtainedMarks:[]
                }
            }

            $scope.selection={
                programs:[],
                semesters:[],
                courses:[],
                distributedMarks:[],
                subHeads:[],
                students:[

                ]
            }

            $scope.flag={
                hideProgramSemesterCourse:false,
                disableSaveButton:true,
            }

            $scope.message = {
                content: "",
                color: ""
            }
            

            $scope.whenProgramIsSelected=function() {
                getSemesters();




                 $scope.selected.semester={};
                 $scope.selected.batch={};
                 $scope.selected.course={}
                 $scope.selected.head={}
                 $scope.selected.subHead={}
                $scope.selected.marks={
                    examMarks:"",
                    obtainedMarks:[]
                }

                 $scope.selection.courses=[];
                 $scope.selection.distributedMarks=[];
                $scope.selection.subHeads = [];
                $scope.flag = {
                    hideProgramSemesterCourse: false,
                    disableSaveButton: true,
                }
            }


            $scope.whenSemesterIsSelected=function(){
                getCourses();
                getBatch();


                $scope.selected.batch={};
                $scope.selected.course={}
                $scope.selected.head={}
                $scope.selected.subHead={}
                $scope.selected.marks={
                    examMarks:"",
                    obtainedMarks:[]
                }


                $scope.selection.distributedMarks=[];
                $scope.selection.subHeads = [];
                $scope.flag = {
                    hideProgramSemesterCourse: false,
                    disableSaveButton: true
                }
            }


            $scope.whenCourseIsSelected=function(){
                getIdAndHeadsOfADistribution();
                
                
                $scope.selected.head={}
                $scope.selected.subHead={}
                $scope.selected.marks={
                    examMarks:"",
                    obtainedMarks:[]
                }
                $scope.selection.subHeads = [];
            }



            
            $scope.whenHeadIsSelected = function(){
                getSubHeadOfAHead();



                $scope.selected.subHead={}
                $scope.selected.marks={
                    examMarks:"",
                    obtainedMarks:[]
                }
                
                $scope.flag = {
                    hideProgramSemesterCourse: false,
                    disableSaveButton: true,
                    isMarksAlreadyDistributed: $scope.flag.isMarksAlreadyDistributed
                }
            }


            $scope.whenSubHeadIsSelected = function () {
                $scope.flag = {
                    hideProgramSemesterCourse: false,
                    disableSaveButton: true,
                    isMarksAlreadyDistributed: $scope.flag.isMarksAlreadyDistributed
                }
                checkIfMarksIsALreadyGivenForSubHead();
                $scope.selected.marks={
                    examMarks:"",
                    obtainedMarks:[]
                }
                
                
                $scope.flag.hideProgramSemesterCourse=true;
            }


            var createObjectForStoringMarks=function(){
                for(var i=0;i<$scope.selection.students.length;i++){
                    $scope.selected.marks.obtainedMarks[i]={
                        id:$scope.selection.students[i].id,
                        marksIndividual:""
                    }
                }
            }

            $scope.whenMarksAreGiven=function () {
                checkWheatherToDisableSaveButton();
            }

            var checkWheatherToDisableSaveButton=function () {
                var temp=checkIfAllMarksAreGiven();
                if(temp==false || $scope.selected.marks.examMarks==""){
                    $scope.flag.disableSaveButton=true;
                }
                else{
                    $scope.flag.disableSaveButton=false;
                }
            }

            var checkIfAllMarksAreGiven=function () {
                for(var i=0;i<$scope.selected.marks.obtainedMarks.length;i++){
                    if($scope.selected.marks.obtainedMarks[i].marksIndividual=="" || $scope.selected.marks.obtainedMarks[i].marksIndividual==null)
                        return false;
                }
                return true;
            }


            $scope.cancel=function(){
                clearData();
            }

            $scope.save=function(){
                processMarksToSave();
                console.log(GiveMarks);
                saveGivenMarks();
            }

            var processMarksToSave=function () {
                GiveMarks.programId=$scope.selected.program.id;
                GiveMarks.semesterId = $scope.selected.semester.id;
                GiveMarks.batchId = $scope.selected.batch.id;
                GiveMarks.courseId=$scope.selected.course.id;
                GiveMarks.subHeadId=$scope.selected.subHead.id;
                GiveMarks.examMarks = $scope.selected.marks.examMarks;
                GiveMarks.marksDistributionId = $scope.selected.distributedMarks.id;
                for(var i=0;i<$scope.selected.marks.obtainedMarks.length;i++){
                    GiveMarks.marks[i] = {
                        studentId:$scope.selected.marks.obtainedMarks[i].id,
                        obtainedMarks:$scope.selected.marks.obtainedMarks[i].marksIndividual,
                    }
                }
            }

            var inatializeThePage=function(){
                getPrograms();
            }



            var checkIfMarksDistributedCallBack=function(){
                if ($scope.selection.distributedMarks.length > 0) {
                    $scope.flag.isMarksAlreadyDistributed = true;
                }
                else {
                    $scope.flag.isMarksAlreadyDistributed = false;
                }
            }

            var checkIfMarksDistributed = function (checkIfMarksDistributedCallBack) {
                checkIfMarksDistributedCallBack();
            }
            

            var clearData = function () {
                GiveMarks = {
                    programId: "",
                    semesterId: "",
                    batchId: "",
                    courseId: "",
                    subHeadId: "",
                    examMarks: "",
                    marksDistributionId: "",
                    marks: []
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
                    distributedMarks: {
                    },
                    subHead: {

                    },
                    marks: {
                        examMarks: "",
                        obtainedMarks: []
                    }
                }

                $scope.selection = {
                    programs:$scope.selection.programs,
                    semesters: [],
                    courses: [],
                    distributedMarks: [],
                    subHeads: [],
                    students: [

                    ]
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
                UtilityService.getCourses($scope.selected.program.id, $scope.selected.semester.id, $scope.selected.batch.id, $scope.selected.head.id)
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

            var checkIfMarksIsALreadyGivenForSubHead = function () {
                console.log("sssss");
                ResultService.checkIfMarksIsGiven($scope.selected.program.id, $scope.selected.semester.id, $scope.selected.batch.id, $scope.selected.course.id, $scope.selected.distributedMarks.head.id, $scope.selected.subHead.id)
                    .then(
			               function (d) {
			                   if (d.Status == "OK") {
                                   if(d.Data==true)
                                       $scope.flag.isMarksGiven = true;
                                   else if (d.Data == false) {
                                       $scope.flag.isMarksGiven = false;
                                       createObjectForStoringMarks();
                                   }
                                       
                                   
			                   }
			                   else if (d.Status == "ERROR") {
			                       showNotification(d.Message, d.Status);
			                   }
			               },
    				        function (errResponse) {
    				            showNotification('Error While Fetching Sub Heads', 'ERROR');
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

            var saveGivenMarks = function () {
                ResultService.saveGivenMarks(GiveMarks)
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














