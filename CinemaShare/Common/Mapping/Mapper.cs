﻿using CinemaShare.Models;
using CinemaShare.Models.InputModels;
using CinemaShare.Models.JsonModels;
using CinemaShare.Models.ViewModels;
using Data.Enums;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CinemaShare.Common.Mapping
{
    public class Mapper : IMapper
    {
        private TTo MapSimilarProperties<TFrom, TTo>(TFrom fromObject)
             where TFrom : class
             where TTo : class, new()
        {
            TTo viewModel = new TTo();//Activator.CreateInstance<TTo>();
            if (fromObject != null)
            {
                PropertyInfo[] props = fromObject.GetType().GetProperties();
                var modelType = viewModel?.GetType();
                foreach (var prop in props)
                {
                    var modelProperty = modelType?.GetProperty(prop.Name);
                    if (modelProperty?.PropertyType?.Equals(prop.PropertyType) ?? false)
                    {
                        modelProperty.SetValue(viewModel, prop.GetValue(fromObject));
                    }
                }
            }
            return viewModel;
        }
        public IEnumerable<FilmCardViewModel> MapToFilmCardViewModel(IEnumerable<FilmData> rawFilms)
        {
            return rawFilms.Select(x => new FilmCardViewModel
            {
                Title = x.Title,
                Genres = string.Join(", ", x.Genre.Select(a => a.Genre.ToString())),
                Poster = x.Poster,
                Rating = Math.Round(x.Film.Rating, 1).ToString(),
                Id = x.FilmId
            });
        }

        public ExtendedFilmCardViewModel MapToExtendedFilmCardViewModel(FilmData filmData)
        {
            var viewModel = MapSimilarProperties<FilmData, ExtendedFilmCardViewModel>(filmData);
            if (filmData != null)
            {
                viewModel.Id = filmData.FilmId;
                viewModel.Genres = string.Join(", ", filmData.Genre.Select(a => a.Genre.ToString()));
                viewModel.Rating = Math.Round(filmData.Film.Rating, 1).ToString();
            }
            return viewModel;
        }

        public FilmDataViewModel MapToFilmDataViewModel(FilmData filmData)
        {
            var viewModel = MapSimilarProperties<FilmData, FilmDataViewModel>(filmData);
            if (filmData != null)
            {
                viewModel.Id = filmData.FilmId;
                viewModel.Genres = string.Join(", ", filmData.Genre.Select(a => a.Genre.ToString()));
                viewModel.Rating = Math.Round(filmData.Film.Rating, 1).ToString();
                viewModel.FilmProjections = filmData.Film.FilmProjection.ToList();
                viewModel.FilmReviews = filmData.Film.FilmReviews.ToList();
                viewModel.CreatedByUserId = filmData.Film.AddedByUserId;
            }
            return viewModel;
        }

        public FilmUpdateInputModel MapToFilmUpdateInputModel(FilmData filmData)
        {
            var viewModel = MapSimilarProperties<FilmData, FilmUpdateInputModel>(filmData);
            if (filmData != null)
            {
                viewModel.Genre = filmData.Genre.Select(x => x.Genre).ToList();
            }
            return viewModel;
        }

        public FilmInputModel MapToFilmInputModel(FilmJsonModel filmData)
        {
            if (filmData.Response == "False")
            {
                return new FilmInputModel { Error = filmData.Error };
            }

            var filmInputModel = new FilmInputModel
            {
                Title = filmData.Title,
                Director = filmData.Director,
                Cast = filmData.Actors,
                Description = filmData.Plot,
                TargetAudience = TargetAudience.All,
            };

            var posterUrl = filmData.Poster;
            posterUrl = posterUrl?.Substring(0, posterUrl.LastIndexOf('@') + 1) + "._V1_SY1000_SX675_AL_.jpg";
            filmInputModel.Poster = posterUrl;

            var genres = new List<Genre>();
            foreach (var genre in filmData.Genre.Split(", "))
            {
                foreach (Genre type in Enum.GetValues(typeof(Genre)))
                {
                    if (genre == type.ToString())
                    {
                        genres.Add(type);
                        break;
                    }
                }
            }
            if (genres.Count == 0)
            {
                genres.Add(Genre.Uncategorized);
            }
            filmInputModel.Genre = genres;

            DateTime releaseDate;
            if (!DateTime.TryParse(filmData.Released, out releaseDate))
            {
                releaseDate = DateTime.Now;
            }
            filmInputModel.ReleaseDate = releaseDate;

            int runtime;
            if (!int.TryParse(filmData.Runtime.Split(" ")?[0], out runtime))
            {
                runtime = 0;
            }
            filmInputModel.Runtime = runtime;

            return filmInputModel;
        }

        public FilmData MapToFilmData(FilmInputModel input, Film film)
        {
            var model = MapSimilarProperties<FilmInputModel, FilmData>(input);
            model.FilmId = film.Id;
            model.Film = film;
            model.Genre = input.Genre.Select(x => new GenreType() { Genre = x }).ToList();
            return model;
        }

        public FilmData MapToFilmData(FilmUpdateInputModel input)
        {
            var model = MapSimilarProperties<FilmUpdateInputModel, FilmData>(input);
            model.Genre = input.Genre.Select(x => new GenreType() { Genre = x }).ToList();
            return model;
        }

        public CinemaDataViewModel MapToCinemaDataViewModel(Cinema cinema)
        {
            var viewModel = MapSimilarProperties<Cinema, CinemaDataViewModel>(cinema);
            if (cinema != null)
            {
                viewModel.Mananger = cinema.Manager.UserName;
            }
            return viewModel;
        }

        public CinemaCardViewModel MapToCinemaCardViewModel(Cinema rawCinema)
        {
            return new CinemaCardViewModel
            {
                City = rawCinema.City,
                Country = rawCinema.Country,
                Name = rawCinema.Name,
                Id = rawCinema.Id,
                Mananger = rawCinema.Manager.UserName
            };
        }

        public CinemaInputModel MapToCinemaUpdateInputModel(Cinema cinema)
        {
            var viewModel = MapSimilarProperties<Cinema, CinemaInputModel>(cinema);
            return viewModel;
        }

        public Cinema MapToCinemaData(CinemaInputModel input)
        {
            var model = MapSimilarProperties< CinemaInputModel, Cinema>(input);
            return model;
        }
    }
}
