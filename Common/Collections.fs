module Common.Collections

open Common.Function

type Multiset<'T when 'T : comparison> = Map<'T, int>

module Multiset =
    let empty : Multiset<'T> = Map.empty

    let add value (mset: Multiset<'T>) : Multiset<'T> =
        match Map.tryFind value mset with
        | None -> Map.add value 1 mset
        | Some count -> Map.add value (count + 1) mset

    let hasCount count (mset: Multiset<'T>) : bool =
        try
            let _letter = Map.findKey (fun _ value -> count = value) mset in true
        with
            :? System.Collections.Generic.KeyNotFoundException as e -> false

    let ofSeq x = Seq.fold (flip add) empty x
         