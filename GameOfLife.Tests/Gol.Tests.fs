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
        

    [<Test>]
    let ``A glider keeps evolving and gliding`` () =
        let glider = LoadCells 3 [o ; x ; x ;
                                  x ; o ; o ;
                                  o ; o ; x ]


        let glider1 = LoadCells 3 [x ; o ; x ;
                                   x ; x ; o ;
                                   o ; o ; o ;]

        let glider2 = LoadCells 3 [x ; x ; x ;
                                   o ; x ; o ;
                                   x ; o ; o ;
                                   x ; o ; x ;]

        let glider3 = LoadCells 3 [x ; x ; x ;
                                   x ; x ; o ;
                                   o ; x ; o ;
                                   x ; o ; o ;]


        GameOfLife.Evolve glider  |> should equal glider1
        GameOfLife.Evolve glider1 |> should equal glider2        
        GameOfLife.Evolve glider2 |> should equal glider3