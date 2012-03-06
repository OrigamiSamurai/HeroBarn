using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace HeroBarn
{
    class ItemLibrary
    {
        public static ObservableCollection<XElement> currentItemsInLibrary = new ObservableCollection<XElement> { };

        public ItemLibrary()
        {
            if (File.Exists("Items.xml"))
            {
                currentItemsInLibrary = XElement.Load("Items.xml").Elements().ToObservableCollection();
            }
            else
            {
                currentItemsInLibrary.Add(new XElement("Items"));
                Save();
            }
        }

        public void Add(XElement newItem)
        {
            currentItemsInLibrary.Add(newItem);
            Save();
        }

        public bool ExistsInLibrary(XElement testedXElement)
        {
            return (currentItemsInLibrary.Any(x => x == testedXElement));
        }

        public void Remove(XElement xElementToBeRemoved)
        {
            if (ExistsInLibrary(xElementToBeRemoved)) { currentItemsInLibrary.Remove(xElementToBeRemoved); };
            Save();
        }

        public void Save()
        {
            XElement Items = new XElement("Items");
            Items.Add(currentItemsInLibrary.ToArray());
            Items.Save("Items.xml");
        }
    }
}
