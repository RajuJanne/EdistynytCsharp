using System;
using System.Collections.Generic;
using static System.Console;

// Janne Rajuvaara
// NTK17SP 2018

namespace CS2T2
{
    class ConsoleControl
    {
        public List<string> Items { get; set; }
        public int Column{get; set;}
        public int Row { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public ConsoleColor BackColor { get; set; }
        public ConsoleColor TextColor { get; set; }

        public ConsoleControl(int col, int row, int width, int height)
        {
            Column = col;
            Row = row;
            Width = width;
            Height = height;
            BackColor = BackgroundColor;
            TextColor = ForegroundColor;
            Items = null; 
        }

        public void Clear()
        {
            int org_column = CursorLeft,
                org_row = CursorTop;

            for (int i = 0; i < Height; i++)
            {
                SetCursorPosition(Column - 1, Row - 1 + i);
                for (int j = 0; j < Width; j++)
                {
                    Write(" ");
                }
            }
            SetCursorPosition(org_column, org_row);
        }
        
        public void Draw()
        {
            int org_column = CursorLeft,
                org_row = CursorTop;

            ConsoleColor org_fore = ForegroundColor,
                         org_back = BackgroundColor;
            ForegroundColor = TextColor;
            BackgroundColor = BackColor;

            for (int i = 0; i < Height; i++)
            {
                SetCursorPosition(Column - 1, Row - 1 + i);
                if (Items != null && i < Items.Count)
                {
                    Write(Items[i].PadRight(Width, ' '));
                }
                else
                {
                    for (int j = 0; j < Width; j++)
                    {
                        Write(" ");
                    }
                }
            }
            SetCursorPosition(org_column, org_row);
            ForegroundColor = org_fore;
            BackgroundColor = org_back;
        }
    }
}
