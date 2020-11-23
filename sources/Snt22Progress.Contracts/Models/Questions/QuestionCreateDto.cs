using System;
using System.Collections.Generic;
using System.Text;

namespace Snt22Progress.Contracts.Models.Questions
{
	public class QuestionCreateDto
	{
		public string Text { get; set; }

		public ChoiseCreateDto[] Choises { get; set; }
	}
}
