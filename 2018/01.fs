module _2018._01
open Common
open Common.Parsing
open System.Collections.Generic

type InputType = int seq

let year, day = 2018, 01

let parse (input: string) : InputType = input.Lines() |> Seq.map int

let solve_1 (input: InputType) = Seq.sum input

let solve_2 (input: InputType) =
    let seen = new HashSet<int>() in
    Seq.infinitely input
    |> Seq.scan (fun prev cur -> prev + cur) 0
    |> Seq.find (Boolean.negate seen.Add)
