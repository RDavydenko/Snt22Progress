using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.Contracts.Infrastructure;
using Snt22Progress.Contracts.Models.Base;

namespace Snt22Progress.Contracts.Models.Questions
{
	public class QuestionDto : IDto<int>
	{
		public int Id { get; set; }

		public string Text { get; set; }

		/// <summary>
		/// Уже проголосовал пользователь или нет
		/// </summary>
		public bool IsVoted { get; set; } = false;

		public ChoiseDto[] Choises { get; set; }

		public DateTime Created { get; set; }

		public Creator Creator { get; set; }

		public DateTime? Edited { get; set; }

		public Editor Editor{ get; set; }
	}
}
