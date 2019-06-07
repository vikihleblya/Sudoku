using Sudoku.Model.Domain;
using Sudoku.Model.Infrastructure;

namespace Sudoku.Model
{
	public static class SudokuTableMapper
	{
		public static SudokuTable Map(SudokuTableDbEntity tableDto)
		{
			return new SudokuTable(tableDto.Id, tableDto.DeserializeTableValues(), tableDto.DeserializeRightAnswers());
		}
	}
}