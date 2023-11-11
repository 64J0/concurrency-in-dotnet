```

BenchmarkDotNet v0.13.10, Ubuntu 22.04.3 LTS (Jammy Jellyfish)
Intel Core i7-7500U CPU 2.70GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET SDK 7.0.401
  [Host]     : .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2


```
| Method               | Mean      | Error     | StdDev    | Ratio | RatioSD |
|--------------------- |----------:|----------:|----------:|------:|--------:|
| SequentialValidation |  6.036 μs | 0.0405 μs | 0.0379 μs |  1.00 |    0.00 |
| ParallelValidation   | 24.427 μs | 0.4782 μs | 0.6545 μs |  4.04 |    0.14 |
