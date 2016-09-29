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
using System.Threading;

namespace KinnectDataToHexAndXYZ
{
    public class Data
    {

        public string FileName { get; private set; }
        public int DecimalPlace { get; private set; }
        public int ID { get; private set; }

        public Thread T { get; private set; }

        public Data(int ID, string FileName = "Data.txt", int DecimalPlace = 3)
        {
            this.ID = ID;
            this.FileName = FileName;
            this.DecimalPlace = DecimalPlace;
            //Firstclean();
            T = new Thread(new ThreadStart(Firstclean));
            T.Start();
        }

        //public static void Removergb(string FileName)
        //{

        //    var Colors = "color";
        //    File.WriteAllText("FileNameQ.txt", File.ReadAllText(FileName).Replace(Colors, "Q"));

        //    var data = File.ReadAllText("FileNameQ.txt");

        //    char[] delimiterChars = { 'Q' };

        //    string[] words = data.Split(delimiterChars);

        //    AppendOutput($"{words.Length} words in text:");
        //    var i = 0;
        //    foreach (string s in words)
        //    {
        //        File.WriteAllText("split" + i++ + FileName, s);

        //    }

        //    // Keep the console window open in debug mode.
        //    AppendOutput("Press any key to exit.");
        //    //Console.ReadKey();

        //    if (data.Contains('Q'))
        //        AppendOutput("found color");

        //    //   int index = data.IndexOf('Q');
        //    //   string result = data.Substring(0, index);

        //    //  File.WriteAllText("xyz"+FileName , result);


        //}

        //,{"type":"IMPLIES","t1":{"type":"Occupy3DPointDouble","x":"0.0","y":"0.0","z":"0.0"},"t2":{"type":"ComponentState","state":"(-Infinity,-Infinity)"}}

        private void AppendOutput(string data)
        {

            Globals.Main_Form.AppendOutput($"({ID}):{data}");

        }

        public void Firstclean()
        {

            AppendOutput("splitting file into two....");
            string input = File.ReadAllText(FileName);
            var rgb = "lor";
            input = input.Replace(rgb, "Q");
            var xyzOutput = input;
            var rgbOutput = input;

            if (input.Contains("Q"))
            {
                xyzOutput = xyzOutput.Remove(input.LastIndexOf("Q") - 1);
                rgbOutput = rgbOutput.Substring(input.IndexOf('Q') + 1);

            }



            File.WriteAllText("xyz." + FileName, xyzOutput);
            File.WriteAllText("rgb." + FileName, rgbOutput);


            var FileNamergb = "rgb." + FileName;

            AppendOutput("removing garbage from xyz");
            FileName = "xyz." + FileName;

            var old = '"';
            var rep = "";

            var old2 =
                "{type:IMPLIES,t1:{type:Occupy3DPointDouble,x:0.0,y:0.0,z:0.0},t2:{type:ComponentState,state:(-Infinity,-Infinity)}},";
            var rep2 = "";

            var oldx = "x:";
            var repx = "";

            var oldy = "y:";
            var repy = "";

            var oldz = "z:";
            var repz = "";

            var old4 = "},{type:IMPLIES,t1:{type:Occupy3DPointDouble";
            var rep4 = ",\n[";

            var old5 = "},t2:{type:ComponentState,state:";
            var rep5 = "], UV:";

            
            File.WriteAllText(FileName, File.ReadAllText(FileName).Replace(old.ToString(), rep));
            File.WriteAllText(FileName, File.ReadAllText(FileName).Replace(old2, rep2));
            File.WriteAllText(FileName, File.ReadAllText(FileName).Replace(old4, rep4));
            File.WriteAllText(FileName, File.ReadAllText(FileName).Replace(old5, rep5));


            File.WriteAllText(FileName, File.ReadAllText(FileName).Replace(",x:", ""));
            File.WriteAllText(FileName, File.ReadAllText(FileName).Replace(oldy, repy));
            File.WriteAllText(FileName, File.ReadAllText(FileName).Replace(oldz, repz));

            AppendOutput("Finish first stage");

            Xyz();

            AppendOutput("Finish XYZ stage");


            AppendOutput("Converting 128 bit to 256 bit");
            OneTwo8ToTwo56();
            AppendOutput("Finish 128 to 256 stage");

            AppendOutput("Converting 256 rgb to hex");
            RGBToHex();
            AppendOutput("Finish hex stage");

        }


        public void OneTwo8ToTwo56()
        {


            var split = ',';



            var aa = '"';
            var a = "";

            File.WriteAllText(FileName, File.ReadAllText(FileName).Replace(aa.ToString(), a));


            var cc = "{type:ComponentState,state:";
            var c = "C:";

            File.WriteAllText(FileName, File.ReadAllText(FileName).Replace(cc, c));


            //first replace all n.0 with n

            var nn = ".0";
            var n = "";

            File.WriteAllText(FileName, File.ReadAllText(FileName).Replace(nn, n));

            AppendOutput("removed all .0");

            int i = -1;
            int x = (256 / 2) * -1;
            while (i >= x)
            {


                int replace = i;
                var old = i.ToString() + ')';
                var old2 = i.ToString() + ",";
                var rep = "";
                var rep2 = "";

                replace = (i * -1) + 127;
                rep = replace.ToString() + ')';
                rep2 = replace.ToString() + ",";
                AppendOutput("\nold: " + old + " new: " + rep);

                File.WriteAllText(FileName, File.ReadAllText(FileName).Replace(old, rep));
                File.WriteAllText(FileName, File.ReadAllText(FileName).Replace(old2, rep2));
                old = null;
                rep = null;

                i--;
            }
            File.WriteAllText(FileName + "256bitColor", File.ReadAllText(FileName));
            // AppendOutput(lines);





        }

        public void RGBToHex()
        {
            //C:(26,128,24)},  ==> {"C":"129,141,137"},

            var input = File.ReadAllText(FileName);

            var newstr = Regex.Replace(
                     input,
                     @"C:\([ ]*(\d+)[ ]*,[ ]*(\d+)[ ]*,[ ]*(\d+)[ ]*\)}",
                     m =>
                     {
                         return "{" + '"' + "C" + '"' + ":" + '"' + Int32.Parse(m.Groups[1].Value).ToString("X2") +
                         Int32.Parse(m.Groups[2].Value).ToString("X2") +
                         Int32.Parse(m.Groups[3].Value).ToString("X2") + '"' + '}';
                     }

                 );
            File.WriteAllText(FileName, File.ReadAllText(FileName).Replace(input, newstr));



            //remove any starting text crap

            input = File.ReadAllText(FileName);

            if (input.Contains("["))
            {
                input = input.Substring(input.IndexOf('[') - 1);

            }
            input = input.Replace("[", "rgb='[{");
            input = input.Replace("}", "]");
            input = input.Replace("{" + '"' + "C" + '"' + ":", "[");
            input = input.Replace("{", "");
            File.WriteAllText(FileName, input);
            File.WriteAllText(FileName + "HEX", input);
            /*
                        File.WriteAllText(FileName, File.ReadAllText(FileName).Replace(input, newstr));

                        File.WriteAllText(FileName, input.Replace("[", "rgb='[{"));

                        File.WriteAllText(FileName, input.Replace("}", "]"));

                        File.WriteAllText(FileName, input.Replace("{"+'"'+"C"+'"'+":", "["));
                        */

        }


        public void xyzReg()
        {

            string input = File.ReadAllText(FileName);

            //   var newstr = decimal.Round(yourValue, 3 MidpointRounding.AwayFromZero);

            var newstr = Regex.Replace(input, @"\\D\\d*", m => m.ToString().Remove(7));
            File.WriteAllText(FileName, File.ReadAllText(FileName).Replace(input, newstr));

        }
        //change this to xyz(string FileName, int DecimalPlace)
        public  void Xyz()
        {

            AppendOutput(FileName);

            string input = File.ReadAllText(FileName);

            System.Text.StringBuilder output = new System.Text.StringBuilder();

            output.Append("xyz = [");


            string pat = "(-?\\d\\D\\d*)";

            var ii = 0;
            var i = 0;

            foreach (Match m in Regex.Matches(input, pat))
            {
                try
                {
                    var newstr = decimal.Parse(m.Value);
                    //change 3 to DecimalePlace
                    string xyz = decimal.Round(newstr, DecimalPlace, MidpointRounding.AwayFromZero).ToString();

                    AppendOutput($"[{newstr}] [{xyz}]");

                    if (ii == 0)
                    {
                        output.Append(" [" + xyz + ", ");
                        ii++;
                    }
                    else if (ii == 1)
                    {
                        output.Append(xyz + ", ");
                        ii++;
                    }
                    else if (ii == 2)

                    {
                        output.Append(xyz + "],");
                        ii++;
                    }

                    else if (ii == 3)
                    {
                        ii++;
                    }
                    else if (ii == 4)
                    {
                        ii = 0;
                    }
                    //   File.WriteAllText("3" + FileName, output.ToString());
                    AppendOutput(i++.ToString());
                    // input = input.Replace(m.ToString(), m.ToString().Remove(7));
                }

                catch (Exception)
                {

                }


            }
            output.Remove(output.Length - 1, 1).Append("]");
            File.WriteAllText(FileName, output.ToString());
            File.WriteAllText(FileName, File.ReadAllText(FileName).Replace("\n", ""));
        }


         void NoLines(string FileName)
        {
            File.WriteAllText(FileName, File.ReadAllText(FileName).Replace("\n", ""));

        }



    }
}
