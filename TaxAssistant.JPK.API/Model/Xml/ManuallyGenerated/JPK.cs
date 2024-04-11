namespace TaxAssistant.JPK.Model.Xml.ManuallyGenerated
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://jpk.mf.gov.pl/wzor/2016/10/26/10262/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://jpk.mf.gov.pl/wzor/2016/10/26/10262/", IsNullable = false)]
    public partial class JPK
    {

        private object[] itemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Naglowek", typeof(JPKNaglowek))]
        [System.Xml.Serialization.XmlElementAttribute("PKPIRCtrl", typeof(JPKPKPIRCtrl))]
        [System.Xml.Serialization.XmlElementAttribute("PKPIRInfo", typeof(JPKPKPIRInfo))]
        [System.Xml.Serialization.XmlElementAttribute("PKPIRWiersz", typeof(JPKPKPIRWiersz))]
        [System.Xml.Serialization.XmlElementAttribute("Podmiot1", typeof(JPKPodmiot1))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

}
