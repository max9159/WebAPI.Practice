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
using TryWebAPI0813.Models;

namespace TryWebAPI0813.Controllers
{
    //already added to global.aspx for all api
    //[MyException]
    [RoutePrefix("clients")]
    public class ClientsController : ApiController
    {
        private FabricsEntities db = new FabricsEntities();

        public ClientsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        [Route("")]
        public IHttpActionResult GetClient()
        {
            return Ok(db.Client);
        }

        [Route("{id:int}/orders")]
        public HttpResponseMessage GetClientOrders(int id)
        {
            var orders = db.Order.Where(q => q.ClientId == id);
            //return Request.CreateResponse<IQueryable<Order>>(
            //HttpStatusCode.OK, orders,
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter);
            return new HttpResponseMessage()
            {
                ReasonPhrase = "HAHA",
                StatusCode = HttpStatusCode.OK,
                Content = new ObjectContent<IQueryable<Order>>(orders,
                GlobalConfiguration.Configuration.Formatters.JsonFormatter)
            };
        }

        [Route("{id:int}/orders/{date:datetime}")]
        public IHttpActionResult GetClientOrdersByDate(int id, DateTime date)
        {
            var addDay1 = date.AddDays(1);
            var orders = db.Order.Where(p => p.ClientId == id && p.OrderDate >= date && p.OrderDate <= addDay1);
            return Ok(orders.ToList());
        }

        //Route attr wildcard parameter
        [Route("{id:int}/orders/{*date:datetime}")]
        [ResponseType(typeof(Client))]
        public IHttpActionResult GetClientOrdersByDate2(int id, DateTime date)
        {
            var next_day = date.AddDays(1);
            var orders = db.Order.Where(p => p.ClientId == id && p.OrderDate >= date && p.OrderDate <= next_day);
            return Ok(orders.ToList());
        }

        [ResponseType(typeof(Client))]
        [Route("{id}")]
        public HttpResponseMessage GetClientById(int id)
        //[HttpPost] //usally suppose not to use [FromBody], cause it will be messy 
        //public HttpResponseMessage GetClientById([FromBody]int id)
        {
            Client client = db.Client.Find(id);
            if (client == null)
            {
                throw new Exception();
            }
            return Request.CreateResponse(client);
        }

        [ResponseType(typeof(void))]
        [Route("{id}")]
        public IHttpActionResult PutClient(int id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.ClientId)
            {
                return BadRequest();
            }

            db.Entry(client).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Clients
        [ResponseType(typeof(Client))]
        [Route("")]
        public IHttpActionResult PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Client.Add(client);
            db.SaveChanges();

            return CreatedAtRoute(nameof(GetClientById), new { id = client.ClientId }, client);
        }

        // DELETE: api/Clients/5
        [ResponseType(typeof(Client))]
        [Route("{id}")]
        public IHttpActionResult DeleteClient(int id)
        {
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            db.Client.Remove(client);
            db.SaveChanges();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return db.Client.Count(e => e.ClientId == id) > 0;
        }
    }
}