﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TwoCentsServer.Models;
using TwoCentsServer.Repositories;

namespace TwoCentsServer.Controllers
{
    public class EventController : ApiController
    {
        public IHttpActionResult Get()
        {
            using (var db = LinqRepository.DataCtx())
            {
                var body = db.Events.Select(r => LinqRepository.ToEventResponse(r, db)).ToList();
                return Json(body);
            }
        }

        public IHttpActionResult Get(int id)
        {
            using (var db = LinqRepository.DataCtx())
            {
                var body = LinqRepository.ToEventResponse(db.Events.FirstOrDefault(r => r.Id == id), db);
                return Json(body);
            }
        }

        public IHttpActionResult Post([FromBody] Event data)
        {
            using (var db = LinqRepository.DataCtx())
            {
                db.Events.InsertOnSubmit(data);
                db.SubmitChanges();
                return Json(data);
            }
        }
    }
}
