angular.module("routineManagement_module").factory("Routine_Service", ["$http", '$q', function ($http, $q) {
    return {
        UploadFile: function (file, information) {
            console.log(information);
            var formData = new FormData();
            formData.append("file", file);
            formData.append("programId", information.programId);
            formData.append("semesterId", information.semesterId);
            formData.append("batchId", information.batchId);

            var defer = $q.defer();
            $http.post("/Two/RoutineManagement/AddRoutine", formData,
            {
                withCredentials: true,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            })
            .success(function (d) {
                defer.resolve(d);
            })
            .error(function () {
                defer.reject("File Upload Failed!");
            });
            return defer.promise;
        },

        checkIfRoutineAlreadyUploaded: function (programId, semesterId) {
            return $http.get('/Two/RoutineManagement/checkIfRoutineUploaded?programId=' + programId + "&semesterId=" + semesterId)
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },

        getRoutines_tp: function (programId, semesterId) {
            return $http.get('/Two/RoutineManagement/getRoutines_tp?programId=' + programId + "&semesterId=" + semesterId)
                    .then(
                            function (response) {
                                console.log(response);
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },

        getRoutines_s: function () {
            return $http.get('/Two/RoutineManagement/getRoutines_s')
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },

        saveEditedRoutine: function (routineInfo) {
            return $http.post('/RoutineManagement/editRoutine', routineInfo)
                .then(

                        function (response) {
                            return response.data;
                        },
                        function (errResponse) {
                            return $q.reject(errResponse);
                        }
                );
        },
        deleteRoutine: function (routineId) {
            return $http.post('/RoutineManagement/deleteRoutine?routineId='+routineId)
                .then(

                        function (response) {
                            return response.data;
                        },
                        function (errResponse) {
                            return $q.reject(errResponse);
                        }
                );
        },

    };

}])