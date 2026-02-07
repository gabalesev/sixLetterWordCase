using SixLetterWord;

// todo: read word length from command arguments 
const int targetWordLength = 6;
const string fileName = "input.txt";

string? rootDir = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName;
string inputFilePath = Path.Combine(
  rootDir ??
  throw new DirectoryNotFoundException("Solution root directory not found."),
  fileName);

// todo: call asynchronously if the file is large
var words = File.ReadLines(inputFilePath) // space complexity O(1)
    .Where(line => !string.IsNullOrWhiteSpace(line))
    .Select(line => line.Trim());

var finder = new WordCombinationFinder(words, targetWordLength);
var combinations = finder.FindCombinations();

// results
foreach (var combination in combinations)
{
    var parts = string.Join("+", combination);
    var result = string.Concat(combination);
    Console.WriteLine($"{parts}={result}");
}