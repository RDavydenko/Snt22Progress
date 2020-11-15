using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.Web.Api.Controllers
{
	[Produces("application/json")]
	public class BaseApiController : ControllerBase
	{
		public BaseApiController()
		{

		}
	}
}
