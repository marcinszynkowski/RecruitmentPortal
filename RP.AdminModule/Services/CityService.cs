using RP.AdminModule.Interfaces;
using RP.Data.Context;
using RP.Entities.Recruitment;
using System.Collections.Generic;
using System.Linq;

namespace RP.AdminModule.Services
{
    public class CityService : ICityService
    {
        #region Fields
        private readonly RPDbContext _db;
        #endregion

        #region Constructors
        public CityService(RPDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Get
        public City GetCityByName(string cityName)
        {
            return _db.Cities.Where(ck => ck.Name == cityName).FirstOrDefault();
        }

        public City GetCityById(int cityId)
        {
            return _db.Cities.Find(cityId);
        }

        public string GetCityName(int cityId)
        {
            return _db.Cities.Find(cityId).Name;
        }

        public List<City> GetAllCities()
        {
            return _db.Cities.ToList();
        }
        #endregion

        #region Set
        public void SetCityName(int cityId, string cityName)
        {
            City city = GetCityById(cityId);
            city.Name = cityName;

            _db.SaveChanges();
        }
        #endregion

        #region Create
        public void CreateCity(string cityName)
        {
            if (GetCityByName(cityName) == null)
            {
                City city = new City
                {
                    Name = cityName,
                };
                _db.Cities.Add(city);
                _db.SaveChanges();
            }
        }
        #endregion

        #region Delete
        public void DeleteCity(string cityName)
        {
            City city = GetCityByName(cityName);

            if (city != null)
            {
                _db.Cities.Remove(city);
                _db.SaveChanges();
            }
        }
        #endregion
    }
}
