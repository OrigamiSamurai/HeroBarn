using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace HeroBarn
{
    class ObservableXElement
    {
        public ObservableXElement(XElement xElem)
        {
            xElement = xElem;
        }
        public ObservableXElement(string xElemXName)
        {
            xElement = new XElement(xElemXName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private XElement m_xElement = new XElement("NewObservableXElement");

        public XElement xElement
        {
            set { m_xElement = value; OnPropertyChanged("xElement"); }
            get { return m_xElement; }
        }
    }
}
