using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalPaymentProj.Models;
using HospitalPaymentProj.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalPaymentProj.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = "LoginScheme")]
    public class PatientController : Controller
    {
        private readonly IPaymentRepository _paymentRepo;
        public PatientController(IPaymentRepository paymentRepository)
        {
            _paymentRepo = paymentRepository;
        }

        public IActionResult Index()
        {
            var id = HttpContext.Session.GetString("UserID");
            if (id != null)
            {
                return View();
            }
            return RedirectToAction("Register_Login", "Home");
        }

        public async Task<IActionResult> SavePatientData(PatientPayment_VM _patientData)
        {
            try
            {
                _patientData.UserId = HttpContext.Session.GetString("UserID");
                var _registerUser = await _paymentRepo.CreateAsync(StaticDetails._savePatientData, _patientData);
                if (_registerUser == true)
                {
                    TempData["Created"] = "Created Successfully";
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            TempData["CreatedError"] = "Error creating user";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetPatientRecords()
        {
            try
            {
                var _registerUser = await _paymentRepo.GetAsyncAll(StaticDetails._getPatientData);

                return Json(_registerUser);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}