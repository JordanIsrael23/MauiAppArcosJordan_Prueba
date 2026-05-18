using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Data.Sqlite;

namespace MauiAppArcosJordan_Prueba
{
    public class Productos
    {
        private string connectionString;
        private SqliteConnection connection; 

        public Productos()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "inventario.db");  
            connectionString = $"Data Source={dbPath};";
            connection = new SqliteConnection(connectionString);
            connection.Open();

            connection.Execute(@"
            
              CREATE TABLE IF NOT EXISTS productos  (
                id INTEGER PRIMARY KEY,
                nombre VARCHAR(20),
                cantidad INTEGER,
                descripcion VARCHAR(100),
                categoria VARCHAR(20)
                );
             ");
        }

        // Crud
        // Create
        public Producto Create(int id, string nombre, int cantidad, string descripcion, string categoria)
        {
            var producto = new Producto
            {
                id = id,
                nombre = nombre,
                cantidad = cantidad,
                descripcion = descripcion,
                categoria = categoria
            };
            var producotnuevo = connection.Execute("INSERT INTO productos (id, nombre, cantidad, descripcion, categoria) " +
                "VALUES (@id, @nombre, @cantidad, @descripcion, @categoria)", producto);

            if(producotnuevo == 0)
            {
                throw new Exception("No se pudo insertar el producto");
            }
            else
            {
                return producto;
            }
        }

        // Read ALL
        public List<Producto> ReadAll()
        {
            var productos = connection.Query<Producto>("SELECT * FROM productos").AsList();
            return productos;
        }

        // Read by ID
        public Producto ReadById(int id)
        {
            var producto = connection.Query<Producto>("SELECT * FROM productos WHERE id = @id", new { id }).ToList();
            if(producto.Count == 0)
            {
                return null;
            }
            else
            {
                return producto[0];
            }
        }

        // Update
        public void Update(int id, string nombre, int cantidad, string descripcion, string categoria)
        {
            var productoActualizado = connection.Execute("UPDATE productos SET nombre = @nombre, cantidad = @cantidad, " +
                "descripcion = @descripcion, categoria = @categoria WHERE id = @id", new {id=id,nombre = nombre, cantidad= cantidad,
                descripcion=descripcion, categoria=categoria});

            if (productoActualizado == 0)
            {
                throw new Exception("No se pudo actualizar el producto");
            }
        }

        // Delete
        public void Delete(int id)
        {
            var productoEliminado = connection.Execute("DELETE FROM productos WHERE id = @idPro", new { idPro=id });
            if (productoEliminado == 0)
            {
                throw new Exception("No se pudo eliminar el producto");
            }
        }
    }
}
