using Microsoft.AspNetCore.Mvc;
using RabotUpdateManager.Abstractions;


namespace RabotUpdateManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UpdateController : Controller
    {
        private readonly ILogger<UpdateController> logger;
        private readonly IDisplayManager displayManager;
        private readonly IUpdateManager updateManager;

        public UpdateController(ILogger<UpdateController> logger, IDisplayManager displayManager, IUpdateManager updateManager)
        {
            this.logger = logger;
            this.displayManager = displayManager;
            this.updateManager = updateManager;
        }


        [HttpGet(Name = "runTestDisplay")]
        public string runTestDisplay()
        {
            logger.LogInformation("runTestDisplay endpoint");
            displayManager.test();
            return "done";
        }


        //[HttpGet(Name = "checkServiceStatus")]
        //public string checkServiceStatus()
        //{
        //    logger.LogInformation("checkServiceStatus endpoint");
        //    updateManager.CheckStatus();
        //    return "done";
        //}
    }
}
