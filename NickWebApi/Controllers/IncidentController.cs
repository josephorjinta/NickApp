using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NickWebApi.Models;
using NickWebApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace NickWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        IIncidentRepository IncidentRepository;
        public IncidentController(IIncidentRepository _IncidentRepository)
        {
            IncidentRepository = _IncidentRepository;
        }

        [HttpGet]
        [Route("GetAllIncident")]
        public async Task<IActionResult> GetAllIncident()
        {
            try
            {
                var Incident = await IncidentRepository.GetIncident();
                if (Incident == null)
                {
                    return NotFound();
                }

                return Ok(Incident);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

     
        [HttpGet]
        [Route("GetIncident")]
        public async Task<IActionResult> GetIncident(string IncidentCode)
        {
            if (IncidentCode == null  )
            {
                return BadRequest();
            }

            try
            {
                var Incident = await IncidentRepository.GetIncident(IncidentCode);

                if (Incident == null)
                {
                    return NotFound();
                }

                return Ok(Incident);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddIncident")]
        public async Task<IActionResult> AddIncident([FromBody]Incident model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var id = await IncidentRepository.AddIncident(model);
                    if (id.ToString() != "")
                    {
                        return Ok(id);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("DeleteIncident")]
        public async Task<IActionResult> DeleteIncident(string IncidentCode)
        {
            int result = 0;

            if (IncidentCode == "")
            {
                return BadRequest();
            }

            try
            {
                result = await IncidentRepository.DeleteIncident(IncidentCode);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpPut]
        [Route("UpdateIncident")]
        public async Task<IActionResult> UpdateIncident([FromBody]Incident model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await IncidentRepository.UpdateIncident(model);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName ==
                                      "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }

    }

}

