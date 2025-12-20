using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyConsole.Utils
{
    public class ConsoleCard
    {
        // Absolute position - for printing, each one should have their own absolute pos
        public int X = 0, Y = 0;

        // Card dimensions
        private int Width;
        private int Height;

        public string Content;
        public bool ShowBorder;
        public int CursorEnd = 61;


        public ConsoleColor ForeColor { get; set; } = ConsoleColor.White;
        public List<ConsoleCard> Children { get; } = new List<ConsoleCard>();

        public ConsoleCard(string baseContent, int width = 0, int height = 0, bool showBorder = false)
        {
            Content = baseContent ?? "";
            ShowBorder = showBorder;

            // Calculate content dimensions 
            var lines = GetContentLines();
            int contentWidth = 0;
            foreach (var line in lines)
                contentWidth = Math.Max(contentWidth, line.Length);

            Width = width > 0 ? width : (showBorder ? contentWidth + 2 : contentWidth);
            Height = height > 0 ? height : (showBorder ? lines.Length + 2 : lines.Length);
        }

        protected string[] GetContentLines()
        {
            return Content.Split(new[] { "\n" }, StringSplitOptions.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="child"></param>
        /// <param name="relativeX"></param>
        /// <param name="relativeY"></param>
        public void AddChild(ConsoleCard child, int relativeX, int relativeY)
        {
            child.X = X + relativeX;
            child.Y = Y + relativeY;
            Children.Add(child);
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Print()
        {
            Console.ForegroundColor = ForeColor;

            if (ShowBorder)
            {
                // Draw the top border.
                Console.SetCursorPosition(X, Y);
                Console.Write("+" + new string('-', Width - 2) + "+");

                // Print each content line (or blank line if none)
                var lines = GetContentLines();
                for (int i = 0; i < Height - 2; i++)
                {
                    Console.SetCursorPosition(X, Y + i + 1);
                    string line = i < lines.Length ? lines[i] : "";
                    // Center the text if shorter than available width.
                    if (line.Length < Width - 2)
                    {
                        int padLeft = (Width - 2 - line.Length) / 2;
                        line = new string(' ', padLeft) + line;
                    }
                    Console.Write("|" + line.PadRight(Width - 2) + "|");
                }

                // Draw the bottom border.
                Console.SetCursorPosition(X, Y + Height - 1);
                Console.Write("+" + new string('-', Width - 2) + "+");
            }
            else
            {
                var lines = GetContentLines();
                for (int i = 0; i < lines.Length; i++)
                {
                    Console.SetCursorPosition(X, Y + i);
                    Console.Write(lines[i]);
                }
            }

            foreach (var child in Children)
                child.Print();

            Console.ResetColor();
            Console.SetCursorPosition(0, CursorEnd);
        }

        /// <summary>
        /// Fill the screen with a certain character
        /// </summary>
        /// <param name="clearChar">The char that should fill screen</param>
        public void Clear(char clearChar = ' ')
        {
            for (int i = 0; i <  Height; i++)
            {
                Console.SetCursorPosition(X, Y + i);
                for (int j = 0; j < Width; j++)
                {
                    Console.Write(clearChar);
                } 
            }
            Console.SetCursorPosition(0, CursorEnd);
        }


    }

}
