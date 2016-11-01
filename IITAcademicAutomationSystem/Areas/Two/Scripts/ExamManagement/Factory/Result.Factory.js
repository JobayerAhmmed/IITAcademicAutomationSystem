angular.module("examinationManagement_module").factory("Result_Service", ["$http", '$q', function ($http, $q) {
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
        getIdAndHeadsOfADistribution: function (programId,semesterId,batchId,courseId) {
            return $http.get('/ResultManagement/getDistributedMarksPartial?programId=' + programId + "&semesterId=" + semesterId + "&batchId=" + batchId + "&courseId=" + courseId)
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },

        addHead: function (head) {
            return $http.post('/ResultManagement/createHead', head)
                .then(

                        function (response) {
                            return response.data;
                        },
                        function (errResponse) {
                            return $q.reject(errResponse);
                        }
                );
        },

        addSubHead: function (subHead) {
            return $http.post('/ResultManagement/createSubHead', subHead)
                .then(

                        function (response) {
                            return response.data;
                        },
                        function (errResponse) {
                            return $q.reject(errResponse);
                        }
                );
        },

        saveMarksDistribution: function (distributedMarks) {
            return $http.post('/ResultManagement/distributeMarks', distributedMarks)
                .then(

                        function (response) {
                            return response.data;
                        },
                        function (errResponse) {
                            return $q.reject(errResponse);
                        }
                );
        },

        getDistributedMarks: function (programId, semesterId, batchId, courseId) {
            var deferred = $q.defer();
            $http.get('/ResultManagement/getDistributedMarks?programId=' + programId + "&semesterId=" + semesterId + "&batchId=" + batchId + "&courseId=" + courseId)
                    .then(function (response) {
                                deferred.resolve(response.data);
                            },
                            function (errResponse) {
                                deferred.reject(errResponse);
                            }
                    );
            return deferred.promise;
        },

        checkIfMarksAlreadyDistributed: function (programId, semesterId, batchId, courseId) {
            return $http.get('/ResultManagement/checkIfMarksIsDistributedForACourse?programId=' + programId + "&semesterId=" + semesterId + "&batchId=" + batchId + "&courseId=" + courseId)
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },
                
        saveEditedMarksDistribution: function (distributedMarks) {
            return $http.post('/ResultManagement/editDistributedMarks', distributedMarks)
                .then(

                        function (response) {
                            return response.data;
                        },
                        function (errResponse) {
                            return $q.reject(errResponse);
                        }
                );
        },        

        saveGivenMarks: function (marks) {
            return $http.post('/ResultManagement/giveMarks', marks)
                .then(

                        function (response) {
                            return response.data;
                        },
                        function (errResponse) {
                            return $q.reject(errResponse);
                        }
                );
        },

        checkIfMarksIsGiven: function (programId, semesterId, batchId, courseId,headId,subHeaId) {
            return $http.get('/ResultManagement/checkIfMarksIsGiven?programId=' + programId + "&semesterId=" + semesterId + "&batchId=" + batchId + "&courseId=" + courseId + "&headId=" + headId + "&subHeadId=" + subHeaId)
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                );
        },

        getGivenMarksOfAllStudentOfACourse_t: function (programId, semesterId, batchId, courseId) {
            return $http.get('/ResultManagement/getGivenMarks_t?programId=' + programId + "&semesterId=" + semesterId + "&batchId=" + batchId + "&courseId=" + courseId)
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                );
        },

        getGivenMarksOfAllStudentOfACourse_s: function (courseId) {
            return $http.get('/ResultManagement/getGivenMarks_s?courseId=' + courseId)
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                );
        },

        getGivenMarksOfAllStudentForEditing: function (programId, semesterId, batchId, courseId, headId, subHeadId) {
            return $http.get('/ResultManagement/getGivenMarksForEditing?programId=' + programId + "&semesterId=" + semesterId + "&batchId=" + batchId + "&courseId=" + courseId + "&headId=" + headId + "&subHeadId=" + subHeadId)
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                );
        },

        saveEditedMarks: function (marks) {
            return $http.post('/ResultManagement/saveEditedMarks', marks)
                .then(
                        
                        function (response) {
                            return response.data;
                        },
                        function (errResponse) {
                            return $q.reject(errResponse);
                        }
                );
        },
        getCourseMarksInfo: function (programId, semesterId, batchId, courseId) {
            return $http.get('/ResultManagement/getMarksGivingOfHeadsInfo?programId=' + programId + "&semesterId=" + semesterId + "&batchId=" + batchId + "&courseId=" + courseId)
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                );
        },
        finallySubmitOfACourse: function (programId, semesterId, batchId, courseId) {
            return $http.post('/ResultManagement/submitFinally?programId=' + programId + "&semesterId=" + semesterId + "&batchId=" + batchId + "&courseId=" + courseId)
                .then(
                        function (response) {
                            return response.data;
                        },
                        function (errResponse) {
                            return $q.reject(errResponse);
                        }
                );
        },
        getFinalSubmissionInfoOfAllCourses: function (programId, semesterId, batchId) {
            return $http.get('/ResultManagement/getFinalSubmissionInfoOfAllCourses?programId=' + programId + "&semesterId=" + semesterId + "&batchId=" + batchId)
                .then(
                        function (response) {
                            return response.data;
                        },
                        function (errResponse) {
                            return $q.reject(errResponse);
                        }
                );
        },
        getResultOfAllStudents: function (programId, semesterId, batchId) {
            return $http.get('/ResultManagement/getResultOfASemester?programId=' + programId + "&semesterId=" + semesterId + "&batchId=" + batchId)
                .then(
                        function (response) {
                            return response.data;
                        },
                        function (errResponse) {
                            return $q.reject(errResponse);
                        }
                );
        },
        getPassFailInfoOfASemester: function (programId, semesterId, batchId) {
            return $http.get('/ResultManagement/getPassFailInfoOfASemester?programId=' + programId + "&semesterId=" + semesterId + "&batchId=" + batchId)
                .then(
                        function (response) {
                            return response.data;
                        },
                        function (errResponse) {
                            return $q.reject(errResponse);
                        }
                );
        }
    };

}])