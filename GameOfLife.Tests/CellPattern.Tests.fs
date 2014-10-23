namespace GameOfLife.Tests

open NUnit.Framework
open FsUnit

open Gol.Model
open GameOfLife

module ``Parsing Board Tests`` =
    [<Test>]
    let ``Parsing a block`` () =
        let block = LoadCells 2 [o ; o ;
                                 o ; o ;]

        block |> should equal [ 0, 0 ; 0, 1 ; 1, 0 ; 1, 1]

    [<Test>]
    let ``Parsing a horizontal blinker`` () =
        let blinker = LoadCells 3 [x ; x ; x ;
                                   x ; o ; x ;
                                   x ; o ; x ;
                                   x ; o ; x ]

        blinker |> should equal [ 1, 1 ; 2, 1 ; 3, 1]

        