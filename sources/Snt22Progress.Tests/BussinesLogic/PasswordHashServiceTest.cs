using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snt22Progress.BussinesLogic.Interfaces;
using Snt22Progress.BussinesLogic.Services;

namespace Snt22Progress.Tests.BussinesLogic
{
	[TestClass]
	public class PasswordHashServiceTest
	{
		IPasswordHashService hashService = new PasswordHashService();

		[TestMethod]
		public async Task GetHashWithSalt()
		{
			var hash = await hashService.GetPasswordHashWithSalt("Aa_02345678", "S31fF!sLxz)$#");
		}
	}
}
