namespace GameOfLife.Tests

open NUnit.Framework
open FsUnit

open GameOfLife
open GameOfLife.Types
open GameOfLife.Loader

module ``GOL Acceptance tests`` = 
    [<Test>]
    let ``A block never changes`` () =
        let block = LoadCells 2 [o ; o ;
                                 o ; o ;]

        block |> Gol.Evolve |> should equal block


    [<Test>]
    let ``A beehive never changes`` () =
        let beehive = LoadCells 4 [__ ; oo ; oo ; __ ;
                                   oo ; __ ; __ ; oo ;
                                   __ ; oo ; oo ; __ ]

        Gol.Evolve beehive |> should equal beehive

    [<Test>]
    let ``A blinker turns from horizontal to vertical`` () =
        let blinker = LoadCells 3 [x ; o ; x ;
                                   x ; o ; x ;
                                   x ; o ; x ]


        let invertedBlinker = LoadCells 3 [x ; x ; x ;
                                           o ; o ; o ;]

        Gol.Evolve blinker |> should equal invertedBlinker
        Gol.Evolve invertedBlinker |> should equal blinker
        

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


        Gol.Evolve glider  |> should equal glider1
        Gol.Evolve glider1 |> should equal glider2        
        Gol.Evolve glider2 |> should equal glider3