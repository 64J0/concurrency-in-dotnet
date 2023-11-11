module IPv4Validation

open System.Threading
open System.Threading.Tasks

open FsToolkit.ErrorHandling

[<Literal>]
let private MAX_OCTET_VALUE = 0b11_11_11_11

[<Literal>]
let private MIN_OCTET_VALUE = 0b00_00_00_00

let private checkOctetQuantity (debug: bool) (ipv4: string) : Result<unit, string> =
    if debug then
        // https://stackoverflow.com/a/45694070
        let msg = $"Task={Task.CurrentId}, Thread={Thread.CurrentThread.ManagedThreadId}"
        printfn "%s" msg

    let octetQuantity = ipv4.Split [| '.' |] |> Array.length

    match octetQuantity with
    | 4 -> Ok()
    | x -> Error $"Invalid octet quantity: {x}. IPv4: {ipv4}."

let private checkOctetsAreCorrectlyBounded (debug: bool) (ipv4: string) : Result<unit, string> =
    if debug then
        // https://stackoverflow.com/a/45694070
        let msg = $"Task={Task.CurrentId}, Thread={Thread.CurrentThread.ManagedThreadId}"
        printfn "%s" msg

    let outOfBoundaryOctet =
        ipv4.Split [| '.' |]
        |> Array.map int // could throw -> TODO improve
        |> Array.tryFind (fun x -> x > MAX_OCTET_VALUE || x < MIN_OCTET_VALUE)

    match outOfBoundaryOctet with
    | Some octet -> Error $"This octet is out of boundary: {octet}. IPv4: {ipv4}."
    | None -> Ok()

let private checkThereIsNoLeadingZero (debug: bool) (ipv4: string) : Result<unit, string> =
    if debug then
        // https://stackoverflow.com/a/45694070
        let msg = $"Task={Task.CurrentId}, Thread={Thread.CurrentThread.ManagedThreadId}"
        printfn "%s" msg

    let octetWithLeadingZero =
        ipv4.Split [| '.' |]
        |> Array.filter (fun x -> x.Length > 1) // no problem if it's only 0
        |> Array.tryFind (fun x -> x.StartsWith '0')

    match octetWithLeadingZero with
    | Some octet -> Error $"This octet has a leading zero: {octet}. IPv4: {ipv4}."
    | None -> Ok()

let sequentialIPv4Validation (debug: bool) (ipv4: string) : Result<unit, string> =
    result {
        do! checkOctetQuantity debug ipv4
        do! checkOctetsAreCorrectlyBounded debug ipv4
        do! checkThereIsNoLeadingZero debug ipv4
    }

let parallelIPv4Validation (debug: bool) (ipv4: string) : Result<unit, string> =
    result {
        let lambda1 = Task.Run(fun () -> checkOctetQuantity debug ipv4)
        let lambda2 = Task.Run(fun () -> checkOctetsAreCorrectlyBounded debug ipv4)
        let lambda3 = Task.Run(fun () -> checkThereIsNoLeadingZero debug ipv4)

        Task.WaitAll(lambda1, lambda2, lambda3)

        do! lambda1.Result
        do! lambda2.Result
        do! lambda3.Result
    }
