module GameOfLife

open Gol.Model

let LoadCells size board =
    let rec chop segmentSize source = 
        seq { 
                if Seq.isEmpty source then () else
                let segment = source |> Seq.truncate segmentSize
                let rest = source |> Seq.skip (Seq.length segment)
                yield segment
                yield! chop segmentSize rest 
        }
    
    let cell2Pos row col state =
        match state with 
        | Alive -> Some(row, col)
        | _ -> None

    let row2Pos row cells = cells |> Seq.mapi (cell2Pos row)

    let map2Cells cells = cells |> Seq.mapi row2Pos

    CellPattern(chop size board
                |> map2Cells
                |> Seq.concat
                |> Seq.choose id
                |> Set.ofSeq)


let Evolve (pattern: CellPattern) =
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
        match aliveNeighbours cell |> List.length with
        | 2 when pattern.Contains cell -> Some cell
        | 3 -> Some cell
        | _ -> None
    
    let rangeIncludingDeadNeighbours () = pattern |> Seq.collect neighbours
    
    CellPattern(rangeIncludingDeadNeighbours ()
                |> Seq.map applyRules
                |> Seq.choose id)



module Patterns =

    let Blinker = LoadCells 3 [x ; o ; x ;
                               x ; o ; x ;
                               x ; o ; x ]
