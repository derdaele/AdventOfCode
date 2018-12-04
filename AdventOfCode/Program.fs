open System
open System.IO

open _2018._04

[<EntryPoint>]
let main argv =
    let input = File.ReadAllText <| sprintf "%02d/%02d.txt" year day
    let parsed = parse input
    printfn "Part 1 = %A" <| solve_1 parsed
    printfn "Part 2 = %A" <| solve_2 parsed

    0
