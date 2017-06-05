using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using YouKnowServer.Models;

namespace YouKnowServer.Controllers
{
    public class CongestionsController : ApiController
    {
        private YouKnowEntities db = new YouKnowEntities();


        // GET: api/GetCongestions
        [Route("api/GetCongestions")]
        [HttpGet]
        public IHttpActionResult GetCongestions(double lattitude, double longitude)
        {
            var sourcePoint = Common.CreatePoint(lattitude, longitude);

            // var context = new GeoLocationContext();

            // find any locations within 5 kilometers ordered by distance
            List<CongestionModel> matches =
               db.Congestions
                        .Where(w => w.Location.Distance(sourcePoint) < 5000 && w.IsActive)
                        .OrderBy(w => w.Location.Distance(sourcePoint))
                        .Select(w => new CongestionModel
                        {
                            Id = w.Id,
                            Name = w.Name,
                            Media = w.Media,
                            IsActive = w.IsActive,
                            Latitude = (double)w.Location.Latitude,
                            Longitude = (double)w.Location.Longitude,
                            Description = w.Description,
                            CreatedDate = w.CreatedDate,
                            Distance = w.Location.Distance(sourcePoint),
                            Contact = w.Contact,
                            IsLifted=w.IsLifted
                        }).ToList();

            return Ok(matches);
        }


        // GET: api/GetCongestionById
        [Route("api/GetCongestionById")]
        [HttpGet]
        public IHttpActionResult GetCongestionById(Guid id)
        {
            var matches =
               db.Congestions
                        .FirstOrDefault(w => w.Id == id && w.IsActive);
            var congestionModel = new GenericDataModel();
                        
            congestionModel.Id = matches.Id;
            congestionModel.Name = matches.Name;
            congestionModel.Media = matches.Media;
            congestionModel.IsActive = matches.IsActive;
            congestionModel.Latitude = (double)matches.Location.Latitude;
            congestionModel.Longitude = (double)matches.Location.Longitude;
            congestionModel.Description = matches.Description;
            congestionModel.CreatedDate = matches.CreatedDate;
            congestionModel.Contact = matches.Contact;
                        

            return Ok(congestionModel);
        }



        // PUT: api/Congestions/5
        [ResponseType(typeof(void))]
        [Route("api/PutCongestion")]
        public IHttpActionResult PutCongestion(CongestionModel congestionModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var congestionsExist = db.Congestions.FirstOrDefault(w => w.Id == congestionModel.Id);
                if (congestionsExist != null)
                {
                    congestionsExist.Location = Common.CreatePoint(congestionModel.Latitude, congestionModel.Longitude);
                    congestionsExist.Description = congestionModel.Description;
                    congestionsExist.Name = congestionModel.Name;
                    db.SaveChanges();
                    Common.Push(congestionModel.Longitude, congestionModel.Latitude, 10000, "Traffic jam near Jayamahal due to " + congestionModel.Name, congestionsExist.Id, 1);

                }
                else
                {
                    var congestion = new Congestion();
                    congestion.Id = Guid.NewGuid();
                    congestion.IsActive = true;
                    congestion.CongestionTypeId = Guid.Parse("108853b3-4cf0-4141-91d7-5b791e2981dc");
                    congestion.CreatedDate = DateTime.UtcNow;
                    congestion.Location = Common.CreatePoint(congestionModel.Latitude, congestionModel.Longitude);
                    congestion.Description = congestionModel.Description;
                    congestion.Name = congestionModel.Name;
                    congestion.OrgUserId = Guid.Parse("111faecf-3e24-4e15-9a96-aa924855205b");
                    congestion.Contact = congestionModel.Contact;
                    db.Congestions.Add(congestion);
                    db.SaveChanges();
                    Common.Push(congestionModel.Longitude, congestionModel.Latitude, 10000, "Traffic jam near Jayamahal due to "+ congestionModel.Name, congestion.Id, 1);

                }
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CongestionExists(Guid id)
        {
            return db.Congestions.Count(e => e.Id == id) > 0;
        }
    }
}