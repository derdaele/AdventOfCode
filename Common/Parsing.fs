module Common.Parsing

open System
open System.Text.RegularExpressions

type String with
    member x.Lines() : String array = x.Split([|'\n'|], StringSplitOptions.RemoveEmptyEntries)

let (|Regex|_|) pattern input =
    let m = Regex.Match(input, pattern)
    if m.Success then Some(List.tail [ for g in m.Groups -> g.Value ])
    else None