module QuicksortParallel

open System.Threading.Tasks

// over-parallelized -> will run slower than the concurrent version
let rec quicksortParallel aList =
    match aList with
    | [] -> []
    | firstEl :: restOfList ->
        let smaller, larger = List.partition (fun n -> n < firstEl) restOfList

        let left = Task.Run(fun () -> quicksortParallel smaller)
        let right = Task.Run(fun () -> quicksortParallel larger)
        left.Result @ (firstEl :: right.Result)

let rec quicksortParallelWithDepth depth aList =
    match aList with
    | [] -> []
    | firstEl :: restOfList ->
        let smaller, larger = List.partition (fun number -> number < firstEl) restOfList

        if depth < 0 then
            let left = quicksortParallelWithDepth depth smaller
            let right = quicksortParallelWithDepth depth larger
            left @ (firstEl :: right)
        else
            let left = Task.Run(fun () -> quicksortParallelWithDepth (depth - 1) smaller)
            let right = Task.Run(fun () -> quicksortParallelWithDepth (depth - 1) larger)
            left.Result @ (firstEl :: right.Result)
