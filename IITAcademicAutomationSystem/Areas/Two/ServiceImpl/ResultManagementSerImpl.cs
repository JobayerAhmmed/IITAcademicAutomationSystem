using IITAcademicAutomationSystem.Areas.Two.Service;
using System;
using System.Collections.Generic;
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

        public void distributeMarks(DistributeMarksFinalReqDto distributeMarksFinalReqDto)
        {



            try
            {
                List<MarksDistribution> marksDistributionToSave = processMarksDistribution(distributeMarksFinalReqDto);

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

        private List<MarksDistribution> processMarksDistribution(DistributeMarksFinalReqDto distributeMarksFinalReqDto)
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
                marksDistribution.marksDistributorId = "" + utilityService.getIdOfLoggedInTeacher();//need to be changed

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

        public void editDistributedMarks(EditedDistributeMarksReqDto editedDistributeMarksReqDto)
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
                        marksDistributionToSave.marksDistributorId = "" + utilityService.getIdOfLoggedInTeacher(); ;

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

        public void saveGivenMarks(GiveMarksReqDto giveMarksReqDto)
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
                    marks.marksGiverId = "" + utilityService.getIdOfLoggedInTeacher();
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

        public GetGivenMarksResto getGivenMarks_s(int courseId)
        {
            var studentId = utilityService.getIdOfLoggedInStudent();
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
                            if (ifVisibleToStudentOrNot)
                                studentMarksOnly.marks = "" + studnetMarksIndividual.ElementAt(k).obtainedMarks;
                            else if (!ifVisibleToStudentOrNot)
                                studentMarksOnly.marks = "Invisible To Student";
                            
                            studentMarksOnly.marks = "" + studnetMarksIndividual.ElementAt(k).obtainedMarks;
                            
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
                int marksDistributionId = marksDistributionRepo.GetMarksDistributionId(programId,semesterId,batchId,courseId,headId);
                List<Marks> marks = marksRepository.getMarks(marksDistributionId, subheadId);

                if (marks.Count == 0)
                {
                    return getGivenMarksToReturn;
                }

                getGivenMarksToReturn.examMarks = marks.ElementAt(0).examMarks;
                GetStudentsResponseDto studentList = utilityService.getStudentsOfACOurse(programId, semesterId, batchId, courseId);

                GetObtainedMarksToEditResDto[] getObtainedMarksList = new GetObtainedMarksToEditResDto[studentList.students.Length];

                for (int i=0;i< studentList.students.Length; i++)
                {
                    GetObtainedMarksToEditResDto getObtainedMarks = new GetObtainedMarksToEditResDto();

                    var student = studentList.students[i];

                    for(int j=0;j< marks.Count; j++)
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

                getGivenMarksToReturn.obtainedMarks = getObtainedMarksList;

                return getGivenMarksToReturn;

            }

            catch(Exception e)
            {
                throw e;
            }
        }

        public void saveEditedMarks(SaveEditedMarksResDto saveEditedMarksResDto)
        {
            try
            {
                Marks[] marks = new Marks[saveEditedMarksResDto.marksToEdit.Length];
                for(int i=0;i< saveEditedMarksResDto.marksToEdit.Length; i++)
                {
                    Marks mark = new Marks();
                    mark.obtainedMarks = saveEditedMarksResDto.marksToEdit[i].marks;
                    mark.Id = saveEditedMarksResDto.marksToEdit[i].id;
                    marks[i] = mark;
                }

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
    }
}