﻿using ProjectBj.BusinessLogic.Services.Interfaces;
using ProjectBj.Logger;
using ProjectBj.ViewModels.History;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProjectBj.MVC.Controllers.Api
{
    public class HistoryController : ApiController
    {
        private readonly IHistoryService _service;

        public HistoryController(IHistoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetFullHistory()
        {
            try
            {
                GetFullHistoryHistoryView view = await _service.GetFullHistory();
                return Ok(view);
            }
            catch (Exception exception)
            {
                Log.Error(exception.ToString());
                return InternalServerError(exception);
            }
        }
    }
}