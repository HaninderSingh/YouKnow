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
    public class DiseasesController : ApiController
    {
        private YouKnowEntities db = new YouKnowEntities();

       

        // GET: api/GetDiseases
        [Route("api/GetDiseases")]
        [HttpGet]
        public IHttpActionResult GetDiseases(double lattitude, double longitude)
        {
            var sourcePoint = Common.CreatePoint(lattitude, longitude);
            List<DiseasesModel> matches =
               db.Diseases
                        .Where(w => w.GroundZero.Distance(sourcePoint) < 5000 && w.IsActive)
                        .OrderBy(w => w.GroundZero.Distance(sourcePoint))
                        .Select(w => new DiseasesModel
                        {
                            Id = w.Id,
                            Name = w.Name,
                            Media = w.Media,
                            IsActive = w.IsActive,
                            Latitude = (double)w.GroundZero.Latitude,
                            Longitude = (double)w.GroundZero.Longitude,
                            ConfirmedCases=w.ConfirmedCases,
                            Description = w.Description,
                            CreatedDate = w.CreatedDate,
                            Distance = w.GroundZero.Distance(sourcePoint),
                            Contact = w.Contact,
                            Diagnosis=w.Diagnosis,
                            FirstIdentifiedOn=w.FirstIdentifiedOn,
                            KeyFacts=w.KeyFacts,
                            Symptoms=w.Symptoms,
                            Prevention=w.Prevention,
                            Transmission=w.Transmission,
                            Treatment=w.Treatment,
                            Outbreaks=w.Outbreaks
                        }).ToList();

            return Ok(matches);
        }

        // GET: api/GetDiseasesById
        [Route("api/GetDiseasesById")]
        [HttpGet]
        public IHttpActionResult GetDiseasesById(Guid id)
        {
            var matches = db.Diseases.FirstOrDefault(w => w.Id == id && w.IsActive);

            var diseasesModel = new GenericDataModel();

                          diseasesModel.Id = matches.Id;
                          diseasesModel.Name = matches.Name;
                          diseasesModel.Media = matches.Media;
                          diseasesModel.IsActive = matches.IsActive;
                          diseasesModel.Latitude = (double)matches.GroundZero.Latitude;
                          diseasesModel.Longitude = (double)matches.GroundZero.Longitude;
                          diseasesModel.Description = matches.Description;
                          diseasesModel.CreatedDate = matches.CreatedDate;
                          diseasesModel.Contact = matches.Contact;
                       

            return Ok(diseasesModel);
        }


        // PUT: api/PutDisease/5
        [HttpPut]
        [Route("api/PutDisease")]
        public IHttpActionResult PutDisease(DiseasesModel diseasesModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var diseaseExist = db.Diseases.FirstOrDefault(w => w.Id == diseasesModel.Id);
                if (diseaseExist != null)
                {
                    diseaseExist.GroundZero = Common.CreatePoint(diseasesModel.Latitude, diseasesModel.Longitude);
                    diseaseExist.Description = diseasesModel.Description;
                    diseaseExist.Name = diseasesModel.Name;
                    //Common.Push(diseasesModel.Longitude, diseasesModel.Latitude, 10000, diseaseExist.Name + " is spreading! Take precaution", diseaseExist.Id, 3);
                    db.SaveChanges();
                }
                else
                {
                    var disease = new Disease();
                    disease.Id =Guid.NewGuid();
                    disease.IsActive =true;
                    disease.CreatedDate =DateTime.UtcNow;
                    disease.GroundZero =Common.CreatePoint(diseasesModel.Latitude, diseasesModel.Longitude);
                    disease.Description = diseasesModel.Description;
                    disease.Media = diseasesModel.Media;
                    disease.Name = diseasesModel.Name;
                    disease.OrgUserId =Guid.Parse("5F6C19ED-2B71-4F8B-8E45-77372C4DA40A");
                    disease.Contact = diseasesModel.Contact;
                    db.Diseases.Add(disease);
                    db.SaveChanges();
                    Common.Push(diseasesModel.Longitude, diseasesModel.Latitude, 10000, disease.Name + " is spreading! Take precaution", disease.Id, 3);

                }
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // POST: api/Diseases
        [ResponseType(typeof(Disease))]
        public IHttpActionResult PostDisease(Disease disease)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Diseases.Add(disease);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DiseaseExists(disease.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = disease.Id }, disease);
        }

        // DELETE: api/Diseases/5
        [ResponseType(typeof(Disease))]
        public IHttpActionResult DeleteDisease(Guid id)
        {
            Disease disease = db.Diseases.Find(id);
            if (disease == null)
            {
                return NotFound();
            }

            db.Diseases.Remove(disease);
            db.SaveChanges();

            return Ok(disease);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiseaseExists(Guid id)
        {
            return db.Diseases.Count(e => e.Id == id) > 0;
        }
    }
}