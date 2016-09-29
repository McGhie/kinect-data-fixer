using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace KinnectDataToHexAndXYZ
{
    public class Program
    {

        
        static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Globals.Main_Form.ShowDialog();

            var filename = "file";


            //   File.WriteAllText("rgb.30 RAW.txt", File.ReadAllText("rgb.30 RAW.txt").Replace(",["+'"'+"805", "]'"));
            // RGBToHex("22.rgb.data.247k.txt");

            //Firstclean(filename);
            // xyzReg("xyz.30 RAW.txt");
            //  Xyz1000Int("xyz.30 RAW.txt");
            //Xyz1000("xyz.30 RAW.txt");


            //  NoLines("xyz.30 RAW.txt");

        }


        //remove not being used
    }

}
