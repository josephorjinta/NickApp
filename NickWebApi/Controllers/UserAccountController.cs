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
    public class UserAccountController : ControllerBase
    {
        IUserAccountRepository UserAccountRepository;
        public UserAccountController(IUserAccountRepository _UserAccountRepository)
        {
            UserAccountRepository = _UserAccountRepository;
        }

        [HttpGet]
        [Route("GetUserAccountByUserName")]
        public async Task<IActionResult> GetUserAccountByUserName(string UserName)
        {
            if (UserName == null)
            {
                return BadRequest();
            }

            try
            {
                var UserAccount = await UserAccountRepository.GetUserAccountByUserName(UserName);

                if (UserAccount == null)
                {
                    return NotFound();
                }

                return Ok(UserAccount);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
       

        [HttpPost]
        [Route("AddUserAccount")]
        public async Task<IActionResult> AddUserAccount([FromBody]UserAccount model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var id = await UserAccountRepository.AddUserAccount(model);
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
        [Route("DeleteUserAccount")]
        public async Task<IActionResult> DeleteUserAccount(string UserAccountCode)
        {
            int result = 0;

            if (UserAccountCode == "")
            {
                return BadRequest();
            }

            try
            {
                result = await UserAccountRepository.DeleteUserAccount(UserAccountCode);
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
        [Route("UpdateUserAccount")]
        public async Task<IActionResult> UpdateUserAccount([FromBody]UserAccount model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await UserAccountRepository.UpdateUserAccount(model);

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

