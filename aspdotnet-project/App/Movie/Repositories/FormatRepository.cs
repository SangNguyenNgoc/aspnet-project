using aspdotnet_project.Context;

namespace aspdotnet_project.App.Movie.Repositories;

public class FormatRepository(MyDbContext context) : IFormatRepository
{
    private readonly MyDbContext _context = context;

    public async Task<Entities.Format?> GetMovieById(long id)
    {
        return await _context.Formats.FindAsync(id);
    }
}