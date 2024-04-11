namespace TaxAssistant.JPK.Model.Xml.ManuallyGenerated
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://jpk.mf.gov.pl/wzor/2016/10/26/10262/")]
    public partial class JPKNaglowek
    {

        private string wariantFormularzaField;

        private string celZlozeniaField;

        private string dataWytworzeniaJPKField;

        private string dataOdField;

        private string dataDoField;

        private string domyslnyKodWalutyField;

        private string kodUrzeduField;

        private JPKNaglowekKodFormularza[] kodFormularzaField;

        /// <remarks/>
        public string WariantFormularza
        {
            get
            {
                return this.wariantFormularzaField;
            }
            set
            {
                this.wariantFormularzaField = value;
            }
        }

        /// <remarks/>
        public string CelZlozenia
        {
            get
            {
                return this.celZlozeniaField;
            }
            set
            {
                this.celZlozeniaField = value;
            }
        }

        /// <remarks/>
        public string DataWytworzeniaJPK
        {
            get
            {
                return this.dataWytworzeniaJPKField;
            }
            set
            {
                this.dataWytworzeniaJPKField = value;
            }
        }

        /// <remarks/>
        public string DataOd
        {
            get
            {
                return this.dataOdField;
            }
            set
            {
                this.dataOdField = value;
            }
        }

        /// <remarks/>
        public string DataDo
        {
            get
            {
                return this.dataDoField;
            }
            set
            {
                this.dataDoField = value;
            }
        }

        /// <remarks/>
        public string DomyslnyKodWaluty
        {
            get
            {
                return this.domyslnyKodWalutyField;
            }
            set
            {
                this.domyslnyKodWalutyField = value;
            }
        }

        /// <remarks/>
        public string KodUrzedu
        {
            get
            {
                return this.kodUrzeduField;
            }
            set
            {
                this.kodUrzeduField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("KodFormularza", IsNullable = true)]
        public JPKNaglowekKodFormularza[] KodFormularza
        {
            get
            {
                return this.kodFormularzaField;
            }
            set
            {
                this.kodFormularzaField = value;
            }
        }
    }

}
