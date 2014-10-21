module GameOfLife.Tests

open NUnit.Framework
open FsUnit

open GameOfLife


[<Test>]
let ``A blinker turns from horizontal to vertical`` () =
    let blinker = CellPattern [x ; x ; x ;
                               x ; o ; x ;
                               x ; o ; x ;
                               x ; o ; x ]


    let invertedBlinker = CellPattern [x ; x ; x ;
                                       o ; o ; o ;]

    GameOfLife.Evolve blinker |> should equal invertedBlinker
    GameOfLife.Evolve invertedBlinker |> should equal blinker
        