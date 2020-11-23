using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;

namespace Snt22Progress.DataAccess.Models
{
	public class QuestionView : IBaseEntity<int>
	{
		public int id { get; set; }

		public string text { get; set; }

		/// <summary>
		/// Строка, содержащая идентификаторы вариантов ответа через разделитель
		/// </summary>
		public string choise_ids { get; set; }

		/// <summary>
		/// Строка, содержащая названия вариантов ответа через разделитель
		/// </summary>
		public string choise_texts { get; set; }

		/// <summary>
		/// Строка, содержащая количество голосов вариантов ответа через разделитель
		/// </summary>
		public string choise_votes_counts { get; set; }

		public DateTime created { get; set; }

		public int? creator_id { get; set; }
		public string creator_fname { get; set; }
		public string creator_lname { get; set; }
		public string creator_mname { get; set; }

		public DateTime? edited { get; set; }

		public int? editor_id { get; set; }
		public string editor_fname { get; set; }
		public string editor_lname { get; set; }
		public string editor_mname { get; set; }

		public bool is_active { get; set; }
	}
}
