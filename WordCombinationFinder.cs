namespace SixLetterWord;

public class WordCombinationFinder
{
    private readonly HashSet<string> _wordSet;
    private readonly HashSet<string> _targetWordSet;
    private readonly int _targetLength;

    public WordCombinationFinder(IEnumerable<string> words, int targetLength = 6)
    {
        _wordSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        _targetWordSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        _targetLength = targetLength;

        foreach (var word in words)
        {
            // skip words longer than target length
            if (string.IsNullOrWhiteSpace(word) || word.Length > targetLength)
                continue;

            // pre-load words once
            if (word.Length == targetLength)
            {
                _targetWordSet.Add(word);
            }
            else
            {
                _wordSet.Add(word);
            }
        }
    }

    public IEnumerable<List<string>> FindCombinations()
    {
        foreach (var target in _targetWordSet)
        {
            foreach (var combination in FindCombinationsForWord(target))
            {
                yield return combination;
            }
        }
    }

    private IEnumerable<List<string>> FindCombinationsForWord(string target)
    {
        var results = new List<List<string>>();
        RecursiveCombinationsSearch(target, 0, [], results);
        return results;
    }

    private void RecursiveCombinationsSearch(
        string target,
        int startIndex,
        List<string> currentParts,
        List<List<string>> results)
    {
        // base case: match target word
        if (startIndex == target.Length)
        {
            // no need to validate currentParts.Count > 1
            // because we did not add the target words to _wordSet
            results.Add(new List<string>(currentParts));

            return;
        }

        // save iterations by not starting from targetlength when startIndex is 0
        int maxPrefixLength = Math.Min(_targetLength - 1, target.Length - startIndex);
        for (int length = 1; length <= maxPrefixLength; length++)
        {
            // todo improvement: avoid string allocation
            string substring = target.Substring(startIndex, length);

            if (_wordSet.Contains(substring)) // O(1) lookup
            {
                // todo improvement: if a word has 2 or more times the same substring,
                // validate if there are enough occurrences in the _wordSet for that substring
                currentParts.Add(substring);
                RecursiveCombinationsSearch(target, startIndex + length, currentParts, results);
                currentParts.RemoveAt(currentParts.Count - 1);
            }
        }
    }
}