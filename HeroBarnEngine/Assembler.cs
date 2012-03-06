using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace HeroBarn
{
    class Assembler
    {
        #region Assemblers

        /// <summary>
        /// Assembles an XElement with the given XName, attribute names, and attribute values.
        /// </summary>
        /// <param name="xElementName">Name of XElement</param>
        /// <param name="attributeNames">List of attribute names</param>
        /// <param name="attributeValues">List of corresponding attribute values</param>
        /// <returns></returns>
        public XElement AssembleXElementWithAttributes(string xElementName, string[] attributeNames, string[] attributeValues)
        {
            XElement assembledXElement = new XElement(xElementName);
            if (attributeNames.Length != attributeValues.Length) { throw new Exception("The number of attribute names and values does not match."); };
            for (int i = 0; i < attributeNames.Length; i++)
            {
                assembledXElement.SetAttributeValue(attributeNames[i], attributeValues[i]);
            }
            return assembledXElement;
        }

        /// <summary>
        /// Assembles child XElements with a parent XElement
        /// </summary>
        /// <param name="parentXElement">Parent XElement</param>
        /// <param name="childXElements">Children XElements</param>
        /// <returns></returns>
        public XElement AssembleXElementAndChildren(XElement parentXElement, params XElement[] childXElements)
        {
            parentXElement.Add(childXElements);
            return parentXElement;
        }

        #endregion
    }
}
