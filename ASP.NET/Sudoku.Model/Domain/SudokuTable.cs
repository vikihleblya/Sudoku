using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Model.Domain
{
	public enum CheckResult
	{
		Wrong = 0,

		Right = 1
	}

	public class SudokuTable
	{
		public SudokuTable(int id, IEnumerable<int> tableValues, IEnumerable<int> rightAnswers)
		{
			Id = id;
			TableValues = tableValues ?? throw new ArgumentNullException(nameof(tableValues));
			_rightAnswers = rightAnswers ?? throw new ArgumentNullException(nameof(rightAnswers));
		}
		
		public int Id { get; }

		public IEnumerable<int> TableValues { get; private set; }

		public void FillSudokuTable(IEnumerable<int> values)
		{
			TableValues = values;
		}

		public IEnumerable<CheckResult> CheckAnswers()
		{
			if(TableValues.Count() != _rightAnswers.Count())
				throw new ArgumentException($"{nameof(_rightAnswers)} length != {nameof(TableValues)}");

			return TableValues.Select((value, index) =>
				value == _rightAnswers.ElementAt(index) ? CheckResult.Right : CheckResult.Wrong);
		}

		private readonly IEnumerable<int> _rightAnswers;
	}
}