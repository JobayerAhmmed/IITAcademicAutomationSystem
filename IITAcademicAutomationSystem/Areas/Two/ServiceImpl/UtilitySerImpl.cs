using IITAcademicAutomationSystem.Areas.Two.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IITAcademicAutomationSystem.Areas.Two.ResponseDto;
using IITAcademicAutomationSystem.Areas.Two.Repo;
using IITAcademicAutomationSystem.Areas.Two.RepoImpl;
using IITAcademicAutomationSystem.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace IITAcademicAutomationSystem.Areas.Two.ServiceImpl
{
    public class UtilitySerImpl : UtilitySerI
    {
        UtilityRepoI utilityRepository=new UtilityRepoImpl();

        /*public int getIdOfLoggedInTeacher()
        {
            try
            {
                return 10;
            }
            catch(Exception e)
            {
                throw e;
            }
        }*/

       /* public int getIdOfLoggedInStudent()
        {
            try
            {
                return 4;
            }
            catch (Exception e)
            {
                throw e;
            }
        }*/

        public GetProgramsResDto getAllPrograms()
        {
            GetProgramsResDto getProgramsResDto = new GetProgramsResDto();
            try
            {
                var programList = utilityRepository.getAllPrograms();
                ProgramResDto[] programResDtoList = new ProgramResDto[programList.Count];
                for (int i = 0; i < programList.Count; i++)
                {
                    ProgramResDto programResDto = new ProgramResDto();
                    programResDto.id = programList.ElementAt(i).Id;
                    programResDto.name = programList.ElementAt(i).ProgramName;

                    programResDtoList[i] = programResDto;
                }

                getProgramsResDto.programs = programResDtoList;
                return getProgramsResDto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public GetProgramsResDto getProgramsOfATeacher(string teacherId)
        {
            GetProgramsResDto getProgramsResDto = new GetProgramsResDto();           

            try
            {
                var programList= utilityRepository.getProgramsOfATeacher(teacherId);
                ProgramResDto[] programResDtoList = new ProgramResDto[programList.Count];
                for (int i = 0; i < programList.Count; i++)
                {
                    ProgramResDto programResDto = new ProgramResDto();
                    programResDto.id = programList.ElementAt(i).Id;
                    programResDto.name = programList.ElementAt(i).ProgramName;

                    programResDtoList[i] = programResDto;
                }

                getProgramsResDto.programs = programResDtoList;
                return getProgramsResDto;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public GetSemestersResDto getSemestersOfAProgram(int programId)
        {
            GetSemestersResDto getSemestersResDto = new GetSemestersResDto();

            try
            {
                var semesterList = utilityRepository.getSemestersOfAProgram( programId);
                SemesterResDto[] semesterResDtoList = new SemesterResDto[semesterList.Count];
                for (int i = 0; i < semesterList.Count; i++)
                {
                    SemesterResDto semesterResDto = new SemesterResDto();
                    semesterResDto.id = semesterList.ElementAt(i).Id;
                    semesterResDto.name = ""+semesterList.ElementAt(i).SemesterNo;

                    semesterResDtoList[i] = semesterResDto;
                }

                getSemestersResDto.semesters = semesterResDtoList;
                return getSemestersResDto;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public GetSemestersResDto getSemestersOfATeacherOfAProgram(string teacherId, int programId)
        {
            GetSemestersResDto getSemestersResDto = new GetSemestersResDto();

            try
            {
                var semesterList = utilityRepository.getSemestersOfATeacherOfAProgram(teacherId,programId);
                SemesterResDto[] semesterResDtoList = new SemesterResDto[semesterList.Count];
                for (int i = 0; i < semesterList.Count; i++)
                {
                    SemesterResDto semesterResDto = new SemesterResDto();
                    semesterResDto.id = semesterList.ElementAt(i).Id;
                    semesterResDto.name = ""+semesterList.ElementAt(i).SemesterNo;

                    semesterResDtoList[i] = semesterResDto;
                }

                getSemestersResDto.semesters = semesterResDtoList;
                return getSemestersResDto;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public GetCoursesResDto getCoursesOfATeacherOfASemesterOfAProgram(string teacherId, int programId, int semesterId)
        {
            GetCoursesResDto getCoursesResDto = new GetCoursesResDto();

            try
            {
                var courseList = utilityRepository.getCoursesOfATeacherOfASemesterOfAProgram(teacherId, programId,semesterId);
                CourseResDto[] courseResDtoList = new CourseResDto[courseList.Count];
                for (int i = 0; i < courseList.Count; i++)
                {
                    CourseResDto courseResDto = new CourseResDto();
                    courseResDto.id = courseList.ElementAt(i).Id;
                    courseResDto.name = courseList.ElementAt(i).CourseCode;

                    courseResDtoList[i] = courseResDto;
                }

                getCoursesResDto.courses = courseResDtoList;
                return getCoursesResDto;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public BatchResDto getBatch(int programId,int semesterId)
        {
            BatchResDto batchResDto = new BatchResDto();
            try
            {
                var batch = utilityRepository.getBatch(programId, semesterId);
                if (batch != null)
                {
                    batchResDto.id = batch.Id;
                    batchResDto.name = "" + batch.BatchNo;//be careful about this
                }
                return batchResDto;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public ProgramSemesterBatchResDto getProgramSemesterBatchOfLoggedInStudent(int studentId)
        {
            ProgramSemesterBatchResDto programSemesterBatchResDto = new ProgramSemesterBatchResDto();
            try
            {
                SemesterResDto semesterResDto = new SemesterResDto();
                Semester semester = utilityRepository.getSemesterOfAStudentByStudentId(studentId);
                if (semester == null)
                    return null;
                semesterResDto.id = semester.Id;
                semesterResDto.name = "Semester: " + semester.SemesterNo;
                programSemesterBatchResDto.semester = semesterResDto;

                ProgramResDto programResDto = new ProgramResDto();
                Program program = utilityRepository.getProgramOfAStudentBySemesterId(semester.Id);
                if (program == null)
                    return null;
                programResDto.id = program.Id;
                programResDto.name = program.ProgramName;
                programSemesterBatchResDto.program = programResDto;

                Batch batch = utilityRepository.getCurrentBatchOfAStudentByStudentId(studentId);
                if (batch == null)
                    return null;
                BatchResDto batchResDto = new BatchResDto();
                batchResDto.id = batch.Id;
                batchResDto.name = "Batch: "+batch.BatchNo;
                programSemesterBatchResDto.batch = batchResDto;

                return programSemesterBatchResDto;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public GetStudentsResponseDto getStudentsOfASemester(int programId, int semesterId, int batchId)//has to be corrected....................
        {
            GetStudentsResponseDto getStudentsResponseDto = new GetStudentsResponseDto();
            try
            {
                List<Student> studentListFromRepo = utilityRepository.getStudentsOfASemester(programId,semesterId, batchId);
                List<StudentResDto> studentList = new List<StudentResDto>();

                for (int i = 0; i < studentListFromRepo.Count; i++)
                {
                    var tempStudent = studentListFromRepo.ElementAt(i);
                    StudentResDto student = new StudentResDto();
                    student.id = tempStudent.Id;
                    //var temp1 = utilityRepository.getStudentByStudentId(studentTemp.id);
                    var studentUser = utilityRepository.getUserByUserId(tempStudent.UserId);
                    student.classRoll = tempStudent.CurrentRoll;
                    student.name = studentUser.FullName;
                    student.examRoll = "";
                    studentList.Add(student);

                }
                
                getStudentsResponseDto.students = studentList.ToArray();
                return getStudentsResponseDto;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public GetStudentsResponseDto getStudentsOfACOurse(int programId, int semesterId,int batchId, int courseId )//has to be corrected....................
        {
            GetStudentsResponseDto getStudentsResponseDto = new GetStudentsResponseDto();
            try
            {
                List<Student> studentListFromRepo = utilityRepository.getStudentsOfACOurse(programId, semesterId, batchId, courseId);
                List<StudentResDto> studentList = new List<StudentResDto>();

                for (int i = 0; i < studentListFromRepo.Count; i++)
                {
                    var tempStudent = studentListFromRepo.ElementAt(i);
                    StudentResDto student = new StudentResDto();
                    student.id = tempStudent.Id;
                    student.classRoll = tempStudent.CurrentRoll;
                    var user = utilityRepository.getUserByUserId(tempStudent.UserId);
                    student.name = user.FullName;
                    student.examRoll = "";
                    studentList.Add(student);

                }

                getStudentsResponseDto.students = studentList.ToArray();
                return getStudentsResponseDto;

            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public GetCoursesResDto getCoursesOfAStudent(int studentId)
        {
            GetCoursesResDto getCoursesResDto = new GetCoursesResDto();

            try
            {
                ProgramSemesterBatchResDto programSemesterBatch = getProgramSemesterBatchOfLoggedInStudent(studentId);
                var courseList = utilityRepository.getCoursesOfAStudentOfASemesterOfAProgram(studentId, programSemesterBatch.program.id, programSemesterBatch.semester.id, programSemesterBatch.batch.id);//.....................
                CourseResDto[] courseResDtoList = new CourseResDto[courseList.Count];
                for (int i = 0; i < courseList.Count; i++)
                {
                    CourseResDto courseResDto = new CourseResDto();
                    courseResDto.id = courseList.ElementAt(i).Id;
                    courseResDto.name = courseList.ElementAt(i).CourseCode;

                    courseResDtoList[i] = courseResDto;
                }

                getCoursesResDto.courses = courseResDtoList;
                return getCoursesResDto;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public StudentFullInfoResDto getStudentByStudentId(int studentId)
        {
           try
           {
                Student studentFromRepo = utilityRepository.getStudentByStudentId(studentId);
                ApplicationUser studentUser = utilityRepository.getUserByUserId(studentFromRepo.UserId);

                StudentFullInfoResDto student = new StudentFullInfoResDto();
                student.id = studentFromRepo.Id;
                student.classRoll = studentFromRepo.CurrentRoll;
                student.name = studentUser.FullName;
                student.examRoll = "";

                student.programId = studentFromRepo.ProgramId;
               Program program = utilityRepository.getProgramByProgramId(studentFromRepo.ProgramId);
                student.programName = program.ProgramName;
                
                student.semesterId = studentFromRepo.SemesterId;
               Semester semester = utilityRepository.getSemesterBySemesterId(studentFromRepo.SemesterId);
                student.semesterName = "Semester: "+semester.SemesterNo;

                student.batchId = studentFromRepo.BatchIdCurrent;
               Batch batch = utilityRepository.getBatchByBatchId(studentFromRepo.BatchIdCurrent);
                student.batchName = "Batch: "+ batch.BatchNo;

                return student;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        

        public CourseResDto getCourse(int courseId)
        {
            try
            {
                Course courseFromRepo = utilityRepository.getCourseByCourseId(courseId);
                CourseResDto courseResDto = new CourseResDto();

                if (courseFromRepo != null)
                {
                    courseResDto.id = courseFromRepo.Id;
                    courseResDto.name = courseFromRepo.CourseCode;
                }
                return courseResDto;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public List<CourseResDto> getAllCoursesOfASemester(int programId, int semesterId, int batchId)
        {
            List<CourseResDto> courseList = new List<CourseResDto>();
            try
            {
                List<Course> courseFromRepo = utilityRepository.getAllCoursesOfASemester(programId, semesterId, batchId);

                for (int i = 0; i < courseFromRepo.Count; i++)
                {
                    CourseResDto course = new CourseResDto();
                    course.id = courseFromRepo.ElementAt(i).Id;
                    course.name = courseFromRepo.ElementAt(i).CourseTitle;
                    courseList.Add(course);
                }
                return courseList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public BatchResDto[] getBatchesOfAProgram(int programId)
        {
            try
            {
                List<Batch> batchListFromRepo = utilityRepository.getBatchesOfAProgram(programId);

                List< BatchResDto > responseToReturn=new List<BatchResDto>();
                for (int i = 0; i < batchListFromRepo.Count; i++)
                {
                    BatchResDto batchResDto = new BatchResDto();
                    var batchTemp = batchListFromRepo.ElementAt(i);

                    batchResDto.id = batchTemp.Id;
                    batchResDto.name = "Batch: "+batchTemp.BatchNo;
                    responseToReturn.Add(batchResDto);
                }

                return responseToReturn.ToArray();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void savePassFailInfoOfAStudnet(int semesterId, int batchId, int studentId, double GPA)
        {
            try
            {
                StudentSemester studentSemester=new StudentSemester();
                studentSemester.SemesterId = semesterId;
                studentSemester.BatchId = batchId;
                studentSemester.StudentId = studentId;
                studentSemester.GPA = GPA;

                utilityRepository.savePassFailInfoOfAStudnet(studentSemester);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void editPassFailInfoOfAStudnet(int semesterId, int batchId, int studentId, double GPA)
        {
            try
            {
                StudentSemester studentSemester = new StudentSemester();
                studentSemester.SemesterId = batchId;
                studentSemester.BatchId = semesterId;
                studentSemester.StudentId = studentId;
                studentSemester.GPA = GPA;

                utilityRepository.editPassFailInfoOfAStudnet(studentSemester);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int checkIfPassedFailInfoIsSaved(int semesterId, int batchId, int studentId)
        {
            try
            {
                return utilityRepository.checkIfPassedFailInfoIsSaved(semesterId, batchId, studentId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void saveCourseWiseGPAOfAStudent(int semesterId, int batchId, int courseId, int studentId, double GPA)
        {
            try
            {
                StudentCourse studentCourse=new StudentCourse();
                studentCourse.SemesterId = semesterId;
                studentCourse.BatchId = batchId;
                studentCourse.CourseId = courseId;
                studentCourse.StudentId = studentId;
                studentCourse.GradePoint = GPA;
                utilityRepository.saveCourseWiseGPAOfAStudent(studentCourse);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void editCourseWiseGPAOfAStudent(int semesterId, int batchId, int courseId, int studentId, double GPA)
        {
            try
            {
                StudentCourse studentCourse = new StudentCourse();
                studentCourse.SemesterId = semesterId;
                studentCourse.BatchId = batchId;
                studentCourse.CourseId = courseId;
                studentCourse.StudentId = studentId;
                studentCourse.GradePoint = GPA;
                utilityRepository.editCourseWiseGPAOfAStudent(studentCourse);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int checkIfCourseWiseGPAIsSaved(int semesterId, int batchId, int courseId, int studentId)
        {
            try
            {
                return utilityRepository.checkIfCourseWiseGPAIsSaved(semesterId, batchId, courseId, studentId);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int getStudentIdByUserId(string userId)
        {
            try
            {
                return utilityRepository.getStudentIdByUserId(userId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public SemesterResDto[] getSemestersOfABatchCoordinator(string batchCoordinaorId,int programId)
        {
            try
            {
                List<Semester> semesterListFromRepo = utilityRepository.getSemestersOfABatchCoordinator(batchCoordinaorId, programId);
                List< SemesterResDto > semesterToReturn=new List<SemesterResDto>();
               for (int i = 0; i < semesterListFromRepo.Count; i++)
                {
                    SemesterResDto tempSemester = new SemesterResDto();
                    tempSemester.id = semesterListFromRepo.ElementAt(i).Id;
                    tempSemester.name = "Semester :"+ semesterListFromRepo.ElementAt(i).SemesterNo;
                    semesterToReturn.Add(tempSemester);
                }

                return semesterToReturn.ToArray();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}