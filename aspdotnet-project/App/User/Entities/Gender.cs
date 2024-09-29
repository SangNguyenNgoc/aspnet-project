namespace aspdotnet_project.App.User.Entities;

public enum Gender
{
    Male = 1,
    Female = 2,
    Unknown = 3
}

public class GenderUtil
{
    public static string GetGenderDescription(Gender gender)
    {
        return gender switch
        {
            Gender.Male => "Nam",
            Gender.Female => "Nữ",
            Gender.Unknown => "Khác",
            _ => "Không xác định"
        };
    }
}
