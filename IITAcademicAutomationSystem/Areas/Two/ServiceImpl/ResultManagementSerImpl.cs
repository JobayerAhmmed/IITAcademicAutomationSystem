using IITAcademicAutomationSystem.Areas.Two.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using IITAcademicAutomationSystem.Areas.Two.RequestDto;
using IITAcademicAutomationSystem.Areas.Two.Repo;
using IITAcademicAutomationSystem.Models;
using IITAcademicAutomationSystem.Areas.Two.RepoImpl;
using IITAcademicAutomationSystem.Areas.Two.ResponseDto;

namespace IITAcademicAutomationSystem.Areas.Two.ServiceImpl
{
    public class ResultManagementSerImpl : ResultManagementSerI
    {
        private MarksHeadRepoI marksHeadRepo = new MarksHeadRepoImpl();
        private MarksSubHeadRepoI marksSubHeadRepo = new MarksSubHeadRepoImpl();
        private MarksDistributionRepoI marksDistributionRepo = new MarksDistributionRepoImpl();
        UtilitySerI utilityService = new UtilitySerImpl();
        MarksRepoI marksRepository = new MarksRepoImpl();
        UtilityRepoI utilityRepository=new UtilityRepoImpl();
        public void createHead(AddHeadRequestDto addHeadRequestDto)
        {
            MarksHead marksHead = new MarksHead();
            try
            {
                marksHead.name = addHeadRequestDto.name;
                marksHeadRepo.createHead(marksHead);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public GetHeadsResDto getAllHeads()
        {
            GetHeadsResDto headsToReturn = new GetHeadsResDto();
            try
            {


                var headsArray = marksHeadRepo.getAllHeads();
                HeadResDto[] heads = new HeadResDto[headsArray.Count];



                for (int i = 0; i < headsArray.Count; i++)
                {
                    HeadResDto head = new HeadResDto();

                    head.id = headsArray[i].Id;
                    head.name = headsArray[i].name;

                    heads[i] = head;
                }
                headsToReturn.heads = heads;


                return headsToReturn;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void createSubHead(AddSubHeadRequestDto addSubHeadRequestDto)
        {
            MarksSubHead subHead = new MarksSubHead();
            try
            {
                subHead.name = addSubHeadRequestDto.name;
                subHead.headId = addSubHeadRequestDto.headId;

                marksSubHeadRepo.createSubHead(subHead);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public GetSubHeadsResDto getSubHeads(int headId)
        {
            GetSubHeadsResDto subHeadsToReturn = new GetSubHeadsResDto();
            try
            {
                var subHeadsArray = marksSubHeadRepo.getSubHeads(headId);

                SubHeadResDto[] subHeads = new SubHeadResDto[subHeadsArray.Count];



                for (int i = 0; i < subHeadsArray.Count; i++)
                {
                    SubHeadResDto subHead = new SubHeadResDto();

                    subHead.id = subHeadsArray[i].Id;
                    subHead.name = subHeadsArray[i].name;
                    subHead.headId = subHeadsArray[i].headId;

                    subHeads[i] = subHead;
                    Console.Write(subHead);
                    Console.Write(subHeads);


                }
                Console.Write(subHeads);

                subHeadsToReturn.subHeads = subHeads;


                return subHeadsToReturn;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void distributeMarks(DistributeMarksFinalReqDto distributeMarksFinalReqDto, string teacherId)
        {



            try
            {
                List<MarksDistribution> marksDistributionToSave = processMarksDistribution(distributeMarksFinalReqDto, teacherId);

                for (int i = 0; i < marksDistributionToSave.Count; i++)
                {
                    marksDistributionRepo.distributeMarks(marksDistributionToSave.ElementAt(i));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private List<MarksDistribution> processMarksDistribution(DistributeMarksFinalReqDto distributeMarksFinalReqDto,string teacherId)
        {
            int headNumber = distributeMarksFinalReqDto.distribution.Length;
            List<MarksDistribution> marksDistributionToSave = new List<MarksDistribution>();

            for (int i = 0; i < headNumber; i++)
            {
                MarksDistribution marksDistribution = new MarksDistribution();

                marksDistribution.marksEvaluated = distributeMarksFinalReqDto.distribution[i].weight;
                var temp1 = distributeMarksFinalReqDto.distribution[i].avarageOrBestCount;
                if (temp1 == "True")
                    marksDistribution.avarageOrBestCount = true;
                else if (temp1 == "False")
                    marksDistribution.avarageOrBestCount = false;
                var temp2 = distributeMarksFinalReqDto.distribution[i].isVisibleToStudent;
                if (temp2 == "True")
                    marksDistribution.isVisibleToStudent = true;
                else if (temp2 == "False")
                    marksDistribution.isVisibleToStudent = false;

                marksDistribution.isFinallySubmitted = false;
                marksDistribution.headId = distributeMarksFinalReqDto.distribution[i].headId;

                marksDistribution.programId = distributeMarksFinalReqDto.programId;
                marksDistribution.semesterId = distributeMarksFinalReqDto.semesterId;
                marksDistribution.batchId = distributeMarksFinalReqDto.batchId;
                marksDistribution.courseId = distributeMarksFinalReqDto.courseId;
                marksDistribution.marksDistributorId = teacherId;//need to be changed

                marksDistributionToSave.Add(marksDistribution);
            }

            return marksDistributionToSave;

        }

        public GetDistributedMarksResDto getDistributedMarks(int programId, int semesterId, int batchId, int courseId)
        {
            GetDistributedMarksResDto getDistributedMarksResDto = new GetDistributedMarksResDto();
            try
            {
                var distributedMarksList = marksDistributionRepo.getDistributedMarks(programId, semesterId, batchId, courseId);
                DistributedMarkResDto[] distributedMarkResDtoTemp = new DistributedMarkResDto[distributedMarksList.Count];
                for (int i = 0; i < distributedMarksList.Count; i++)
                {
                    if(i==0)
                        getDistributedMarksResDto.isFinallySubmitted = distributedMarksList.ElementAt(i).isFinallySubmitted;
                    DistributedMarkResDto distributedMarkResDto = new DistributedMarkResDto();
                    distributedMarkResDto.id = distributedMarksList.ElementAt(i).Id;
                    distributedMarkResDto.weight = distributedMarksList.ElementAt(i).marksEvaluated;
                    var tempHead = marksHeadRepo.getHead(distributedMarksList.ElementAt(i).headId);
                    HeadResDto tempHead2 = new HeadResDto();
                    tempHead2.id = tempHead.Id;
                    tempHead2.name = tempHead.name;
                    distributedMarkResDto.head = tempHead2;
                    bool temp = distributedMarksList.ElementAt(i).avarageOrBestCount;
                    if (temp == true)
                        distributedMarkResDto.avarageOrBestCount = "Avarage";
                    else if (temp == false)
                        distributedMarkResDto.avarageOrBestCount = "Best Count";

                    temp = distributedMarksList.ElementAt(i).isVisibleToStudent;
                    if (temp == true)
                        distributedMarkResDto.isVisibleToStudent = "Yes";
                    else if (temp == false)
                        distributedMarkResDto.isVisibleToStudent = "No";

                    distributedMarkResDtoTemp[i] = distributedMarkResDto;

                }

                getDistributedMarksResDto.distributedMarks = distributedMarkResDtoTemp;

                return getDistributedMarksResDto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool checkIfMarksIsDistributedForACourse(int programId, int semesterId, int batchId, int courseId)
        {
            try
            {
                var distributedMarksList = marksDistributionRepo.getDistributedMarks(programId, semesterId, batchId, courseId);
                if (distributedMarksList.Count > 0)
                    return true;
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void editDistributedMarks(EditedDistributeMarksReqDto editedDistributeMarksReqDto, string teacherId)
        {
            try
            {

                for (int i = 0; i < editedDistributeMarksReqDto.distributions.Length; i++)
                {
                    if (editedDistributeMarksReqDto.distributions[i].id == -1)
                    {
                        MarksDistribution marksDistributionToSave = new MarksDistribution();
                        marksDistributionToSave.marksEvaluated = editedDistributeMarksReqDto.distributions[i].weight;
                        var temp = editedDistributeMarksReqDto.distributions[i].avarageOrBestCount;
                        if (temp == "Avarage")
                            marksDistributionToSave.avarageOrBestCount = true;
                        else if (temp == "Best Count")
                            marksDistributionToSave.avarageOrBestCount = false;

                        temp = editedDistributeMarksReqDto.distributions[i].isVisibleToStudent;
                        if (temp == "Yes")
                            marksDistributionToSave.isVisibleToStudent = true;
                        else if (temp == "No")
                            marksDistributionToSave.isVisibleToStudent = false;

                        marksDistributionToSave.isFinallySubmitted = false;
                        marksDistributionToSave.programId = editedDistributeMarksReqDto.programId;
                        marksDistributionToSave.semesterId = editedDistributeMarksReqDto.semesterId;
                        marksDistributionToSave.batchId = editedDistributeMarksReqDto.batchId;
                        marksDistributionToSave.courseId = editedDistributeMarksReqDto.courseId;
                        marksDistributionToSave.headId = editedDistributeMarksReqDto.distributions[i].headId;
                        marksDistributionToSave.marksDistributorId = teacherId; ;

                        marksDistributionRepo.distributeMarks(marksDistributionToSave);
                    }

                    else if (editedDistributeMarksReqDto.distributions[i].id != -1)
                    {
                        MarksDistribution marksDistribution = marksDistributionRepo.getDistributedMarks(editedDistributeMarksReqDto.distributions[i].id);

                        marksDistribution.marksEvaluated = editedDistributeMarksReqDto.distributions[i].weight;

                        var temp1 = editedDistributeMarksReqDto.distributions[i].avarageOrBestCount;
                        if (temp1 == "Avarage")
                            marksDistribution.avarageOrBestCount = true;
                        else if (temp1 == "Best Count")
                            marksDistribution.avarageOrBestCount = false;

                        var temp2 = editedDistributeMarksReqDto.distributions[i].isVisibleToStudent;
                        if (temp2 == "Yes")
                            marksDistribution.isVisibleToStudent = true;
                        else if (temp2 == "No")
                            marksDistribution.isVisibleToStudent = false;

                        marksDistribution.headId = editedDistributeMarksReqDto.distributions[i].headId;

                        marksDistributionRepo.editDistributedMarks(marksDistribution);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void saveGivenMarks(GiveMarksReqDto giveMarksReqDto, string teacherId)
        {
            int marksLength = giveMarksReqDto.marks.Length;
            try
            {
                Marks[] marksListToSave = new Marks[marksLength];

                for (int i = 0; i < marksLength; i++)
                {
                    Marks marks = new Marks();
                    marks.examMarks = giveMarksReqDto.examMarks;
                    marks.obtainedMarks = giveMarksReqDto.marks[i].obtainedMarks;
                    marks.marksDistributionId = giveMarksReqDto.marksDistributionId;
                    marks.subheadId = giveMarksReqDto.subHeadId;
                    marks.studentId = giveMarksReqDto.marks[i].studentId;
                    marks.marksGiverId = teacherId;
                    marksListToSave[i] = marks;
                }

                for (int i = 0; i < marksLength; i++)
                {
                    marksRepository.saveGivenMarks(marksListToSave[i]);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public GetDistributedMarksPartialResDto getDistributedMarksPartially(int programId, int semesterId, int batchId, int courseId)
        {
            GetDistributedMarksPartialResDto responseDtoToReturn = new GetDistributedMarksPartialResDto();

            try
            {
                List<MarksDistribution> marksDistributionList = marksDistributionRepo.getDistributedMarks(programId, semesterId, batchId, courseId);
                DistributedMarkPartialResDto[] toProcess = new DistributedMarkPartialResDto[marksDistributionList.Count];

                for (int i = 0; i < marksDistributionList.Count; i++)
                {
                    DistributedMarkPartialResDto temp = new DistributedMarkPartialResDto();

                    temp.id = marksDistributionList.ElementAt(i).Id;

                    HeadResDto head = new HeadResDto();
                    head.id = marksDistributionList.ElementAt(i).headId;
                    head.name = marksHeadRepo.getHead(head.id).name;

                    temp.head = head;
                    toProcess[i] = temp;

                }

                responseDtoToReturn.distributedMarks = toProcess;
                return responseDtoToReturn;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public GetGivenMarksResto getGivenMarks_t(int programId, int semesterId, int batchId, int courseId)
        {
            GetGivenMarksResto responseToReturn = new GetGivenMarksResto();
            StudentMarksResDto studentMarksResDto = new StudentMarksResDto();
            MarksOfAStudentResDto marksOfAStudentResDto = new MarksOfAStudentResDto();
            try
            {
                List<MarksDistribution> marksDistributionList = marksDistributionRepo.getDistributedMarks(programId, semesterId, batchId, courseId);

                if (marksDistributionList.Count != 0) {
                    responseToReturn.heads = processHead(marksDistributionList);
                    responseToReturn.subHeads = processSubHead(marksDistributionList);
                    GetStudentsResponseDto studentList = utilityService.getStudentsOfACOurse(programId, semesterId, batchId, courseId);
                    responseToReturn.marksOfStudents = processStudentMarks_t(studentList.students, marksDistributionList, responseToReturn.subHeads);
                }
                return responseToReturn;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public GetGivenMarksResto getGivenMarks_s(int studentId,int courseId)
        {
            
            var studentInfo = utilityService.getStudentByStudentId(studentId);

            int programId = studentInfo.programId;
            int semesterId = studentInfo.semesterId;
            int batchId = studentInfo.batchId;
            GetGivenMarksResto responseToReturn = new GetGivenMarksResto();
            StudentMarksResDto studentMarksResDto = new StudentMarksResDto();
            MarksOfAStudentResDto marksOfAStudentResDto = new MarksOfAStudentResDto();
            try
            {

                List<MarksDistribution> marksDistributionList = marksDistributionRepo.getDistributedMarks(programId, semesterId, batchId, courseId);

                if (marksDistributionList.Count != 0)
                {
                    responseToReturn.heads = processHead(marksDistributionList);
                    responseToReturn.subHeads = processSubHead(marksDistributionList);
                    GetStudentsResponseDto studentList = utilityService.getStudentsOfACOurse(programId, semesterId, batchId, courseId);
                    responseToReturn.marksOfStudents = processStudentMarks_s(studentList.students, marksDistributionList, responseToReturn.subHeads);
                }
                return responseToReturn;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        private HeadResDto[] processHead(List<MarksDistribution> marksDistributionList)
        {
            int marksDistributionListSize = marksDistributionList.Count;

            HeadResDto[] headListToReturn = new HeadResDto[marksDistributionListSize];

            for (int i = 0; i < marksDistributionListSize; i++)
            {
                HeadResDto headResDto = new HeadResDto();
                var individualMarksDistribution = marksDistributionList.ElementAt(i);
                int headIdReq = individualMarksDistribution.headId;
                MarksHead marksHead = marksHeadRepo.getHead(headIdReq);
                headResDto.id = marksHead.Id;
                headResDto.name = marksHead.name;
                headListToReturn[i] = headResDto;
            }
            return headListToReturn;
        }

        private SubHeadForMarksResDto[] processSubHead(List<MarksDistribution> marksDistributionList)
        {
            int marksDistributionListSize = marksDistributionList.Count;
            List<SubHeadForMarksResDto> subHeadForMarksResDtoList = new List<SubHeadForMarksResDto>();

            

            for (int i = 0; i < marksDistributionListSize; i++)
            {
                var individualMarksDistribution = marksDistributionList.ElementAt(i);
                int headIdReq = individualMarksDistribution.headId;

                List<MarksSubHead> subHeadListTemp = marksRepository.getSubHeadByDistributionId(individualMarksDistribution.Id);
                if (subHeadListTemp.Count == 0)
                    continue;

                MarksHead marksHead = marksHeadRepo.getHead(headIdReq);

                for (int j = 0; j < subHeadListTemp.Count; j++)
                {
                    SubHeadForMarksResDto subHeadForMarksResDtoTemp = new SubHeadForMarksResDto();

                    subHeadForMarksResDtoTemp.id = subHeadListTemp.ElementAt(j).Id;
                    subHeadForMarksResDtoTemp.name = subHeadListTemp.ElementAt(j).name;
                    subHeadForMarksResDtoTemp.headName = marksHead.name;                

                    var marksTemp = marksRepository.getMarksByDistribution(individualMarksDistribution.Id, subHeadListTemp.ElementAt(j).Id);

                    if(marksTemp!=null)
                        subHeadForMarksResDtoTemp.examMarks = marksTemp.examMarks;

                    subHeadForMarksResDtoList.Add(subHeadForMarksResDtoTemp);
                }
            }

            return subHeadForMarksResDtoList.ToArray();
        }

        private StudentMarksResDto[] processStudentMarks_t(StudentResDto[] studentList, List<MarksDistribution> marksDistributionList, SubHeadForMarksResDto[] subHeads)
        {

            List<Marks> marks = getAllMarksByDistribution(marksDistributionList);
            int studentListLength = studentList.Length;
            List<StudentMarksResDto> studentMarksToReturn = new List<StudentMarksResDto>();

            for (int i = 0; i < studentListLength; i++)
            {
                StudentResDto student = studentList[i];
                int studenId = student.id;
                List<Marks> studnetMarksIndividual = new List<Marks>();

                for (int j = 0; j < marks.Count; j++)// create all subheads' marks of a student of a p,s,b,c
                {
                    if (marks[j].studentId == studenId)
                    {
                        studnetMarksIndividual.Add(marks[j]);
                    }
                }

                StudentMarksResDto studentMarks = new StudentMarksResDto();

                studentMarks.studentName = student.name;
                studentMarks.classRoll = student.classRoll;
                studentMarks.examRoll = student.examRoll;

                List<MarksOfAStudentResDto> marksOfAStudentList = new List<MarksOfAStudentResDto>();

                for (int j = 0; j < subHeads.Length; j++)
                {
                    int subHeadId = subHeads[j].id;
                    for (int k = 0; k < studnetMarksIndividual.Count; k++)
                    {
                        if (subHeadId == studnetMarksIndividual.ElementAt(k).subheadId)
                        {
                            MarksOfAStudentResDto studentMarksOnly = new MarksOfAStudentResDto();
                            studentMarksOnly.subHead = subHeads[j].name;
                            /*bool role = false;//..........................................................tis is to be modified when authentiation module is ready
                            if (role)// for student
                            {
                                var ifVisibleToStudentOrNot = checkIfMarksVisibleYoStudentOrNot(marksDistributionList, studnetMarksIndividual.ElementAt(k).marksDistributionId);

                                if (ifVisibleToStudentOrNot)
                                    studentMarksOnly.marks = "" + studnetMarksIndividual.ElementAt(k).obtainedMarks;
                                else if (!ifVisibleToStudentOrNot)
                                    studentMarksOnly.marks = "Invisible To Student";
                            }
                            else if (!role) //for teacher
                            {*/
                                studentMarksOnly.marks = ""+studnetMarksIndividual.ElementAt(k).obtainedMarks;
                           // }
                            marksOfAStudentList.Add(studentMarksOnly);
                        }
                    }
                    studentMarks.marksOfAllSubHeads = marksOfAStudentList.ToArray();
                }
                studentMarksToReturn.Add(studentMarks);
            }
            return studentMarksToReturn.ToArray();
        }

        private StudentMarksResDto[] processStudentMarks_s(StudentResDto[] studentList, List<MarksDistribution> marksDistributionList, SubHeadForMarksResDto[] subHeads)
        {

            List<Marks> marks = getAllMarksByDistribution(marksDistributionList);
            int studentListLength = studentList.Length;
            List<StudentMarksResDto> studentMarksToReturn = new List<StudentMarksResDto>();

            for (int i = 0; i < studentListLength; i++)
            {
                StudentResDto student = studentList[i];
                int studenId = student.id;
                List<Marks> studnetMarksIndividual = new List<Marks>();

                for (int j = 0; j < marks.Count; j++)// create all subheads' marks of a student of a p,s,b,c
                {
                    if (marks[j].studentId == studenId)
                    {
                        studnetMarksIndividual.Add(marks[j]);
                    }
                }

                StudentMarksResDto studentMarks = new StudentMarksResDto();

                studentMarks.studentName = student.name;
                studentMarks.classRoll = student.classRoll;
                studentMarks.examRoll = student.examRoll;

                List<MarksOfAStudentResDto> marksOfAStudentList = new List<MarksOfAStudentResDto>();

                for (int j = 0; j < subHeads.Length; j++)
                {
                    int subHeadId = subHeads[j].id;
                    for (int k = 0; k < studnetMarksIndividual.Count; k++)
                    {
                        if (subHeadId == studnetMarksIndividual.ElementAt(k).subheadId)
                        {
                            MarksOfAStudentResDto studentMarksOnly = new MarksOfAStudentResDto();
                            studentMarksOnly.subHead = subHeads[j].name;
                            var ifVisibleToStudentOrNot = checkIfMarksVisibleYoStudentOrNot(marksDistributionList, studnetMarksIndividual.ElementAt(k).marksDistributionId);
                            if (ifVisibleToStudentOrNot==true)
                                studentMarksOnly.marks = "" + studnetMarksIndividual.ElementAt(k).obtainedMarks;
                            else if (ifVisibleToStudentOrNot==false)
                                studentMarksOnly.marks = "Invisible";
                            
                            marksOfAStudentList.Add(studentMarksOnly);
                        }
                    }
                    studentMarks.marksOfAllSubHeads = marksOfAStudentList.ToArray();
                }
                studentMarksToReturn.Add(studentMarks);
            }
            return studentMarksToReturn.ToArray();
        }

        private List<Marks> getAllMarksByDistribution(List<MarksDistribution> marksDistributionList)
        {
            int marksDistributionListCount = marksDistributionList.Count;

            List<Marks> marksFromDb = new List<Marks>();

            for (int i = 0; i < marksDistributionListCount; i++)
            {
                var marksDistribution = marksDistributionList.ElementAt(i);
                List<Marks> marksFromDbTemp = marksRepository.getMarks(marksDistribution.Id);
                marksFromDb.AddRange(marksFromDbTemp);

            }

            return marksFromDb;
        }

        private bool checkIfMarksVisibleYoStudentOrNot(List<MarksDistribution> marksDistributionList,int marksDistributionId)
        {
            bool temp = true;

            for(int i = 0; i < marksDistributionList.Count; i++)
            {
                if (marksDistributionList.ElementAt(i).Id != marksDistributionId)                
                    continue;                 

                else if(marksDistributionList.ElementAt(i).Id == marksDistributionId){
                    temp= marksDistributionList.ElementAt(i).isVisibleToStudent;

                }
            }
            return temp;
        }

        public GetGivenMarksToEditResDto getGivenMarks(int programId, int semesterId, int batchId, int courseId, int headId, int subheadId)
        {
            GetGivenMarksToEditResDto getGivenMarksToReturn= new GetGivenMarksToEditResDto();
            try
            {
                var marksDistribution = marksDistributionRepo.GetMarksDistribution(programId,semesterId,batchId,courseId,headId);
                List<Marks> marks = marksRepository.getMarks(marksDistribution.Id, subheadId);

                if (marks.Count != 0)//when data is picked up during give marks
                {
                    getGivenMarksToReturn.isFinallYSubmitted = marksDistribution.isFinallySubmitted;

                    getGivenMarksToReturn.examMarks = marks.ElementAt(0).examMarks;
                }

                
                
                GetStudentsResponseDto studentList = utilityService.getStudentsOfACOurse(programId, semesterId, batchId, courseId);

                GetObtainedMarksToEditResDto[] getObtainedMarksList = new GetObtainedMarksToEditResDto[studentList.students.Length];

                for (int i=0;i< studentList.students.Length; i++)
                {
                    GetObtainedMarksToEditResDto getObtainedMarks = new GetObtainedMarksToEditResDto();

                    var student = studentList.students[i];


                    if (marks.Count == 0)
                    {


                        getObtainedMarks.id = -1;//-1 will detect that,its have to be created in db
                        getObtainedMarks.studentClassRoll = student.classRoll;
                        getObtainedMarks.studentName = student.name;
                        getObtainedMarks.studentId = student.id;
                        getObtainedMarks.marks = 0;
                        getObtainedMarksList[i] = getObtainedMarks;
                            

                        
                    }

                    else
                    {
                        for (int j = 0; j < marks.Count; j++)
                        {
                            if (student.id == marks.ElementAt(j).studentId && marks.ElementAt(j).subheadId == subheadId)
                            {
                                getObtainedMarks.id = marks.ElementAt(j).Id;
                                getObtainedMarks.studentClassRoll = student.classRoll;
                                getObtainedMarks.studentName = student.name;
                                getObtainedMarks.marks = marks.ElementAt(j).obtainedMarks;

                                getObtainedMarksList[i] = getObtainedMarks;
                            }

                        }
                    }

                    
                }

                getGivenMarksToReturn.obtainedMarks = getObtainedMarksList;

                return getGivenMarksToReturn;

            }

            catch(Exception e)
            {
                throw e;
            }
        }

        public void saveEditedMarks(SaveEditedMarksResDto saveEditedMarksResDto,string teacherId)
        {
            try
            {
                List<Marks> marksToCreate=new List<Marks>();
                Marks[] marks = new Marks[saveEditedMarksResDto.marksToEdit.Length];
                for(int i=0;i< saveEditedMarksResDto.marksToEdit.Length; i++)
                {
                    if (saveEditedMarksResDto.marksToEdit[i].marksId == -1)
                    {
                        var individualMarks = saveEditedMarksResDto.marksToEdit[i];
                        Marks marksTemp = new Marks();
                        marksTemp.examMarks = saveEditedMarksResDto.examMarks;
                        marksTemp.obtainedMarks = individualMarks.obtainedMarks;
                        marksTemp.marksDistributionId = saveEditedMarksResDto.marksDistributionId;
                        marksTemp.subheadId = saveEditedMarksResDto.subheadId;
                        marksTemp.studentId = individualMarks.studentId;
                        marksTemp.marksGiverId = teacherId;
                        marksToCreate.Add(marksTemp);
                        continue;
                    }

                    else
                    {
                        Marks mark = new Marks();
                        mark.obtainedMarks = saveEditedMarksResDto.marksToEdit[i].obtainedMarks;
                        mark.Id = saveEditedMarksResDto.marksToEdit[i].marksId;
                        marks[i] = mark;
                    }

                    
                }
                if (saveEditedMarksResDto.marksToEdit[0].marksId == -1)
                {
                    marksRepository.saveGivenMarks(marksToCreate.ToArray());
                }
                else if(saveEditedMarksResDto.marksToEdit[0].marksId != -1)
                    marksRepository.saveEditedMarks(marks);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public AllHeadsMarksStatusOfACourseGivingInfoResDto getHeadsMarksGivingInfoOfACourse(int programId, int semesterId, int batchId, int courseId)
        {
            try
            {
                AllHeadsMarksStatusOfACourseGivingInfoResDto responseToReturn = new AllHeadsMarksStatusOfACourseGivingInfoResDto();

                //......get all distributions(by heads) of a course
                List<MarksDistribution> marksDistributionList = marksDistributionRepo.getDistributedMarks(programId, semesterId, batchId, courseId);

                List<IndividualHeadMarksGivingInfoResDto> marksGivingInfoList = new List<IndividualHeadMarksGivingInfoResDto>();
                for (int i = 0; i < marksDistributionList.Count; i++)
                {

                    if (i == 0)
                    {
                        var courseInfo = utilityService.getCourse(marksDistributionList.ElementAt(i).courseId);
                        responseToReturn.courseId = courseInfo.id;
                        responseToReturn.courseName = courseInfo.name;
                        responseToReturn.ifAlreadyFinallySubmitted= marksDistributionRepo.checkIfFinallySubmitted(programId, semesterId, batchId, courseId);
                    }

                    IndividualHeadMarksGivingInfoResDto tempResDto = new IndividualHeadMarksGivingInfoResDto();

                    var marksDistribution = marksDistributionList.ElementAt(i);
                    var head = marksHeadRepo.getHead(marksDistribution.headId);

                    tempResDto.headId = head.Id;
                    tempResDto.name = head.name;
                    //.....check if marks is given
                    tempResDto.isMarksGiven = marksRepository.checkIfMarksIsGivenOfAHead(marksDistribution.Id);
                    marksGivingInfoList.Add(tempResDto);
                }
                responseToReturn.headMarksInfo = marksGivingInfoList.ToArray();
                return responseToReturn;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void submitFinally(int programId, int semesterId, int batchId, int courseId)
        {
            try
            {
                marksDistributionRepo.submitFinally(programId, semesterId, batchId, courseId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public AllFinalSubmissionResDto getFinalSubmissionInfoOfAllCourses(int programId, int semesterId, int batchId)
        {
            try
            {
                var allCoursesList = utilityService.getAllCoursesOfASemester(programId, semesterId, batchId);

                AllFinalSubmissionResDto responseToReturn = new AllFinalSubmissionResDto();

                List<IndividualFinalSubmissionOfACoaurseResDto> finalSubmissionList = new List<IndividualFinalSubmissionOfACoaurseResDto>();

                for (int i = 0; i < allCoursesList.Count; i++)
                {
                    IndividualFinalSubmissionOfACoaurseResDto tempSubmissionInfo = new IndividualFinalSubmissionOfACoaurseResDto();
                    tempSubmissionInfo.courseId = allCoursesList.ElementAt(i).id;
                    tempSubmissionInfo.courseName = allCoursesList.ElementAt(i).name;
                    tempSubmissionInfo.isFinallySubmitted = marksDistributionRepo.checkIfFinallySubmitted(programId, semesterId, batchId, allCoursesList.ElementAt(i).id);
                    finalSubmissionList.Add(tempSubmissionInfo);
                }
                responseToReturn.submissionInfos = finalSubmissionList.ToArray();

                return responseToReturn;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool checkIfAllCourseAreFinallySubmitted(int programId, int semesterId, int batchId)
        {
            try
            {
                AllFinalSubmissionResDto finalSubmissionInfo = getFinalSubmissionInfoOfAllCourses(programId, semesterId, batchId);

                int counter = 0;
                for (int i = 0; i < finalSubmissionInfo.submissionInfos.Length; i++)
                {
                    var submissionInfoTemp = finalSubmissionInfo.submissionInfos[i];
                    if (submissionInfoTemp.isFinallySubmitted == true)
                        counter++;


                }

                if (counter == finalSubmissionInfo.submissionInfos.Length)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool checkIfMarksIsGiven(int programId, int semesterId, int batchId, int courseId, int headId, int subheadId)
        {
            try
            {
                int marksDistributionId = marksDistributionRepo.GetMarksDistributionId(programId, semesterId, batchId, courseId, headId);

                if (marksDistributionId == -1)
                    return false;

                Marks marks = marksRepository.getMarksByDistribution(marksDistributionId, subheadId);
                if (marks != null)
                    return true;
                else
                    return false;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public AllStudentsResultResDto getCompleteResultOfAllStudents(int programId, int semesterId, int batchId)
        {
            AllStudentsResultResDto responseToReturn = new AllStudentsResultResDto();
            try
            {
                //.............................has to be changed wile integrating .............................................................................

                int lastSemesterIdWhereallCoursesAreFinallYSubmitted = semesterId;
                semesterId = lastSemesterIdWhereallCoursesAreFinallYSubmitted;

                var semester = utilityRepository.getSemesterBySemesterId(semesterId);
                var studentSemesterList = utilityRepository.getStudentsAllSemester(programId, semester.SemesterNo);

                List< AllStudentsResultResDto > studentResultListAllSemester=new List<AllStudentsResultResDto>();

                for (int semesterCount = 0; semesterCount < studentSemesterList.Count; semesterCount++)
                {
                    int semesterIdTemp = studentSemesterList.ElementAt(semesterCount).Id;
                    AllStudentsResultResDto resultOfASemesterTemp = getResultOfALlStudentOfASemester(programId, semesterIdTemp, batchId);
                    studentResultListAllSemester.Add(resultOfASemesterTemp);
                }

                AllStudentsResultResDto latestSemesterResult = new AllStudentsResultResDto();

                for (int i = 0; i < studentResultListAllSemester.Count; i++)
                {
                    if (studentResultListAllSemester.ElementAt(i).semesterId == semesterId)
                    {
                        latestSemesterResult = studentResultListAllSemester.ElementAt(i);
                        break;
                    }
                }

                studentResultListAllSemester = processGPAOfEachSemester(studentResultListAllSemester, programId,semesterId, batchId);// 
                responseToReturn = processFinalResult(latestSemesterResult, studentResultListAllSemester);

                return setAllValuesToTwoDecimal(responseToReturn);


            }
            catch (Exception e)
            {
                throw e;
            }
             }

        //................. result process will start from here.............
        //get all courses of a program,semester & batch
        //get all students of program,semester,batch
        //-run loop for all student
        //-- run loop for courses
        // count GPA for that course


        // 4 tar moddhe jodi 3 ta course ney, baki 1tar jonno error handle baki ase
        private AllStudentsResultResDto getResultOfALlStudentOfASemester(int programId,int semesterId,int batchId)
        {
            try
            {
                AllStudentsResultResDto responseToReturn = new AllStudentsResultResDto();

                var courseList = utilityService.getAllCoursesOfASemester(programId, semesterId, batchId);
                var studentList = utilityService.getStudentsOfASemester(programId, semesterId, batchId);


                List<IndividualResultResDto> eachStudentResultList = new List<IndividualResultResDto>();
                for (int studentCount = 0; studentCount < studentList.students.Length; studentCount++)
                {
                    var studentTemp = studentList.students.ElementAt(studentCount);

                    IndividualResultResDto eachStudentResult = new IndividualResultResDto();

                    List<IndividualCourseResultResDto> courseResultList = new List<IndividualCourseResultResDto>();
                    for (int courseCount = 0; courseCount < courseList.Count; courseCount++)
                    {
                        IndividualCourseResultResDto individualCourseResultResDto = new IndividualCourseResultResDto();
                        var courseTemp = courseList.ElementAt(courseCount);

                        double totalMarksOfCourse = countMarksOfACourse(programId, semesterId, batchId, courseTemp.id, studentTemp.id);
                        individualCourseResultResDto.courseId = courseTemp.id;
                        individualCourseResultResDto.courseName = courseTemp.name;
                        if (totalMarksOfCourse != -1 && totalMarksOfCourse != -2)
                        {
                            double GPAOfCourse = findGPABasedOnMarks(totalMarksOfCourse);
                            individualCourseResultResDto.GPA = GPAOfCourse;
                        }
                        
                        else if (totalMarksOfCourse == -1 || totalMarksOfCourse != -2)
                        {
                            individualCourseResultResDto.GPA = -1;//-1 when student does not have he course or marks is not given
                        }                                         

                        courseResultList.Add(individualCourseResultResDto);
                        
                    }
                    eachStudentResult.id = studentTemp.id;
                    eachStudentResult.studentName = studentTemp.name;
                    eachStudentResult.classRoll = studentTemp.classRoll;
                    eachStudentResult.examRoll = studentTemp.examRoll;
                    eachStudentResult.result = courseResultList.ToArray();

                    eachStudentResultList.Add(eachStudentResult);

                }
                var programTemp = utilityRepository.getProgramByProgramId(programId);
                responseToReturn.programId = programTemp.Id;
                responseToReturn.programName = programTemp.ProgramName;
                var semesterTemp = utilityRepository.getSemesterBySemesterId(semesterId);
                responseToReturn.semesterId = semesterTemp.Id;
                responseToReturn.semesterName = semesterTemp.SemesterNo;
                var batchTemp = utilityRepository.getBatch(programTemp.Id, semesterTemp.Id);
                responseToReturn.batchId = batchTemp.Id;
                responseToReturn.batchName = "Batch: "+ batchTemp.BatchNo;

                List<String> courseToResponse = new List<String>();

                for (int temp = 0; temp < courseList.Count; temp++)
                {
                    courseToResponse.Add(courseList.ElementAt(temp).name);
                }
                responseToReturn.courses = courseToResponse.ToArray();
                responseToReturn.results = eachStudentResultList.ToArray();

                return responseToReturn;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private double countMarksOfACourse(int programId,int semesterId,int batchId,int courseId,int studentId)
        {
            var marksDistributionList = getDistributedMarksPartially(programId, semesterId, batchId, courseId);  //1 marks distribution for each head

            double finalMarks = 0.00;

            for (int marksDistributionCount = 0; marksDistributionCount
                 < marksDistributionList.distributedMarks.Length;
                marksDistributionCount++)
            {
                var marksDistributionTemp = marksDistributionList.distributedMarks[marksDistributionCount];
                var marksListOfAllSubHeadOfAStudent = marksRepository.getMarksOfAHeadOfAllSubHeadOfAStudent(marksDistributionTemp.id, studentId);
                if (marksListOfAllSubHeadOfAStudent == null)//when a course is not aloocated to a student
                {
                    return -1;
                }
                if (marksListOfAllSubHeadOfAStudent.Count == 0 )//when marks is not given for All sub head of a head
                {
                    return -2;
                }

                MarksDistribution marksDistribution = marksDistributionRepo.getDistributedMarks(marksDistributionTemp.id);

                

                double individualHeadmarks = countMarksOfAHead(marksDistribution, marksListOfAllSubHeadOfAStudent);

                finalMarks = finalMarks + individualHeadmarks;
            }
            return finalMarks;
        }

        private double countMarksOfAHead(MarksDistribution marksDistribution, List<Marks> marksListOfAllSubHeadOfAStudent)
        {
           if (marksDistribution.avarageOrBestCount == true)
                return countMarksBasedOnAvarageOfSubheads(marksDistribution.marksEvaluated, marksListOfAllSubHeadOfAStudent);

           if(marksDistribution.avarageOrBestCount == false)
                return countMarksBasedOnBestCountOfSubheads(marksDistribution.marksEvaluated, marksListOfAllSubHeadOfAStudent);
            return 0.00;
        }

        private double countMarksBasedOnAvarageOfSubheads(double weightOfHead, List<Marks> marksListOfAllSubHeadOfAStudent)
        {
            double solidMarks = 0;

            for (int i = 0; i < marksListOfAllSubHeadOfAStudent.Count; i++)
            {
                var marksTemp = marksListOfAllSubHeadOfAStudent.ElementAt(i);
                solidMarks = solidMarks + ((marksTemp.obtainedMarks/ marksTemp.examMarks)* weightOfHead);
            }
            double avarageOfMarks = solidMarks/marksListOfAllSubHeadOfAStudent.Count;

            return avarageOfMarks;
        }

        private double countMarksBasedOnBestCountOfSubheads(double weightOfHead, List<Marks> marksListOfAllSubHeadOfAStudent)
        {
            double bestMarks = 0.00;

            for (int i = 0; i < marksListOfAllSubHeadOfAStudent.Count; i++)
            {
                var marksTemp = marksListOfAllSubHeadOfAStudent.ElementAt(i);
                double solidMarks = (marksTemp.obtainedMarks*weightOfHead)/marksTemp.examMarks;

                if (solidMarks > bestMarks)
                    bestMarks = solidMarks;
            }

            return bestMarks;
        }

        private double findGPABasedOnMarks(double marks)
        {
            if (marks >= 80)
                return 4.00;
            if (marks >= 75 && marks < 80)
                return 3.75;
            if (marks >= 70 && marks < 75)
                return 3.50;
            if (marks >= 65 && marks < 70)
                return 3.25;
            if (marks >= 60 && marks < 65)
                return 3.00;
            if (marks >= 55 && marks < 60)
                return 2.75;
            if (marks >= 50 && marks < 55)
                return 2.50;
            if (marks >= 45 && marks < 50)
                return 2.25;
            if (marks >= 40 && marks < 45)
                return 2.25;
            if (marks < 40)//it means fails
                return 0.00;//it means fail
            return 0.00;
        }

        public IndividualStudentPromotionResDto[] getPassFailInfoOfStudents(int programId,int semesterId,int batchId)
        {
            List<IndividualStudentPromotionResDto> responseToReturn = new List<IndividualStudentPromotionResDto>();

            try
            {
                AllStudentsResultResDto resultList = getResultOfALlStudentOfASemester(programId, semesterId, batchId);


                for (int resultListCount = 0; resultListCount < resultList.results.Length; resultListCount++)
                {
                    IndividualStudentPromotionResDto individualStudentPromotion = new IndividualStudentPromotionResDto();
                    

                    var resultTemp = resultList.results[resultListCount];

                    individualStudentPromotion.studentId = resultTemp.id;
                    individualStudentPromotion.name = resultTemp.studentName;
                    individualStudentPromotion.examRoll = resultTemp.examRoll;
                    individualStudentPromotion.classRoll = resultTemp.classRoll;

                    bool isPassed = true;

                    for (var courseResultCount = 0; courseResultCount < resultTemp.result.Length; courseResultCount++)
                    {
                        var courseResultTemp = resultTemp.result[0];

                        if (courseResultTemp.GPA == -1)
                            continue;   // suppose student has taken 5 ourses from 6 courses, forthat course which student have not taken, CGPA will be -1
                        if (courseResultTemp.GPA == 0.00) // failing GPA is counted as 0.00
                        {
                            isPassed = false;
                            break;
                        }

                    }
                    individualStudentPromotion.isPassedAllCourses = isPassed;

                    responseToReturn.Add(individualStudentPromotion);
                }

                return responseToReturn.ToArray();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private bool checkIfPassedOrFailed(double marks)
        {
            if (marks >= 40.00)
                return true;

            else
            {
                return false;
            }
        }

        private List<AllStudentsResultResDto> processGPAOfEachSemester(List<AllStudentsResultResDto> allSemesterResultList,int programId,int semesterId,int batchId)
        {
            for (int semesterResultCount = 0; semesterResultCount < allSemesterResultList.Count; semesterResultCount++)//loop ..for all result of all semester from last semester to previous semesters
            {
                var allStudentResultTemp = allSemesterResultList.ElementAt(semesterResultCount);

                for (int studentCount = 0; studentCount < allStudentResultTemp.results.Length; studentCount++)
                {
                    var individualStudentResult = allStudentResultTemp.results.ElementAt(studentCount);

                    double SummationOfGPAOfAllCourses = 0.00;
                    int courseNoFlag = 0;
                    for (int individualStudentCoursesCount = 0;
                        individualStudentCoursesCount < individualStudentResult.result.Length;
                        individualStudentCoursesCount++)
                    {
                        
                        var individualCourseResult = individualStudentResult.result[individualStudentCoursesCount];

                        if (individualCourseResult.GPA == -1) // if student does not have a course,its GPA is -1
                            continue;
                        
                        SummationOfGPAOfAllCourses = SummationOfGPAOfAllCourses + individualCourseResult.GPA;//counting summation of GPA of all courses of a semester
                        courseNoFlag++;

                        int ifCourseWiseCGPAGiven = utilityService.checkIfCourseWiseGPAIsSaved(semesterId, batchId,
                            individualCourseResult.courseId, individualStudentResult.id);

                        //next two if else for saving result info in db for promotion
                        if (ifCourseWiseCGPAGiven==-1)
                            utilityService.saveCourseWiseGPAOfAStudent(semesterId, batchId, individualCourseResult.courseId, individualStudentResult.id,individualCourseResult.GPA);
                        else if (ifCourseWiseCGPAGiven == 0)
                            utilityService.editCourseWiseGPAOfAStudent(semesterId, batchId, individualCourseResult.courseId, individualStudentResult.id, individualCourseResult.GPA);

                    }

                    double GPATemp = SummationOfGPAOfAllCourses/courseNoFlag;
                    allSemesterResultList.ElementAt(semesterResultCount).results[studentCount].GPA = GPATemp;
                    //next two if else for saving result info in db for promotion
                    int ifPassedFailedInfoGiven = utilityService.checkIfPassedFailInfoIsSaved(semesterId, batchId,
                        individualStudentResult.id);

                    if (ifPassedFailedInfoGiven==-1)
                        utilityService.savePassFailInfoOfAStudnet(semesterId, batchId, individualStudentResult.id, GPATemp);
                    else if (ifPassedFailedInfoGiven == 0)
                        utilityService.editPassFailInfoOfAStudnet(semesterId, batchId, individualStudentResult.id, GPATemp);

                }
            }
            return setGPAAndCGPAZeroWhenFailedOneCourse(allSemesterResultList);
        }

        private List<AllStudentsResultResDto> setGPAAndCGPAZeroWhenFailedOneCourse(List<AllStudentsResultResDto> allSemesterResultList)
        {
            for (int semesterResultCount = 0; semesterResultCount < allSemesterResultList.Count; semesterResultCount++)//loop ..for all result of all semester from last semester to previous semesters
            {
                var allStudentResultTemp = allSemesterResultList.ElementAt(semesterResultCount);

                for (int studentCount = 0; studentCount < allStudentResultTemp.results.Length; studentCount++)
                {
                    var individualStudentResult = allStudentResultTemp.results.ElementAt(studentCount);

                    double SummationOfGPAOfAllCourses = 0.00;
                    int courseNoFlag = 0;
                    for (int individualStudentCoursesCount = 0;
                        individualStudentCoursesCount < individualStudentResult.result.Length;
                        individualStudentCoursesCount++)
                    {
                        var individualCourseResult = individualStudentResult.result[individualStudentCoursesCount];
                        if (individualCourseResult.GPA == 0.00)
                        {
                            individualStudentResult.GPA = 0.00;
                            individualStudentResult.CGPA = 0.00;
                            break;
                        }
                        
                    }
                }
            }
            return allSemesterResultList;
        }

        private AllStudentsResultResDto processFinalResult(AllStudentsResultResDto lastSemesterResult , List<AllStudentsResultResDto> allSemesterResultList)
        {
            for (int studentCount = 0; studentCount < lastSemesterResult.results.Length; studentCount++)
            {
                var individualStudentResult = lastSemesterResult.results[studentCount];
                double CGPA = countCGPAOfAStudent(allSemesterResultList, individualStudentResult.id);
                lastSemesterResult.results[studentCount].CGPA = CGPA;
            }
            return lastSemesterResult;
        }

        private double countCGPAOfAStudent(List<AllStudentsResultResDto> allSemesterResultList,int studentId)
        {
            double summationOfGPA = 0.00;
            double noOfSemester = 0.00;
            for (int semesterCount = 0; semesterCount < allSemesterResultList.Count; semesterCount++)
            {
                var semesterResultOfAllStudents = allSemesterResultList.ElementAt(semesterCount);
                for (int studentCount = 0; studentCount < semesterResultOfAllStudents.results.Length; studentCount++)
                {
                    var studentResultTemp = semesterResultOfAllStudents.results[studentCount];
                    if(studentResultTemp.id!= studentId)
                        continue;
                    if (studentResultTemp.id == studentId)
                    {
                        summationOfGPA = summationOfGPA + studentResultTemp.GPA;
                        noOfSemester++;
                        break;
                    }
                }
            }
            double CGPA = summationOfGPA/noOfSemester;
            return CGPA;
        }

        private AllStudentsResultResDto setAllValuesToTwoDecimal(AllStudentsResultResDto result)
        {
            for (int studentCount = 0; studentCount < result.results.Length; studentCount++)
            {
                var studentResultTemp = result.results[studentCount];

                result.results[studentCount].GPA= Math.Round(result.results[studentCount].GPA, 2);
                result.results[studentCount].CGPA = Math.Round(result.results[studentCount].CGPA, 2);

                for (int courseCount = 0; courseCount < studentResultTemp.result.Length; courseCount++)
                {
                    var courseResultTemp = studentResultTemp.result[courseCount];

                    result.results[studentCount].result[courseCount].GPA= Math.Round(result.results[studentCount].result[courseCount].GPA, 2);
                }
            }

            return result;
        }

        /*private void savingResultToDb(List<AllStudentsResultResDto> allSemesterResultList)
        {
            for (int semesterCounter = 0; semesterCounter < allSemesterResultList.Count; semesterCounter++)
            {
                var semesterResult = allSemesterResultList.ElementAt(semesterCounter);

                for (int studentCounter = 0; studentCounter < semesterResult.results.Length; studentCounter++)
                {
                    var individualStudentResult = semesterResult.results[studentCounter];


                }
            }
        }*/

    }
}