module GameOfLife.Tests

open NUnit.Framework
open FsUnit

open GameOfLife

[<Test>]
let ``A block never changes`` () =
    let block = CellPattern(3, [o ; o ;
                                o ; o ;])

    let sameBlock = CellPattern(3, [o ; o ;
                                    o ; o ;])


    GameOfLife.Evolve block |> should equal sameBlock


[<Test>]
let ``A blinker turns from horizontal to vertical`` () =
    let blinker = CellPattern(3, [x ; x ; x ;
                                  x ; o ; x ;
                                  x ; o ; x ;
                                  x ; o ; x ])


    let invertedBlinker = CellPattern(3, [x ; x ; x ;
                                          x ; x ; x ;
                                          o ; o ; o ;])

    GameOfLife.Evolve blinker |> should equal invertedBlinker
    GameOfLife.Evolve invertedBlinker |> should equal blinker
        