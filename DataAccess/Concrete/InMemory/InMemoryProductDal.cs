using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;  // global field
        public InMemoryProductDal()
        {
            // for example: vt'den gelen veriler 
            _products = new List<Product>
            {
                new Product { ProductId=1, CategoryId=1, ProductName="Glass", UnitPrice=15, UnitsInStock=15 },
                new Product { ProductId=2, CategoryId=1, ProductName="Camera", UnitPrice=500, UnitsInStock=3 },
                new Product { ProductId=3, CategoryId=2, ProductName="Mobile Phone", UnitPrice=1500, UnitsInStock=2 },
                new Product { ProductId=4, CategoryId=2, ProductName="Keyboard", UnitPrice=150, UnitsInStock=65 },
                new Product { ProductId=5, CategoryId=2, ProductName="Mouse", UnitPrice=85, UnitsInStock=1 }
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            // _products.Remove(product); // bu kod bloğu nesnenin silinmesini sağlamaz --> referansların adresleri aynı değil
            #region standard removing codes
            //Product productToDelete = null;
            //foreach (Product p in _products)
            //{
            //    if (product.ProductId == p.ProductId) // ürünleri dolaşma
            //    {
            //        productToDelete = p;
            //    }
            //}
            //_products.Remove(productToDelete);
            #endregion

            // LINQ (Language Intergrated Query) Version
            Product productToDelete = _products.SingleOrDefault(p=>p.ProductId==product.ProductId); // ürünleri dolaşma
            _products.Remove(productToDelete);
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId==categoryId).ToList();
            // where: içindeki şartı sağlayan tüm elemanları yeni bir liste halinde döndürür
        }

        public void Update(Product product)
        {
            // gönderilen ürün id'sine sahip ürünü bulur
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId); // ürünleri dolaşma
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId=product.CategoryId;
            productToUpdate.UnitPrice=product.UnitPrice;
            productToUpdate.UnitsInStock=product.UnitsInStock;
        }
    }
}
