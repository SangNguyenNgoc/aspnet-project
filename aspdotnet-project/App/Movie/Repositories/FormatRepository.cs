using aspdotnet_project.Context;

namespace aspdotnet_project.App.Movie.Repositories;

public class FormatRepository(MyDbContext context) : IFormatRepository
{
    public async Task<Entities.Format?> GetMovieById(long id)
    {
        return await context.Formats.FindAsync(id);
    }
}