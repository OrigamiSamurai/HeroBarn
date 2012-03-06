using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroBarn
{
    class HeroBarnMetaLanguage
    {
        static public object GetValueOfField(string fieldName)
        {
            return Hero.heroStats.Single(x => x.Name == fieldName).HeldObject;
        }

        static public object SUM(params object[] sourceObjects)
        {
            string type = sourceObjects[0].GetType().ToString();
            switch (type)
            {
                case "integer":
                    int integerSum = 0;
                    foreach (string sourceObject in sourceObjects)
                    {
                        integerSum += Convert.ToInt32(sourceObject);
                    };
                    return integerSum;
                case "decimal":
                    decimal decimalSum = 0;
                    foreach (string sourceObject in sourceObjects)
                    {
                        decimalSum += Convert.ToInt32(sourceObject);
                    };
                    return decimalSum;
                default:
                    throw new Exception("Cannot add unknown types");
            };
        }

        static public object MINUS(object subtractee, object subtractor)
        {
            string type = subtractee.GetType().ToString();
            switch (type)
            {
                case "integer":
                    return Convert.ToInt32(subtractee) - Convert.ToInt32(subtractor);
                case "decimal":
                    return Convert.ToDecimal(subtractee) - Convert.ToDecimal(subtractor);
                default:
                    throw new Exception("Cannot subtract unknown types");
            };
        }

        /* static public object ROUNDDOWN(string sourceField)
         {
             //DEFINE
         } */

    }
}
