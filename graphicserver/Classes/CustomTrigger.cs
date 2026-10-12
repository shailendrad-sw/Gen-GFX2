using MLCHARGENLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace graphicserver.Classes
{
    public class CustomTrigger
    {
        FrmMainServer objMain;
        public CustomTrigger(FrmMainServer obj)
        {
            objMain = obj;
        }
        string Graphics = "";
        string Id = "";
        string Value = "";
        string Type = "";
        string BugType = "";

        List<TriggerList> LST = new List<TriggerList>();
        public void ReadXmlFile()
        {
            try
            {
                LST.Clear();
                string _fileName = $@"{objMain.TemplatePath }\{Graphics}.ml-cgs";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(_fileName);
                XmlNodeList nodelist = xmlDoc.SelectNodes("cg-composition/custom-triggers/setter");
                for (int i = 0; i < nodelist.Count; i++)
                {
                    XmlNode Node = nodelist.Item(i);
                    XmlAttributeCollection ac = Node.Attributes;
                    TriggerList tl = new TriggerList();
                    foreach (XmlAttribute item in ac)
                    {
                        if (item.Name.ToLower() == "targetid")
                        {
                            tl.targetid = item.Value;
                        }
                        else if (item.Name.ToLower() == "id")
                        {
                            tl.id = item.Value;
                        }
                        else if (item.Name.ToLower() == "property")
                        {
                            tl.property = item.Value;
                        }
                        else if (item.Name.ToLower() == "targetproperty")
                        {
                            tl.targetproperty = item.Value;
                        }
                        else if (item.Name.ToLower() == "function")
                        {
                            tl.function = item.Value;
                        }
                    }
                    objMain.objlog.WriteEvents("CustomTrigger.ReadXmlFile", $"targetId={tl.targetid},targetproperty={ tl.targetproperty},id={tl.id}, property={tl.property},function={tl.function} ");
                    LST.Add(tl);
                }
            }
            catch (Exception ex)
            {
                objMain.objlog.WriteError("ReadXmlFile", "error in reading template File" + ex.ToString());
            }
        }
        public void ExecuteProcess(string _graphics, string _Id, string _Value, string _Type, string _BugType)
        {
            try
            {
                objMain.objlog.WriteEvents("ExecuteProcess", $"Receive Parameter templatename={_graphics},id={_Id},_value={_Value},type={_Type},bugtype={_BugType} ");
                Graphics = _graphics;
                Id = _Id;
                Value = _Value;
                Type = _Type;
                BugType = _BugType;
                ReadXmlFile();
                foreach (TriggerList tl in LST)
                {
                    if (tl.id.ToLower() == _Id.ToLower())
                    {
                        string functionname = GetFunctionName(tl.function);
                        string[] objparm = GetFunctionParameter(tl.function);
                        MethodInfo addMethod = this.GetType().GetMethod(functionname);
                        if (addMethod != null)
                        {
                            object result = addMethod.Invoke(this, objparm);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objMain.objlog.WriteError("ExecuteProcess", "Error In Execute Process " + ex.ToString());
            }
        }

        private string GetFunctionName(string name)
        {
            try
            {
                return name.Substring(0, name.IndexOf('('));
            }
            catch (Exception ex)
            {
                objMain.objlog.WriteError("GetFunctionName", "Error In Getting Function Name " + ex.ToString());
                return "";
            }

        }
        private string[] GetFunctionParameter(string name)
        {
            try
            {
                int startindex = name.IndexOf('(');
                int lastindex = name.LastIndexOf(')');
                string a = name.Substring(startindex + 1, (lastindex - startindex) - 1);
                return a.Split(',');
            }
            catch (Exception ex)
            {
                objMain.objlog.WriteError("GetFunctionName", "Error In Getting Parameter Name ..." + ex.ToString());
                return null;
            }

        }
        /// <summary>
        ///   <custom-triggers>
        ///        <setter id = "text-001" function="gettextwidth1(text-001,273,90,item-000,width,Segoe UI,25)" />
        ///   </custom-triggers>
        /// </summary>
        /// <param name="text"></param>
        /// <param name="defaultvalue"></param>
        /// <param name="addoffset"></param>
        /// <param name="targetproperty"></param>
        /// <param name="propertyforupdate"></param>
        public void gettextwidth(string text, string defaultvalue, string addoffset, string targetproperty, string propertyforupdate, string font, string fontsize)
        {
            try
            {
                objMain.objlog.WriteEvents("gettextwidth", $"start execute method text={text},defaultValue={defaultvalue},addoffset={addoffset},targetproperty {targetproperty},propertyforupdate={propertyforupdate}, font {font} ,fontsize={fontsize} ");
                int result = Convert.ToInt32(defaultvalue);
                string textItemId = "";
                objMain.m_objCharGen.AddNewItem(Value, 0, 0, 0, 1, ref textItemId);
                CG_TEXT_PROPS cg = new CG_TEXT_PROPS();
                cg.bsFontFace = font;
                cg.nFontHeight = Convert.ToInt32(fontsize);
                cg.bsTextString = Value;
                objMain.m_objCharGen.TextSetProps(textItemId, ref cg, 1, 1);
                tagSIZE tag;
                objMain.m_objCharGen.GetItemOriginalSize(textItemId, out tag);
                objMain.m_objCharGen.RemoveItem(textItemId, 0);
                int offset = tag.cx + Convert.ToInt32(addoffset);
                if (offset > result)
                {
                    result = offset;
                }
                XmlDocument Xdock = new XmlDocument();
                string _fileName = $@"{ objMain.TemplatePath}\{Graphics}.ml-cgs";
                Xdock.Load(_fileName);
                XmlNodeList nodelist = Xdock.SelectNodes("cg-composition/cg-items/cg-item[@id ='" + targetproperty + "']/cg-props");
                if (nodelist.Count > 0)
                {
                    XmlNode node = nodelist.Item(0);
                    if (node.Attributes != null && node.Attributes[propertyforupdate] != null)
                    {
                        node.Attributes[propertyforupdate].Value = result.ToString();
                    }
                    Xdock.Save(_fileName);
                }

            }
            catch (Exception ex)
            {

                objMain.objlog.WriteEvents("gettextwidth", $"error is execute method-" + ex.ToString());
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="textvalue"></param>
        public void ConverCamelCase(string targetid)
        {
            try
            {
                objMain.objlog.WriteEvents("ConverCamelCase", $"start execute method targetid={targetid} ");
                System.Globalization.TextInfo txtInfo = new System.Globalization.CultureInfo("en-us", false).TextInfo;
                string textWithCamelCase = txtInfo.ToTitleCase(Value.ToLower());
                XmlDocument Xdock = new XmlDocument();
                string _fileName = $@"{ objMain.TemplatePath}\{Graphics}.ml-cgs";
                Xdock.Load(_fileName);
                XmlNode nodelist = Xdock.SelectSingleNode("cg-composition/cg-items/cg-item[@id ='" + targetid + "']/text[@type ='text']");
                if (nodelist != null)
                {
                    nodelist.InnerText = textWithCamelCase;
                    Xdock.Save(_fileName);
                }
            }
            catch (Exception ex)
            {
                objMain.objlog.WriteError("ConverCamelCase", $"error is executing targetid={targetid} error is "+ex.ToString());
            }

        }
    



    //end of class
}
public class TriggerList
{
    public string targetid { get; set; }
    public string targetproperty { get; set; }
    public string id { get; set; }
    public string property { get; set; }
    public string function { get; set; }
}
}
