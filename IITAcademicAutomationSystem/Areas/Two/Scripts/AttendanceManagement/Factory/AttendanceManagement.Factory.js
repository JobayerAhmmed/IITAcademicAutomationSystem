angular.module("attendanceManagement_module").factory("Attendance_Service", ["$http", '$q', function ($http, $q) {
    return {
        getLastClassNumber: function (programId, semesterId, batchId, courseId) {
            return $http.get('/AttendanceManagement/getLastClassNumber?programId=' + programId + "&semesterId=" + semesterId + "&batchId=" + batchId + "&courseId=" + courseId)
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },
        saveAttendance: function (attendance) {
            return $http.post('/AttendanceManagement/saveAttendance', attendance)
                .then(
                        function (response) {
                            return response.data;
                        },
                        function (errResponse) {
                            return $q.reject(errResponse);
                        }
                );
        },
        getAttendance: function (programId, semesterId, batchId, courseId,classNo) {
            return $http.get('/AttendanceManagement/getAttendancesForEditing?programId=' + programId + "&semesterId=" + semesterId + "&batchId=" + batchId + "&courseId=" + courseId + "&classNo=" + classNo)
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },
        getClassNumberAndDate: function (programId, semesterId, batchId, courseId, classId) {
            return $http.get('/AttendanceManagement/getClassesNumbersAndDates?programId=' + programId + "&semesterId=" + semesterId + "&batchId=" + batchId + "&courseId=" + courseId + "&classId=" + classId)
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },
        saveEditedAttendance: function (attendance) {
            return $http.post('/AttendanceManagement/saveEditedAttendance', attendance)
                .then(
                        function (response) {
                            return response.data;
                        },
                        function (errResponse) {
                            return $q.reject(errResponse);
                        }
                );
        },
        getAttendanceCourseWise: function (programId, semesterId, batchId, courseId) {
            return $http.get('/AttendanceManagement/getAttendancesCourseWise?programId=' + programId + "&semesterId=" + semesterId + "&batchId=" + batchId + "&courseId=" + courseId)
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },
        getAttendanceIndividual: function () {
            return $http.get('/AttendanceManagement/getAttendancesIndividual')
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