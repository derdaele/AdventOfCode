module _2018._03

open Common.Parsing

type Claim = {number:int; x:int; y:int; width:int; height:int}

type InputType = Claim seq

let year, day = 2018, 03

// #13 @ 514,234: 15x27
let parse (input: string) : InputType = input.Lines() |> Seq.map (fun line -> 
    match line with 
    | Regex @"#(\d+) @ (\d+),(\d+): (\d+)x(\d+)" [number; x; y; width; height] ->
        { number = int number; x = int x; y = int y; width = int width; height = int height }
    | _ -> failwith <| sprintf "Couldn't parse line '%s'" line)

let solve_1 (input: InputType) = "Not implemented"

let solve_2 (input: InputType) = "Not Implemented"