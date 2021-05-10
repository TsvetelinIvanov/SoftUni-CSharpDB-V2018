using System;

namespace TeamBuilder.App.Utilities
{
    public static class Check
    {
        public static void CheckLength(int expectedLenght, string[] array)
        {
            if (expectedLenght != array.Length)
            {
                throw new FormatException(Constants.ErrorMessages.InvalidArgumentsCount);
            }
        }
    }
}