(
    function(){
        angular.module("examinationManagement_module").controller("viewOrCreateHead_controller", ["$scope", "Result_Service", "Utility_Service", function ($scope, ResultService, UtilityService) {
            $scope.name="Ishmnam";
            var AddHead={
                name:""
            }
            $scope.selected={
                headName:""
            }
            $scope.selection={
                heads:[]
            }
            $scope.flag = {
                isCreateHeadClicked:false,
                ifHeadAlreadyExist:false,
                disableSubmitButton:true
            }
            $scope.message = {
                content: "",
                color:""
            }

            $scope. whenCreateHeadIsClicked = function () {
                $scope.flag.isCreateHeadClicked = true;
            }

            $scope.whenCancelCreateHeadIsClicked = function () {
                $scope.flag.isCreateHeadClicked = false;
            }
            $scope.checkIfHeadExist = function () {
                if($scope.selected.headName===undefined)
                    return;
                var ifExist;
                for(var i=0;i<$scope.selection.heads.length;i++){                   
                    var currentHeadLowerCased=$scope.selection.heads[i].name.toLowerCase();
                    var inputLowerCased=$scope.selected.headName.toLowerCase();                    
                    if(currentHeadLowerCased==inputLowerCased)
                        ifExist=true;
                }
                if(ifExist){
                    $scope.flag.ifHeadAlreadyExist=true;
                    $scope.flag.disableSubmitButton=true;
                }
                else if(!ifExist){
                    $scope.flag.ifHeadAlreadyExist=false;
                    $scope.flag.disableSubmitButton=false;
                }
            }


            $scope.cancel=function(){              
                clearData();
            }


            $scope.saveHead=function(){
                AddHead.name = $scope.selected.headName;
                addHead(AddHead);
            }

            var clearData = function () {
                $scope.selected = {
                    headName: ""
                }

                $scope.flag = {
                    isCreateHeadClicked: false,
                    ifHeadAlreadyExist: false,
                    disableSubmitButton: true
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

            var getHeads = function () {
                ResultService.getAllHeads()
                    .then(
			               function (d) {
			                   if (d.Status == "OK") {
			                       $scope.selection.heads = d.Data.heads;

			                   }
			                   else if (d.Status == "ERROR") {
			                       showNotification(d.Message,d.Status);
			                   }

			               },
    				        function (errResponse) {
    				            console.error('Error While Fetching Heads','ERROR');
    				        }
	                );
            }

            var addHead = function (head) {
                ResultService.addHead(head)
	              .then(
                       function (d) {
                           if (d.Status == "OK") {
                               getHeads();
                               showNotification(d.Message, d.Status);
                           }
                           else if (d.Status == "ERROR") {
                               showNotification(d.Message,d.Status);
                           }
                       },
			            function (errResponse) {
			                showNotification('Error while Adding Head', 'ERROR');
			            });

                clearData();
            }


            var inatializeThePagfunction=function(){
                getHeads();

                if ($scope.selection.heads.length != 0)
                    $scope.selected.head = {}
            }
            

            inatializeThePagfunction();
        }])

    }()

)


