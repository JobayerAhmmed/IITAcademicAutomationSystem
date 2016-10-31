angular.module("academicCalendarManagement_module").factory("AcademicCalendar_Service", ["$http", '$q', function ($http, $q) {
    return {
        UploadFile: function (file, information) {
            console.log(information);
            var formData = new FormData();
            formData.append("file", file);
            formData.append("programId", information.programId);
            formData.append("semesterId", information.semesterId);
            formData.append("batchId", information.batchId);

            var defer = $q.defer();
            $http.post("/Two/AcademicCalendarManagement/AddAcademicCalendar", formData,
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

        checkIfAcademicCalendarAlreadyUploaded: function (programId, semesterId) {
            return $http.get('/Two/AcademicCalendarManagement/checkIfAcademicCalendarUploaded?programId=' + programId + "&semesterId=" + semesterId)
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },

        getAcademicCalendars_tp: function (programId, semesterId) {
            return $http.get('/Two/AcademicCalendarManagement/getAcademicCalendars_tp?programId=' + programId + "&semesterId=" + semesterId)
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },

        getAcademicCalendars_s: function () {
            return $http.get('/Two/AcademicCalendarManagement/getAcademicCalendars_s')
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },

        saveEditedAcademicCalendar: function (academicCalendarInfo) {
            return $http.post('/AcademicCalendarManagement/editAcademicCalendar', academicCalendarInfo)
                .then(

                        function (response) {
                            return response.data;
                        },
                        function (errResponse) {
                            return $q.reject(errResponse);
                        }
                );
        },
        deleteAcademicCalendar: function (academicCalendarId) {
            return $http.post('/AcademicCalendarManagement/deleteAcademicCalendar?academicCalendarId='+academicCalendarId)
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