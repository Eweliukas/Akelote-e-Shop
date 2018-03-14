using Akelote_e_Shop.Dtos;
using Akelote_e_Shop.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Akelote_e_Shop.Controllers.API
{
    public class ItemsController : ApiController
    {
        private ApplicationDbContext _context;
        public ItemsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET api/items
        public IEnumerable<ItemDto> GetItems()
        {
            return _context.Item.ToList().Select(Mapper.Map<Item, ItemDto>);
        }

        // GET api/Items/1

        public ItemDto GetItem(int id)
        {
            var item = _context.Item.SingleOrDefault(c => c.Id == id);
            if (item == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Item, ItemDto>(item);

        }

        //POST api/items
        [HttpPost]
        public Item CreateItem(Item item)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Item.Add(item);
            _context.SaveChanges();

            return item;

        }

        //PUT api/Items/1
        [HttpPut]
        public void UpdateItem(int id, Item item)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var itemDb = _context.Item.SingleOrDefault(c => c.Id == id);

            if (itemDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            itemDb.Title = item.Title;
            itemDb.Price = item.Price;
            itemDb.CategoryId = item.CategoryId;
            itemDb.Description = item.Description;
            itemDb.Discount = item.Discount;

            _context.SaveChanges();

        }

        //DELETE /api/items/1
        [HttpDelete]
        public void DeleteItem(int id)
        {
            var itemDb = _context.Item.SingleOrDefault(c => c.Id == id);

            if (itemDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Item.Remove(itemDb);
            _context.SaveChanges();

        }


    }
}

