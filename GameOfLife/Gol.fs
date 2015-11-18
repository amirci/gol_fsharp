module GameOfLife.Gol

open Types

module Board =
    let isAlive c = List.exists ((=) c)

let private nbrs (a, b) = 
    [for i in -1..1 do 
        for j in -1..1 do 
            if i <> 0 || j <> 0 then yield (a + i, b + j)]

let Evolve board =
    let rules (c, cells) =
        let n = Seq.length cells
        (n = 3) || (n = 2 && board |> Board.isAlive c)
    
    let findNeighbours = List.map nbrs >> List.concat
    let applyRules = Seq.sort >> Seq.groupBy id >> Seq.filter rules >> Seq.map fst

    board
    |> findNeighbours
    |> applyRules
    |> List.ofSeq







    
