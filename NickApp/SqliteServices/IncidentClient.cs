using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NickApp.Models;

namespace NickApp.SqliteServices
{
    public partial class SQLiteHelper
    {
        //Insert and Update new record
        public Task<int> SaveIncidentAsync(Incident incident)
        {
            return db.UpdateAsync(incident);
        }

        public Task<int> AddIncidentAsync(Incident incident)
        {
                return db.InsertAsync(incident);
        }
        //Delete
        public Task<int> DeleteIncidentAsync(Incident incident)
        {
            return db.DeleteAsync(incident);
        }

        public Task<List<Incident>> GetAllIncidentAsync()
        {
            return db.Table<Incident>().ToListAsync();
        }
        //Read Item
        public Task<List<Incident>> GetIncidentAsync(string incidentCode)
        {
            return db.Table<Incident>().Where(s => s.IncidentCode == incidentCode).ToListAsync();
        }
    }
}
