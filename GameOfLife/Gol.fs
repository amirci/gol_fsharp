module GameOfLife.Gol

open Types

let Evolve (pattern: CellPattern) : CellPattern =
    let add (p1, p2) (q1, q2) = p1 + q1, p2 + q2

    let neighbours cell =
        [-1, -1 ; -1, 0 ; -1, 1; 
          0, -1 ;          0, 1; 
          1, -1 ;  1, 0 ;  1, 1]
        |> List.map (add cell)

    let aliveNeighbours cell =
        cell
        |> neighbours
        |> List.filter pattern.Contains

    let applyRules cell =
        let neighbourCount = cell |> aliveNeighbours |> List.length
        match neighbourCount with
        | 2 when pattern.Contains cell -> Some cell
        | 3 -> Some cell
        | _ -> None
    
    let cellsToEvaluate () = pattern |> Seq.collect neighbours
    
    let applyTheRules = Seq.map applyRules

    let chooseOnlyAlive = Seq.choose id

    CellPattern(cellsToEvaluate ()
                |> applyTheRules
                |> chooseOnlyAlive)





    
