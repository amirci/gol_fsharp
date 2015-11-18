
open System
open GameOfLife
open GameOfLife
open GameOfLife.Types
open GameOfLife.Patterns

[<EntryPoint>]
let main argv =

    let rowMargin = 3
    let colMargin = 5
    let growth = 3
    let boardSize = 10

    let all = [
        "Blinker", Blinker 
        "Beehive", Beehive 
        "Block"  , Block 
        "Glider" , Glider
    ]

    let evolve (name, pattern) = name, (pattern |> Gol.Evolve)

    let printPattern (name, pattern: Board) =
        let showCell alive =
            let cell = 
                match alive with
                | true -> Console.ForegroundColor <- ConsoleColor.Yellow ; 'o'
                | _    -> Console.ForegroundColor <- ConsoleColor.Cyan ; 'x'
            Console.Write cell
            Console.Write ' '

        let printCellAt row col =
            let factor = growth + 1
            seq { 0..growth - 1 }
            |> Seq.iter (fun rowAdj ->
                seq { 0..growth-1 }
                |> Seq.iter (fun colAdj -> 
                    Console.SetCursorPosition(
                        colMargin + col * factor + colAdj, 
                        rowMargin + row * factor + rowAdj)
                    showCell (pattern |> Gol.Board.isAlive (row, col))
                ) 
            )

        let printCols row = seq { 0..boardSize } |> Seq.iter (printCellAt row)
    
        Console.SetCursorPosition(colMargin, 0)
        Console.ForegroundColor <- ConsoleColor.Yellow
        printf "%s" name

        seq { 0..boardSize } |> Seq.iter printCols

        seq { 0..2 } |> Seq.iter (fun i -> printf "")

        Console.ResetColor()

    let rec repeat count pattern  =
        let lineLength = colMargin + boardSize * (growth + 1) - 1

        match count with
        | x when x > 0 -> 
            Console.SetCursorPosition(colMargin, 0)
            printf "%s" (count.ToString().PadLeft lineLength)
            printPattern pattern
            Threading.Thread.Sleep 200
            pattern |> evolve |> repeat (count - 1)

        | _ -> 
            Console.SetCursorPosition(colMargin, 0)
            Console.Write(String(' ', lineLength))
            pattern

    let (|AnInt|_|) str =
       match System.Int32.TryParse(str) with
       | true, int -> Some int
       | _ -> None
   
    let choosePattern () =

        let isInRange i = i > 0 && i <= all.Length

        Console.Clear()

        printf "Available Patterns:\n"

        all |> Seq.iteri (fun i (name, _) -> printf "%d: %s\n" (i + 1) name)

        let answer = Console.ReadKey().KeyChar.ToString()

        Console.Clear()

        match answer with
        | AnInt i when isInRange i -> Some all.[i - 1]
        | _ -> None


    let askOption () =
        printf "\n\n"
        printf "Options:\n"
        printf " n : next evolve\n"
        printf " t : evolve 10 times\n"
        printf " w : evolve 20 times\n"
        printf " p : choose a pattern\n"
        printf " any other key to quit\n"
    
        Console.ReadKey().KeyChar

    let changePatternAnd showFn = 
        match choosePattern() with
        | Some(pattern) -> pattern |> showFn
        | _ -> ignore()


    let rec showIt pattern =
        printPattern pattern
        match askOption() with
        | 'n' -> pattern |> evolve |> showIt
        | 't' -> pattern |> evolve |> repeat 10 |> showIt
        | 'w' -> pattern |> evolve |> repeat 20 |> showIt
        | 'p' -> changePatternAnd showIt
        | x   -> 
            Console.SetCursorPosition(0, Console.CursorTop)
            printf " \nYou pressed '%O' - See you next time!\n\n" x


    changePatternAnd showIt
    0 // return an integer exit code
