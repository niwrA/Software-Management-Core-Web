using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CommandsShared;
using ProductsShared;
//using SoftwareManagementEFCoreRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftwareManagementCoreWeb.Controllers
{
    [Route("api/[controller]")]
    public class CommandsController : Controller
    {
        private IProductService _productService;
        private IProductStateRepository _productStateRepository;
        private ICommandRepository _commandRepository;

        public CommandsController(IProductService productService, IProductStateRepository productStateRepository, ICommandRepository commandRepository)
        {
            _productService = productService;
            _productStateRepository = productStateRepository;
            _commandRepository = commandRepository;
        }
        // GET: api/commands
        [HttpGet]
        public IEnumerable<ICommand> Get()
        {
            //var repository = new MainRepository(new MainContext());

            // return new List<ICommand> { new CreateProductCommand(repository), new RenameProductCommand(repository) };
            return new List<ICommand>();
        }

        // POST api/commands
        //[HttpPost]
        //public void Post([FromBody]IEnumerable<ICommand> commands)
        //{
        //    // simple, direct execution like transaction
        //    // todo: save commands, then execute, for eventsourcing
        //    // _commandRepository.PersistChanges();

        //    // consider as transaction
        //    var commandManager = new CommandManager();
        //    var commandConfig = new CommandConfig { Name = "Create", ProcessorName = "Project", Processor = _productService };

        //    foreach (var command in commands)
        //    {
        //        commandManager.ProcessCommand(command);
        //        // get repository by command.CommandTypeId 
        //    }

        //    _productStateRepository.PersistChanges();
        //}
        // POST api/commands
        [HttpPost]
        [Route("batch")]
        public ICommand Post([FromBody]ICommand command)
        {
            // simple, direct execution like transaction
            // todo: save commands, then execute, for eventsourcing
            // _commandRepository.PersistChanges();

            // consider as transaction
            var commandManager = new CommandManager();
            var commandConfig = new CommandConfig { Name = "Create", ProcessorName = "Project", Processor = _productService };
            commandManager.AddConfig(commandConfig);
            commandManager.ProcessCommand(command);
            _productStateRepository.PersistChanges();
            return command;
        }
        [HttpPost]
        public string PostJson([FromBody]string commandString)
        {
            // simple, direct execution like transaction
            // todo: save commands, then execute, for eventsourcing
            // _commandRepository.PersistChanges();

            // consider as transaction
            var commandManager = new CommandManager();
            var commandConfig = new CommandConfig { Name = "Create", ProcessorName = "Project", Processor = _productService };
            commandManager.AddConfig(commandConfig);
//            commandManager.ProcessCommand(command);
            _productStateRepository.PersistChanges();
            return commandString;
        }

    }
}
