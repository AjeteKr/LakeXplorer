using Intersoft.Crosslight.Containers;

namespace LakeXplorerBlazor.Data
{
    // Represents the result of a user registration attempt.
    public class RegistrationResultDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}