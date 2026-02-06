using System.Collections.Generic;
using SixLetterWord;

const int targetWordLength = 6;

string? rootDir = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName;
string inputFilePath = Path.Combine(
  rootDir ??
  throw new DirectoryNotFoundException("Solution root directory not found."),
  "input.txt");

var words = await File.ReadAllLinesAsync(inputFilePath);

var finder = new WordCombinationFinder(words, targetWordLength);
var combinations = finder.FindCombinations();

// results
foreach (var combination in combinations)
{
    var parts = string.Join("+", combination);
    var result = string.Concat(combination);
    Console.WriteLine($"{parts}={result}");
}