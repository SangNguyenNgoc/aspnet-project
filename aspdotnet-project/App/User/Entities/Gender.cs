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

    public static Gender GetGenderNum(string genderDescription){
        return genderDescription switch
        {
            "Nam" => Gender.Male,
            "Nữ" => Gender.Female,
            "Khác" => Gender.Unknown,
            _ => throw new ArgumentException("Invalid gender description"),
        };
    }
}
