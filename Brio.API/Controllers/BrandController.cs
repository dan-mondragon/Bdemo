using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Brio.Domain.Supervisors;
using Brio.Domain.Utils;
using Brio.Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Brio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandSupervisor _supervisor;
        public BrandController(IBrandSupervisor supervisor)
        {
            _supervisor = supervisor;
        }

        [HttpGet]
        [Produces(typeof(List<BrandViewModel>))]
        public async Task<IActionResult> Get([FromQuery]PagingParameter pagingParameter, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                var results = await _supervisor.GetAllBrandAsync(pagingParameter, ct);
                
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(results.Item2));
                return new ObjectResult(results.Item1);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [Produces(typeof(BrandViewModel))]
        public async Task<IActionResult> Get(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (await _supervisor.GetBrandByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                return Ok(await _supervisor.GetBrandByIdAsync(id, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Produces(typeof(BrandViewModel))]
        public async Task<IActionResult> Post([FromBody]BrandViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, await _supervisor.AddBrandAsync(input, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        [Produces(typeof(BrandViewModel))]
        public async Task<IActionResult> Put(int id, [FromBody]BrandViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (await _supervisor.GetBrandByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }

                input.BrandId = id;
                if (await _supervisor.UpdateBrandAsync(input, ct))
                {
                    return Ok(input);
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{id}")]
        [Produces(typeof(void))]
        public async Task<ActionResult> Delete(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (await _supervisor.GetBrandByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }

                if (await _supervisor.DeleteBrandAsync(id, ct))
                {
                    return Ok();
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}