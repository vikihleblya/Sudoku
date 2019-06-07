using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sudoku.Web.Models
{
	public class ResponseSudokuTableModel
	{
		public ResponseSudokuTableModel(int id, IEnumerable<int> tableValues)
		{
			Id = id;
			TableValues = tableValues ?? throw new ArgumentNullException(nameof(tableValues));
		}

		[JsonProperty(PropertyName = "id")]
		public int Id { get; }

		[JsonProperty(PropertyName = "values")]
		public IEnumerable<int> TableValues { get; }
	}
}