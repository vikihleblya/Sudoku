//
//  sudokuModel.swift
//  Sudoku
//
//  Created by Victor on 25/05/2019.
//  Copyright © 2019 com.example.LoD. All rights reserved.
//

import Foundation

struct SudokuModel: Codable {
    var id: Int
    var sudokuPuzzle: [Int]
    
    init(id: Int, sudoku: [Int]) {
        self.id = id
        self.sudokuPuzzle = sudoku
    }
}
