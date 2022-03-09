using System.Collections.Generic;
using RP.Entities.Recruitment;

namespace RP.AdminModule.Interfaces
{
    public interface ICityService
    {
        void CreateCity(string cityName);
        void DeleteCity(string cityName);
        List<City> GetAllCities();
        City GetCityById(int cityId);
        City GetCityByName(string cityName);
        string GetCityName(int cityId);
        void SetCityName(int cityId, string cityName);
    }
}