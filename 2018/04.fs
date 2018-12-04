module _2018._04

open Common.Parsing
open System

type EventType = BeginsShift of int
               | WakesUp
               | FallsAsleep

type Event = DateTime * EventType

type InputType = Map<int, int * Event list>

let year, day = 2018, 04

let parse_event_type (event_type_str: string) =
    match event_type_str with
    | "falls asleep" -> FallsAsleep
    | "wakes up" -> WakesUp
    | Regex @"Guard #(\d+) begins shift" [number] -> BeginsShift (int number)
    | _ -> failwith <| sprintf "Couldn't parse event_type '%s'" event_type_str

let rec totalSleep (events : Event list) =
    match events with
    | (start, FallsAsleep)::(finish, WakesUp)::rest -> (finish - start).Minutes + totalSleep rest
    | [] -> 0
    | _ -> failwith "unexpected"

let addToGuardEvents map (_, events: Event seq) =
    let sorted = Seq.sort events
    match Seq.head sorted with
    | (_, BeginsShift guardNumber) ->
        let (sleep, evs) = match Map.tryFind guardNumber map with Some (sleep, evs) -> (sleep, evs) | None -> (0, [])
        Map.add guardNumber (totalSleep (List.tail <| List.ofSeq sorted) + sleep, ((Seq.toList <| Seq.tail sorted) @ evs)) map
    | _ -> failwith "Expected begins shift event"

let parse (input: string) : InputType =
    input.Lines()
    |> Seq.map (fun line ->
                match line with
                | Regex @"\[([^\]]+)\] (.*)" [datetime; event] -> (DateTime.Parse datetime, parse_event_type event)
                | _ -> failwith <| sprintf "Couldn't parse line '%s'" line)
    |> Seq.groupBy (fun (datetime, event_type) -> datetime.AddHours(2.0).Date)
    |> Seq.fold addToGuardEvents Map.empty

let findMaxMinute (events : Event list) =
    List.sortBy (fun ((dt, et) : Event) -> (dt.Minute, et)) events
    |> List.map (fun (dt, ev) -> (dt.Minute, match ev with FallsAsleep -> +1 | WakesUp -> -1))
    |> List.scan (fun (_, prev) (m, delta) -> (m, prev + delta)) (0, 0)
    |> List.maxBy snd

let findMax (max_guard, max_minute, max_occurence) guard (_, events) =
    let (current_minute, current_max) = findMaxMinute events

    if max_occurence < current_max then
        (guard, current_minute, current_max)
    else
        (max_guard, max_minute, max_occurence)

let solve_1 (input: InputType) =
    let (sleeperGuard, (_, evs)) = Map.toSeq input |> Seq.maxBy (snd >> fst)
    let (maxMinute, _) = findMaxMinute evs
    sleeperGuard * maxMinute

let solve_2 (input: InputType) =
    let (max_guard, max_minute, max_occurence) = Map.fold findMax (-1, -1, -1) input
    max_guard * max_minute