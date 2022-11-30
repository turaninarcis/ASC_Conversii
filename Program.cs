using System.Numerics;

namespace Conversii
{
    internal class Program
    {
        static int b1, b2;
        static string nr;
        static bool isNegative = false;
        static char[] delimitatoare = { ',', '.' };
        static char[] numberCharacters = { '0','0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        static void Main(string[] args)
        {
            //IntroducereDate();
            //IntroducereDate();
            //PrelucrareDate(nr, b1, b2);
            Console.WriteLine(TranformFractionDigitsFromBaseXToBase10("201", 3));
        }
        private static void IntroducereDate()
        {
            string cerinta = "Introduceti numarul urmat de baza acestuia si baza in care doriti sa transformati numarul";
            Console.WriteLine(cerinta);
            string[] numere;
            bool parseSucceeded = false;
            char[] separatoare = {';', ',',' '};

            while (parseSucceeded == false)
            {
                string SirulIntrodus = Console.ReadLine();
                if (SirulIntrodus != "")
                {
                    numere = SirulIntrodus.Split(separatoare, StringSplitOptions.RemoveEmptyEntries);
                    if (numere.Length == 3 && int.TryParse(numere[1], out b1) && int.TryParse(numere[2],out b2))
                    {
                        nr = numere[0];
                        parseSucceeded = true;
                    }
                    else
                    {
                        Console.WriteLine(cerinta); continue;
                    }
                        

                }
                else Console.WriteLine(cerinta);

            }
            if (nr[0]=='-')
            {
                isNegative = true;
                nr=nr.Substring(1);
            }
        }


        

        private static void PrelucrareDate(string number, int b1,int b2)
        {
            int NumberInBase10;
            if (!(number.Contains(delimitatoare[0]) || number.Contains(delimitatoare[1])))
            {
                NumberInBase10 = FromBaseXToBase10(number, b1);
                Console.WriteLine($"Numarul {number} in baza {b2} este {FromBase10ToBaseX(NumberInBase10,b2)}");
            }
            else 
            {
                string[] numberParts = number.Split(delimitatoare);
                string integerDigits = numberParts[0];
                string fractionalDigits= numberParts[1];
                Console.WriteLine(integerDigits+ " "+ fractionalDigits);
            }


        }
        
 
       


        




        #region UtilityFunctions
        private static double TranformFractionDigitsFromBaseXToBase10(string number,int b1)
        {
            double FractionalDigitsInBase10 = 0;
            int suma = 0;
            int caractere = number.Length;
            int numberForFractioning;

            for(int i = 1;i<= caractere;i++)
            {
                suma += int.Parse(number[i-1].ToString()) * (int)Math.Pow(b1, caractere - i);
            }

            FractionalDigitsInBase10 = suma/Math.Pow(b1, caractere);
            return FractionalDigitsInBase10;
        }
        private static int TransformCharacterToNumber(char character)
        {
            switch (character)
            {
                case '0': return 0;
                case '1': return 1;
                case '2': return 2;
                case '3': return 3;
                case '4': return 4;
                case '5': return 5;
                case '6': return 6;
                case '7': return 7;
                case '8': return 8;
                case '9': return 9;
                case 'A': return 10;
                case 'B': return 11;
                case 'C': return 12;
                case 'D': return 13;
                case 'E': return 14;
                case 'F': return 15;
                default: throw new ArgumentException();
            }
        }
        private static char TranformNumberToCharacter(int number)
        {

            switch (number)
            {
                case 0: return '0';
                case 1: return '1';
                case 2: return '2';
                case 3: return '3';
                case 4: return '4';
                case 5: return '5';
                case 6: return '6';
                case 7: return '7';
                case 8: return '8';
                case 9: return '9';
                case 10: return 'A';
                case 11: return 'B';
                case 12: return 'C';
                case 13: return 'D';
                case 14: return 'E';
                case 15: return 'F';
                default: throw new ArgumentException();
            }
        }
        private static bool IsTheNumberInTheCorrectBase(string number, int b1)
        {
            switch (b1)
            {
                case 0: return false;
                case 1: return false;
                case 2: return CharacterControll(number, 2);
                case 3: return CharacterControll(number, 3);
                case 4: return CharacterControll(number, 4);
                case 5: return CharacterControll(number, 5);
                case 6: return CharacterControll(number, 6);
                case 7: return CharacterControll(number, 7);
                case 8: return CharacterControll(number, 8);
                case 9: return CharacterControll(number, 9);
                case 10: return CharacterControll(number, 10);
                case 11: return CharacterControll(number, 11);
                case 12: return CharacterControll(number, 12);
                case 13: return CharacterControll(number, 13);
                case 14: return CharacterControll(number, 14);
                case 15: return CharacterControll(number, 15);
                case 16: return CharacterControll(number, 16);
                default: return false;
            }
        }

        private static int FromBaseXToBase10(string number, int b1)
        {
            number = number.ToUpper();
            if (!IsTheNumberInTheCorrectBase(number, b1))
                return -1;

            int cifre = number.Length - 1;
            int NrInBase10 = 0;
            int numberToBeAdded;
            foreach (char c in number)
            {
                numberToBeAdded = 1;
                for (int i = cifre; i > 0; i--)
                {
                    numberToBeAdded = numberToBeAdded * b1;
                }
                cifre--;
                NrInBase10 += numberToBeAdded * TransformCharacterToNumber(c);
            }
            return NrInBase10;
        }

        private static string FromBase10ToBaseX(int number, int b2)
        {
            string convertedNumber = "";
            if (isNegative == true) convertedNumber += '-';
            Stack<int> resturi = new Stack<int>();
            while(number!=0)
            {
                resturi.Push(number % b2);
                number = number / b2;
            }
            while(resturi.Count>0)
            {
                convertedNumber += TranformNumberToCharacter(resturi.Pop());
            }    
            return convertedNumber;
        }

        private static bool CharacterControll(string number, int startingNumber)
        {
            for (int i = startingNumber + 1; i <= 16; i++)
            {
                if (number.Contains(numberCharacters[i])) { Console.WriteLine("Baza este incorecta"); return false; }
            }
            return true;
        }

        #endregion
    }
}