using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using MySqlX.XDevAPI.Relational;
using Newtonsoft.Json;

namespace Sudoku.Model.Infrastructure
{
	public class SudokuTableDbEntity
	{
		public int Id { get; set; }

		public string TableValues { get; set; }

		public string RightAnswers { get; set; }

		public int Level { get; set; }

		public IEnumerable<int> DeserializeTableValues()
		{
			return JsonConvert.DeserializeObject<List<int>>(TableValues);
		}

		public IEnumerable<int> DeserializeRightAnswers()
		{
			return JsonConvert.DeserializeObject<List<int>>(RightAnswers);
		}
	}
}