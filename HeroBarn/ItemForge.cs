using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace HeroBarn
{
    class ItemForge
    {
        //I added some code

        public static ObservableCollection<XElement> currentCustomWeaponDamageDice = new ObservableCollection<XElement> { };
        public static ObservableCollection<XElement> currentCustomWeaponPowers = new ObservableCollection<XElement> { };
        public static ObservableCollection<XElement> currentCustomArmorPowers = new ObservableCollection<XElement> { };
        public static ObservableCollection<XElement> currentCustomItemPowers = new ObservableCollection<XElement> { };
        public static ObservableCollection<XElement> currentCustomItemProperties = new ObservableCollection<XElement> { };
        public static ObservableXElement currentCustomItemBasicAttributes = new ObservableXElement("Item");

        #region Validated Assemblers

        /// <summary>
        /// Creates an XElement validated from the data stored in ItemValidation.xml
        /// </summary>
        /// <param name="xElementName">XName of element</param>
        /// <param name="attributeNames">Attribute names</param>
        /// <param name="attributeValues">Attribute values</param>
        /// <returns></returns>
        public XElement CreateValidatedXElement(string xElementName, string[] attributeNames, string[] attributeValues)
        {
            Validator v = new Validator();
            v.ValidateXElementXName(xElementName);
            v.ValidateXElementXAttributeXNames(xElementName, attributeNames);
            v.CheckForRequiredXAttributes(xElementName, attributeNames);
            v.ValidateXAttributeValueTypes(xElementName, attributeValues, attributeNames);

            Assembler a = new Assembler();
            return a.AssembleXElementWithAttributes(xElementName, attributeNames, attributeValues);
        }

        /// <summary>
        /// Adds XElements as children to given XElement after checking that they are valid children.
        /// </summary>
        /// <param name="parentXElement">Parent XElement</param>
        /// <param name="childXElements">Child EXelements</param>
        public XElement AddValidatedChildrenToXElement(XElement parentXElement, params XElement[] childXElements)
        {
            var childXElementNames = from element in childXElements select element.Name.ToString();
            Validator v = new Validator();
            v.ValidateXElementChildrenOfXElement(parentXElement.Name.ToString(), childXElementNames.ToArray());
            Assembler a = new Assembler();
            return a.AssembleXElementAndChildren(parentXElement, childXElements);
        }

        #endregion
    }
}
