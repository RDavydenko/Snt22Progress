using System;
using System.Collections.Generic;
using System.Text;

namespace Snt22Progress.BussinesLogic.Models
{
	/// <summary>
	/// Класс для передачи информации постранично
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class PagingData<T>
	{
		/// <summary>
		/// Номер текущей страницы
		/// </summary>
		public int PageNumber { get; set; }

		/// <summary>
		/// Количество всего элементов
		/// </summary>
		public int Total { get; set; }

		/// <summary>
		/// Элементы на текущей странице
		/// </summary>
		public T[] Items { get; set; }

		public PagingData(T[] items, int pageNumber, int total)
		{
			Items = items;
			PageNumber = pageNumber;
			Total = total;
		}
	}
}
