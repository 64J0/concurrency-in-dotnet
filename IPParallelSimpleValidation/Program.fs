// IPv4 validations to compare running sequentially in a single thread
// and running in parallel in multiple threads.

open BenchmarkDotNet.Running

open IPv4Validation
open IPv4Benchmark

let _runValidation () =
    let debug = true

    let inputList =
        [ // valid IPv4
          "192.168.0.1"
          "10.0.0.0"
          "0.0.0.0"
          "255.255.255.255"
          "172.16.0.1"
          "8.8.8.8"
          // invalid IPv4
          "00.0.0.0"
          "1000.6000.256.0"
          "1.1.1.1.1" ]

    let printResult (x: Result<unit, string>) =
        match x with
        | Ok() -> printfn "Valid IP"
        | Error err -> eprintfn $"{err}"

    printfn "Sequential validation:\n"

    inputList |> List.map (sequentialIPv4Validation debug) |> List.iter printResult

    printfn "// =====================================\n"
    printfn "Parallel validation:\n"

    inputList |> List.map (parallelIPv4Validation debug) |> List.iter printResult

[<EntryPoint>]
let main (_args: string array) : int =
    // _runValidation ()
    BenchmarkRunner.Run<SingleIPv4Validation>() |> ignore
    BenchmarkRunner.Run<MultipleIPv4Validation>() |> ignore

    0
