using LocalizationUsingAcceptLangauage.WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalizationUsingAcceptLangauage.WebAPI.Localize;

namespace LocalizationUsingAcceptLangauage.WebAPI.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IStringLocalizer _localizer;

        public MoviesService(DatabaseContext databaseContext, IStringLocalizer<Resources> localizer)
        {
            _databaseContext = databaseContext;
            _localizer = localizer;
        }

        public async Task<IEnumerable<Movie>> Get()
        {
            var movies = _databaseContext.Movies.AsQueryable();
            movies.ToList().ForEach(x => x.Category = _localizer[x.Category]?.Value);
            return movies;
        }

        public async Task<IEnumerable<Movie>> Get(int[] ids)
        {
            var movies = _databaseContext.Movies.AsQueryable();

            if (ids != null && ids.Any())
                movies = movies.Where(x => ids.Contains(x.Id));
            var result = await movies.AsNoTracking().ToListAsync();

            result.ForEach(x => x.Category = _localizer[x.Category]?.Value);

            return result;
        }

        public async Task<Movie> Add(Movie movie)
        {
            await _databaseContext.Movies.AddAsync(movie);

            await _databaseContext.SaveChangesAsync();
            return movie;
        }
    }

    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> Get();
        Task<IEnumerable<Movie>> Get(int[] ids);

        Task<Movie> Add(Movie movie);
    }
}
