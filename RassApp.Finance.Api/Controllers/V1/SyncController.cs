using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RassApp.Finance.Application.Features.Sync;
using System;
using System.Collections.Generic;
using System.Text;

namespace RassAppFinanceiro.RassApp.Finance.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/sync")]
    [ApiVersion("1.0")]
    [Authorize]
    public class SyncController : ControllerBase
    {
        private readonly ISyncService _syncService;

        public SyncController(ISyncService syncService)
        {
            _syncService = syncService;
        }

        [HttpPost("push")]
        public async Task<IActionResult> Push([FromBody] SyncBatchDto batch)
        {
            await _syncService.ApplyChanges(batch);
            return Ok();
        }

        [HttpGet("pull")]
        public async Task<IActionResult> Pull([FromQuery] long lastVersion)
        {
            var changes = await _syncService.GetChanges(lastVersion);
            return Ok(changes);
        }
    }
}
