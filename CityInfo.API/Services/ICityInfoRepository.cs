﻿using CityInfo.API.Entities;
using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        IEnumerable<City> GetCities();

        City GetCity(int cityId);

        IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId);

        PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId);

        

        bool CityExists(int cityId);
        

        void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);
    }
}
