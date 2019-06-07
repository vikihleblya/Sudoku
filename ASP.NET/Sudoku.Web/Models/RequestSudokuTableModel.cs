using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sudoku.Web.Models
{
	public class RequestSudokuTableModel
	{
		[JsonProperty(PropertyName = "id")]
		public int Id { get; set; }

		[JsonProperty(PropertyName = "answers")]
		public List<int> TableAnswers { get; set; }
	}
}