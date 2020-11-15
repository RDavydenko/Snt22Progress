using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;

namespace Snt22Progress.Web.Api.Controllers
{
	[Produces("application/json")]
	public class BaseApiController : ControllerBase
	{
		public int? UserId { get; set; }

		public string UserEmail { get; set; }

		public BaseApiController()
		{
			if (User?.Identity?.IsAuthenticated == true)
			{
				var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? null;
				if (id != null)
				{
					UserId = int.Parse(id);
				}
				var email = User?.FindFirst(ClaimTypes.Email)?.Value ?? null;
				if (email != null)
				{
					UserEmail = email;
				}
			}
		}
	}
}
