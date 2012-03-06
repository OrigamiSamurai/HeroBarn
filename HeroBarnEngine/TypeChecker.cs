using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;

namespace HeroBarn
{
    public static class TypeChecker
    {
        /// <summary>
        /// Checks whether the input can be converted to the C# type
        /// </summary>
        /// <param name="input">Value</param>
        /// <param name="targetType">Target C# type</param>
        /// <returns></returns>
        public static bool Is(this string input, Type targetType)
        {
            try
            {
                TypeDescriptor.GetConverter(targetType).ConvertFromString(input);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
