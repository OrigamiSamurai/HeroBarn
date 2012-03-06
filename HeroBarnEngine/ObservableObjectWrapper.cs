using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace HeroBarn
{
    public class ObservableObjectWrapper
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private string m_Name;

        public string Name
        {
            set { m_Name = value; OnPropertyChanged("Name"); }
            get { return m_Name; }
        }

        private object m_heldObject = new object();

        public object HeldObject
        {
            set { m_heldObject = value; OnPropertyChanged("HeldObject"); }
            get { return m_heldObject; }
        }
    }
}
