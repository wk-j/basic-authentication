using BasicAuthentication.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuthentication.Controllers {

    [Route("api/[controller]/[action]")]
    public class SecureController : ControllerBase {
        [BasicAuthorize(typeof(BasicAuthorizeFilter))]
        public dynamic SayHello() {
            return new { Hello = "World" };
        }
    }
}