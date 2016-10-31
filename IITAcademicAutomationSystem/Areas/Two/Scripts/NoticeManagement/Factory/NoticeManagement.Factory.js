angular.module("noticeManagement_module").factory("Notice_Service", ["$http", '$q', function ($http, $q) {
    return {
        UploadFile: function (file, information) {
            console.log(information);
            var formData = new FormData();
            formData.append("file", file);
            formData.append("title", information.title);
            formData.append("description", information.description);
            formData.append("programId", information.programId);
            formData.append("semesterId", information.semesterId);
            formData.append("batchId", information.batchId);

            var defer = $q.defer();
            $http.post("/Two/NoticeManagement/AddNotice", formData,
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

        getNotices_tp: function (programId, semesterId) {
            return $http.get('/Two/NoticeManagement/getNotices_tp?programId=' + programId + "&semesterId=" + semesterId)
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },

        getNotices_s: function () {
            return $http.get('/Two/NoticeManagement/getNotices_s')
                    .then(
                            function (response) {
                                return response.data;
                            },
                            function (errResponse) {
                                return $q.reject(errResponse);
                            }
                    );
        },

        saveEditedNotice: function (noticeInfo) {
            return $http.post('/NoticeManagement/editNotice', noticeInfo)
                .then(

                        function (response) {
                            return response.data;
                        },
                        function (errResponse) {
                            return $q.reject(errResponse);
                        }
                );
        },
        deleteNotice: function (noticeId) {
            return $http.post('/NoticeManagement/deleteNotice?noticeId='+noticeId)
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