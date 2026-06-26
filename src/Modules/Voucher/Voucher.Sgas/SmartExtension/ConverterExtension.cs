using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Voucher.Sgas.SmartExtension;

public class ConverterExtension
{
    public static string SmartConvertDatetimeVN(DateTime date)
    { return date.ToString("d", CultureInfo.CreateSpecificCulture("vi-VN")); }
    public static string SmartConvertDatetimeVN(string ctr)
    { return Convert.ToDateTime(ctr).ToString("d", CultureInfo.CreateSpecificCulture("vi-VN")); }
    public static string SmartConvertDatetime(DateTime date)
    { return date.ToString("d", DateTimeFormatInfo.InvariantInfo); }
    public static string SmartConvertDatetime(string ctr)
    { return Convert.ToDateTime(ctr).ToString("d", DateTimeFormatInfo.InvariantInfo); }

    public static string ConvertNumberToText(double inputNumber, bool suffix = true)
    {
        string[] unitNumbers = ["không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín"];
        string[] placeValues = ["", "nghìn,", "triệu,", "tỷ,"];
        bool isNegative = false;

        string sNumber = inputNumber.ToString("#");
        if (string.IsNullOrEmpty(sNumber))
            return "";
        if (string.Equals(sNumber, "0"))
            return "0 đồng.";
        double number = Convert.ToDouble(sNumber);
        if (number < 0)
        {
            number = -number;
            sNumber = number.ToString();
            isNegative = true;
        }
        int ones, tens, hundreds;

        int positionDigit = sNumber.Length;

        string result = " ";

        if (positionDigit == 0)
            result = unitNumbers[0] + result;
        else
        {
            // 0:       ###
            // 1: nghìn ###,###
            // 2: triệu ###,###,###
            // 3: tỷ    ###,###,###,###
            int placeValue = 0;

            while (positionDigit > 0)
            {
                tens = hundreds = -1;
                ones = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                positionDigit--;
                if (positionDigit > 0)
                {
                    tens = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                    positionDigit--;
                    if (positionDigit > 0)
                    {
                        hundreds = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                        positionDigit--;
                    }
                }
                if ((ones > 0) || (tens > 0) || (hundreds > 0) || (placeValue == 3))
                    result = placeValues[placeValue] + result;

                placeValue++;
                if (placeValue > 3) placeValue = 1;

                if ((ones == 1) && (tens > 1))
                    result = "một " + result;
                else
                {
                    if ((ones == 5) && (tens > 0))
                        result = "lăm " + result;
                    else if (ones > 0)
                        result = unitNumbers[ones] + " " + result;
                }
                if (tens < 0)
                    break;
                else
                {
                    if ((tens == 0) && (ones > 0)) result = "lẻ " + result;
                    if (tens == 1) result = "mười " + result;
                    if (tens > 1) result = unitNumbers[tens] + " mươi " + result;
                }
                if (hundreds < 0) break;
                else
                {
                    if ((hundreds > 0) || (tens > 0) || (ones > 0))
                        result = unitNumbers[hundreds] + " trăm " + result;
                }
                result = " " + result;
            }
        }
        result = result.Trim();
        result = result.Substring(0, 1).ToUpper() + result.Substring(1, result.Length - 1);
        if (isNegative) result = "Âm " + result;
        if (result.EndsWith(","))
        {
            result = result.Substring(0, result.Length - 1);
        }
        result = result.Replace("lẻ", "linh");
        return result + (suffix ? " đồng" : "") + "./.";
    }

    public static string ObjectToXmlNotSchema<T>(T obj)
    {
        var serializer = new XmlSerializer(typeof(T));
        var ns = new XmlSerializerNamespaces();
        ns.Add("", "");
        var sw = new StringWriter();
        var xmlWriter = XmlWriter.Create(sw, new XmlWriterSettings() { OmitXmlDeclaration = true });
        serializer.Serialize(xmlWriter, obj, ns);
        string xmlData = sw.ToString();
        return xmlData;
    }
    public static string ObjectToXmlNotSchema<T>(T obj, string strSignReplace)
    {
        var serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(strSignReplace));
        var ns = new XmlSerializerNamespaces();
        ns.Add("", "");
        var sw = new StringWriter();
        var xmlWriter = XmlWriter.Create(sw, new XmlWriterSettings() { OmitXmlDeclaration = true });
        serializer.Serialize(xmlWriter, obj, ns);
        string xmlData = sw.ToString();
        return xmlData;
    }
    public static string ListObjectToXmlNotSchema<T>(List<T> arrObj, string strSignReplace)
    {
        var serializer = new XmlSerializer(typeof(List<T>), new XmlRootAttribute(strSignReplace));
        var ns = new XmlSerializerNamespaces();
        ns.Add("", "");
        var sw = new StringWriter();
        var xmlWriter = XmlWriter.Create(sw, new XmlWriterSettings() { OmitXmlDeclaration = true });
        serializer.Serialize(xmlWriter, arrObj, ns);
        string xmlData = sw.ToString();
        return xmlData;
    }
    public static string ToXML<T>(T obj)
    {
        using (var stringwriter = new System.IO.StringWriter())
        {
            var serializer = new XmlSerializer(obj.GetType());
            serializer.Serialize(stringwriter, obj);
            return stringwriter.ToString();
        }
    }
    /// <summary>
    /// Object to xml
    /// </summary>
    /// <typeparam name="T">Object</typeparam>
    /// <param name="dataToSerialize">Data</param>
    /// <returns>String xml</returns>
    public static string Serialize<T>(T dataToSerialize)
    {
        try
        {
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stringwriter, dataToSerialize);
            return stringwriter.ToString();
        }
        catch
        {
            throw;
        }
    }
    /// <summary>
    /// String xml to object
    /// </summary>
    /// <typeparam name="T">Object</typeparam>
    /// <param name="xmlText">String xml</param>
    /// <returns>Object</returns>
    public static T Deserialize<T>(string xmlText)
    {
        try
        {
            var stringReader = new System.IO.StringReader(xmlText);
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stringReader);
        }
        catch
        {
            throw;
        }
    }
    public static DataTable ToDataTable<T>(IList<T> items)
    {
        DataTable table = new DataTable()
        {
            TableName = "DataObject"
        };
        try
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in items)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return table;
    }
}
