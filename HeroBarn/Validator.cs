using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.ComponentModel;
using System.Text;

namespace HeroBarn
{
    public class Validator
    {
        XElement validationTree = XElement.Load("ItemValidation.xml");

        #region Validators

        /// <summary>
        /// Returns a string array of the valid values for the given global value list.
        /// </summary>
        /// <param name="typeOfValues">The type of value you are looking for.</param>
        /// <returns></returns>
        public static string[] GetValidGlobalValues(string typeOfValues)
        {
            try
            {
                var validator = new Validator();
                var validNames = from validName in validator.validationTree.Element("GlobalValues").Element(typeOfValues).Elements()
                                 select validName.Name.ToString();
                return validNames.ToArray();
            }
            catch
            {
                throw new Exception("The global value list you are looking for does not exist.");
            }
        }

        /// <summary>
        /// Retruns a string array of the valid d20 die sizes.
        /// </summary>
        /// <returns></returns>
        public static string[] GetValidDieSizes()
        {
            var validator = new Validator();
            var validDieSizes = from validDieSize in validator.validationTree.Element("GlobalValues").Element("DieSize").Elements()
                                select validDieSize.Value;
            return validDieSizes.ToArray();
        }

        /// <summary>
        /// Loads the valid XElement names into a list.
        /// </summary>
        /// <returns>XElement tree representing valid XElement XNames, XAttribute XNames, and XAttribute values.</returns>
        public List<string> GetValidXElementNames()
        {
            var validXElementNames = from validXElementName in validationTree.Element("XElements").Elements()
                                     select validXElementName.Name.ToString();
            return validXElementNames.ToList();
        }

        /// <summary>
        /// Loads the valid XAttribute names into a list for the given XElement name.
        /// </summary>
        /// <param name="xElementName">XName of element to get valid attributes for.</param>
        /// <returns>List of valid attribute names.</returns>
        public List<string> GetValidXAttributeNames(string xElementName)
        {
            var validAttributeNames = from validAttribute in validationTree.Element("XElements").Element(xElementName).Element("ValidAttributeNames").Elements()
                                      select validAttribute.Name.ToString();
            return validAttributeNames.ToList();
        }

        /// <summary>
        /// Returns a list of the names of the requried attributes for the given XElement name.
        /// </summary>
        /// <param name="xElementName">Name of the XElement to be created.</param>
        /// <returns></returns>
        public List<string> GetRequiredXAttributeNames(string xElementName)
        {
            var requiredXAttributeXNames = from validXAttributeName in validationTree.Element("XElements").Element(xElementName).Element("ValidAttributeNames").Elements()
                                           where validXAttributeName.Attribute("Required").Value == "true"
                                           select validXAttributeName.Name.ToString();
            if (requiredXAttributeXNames.Any())
            {
                return requiredXAttributeXNames.ToList();
            }
            else
            {
                throw new Exception("The xElement you are trying to create has no required attributes");
            }
        }

        /// <summary>
        /// Throws an exception if the element being created does not have a valid XName.
        /// </summary>
        /// <param name="xElementName">XName of the element to be created.</param>
        public void ValidateXElementXName(string xElementName)
        {
            if (!GetValidXElementNames().Any(x => x == xElementName))
            {
                throw new Exception("The name of the element you are trying to create is invalid");
            };
        }

        /// <summary>
        /// Throws an exception if the element being created does not have all required attributes.
        /// </summary>
        /// <param name="xElementName">XName of the element to be created.</param>
        /// <param name="attributeNames">Attributes to be added to the element.</param>
        public void ValidateXElementXAttributeXNames(string xElementName, params string[] attributeNames)
        {
            List<string> xAttributeNames = GetValidXAttributeNames(xElementName);
            foreach (string attributeName in attributeNames)
            {
                if (!xAttributeNames.Any(n => n == attributeName))
                {
                    throw new Exception("One of the attributes you have tried to add is invalid");
                }
            }
        }

        /// <summary>
        /// Compares attribute names given to the lists in the validation tree. Throws an error if a required attribute is missing.
        /// </summary>
        /// <param name="xElementName">Name of the element the attributes will be added to.</param>
        /// <param name="attributeNames">Names of the attributes to be added.</param>
        public void CheckForRequiredXAttributes(string xElementName, params string[] attributeNames)
        {
            List<string> requiredXAttributeNames = GetRequiredXAttributeNames(xElementName);
            foreach (string requiredAttributeName in requiredXAttributeNames)
            {
                if (!attributeNames.Contains(requiredAttributeName))
                {
                    throw new Exception("You are missing a required attribute.");
                }
            }
        }

        /// <summary>
        /// Validates a single XAttribute value based on its value and the type of value it should be.
        /// </summary>
        /// <param name="inputValue">Value of input.</param>
        /// <param name="inputTypeIdentifier">Type of value.</param>
        public void ValidateXAttributeValueType(string inputValue, string inputTypeIdentifier)
        {
            if (inputTypeIdentifier.StartsWith("SPECIFIC:")) { inputTypeIdentifier = inputTypeIdentifier.Substring(9); };

            switch (inputTypeIdentifier)
            {
                case "string":
                    break;
                case "decimal":
                    if (!TypeChecker.Is(inputValue, typeof(decimal))) { throw new Exception("One of the attribute values cannot be converted (decimal)."); };
                    break;
                case "integer":
                    if (!TypeChecker.Is(inputValue, typeof(int))) { throw new Exception("One of the attribute values cannot be converted (integer)."); };
                    break;
                case "integer5":
                    if (!((TypeChecker.Is(inputValue, typeof(int))) && (Convert.ToInt32(inputValue) % 5 == 0))) { throw new Exception("One of the attribute values cannot be converted (integer in increment of 5)."); };
                    break;
                default:
                    if (!GetValidGlobalValues(inputTypeIdentifier).Any(v => v == inputValue)) { throw new Exception("One of the attribute values cannot be converted (" + inputTypeIdentifier + ")."); };
                    break;
            };
        }

        /// <summary>
        /// Gets the value type for each of the attribute names and validates each attribute value individually.
        /// </summary>
        /// <param name="xElementName">Specifies the XElement to which the attributes will be attached.</param>
        /// <param name="inputValue">Values of inputs.</param>
        /// <param name="inputTypeIdentifier">Types of values.</param>
        public void ValidateXAttributeValueTypes(string xElementName, string[] attributeValues, string[] attributeNames)
        {
            if (attributeNames.Length != attributeValues.Length) { throw new Exception("The number of values and attribute names does not match."); };
            for (int i = 0; i < attributeNames.Length; i++)
            {
                string attributeValueType = validationTree.Element("XElements").Element(xElementName).Element("ValidAttributeNames").Element(attributeNames[i]).Attribute("ValidValueType").Value.ToString();
                ValidateXAttributeValueType(attributeValues[i], attributeValueType);
            }
        }

        /// <summary>
        /// Checks list of child XNames against a list of valid child XElement names for the given XElement.
        /// </summary>
        /// <param name="xElementName">Parent XElement name</param>
        /// <param name="childXElementXNames">Names of the child XElements</param>
        public void ValidateXElementChildrenOfXElement(string xElementName, params string[] childXElementXNames)
        {
            XElement checkedXElement = validationTree.Element("XElements").Element(xElementName);

            foreach (string childXElementXName in childXElementXNames)
            {
                if (!checkedXElement.Element("ValidChildNames").Elements().Any(x => x.Name == childXElementXName)) { throw new Exception("An XElement you tried to add to another is not a valid child."); };
            }

        }

        #endregion
    }
}
