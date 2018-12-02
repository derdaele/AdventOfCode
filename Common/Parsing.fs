module Common.Parsing
open System

type String with
    member x.Lines() : String array = x.Split([|'\n'|], StringSplitOptions.RemoveEmptyEntries)

