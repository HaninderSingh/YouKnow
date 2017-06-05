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
    public class BloodsController : ApiController
    {
        private YouKnowEntities db = new YouKnowEntities();

        // GET: api/Bloods
        [Route("api/GetBloods")]
        [HttpGet]
        public IHttpActionResult GetBloods(double lattitude, double longitude)
        {
            var sourcePoint = Common.CreatePoint(lattitude, longitude);

            List<BloodModel> matches =
               db.Bloods
                        .Where(w => w.WantedFrom.Distance(sourcePoint) < 5000 && w.IsActive)
                        .OrderBy(w => w.WantedFrom.Distance(sourcePoint))
                        .Select(w => new BloodModel
                        {
                            Id = w.Id,
                            Name = w.Name,
                            Media = w.Media,
                            IsActive = w.IsActive,
                            Latitude = (double)w.WantedFrom.Latitude,
                            Longitude = (double)w.WantedFrom.Longitude,
                            IsFulfilled = w.IsFulfilled,
                            Description = w.Description,
                            CreatedDate = w.CreatedDate,
                            Distance = w.WantedFrom.Distance(sourcePoint),
                            Contact = w.Contact,
                        }).ToList();

            return Ok(matches);
        }
        // GET: api/GetCounts
        [Route("api/GetCounts")]
        [HttpGet]
        public IHttpActionResult GetCounts(double lattitude, double longitude)
        {
            var sourcePoint = Common.CreatePoint(lattitude, longitude);
            List<CountModel> countModelList = new List<CountModel>();

            countModelList.Add(new CountModel
            {
                Type =0,
                Count = db.Bloods.Where(w => w.WantedFrom.Distance(sourcePoint) < 5000 && w.IsActive).Count()
            });
            countModelList.Add(new CountModel
            {
                Type =1,
                Count = db.Congestions
                      .Where(w => w.Location.Distance(sourcePoint) < 5000 && w.IsActive).Count()
            });
            countModelList.Add(new CountModel
            {
                Type =2,
                Count = db.Disasters.Where(w => w.MissingFrom.Distance(sourcePoint) < 5000 && w.IsActive).Count()
            });
            countModelList.Add(new CountModel
            {
                Type =3,
                Count = db.Diseases.Where(w => w.GroundZero.Distance(sourcePoint) < 5000 && w.IsActive).Count()
            });
            countModelList.Add(new CountModel
            {
                Type =4,
                Count = db.Missings.Where(w => w.MissingFrom.Distance(sourcePoint) < 5000 && w.IsActive).Count()
            });
            countModelList.Add(new CountModel
            {
                Type =5,
                Count = db.Wanteds.Where(w => w.WantedFrom.Distance(sourcePoint) < 5000 && w.IsActive).Count()
            });
           

            return Ok(countModelList);
        }

        // GET: api/GetCarouselData
        [Route("api/GetCarouselData")]
        [HttpGet]
        public IHttpActionResult GetCarouselData(double lattitude, double longitude)
        {
            var sourcePoint = Common.CreatePoint(lattitude, longitude);
            List<GenericModel> matchedData = new List<GenericModel>();

            var list =
               db.Bloods
                        .Where(w => w.WantedFrom.Distance(sourcePoint) < 10000 && w.IsActive)
                        .OrderBy(w => w.WantedFrom.Distance(sourcePoint))
                        .Select(w => new GenericModel
                        {
                            Id = w.Id,
                            Name = w.Name,
                            Media = w.Media,
                            IsActive = w.IsActive,
                            Latitude = (double)w.WantedFrom.Latitude,
                            Longitude = (double)w.WantedFrom.Longitude,
                            Description = w.Description,
                            CreatedDate = w.CreatedDate,
                        }).ToList();
            foreach (var item in list)
            {
                matchedData.Add(item);
            }
            list = db.Congestions
                      .Where(w => w.Location.Distance(sourcePoint) < 10000 && w.IsActive)
                      .OrderBy(w => w.Location.Distance(sourcePoint))
                      .Select(w => new GenericModel
                      {
                          Id = w.Id,
                          Name = w.Name,
                          Media = w.Media,
                          IsActive = w.IsActive,
                          Latitude = (double)w.Location.Latitude,
                          Longitude = (double)w.Location.Longitude,
                          Description = w.Description,
                          CreatedDate = w.CreatedDate
                      }).ToList();
            foreach (var item in list)
            {
                matchedData.Add(item);
            }
            list = db.Disasters
                        .Where(w => w.MissingFrom.Distance(sourcePoint) < 10000 && w.IsActive)
                        .OrderBy(w => w.MissingFrom.Distance(sourcePoint))
                        .Select(w => new GenericModel
                        {
                            Id = w.Id,
                            Name = w.Name,
                            Media = w.Media,
                            IsActive = w.IsActive,
                            Latitude = (double)w.MissingFrom.Latitude,
                            Longitude = (double)w.MissingFrom.Longitude,
                            Description = w.Description,
                            CreatedDate = w.CreatedDate,
                        }).ToList();
            foreach (var item in list)
            {
                matchedData.Add(item);
            }
            list = db.Diseases
                        .Where(w => w.GroundZero.Distance(sourcePoint) < 10000 && w.IsActive)
                        .OrderBy(w => w.GroundZero.Distance(sourcePoint))
                        .Select(w => new GenericModel
                        {
                            Id = w.Id,
                            Name = w.Name,
                            Media = w.Media,
                            IsActive = w.IsActive,
                            Latitude = (double)w.GroundZero.Latitude,
                            Longitude = (double)w.GroundZero.Longitude,
                            Description = w.Description,
                            CreatedDate = w.CreatedDate
                        }).ToList();
            foreach (var item in list)
            {
                matchedData.Add(item);
            }
            list = db.Missings
                        .Where(w => w.MissingFrom.Distance(sourcePoint) < 10000 && w.IsActive)
                        .OrderBy(w => w.MissingFrom.Distance(sourcePoint))
                        .Select(w => new GenericModel
                        {
                            Id = w.Id,
                            Name = w.Name,
                            Media = w.Media,
                            IsActive = w.IsActive,
                            Latitude = (double)w.MissingFrom.Latitude,
                            Longitude = (double)w.MissingFrom.Longitude,
                            CreatedDate = w.CreatedDate
                        }).ToList();
            foreach (var item in list)
            {
                matchedData.Add(item);
            }
            list = db.Wanteds
                        .Where(w => w.WantedFrom.Distance(sourcePoint) < 10000 && w.IsActive)
                        .OrderBy(w => w.WantedFrom.Distance(sourcePoint))
                        .Select(w => new GenericModel
                        {
                            Id = w.Id,
                            Name = w.Name,
                            Media = w.Media,
                            IsActive = w.IsActive,
                            Latitude = (double)w.WantedFrom.Latitude,
                            Longitude = (double)w.WantedFrom.Longitude,
                            Description = w.Description,
                            CreatedDate = w.CreatedDate
                        }).ToList();
            foreach (var item in list)
            {
                matchedData.Add(item);
            }
            return Ok(matchedData);
        }
        // GET: api/GetCarouselData2
        [Route("api/GetCarouselData2")]
        [HttpGet]
        public IHttpActionResult GetCarouselData2(double lattitude, double longitude)
        {
            var sourcePoint = Common.CreatePoint(lattitude, longitude);
            List<GenericModel2> matchedData = new List<GenericModel2>();

            var list =
               db.Bloods
                        .Where(w => w.WantedFrom.Distance(sourcePoint) < 10000 && w.IsActive)
                        .OrderBy(w => w.WantedFrom.Distance(sourcePoint))
                        .Select(w => new GenericModel2
                        {
                            Id = w.Id,
                            Name = w.Name,
                            Media = w.Media,
                            IsActive = w.IsActive,
                            Latitude = (double)w.WantedFrom.Latitude,
                            Longitude = (double)w.WantedFrom.Longitude,
                            Description = w.Description,
                            CreatedDate = w.CreatedDate
                           
                        }).ToList();
            foreach (var item in list)
            {
                matchedData.Add(item);
            }
            list = db.Congestions
                      .Where(w => w.Location.Distance(sourcePoint) < 10000 && w.IsActive)
                      .OrderBy(w => w.Location.Distance(sourcePoint))
                      .Select(w => new GenericModel2
                      {
                          Id = w.Id,
                          Name = w.Name,
                          Media = w.Media,
                          IsActive = w.IsActive,
                          Latitude = (double)w.Location.Latitude,
                          Longitude = (double)w.Location.Longitude,
                          Description = w.Description,
                          CreatedDate = w.CreatedDate,
                          TagModel2 = w.CongestionTags.FirstOrDefault().Tag.TagType.Alias 
                      }).ToList();
            foreach (var item in list)
            {
                matchedData.Add(item);
            }
            list = db.Disasters
                        .Where(w => w.MissingFrom.Distance(sourcePoint) < 10000 && w.IsActive)
                        .OrderBy(w => w.MissingFrom.Distance(sourcePoint))
                        .Select(w => new GenericModel2
                        {
                            Id = w.Id,
                            Name = w.Name,
                            Media = w.Media,
                            IsActive = w.IsActive,
                            Latitude = (double)w.MissingFrom.Latitude,
                            Longitude = (double)w.MissingFrom.Longitude,
                            Description = w.Description,
                            CreatedDate = w.CreatedDate,
                            TagModel2 = w.DisasterTags.FirstOrDefault().Tag.TagType.Alias
                        }).ToList();
            foreach (var item in list)
            {
                matchedData.Add(item);
            }
            list = db.Diseases
                        .Where(w => w.GroundZero.Distance(sourcePoint) < 10000 && w.IsActive)
                        .OrderBy(w => w.GroundZero.Distance(sourcePoint))
                        .Select(w => new GenericModel2
                        {
                            Id = w.Id,
                            Name = w.Name,
                            Media = w.Media,
                            IsActive = w.IsActive,
                            Latitude = (double)w.GroundZero.Latitude,
                            Longitude = (double)w.GroundZero.Longitude,
                            Description = w.Description,
                            CreatedDate = w.CreatedDate,
                            //TagModel2 = new List<TagModel2>() { new TagModel2() { Id = w.DiseaseTags.FirstOrDefault().Id, TagTypeMessage = w.DiseaseTags.FirstOrDefault().Tag.TagType.Name } }
                        }).ToList();
            foreach (var item in list)
            {
                matchedData.Add(item);
            }
            list = db.Missings
                        .Where(w => w.MissingFrom.Distance(sourcePoint) < 10000 && w.IsActive)
                        .OrderBy(w => w.MissingFrom.Distance(sourcePoint))
                        .Select(w => new GenericModel2
                        {
                            Id = w.Id,
                            Name = w.Name,
                            Media = w.Media,
                            IsActive = w.IsActive,
                            Latitude = (double)w.MissingFrom.Latitude,
                            Longitude = (double)w.MissingFrom.Longitude,
                            CreatedDate = w.CreatedDate,
                            //TagModel2 = new List<TagModel2>() { new TagModel2() { Id = w.MissingTags.FirstOrDefault().Id, TagTypeMessage = w.MissingTags.FirstOrDefault().Tag.TagType.Alias } }
                        }).ToList();
            foreach (var item in list)
            {
                matchedData.Add(item);
            }
            list = db.Wanteds
                        .Where(w => w.WantedFrom.Distance(sourcePoint) < 10000 && w.IsActive)
                        .OrderBy(w => w.WantedFrom.Distance(sourcePoint))
                        .Select(w => new GenericModel2
                        {
                            Id = w.Id,
                            Name = w.Name,
                            Media = w.Media,
                            IsActive = w.IsActive,
                            Latitude = (double)w.WantedFrom.Latitude,
                            Longitude = (double)w.WantedFrom.Longitude,
                            Description = w.Description,
                            CreatedDate = w.CreatedDate,
                            //TagModel2 = new List<TagModel2>() { new TagModel2() { Id = w.WantedTags.FirstOrDefault().Id, TagTypeMessage = w.WantedTags.FirstOrDefault().Tag.TagType.Alias } }
                        }).ToList();
            foreach (var item in list)
            {
                matchedData.Add(item);
            }
            return Ok(matchedData);
        }


        // PUT: api/Bloods/5
        [Route("api/PutBlood")]
        [HttpPut]
        public IHttpActionResult PutBlood(BloodModel bloodModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            

            try
            {
                var bloodDataExist = db.Bloods.FirstOrDefault(w => w.Id == bloodModel.Id);
                if (bloodDataExist != null)
                {
                    bloodDataExist.WantedFrom = Common.CreatePoint(bloodModel.Latitude, bloodModel.Longitude);
                    bloodDataExist.Description = bloodModel.Description;
                    bloodDataExist.Name = bloodModel.Name;
                    db.SaveChanges();
                }
                else
                {
                    var blood = new Blood();
                    blood.Id = Guid.NewGuid();
                    blood.IsActive = true;
                    blood.CreatedDate = DateTime.UtcNow;
                    blood.WantedFrom = Common.CreatePoint(bloodModel.Latitude, bloodModel.Longitude);
                    blood.Description = bloodModel.Description;
                    blood.Name = bloodModel.Name;
                    blood.OrgUserId = Guid.Parse("b55f9060-2f96-4ff0-91c7-ecdbdc386056");
                    blood.Contact = bloodModel.Contact;
                    blood.IsFulfilled = true;
                    blood.BloodGroup = bloodModel.Name;
                    db.Bloods.Add(blood);
                    db.SaveChanges();
                    Common.Push(bloodModel.Longitude, bloodModel.Latitude, 10000, bloodModel.Name + " blood required urgently "  , bloodModel.Id, 0);

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

        private bool BloodExists(Guid id)
        {
            return db.Bloods.Count(e => e.Id == id) > 0;
        }
    }
}