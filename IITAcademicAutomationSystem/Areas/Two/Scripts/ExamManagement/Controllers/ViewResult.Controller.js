(
    function(){
        angular.module("examinationManagement_module").controller("viewResult_controller", ["$scope", "Result_Service", "Utility_Service", "$window", function ($scope, ResultService, UtilityService,$window) {
            $scope.name="Ishmnam";

            $scope.selected = {
                program: {
                },
                semester: {
                },
                batch: {
                },
                
            }

           
            

            $scope.selection = {
                programs: [],
                semesters: [],
                courses: [],
                resultInfo:{

                }
            }
            $scope.message = {
                content: "",
                color: ""
            }
            $scope.whenProgramIsSelected = function () {
                if ($scope.selected.program.id)
                    getSemesters();

                $scope.selected.semester = {};
                $scope.selected.batch = {};
                $scope.selected.course = {};

            }


            $scope.whenSemesterIsSelected = function () {
                if ($scope.selected.program.id && $scope.selected.semester.id) {
                   
                    getBatch();
                }
            }

            

            $scope.printIt = function(){
                var table = document.getElementById('printArea').innerHTML;
                var myWindow = $window.open('', '', 'width=800, height=600');
                myWindow.document.write(table);
                myWindow.print();
            };

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
                              console.log(d);
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
                                  getResult();
                                  console.log(d.Data)
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

            var getResult = function () {
                ResultService.getResultOfAllStudents($scope.selected.program.id, $scope.selected.semester.id, $scope.selected.batch.id)
                   .then(
                          function (d) {
                              if (d.Status == "OK") {
                                  $scope.selection.resultInfo = d.Data;
                                  console.log(d.Data)
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
            

           var getResultOfABatch=function () {
               return{
                   program:"BSSE",
                   semester:"8th",
                   batch:"5th",
                   courses:["CSE-801","CSE-802","CSE-803","CSE-808","CSE-809","CSE-8031","CSE-835"],
                   results:[
                       {
                           studentName:"Ishmam 1",
                           classRoll:"0523",
                           examRoll:"001",
                           result:[
                               {
                                   courseName:"CSE-801",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-802",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-803",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-808",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-809",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-831",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-835",
                                   GPA:-1
                               }
                           ],

                           GPA:3.2,
                           CGPA:3.61
                       },
                       {
                           studentName:"Ishmam 2",
                           classRoll:"0523",
                           examRoll:"001",
                           result:[
                               {
                                   courseName:"CSE-801",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-802",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-803",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-808",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-809",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-831",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-835",
                                   GPA:-1
                               }
                           ],

                           GPA:3.2,
                           CGPA:3.61
                       },
                       {
                           studentName:"Ishmam 3",
                           classRoll:"0523",
                           examRoll:"001",
                           result:[
                               {
                                   courseName:"CSE-801",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-802",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-803",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-808",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-809",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-831",
                                   GPA:-1
                               },
                               {
                                   courseName:"CSE-835",
                                   GPA:3.5
                               }
                           ],

                           GPA:3.2,
                           CGPA:3.61
                       },
                       {
                           studentName:"Ishmam 4",
                           classRoll:"0523",
                           examRoll:"001",
                           result:[
                               {
                                   courseName:"CSE-801",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-802",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-803",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-808",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-809",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-831",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-835",
                                   GPA:-1
                               }
                           ],

                           GPA:3.2,
                           CGPA:3.61
                       },
                       {
                           studentName:"Ishmam 5",
                           classRoll:"0523",
                           examRoll:"001",
                           result:[
                               {
                                   courseName:"CSE-801",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-802",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-803",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-808",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-809",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-831",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-835",
                                   GPA:-1
                               }
                           ],

                           GPA:3.2,
                           CGPA:3.61
                       },
                       {
                           studentName:"Ishmam 6",
                           classRoll:"0523",
                           examRoll:"001",
                           result:[
                               {
                                   courseName:"CSE-801",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-802",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-803",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-808",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-809",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-831",
                                   GPA:-1
                               },
                               {
                                   courseName:"CSE-835",
                                   GPA:3.5
                               }
                           ],

                           GPA:3.2,
                           CGPA:3.61
                       },
                       {
                           studentName:"Ishmam 7",
                           classRoll:"0523",
                           examRoll:"001",
                           result:[
                               {
                                   courseName:"CSE-801",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-802",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-803",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-808",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-809",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-831",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-835",
                                   GPA:-1
                               }
                           ],

                           GPA:3.2,
                           CGPA:3.61
                       },
                       {
                           studentName:"Ishmam 8",
                           classRoll:"0523",
                           examRoll:"001",
                           result:[
                               {
                                   courseName:"CSE-801",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-802",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-803",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-808",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-809",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-831",
                                   GPA:3.5
                               },
                               {
                                   courseName:"CSE-835",
                                   GPA:-1
                               }
                           ],

                           GPA:3.2,
                           CGPA:3.61
                       }
                   ]
               }
           }


            var inatializeThePagfunction=function(){
                getPrograms();
            }


            inatializeThePagfunction();
        }])

    }()

)





