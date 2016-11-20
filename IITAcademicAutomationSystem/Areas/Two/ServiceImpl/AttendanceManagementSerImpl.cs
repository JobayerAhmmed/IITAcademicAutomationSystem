using IITAcademicAutomationSystem.Areas.Two.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IITAcademicAutomationSystem.Areas.Two.RequestDto;
using IITAcademicAutomationSystem.Areas.Two.Repo;
using IITAcademicAutomationSystem.Areas.Two.RepoImpl;
using IITAcademicAutomationSystem.Models;
using IITAcademicAutomationSystem.Areas.Two.ResponseDto;

namespace IITAcademicAutomationSystem.Areas.Two.ServiceImpl
{
    public class AttendanceManagementSerImpl : AttendanceManagementSerI
    {
        private AttendanceRepoI attendanceRepo=new AttendanceRepoImpl();
        private UtilitySerI utilityService = new UtilitySerImpl();

        public GetCLassesNumbersAndDatesResDto getClassesNumbersAndDates(int programId, int semesterId, int batchId, int courseId)
        {
            GetCLassesNumbersAndDatesResDto attendanceToReturn = new GetCLassesNumbersAndDatesResDto();
            try
            {
                List<Attendance> attendanceListFromRepo = attendanceRepo.getAttendance(programId, semesterId, batchId, courseId);
                attendanceListFromRepo = createUniqueClassesList(attendanceListFromRepo);
                List<GetClassesNumberAndDateIndividualResDto> attendanceList = new List<GetClassesNumberAndDateIndividualResDto>();                
                for(int i=0;i< attendanceListFromRepo.Count; i++)
                {
                    GetClassesNumberAndDateIndividualResDto tempClassNoAndDate = new GetClassesNumberAndDateIndividualResDto();
                    var attendanceTemp = attendanceListFromRepo.ElementAt(i);
                    bool ifClassExist = checkIfClassNoExistInArray(attendanceList, attendanceTemp);
                    if (!ifClassExist)
                    {
                        tempClassNoAndDate.classNo = attendanceTemp.class_no;
                        tempClassNoAndDate.date = attendanceTemp.classDate;//date is comming in correct format
                        attendanceList.Add(tempClassNoAndDate);
                    }                    
                }
                attendanceToReturn.classesNubesAndDates = attendanceList.ToArray();
                return attendanceToReturn;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        private bool checkIfClassNoExistInArray(List<GetClassesNumberAndDateIndividualResDto> array, Attendance attendance)
        {
            bool flag = false;
            for(int i=0;i< array.Count; i++)
            {
                if (array[i] == null)
                {
                    break;
                }
                else if (array[i].classNo == attendance.class_no)
                {
                    flag = true;
                    break;
                }
            }

            return flag;
        }

        private List<Attendance> createUniqueClassesList(List<Attendance> attendanceList)
        {
            List<Attendance> attendanceToReturn = new List<Attendance>();
            for(int i=0;i< attendanceList.Count; i++)
            {
                var attendancetemp = attendanceList.ElementAt(i);
                var ifExist = checkIfExist(attendanceToReturn, attendancetemp);
                if (!ifExist)
                {
                    attendanceToReturn.Add(attendancetemp);
                }
            }
            return attendanceToReturn;
        }

        private bool checkIfExist(List<Attendance> attendanceList,Attendance attendance)
        {
            bool flag = false;
            for (int i = 0; i < attendanceList.Count; i++)
            {
                if (attendanceList[i] == null)
                {
                    break;
                }
                else if (attendanceList[i].Id == attendance.Id)
                {
                    flag = true;
                    break;
                }
            }

            return flag;
        }

        public int getLastClassNumber(int programId, int semesterId, int batchId, int courseId)
        {
            try
            {
                var lastClassNo= attendanceRepo.getLastClassNumber(programId, semesterId, batchId, courseId);
                return lastClassNo;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void saveAttendance(GiveAttendanceResDto giveAttendanceResDto,string teacherId)
        {
            Attendance[] attendanceListToSave = new Attendance[giveAttendanceResDto.attendances.Length];
            try
            {
                for (int i=0;i< giveAttendanceResDto.attendances.Length; i++)
                {
                    Attendance attendance = new Attendance();
                    attendance.programId = giveAttendanceResDto.programId;
                    attendance.semesterId = giveAttendanceResDto.semesterId;
                    attendance.batchId = giveAttendanceResDto.batchId;
                    attendance.courseId = giveAttendanceResDto.courseId;
                    attendance.class_no = giveAttendanceResDto.classNo;
                    attendance.classDate = giveAttendanceResDto.classDate;
                    attendance .studentId= giveAttendanceResDto.attendances[i].studentId;
                    attendance.teacherId = teacherId;
                    attendance.is_present = giveAttendanceResDto.attendances[i].isPresent;

                    attendanceListToSave[i] = attendance;
                }

                attendanceRepo.saveAttendance(attendanceListToSave);

            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public GetAttendanceForEditing getAttendances(int programId, int semesterId, int batchId, int courseId, int classNo)
        {
            GetAttendanceForEditing responseToReturn = new GetAttendanceForEditing();

            try
            {
                List<Attendance> attendanceListFromRepo = attendanceRepo.getAttendance(programId,semesterId,batchId,courseId, classNo);
                GetStudentsResponseDto student = utilityService.getStudentsOfACOurse(programId, semesterId, batchId, courseId);

                AttendanceInfoForEditing[] attendanceList = new AttendanceInfoForEditing[attendanceListFromRepo.Count];


                for(int i=0;i< attendanceListFromRepo.Count; i++)
                {
                    AttendanceInfoForEditing attendanceIndividual = new AttendanceInfoForEditing();
                    var attedance = attendanceListFromRepo.ElementAt(i);

                    attendanceIndividual.id = attedance.Id; 
                    attendanceIndividual.classRoll = findClassRollOfAStudent(student, attedance.studentId);
                    attendanceIndividual.name = findNameOfAStudent(student, attedance.studentId);
                    attendanceIndividual.isPresent = attedance.is_present;

                    attendanceList[i] = attendanceIndividual;
                }

                responseToReturn.attendances = attendanceList;

                return responseToReturn;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        private string findNameOfAStudent(GetStudentsResponseDto student,int studentId)
        {
            string name = null;
            for(int i=0;i< student.students.Length; i++)
            {
                if (student.students[i] == null)
                    break;

                if (student.students[i].id == studentId)
                {
                    name = student.students[i].name;
                    break;
                }

            }
            return name;
        }

        private string findClassRollOfAStudent(GetStudentsResponseDto student, int studentId)
        {
            string roll = null;
            for (int i = 0; i < student.students.Length; i++)
            {
                if (student.students[i] == null)
                    break;

                if (student.students[i].id == studentId)
                {
                    roll = student.students[i].classRoll;
                    break;
                }

            }
            return roll;
        }

        public void saveEditedAttendance(EditAttendanceReqDto editAttendanceReqDto)
        {
            try
            {
                Attendance[] attendanceListToSave = new Attendance[editAttendanceReqDto.attendances.Length];

                for(int i=0;i< editAttendanceReqDto.attendances.Length; i++)
                {
                    Attendance attendance = new Attendance();
                    attendance.Id = editAttendanceReqDto.attendances.ElementAt(i).attendanceId;
                    attendance.is_present = editAttendanceReqDto.attendances.ElementAt(i).isPresent;

                    attendanceListToSave[i] = attendance;
                }

                attendanceRepo.saveEditedAttendance(attendanceListToSave);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public AttendanceHistoryResDto getAttendancesCourseWise(int programId, int semesterId, int batchId, int courseId)
        {
            AttendanceHistoryResDto attendanceHistory = new AttendanceHistoryResDto();
            try
            {
                List<Attendance> attendanceListFromRepo= attendanceRepo.getAttendance(programId, semesterId, batchId, courseId);
                GetStudentsResponseDto student = utilityService.getStudentsOfACOurse(programId, semesterId, batchId, courseId);
                GetCLassesNumbersAndDatesResDto classNumbersAndDates = getClassesNumbersAndDates(programId, semesterId, batchId, courseId);

                attendanceHistory.classNoAndDate = classNumbersAndDates.classesNubesAndDates;

                List<AllAttendanceHistoryOfAStudentResDto> allAttendanceHistoryOfAStudentList = new List<AllAttendanceHistoryOfAStudentResDto>();
                for (int i=0;i< student.students.Length; i++)
                {
                    var individualStudent = student.students[i];
                    var individualStudentAttendance = processAttendanceOfAStudent(attendanceListFromRepo, classNumbersAndDates, individualStudent);
                    allAttendanceHistoryOfAStudentList.Add(individualStudentAttendance);
                }
                attendanceHistory.attendanceHistoryAll = allAttendanceHistoryOfAStudentList.ToArray();
                return attendanceHistory;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        private AllAttendanceHistoryOfAStudentResDto processAttendanceOfAStudent(List<Attendance> attendanceList, GetCLassesNumbersAndDatesResDto classNumbersAndDates, StudentResDto student)
        {
            AllAttendanceHistoryOfAStudentResDto allAttendanceHistoryOfAStudent = new AllAttendanceHistoryOfAStudentResDto();
            List<IndividualAttendanceHistoryOfAStudentResDto> individualAttendanceHistoryOfAStudent = new List<IndividualAttendanceHistoryOfAStudentResDto>();

            allAttendanceHistoryOfAStudent.name = student.name;
            allAttendanceHistoryOfAStudent.classRoll = student.classRoll;

            for (int i=0;i< classNumbersAndDates.classesNubesAndDates.Length; i++)
            {
                var classNoAndDateTemp = classNumbersAndDates.classesNubesAndDates[i];
                for(int j=0;j< attendanceList.Count; j++)
                {
                    var attendanceTemp = attendanceList.ElementAt(j);
                    if (classNoAndDateTemp.classNo == attendanceTemp.class_no && attendanceTemp.studentId == student.id) 
                    {
                        IndividualAttendanceHistoryOfAStudentResDto attendanceHistoryIndividual = new IndividualAttendanceHistoryOfAStudentResDto();
                        attendanceHistoryIndividual.id = attendanceTemp.Id;
                        attendanceHistoryIndividual.isPresent = attendanceTemp.is_present;
                        individualAttendanceHistoryOfAStudent.Add(attendanceHistoryIndividual);
                    }
                }
            }
            allAttendanceHistoryOfAStudent.attendanceHistoryIndividual = individualAttendanceHistoryOfAStudent.ToArray();
            return allAttendanceHistoryOfAStudent;
        }

        public AllCourseAttendanceHistoryOfAStudentResDto getAttendanceOfAStudentOfAllCourses(int studentId)
        {
            AllCourseAttendanceHistoryOfAStudentResDto responseToReturn=new AllCourseAttendanceHistoryOfAStudentResDto();
            try
            {

                
                StudentFullInfoResDto student = utilityService.getStudentByStudentId(studentId);

                responseToReturn.program = student.programName;
                responseToReturn.semester = student.semesterName;
                responseToReturn.batch = student.batchName;
                responseToReturn.studentName = student.name;
                responseToReturn.classRoll = student.classRoll;
                responseToReturn.classRoll = student.classRoll;

                GetCoursesResDto courseOfAStudent = utilityService.getCoursesOfAStudent(studentId);
                List<Attendance> attendanceListFromRepo = attendanceRepo.getAttendanceOfAStudentOfAllCourses(student.programId, student.semesterId, student.batchId, studentId);
                responseToReturn.attendanceOfAllCourses = processAllCoursesAttendance(student, courseOfAStudent, attendanceListFromRepo);
                return responseToReturn;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        private AllCourseAttendanceHistoryOnlyOfAStudentResDto[] processAllCoursesAttendance(StudentFullInfoResDto student, GetCoursesResDto courseOfAStudent, List<Attendance> attendanceListFromRepo)
        {
            AllCourseAttendanceHistoryOnlyOfAStudentResDto[] allCourseAttendanceHistoryOfAStudentList = new AllCourseAttendanceHistoryOnlyOfAStudentResDto[courseOfAStudent.courses.Length];

            for (int i=0;i< courseOfAStudent.courses.Length; i++)
            {
                AllCourseAttendanceHistoryOnlyOfAStudentResDto allCourseAttendanceHistoryOfAStuden = new AllCourseAttendanceHistoryOnlyOfAStudentResDto();
                var course = courseOfAStudent.courses[i];

                allCourseAttendanceHistoryOfAStuden.courseName = course.name;

                List<AClassAttendanceHistoryOfAStudentResDto> aClassAttendanceHistoryOfAStudentList = new List<AClassAttendanceHistoryOfAStudentResDto>();
                for (int j = 0; j < attendanceListFromRepo.Count; j++)
                {
                    var attendanceTemp = attendanceListFromRepo.ElementAt(j);
                    if (attendanceTemp.studentId==student.id && attendanceTemp.programId==student.programId && attendanceTemp.semesterId==student.semesterId && attendanceTemp.batchId==student.batchId && attendanceTemp.courseId== course.id)
                    {
                        AClassAttendanceHistoryOfAStudentResDto tempAtt = new AClassAttendanceHistoryOfAStudentResDto();

                        tempAtt.classNo = attendanceTemp.class_no;
                        tempAtt.date = attendanceTemp.classDate;
                        tempAtt.isPresent = attendanceTemp.is_present;

                        aClassAttendanceHistoryOfAStudentList.Add(tempAtt);

                    }

                }

                allCourseAttendanceHistoryOfAStuden.attendances = aClassAttendanceHistoryOfAStudentList.ToArray();

                allCourseAttendanceHistoryOfAStudentList[i] = allCourseAttendanceHistoryOfAStuden;
            }

            return allCourseAttendanceHistoryOfAStudentList;
        }
    }
}