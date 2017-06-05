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
    public class MissingsController : ApiController
    {
        private YouKnowEntities db = new YouKnowEntities();


        // GET: api/GetMissings
        [Route("api/GetMissings")]
        [HttpGet]
        public IHttpActionResult GetMissings(double lattitude, double longitude)
        {
            var sourcePoint = Common.CreatePoint(lattitude, longitude);
            
            List<MissingModel> matches =
               db.Missings
                        .Where(w => w.MissingFrom.Distance(sourcePoint) < 5000 && w.IsActive)
                        .OrderBy(w => w.MissingFrom.Distance(sourcePoint))
                        .Select(w => new MissingModel
                        {
                            Id = w.Id,
                            Name = w.Name,
                            Media = w.Media,
                            IsActive = w.IsActive,
                            Latitude = (double)w.MissingFrom.Latitude,
                            Longitude = (double)w.MissingFrom.Longitude,
                            HasReturned = w.HasReturned,
                            MissingType = w.Description,
                            Description = w.Description,
                            Distance = w.MissingFrom.Distance(sourcePoint),
                            Contact = w.Contact,
                            CreatedDate = w.CreatedDate
                        }).ToList();

            return Ok(matches);
        }
       
        // PUT: api/Missings/5
        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("api/PutMissing")]
        public IHttpActionResult PutMissing(MissingModel missingModel,Guid missingTypeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var missingExist = db.Missings.FirstOrDefault(w => w.Id == missingModel.Id);
                if (missingExist != null)
                {
                    missingExist.MissingFrom = Common.CreatePoint(missingModel.Latitude, missingModel.Longitude);
                    missingExist.Description = missingModel.Description;
                    missingExist.Name = missingModel.Name;
                    db.SaveChanges();
                }
                else
                {
                    var missing = new Missing();
                    missing.Id = Guid.NewGuid();
                    missing.IsActive = true;
                    missing.MissingTypeId = missingTypeId;
                    missing.CreatedDate = DateTime.UtcNow;
                    missing.MissingFrom = Common.CreatePoint(missingModel.Latitude, missingModel.Longitude);
                    missing.Description = missingModel.Description;
                    missing.Name = missingModel.Name;
                    missing.OrgUserId = Guid.Parse("5F6C19ED-2B71-4F8B-8E45-77372C4DA40A");
                    missing.Contact = missingModel.Contact;
                    db.Missings.Add(missing);
                    db.SaveChanges();
                    Common.Push(missingModel.Longitude, missingModel.Latitude, 10000, missingModel.Name + " is missing! Have you seen this person?", missing.Id, 3);

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

        private bool MissingExists(Guid id)
        {
            return db.Missings.Count(e => e.Id == id) > 0;
        }
    }
}