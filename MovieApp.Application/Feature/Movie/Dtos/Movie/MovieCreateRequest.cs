using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MovieApp.Application.Feature.Movie.Dtos;

public class MovieCreateRequest
{

    [Required(ErrorMessage = "Tên phim là bắt buộc.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Tên phụ là bắt buộc.")]
    public string SubName { get; set; }

    [Required(ErrorMessage = "Mô tả là bắt buộc.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Giới hạn tuổi là bắt buộc.")]
    public int AgeRestriction { get; set; }

    [Required(ErrorMessage = "Đạo diễn là bắt buộc.")]
    public string Director { get; set; }

    [Required(ErrorMessage = "Ngày phát hành là bắt buộc.")]
    public DateOnly ReleaseDate { get; set; }

    [Required(ErrorMessage = "Ngày kết thúc là bắt buộc.")]
    public DateOnly EndDate { get; set; }

    [Required(ErrorMessage = "Thời lượng chiếu là bắt buộc.")]
    public int RunningTime { get; set; }

    [Required(ErrorMessage = "Poster là bắt buộc.")]
    public IFormFile Poster { get; set; }

    [Required(ErrorMessage = "Nhà sản xuất là bắt buộc.")]
    public string Producer { get; set; }

    [Required(ErrorMessage = "Trailer là bắt buộc.")]
    public string Trailer { get; set; }

    [Required(ErrorMessage = "Poster ngang là bắt buộc.")]
    public IFormFile HorizontalPoster { get; set; }

    [Required(ErrorMessage = "Ngôn ngữ là bắt buộc.")]
    public string Language { get; set; }

    [Required(ErrorMessage = "Diễn viên là bắt buộc.")]
    public string Performers { get; set; }

    [Required(ErrorMessage = "Trạng thái là bắt buộc.")]
    public long Status { get; set; }
    
    [Required(ErrorMessage = "Thể loại là bắt buộc.")]
    public List<long> Genres { get; set; }
    
    [Required(ErrorMessage = "Định dạng là bắt buộc.")]
    public List<long> Formats { get; set; }
}