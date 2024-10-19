﻿using MovieApp.Domain.Movie.Entities;

namespace MovieApp.Domain.Movie.Repositories;

public interface IMovieStatusRepository
{
    Task<List<MovieStatus>> FindByIdOrId(long id1, long id2);
}