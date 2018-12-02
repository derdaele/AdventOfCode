module Common.Seq

let public infinitely repeatedList =
    Seq.initInfinite (fun _ -> repeatedList)
    |> Seq.concat

let countIf pred = Seq.filter pred >> Seq.length