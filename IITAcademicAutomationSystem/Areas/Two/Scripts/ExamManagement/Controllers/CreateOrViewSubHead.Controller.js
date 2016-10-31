(
    function(){
        angular.module("examinationManagement_module").controller("createOrViewSubHead_controller", ["$scope", "Result_Service", "Utility_Service", function ($scope, ResultService, UtilityService) {
            $scope.name="Ishmnam";
            var AddSubHead={
                headId:"",
                name:""
            }
            $scope.selected={
                head:{
                },
                subHeadName:""                
            }
            $scope.selection={                
                heads:[],
                subHeads:[]
            }
            $scope.message = {
                content: "",
                color: ""
            }
            $scope.flag = {
                isCreateSubHeadClicked: false,
                ifSubHeadAlreadyExist:false,
                disableSubmitButton:true
            }

            $scope.whenCreateSubHeadIsClicked = function () {
                $scope.flag.isCreateSubHeadClicked = true;
            }

            


            $scope.whenHeadIsSelected = function(){
                getSubHeadOfAHead($scope.selected.head.id);
                $scope.selected.subHead={}
            }          
            $scope.checkIfSubHeadExist=function(){
                if($scope.selected.subHeadName===undefined)
                    return;
                var ifExist;
                for(var i=0;i<$scope.selection.subHeads.length;i++){
                    var currentHeadLowerCased=$scope.selection.subHeads[i].name.toLowerCase();
                    var inputLowerCased=$scope.selected.subHeadName.toLowerCase();
                    if(currentHeadLowerCased==inputLowerCased)
                        ifExist=true;
                }
                if(ifExist){
                    $scope.flag.ifSubHeadAlreadyExist=true;
                    $scope.flag.disableSubmitButton=true;
                }
                else if(!ifExist){
                    $scope.flag.ifSubHeadAlreadyExist=false;
                    $scope.flag.disableSubmitButton=false;
                }
            }


            $scope.cancel=function(){
                clearData();
            }
            $scope.saveSubHead=function(){
                AddSubHead.headId=$scope.selected.head.id;
                AddSubHead.name=$scope.selected.subHeadName;
                addSubHead(AddSubHead);
            }

            var clearData = function () {
                var AddSubHead = {
                    headId: "",
                    name: ""
                }
                $scope.selected = {
                    head: $scope.selected.head,
                    subHeadName: ""
                }
                $scope.message = {
                    content: "",
                    color: ""
                }
                $scope.flag = {
                    isCreateSubHeadClicked: false,
                    ifSubHeadAlreadyExist: false,
                    disableSubmitButton: true
                }
            }
            var showNotification = function (message, status) {
                $scope.message.content = message;

                if (status == "OK") {
                    $scope.message.color = "alert alert-success";
                }
                else if (status = "ERROR") {
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
			                       showNotification(d.Message, d.Status);
			                   }
			               },
    				        function (errResponse) {
    				            showNotification('Error While Fetching Heads',"ERROR");
    				        }
	                );
            }
            var getSubHeadOfAHead = function (headId) {
                ResultService.getSubHead(headId)
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
            var addSubHead = function (subHead) {
                ResultService.addSubHead(subHead)
	              .then(
                       function (d) {
                           if (d.Status == "OK") {
                               getSubHeadOfAHead($scope.selected.head.id);
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

            var inatializeThePagfunction=function(){
                getHeads();
            }
            inatializeThePagfunction();
        }])

    }()

)





