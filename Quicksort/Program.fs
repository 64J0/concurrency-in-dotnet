open System

open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running

let maxRange = 2_500_000
let random = new Random()
let getRandomNumber () = random.Next(-maxRange, maxRange)
let maxDepth = Math.Log(float Environment.ProcessorCount, 2.)

[<MemoryDiagnoser>]
type Quicksort() =

    [<Params (1000, 10000, 100000)>] 
    member val ListSize :int = 0 with get, set

    member self.unsortedList = [ for i in 1..self.ListSize do getRandomNumber () ]

    [<Benchmark(Baseline = true)>]
    member self.SequentialQuicksort() =
        QuicksortSequential.quicksortSequential self.unsortedList

    [<Benchmark>]
    member self.OverParallelQuicksort() =
        QuicksortParallel.quicksortParallel self.unsortedList

    [<Benchmark>]
    member self.ParallelQuicksort() =
        QuicksortParallel.quicksortParallelWithDepth (int maxDepth) self.unsortedList

[<EntryPoint>]
let main (_args: string[]) : int =
    BenchmarkRunner.Run<Quicksort>() |> ignore
    0
