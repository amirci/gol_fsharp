
open System
open GameOfLife
open Gol.Model

[<EntryPoint>]
let main argv =

    let rowMargin = 3
    let colMargin = 5
    let growth = 3
    let boardSize = 10

    let printPattern (pattern: CellPattern) =
        let showCell alive =
            let cell = match alive with
                        | true -> 
                            Console.ForegroundColor <- ConsoleColor.Yellow
                            'o'
                        | _ -> 
                            Console.ForegroundColor <- ConsoleColor.Cyan
                            'x'
            Console.Write cell
            Console.Write ' '

        let printCell row col =
            let factor = growth + 1
            seq { 0..growth - 1 }
            |> Seq.iter (fun rowAdj ->
                seq { 0..growth-1 }
                |> Seq.iter (fun colAdj -> 
                    Console.SetCursorPosition(
                        colMargin + col * factor + colAdj, 
                        rowMargin + row * factor + rowAdj)
                    let isAlive = pattern.Contains(row, col)
                    showCell isAlive
                ) 
            )

        let printRows row = seq { 0..boardSize } |> Seq.iter (printCell row)
    
        seq { 0..boardSize } |> Seq.iter printRows

        seq { 0..2 } |> Seq.iter (fun i -> printf "")

        Console.ResetColor()

    let rec showAndEvolveRepeteadly pattern count callback =
        let padLeft (s:string) = s.PadLeft (colMargin + boardSize * (growth+1))
        match count with
        | x when x > 0 -> 
            Console.SetCursorPosition(colMargin, 0)
            printf "%s" (count.ToString() |> padLeft)
            printPattern pattern
            Threading.Thread.Sleep 1000

            showAndEvolveRepeteadly (pattern |> Evolve) (count - 1) callback
        | _ -> 
            Console.SetCursorPosition(colMargin, 0)
            Console.Write(String(' ', 30))
            callback (pattern |> Evolve)

    let rec showAndEvolve pattern =
        printPattern pattern
        printf "\n\n"
        printf "Options:\n"
        printf " y : continue\n"
        printf " e : evolve 10 times\n"
        printf " any other key to quit\n"
    
        let yesOrNo = Console.ReadKey().KeyChar

        match yesOrNo with
        | 'y' -> showAndEvolve (pattern |> Evolve)
        | 'e' -> showAndEvolveRepeteadly (pattern |> Evolve) 10 showAndEvolve
        | _   -> printf "Thanks! you pressed %O\n" yesOrNo


    Console.Clear()
    showAndEvolve Patterns.Blinker


    0 // return an integer exit code
