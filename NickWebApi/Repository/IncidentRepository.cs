using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NickWebApi.Models;
using NickWebApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace NickWebApi.Repository
{
    public class IncidentRepository: IIncidentRepository
    {

        NickDBContext db;
        public IncidentRepository(NickDBContext _db)
        {
            db = _db;
        }


        public async Task<List<Incident>> GetIncident()
        {
            if (db != null)
            {
                return await db.Incidents.ToListAsync();
            }
            return null;
        }
       


        public async Task<List<Incident>> GetIncident(string IncidentCode)
        {
            if (db != null)
            {

              
                    return await db.Incidents.Where(x => x.IncidentCode == IncidentCode).ToListAsync();


            }

            return null;
        }

        public async Task<string> AddIncident(Incident Incident)
        {
            if (db != null)
            {
                await db.Incidents.AddAsync(Incident);
                await db.SaveChangesAsync();

                return Incident.IncidentCode;
            }

            return string.Empty;
        }

        public async Task<int> DeleteIncident(string IncidentCode)
        {
            int result = 0;

            if (db != null)
            {
                //Find the post for specific post id
                var Incident = await db.Incidents.FirstOrDefaultAsync(x => x.IncidentCode == IncidentCode);

                if (Incident != null)
                {
                    //Delete that post
                    db.Incidents.Remove(Incident);

                    //Commit the transaction
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }


        public async Task UpdateIncident(Incident Incident)
        {
            if (db != null)
            {
                //Update that Incident
                db.Update(Incident);

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }
    }


}
