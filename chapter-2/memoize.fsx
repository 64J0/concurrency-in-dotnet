open System.Collections.Generic
open System.Threading

let memoize func =
    let table = Dictionary<_, _>()

    fun x ->
        if table.ContainsKey(x) then
            table.[x]
        else
            let result = func x
            table.[x] <- result
            result

let funcImp =
    fun number ->
        Thread.Sleep 2000
        number * 2

let memoizedFunc = memoize funcImp

#time "on"
memoizedFunc 10
// Real: 00:00:02.004, CPU: 00:00:00.090, GC gen0: 0, gen1: 0, gen2: 0
// 20

memoizedFunc 10
// Real: 00:00:00.004, CPU: 00:00:00.010, GC gen0: 0, gen1: 0, gen2: 0
// 20

memoizedFunc 20
// Real: 00:00:02.002, CPU: 00:00:00.090, GC gen0: 0, gen1: 0, gen2: 0
// 40
