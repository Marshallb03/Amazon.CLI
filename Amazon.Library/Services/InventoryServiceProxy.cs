using Amazon.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Amazon.Library.Utilities;
using Amazon.Library.DTO;
using System.Reflection.Metadata.Ecma335;


namespace Amazon.Library.Services
{
    public class InventoryServiceProxy
    {
        private static InventoryServiceProxy? instance;
        public static object InstanceLock = new object();

        private List<ProductDTO> products;

        public ReadOnlyCollection<ProductDTO> Products
        {
            get
            {
                return products.AsReadOnly();
            }
        }

        public async Task<ProductDTO> AddOrUpdateProduct(ProductDTO product)
        {

            JsonSerializerSettings settings = new JsonSerializerSettings();
           var result = await new WebRequestHandler().Post("/Inventory", product);
           return JsonConvert.DeserializeObject<ProductDTO>(result, settings);
           
        }

        public async Task<IEnumerable<ProductDTO>> Get()
        {
            var response = await new WebRequestHandler().Get("/Inventory");
            var deserializedResult =  JsonConvert.DeserializeObject<List<ProductDTO>>(response);
            products = deserializedResult?.ToList() ?? new List<ProductDTO>();
            return products;
        }

        private InventoryServiceProxy()
        {

            var response = new WebRequestHandler().Get("/Inventory").Result;
            products = JsonConvert.DeserializeObject<List<ProductDTO>>(response);            
            
        }

        public static InventoryServiceProxy Current
        {
            get
            {
                lock (InstanceLock)
                {
                    if (instance == null)
                    {
                        instance = new InventoryServiceProxy();
                    }
                }
                return instance;
            }
        }

        public async Task<IEnumerable<ProductDTO>> Search(Query query)
        {
            if (query == null || string.IsNullOrEmpty(query.QueryString))
            {
                return await Get();
            }

            var result = await new WebRequestHandler().Post("/Inventory/Search", query);
            products = JsonConvert.DeserializeObject<List<ProductDTO>>(result) ?? new List<ProductDTO>();
            return Products;
        }

        public async Task<ProductDTO?> Delete(int id)
        {
            //var itemToDelete = products.FirstOrDefault(p => p.Id == id);

            //if (itemToDelete == null)
            //{
            //    return null;
            //}

            //products.Remove(itemToDelete);
            var response = await new WebRequestHandler().Delete($"/{id}");
            var itemToDelete = JsonConvert.DeserializeObject<ProductDTO>(response);
            return itemToDelete;


        }
    }
}
