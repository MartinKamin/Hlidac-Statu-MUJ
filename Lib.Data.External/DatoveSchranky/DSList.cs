﻿using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Net;
using System.IO.Compression;

namespace HlidacStatu.Lib.Data.External.DatoveSchranky
{


    public class DSList
    {


        public static IEnumerable<(string ico, string ds, string parentDS, string dsName)> UpdateListDS()
        {
            List<listBox> dsL = new List<listBox>();

            string[] urls = new string[] {
            "https://www.mojedatovaschranka.cz/sds/datafile?format=xml&service=seznam_ds_ovm",
            "https://www.mojedatovaschranka.cz/sds/datafile?format=xml&service=seznam_ds_po",
            "https://www.mojedatovaschranka.cz/sds/datafile?format=xml&service=seznam_ds_pfo",
            "https://www.mojedatovaschranka.cz/sds/datafile?format=xml&service=seznam_ds_fo",
            };

            var serializer = new XmlSerializer(typeof(list), "http://seznam.gov.cz/ovm/datafile/seznam_ds/v1");
            var wc = new WebClient();
            foreach (var url in urls)
            {
                wc.Headers[HttpRequestHeader.AcceptEncoding] = "gzip";
                var responseStream = new GZipStream(wc.OpenRead(url), CompressionMode.Decompress);
                var wcreader = new StreamReader(responseStream);
                var xmls = wcreader.ReadToEnd();
                //var xmls = wc.DownloadString(url);
                using (TextReader reader = new StringReader(xmls))
                {
                    var dsFromXML = (list)serializer.Deserialize(reader);
                    if (dsFromXML.box?.Count() > 0)
                    {
                        dsL.AddRange(dsFromXML.box);
                    }

                }
            }

            return dsL.Select(
                m => (m.ico, m.id, m.hierarchy?.masterId, m?.name?.tradeName)
                );

        }






        //------------------------------------------------------------------------------
        //------------------------------------------------------------------------------
        //------------------------------------------------------------------------------




        //------------------------------------------------------------------------------
        // <auto-generated>
        //     This code was generated by a tool.
        //     Runtime Version:4.0.30319.42000
        //
        //     Changes to this file may cause incorrect behavior and will be lost if
        //     the code is regenerated.
        // </auto-generated>
        //------------------------------------------------------------------------------


        // 
        // This source code was auto-generated by xsd, Version=4.8.3928.0.
        // 


        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlTypeAttribute(AnonymousType = true, Namespace = "http://seznam.gov.cz/ovm/datafile/seznam_ds/v1")]
        [XmlRootAttribute(Namespace = "http://seznam.gov.cz/ovm/datafile/seznam_ds/v1", IsNullable = false)]
        public partial class list
        {

            private listBox[] boxField;

            /// <remarks/>
            [XmlElementAttribute("box")]
            public listBox[] box
            {
                get
                {
                    return boxField;
                }
                set
                {
                    boxField = value;
                }
            }
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlTypeAttribute(AnonymousType = true, Namespace = "http://seznam.gov.cz/ovm/datafile/seznam_ds/v1")]
        public partial class listBox
        {

            private string idField;

            private string typeField;

            private int subtypeField;

            private bool subtypeFieldSpecified;

            private NameType nameField;

            private string icoField;

            private AddressType addressField;

            private bool pdzField;

            private bool pdzFieldSpecified;

            private bool ovmField;

            private bool ovmFieldSpecified;

            private HierarchyType hierarchyField;

            private string idOVMField;

            /// <remarks/>
            public string id
            {
                get
                {
                    return idField;
                }
                set
                {
                    idField = value;
                }
            }

            /// <remarks/>
            public string type
            {
                get
                {
                    return typeField;
                }
                set
                {
                    typeField = value;
                }
            }

            /// <remarks/>
            public int subtype
            {
                get
                {
                    return subtypeField;
                }
                set
                {
                    subtypeField = value;
                }
            }

            /// <remarks/>
            [XmlIgnoreAttribute()]
            public bool subtypeSpecified
            {
                get
                {
                    return subtypeFieldSpecified;
                }
                set
                {
                    subtypeFieldSpecified = value;
                }
            }

            /// <remarks/>
            public NameType name
            {
                get
                {
                    return nameField;
                }
                set
                {
                    nameField = value;
                }
            }

            /// <remarks/>
            public string ico
            {
                get
                {
                    return icoField;
                }
                set
                {
                    icoField = value;
                }
            }

            /// <remarks/>
            public AddressType address
            {
                get
                {
                    return addressField;
                }
                set
                {
                    addressField = value;
                }
            }

            /// <remarks/>
            public bool pdz
            {
                get
                {
                    return pdzField;
                }
                set
                {
                    pdzField = value;
                }
            }

            /// <remarks/>
            [XmlIgnoreAttribute()]
            public bool pdzSpecified
            {
                get
                {
                    return pdzFieldSpecified;
                }
                set
                {
                    pdzFieldSpecified = value;
                }
            }

            /// <remarks/>
            public bool ovm
            {
                get
                {
                    return ovmField;
                }
                set
                {
                    ovmField = value;
                }
            }

            /// <remarks/>
            [XmlIgnoreAttribute()]
            public bool ovmSpecified
            {
                get
                {
                    return ovmFieldSpecified;
                }
                set
                {
                    ovmFieldSpecified = value;
                }
            }

            /// <remarks/>
            public HierarchyType hierarchy
            {
                get
                {
                    return hierarchyField;
                }
                set
                {
                    hierarchyField = value;
                }
            }

            /// <remarks/>
            public string idOVM
            {
                get
                {
                    return idOVMField;
                }
                set
                {
                    idOVMField = value;
                }
            }
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlTypeAttribute(Namespace = "http://seznam.gov.cz/ovm/datafile/seznam_ds/v1")]
        public partial class NameType
        {

            private PersonType personField;

            private string tradeNameField;

            /// <remarks/>
            public PersonType person
            {
                get
                {
                    return personField;
                }
                set
                {
                    personField = value;
                }
            }

            /// <remarks/>
            public string tradeName
            {
                get
                {
                    return tradeNameField;
                }
                set
                {
                    tradeNameField = value;
                }
            }
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlTypeAttribute(Namespace = "http://seznam.gov.cz/ovm/datafile/seznam_ds/v1")]
        public partial class PersonType
        {

            private string firstNameField;

            private string lastNameField;

            private string middleNameField;

            private string lastNameAtBirthField;

            /// <remarks/>
            public string firstName
            {
                get
                {
                    return firstNameField;
                }
                set
                {
                    firstNameField = value;
                }
            }

            /// <remarks/>
            public string lastName
            {
                get
                {
                    return lastNameField;
                }
                set
                {
                    lastNameField = value;
                }
            }

            /// <remarks/>
            public string middleName
            {
                get
                {
                    return middleNameField;
                }
                set
                {
                    middleNameField = value;
                }
            }

            /// <remarks/>
            public string lastNameAtBirth
            {
                get
                {
                    return lastNameAtBirthField;
                }
                set
                {
                    lastNameAtBirthField = value;
                }
            }
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlTypeAttribute(Namespace = "http://seznam.gov.cz/ovm/datafile/seznam_ds/v1")]
        public partial class HierarchyType
        {

            private bool isMasterField;

            private string masterIdField;

            /// <remarks/>
            public bool isMaster
            {
                get
                {
                    return isMasterField;
                }
                set
                {
                    isMasterField = value;
                }
            }

            /// <remarks/>
            public string masterId
            {
                get
                {
                    return masterIdField;
                }
                set
                {
                    masterIdField = value;
                }
            }
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [XmlTypeAttribute(Namespace = "http://seznam.gov.cz/ovm/datafile/seznam_ds/v1")]
        public partial class AddressType
        {

            private long codeField;

            private bool codeFieldSpecified;

            private string cityField;

            private string districtField;

            private string streetField;

            private string cpField;

            private string coField;

            private string ceField;

            private string zipField;

            private string regionField;

            private long addressPointField;

            private bool addressPointFieldSpecified;

            private string stateField;

            private string fullAddressField;

            /// <remarks/>
            public long code
            {
                get
                {
                    return codeField;
                }
                set
                {
                    codeField = value;
                }
            }

            /// <remarks/>
            [XmlIgnoreAttribute()]
            public bool codeSpecified
            {
                get
                {
                    return codeFieldSpecified;
                }
                set
                {
                    codeFieldSpecified = value;
                }
            }

            /// <remarks/>
            public string city
            {
                get
                {
                    return cityField;
                }
                set
                {
                    cityField = value;
                }
            }

            /// <remarks/>
            public string district
            {
                get
                {
                    return districtField;
                }
                set
                {
                    districtField = value;
                }
            }

            /// <remarks/>
            public string street
            {
                get
                {
                    return streetField;
                }
                set
                {
                    streetField = value;
                }
            }

            /// <remarks/>
            public string cp
            {
                get
                {
                    return cpField;
                }
                set
                {
                    cpField = value;
                }
            }

            /// <remarks/>
            public string co
            {
                get
                {
                    return coField;
                }
                set
                {
                    coField = value;
                }
            }

            /// <remarks/>
            public string ce
            {
                get
                {
                    return ceField;
                }
                set
                {
                    ceField = value;
                }
            }

            /// <remarks/>
            public string zip
            {
                get
                {
                    return zipField;
                }
                set
                {
                    zipField = value;
                }
            }

            /// <remarks/>
            public string region
            {
                get
                {
                    return regionField;
                }
                set
                {
                    regionField = value;
                }
            }

            /// <remarks/>
            public long addressPoint
            {
                get
                {
                    return addressPointField;
                }
                set
                {
                    addressPointField = value;
                }
            }

            /// <remarks/>
            [XmlIgnoreAttribute()]
            public bool addressPointSpecified
            {
                get
                {
                    return addressPointFieldSpecified;
                }
                set
                {
                    addressPointFieldSpecified = value;
                }
            }

            /// <remarks/>
            public string state
            {
                get
                {
                    return stateField;
                }
                set
                {
                    stateField = value;
                }
            }

            /// <remarks/>
            public string fullAddress
            {
                get
                {
                    return fullAddressField;
                }
                set
                {
                    fullAddressField = value;
                }
            }
        }

    }
}