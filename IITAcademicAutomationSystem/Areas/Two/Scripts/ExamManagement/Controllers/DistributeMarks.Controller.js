(
    function(){
        angular.module("examinationManagement_module").controller("distributeMarks_controller", ["$scope", "Result_Service", "Utility_Service", function ($scope, ResultService, UtilityService) {
            $scope.name="Ishmnam";

            var AddMarksDistribution={
                programId:"",
                semesterId:"",
                batchId:"",
                courseId:"",
                distribution:[]
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
                distribution:{
                    number:0,
                    head:[]
                }
            }

            $scope.selection={
                programs:[],
                semesters:[],
                courses:[],
                distributedMarks:[],
                heads:[],
                isVisibleToStudent:["Yes","No"],
                avarageOrBestCount:["Avarage","Best Count"]
            }

            $scope.flag={
                hideProgramSemesterCourse:false,
                isWeightIsDistributedInHundred:false,
                disableSaveButton:true,
                isSameHeadSelectedMultipleTimes:{}
            }

            $scope.message = {
                content: "",
                color: ""
            }          

            $scope.whenProgramIsSelected=function() {
                getSemesters();

                $scope.selected.semester = {};
                $scope.selected.batch = {};
                $scope.selected.course = {}
                $scope.selected.distribution = {
                                        number: "",
                                        head: []
                                    };
                $scope.selection.courses = [];

                $scope.flag={
                    hideProgramSemesterCourse: false,
                    isWeightIsDistributedInHundred: false,
                    disableSaveButton: true,
                    isSameHeadSelectedMultipleTimes: {}
                }

            }

            $scope.whenSemesterIsSelected=function(){
                getCourses();
                getBatch();
                
                $scope.selected.course = {}
                $scope.selected.distribution = {
                    number: "",
                    head: []
                };
                $scope.selection.courses = [];

                $scope.flag = {
                    hideProgramSemesterCourse: false,
                    isWeightIsDistributedInHundred: false,
                    disableSaveButton: true,
                    isSameHeadSelectedMultipleTimes: {}
                }
            }

            $scope.whenCourseIsSelected=function(){
                checkIfMarksAlreadyDistributed();

                $scope.selected.distribution = {
                    number: "",
                    head: []
                };

                $scope.flag = {
                    hideProgramSemesterCourse: false,
                    isWeightIsDistributedInHundred: false,
                    disableSaveButton: true,
                    isSameHeadSelectedMultipleTimes: {}
                }
            }



            var createFormationWithSelectedForMarksDistribution=function(){
                $scope.selected.distribution.head=[];

                for(var i=0;i<$scope.selected.distribution.number;i++){
                        $scope.selected.distribution.head[i]={
                            headId:"",
                            weight:"",
                            isVisibleToStudent:$scope.selection.isVisibleToStudent[0],//0 index ontains "Yes"
                            avarageOrBestCount:$scope.selection.avarageOrBestCount[0]//0 index contains "Avarage"
                        }
                }
            }


            $scope.whenNumberOfHeadIsSelected=function(){
                $scope.flag.isNumberOfHeadSelected=true;
                $scope.flag.hideProgramSemesterCourse=true;
                getAvailableHeads();
                inatializeAllMultipleHeadSelectedErrorToFalse();
                $scope.checkIfHeadAndWeightAreDistributed();
                createFormationWithSelectedForMarksDistribution();
            }



            $scope.whenHeadIsSelected = function(){

                $scope.checkIfHeadAndWeightAreDistributed();
                checkIfSameHeadIsSelectedMultipleTimes();
            }



            $scope.getTimes=function(n){
                return new Array(n);
            }




            $scope.checkIfHeadAndWeightAreDistributed=function(){
                var headFlag=checkIfALlHeadsAreSelected();
                var weightGlag=checkIfWeightAreDistributedInHundred();
                if(headFlag && weightGlag){
                    $scope.flag.disableSaveButton=false;
                }
                else
                    $scope.flag.disableSaveButton=true;
            }


            var checkIfALlHeadsAreSelected=function(){
                var numberOfSelectedHeads=0;
                for(var i=0;i<$scope.selected.distribution.number;i++){

                    if($scope.selected.distribution.head[i]==undefined)
                        continue;
                    if($scope.selected.distribution.head[i].headId==undefined)
                        continue;
                    if($scope.selected.distribution.head[i].headId!="")
                        numberOfSelectedHeads++;

                    console.log(numberOfSelectedHeads);
                }
                if(numberOfSelectedHeads==$scope.selected.distribution.number)
                    return true;
                else
                    return false;
            }

            var checkIfWeightAreDistributedInHundred=function(){
                var totalWeight=0;
                for(var i=0;i<$scope.selected.distribution.number;i++){
                    if($scope.selected.distribution.head[i]==undefined)
                        continue;
                    if($scope.selected.distribution.head[i].weight==undefined)
                        continue;
                    if($scope.selected.distribution.head[i].weight!="")
                        totalWeight=totalWeight + $scope.selected.distribution.head[i].weight ;
                }
                if(totalWeight==100) {
                    $scope.flag.isWeightIsDistributedInHundred=true;
                    return true;
                }
                else{
                    $scope.flag.isWeightIsDistributedInHundred=false;
                    return false;
                }
            }



            var checkIfSameHeadIsSelectedMultipleTimes=function(){
                inatializeAllMultipleHeadSelectedErrorToFalse();
                var flag=false;
                var index=[]
                for(var i=0;i<$scope.selected.distribution.number;i++){
                    if($scope.selected.distribution.head[i].headId==undefined || $scope.selected.distribution.head[i].headId==""){
                        // console.log("aal1")
                        continue;
                    }
                    for(var j=i+1;j<$scope.selected.distribution.head.length;j++){
                        if($scope.selected.distribution.head[j].headId==undefined || $scope.selected.distribution.head[j].headId==""){
                            // console.log("aal2")
                            continue;
                        }
                        // console.log("Tob re tob")
                        if($scope.selected.distribution.head[i].headId==$scope.selected.distribution.head[j].headId){
                            //flag=true;
                            $scope.flag.isSameHeadSelectedMultipleTimes[i]=true;
                            $scope.flag.isSameHeadSelectedMultipleTimes[j]=true;
                            $scope.flag.disableSaveButton=true;
                            //index.push(i);
                        }
                    }
                }
            }

            var inatializeAllMultipleHeadSelectedErrorToFalse=function(){
                for(var i=0;i<$scope.selected.distribution.number;i++){
                    $scope.flag.isSameHeadSelectedMultipleTimes[i]=false;
                }
            }


            $scope.cancelDistribution=function(){
                clearData();
            }


            $scope.save = function () {
                processMarksDistriution();
                console.log("Final Submit");
                console.log(AddMarksDistribution);
                saveDistributedMarks();
                
                
            }

            var processMarksDistriution = function () {
                AddMarksDistribution.programId = $scope.selected.program.id;
                AddMarksDistribution.semesterId = $scope.selected.semester.id;
                AddMarksDistribution.batchId = $scope.selected.batch.id;
                AddMarksDistribution.courseId = $scope.selected.course.id;
                AddMarksDistribution.distribution = [];


                for (var i = 0; i < $scope.selected.distribution.number; i++) {
                    if ($scope.selected.distribution.head[i].isVisibleToStudent == "Yes") {
                        $scope.selected.distribution.head[i].isVisibleToStudent = true;
                    }

                    else if ($scope.selected.distribution.head[i].isVisibleToStudent == "No") {
                        $scope.selected.distribution.head[i].isVisibleToStudent = false;
                    }

                    if ($scope.selected.distribution.head[i].avarageOrBestCount == "Avarage") {
                        $scope.selected.distribution.head[i].avarageOrBestCount = true;
                    }

                    else if ($scope.selected.distribution.head[i].avarageOrBestCount == "Best Count") {
                        $scope.selected.distribution.head[i].avarageOrBestCount = false;
                    }

                    AddMarksDistribution.distribution[i] = $scope.selected.distribution.head[i];
                }
            }

            var clearData = function () {
                var AddMarksDistribution = {
                    programId: "",
                    semesterId: "",
                    batchId: "",
                    courseId: "",
                    distribution: []
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
                        number: 0,
                        head: []
                    }
                }

                $scope.selection = {
                    programs: $scope.selection.programs,
                    semesters: [],
                    courses: [],
                    distributedMarks: [],
                    heads: [],
                    isVisibleToStudent: ["Yes", "No"],
                    avarageOrBestCount: ["Avarage", "Best Count"]
                }

                $scope.flag = {
                    hideProgramSemesterCourse: false,
                    isNumberOfHeadSelected: false,
                    isWeightIsDistributedInHundred: false,
                    disableSaveButton: true,
                    isSameHeadSelectedMultipleTimes: {}
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

            var saveDistributedMarks = function () {
                ResultService.saveMarksDistribution(AddMarksDistribution)
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
                UtilityService.getAllHeads()
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

            var checkIfMarksAlreadyDistributed = function () {
                ResultService.checkIfMarksAlreadyDistributed($scope.selected.program.id, $scope.selected.semester.id, $scope.selected.batch.id, $scope.selected.course.id)
                    .then(
			               function (d) {
			                   if (d.Status == "OK") {
			                       $scope.flag.isMarksAlreadyDistributed = d.Data;
			                       if ($scope.flag.isMarksAlreadyDistributed == false) {
			                           createFormationWithSelectedForMarksDistribution();
			                       }
			                   }
			                   else if (d.Status == "ERROR", d.Status) {
			                       showNotification(d.Message);
			                   }
			               },
    				        function (errResponse) {
    				            showNotification('Error While Checking','ERROR');
    				        }
	                );
            }

            var inatializeThePage=function(){

                getPrograms();

            }

           
            inatializeThePage();




        }])

    }()

)












