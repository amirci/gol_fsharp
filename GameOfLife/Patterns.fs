module GameOfLife.Patterns

open Loader
open Types

let Blinker = LoadCells 3 [x ; o ; x ;
                           x ; o ; x ;
                           x ; o ; x ]


let Beehive = LoadCells 4 [__ ; oo ; oo ; __ ;
                           oo ; __ ; __ ; oo ;
                           __ ; oo ; oo ; __ ]


let Block = LoadCells 2 [o ; o ;
                         o ; o ;]

let Glider = LoadCells 3 [o ; x ; x ;
                          x ; o ; o ;
                          o ; o ; x ]

    
