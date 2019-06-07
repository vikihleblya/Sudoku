import Foundation
import Alamofire

class NetworkService{
    func getSudoku(level: Int, completion: @escaping (SudokuModel) -> Void) {
        AF.request(URL(string: "http://localhost:5000/sudokus?level=\(level)")!)
            .responseJSON { response in
            guard let responseJSON = response.value as? [String: AnyObject],
                let id = responseJSON["id"] as? Int,
                let sudokuPuzzle = responseJSON["values"] as? [Int]
                else {return}
                completion(SudokuModel(id: id, sudoku: sudokuPuzzle))
        }
    }
    
    func checkSudoku(sudoku: SudokuModel, completion: @escaping (([Int], Error?)->Void)) {
        let parameters = ["id": sudoku.id, "answers": sudoku.sudokuPuzzle] as [String : Any]
        AF.request(URL(string: "http://localhost:5000/sudokus/\(sudoku.id)")!, method: .put, parameters: parameters, encoding: JSONEncoding.default , headers: nil, interceptor: nil).responseJSON { response in
            switch response.result{
            case .success(let value):
                if let responseJSON = response.value as? [String: AnyObject], let answers = responseJSON["checkResults"] as? [Int]{
                    completion(answers, nil)
                }
            case .failure(let error):
                print("Unable to get answers")
            }
        }
    }
    

}

public enum Result<Success, Failure: Error> {
    case success(Success), failure(Failure)
}
