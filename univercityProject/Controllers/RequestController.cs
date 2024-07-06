using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using univercityProject.Models.DBModel;
using univercityProject.Models.Dtos;
using univercityProject.Services;

namespace univercityProject.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : Controller
    {
        private readonly IRequestService _requestRepository;
        private readonly IUserservices _userServices;

        public RequestController(IRequestService requestRepository, IUserservices userServices)
        {
            _requestRepository = requestRepository;
            _userServices = userServices;
        }

        // Employee creates a request
        [HttpPost("Create")]
        [Authorize(Roles = "Employee")]
        public ActionResult CreateRequest([FromBody] RequestViewModel requestViewModel)
        {
            if (ModelState.IsValid)
            {
                var request = new Request
                {

                    Title = requestViewModel.Title,
                    Body = requestViewModel.Body,
                    CreateDate = DateTime.Now,
                    ManagerAccepted = false,
                    CEOIsAccepted = false,
                    StartDate = DateTime.MinValue,
                    EndDate = DateTime.MinValue,
                    Doscription = requestViewModel.Doscription,
                    User_id = _userServices.GetUserById(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)).ID
                };

                _requestRepository.AddRequest(request);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        
        [HttpPut("ManagerAccept/{id}")]
        [Authorize(Roles = "Manager")]
        public ActionResult ManagerAcceptRequest(int id, [FromBody] string managerComments)
        {
            var request = _requestRepository.GetRequestById(id);
            if (request == null)
            {
                return NotFound();
            }

            request.ManagerAccepted = true;
            request.Doscription = managerComments;
            _requestRepository.UpdateRequest(new Request()
            {
                Body = request.Body,
                Title = request.Title,
                CEOIsAccepted = request.CEOIsAccepted,
                CreateDate = request.CreateDate,
                ManagerAccepted = request.ManagerAccepted,
                Doscription = request.Doscription,
                Req_ID = request.Req_ID,
                EndDate= request.EndDate,
                StartDate = request.StartDate,
                User_id = request.UserID
            });

            return Ok();
        }

        // CEO accepts the request and sets start and end dates
        [HttpPut("CEOAccept/{id}")]
        [Authorize(Roles = "CEO")]
        public ActionResult CEOAcceptRequest(int id, [FromBody] RequestViewModel requestViewModel)
        {
            var request = _requestRepository.GetRequestById(id);
            if (request == null)
            {
                return NotFound();
            }

            request.CEOIsAccepted = true;
            request.StartDate = requestViewModel.StartDate;
            request.EndDate = requestViewModel.EndDate;
            request.Doscription = requestViewModel.Doscription;
            _requestRepository.UpdateRequest(new Request()
            {
                Body = request.Body,
                Title = request.Title,
                CEOIsAccepted = request.CEOIsAccepted,
                CreateDate = request.CreateDate,
                ManagerAccepted = request.ManagerAccepted,
                Doscription = request.Doscription,
                Req_ID = request.Req_ID,
                EndDate = request.EndDate,
                StartDate = request.StartDate,
                User_id = request.UserID
            });

            return Ok();
        }

        // Get all requests
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<RequestListViewModel>> GetAllRequests()
        {
            var requests = _requestRepository.GetAllRequests()
                .Select(r => new RequestListViewModel
                {
                    Req_ID = r.Req_ID,
                    Title = r.Title,
                    UserID = r.UserID, // Mock data: You need to get the actual user ID
                    User_FirstName = r.User_FirstName, // Mock data: You need to get the actual first name
                    User_LastName = r.User_LastName // Mock data: You need to get the actual last name
                }).ToList();

            return Ok(requests);
        }

        // Get request details by ID
        [HttpGet("Details/{id}")]
        public ActionResult<RequestViewModel> GetRequestDetails(int id)
        {
            var request = _requestRepository.GetRequestById(id);
            var use = _userServices.GetUserById(request.UserID);
            if (request == null)
            {
                return NotFound();
            }

            var requestViewModel = new RequestViewModel
            {
                Req_ID = request.Req_ID,
                Title = request.Title,
                Body = request.Body,
                CreateDate = request.CreateDate,
                ManagerAccepted = request.ManagerAccepted,
                CEOIsAccepted = request.CEOIsAccepted,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Doscription = request.Doscription,
                UserID = use.ID, // Mock data: You need to get the actual user ID
                User_FirstName = use.FirstName, // Mock data: You need to get the actual first name
                User_LastName = use.LastName // Mock data: You need to get the actual last name
            };

            return Ok(requestViewModel);
        }
    }
}

