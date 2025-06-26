using Microsoft.AspNetCore.Mvc;
using TestApp.Interfaces;
using TestApp.Module;

namespace TestApp.Steps
{
    public class StudyGroupComponent
    {
        private readonly IStudyGroupRepository _studyGroupRepository;

        public StudyGroupComponent(IStudyGroupRepository studyGroupRepository)
        {
            _studyGroupRepository = studyGroupRepository;
        }

        public async Task<IActionResult> CreateStudyGroup(StudyGroup studyGroup)
        {
            try
            {
                await _studyGroupRepository.CreateStudyGroup(studyGroup);
                return new OkResult();
            }
            catch (InvalidOperationException ex)
            {
                return new ConflictObjectResult(ex.Message);
            }
        }

        public async Task<IActionResult> GetStudyGroups()
        {
            var studyGroups = await _studyGroupRepository.GetStudyGroups();
            return new OkObjectResult(studyGroups);
        }

        public async Task<IActionResult> SearchStudyGroups(string subject)
        {
            var studyGroups = await _studyGroupRepository.SearchStudyGroups(subject);
            return new OkObjectResult(studyGroups);
        }

        public async Task<IActionResult> JoinStudyGroup(int studyGroupId, int userId)
        {
            await _studyGroupRepository.JoinStudyGroup(studyGroupId, userId);
            return new OkResult();
        }

        public async Task<IActionResult> LeaveStudyGroup(int studyGroupId, int userId)
        {
            await _studyGroupRepository.LeaveStudyGroup(studyGroupId, userId);
            return new OkResult();
        }
    }
}