using AutoMapper;
using Elfland.Dapr.Application.Queries.WeatherForecastQueries;
using Elfland.Dapr.Data.Entities;

namespace Elfland.Dapr.Configurations;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<WeatherForecast, GetWeatherForecastResponse>();
    }
}
