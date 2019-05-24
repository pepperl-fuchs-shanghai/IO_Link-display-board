using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for Common
/// </summary>
public class Common
{
    public static string HttpGet(string Url, string postDataStr)
    {
        string retString = "";
        string finalUrl = Url + postDataStr;
        try
        {
            System.Net.HttpWebRequest request = (HttpWebRequest)WebRequest.Create(finalUrl);

            request.Method = "GET";

            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream myResponseStream = response.GetResponseStream();

            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

            retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();

            myResponseStream.Close();


        }
        catch (Exception Error)
        {
            // MessageBox.Show(Error.ToString());
        }
        return retString;

    }

    public static LinkData GetDataByRegex(string str)
    {
        LinkData data = new LinkData();
           MatchCollection matches = Regex.Matches(str, "<div\\sclass=\"item\\sJ_MouserOnverReq[\\s\\S]*?<span>旺旺",
                  RegexOptions.IgnoreCase);
        foreach (Match match in matches)
        {

            string product = match.Value;
        }
        return data;
    }



    public static string ConvertArrayToString(List<Object> arr)
    {
        string str = "";
        for (int i = 0; i < 2; i++)
        {
            
            str = str + Convert.ToString(arr[i]).PadLeft(2, '0') + " ";
        }
        return str.TrimEnd();
    }

    public static string Convert_UC400F77_Distance(List<Object> arr)
    {
        string str = "";
        for (int i = 0; i < 2; i++)
        {
            str = str + Convert.ToString(Convert.ToInt32(arr[i]), 2).PadLeft(8, '0');
        }
        str = str.Substring(0, 14);
        string  value = Convert.ToInt32(str, 2).ToString()+"mm";


        return value;
    }

    public static string ConvertVDMDistance(List<Object> arr)
    {
        int data =0;
        for (int i = 0; i < 2; i++)
        {
            data = data * 256 + Convert.ToInt16(arr[i]);
        }
        return data.ToString()+"cm";
    }

    public static string ConvertUC400Distance(List<Object> arr)
    {
        int data = 0;
        for (int i = 0; i < 2; i++)
        {
            data = data * 256 + Convert.ToInt16(arr[i]);
        }
        double value = (double)data / (double)10;
        value = Math.Round(value, 1);

        return value.ToString() + "mm";
    }

    public static string ConvertOMTDistance(List<Object> arr)
    {
        int data = 0;
        for (int i = 0; i < 2; i++)
        {
            data = data * 256 + Convert.ToInt16(arr[i]);
        }
        double value = (double)data / (double)10;
        value = Math.Round(value, 1);

        return value.ToString() + "mm";
    }
    public static string Convert_IO_Value(List<Object> arr)
    {
       
        int data = Convert.ToInt16(arr[0]);
        if (data == 0)
        {
            return "bit_1 off |   bit_2 off";
        }
        if (data == 1)
        {
            return "bit_1 on |    bit_2 off";
        }
        if (data == 2)
        {
            return "bit_1 off |   bit_2 on";
        }
        if (data == 3)
        {
            return "bit_1 on |   bit_2 on";
        }
        return "";
    }
    public static string ConvertIO(List<Object> arr)
    {
        
       int data = Convert.ToInt16(arr[0]);
        if (data == 1)
        {
            return "on";
        }
        else
        {
            return "off";
        }
    }


    public static dynamic ConvertJson(string json)
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        jss.RegisterConverters(new JavaScriptConverter[] { new DynamicJsonConverter() });
        dynamic dy = jss.Deserialize(json, typeof(object)) as dynamic;
        return dy;
    }

    public class DynamicJsonConverter : JavaScriptConverter
    {
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            if (type == typeof(object))
            {
                return new DynamicJsonObject(dictionary);
            }

            return null;
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(object) })); }
        }
    }


    public class DynamicJsonObject : DynamicObject
    {
        private IDictionary<string, object> Dictionary { get; set; }

        public DynamicJsonObject(IDictionary<string, object> dictionary)
        {
            this.Dictionary = dictionary;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = this.Dictionary[binder.Name];

            if (result is IDictionary<string, object>)
            {
                result = new DynamicJsonObject(result as IDictionary<string, object>);
            }
            else if (result is ArrayList && (result as ArrayList) is IDictionary<string, object>)
            {
                result = new List<DynamicJsonObject>((result as ArrayList).ToArray().Select(x => new DynamicJsonObject(x as IDictionary<string, object>)));
            }
            else if (result is ArrayList)
            {
                result = new List<object>((result as ArrayList).ToArray());
            }

            return this.Dictionary.ContainsKey(binder.Name);
        }
    }
}