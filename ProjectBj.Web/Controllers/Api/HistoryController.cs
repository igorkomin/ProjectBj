﻿using ProjectBj.BusinessLogic.Services.Interfaces;
using ProjectBj.Logger;
using ProjectBj.ViewModels.History;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProjectBj.Web.Controllers.Api
{
    public class HistoryController : ApiController
    {
        private readonly IHistoryService _service;

        public HistoryController(IHistoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetGameHistory([FromBody] int sessionId)
        {
            try
            {
                GetGameHistoryHistoryView view = await _service.GetGameHistory(sessionId);
                return Ok(view);
            }
            catch (Exception exception)
            {
                Log.Error(exception.ToString());
                return InternalServerError(exception);
            }
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