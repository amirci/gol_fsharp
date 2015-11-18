module GameOfLife.Types

type Cell =
    | Alive
    | Dead

let x = Dead
let o = Alive
let oo = Alive
let __ = Dead

type Position = int * int

type Board = Position list


