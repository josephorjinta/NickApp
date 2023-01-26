using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NickWebApi.Models;
using NickWebApi.Repository;

namespace NickWebApi.Repository
{
    public interface IIncidentRepository
    {
        Task<List<Incident>> GetIncident();

        Task<List<Incident>> GetIncident(string IncidentCode);

       
        Task<string> AddIncident(Incident Incident);

        Task<int> DeleteIncident(string IncidentCode);

        Task UpdateIncident(Incident Incident);
    }
}
