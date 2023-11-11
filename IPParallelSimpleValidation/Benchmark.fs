module IPv4Benchmark

open BenchmarkDotNet.Attributes

open IPv4Validation

type SingleIPv4Validation() =
    let _debug: bool = false
    let _validIp: string = "192.0.0.1"
    let _invalidIp1: string = "00.0.0.0"
    let _invalidIp2: string = "1000.6000.256.0"
    let _invalidIp3: string = "1.1.1.1.1"

    [<Benchmark(Baseline = true)>]
    member _.SequentialValidation() : unit =
        let _ = _validIp |> (sequentialIPv4Validation _debug)
        let _ = _invalidIp1 |> (sequentialIPv4Validation _debug)
        let _ = _invalidIp2 |> (sequentialIPv4Validation _debug)
        let _ = _invalidIp3 |> (sequentialIPv4Validation _debug)

        ()

    [<Benchmark>]
    member _.ParallelValidation() : unit =
        let _ = _validIp |> (parallelIPv4Validation _debug)
        let _ = _invalidIp1 |> (parallelIPv4Validation _debug)
        let _ = _invalidIp2 |> (parallelIPv4Validation _debug)
        let _ = _invalidIp3 |> (parallelIPv4Validation _debug)

        ()

type MultipleIPv4Validation() =
    let _debug: bool = false

    let _inputList =
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

    [<Benchmark(Baseline = true)>]
    member _.SequentialValidation() : unit =
        let _ = _inputList |> List.map (sequentialIPv4Validation _debug)

        ()

    [<Benchmark>]
    member _.ParallelValidation() : unit =
        let _ = _inputList |> List.map (parallelIPv4Validation _debug)

        ()
