module _2018._05

open System
open Common.Parsing

type InputType = char list

let year, day = 2018, 05

let parse (input: string) : InputType = input.Lines() |> Seq.head |> List.ofSeq

let shouldDestroy (c1: char) (c2: char) = c1 <> c2 && (Char.ToLowerInvariant c1) = (Char.ToLowerInvariant c2)

let rec reduce (polymer: char list) (blacklist: char) acc =
    match polymer with
    | c::tl when (Char.ToLowerInvariant c) = blacklist -> reduce tl blacklist acc
    | c1::c2::tl when shouldDestroy c1 c2 -> reduce tl blacklist acc
    | hd::tl -> reduce tl blacklist (hd :: acc)
    | [] -> acc


let reaction (polymer: char list) (blacklist: char) =
    let mutable cur = polymer
    let mutable next = reduce cur blacklist []

    while cur.Length <> next.Length do
        cur <- next;
        next <- reduce cur blacklist []

    next

let solve_1 (input: InputType) = reaction input ' ' |> Seq.length

let solve_2 (input: InputType) = ['a'..'z'] |> List.map (fun blacklist -> reaction input blacklist |> Seq.length) |> List.min
