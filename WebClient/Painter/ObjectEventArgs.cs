using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLW_WebClient.Painter
{
    public class ObjectEventArgs : EventArgs
    {
        public delegate void ObjectEventHandler(object sender, ObjectEventArgs e);

        private object _obj;

        public Object SendObject
        {
            get { return this._obj; }
        }

        public ObjectEventArgs(object obj) : base()
        {
            this._obj = obj;
        }
    }
}
