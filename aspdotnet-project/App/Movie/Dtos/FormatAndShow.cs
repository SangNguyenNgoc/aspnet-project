using aspdotnet_project.App.Show.Dtos;

namespace aspdotnet_project.App.Movie.Dtos;

public class FormatAndShow
{
    public long Id { get; set; }
    public string Caption { get; set; }
    public string Version { get; set; }
    public List<ShowResponseInFormat> Shows { get; set; }
    
}