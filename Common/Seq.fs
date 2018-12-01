module Common.Seq

let public infinitely repeatedList =
    Seq.initInfinite (fun _ -> repeatedList)
    |> Seq.concat