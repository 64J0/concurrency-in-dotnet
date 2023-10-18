open System

open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running

let random = new Random()
let getRandomNumber () = random.Next(-2_500_000, 2_500_000)

type Quicksort() =
    let _maxDepth = Math.Log(float Environment.ProcessorCount, 2.) + 4.

    let _unsortedInput =
        [ for i in 1..2_500_000 do
              getRandomNumber () ]

    [<Benchmark(Baseline = true)>]
    member _.ConcurrentQuicksort() =
        QuicksortSequential.quicksortSequential _unsortedInput

    [<Benchmark>]
    member _.OverParallelQuicksort() =
        QuicksortParallel.quicksortParallel _unsortedInput

    [<Benchmark>]
    member _.ParallelQuicksort() =
        QuicksortParallel.quicksortParallelWithDepth (int _maxDepth) _unsortedInput

[<EntryPoint>]
let main (_args: string[]) : int =
    BenchmarkRunner.Run<Quicksort>() |> ignore
    0
