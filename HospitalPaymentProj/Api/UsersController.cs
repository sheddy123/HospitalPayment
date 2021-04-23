using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalPaymentProj.Models;
using HospitalPaymentProj.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalPaymentProj.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        #region End Point To Authenticate A User
        /// <summary>
        /// End Point To Handle Login Request 
        /// </summary>
        /// <param name="request">The Request Object consisting of the appropriate User Login Credentials</param>
        /// <returns>A String Status</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Users request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    var requestResult = await _userRepo.AuthenticateUser(request);
                    if (requestResult != "0")
                    {
                        return StatusCode(200, requestResult);
                    }
                    else
                    {
                        return StatusCode(404, "User not found or Account is in-active");
                    }


                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"The following error occured while attempting the Login: {ex.Message}");
                }
            }
        }
        #endregion End Point To Authenticate A User

        #region End Point To Register A User
        /// <summary>
        /// End Point To Handle Login Request 
        /// </summary>
        /// <param name="request">The Request Object consisting of the appropriate User Login Credentials</param>
        /// <returns>A String Status</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Users request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    var requestResult = await _userRepo.RegisterUser(request);
                    if (requestResult != false)
                    {
                        return StatusCode(200, requestResult);
                    }
                    else
                    {
                        return StatusCode(404, "User not found or Account is in-active");
                    }


                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"The following error occured while attempting the Login: {ex.Message}");
                }
            }
        }
        #endregion End Point To Register A User

        #region Endpoint to Create Patient Record
        /// <summary>
        /// End Point To Create Patient Record
        /// </summary>
        /// <param name="request">The Request Object consisting of the appropriate User Login Credentials</param>
        /// <returns>A String Status</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("SavePatientData")]
        public async Task<IActionResult> SavePatientData(PatientPayment_VM _patientDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    var requestResult = await _userRepo.CreatePatientRecord(_patientDetails);
                    if (requestResult == "Created successfully")
                    {
                        return StatusCode(200, requestResult);
                    }
                    else
                    {
                        return StatusCode(404, "User not found or Account is in-active");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"The following error occured while attempting the Login: {ex.Message}");
                }
            }
        }
        #endregion

        #region Endpoint to Get Patients Record
        /// <summary>
        /// End Point To Get Patient's Records
        /// </summary>
        /// <param name="request">The Request Object consisting of the appropriate User Login Credentials</param>
        /// <returns>A String Status</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("GetPatientData")]
        public async Task<IActionResult> GetPatientData()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    var requestResult = await _userRepo.GetPatientRecord();
                    if (requestResult.Count > 0)
                    {
                        return StatusCode(200, requestResult);
                    }
                    else
                    {
                        return StatusCode(404, "No record found");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"The following error occured while attempting the Login: {ex.Message}");
                }
            }
        }
        #endregion
    }
}