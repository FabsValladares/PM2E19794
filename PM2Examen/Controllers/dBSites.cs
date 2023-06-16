using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using PM2Examen.Models;
using System.Threading.Tasks;

namespace PM2Examen.Controllers
{
    public class dBSites
    {
        readonly SQLiteAsyncConnection dBSite;

        //Constructor de la clase  

        public dBSites(string dbpath)
        {
            //condision a la base de datos 
            dBSite = new SQLiteAsyncConnection(dbpath);


            //crearemos las tablas en la base de datos 
            dBSite.CreateTableAsync<Sitios>();
        }
        //hacemos la creacion del crud


        public Task<int> GuardarSitio(Sitios sitios)
        {
            if (sitios.id != 0)
            {
                return dBSite.UpdateAsync(sitios);//actualizamos
            }
            else
            {
                return dBSite.InsertAsync(sitios);//i si no esta lo inserta
            }
        }

        //con read podemos leer informacion de la tabla 
        //read
        public Task<List<Sitios>> ObtenerlistadeSitios()

        {
            return dBSite.Table<Sitios>().ToListAsync();
        }

        //eliminar
        public Task<int> EliminarSitio(Sitios sitios)
        {
            return dBSite.DeleteAsync(sitios);

        }
        // Obtener Latitud de UbicacionesDB
        public Task<Sitios> ObtenerLongitud(string Longitude)
        {
            return dBSite.Table<Sitios>().Where(i => i.longitud == Longitude).FirstOrDefaultAsync();
        }

        // Obtener Latitud de UbicacionesDB
        public Task<Sitios> ObtenerLatitud(string Latitude)
        {
            return dBSite.Table<Sitios>().Where(i => i.latitud == Latitude).FirstOrDefaultAsync();
        }

        // Obtener Descripcion de UbicacionesDB
        public Task<Sitios> ObtenerDescripcion(String uDescripcion)
        {
            return dBSite.Table<Sitios>().Where(i => i.descripcion == uDescripcion).FirstOrDefaultAsync();
        }

        
    }
}
