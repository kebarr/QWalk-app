using System;
using System.Collections.Generic;
using System.Numerics;
using System.Web.Http;
using System.Web.Mvc;
using QWalk;
using QWalker.Models;
using Raven.Client.Document;
using System.Linq;

namespace QWalker.Controllers
{
    public class InputController : Controller
    {   // according to http://www.c-sharpcorner.com/UploadFile/ff2f08/actionresult-return-type-in-mvc-3-0/ this should work!
        // this should hopefully do most of the stuff that was done in the forms application- this returns three things potentially.


        public ContentResult ContentResultTest()
        {
            return Content("Hello My Friend!");
        }


        public JsonResult SearchByDate(DateTime date)
        {
            var store = new DocumentStore { ConnectionStringName = "RavenServer" };
            store.Initialize();


            using (var session = store.OpenSession())
            {

                return Json(session.Query<WalkResults>().Where(result => result.DateRun > date).ToList());

            }
        }
        public JsonResult Walk(InputModel model)
        {
            var returnedModel = new InputModel();
            if (Inputs.IsValid(model.Left) == false)
            {
                returnedModel.ErrorMessage = "Left coin state value must be of form a, bi, a+bi, sqrt(a), sqrt(b)i or sqrt(a) + sqrt(b)i, default value sqrt(0.5) used for simulation";


                return Json(returnedModel);
                //return null;
            }


            Complex leftInitial = Inputs.Convert(model.Left);


            if (Inputs.IsValid(model.Right) == false)
            {
                returnedModel.ErrorMessage = "Right coin state value must be of form a, bi, a+bi, sqrt(a), sqrt(b)i or sqrt(a) + sqrt(b)i, default value sqrt(0.5) used for simulation";




                return Json(returnedModel);
                //return null;
            }


            // check initial state is normalised. if not, just reassign to something that is for time being. eventually
            // want to make it so only runs if user puts in valid initial condition, but need it to work for all such conditions first.


            Complex rightInitial = Inputs.Convert(model.Right);
            if (qWalker.isNormalised(rightInitial, leftInitial) == false)
            {
                // in this case need to notify user aND still run walk.
                rightInitial = new Complex(0, Math.Sqrt(0.5));
                leftInitial = new Complex(0, Math.Sqrt(0.5));
            }
            var results = qWalker.runWalk(rightInitial, leftInitial);
            SaveResults(results);

            returnedModel.Results = results.Results;
            return Json(returnedModel);
        }


        private void SaveResults(WalkResults results)
        {
            var store = new DocumentStore { ConnectionStringName = "RavenServer" };
            store.Initialize();


            using (var session = store.OpenSession())
            {
                session.Store(results);
                session.Query<WalkResults>().Where(result => result.DateRun > DateTime.Parse("28/6/2013")).ToList();
                session.SaveChanges();
            }
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }


        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }


        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }


        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}

