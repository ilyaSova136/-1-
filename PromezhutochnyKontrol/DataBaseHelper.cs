using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromezhutochnyKontrol
{
    public class DataBaseHelper
    {
        string connstring = "Host=localhost;Database=lopyxxx;Username=postgres;Password=131126";
        
        //вытаскиваем продукты
        public List<Product> GetProducts()
        {
            List <Product> products = new List <Product>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connstring))
            {
                connection.Open();

                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM public.\"Product\"", connection);

                NpgsqlDataReader dataReader = cmd.ExecuteReader();


                while (dataReader.Read()) // построчно считываем данные
                {
                    products.Add(new Product
                    {
                        Id = (int)dataReader.GetValue(0),
                        Title = dataReader.GetValue(1).ToString(),
                        ProductTypeId = (int)dataReader.GetValue(2),
                        Article = (string)dataReader.GetValue(3),
                        Image = !dataReader.IsDBNull(5) ? dataReader.GetString(5) : null
                });
                }

            }

            //выиаскиваем тип продукта
            using (NpgsqlConnection connection = new NpgsqlConnection(connstring))
            {
                connection.Open();
                foreach (var item in products)
                {
                    NpgsqlCommand cmd1 = new NpgsqlCommand($"SELECT * FROM public.\"ProductType\" where \"ID\" = {item.ProductTypeId};", connection);
                    
                    NpgsqlDataReader reader = cmd1.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        item.ProductType = new ProductType { Id = (int)reader.GetValue(0), Title = (string)reader.GetValue(1), DefectedOercent = (double)reader.GetValue(2) };
                    }
                    reader.Close();
                }
            }

            //вытаскиваем материалы
            foreach (var item in products)
            {
                item.Materials = GetMaterials(item.Id);
            }


            return products;
        }

        public List<Product> FindProduct(string findstring)
        {
            List<Product> products = new List<Product>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connstring))
            {
                connection.Open();

                NpgsqlCommand cmd = new NpgsqlCommand($"select * from public.\"Product\" where \"Title\" ilike '%{findstring}%'", connection); ;

                NpgsqlDataReader dataReader = cmd.ExecuteReader();


                while (dataReader.Read()) // построчно считываем данные
                {
                    products.Add(new Product
                    {
                        Id = (int)dataReader.GetValue(0),
                        Title = dataReader.GetValue(1).ToString(),
                        ProductTypeId = (int)dataReader.GetValue(2),
                        Article = (string)dataReader.GetValue(3),
                        Image = !dataReader.IsDBNull(5) ? dataReader.GetString(5) : null
                    });
                }

            }

            //выиаскиваем тип продукта
            using (NpgsqlConnection connection = new NpgsqlConnection(connstring))
            {
                connection.Open();
                foreach (var item in products)
                {
                    NpgsqlCommand cmd1 = new NpgsqlCommand($"SELECT * FROM public.\"ProductType\" where \"ID\" = {item.ProductTypeId};", connection);

                    NpgsqlDataReader reader = cmd1.ExecuteReader();

                    while (reader.Read())
                    {
                        item.ProductType = new ProductType { Id = (int)reader.GetValue(0), Title = (string)reader.GetValue(1), DefectedOercent = (double)reader.GetValue(2) };
                    }
                    reader.Close();
                }
            }

            //вытаскиваем материалы
            foreach (var item in products)
            {
                item.Materials = GetMaterials(item.Id);
            }

            return products;
        }

        public void AddOrUpdateProduct(Product product)
        {
            string sqlExpression = $"INSERT INTO \"Product\" (\"Title\", \"ProductTypeID\", \"ArticleNumber\", \"Description\", \"Image\", \"ProductionPersonCount\", \"ProductionWorkshopNumber\", \"MinCostForAgent\") " +
                    $"VALUES (@Title, @ProductTypeID, @ArticleNumber, @Description, @Image, @ProductionPersonCount, @ProductionWorkshopNumber, @MinCostForAgent);";

            using (NpgsqlConnection connection = new NpgsqlConnection(connstring))
            {
                connection.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlExpression, connection))
                {
                    cmd.Parameters.AddWithValue("@Title", product.Title);
                    cmd.Parameters.AddWithValue("@ArticleNumber", product.Article);
                    cmd.Parameters.AddWithValue("@ProductTypeID", product.ProductTypeId);
                    cmd.Parameters.AddWithValue("@Description", product.Description);
                    cmd.Parameters.AddWithValue("@Image", product.Image ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ProductionPersonCount", product.PersonCount);
                    cmd.Parameters.AddWithValue("@ProductionWorkshopNumber", product.WorkshopNumber);
                    cmd.Parameters.AddWithValue("@MinCostForAgent", product.MinCost);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public List<Material> GetMaterials(int id)
        {
            List<Material> materials = new List<Material>();
            List<int> ints;
            using (NpgsqlConnection connection = new NpgsqlConnection(connstring))
            {
                connection.Open();

                NpgsqlCommand cmd = new NpgsqlCommand($"SELECT * FROM public.\"ProductMaterial\" where \"ProductID\" = {id};", connection);

                NpgsqlDataReader dataReader = cmd.ExecuteReader();

                ints = new List<int>();

                while (dataReader.Read())
                {
                    ints.Add((int)dataReader.GetValue(2));
                }
                
            }
            
            using (NpgsqlConnection connection = new NpgsqlConnection(connstring))
            {
                connection.Open();
                foreach (var item in ints)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand($"SELECT * FROM public.\"Material\" where \"ID\" = {item};", connection);

                    NpgsqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        materials.Add(new Material { Id = dataReader.GetInt32(0), Title = dataReader.GetString(1)});
                    }
                    dataReader.Close();
                }

            }


            return materials;
        }

       
    }
}
