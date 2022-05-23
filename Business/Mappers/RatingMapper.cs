using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Business.Mappers.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mappers
{
    public class RatingMapper : IRatingMapper
    {
        public RatingOutDto EntityToDto(Rating entity)
        {
            return new RatingOutDto(entity);
        }

        public Rating EntityFromDto(RatingInDto dto)
        {
            return new Rating
            {
                PlanetRating = dto.PlanetRating,
                PeopleRating = dto.PeopleRating,
                AnimalsRating = dto.AnimalsRating,
                Description = dto.Description
            };
        }

        public Rating CopyFromDto(Rating entity, RatingInDto dto)
        {
            entity.PlanetRating = dto.PlanetRating;
            entity.PeopleRating = dto.PeopleRating;
            entity.AnimalsRating = dto.AnimalsRating;
            entity.Description = dto.Description;

            return entity;
        }
    }
}
