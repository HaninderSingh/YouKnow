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
    public class DisastersController : ApiController
    {
        private YouKnowEntities db = new YouKnowEntities();

        // GET: api/Disasters
        [Route("api/GetDisasters")]
        [HttpGet]
        public IHttpActionResult GetDisasters(double lattitude ,double longitude)
        {
            var sourcePoint = Common.CreatePoint(lattitude, longitude);
            
            List<DisasterModel> matches =
               db.Disasters
                        .Where(w => w.MissingFrom.Distance(sourcePoint) < 5000 && w.IsActive)
                        .OrderBy(w => w.MissingFrom.Distance(sourcePoint))
                        .Select(w => new DisasterModel {
                            Id=w.Id,
                            Name=w.Name,
                            Media=w.Media,
                            IsActive=w.IsActive,
                            Latitude = (double)w.MissingFrom.Latitude,
                            Longitude = (double)w.MissingFrom.Longitude,
                            DisasterTypeName =db.DisasterTypes.FirstOrDefault(e=>e.Id==w.DisasterTypeId).Name,
                            Description=w.Description,
                            CreatedDate=w.CreatedDate,
                            Distance=w.MissingFrom.Distance(sourcePoint),
                            Contact=w.Contact,
                        }).ToList();
            
            return Ok(matches);
        }

        
        // PUT: api/Disasters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDisaster(DisasterModel disasterModel,Guid disasterTypeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var disasterExist = db.Disasters.FirstOrDefault(w => w.Id == disasterModel.Id);
                if (disasterExist != null)
                {
                    disasterExist.MissingFrom = Common.CreatePoint(disasterModel.Latitude, disasterModel.Longitude);
                    disasterExist.Description = disasterModel.Description;
                    disasterExist.Name = disasterModel.Name;
                    db.SaveChanges();
                }
                else
                {
                    var disaster = new Disaster();
                    disaster.Id = Guid.NewGuid();
                    disaster.IsActive = true;
                    disaster.DisasterTypeId = disasterTypeId;
                    disaster.CreatedDate = DateTime.UtcNow;
                    disaster.MissingFrom = Common.CreatePoint(disasterModel.Latitude, disasterModel.Longitude);
                    disaster.Description = disasterModel.Description;
                    disaster.Name = disasterModel.Name;
                    disaster.OrgUserId = Guid.Parse("5F6C19ED-2B71-4F8B-8E45-77372C4DA40A");
                    disaster.Contact = disasterModel.Contact;
                    db.Disasters.Add(disaster);
                    db.SaveChanges();
                    Common.Push(disasterModel.Longitude, disasterModel.Latitude, 50000, disasterModel.Name + " Alert", disaster.Id, 2);

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

        private bool DisasterExists(Guid id)
        {
            return db.Disasters.Count(e => e.Id == id) > 0;
        }
    }
}