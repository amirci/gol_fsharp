module GameOfLife.Loader

open Types

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

    chop size board
    |> map2Cells
    |> Seq.concat
    |> Seq.choose id
    |> List.ofSeq
                
