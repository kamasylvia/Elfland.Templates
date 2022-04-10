using AutoMapper;
using Elfland.WebApi.Application.Queries.WeatherForecastQueries;
using Elfland.WebApi.Data.Entities;

namespace Elfland.WebApi.Configurations;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<WeatherForecast, GetWeatherForecastResponse>();
    }
}
