angular.module("examinationManagement").directive("programSemesterCourse", [function () {
    return {
        restrict: 'EAC',
        templateUrl: '~/Areas/Two/Directive/ProgramSemesterCourseDirective.html',
        replace: true,
        scope: {
            selected: "=",
            selection: "=",
            whenProgramIsSelected: "&",
            whenSemesterIsSelected: "&",
            whenCourseIsSelected: "&"

        },
        transclude: true

    }

}])