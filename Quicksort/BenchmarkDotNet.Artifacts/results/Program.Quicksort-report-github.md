```

BenchmarkDotNet v0.13.9+228a464e8be6c580ad9408e98f18813f6407fb5a, Ubuntu 22.04.3 LTS (Jammy Jellyfish)
Intel Core i7-7500U CPU 2.70GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET SDK 7.0.401
  [Host]     : .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2


```
| Method                | Mean    | Error   | StdDev  | Ratio | RatioSD |
|---------------------- |--------:|--------:|--------:|------:|--------:|
| ConcurrentQuicksort   | 13.23 s | 0.257 s | 0.377 s |  1.00 |    0.00 |
| OverParallelQuicksort | 16.43 s | 0.326 s | 0.780 s |  1.26 |    0.07 |
| ParallelQuicksort     | 13.09 s | 0.129 s | 0.115 s |  0.99 |    0.03 |
