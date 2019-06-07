using System;
using System.Collections.Generic;
using Sudoku.Model.Domain;

namespace Sudoku.Web.Models
{
	public class CheckResultsModel
	{
		public CheckResultsModel(IEnumerable<CheckResult> checkResults)
		{
			CheckResults = checkResults ?? throw new ArgumentNullException(nameof(checkResults));
		}

		public IEnumerable<CheckResult> CheckResults { get; }
	}
}