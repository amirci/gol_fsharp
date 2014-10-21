module GameOfLife


type Cell =
    | Alive
    | Dead

let x = Alive
let o = Dead

type CellPattern (cells: Cell List) = 

    member this.x = "F#"

let Evolve (pattern: CellPattern) =
    pattern