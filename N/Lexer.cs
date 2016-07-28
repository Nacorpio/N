using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N
{
    public class Lexer : AdvanceableList<char>
    {
        /// <summary>
        /// Initializes an instance of the <see cref="Lexer"/> class.
        /// </summary>
        /// <param name="input">The string input to tokenize.</param>
        public Lexer(string input) : base(input.ToCharArray())
        {
            Advanced += OnAdvanced;
        }

        /// <summary>
        /// Begins lexing the given input.
        /// </summary>
        public void Begin()
        {
            AdvanceUntilEnd();
        }

        private void OnAdvanced(object sender, AdvanceableList<char> advanceableList)
        {
            var current = advanceableList.GetCurrent();
            switch (current)
            {
                case ' ':
                {
                    AdvanceUntil(x => x != ' ');
                    break;
                }

                // TODO: Create a token, depending on the current character.
                default:
                {
                    if (char.IsLetter(current))
                    {
                        // TODO: Keep advancing while there's a letter currently present.
                        var value = AdvanceUntil(x => !(char.IsLetter(GetCurrent()) || char.IsDigit(GetCurrent())));
                    }

                    if (char.IsDigit(current))
                    {
                        // TODO: Keep advancing while there's a digit currently present.
                        var value = AdvanceUntil(x => !char.IsDigit(GetCurrent()));
                    }

                    break;
                }
            }
        }
    }
}
