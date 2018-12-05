open System
open System.IO

open _2018._05

let duration f x =
    let timer = new System.Diagnostics.Stopwatch()
    timer.Start()
    let returnValue = f x
    printfn "Elapsed Time: %s" (timer.Elapsed.ToString())
    returnValue

[<EntryPoint>]
let main argv =
    let input = File.ReadAllText <| sprintf "%02d/%02d.txt" year day
    let parsed = parse input
    printfn "Part 1 = %A" <| duration solve_1 parsed
    printfn "Part 2 = %A" <| duration solve_2 parsed

    0
