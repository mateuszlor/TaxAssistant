namespace TaxAssistant.JPK.Model.Xml.ManuallyGenerated
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://jpk.mf.gov.pl/wzor/2016/10/26/10262/")]
    public partial class JPKNaglowekKodFormularza
    {

        private string kodSystemowyField;

        private string wersjaSchemyField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string kodSystemowy
        {
            get
            {
                return this.kodSystemowyField;
            }
            set
            {
                this.kodSystemowyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string wersjaSchemy
        {
            get
            {
                return this.wersjaSchemyField;
            }
            set
            {
                this.wersjaSchemyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

}
