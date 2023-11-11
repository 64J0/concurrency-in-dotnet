```

BenchmarkDotNet v0.13.10, Ubuntu 22.04.3 LTS (Jammy Jellyfish)
Intel Core i7-7500U CPU 2.70GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET SDK 7.0.401
  [Host]     : .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 7.0.11 (7.0.1123.42427), X64 RyuJIT AVX2


```
| Method               | Mean      | Error     | StdDev    | Ratio | RatioSD |
|--------------------- |----------:|----------:|----------:|------:|--------:|
| SequentialValidation |  2.907 μs | 0.0359 μs | 0.0318 μs |  1.00 |    0.00 |
| ParallelValidation   | 11.859 μs | 0.2355 μs | 0.3596 μs |  4.12 |    0.17 |
