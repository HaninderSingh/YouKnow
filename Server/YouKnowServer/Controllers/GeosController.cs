using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Spatial;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using YouKnowServer.Models;

namespace YouKnowServer.Controllers
{
    public class GeosController : ApiController
    {
        private YouKnowEntities db = new YouKnowEntities();

        // GET: api/Geos
        public IHttpActionResult GetGeos()
        {
            var location = db.Geos;
            List<GeoModel> geomodels = new List<GeoModel>();
            foreach (var item in location)
            {
                GeoModel model = new GeoModel();
                model.Latitude = (double)item.Location.Latitude;
                model.Longitude = (double)item.Location.Longitude;
                model.Id = item.Id;
                model.Address = item.Address;
                geomodels.Add(model);
            }
            return Ok(geomodels);
        }

        [Route("api/GetNearByPlaces")]
        [HttpGet]
        public IHttpActionResult GetNearByPlaces(double lat, double longitude, int radius)
        {
            var sourcePoint = Common.CreatePoint(lat, longitude);

            var context = new GeoLocationContext();

            // find any locations within 5 kilometers ordered by distance
            List<GeoDistance> matches =
               db.Geos
                        .Where(loc => loc.Location.Distance(sourcePoint) < radius)
                        .OrderBy(loc => loc.Location.Distance(sourcePoint))
                        .Select(loc => new GeoDistance { Address = loc.Address, Distance = loc.Location.Distance(sourcePoint) }).ToList();


            return Ok(matches);
        }

        // PUT: api/Geos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGeo(GeoModel geoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {



                var locationExist = db.Geos.FirstOrDefault(w => w.Id == geoModel.Id);
                if (locationExist != null)
                {
                    var locationValue = Common.CreatePoint(geoModel.Latitude, geoModel.Longitude);
                    locationExist.Location = locationValue;
                }
                else
                {
                    var locationValue = Common.CreatePoint(geoModel.Latitude, geoModel.Longitude);
                    var location = new Geo();
                    location.Id = geoModel.Id;
                    location.Address = geoModel.Address;
                    location.Location = locationValue;
                    db.Geos.Add(location);
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GeoExists(int id)
        {
            return db.Geos.Count(e => e.Id == id) > 0;
        }
    }
}