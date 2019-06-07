using System;
using System.Collections.Generic;
using Sudoku.Model.Infrastructure;

namespace Sudoku.Model.Domain
{
	public class SudokuTableManager
	{
		public SudokuTableManager(SudokuTableRepository sudokuRepository)
		{
			_sudokuRepository = sudokuRepository ?? throw new ArgumentNullException(nameof(sudokuRepository));
		}

		public (int Id, IEnumerable<int> Values) GetRandomSudokuTableValues(int level)
		{
			var sudokuTable = _sudokuRepository.GetSudokuTableByLevel(level);
			var mappedTable = SudokuTableMapper.Map(sudokuTable);

			return (mappedTable.Id, mappedTable.TableValues);
		}

		public IEnumerable<CheckResult> CheckAnswers(int tableId, IEnumerable<int> answers)
		{
			var sudokuTable = _sudokuRepository.GetSudokuTableById(tableId);
			var mappedTable = SudokuTableMapper.Map(sudokuTable);
			mappedTable.FillSudokuTable(answers);

			return mappedTable.CheckAnswers();
		}
		private readonly SudokuTableRepository _sudokuRepository;
	}
}