module GameOfLife

type Cell =
    | Alive
    | Dead

let x = Alive
let o = Dead

type Position = int * int

type CellPattern (size: int, startCells: Cell List) = 

    let rec chop segmentSize source = 
        seq { 
                if Seq.isEmpty source then () else
                let segment = source |> Seq.truncate segmentSize
                let rest = source |> Seq.skip (Seq.length segment)
                yield segment
                yield! chop segmentSize rest 
        }
    
    let parseCells board =
        let cell2Pos row col state =
            match state with 
            | Alive -> Some(row, col)
            | _ -> None

        let row2Pos row cells = cells |> Seq.mapi (cell2Pos row)

        let map2Cells cells = cells |> Seq.mapi row2Pos

        chop 3 board
        |> map2Cells
        |> Seq.concat
        |> Seq.choose id

    member this.Parsed = parseCells startCells

    override this.Equals obj =
        match obj with
        | :? CellPattern as other -> this.Parsed = other.Parsed
        | _ -> false

    override this.GetHashCode () =
        startCells.GetHashCode()

let Evolve (pattern: CellPattern) =
    pattern