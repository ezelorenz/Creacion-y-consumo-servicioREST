using Microsoft.AspNetCore.Mvc;
using ClientesAPI.Dto;
using System.Security.AccessControl;
using ClientesAPI.Utilidades;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ClientesAPI.Entidades;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Cors;

namespace ClientesAPI.Controllers
{
    [EnableCors("ReglasCors")]
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : Controller
    {
        private readonly AplicationDbContext aplicationDbContext;
        private readonly IMapper mapper;

        public CustomerController(AplicationDbContext aplicationDbContext, IMapper mapper)
        {
            this.aplicationDbContext = aplicationDbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerDTO>>> Get()
        {
            var customers = await aplicationDbContext.Customers.ToListAsync();

            return  mapper.Map<List<CustomerDTO>>(customers);
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<ActionResult<CustomerDTO>>GetbyId(int id)
        {
            var customer = await aplicationDbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return mapper.Map<CustomerDTO>(customer);
        }


        [HttpPost]
        public async Task<ActionResult> CreateCustomer([FromBody] CreateCustomerDTO createCustomerDTO)
        {
            var customer = mapper.Map<Customer>(createCustomerDTO);

            aplicationDbContext.Add(customer);
            await aplicationDbContext.SaveChangesAsync();

            var dto = mapper.Map<CustomerDTO>(customer);

            return new CreatedAtRouteResult("GetCustomer", new { id = customer.Id }, dto);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult>UpdateCustomer(int id, CreateCustomerDTO createCustomerDTO)
        {
            var customer =  await aplicationDbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            
            mapper.Map(createCustomerDTO, customer);

            aplicationDbContext.Entry(customer).State = EntityState.Modified;
            await aplicationDbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var customer =  await aplicationDbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);

            if (customer == null)
            {
                return NotFound();
            }
            aplicationDbContext.Entry(customer).State = EntityState.Deleted;

            await aplicationDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
