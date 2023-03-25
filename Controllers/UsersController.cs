using BasicApplication.Domain.Dtos;
using BasicApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace BasicApplication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] UserCreationDto model)
        {
            try
            {
                var result = await _userService.CreateAsync(model);
                if (result)
                {
                    return Ok("User created successfully");
                }
                else
                {
                    return BadRequest("User created failed");
                }
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> List([FromQuery] UserSearchDto model)
        {
            var result = await _userService.ListUsersAsync(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            try
            {

                var result = await _userService.Login(model);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public IActionResult RemoveCoveredLines([FromForm] LuestDto file)
        {
            // Read the JSON file from the request body
            using (StreamReader reader = new StreamReader(file.file.OpenReadStream()))
            {
                string json = reader.ReadToEnd();

                // Parse the JSON data into a list of dictionaries
                List<Dictionary<string, object>> data = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);

                // Remove the 'coveredLines' property from each dictionary in the list
                foreach (Dictionary<string, object> obj in data)
                {
                    if (obj.ContainsKey("coveredLines"))
                    {
                        obj.Remove("coveredLines");
                    }
                }

                // Return the modified array
                return Ok(data);
            }
        }
        [HttpPost]
        public async Task<IActionResult> RefreshToken(TokenDto tokenDTO)
        {
            try
            {
                TokenDto tokenResponseDTO = await _userService.RefreshToken(tokenDTO);
                return Ok(tokenResponseDTO);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
