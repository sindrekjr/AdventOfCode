namespace AdventOfCode.Solutions.Year2021.Day01

module FSharp =
    let rec CountIncreases input acc =
        match input with
        | x :: xs ->
            match xs with
            | y :: _ -> 
                if x < y
                then (CountIncreases xs (acc + 1))
                else (CountIncreases xs acc)
            | [] -> acc
        | [] -> acc

    let SumGroupsOfThree measurements =
        measurements
        |> List.windowed 3
        |> List.map (List.reduce (+))
