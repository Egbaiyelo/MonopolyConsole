using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.Utils
{
    public class ConsolePrinter
    {
        private int _top;

        internal void ClearQuery()
        {
            while (Console.CursorTop != _top)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.BufferWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
        }
    }

    internal class ConsoleQuery<T> : ConsolePrinter
    {
        private readonly string _prompt;
        private readonly string _onSuccess;
        private readonly string _onError;

        private T _value;
        private readonly Func<T, bool> _;
        private readonly List<(string, T)> _options;

        private readonly bool _listFormat;

        private int _top;

        public ConsoleQuery(
            string prompt, 
            string successMessage,
            string errorMessage,
            List<(string, T)> options,
            bool listFormat = false)
        {
            _prompt = prompt;
            _onSuccess = successMessage;
            _onError = errorMessage;
            _options = options;

            _listFormat = listFormat;
            _top = Console.CursorTop;

            // Needs to be run immediately after

        }

        //- returns number now, it takes options string and returns numbers, dont remember second item rightnot
        public T RunQuery()
        {
            while (true)
            {
                Console.WriteLine(_prompt);
                foreach (var option in _options)
                {
                    //- or make delegate if it matters
                    if (_listFormat)
                        Console.WriteLine($"- {option.Item1}");
                    else Console.Write($"{option.Item1}, ");
                }
                if (!_listFormat) Console.WriteLine("\b\b  ");
                
                string input = Console.ReadLine();

                //- Still needs finetuning
                var match = _options.FirstOrDefault(o =>
                    o.Item1.Equals(input, StringComparison.OrdinalIgnoreCase) || // full match
                    o.Item1.StartsWith(input, StringComparison.OrdinalIgnoreCase) || // prefix
                    input.Split(' ').Any(word => o.Item1.Contains(word, StringComparison.OrdinalIgnoreCase)) // fuzzy word match
                );

                if (!EqualityComparer<(string, T)>.Default.Equals(match, default))
                {
                    _value = match.Item2;
                    ClearQuery();
                    Console.WriteLine(_onSuccess);
                    return _value;
                }

                ClearQuery();
                Console.WriteLine(_onError);
            }
        }
    }

    internal class ConsoleDisplay : ConsolePrinter
    {
        private readonly string[] _options;
        private readonly bool _listFormat;

        public ConsoleDisplay(string[] options, bool listFormat = true)
        {
            _options = options;
            _listFormat = listFormat;
        }

        public void RunDisplay()
        {
            foreach (var option in _options)
            {
                //- or make delegate if it matters
                if (_listFormat)
                    Console.WriteLine(option);
                else Console.Write(option + ", ");
            }
            if (!_listFormat) Console.WriteLine();
            Console.ReadLine();
            ClearQuery(); 
        }
    }



}
