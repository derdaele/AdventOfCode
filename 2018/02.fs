module _2018._02

open System
open Common
open Common.Parsing
open Common.Collections
open Common.Function

type InputType = string array

let year, day = 2018, 02

let parse (input: string) : InputType = input.Lines()

// Part 1
let letter_freq = Seq.fold (flip Multiset.add) Multiset.empty

let count_freq value = Seq.filter (fun f -> Multiset.hasCount value f) >> Seq.length

let solve_1 (input: InputType) =
    let freqs = Seq.map letter_freq input
    (count_freq 2 freqs) * (count_freq 3 freqs)

// Part 2
let dist (a: string) (b: string) = Seq.zip a b |> Seq.countIf (fun (la, lb) -> la <> lb)

let reconstruct_word ((a, b) : string * string) =
    Seq.zip a b |> Seq.filter (fun (la, lb) -> la = lb) |> Seq.map fst |> String.Concat

let solve_2 (input: InputType) =
    Seq.choose (fun a -> match Seq.tryFind (fun b -> a <> b && (dist a b) = 1) input with
                         | None -> None
                         | Some word -> Some (a, word)
                ) input
    |> Seq.head
    |> reconstruct_word