using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;

namespace Sudoku.Model.Infrastructure
{
	public class SudokuTableRepository
	{
		public SudokuTableRepository(string connectionString)
		{
			_connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
		}

		public SudokuTableDbEntity GetSudokuTableById(int id)
		{
			using (var connection = new MySqlConnection(_connectionString))
			{
				return connection.Query<SudokuTableDbEntity>("SELECT * FROM sudoku_puzzles WHERE id = @id", new {id})
					.FirstOrDefault();
			}
		}

		public SudokuTableDbEntity GetSudokuTableByLevel(int level)
		{
			using (var connection = new MySqlConnection(_connectionString))
			{
				return connection
					.Query<SudokuTableDbEntity>("SELECT * FROM sudoku_puzzles WHERE level = @level", new {level})
					.Shuffle()
					.FirstOrDefault();
			}
		}

		private readonly string _connectionString;
	}
}