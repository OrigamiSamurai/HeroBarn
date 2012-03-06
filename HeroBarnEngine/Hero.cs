using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace HeroBarn
{
    public class Hero
    {
        public static ObservableCollection<ObservableObjectWrapper> heroStats = new ObservableCollection<ObservableObjectWrapper> { new ObservableObjectWrapper { Name = "ObservableStat1", HeldObject = new XElement("IAmAnXElement") } };

        XElement linkedStats = XElement.Load("UpdateWeb.xml");

        public ObservableObjectWrapper herro = new ObservableObjectWrapper { Name = "ObservableStat1", HeldObject = new XElement("IAmAnXElement") };


        public Hero()
        {
            SubscribeToStatChanges();
        }

        private void SubscribeToStatChanges()
        {
            foreach (ObservableObjectWrapper stat in heroStats)
            {
                herro.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(stat_PropertyChanged);
            }
        }

        private void stat_PropertyChanged(object sender, EventArgs e)
        {
           // MessageBoxResult message = MessageBox.Show(sender.ToString());
        }

        public static void UpdateObservableObjectWrapper(ObservableObjectWrapper observableObjectWrapper)
        {
            //get my name
            /*myParentFields = GetRelevantFields(observableObjectWrapper.Name)*/
                //get list of elements that I should work with to figure out my value
            /*calculateField(myParentFields, observableObjectWrapper.Name)*/
                //get operations to perform
                //perform some operation on that data
            //set my HeldObject's value to that
        }

        public void UpdateLinkedChildObservableObjectWrappers(string currentFieldName)
        {
            //get child stats
            //go update them
            linkedStats = XElement.Load("UpdateWeb.xml");
            var myLinkedStatNames = GetLinkedChildObservableObjectWrapperNames(currentFieldName);
            if (myLinkedStatNames.Any())
            {
                foreach (string statName in myLinkedStatNames)
                {
                    // Hero.heroStats.Single(x => x.Name == statName).Update();
                }
            }

        }

        public string[] GetLinkedChildObservableObjectWrapperNames(string currentFieldName)
        {
            var linkedStatNames = from stats in linkedStats.Element(currentFieldName).Elements() where stats.Name.ToString() == currentFieldName select stats.Name.ToString();
            return linkedStatNames.ToArray();
        }
    }
}
