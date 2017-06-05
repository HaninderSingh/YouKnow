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
    public class WantedsController : ApiController
    {
        private YouKnowEntities db = new YouKnowEntities();

        // GET: api/Wanteds
        public IQueryable<Wanted> GetWanteds()
        {
            return db.Wanteds;
        }
        // GET: api/GetWanteds
        [Route("api/GetWanteds")]
        [HttpGet]
        public IHttpActionResult GetWanteds(double lattitude, double longitude)
        {
            var sourcePoint = Common.CreatePoint(lattitude, longitude);
            
            List<WantedModel> matches =
               db.Wanteds
                        .Where(w => w.WantedFrom.Distance(sourcePoint) < 5000)
                        .OrderBy(w => w.WantedFrom.Distance(sourcePoint))
                        .Select(w => new WantedModel
                        {
                            Id = w.Id,
                            Name = w.Name,
                            Media = w.Media,
                            IsActive = w.IsActive,
                            Latitude = (double)w.WantedFrom.Latitude,
                            Longitude = (double)w.WantedFrom.Longitude,
                            Description = w.Description,
                            Contact = w.Contact,
                            CreatedDate = w.CreatedDate,
                            IsArrested=w.IsArrested
                        }).ToList();

            return Ok(matches);
        }


        // PUT: api/Wanteds/5
        [HttpPut]
        [Route("api/PutWanted")]
        public IHttpActionResult PutWanted(WantedModel wantedModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var wantedExist = db.Wanteds.FirstOrDefault(w => w.Id == wantedModel.Id);
                if (wantedExist != null)
                {
                    wantedExist.WantedFrom = Common.CreatePoint(wantedModel.Latitude, wantedModel.Longitude);
                    wantedExist.Description = wantedModel.Description;
                    wantedExist.Name = wantedModel.Name;
                    db.SaveChanges();
                }
                else
                {
                    var wanted = new Wanted();
                    wanted.Id = Guid.NewGuid();
                    wanted.IsActive = true;
                    wanted.CreatedDate = DateTime.UtcNow;
                    wanted.WantedFrom = Common.CreatePoint(wantedModel.Latitude, wantedModel.Longitude);
                    wanted.Description = wantedModel.Description;
                    wanted.Name = wantedModel.Name;
                    wanted.OrgUserId = Guid.Parse("5F6C19ED-2B71-4F8B-8E45-77372C4DA40A");
                    wanted.Contact = wantedModel.Contact;
                    db.Wanteds.Add(wanted);
                    db.SaveChanges();
                    Common.Push(wantedModel.Longitude, wantedModel.Latitude, 10000, wantedModel.Name + "is wanted.", wantedModel.Id, 3);

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

        private bool WantedExists(Guid id)
        {
            return db.Wanteds.Count(e => e.Id == id) > 0;
        }
    }
}