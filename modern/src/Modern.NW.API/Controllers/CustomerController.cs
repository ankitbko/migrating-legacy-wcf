using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modern.NW.API.Models;
using Modern.NW.Persistence;
using Modern.NW.Persistence.Models;

namespace Modern.NW.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IPersistence<Customer> repository;
        private readonly IMapper mapper;
        private readonly ILogger<CustomerController> logger;

        public CustomerController(
            IPersistence<Customer> repository,
            IMapper mapper,
            ILogger<CustomerController> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> Get()
        {
            try
            {
                var customers = await this.repository.GetAll().ToListAsync();
                return Ok(customers.Select(customer => mapper.Map<CustomerDto>(customer)));
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }
    }
}