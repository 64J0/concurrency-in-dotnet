let example1 () =
    let somePhrase = "is blue"
    let lambdaFn = fun x -> $"{x} {somePhrase}"

    let result = lambdaFn "The ocean"
    printfn "result: %s" result // The ocean is blue

example1 ()

open System.Threading
open System.Threading.Tasks

// Doesn't work
let example2 () =
    for iteration in [ 1..10 ] do
        Task.Factory.StartNew(fun () -> printfn "%A - %i" Thread.CurrentThread.ManagedThreadId, iteration)

example2 ()

// In theory it would print the number 10 ten times. The explanation is that the iteration kept running
// in the foreground while the new tasks were scheduled to run in the background. I assume it takes more
// time to start and run the new tasks, so when they're started the iteration already reached the end.

let example3 () =
    let displayNumber = fun n -> printfn "%i" n
    let mutable i = 5
    let taskOne = Task.Factory.StartNew(fun () -> displayNumber i)
    i <- 7
    let taskTwo = Task.Factory.StartNew(fun () -> displayNumber i)

    Task.WaitAll(taskOne, taskTwo)
// 7
// 7

example3 ()

let example4 () =
    let tasks = Array.zeroCreate<Task> 10

    for index in [ 1..10 ] do
        tasks.[index - 1] <- Task.Factory.StartNew(fun () -> printfn "%i" index)

// One each line:
// 1, 2, 4, 5, 6, 7, 8, 9, 10, 3

example4 ()
