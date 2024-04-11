namespace TaxAssistant.JPK.Model.Xml.ManuallyGenerated
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://jpk.mf.gov.pl/wzor/2016/10/26/10262/")]
    public partial class JPKPodmiot1IdentyfikatorPodmiotu
    {

        private string nIPField;

        private string pelnaNazwaField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2016/01/25/eD/DefinicjeTypy/")]
        public string NIP
        {
            get
            {
                return this.nIPField;
            }
            set
            {
                this.nIPField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2016/01/25/eD/DefinicjeTypy/")]
        public string PelnaNazwa
        {
            get
            {
                return this.pelnaNazwaField;
            }
            set
            {
                this.pelnaNazwaField = value;
            }
        }
    }
}
