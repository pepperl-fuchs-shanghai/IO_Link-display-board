<%@ WebHandler Language="C#" Class="getdata" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;

public class getdata : IHttpHandler {

    public void ProcessRequest(HttpContext context) {
        context.Response.ContentType = "text/plain";
        List<LinkData> linkdatalist = new List<LinkData>();
        string content = "";
        for (int i = 0; i < 4; i++)
        {
            content = Common.HttpGet("http://192.168.1.2/r/ioldetails.lr?port=", i.ToString());
            dynamic data = null;
            try
            {



                data = Common.ConvertJson(content);
                //string status = data.status.leds.ports[i]["iol"].ToString();
                //string port = data.iol.port.ToString();
                //string state = data.status.dia.device[0]["state"].ToString();
                ////string input = data.iol.data.input[2].ToString("X2");
                //List<Object> arr = data.iol.data.input;
                //string input = Common.ConvertArrayToString(arr);
                //string device_id = data.iol.device_id.ToString();

                //string device_name = data.iol.device_name.ToString();
                //string prod_id = data.iol.prod_id.ToString();
                //string prod_text = data.iol.prod_text.ToString();
                //string fw_version = data.iol.fw_version.ToString();
                //string hw_version = data.iol.hw_version.ToString();


                string powerOnTime = DateTime.Now.ToString();
                string temperature = "Good";

                List<Object> arr = data.iol.data.input;



                object tagdata = data.iol.tag;
                string tag = tagdata == null ? "Your Automation, Our Passion" : tagdata.ToString();
                string device_name = data.iol.device_name.ToString();
                device_name = device_name.Replace("/", "_");
                string input = Common.Convert_IO_Value(arr);
                if (device_name.Contains("VDM"))
                {
                    input = Common.ConvertVDMDistance(arr);
                }
                if(device_name.Contains("OMT"))
                {
                    input = Common.ConvertOMTDistance(arr);
                }
                if (device_name.Contains("UC400"))
                {
                    input = Common.Convert_UC400F77_Distance(arr);
                }
               


                linkdatalist.Add(new LinkData
                {
                    status = data.status.leds.ports[i]["iol"].ToString(),
                    port = (i+1).ToString(),
                    state = data.status.dia.device[0]["state"].ToString(),
                    input = input,
                    device_id = data.iol.device_id.ToString(),
                    tag = tag,
                    device_name = device_name,
                    prod_id = data.iol.prod_id.ToString(),
                    prod_text = data.iol.prod_text.ToString(),
                    fw_version = data.iol.fw_version.ToString(),
                    hw_version = data.iol.hw_version.ToString(),
                    powerOnTime = DateTime.Now.ToString(),
                    temperature = "Good"
                });
            }
            catch (Exception ex)
            {
                linkdatalist.Add(new LinkData
                {
                    status = "0",
                    port = (i+1).ToString(),
                    state = "not connected",
                    input = "",
                    device_id = "",
                    tag = "",
                    device_name = "",
                    prod_id = "",
                    prod_text = "",
                    fw_version = "",
                    hw_version = "",
                    powerOnTime = "",
                    temperature = "",
                });
            }
            finally
            {
                content = "";
            }

        }

        //Random random = new Random();
        //Random random1 = new Random();

        //for (int i = 1; i <= 4; i++)
        //{
        //    linkdatalist.Add(new LinkData
        //    {
        //        status = random.Next(0, 2).ToString(),
        //        port = i.ToString(),
        //        state = random.Next(0, 2).ToString(),
        //        input = "",
        //        device_id = random.Next(1, 7).ToString(),
        //        device_name = "",
        //        prod_id = "",
        //        prod_text = "",
        //        tag = "",
        //        fw_version = "",
        //        hw_version = "",

        //        powerOnTime = "",
        //        temperature = "",

        //    });
        //}
        System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
        context.Response.Write(js.Serialize(linkdatalist));
    }



    public bool IsReusable {
        get {
            return false;
        }
    }

}
