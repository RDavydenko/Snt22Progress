using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;

namespace Snt22Progress.DataAccess.Models
{
	public class QuestionFullDto
	{
		private string separator = "|";

		public int id { get; set; }

		public string text { get; set; }

		/// <summary>
		/// Строка, содержащая идентификаторы вариантов ответа через разделитель
		/// </summary>
		public string choise_ids { get; set; }

		public int[] _choise_ids
		{
			get
			{
				return choise_ids?.Split(separator, StringSplitOptions.None)
					?.Select(x => {
						var parsed = int.TryParse(x, out int y);
						return parsed ? y : 0;
					})?.ToArray() ?? Array.Empty<int>();
			}
		}

		/// <summary>
		/// Строка, содержащая названия вариантов ответа через разделитель
		/// </summary>
		public string choise_texts { get; set; }

		public string[] _choise_texts
		{
			get
			{
				return choise_texts
					?.Split(separator, StringSplitOptions.None)
					?.ToArray() ?? Array.Empty<string>();
			}
		}

		/// <summary>
		/// Строка, содержащая количество голосов вариантов ответа через разделитель
		/// </summary>
		public string choise_votes_counts { get; set; }

		public int[] _choise_votes_counts
		{
			get
			{
				return choise_votes_counts?.Split(separator, StringSplitOptions.None)
					?.Select(x => {
						var parsed = int.TryParse(x, out int y);
						return parsed ? y : 0;
					})?.ToArray() ?? Array.Empty<int>();
			}
		}

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

