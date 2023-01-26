using System.Collections.Generic;
using System.Threading.Tasks;
using NickApp.Models;

namespace NickApp.Services
{
    public interface IIncidentService
    {
        Task<IEnumerable<Incident>> GetIncident();
        Task<IEnumerable<Incident>> GetIncident(string Incidentcode); 
      
        Task AddIncident(Incident Incident);
        Task SaveIncident(Incident Incident);
        Task DeleteIncident(Incident Incident);      
    }
}
