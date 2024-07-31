using Amazon.Library.DTO;
using Amazon.Library.Models;
using Final_Summer2024.API.Database;

namespace Final_Summer2024.API.EC
{
    public class InventoryEC
    {
        public InventoryEC()
        {

        }

        public async Task<IEnumerable<ProductDTO>> Get()
        {
            return FakeDatabase.Products.Take(100).Select(p => new ProductDTO(p));
        }

        public async Task<ProductDTO> Delete (int id)
        {
            var prodToDelete = FakeDatabase.Products.FirstOrDefault(p => p.Id == id);
            if (prodToDelete == null)
            {
                return null;
            }
            FakeDatabase.Products.Remove(prodToDelete);
            return new ProductDTO(prodToDelete);
        }

        public async Task<ProductDTO> AddOrUpdate(ProductDTO p)
        {
            bool isAdd = false;
            if (p.Id == 0)
            {
                isAdd = true;
                p.Id = FakeDatabase.NextProductId;
            }
            if (isAdd)
            {
                FakeDatabase.Products.Add(new Product(p));
            }
            else
            {
                var prodToUpdate = FakeDatabase.Products.FirstOrDefault(a => a.Id == a.Id);
                if (prodToUpdate != null)
                {
                    var index =FakeDatabase.Products.IndexOf(prodToUpdate);
                    FakeDatabase.Products.RemoveAt(index);
                    prodToUpdate = new Product(p);
                    FakeDatabase.Products.Insert(index, prodToUpdate);
                }
                
               
            }

            return p;

        }


        public async Task<IEnumerable<ProductDTO>> Search(string?query)
        {
            return FakeDatabase.Products.Where(p => (p?.Name != null &&
            p.Name.ToUpper().Contains(query?.ToUpper() ?? string.Empty))
            || (p?.Description != null &&
            p.Description.ToUpper().Contains(query?.ToUpper() ?? string.Empty)))
            .Take(100).Select(p => new ProductDTO(p));
        }

        
    }
}
