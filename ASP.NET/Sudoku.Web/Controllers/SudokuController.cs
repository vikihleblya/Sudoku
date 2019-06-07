using System;
using Microsoft.AspNetCore.Mvc;
using Sudoku.Model;
using Sudoku.Model.Domain;
using Sudoku.Web.Models;

namespace Sudoku.Web.Controllers
{
	public class SudokuController : Controller
	{
		public SudokuController(SudokuTableManager sudokuManager)
		{
			_sudokuManager = sudokuManager ?? throw new ArgumentNullException(nameof(sudokuManager));
		}

		[HttpGet]
		[Route("sudokus")]
		public IActionResult GetRandomSudokuTableValues([FromQuery] int level)
		{
			var tableValues = _sudokuManager.GetRandomSudokuTableValues(level);
			var response = new ResponseSudokuTableModel(tableValues.Id, tableValues.Values);
			return new JsonResult(response);
		}

		[HttpPut]
		[Route("sudokus/{id}")]
		public IActionResult CheckAnswers([FromBody] RequestSudokuTableModel sudokuTableModel)
		{
			var checkResults = _sudokuManager.CheckAnswers(sudokuTableModel.Id, sudokuTableModel.TableAnswers);
			var response = new CheckResultsModel(checkResults);
			return new JsonResult(response);
		}

		private readonly SudokuTableManager _sudokuManager;
	}
}