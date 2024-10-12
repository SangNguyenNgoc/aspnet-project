using aspdotnet_project.App.Movie.Entities;
using aspdotnet_project.Context;
using Microsoft.EntityFrameworkCore;

namespace aspdotnet_project.App.Movie.Repositories;

public class MovieStatusRepository(MyDbContext context) : IMovieStatusRepository
{
    public async Task<List<MovieStatus>> FindByIdOrId(long id1, long id2)
    {
        return await context.MovieStatus
            .Include(ms => ms.Movies)
            .ThenInclude(mv => mv.Formats)
            .Include(ms => ms.Movies)
            .ThenInclude(mv => mv.Genres)
            .Where(ms => ms.Id == id1 || ms.Id == id2)
            .OrderBy(ms => ms.Id)
            .AsSplitQuery() 
            .ToListAsync();
    }
}