angular.module("utility_module").factory("Utility_Service", ["$http", '$q', function ($http, $q) {
    return {

        getAllHeads: function () {
            return $http.get('/ResultManagement/getAllHeads')
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },

        getSubHead: function (headId) {
            return $http.get('/ResultManagement/getSubHeads?headId=' + headId)
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },

        //............move it to result factory

        getAllPrograms: function () {
            return $http.get('/Utility/getAllPrograms')
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },

        getPrograms: function () {
            return $http.get('/Utility/getProgramsOfATeacher')
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },

        /*getPrograms: function () {
            var deferred = $q.defer();
            return $http.get('/Utility/getProgramsOfATeacher')
                    .then(function (response) {
                        console.log(response);
                        deferred.resolve(response.data);
                    },
                            function (errResponse) {
                                deferred.reject(errResponse);
                            }
                    );
            return deferred.promise.data;
        },*/

        getSemestersOfAProgram: function (programId) {
            return $http.get('/Utility/getSemestersOfAProgram?programId=' + programId)
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },

        getSemesters: function (programId) {
            return $http.get('/Utility/getSemestersOfATeacherOfAProgramr?programId=' + programId)
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },
        getBatch: function (programId, semesterId) {
            return $http.get('/Utility/getBatchOfASemesterOfAProgram?programId=' + programId + "&semesterId=" + semesterId)
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },

        getBatchesOfAProgram: function (programId) {
            return $http.get('/Utility/getBatches?programId=' + programId)
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },
        getCourses: function (programId, semesterId) {
            return $http.get('/Utility/getCoursesOfATeacherOfASemesterOfAProgram?programId=' + programId + "&semesterId=" + semesterId)
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },

        getProgramSemesterBatchOfAStuent: function () {
            return $http.get('/Utility/getProgramSemesterBatchOfLoggedInStudent')

                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },
        
        getAllStudentOfACourse: function (programId, semesterId, batchId, courseId) {
            return $http.get('/Utility/getStudentsOfACOurse?programId=' + programId + "&semesterId=" + semesterId + "&batchId=" + batchId + "&courseId=" + courseId)
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },
        getAllCoursesOfAStudent: function () {
            return $http.get('/Utility/getAllCoursesOfAStudent')
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