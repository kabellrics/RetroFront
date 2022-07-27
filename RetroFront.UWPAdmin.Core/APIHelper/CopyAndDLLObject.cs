using System;
using System.Collections.Generic;
using System.Text;

namespace RetroFront.UWPAdmin.Core.APIHelper
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.10.67.0 (Newtonsoft.Json v9.0.0.0)")]
    public partial class CopyAndDLLObject : System.ComponentModel.INotifyPropertyChanged
    {
        private string _source;
        private string _dest;

        [Newtonsoft.Json.JsonProperty("source", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Source
        {
            get { return _source; }
            set
            {
                if (_source != value)
                {
                    _source = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Newtonsoft.Json.JsonProperty("dest", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Dest
        {
            get { return _dest; }
            set
            {
                if (_dest != value)
                {
                    _dest = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static CopyAndDLLObject FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<CopyAndDLLObject>(data);
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

    }
}
