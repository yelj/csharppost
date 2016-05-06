using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using System.Xml;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace LoginDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Console.WriteLine("sss");


            //获取天气和解析
            /*    System.Net.HttpWebRequest request = (HttpWebRequest)HttpWebRequest.
                    Create(Consts.ServerUrl+ "/user/login");
           //     Console.WriteLine(Consts.ServerUrl);
                request.Timeout = 5000;
                request.Method = "POST";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream());*/
            //   string jsonstr = HttpPost(Consts.ServerUrl+"/user/load?sid=" + "", "DD");

              String pwd="";
        
            string str = "456789" + "gsdef";

            MD5 md = MD5.Create();

            byte[] bytes = md.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));

            foreach (byte b in bytes)

            {

                pwd += b.ToString("x2");

            }
            Console.WriteLine(pwd);

            Account account = new Account("18689662551", pwd, "","android", "11", "6","","");
            /*    account.setUsername("18689662551");
                account.setPassword(pwd);
                account.setPlatform("android");
                account.setSystemversion("11");
                account.setVersion("6");
                account.setClientid("");*/

            JavaScriptSerializer javSe= new JavaScriptSerializer();
      //      Weather weather = new Weather();
         string   acStr = javSe.Serialize(account);

            Console.WriteLine(acStr);

         string jsonstr= doPostMethodToObj(Consts.ServerUrl + "/user/login" + "",           acStr);

           // string jsonstr = doPostMethodToObj(Consts.ServerUrl + "/user/load" + "", acStr);
            JavaScriptSerializer j = new JavaScriptSerializer();
            LoginResultInfos weather = new LoginResultInfos();
            weather = j.Deserialize<LoginResultInfos>(jsonstr);
            saveXml(weather);
      //      string acSt11r = javSe.Serialize(weather);
            Console.WriteLine(weather.token+";");
            /*  Usuarios user = new Usuarios();
              user.Usuario = this.txtUsuario.Text;
              user.Contrasena = this.txtContrasena.Text;

                  if(user.buscar()==true){
                      MessageBox.Show(user.Mensaje,"Iniciar Seccion",MessageBoxButtons.OK,MessageBoxIcon.Information);
                      MessageBox.Show("Usuario: '"+user.Usuario+"' con contrasena: '"+user.Contrasena+ "' Existe en la base de datos");
                  }
                  else
                  {
                      MessageBox.Show(user.Mensaje, "Iniciar Seccion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      MessageBox.Show("'"+txtUsuario.Text+" "+txtContrasena.Text+"' No existe en la base de datos");

                  }*/
        }


        public static string Serialize(object o)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();
            json.Serialize(o, sb);
            return sb.ToString();
        }

       

        public static string doPostMethodToObj(string metodUrl, string jsonBody)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(metodUrl);
            request.Method = "post";
            request.ContentType = "application/json;charset=UTF-8";
            var stream = request.GetRequestStream();
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(jsonBody);
                writer.Flush();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();


            using (StreamReader reader = new StreamReader(response.GetResponseStream(),
                System.Text.Encoding.GetEncoding("utf-8")))
            {
                return reader.ReadToEnd();
            }
        //    string json =       getResponsestring(response);
          //  return JsonConvert.DeserializeObject<T>(json);
        }


        private void saveXml(LoginResultInfos weather)
        {
            XmlDocument xml;
            string path = Application.StartupPath + "\\功能测试参数.xml";
            if (System.IO.File.Exists(path))
            {
                xml = new XmlDocument();
                xml.Load(path); //加载XML文档
            }
            else
            {
                xml = new XmlDocument();            //创建根节点 config    
                xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", ""));
                XmlElement one = xml.CreateElement("XMLDatabase-Entities"); //把根节点加到xml文档中   
                xml.AppendChild(one);

                XmlElement preview = xml.CreateElement("Customer");  // 创建preview元素  
                preview.SetAttribute("_uuid", "album1");    //  
                preview.SetAttribute("extension", "xpi");  //  
                preview.SetAttribute("sizew", "680");      //    设置属性  
                preview.SetAttribute("sizeh", "474");      //  
                preview.SetAttribute("totalpage", "25");   //  
                one.AppendChild(preview);
            }

            XmlNode root = xml.SelectSingleNode("XMLDatabase-Entities");

            XmlElement newEle = xml.CreateElement("Customer");  // 创建preview元素  
            newEle.SetAttribute("_uuid", "al22bum1");    //  
            newEle.SetAttribute("extension", "xpi");  //  
            newEle.SetAttribute("sizew", "680");      //    设置属性  
            newEle.SetAttribute("sizeh", "474");      //  
            newEle.SetAttribute("totalpage", "25");   //  
            root.AppendChild(newEle);
         
            xml.Save(path);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }


    public class BaseResult
    {
        public int state;
        public String errorcode;
        public String errormsg;
    }
    public class LoginResultInfos : BaseResult
    {
            public String token;
        public Profile data;        
}

    public class Profile
    {


        public int role;//-1黑户，2犬舍，3宠物店，4宠信宝用户
        public long uid;
        public int sex;
        public String address = "";
        // 介绍
        public String intro;
        public String nickname;
        public String mobile;


        public int friendship;


       // public int checked= 0;//验证过手机号码 1为验证过


    public String topicPic;
        // 头像，url形式保存
        public String avatar;
        public int cityid;
        // 积分
        public int score;
        // 威望
        public int prestige;

        public Count count;


        public Company company;


        public String city = "";


        public class Count
        {
            public int picCount;
            public int fansCount;
            public int friendCount;
            public int zanCount;
            public int feedCount;
            public int petCount;
        }

        public class Company
        {
            public int cid;
            public String name;
            public String logo;
            public String telephone;
            public String address;
            public String latitude;
            public String longtitude;
            public String distance;
            public String introduction;
            public String opentime;
            public String shareurl;
        }



        public Level level;

        public class Level
        {
            public String goldCount = "0";
            public String voucherCount = "";
        }


    }


    [Serializable]
    public class Account
    {
        public string username;
        public string password;
        public string clientid;
        public string platform;
        public string version;
        public string systemversion;
        public string unionid;
        public string code;
        public Account(string username, string password,
      string clientid, string platform, string version, string systemversion,
      string unionid, string code)
        {
            this.username = username;
            this.password = password;
            this.clientid = clientid;
            this.platform = platform;
            this.version = version;
            this.systemversion = systemversion;
            this.unionid = unionid;

            this.code = code;
        }
       

    }


}
