namespace GameOfLife.Tests

open NUnit.Framework
open FsUnit

open GameOfLife
open Gol.Model

module ``GOL Acceptance tests`` = 
    [<Test>]
    let ``A block never changes`` () =
        let block = LoadCells 2 [o ; o ;
                                 o ; o ;]

        GameOfLife.Evolve block |> should equal block


    [<Test>]
    let ``A beehive never changes`` () =
        let beehive = LoadCells 4 [__ ; oo ; oo ; __ ;
                                   oo ; __ ; __ ; oo ;
                                   __ ; oo ; oo ; __ ]

        GameOfLife.Evolve beehive |> should equal beehive

    [<Test>]
    let ``A blinker turns from horizontal to vertical`` () =
        let blinker = LoadCells 3 [x ; o ; x ;
                                   x ; o ; x ;
                                   x ; o ; x ]


        let invertedBlinker = LoadCells 3 [x ; x ; x ;
                                           o ; o ; o ;]

        GameOfLife.Evolve blinker |> should equal invertedBlinker
        GameOfLife.Evolve invertedBlinker |> should equal blinker
        