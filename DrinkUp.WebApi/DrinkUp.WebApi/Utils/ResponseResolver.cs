using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DrinkUp.WebApi.Utils {

    public interface IResponseResolver {
        IActionResult GetResponse();
    }
    public class ResponseResolver : IResponseResolver {
    }
}
