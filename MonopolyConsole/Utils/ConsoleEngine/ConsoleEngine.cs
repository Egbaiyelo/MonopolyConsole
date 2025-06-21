using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace MonopolyConsole.Utils.ConsoleEngine
{
    public struct Point { int  x, y };

    public string TopTile = 
        "______"
        "|    |"
        "|    |"
        "|    |"
        "";

    class ConsoleEngine
    {
        
    }

    internal class ConsoleCard {

        // Relative position
        public int X, Y;

        // Card dimensions
        private int Width;
        private int Height;

        public string Content;
		public bool ShowBorder

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

		public void AddChild(ConsoleCard child, int relativeX, int relativeY)
		{
			child.X = relativeX;
			child.Y = relativeY;
			Children.Add(child);
		}


		public virtual void Print(int offsetX = 0, int offsetY = 0)
		{
			int actualX = offsetX + X;
			int actualY = offsetY + Y;
			Console.ForegroundColor = ForeColor;

			if (ShowBorder)
			{
				// Draw the top border.
				Console.SetCursorPosition(actualX, actualY);
				Console.Write("+" + new string('-', Width - 2) + "+");

				// Print each content line (or blank line if none)
				var lines = GetContentLines();
				for (int i = 0; i < Height - 2; i++)
				{
					Console.SetCursorPosition(actualX, actualY + i + 1);
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
				Console.SetCursorPosition(actualX, actualY + Height - 1);
				Console.Write("+" + new string('-', Width - 2) + "+");
			}
			else
			{
				var lines = GetContentLines();
				for (int i = 0; i < lines.Length; i++)
				{
					Console.SetCursorPosition(actualX, actualY + i);
					Console.Write(lines[i].PadRight(Width));
				}
			}

			foreach (var child in Children)
				child.Print(actualX, actualY);

			Console.ResetColor();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="clearChar">The char that should fill screen</param>
        public void Clear(char clearChar = ' ')
        {

        }

    }
}
