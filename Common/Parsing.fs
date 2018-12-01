module Common.Parsing
open System

type String with
    member x.Lines() : String seq = Array.toSeq <| x.Split([|'\n'|], StringSplitOptions.RemoveEmptyEntries)

