import UIKit

class ViewController: UIViewController {

    let numberToNumberTable = [1,2,3,4,5,6,7,8,9]
    var sudokuModel: SudokuModel?{
        didSet{
            numberToSudoku = sudokuModel!.sudokuPuzzle
            responseNumberToSudoku = sudokuModel!.sudokuPuzzle
        }
    }
    var numberToSudoku:[Int] = []{
        didSet{
            DispatchQueue.main.async {
                self.sudokuTable.reloadData()
            }
        }
    }
    var responseNumberToSudoku: [Int] = []
    var answersToSudoku: [Int] = []{
        didSet{
            DispatchQueue.main.async {
                self.sudokuTable.reloadData()
            }
        }
    }
    
    var networkService = NetworkService()
    var indexPathRow: Int = 0
    var cell: SudokuCollectionViewCell? = nil
    
    @IBOutlet var levelSegmentedControl: UISegmentedControl!
    @IBOutlet var startIntroductionLabel: UILabel!
    @IBOutlet var sudokuTable: UICollectionView!
    @IBOutlet var sudokuNumberTable: UICollectionView!
    
    @IBAction func checkSudoku(_ sender: Any) {
        networkService.checkSudoku(sudoku: SudokuModel(id: sudokuModel!.id, sudoku: responseNumberToSudoku)) { (answers, err) in
            self.answersToSudoku = answers
        }
    }
    @IBAction func getSudokuTable(_ sender: Any) {
        startIntroductionLabel.isHidden = true
        sudokuTable.isHidden = false
        networkService.getSudoku(level: levelSegmentedControl.selectedSegmentIndex + 1) { (sudoku) in
            self.sudokuModel = sudoku
        }
    }
    
    
    override func viewDidLoad() {
        super.viewDidLoad()
        sudokuNumberTable.delegate = self
        sudokuTable.delegate = self
        sudokuNumberTable.dataSource = self
        sudokuTable.dataSource = self
    }


}

extension ViewController: UICollectionViewDelegate, UICollectionViewDataSource, UICollectionViewDelegateFlowLayout{
    func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        if collectionView == sudokuTable{
            return numberToSudoku.count
        }
        else{
            return numberToNumberTable.count
        }
    }
    
    func collectionView(_ collectionView: UICollectionView, didSelectItemAt indexPath: IndexPath) {
        if collectionView == sudokuTable{
            cell = collectionView.cellForItem(at: indexPath) as! SudokuCollectionViewCell
            self.indexPathRow = indexPath.row
            checkColor(cell: cell!, indexPath: indexPath)
        }
        else{
            if let selectedCell = cell{
                let cellNumber = collectionView.cellForItem(at: indexPath) as? CollectionViewCell
                if self.numberToSudoku[self.indexPathRow] == 0{
                    selectedCell.number.text = cellNumber?.number.text
                    selectedCell.number.textColor = .gray
                    self.responseNumberToSudoku[self.indexPathRow] = Int(cellNumber!.number.text!)!
                }
            }
        }
    }
    
    func checkColor(cell: SudokuCollectionViewCell, indexPath: IndexPath){
        for cell in sudokuTable.visibleCells{
            let cellColor = cell as! SudokuCollectionViewCell
            cellColor.backgroundColor = .white
        }
        if self.numberToSudoku[indexPath.row] == 0{
            cell.backgroundColor = .yellow
        }
       
    }
    
    func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        if collectionView == sudokuTable{
            let cell = collectionView.dequeueReusableCell(withReuseIdentifier: "cellSudoku", for: indexPath) as! SudokuCollectionViewCell
            cell.layer.borderColor = UIColor.black.cgColor
            cell.layer.borderWidth = CGFloat(integerLiteral: 1)
            
            if !self.answersToSudoku.isEmpty && self.answersToSudoku[indexPath.row] == 0{
                cell.backgroundColor = .red
            }
            
            if self.responseNumberToSudoku[indexPath.row] != 0{
                cell.number.text = "\(self.responseNumberToSudoku[indexPath.row])"
            }
            else{
                cell.number.text = ""
            }
            
            return cell
        }
        else{
            let cell = collectionView.dequeueReusableCell(withReuseIdentifier: "cellNumber", for: indexPath) as! CollectionViewCell
            cell.layer.backgroundColor = UIColor.lightGray.cgColor
            
            cell.layer.cornerRadius = cell.frame.height / 2
            cell.number.text = "\(self.numberToNumberTable[indexPath.row])"
            return cell
        }
    }
    
    func collectionView(_ collectionView: UICollectionView, layout collectionViewLayout: UICollectionViewLayout, sizeForItemAt indexPath: IndexPath) -> CGSize {
        let sizeForCell = collectionView.frame.width / CGFloat(integerLiteral: 9)
        return CGSize(width: sizeForCell, height: sizeForCell)
    }
    
    func collectionView(_ collectionView: UICollectionView, layout collectionViewLayout: UICollectionViewLayout, minimumLineSpacingForSectionAt section: Int) -> CGFloat {
        return 0.0
    }
    
    func collectionView(_ collectionView: UICollectionView, layout collectionViewLayout: UICollectionViewLayout, minimumInteritemSpacingForSectionAt section: Int) -> CGFloat {
        return 0.0
    }
    
}
