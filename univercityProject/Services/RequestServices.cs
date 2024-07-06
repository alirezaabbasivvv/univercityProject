using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using univercityProject.Models.Context;
using univercityProject.Models.DBModel;
using univercityProject.Models.Dtos;

namespace univercityProject.Services
{
    public interface IRequestService
    {
        RequestViewModel GetRequestById(int id);
        IEnumerable<RequestListViewModel> GetAllRequests();
        void AddRequest(Request request);
        bool UpdateRequest(Request request);
        bool RemoveRequest(int id);
        void AcceptFromManager(int id);
        void AcceptFromCEO(int id, DateTime start, DateTime end);
    }

    public class RequestService : IRequestService
    {
        private readonly UNDBContext _context;

        public RequestService(UNDBContext context)
        {
            _context = context;
        }

        public RequestViewModel GetRequestById(int id)
        {
            try
            {
                return _context.Requests.Where(p => p.Req_ID == id).Include(p => p.User)
                    .Select(p =>
                    new RequestViewModel() 
                    {
                        Req_ID = p.Req_ID, 
                        Body = p.Body, 
                        CEOIsAccepted = p.CEOIsAccepted, 
                        CreateDate = p.CreateDate, 
                        Doscription = p.Doscription, 
                        ManagerAccepted = p.ManagerAccepted,
                        Title = p.Title, 
                        UserID = p.User.ID, 
                        User_FirstName = p.User.FirstName, 
                        User_LastName = p.User.LastName 
                    }).FirstOrDefault();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IEnumerable<RequestListViewModel> GetAllRequests()
        {
            try
            {
                return _context.Requests.Include(p => p.User).Select(p => new RequestListViewModel() { Req_ID = p.Req_ID, Title = p.Title, UserID = p.User.ID, User_FirstName = p.User.FirstName, User_LastName = p.User.LastName }).ToList();
            }
            catch (Exception ex)
            {
                return new List<RequestListViewModel>();
            }
        }

        public void AddRequest(Request request)
        {
            if (request == null || !ValidateRequest(request))
                return;

            try
            {
                _context.Requests.Add(request);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        public bool UpdateRequest(Request request)
        {
            if (request == null || !ValidateRequest(request))
                return false;

            try
            {
                var existingRequest = _context.Requests.FirstOrDefault(r => r.Req_ID == request.Req_ID);
                if (existingRequest == null)
                    return false;

                existingRequest.Title = request.Title;
                existingRequest.Body = request.Body;
                existingRequest.CreateDate = request.CreateDate;
                existingRequest.ManagerAccepted = request.ManagerAccepted;
                existingRequest.CEOIsAccepted = request.CEOIsAccepted;
                existingRequest.Doscription = request.Doscription;
                existingRequest.StartDate = request.StartDate;
                existingRequest.EndDate = request.EndDate;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool RemoveRequest(int id)
        {
            try
            {
                var request = _context.Requests.FirstOrDefault(r => r.Req_ID == id);
                if (request == null)
                    return false;

                _context.Requests.Remove(request);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool ValidateRequest(Request request)
        {
            if (string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Body))
                return false;

            if (request.CreateDate == default)
                return false;

            return true;
        }

        public void AcceptFromManager(int id)
        {
            try
            {
                var existingRequest = _context.Requests.FirstOrDefault(r => r.Req_ID == id);
                if (existingRequest == null)
                    return;

                existingRequest.ManagerAccepted = true;

                _context.SaveChanges();
                return;
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public void AcceptFromCEO(int id, DateTime start, DateTime end)
        {
            try
            {
                var existingRequest = _context.Requests.FirstOrDefault(r => r.Req_ID == id);
                if (existingRequest == null)
                    return;

                existingRequest.ManagerAccepted = true;
                existingRequest.CEOIsAccepted = true;
                existingRequest.StartDate = start;
                existingRequest.EndDate = end;

                _context.SaveChanges();
                return;
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}



