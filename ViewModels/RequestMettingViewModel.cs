using WebApplication_SRPFIQ.DTOs;
using WebApplication_SRPFIQ.Models;

namespace WebApplication_SRPFIQ.ViewModels
{
    public class RequestMettingViewModel
    {
        public RequestRelaiDTO RequestRelaiDTO { get; set; }
        public List<MeetingsDTO>? Meetings { get; set; } = new List<MeetingsDTO>();
        public List<MedicalNotesDTO>? MedicalNotes { get; set; } = new List<MedicalNotesDTO>();
        public QuestionnaireAnswersDTO QuestionnaireAnswersDTO { get; set; }
        public MaternalExperiencesDTO MaternalExperiencesDTO { get; set; }
        public UserDTO UserDTO { get; set; }
    }
}
