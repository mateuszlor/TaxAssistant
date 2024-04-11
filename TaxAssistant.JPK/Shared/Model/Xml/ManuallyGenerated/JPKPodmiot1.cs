namespace TaxAssistant.JPK.Model.Xml.ManuallyGenerated
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://jpk.mf.gov.pl/wzor/2016/10/26/10262/")]
    public partial class JPKPodmiot1
    {

        private JPKPodmiot1IdentyfikatorPodmiotu[] identyfikatorPodmiotuField;

        private JPKPodmiot1AdresPodmiotu[] adresPodmiotuField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("IdentyfikatorPodmiotu")]
        public JPKPodmiot1IdentyfikatorPodmiotu[] IdentyfikatorPodmiotu
        {
            get
            {
                return this.identyfikatorPodmiotuField;
            }
            set
            {
                this.identyfikatorPodmiotuField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AdresPodmiotu")]
        public JPKPodmiot1AdresPodmiotu[] AdresPodmiotu
        {
            get
            {
                return this.adresPodmiotuField;
            }
            set
            {
                this.adresPodmiotuField = value;
            }
        }
    }
}
