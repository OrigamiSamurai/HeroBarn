using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Windows;

namespace HeroBarn
{
    class Hero
    {
        public static ObservableCollection<ObservableCascadingStat> heroStats = new ObservableCollection<ObservableCascadingStat> { new ObservableCascadingStat { Name = "ObservableStat1", HeldObject = new XElement("IAmAnXElement") } };

        XElement linkedStats = XElement.Load("UpdateWeb.xml");

        public ObservableCascadingStat herro = new ObservableCascadingStat { Name = "ObservableStat1", HeldObject = new XElement("IAmAnXElement") };


        public Hero()
        {
            SubscribeToStatChanges();
        }

        private void SubscribeToStatChanges()
        {
            foreach (ObservableCascadingStat stat in heroStats)
            {
                herro.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(stat_PropertyChanged);
            }
        }

        private void stat_PropertyChanged(object sender, EventArgs e)
        {
            MessageBoxResult message = MessageBox.Show(sender.ToString());
        }

        public void UpdateObservableCascadingStat()
        {
            //get my name
            //get list of elements that I should work with to figure out my value
            //get operations to perform
            //perform some operation on that data
            //set my HeldObject's value to that
        }

        public void UpdateLinkedChildObservableCascadingStats(string currentFieldName)
        {
            //get child stats
            //go update them
            linkedStats = XElement.Load("UpdateWeb.xml");
            var myLinkedStatNames = GetLinkedChildObservableCascadingStatNames(currentFieldName);
            if (myLinkedStatNames.Any())
            {
                foreach (string statName in myLinkedStatNames)
                {
                    // Hero.heroStats.Single(x => x.Name == statName).Update();
                }
            }

        }

        public string[] GetLinkedChildObservableCascadingStatNames(string currentFieldName)
        {
            //get names of child stats
            var linkedStatNames = from stats in linkedStats.Element(currentFieldName).Elements() where stats.Name.ToString() == currentFieldName select stats.Name.ToString();
            return linkedStatNames.ToArray();
        }
    }
}
