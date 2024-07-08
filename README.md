# WordFinder

## Table of Contents
- [Introduction](#introduction)
- [Features](#features)
- [Usage](#usage)
- [Performance Considerations](#performance-considerations)
- [Testing](#testing)

## Introduction
`WordFinder` is a C# class designed to search for words in a given character matrix. It efficiently counts the occurrences of words from a provided word stream within the matrix and returns the top 10 most frequently found words.

## Features
- Efficiently handles up to 64x64 character matrices.
- Searches words horizontally (left to right) and vertically (top to bottom).
- Returns the top 10 most frequently found words in the matrix.
- Ignores duplicate words in the word stream for counting purposes.

## Usage
1. **Initialize the `WordFinder` class** with a character matrix.
2. **Call the `Find` method** with a word stream to get the top 10 most frequently found words.

## Performance Considerations
- **Preprocessing**: The matrix is preprocessed to store character positions, enhancing the search efficiency.
- **Optimized Search**: Searches are conducted starting from the stored positions of the first character of each word, reducing unnecessary comparisons.

## Testing
To ensure the correctness and performance of the `WordFinder` class, there are some unit tests covering various scenarios, such as:
- Words found horizontally
- Words found vertically
- Words not found in the matrix
- Duplicate words in the word stream
- Find words in single character matrix
- Null or empty cases

Also, there are benchmark tests using 'BenchmarkDotNet' library. These are the results:

```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3737/23H2/2023Update/SunValley3)
AMD Ryzen 7 5825U with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 7.0.404
  [Host]     : .NET 7.0.14 (7.0.1423.51910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.14 (7.0.1423.51910), X64 RyuJIT AVX2

```
| Method                                      | Mean     | Error     | StdDev    |
|-------------------------------------------- |---------:|----------:|----------:|
| FindWordsBenchmark                          | 3.909 μs | 0.0771 μs | 0.2248 μs |
| FindWordsWithSingleCharacterMatrixBenchmark | 3.644 μs | 0.0728 μs | 0.1891 μs |
