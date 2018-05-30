using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCaching.Core;
using Microsoft.AspNetCore.Mvc;

namespace webApiWithCaching.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly TodoService _todoService;
        public TodoController(IEasyCachingProvider provider)
        {
            _todoService = new TodoService(provider);
        }

        [HttpGet]
        public ApiResponse Get()
        {
            var response = new ApiResponse();
            try
            {
                response.Data = _todoService.GetAll();
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.ErrorMessage = ex.Message;
            }
            response.ResponseDate = DateTime.Now;
            return response;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ApiResponse Get(int id)
        {
            var response = new ApiResponse();
            try
            {
                response.Data = _todoService.GetById(id);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.ErrorMessage = ex.Message;
            }
            response.ResponseDate = DateTime.Now;
            return response;
        }

        // POST api/values
        [HttpPost]
        public ApiResponse Post([FromBody]Todo value)
        {
            var response = new ApiResponse();
            try
            {
                response.Data = _todoService.Add(value);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.ErrorMessage = ex.Message;
            }
            response.ResponseDate = DateTime.Now;
            return response;
        }

        // PUT api/values/5
        [HttpPut()]
        public ApiResponse Put([FromBody]Todo value)
        {
            var response = new ApiResponse();
            try
            {
                response.Data = _todoService.Update(value);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.ErrorMessage = ex.Message;
            }
            response.ResponseDate = DateTime.Now;
            return response;
        }

        [HttpDelete("{id}")]
        public ApiResponse Delete(int id)
        {
            var response = new ApiResponse();
            try
            {
                response.Data = _todoService.Delete(id);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.ErrorMessage = ex.Message;
            }
            response.ResponseDate = DateTime.Now;
            return response;
        }
    }
}