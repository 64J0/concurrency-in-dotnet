open System
open System.Net
open System.Threading

let downloadSite (url: string) =
    let wc = new WebClient()
    let content = wc.DownloadString(url)
    printfn $"The size of the web site {url} is {content.Length}"

let threadA = new Thread(fun () -> downloadSite "https://www.google.com/")
let threadB = new Thread(fun () -> downloadSite "https://www.facebook.com/")

#time "on"
threadA.Start()
threadB.Start()
threadA.Join() // wait
threadB.Join() // wait

#time "off"

#time "on"

ThreadPool.QueueUserWorkItem(fun _ -> downloadSite "https://www.google.com/")
ThreadPool.QueueUserWorkItem(fun _ -> downloadSite "https://www.facebook.com/")
#time "off"

Console.ReadLine()

// dotnet fsi threadpool.fsx
//
// task-parallelism/threadpool.fsx(6,18): warning FS0044: This construct is deprecated. WebRequest, HttpWebRequest, ServicePoint, and WebClient are obsolete. Use HttpClient instead.

// The size of the web site https://www.facebook.com/ is 45692
// The size of the web site https://www.google.com/ is 56752
// Real: 00:00:00.964, CPU: 00:00:00.900, GC gen0: 0, gen1: 0, gen2: 0
// Real: 00:00:00.000, CPU: 00:00:00.000, GC gen0: 0, gen1: 0, gen2: 0
// The size of the web site https://www.facebook.com/ is 45692
// The size of the web site https://www.google.com/ is 56793
//
// Notice that it does not wait in the main thread for the ThreadPool
// queued functions to run.
