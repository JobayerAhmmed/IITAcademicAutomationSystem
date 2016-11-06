using IITAcademicAutomationSystem.Areas.Two.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IITAcademicAutomationSystem.Areas.Two.ResponseDto;
using IITAcademicAutomationSystem.Areas.Two.Repo;
using IITAcademicAutomationSystem.Areas.Two.RepoImpl;
using IITAcademicAutomationSystem.Models;

namespace IITAcademicAutomationSystem.Areas.Two.ServiceImpl
{
    public class UtilitySerImpl : UtilitySerI
    {
        UtilityRepoI utilityRepository=new UtilityRepoImpl();

        public int getIdOfLoggedInTeacher()
        {
            try
            {
                return 10;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public int getIdOfLoggedInStudent()
        {
            try
            {
                return 10;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

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

        public GetProgramsResDto getProgramsOfATeacher(int teacherId)
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

        public GetSemestersResDto getSemestersOfATeacherOfAProgram(int teacherId, int programId)
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

        public GetCoursesResDto getCoursesOfATeacherOfASemesterOfAProgram(int teacherId, int programId, int semesterId)
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
                batchResDto.id = batch.Id;
                batchResDto.name = ""+batch.BatchNo;//be careful about this
                return batchResDto;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public ProgramSemesterBatchResDto getProgramSemesterBatchOfLoggedInStudent()
        {
            ProgramSemesterBatchResDto programSemesterBatchResDto = new ProgramSemesterBatchResDto();
            try
            {
                ProgramResDto programResDto = new ProgramResDto();
                programResDto.id = 1;
                programResDto.name = "BSSE";
                programSemesterBatchResDto.program = programResDto;


                SemesterResDto semesterResDto = new SemesterResDto();
                semesterResDto.id = 2;
                semesterResDto.name="2nd";

                programSemesterBatchResDto.semester = semesterResDto;

                                              
                BatchResDto batchResDto = new BatchResDto();
                batchResDto.id = 4;
                batchResDto.name = "Fifth Batch";
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
                StudentResDto[] studentList = new StudentResDto[5];
                StudentResDto student = new StudentResDto();

                student.id = 1;
                student.classRoll = "0501";
                student.name = "Dipok Chandra Dus";
                student.examRoll = "111";
                studentList[0] = student;
                student = new StudentResDto();

                student.id = 2;
                student.classRoll = "0502";
                student.name = "Jobayer Ahmed";
                student.examRoll = "222";
                studentList[1] = student;
                student = new StudentResDto();

                student.id = 3;
                student.classRoll = "0503";
                student.name = "Shofol Kawsir";
                student.examRoll = "666666";
                studentList[2] = student;
                student = new StudentResDto();

                student.id = 4;
                student.classRoll = "0504";
                student.name = "Tayeb Zayed";
                student.examRoll = "333";
                studentList[3] = student;
                student = new StudentResDto();

                student.id = 5;
                student.classRoll = "0505";
                student.name = "Atikur Rahman";
                student.examRoll = "555";
                studentList[4] = student;
                student = new StudentResDto();

                /*student.id = 7;
                student.classRoll = "0507";
                student.name = "Shadiqur Rahman";
                student.examRoll = "666";
                studentList[5] = student;
                student = new StudentResDto();

                student.id = 8;
                student.classRoll = "0508";
                student.name = "Misu Bin Imp";
                student.examRoll = "777";
                studentList[6] = student;
                student = new StudentResDto();

                student.id = 9;
                student.classRoll = "0509";
                student.name = "Mostaq Adil";
                student.examRoll = "888";
                studentList[7] = student;
                student = new StudentResDto();

                student.id = 10;
                student.classRoll = "0510";
                student.name = "Babu Pahari";
                student.examRoll = "999";
                studentList[8] = student;
                student = new StudentResDto();

                student.id = 11;
                student.classRoll = "0511";
                student.name = "Saimul Islam";
                student.examRoll = "000";
                studentList[9] = student;
                student = new StudentResDto();


                student.id = 12;
                student.classRoll = "0512";
                student.name = "Ishmam Shahriar";
                student.examRoll = "221";
                studentList[10] = student;
                student = new StudentResDto();*/

                getStudentsResponseDto.students = studentList;
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
                StudentResDto[] studentList = new StudentResDto[5];
                StudentResDto student = new StudentResDto();

                student.id = 1;
                student.classRoll = "0501";
                student.name = "Dipok Chandra Dus";
                student.examRoll = "111";
                studentList[0] = student;
                student = new StudentResDto();

                student.id = 2;
                student.classRoll = "0502";
                student.name = "Jobayer Ahmed";
                student.examRoll = "222";
                studentList[1] = student;
                student = new StudentResDto();

                student.id = 3;
                student.classRoll = "0503";
                student.name = "Shofol Kawsir";
                student.examRoll = "666666";
                studentList[2] = student;
                student = new StudentResDto();

                student.id = 4;
                student.classRoll = "0504";
                student.name = "Tayeb Zayed";
                student.examRoll = "333";
                studentList[3] = student;
                student = new StudentResDto();

                student.id = 5;
                student.classRoll = "0505";
                student.name = "Atikur Rahman";
                student.examRoll = "555";
                studentList[4] = student;
                student = new StudentResDto();

                /*student.id = 7;
                student.classRoll = "0507";
                student.name = "Shadiqur Rahman";
                student.examRoll = "666";
                studentList[5] = student;
                student = new StudentResDto();

                student.id = 8;
                student.classRoll = "0508";
                student.name = "Misu Bin Imp";
                student.examRoll = "777";
                studentList[6] = student;
                student = new StudentResDto();

                student.id = 9;
                student.classRoll = "0509";
                student.name = "Mostaq Adil";
                student.examRoll = "888";
                studentList[7] = student;
                student = new StudentResDto();

                student.id = 10;
                student.classRoll = "0510";
                student.name = "Babu Pahari";
                student.examRoll = "999";
                studentList[8] = student;
                student = new StudentResDto();

                student.id = 11;
                student.classRoll = "0511";
                student.name = "Saimul Islam";
                student.examRoll = "000";
                studentList[9] = student;
                student = new StudentResDto();
                

                student.id = 12;
                student.classRoll = "0512";
                student.name = "Ishmam Shahriar";
                student.examRoll = "221";
                studentList[10] = student;
                student = new StudentResDto();*/

                getStudentsResponseDto.students = studentList;
                return  getStudentsResponseDto;

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
                var courseList = utilityRepository.getCoursesOfATeacherOfASemesterOfAProgram(10,10,10);//.....................
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
                StudentFullInfoResDto student = new StudentFullInfoResDto();               

                student.id = 4;
                student.classRoll = "0510";
                student.name = "Tayeb Zayed";
                student.examRoll = "999";

                student.programId = 1;
                student.programName = "BSSE";

                student.semesterId = 2;
                student.semesterName = "2nd Semester";

                student.batchId = 4;
                student.batchName = "Fifth Batch";

                return student;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string getIdOfLoggedInProgramOfficer()
        {
            try
            {
                return ""+10;
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
                CourseResDto courseResDto = new CourseResDto();
                List<Course> courseList = new List<Course>();

                Course course = new Course();
                course.Id = 1;
                course.CourseCode = "CSE-801";
                courseList.Add(course);

                course = new Course();
                course.Id = 2;
                course.CourseCode = "CSE-802";
                courseList.Add(course);

                course = new Course();
                course.Id = 3;
                course.CourseCode = "CSE-803";
                courseList.Add(course);

                for (int i = 0; i < courseList.Count; i++)
                {
                    if (courseList.ElementAt(i).Id == courseId)
                    {
                        courseResDto.id = courseList.ElementAt(i).Id;
                        courseResDto.name = courseList.ElementAt(i).CourseCode;
                    }
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
                CourseResDto course = new CourseResDto();
                course.id = 1;
                course.name = "CSE-801";
                courseList.Add(course);

                course = new CourseResDto();
                course.id = 2;
                course.name = "CSE-802";
                courseList.Add(course);

                course = new CourseResDto();
                course.id = 3;
                course.name = "CSE-803";
                courseList.Add(course);

                return courseList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public GetCoursesResDto getCoursesOfAStudent()
        {
            try
            {
                var studentId = getIdOfLoggedInStudent();
                return getCoursesOfAStudent(studentId);
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
    }
}