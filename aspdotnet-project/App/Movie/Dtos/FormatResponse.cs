using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace aspdotnet_project.App.Movie.Dtos;

public class FormatResponse
{
    public long Id { get; set; }
    public string Caption { get; set; }
    public string Version { get; set; }
}