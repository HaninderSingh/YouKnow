using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace YouKnowServer.Models
{
    public class GeoLocationContext : DbContext
    {
        public DbSet<GeoLocationModel> Locations { get; set; }
    }
    public class GeoLocationModel
    {
        public int Id { get; set; }
        public DbGeography Location { get; set; }
        public string Address { get; set; }
    }
    public class GeoModel
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
    }

    public class GeoDistance
    {
        public string Address { get; set; }

        public double? Distance { get; set; }
    }

    public class DisasterModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Media { get; set; }
        public string Description { get; set; }
        public string DisasterTypeName { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Contact { get; set; }
        public double? Distance { get; set; }
    }

    public class BloodModel
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Media { get; set; }
        public string Description { get; set; }
        public bool IsFulfilled { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string Contact { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Distance { get; set; }
    }

    public class MissingModel
    {
        public System.Guid Id { get; set; }
        public string MissingType { get; set; }
        public string Name { get; set; }
        public string Media { get; set; }
        public string Description { get; set; }
        public bool HasReturned { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Contact { get; set; }
        public double? Distance { get; set; }
    }

    public class DiseasesModel
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Media { get; set; }
        public string Description { get; set; }
        public DateTime? FirstIdentifiedOn { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Contact { get; set; }
        public string KeyFacts { get; set; }
        public string Outbreaks { get; set; }
        public string Transmission { get; set; }
        public string Symptoms { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public string Prevention { get; set; }
        public string ConfirmedCases { get; set; }
        public double? Distance { get; set; }
    }

    public class WantedModel
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Media { get; set; }
        public string Description { get; set; }
        public bool IsArrested { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Contact { get; set; }
    }
    public class GenericModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Media { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class GenericDataModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Media { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Contact { get; set; }
    }

    public class GenericModel2
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Media { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string TagModel2 { get; set; }
    }

    public class TagModel2
    {
        public Guid Id { get; set; }
       public string TagTypeMessage { get; set; }
        
    }
    public class CongestionModel
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Media { get; set; }
        public string Description { get; set; }
        public bool IsLifted { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Contact { get; set; }
        public double? Distance { get; set; }
    }
    public class CountModel
    {
        public int Type { get; set; }
        public int Count { get; set; }
    }
}