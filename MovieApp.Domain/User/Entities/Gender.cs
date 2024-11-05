namespace MovieApp.Domain.User.Entities;

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

    public static Gender GetGenderNum(int genderValue)
    {
        return genderValue switch
        {
            1 => Gender.Male,
            2 => Gender.Female,
            3 => Gender.Unknown,
            _ => throw new ArgumentException("Invalid gender description")
        };
    }
}