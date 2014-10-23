namespace GameOfLife.Tests

open NUnit.Framework
open FsUnit

open GameOfLife

module ``CellPattern tests`` =
    [<Test>]
    let ``Parsing a block`` () =
        let block = CellPattern(2, [o ; o ;
                                    o ; o ;])

        block.Parsed |> should equal [ 0, 0 ; 0, 1 ; 1, 0 ; 1, 1]

    [<Test>]
    let ``Parsing a horizontal blinker`` () =
        let blinker = CellPattern(3, [x ; x ; x ;
                                      x ; o ; x ;
                                      x ; o ; x ;
                                      x ; o ; x ])

        blinker.Parsed |> should equal [ 1, 1 ; 2, 1 ; 3, 1]

        