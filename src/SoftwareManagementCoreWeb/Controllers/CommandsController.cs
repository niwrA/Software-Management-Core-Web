using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CommandsShared;
using ProductsShared;
using SoftwareManagementEFCoreRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftwareManagementCoreWeb.Controllers
{
    [Route("api/[controller]")]
    public class CommandsController : Controller
    {
        // GET: api/commands
        [HttpGet]
        public IEnumerable<ICommand> Get()
        {
            return new List<ICommand> { new CreateProductCommand(), new RenameProductCommand() };
        }

        // POST api/commands
        [HttpPost]
        public void Post([FromBody]IEnumerable<ICommand> commands)
        {
            // simple, direct execution like transaction
            // todo: save commands, then execute, for eventsourcing
            // todo: decide if mapped by $type or if manually mapped by CommandTypeId (probably,
            // combined with eventsourcing)

            // consider as transaction
            var repository = new MainRepository(new MainContext());
            foreach (var command in commands)
            {
                // get repository by command.CommandTypeId 
                command.EntityRepository = repository;
                command.Execute();
            }

            // complete transaction
            repository.PersistChanges();
        }

    }
}
