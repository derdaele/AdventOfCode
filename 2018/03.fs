module _2018._03

open Common.Parsing
open System.Collections.Generic

type Claim = {number:int; x:int; y:int; width:int; height:int} with
    static member X c = c.x
    static member Y c = c.y

type InputType = Claim seq

let year, day = 2018, 03

// #13 @ 514,234: 15x27
let parse (input: string) : InputType = input.Lines() |> Seq.map (fun line ->
    match line with
    | Regex @"#(\d+) @ (\d+),(\d+): (\d+)x(\d+)" [number; x; y; width; height] ->
        { number = int number; x = int x; y = int y; width = int width; height = int height }
    | _ -> failwith <| sprintf "Couldn't parse line '%s'" line)


let segment on op c1 c2 = if op (on c1) (on c2) then c1, c2 else c2, c1

let common_zone (c1 : Claim) (c2 : Claim) =
    let top, bottom = segment Claim.Y (<) c1 c2
    let right, left = segment Claim.X (>) c1 c2

    let x = right.x
    let y = bottom.y

    let vertical = max 0 ((min (top.y + top.height) (bottom.y + bottom.height)) - bottom.y)
    let horizontal = max 0 ((min (left.x + left.width) (right.x + right.width)) - right.x)

    if vertical > 0 && horizontal > 0 then
        Some (x, y, vertical, horizontal)
    else
        None

let count_common_area (seen: HashSet<int * int>) (c1 : Claim) (c2 : Claim) =
    match common_zone c1 c2 with
    | Some (x, y, width, height) ->
        let mutable count = 0;

        if width > 0  && height > 0 then
            for dy = 0 to width - 1 do
                for dx = 0 to height - 1 do
                    if seen.Add (x + dx, y + dy) then
                        count <- count + 1

        count
    | None -> 0

let rec sum_common_area seen (claims: Claim list) =
    match claims with
    | hd::tl -> (Seq.map (fun c -> count_common_area seen hd c) tl |> Seq.sum) + sum_common_area seen tl
    | [] -> 0

let solve_1 (input: InputType) =
    let seen = new HashSet<int * int>()
    sum_common_area seen <| List.ofSeq input

let solve_2 (input: InputType) =
    let claim =
        Seq.find (
            // Find the clain that
            fun c1 ->
                // There is no common zone
                not (Seq.exists
                        (fun c2 -> c1 <> c2 && Option.isSome (common_zone c1 c2))
                        input
                    )
            )
            input

    claim.number