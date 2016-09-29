using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace KinnectDataToHexAndXYZ
{
    public class Data
    {

        public string FileName { get; private set; }
        public int DecimalPlace { get; private set; }
        //public int ID { get; private set; }

        public Thread T { get; private set; }

        public Data(int ID, string FileName = "Data.txt", int DecimalPlace = 3)
        {
            //this.ID = ID;
            this.FileName = FileName;
            this.DecimalPlace = DecimalPlace;
            //Firstclean();
            T = new Thread(new ThreadStart(Firstclean));
            T.Start();
        }


        //,{"type":"IMPLIES","t1":{"type":"Occupy3DPointDouble","x":"0.0","y":"0.0","z":"0.0"},"t2":{"type":"ComponentState","state":"(-Infinity,-Infinity)"}}

        public void Firstclean()
        {
            Console.Title = "Firstclean";
            string filename = FileName;
            Console.WriteLine("splitting file into two....");
            string input = File.ReadAllText(filename);
            var rgb = "lor";
            input = input.Replace(rgb, "Q");
            var xyzOutput = input;
            var rgbOutput = input;

            if (input.Contains("Q"))
            {
                xyzOutput = xyzOutput.Remove(input.LastIndexOf("Q") - 1);
                rgbOutput = rgbOutput.Substring(input.IndexOf('Q') + 1);

            }



            File.WriteAllText("xyz." + filename, xyzOutput);
            File.WriteAllText("rgb." + filename, rgbOutput);


            var filenamergb = "rgb." + filename;

            Console.WriteLine("removing garbage from xyz");
            filename = "xyz." + filename;

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



            File.WriteAllText(filename, File.ReadAllText(filename).Replace(old.ToString(), rep));
            File.WriteAllText(filename, File.ReadAllText(filename).Replace(old2, rep2));
            File.WriteAllText(filename, File.ReadAllText(filename).Replace(old4, rep4));
            File.WriteAllText(filename, File.ReadAllText(filename).Replace(old5, rep5));


            File.WriteAllText(filename, File.ReadAllText(filename).Replace(",x:", ""));
            File.WriteAllText(filename, File.ReadAllText(filename).Replace(oldy, repy));
            File.WriteAllText(filename, File.ReadAllText(filename).Replace(oldz, repz));

            Console.WriteLine("Finish first stage");

            Xyz(filename);

            Console.WriteLine("Finish XYZ stage");


            Console.WriteLine("Converting 128 bit to 256 bit");
            OneTwo8ToTwo56(filenamergb);
            Console.WriteLine("Finish 128 to 256 stage");

            Console.WriteLine("Converting 256 rgb to hex");
            RGBToHex(filenamergb);
            Console.WriteLine("Finish hex stage");

        }


        public void OneTwo8ToTwo56(string filename)
        {

            Console.Title = "128 TO 256";
            var split = ',';



            var aa = '"';
            var a = "";

            File.WriteAllText(filename, File.ReadAllText(filename).Replace(aa.ToString(), a));


            var cc = "{type:ComponentState,state:";
            var c = "C:";

            File.WriteAllText(filename, File.ReadAllText(filename).Replace(cc, c));


            //first replace all n.0 with n

            var nn = ".0";
            var n = "";

            File.WriteAllText(filename, File.ReadAllText(filename).Replace(nn, n));

            Console.WriteLine("removed all .0");

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
                Console.WriteLine("\nold: " + old + " new: " + rep);

                File.WriteAllText(filename, File.ReadAllText(filename).Replace(old, rep));
                File.WriteAllText(filename, File.ReadAllText(filename).Replace(old2, rep2));
                old = null;
                rep = null;

                i--;
            }
            // Console.WriteLine(lines);





        }

        public void RGBToHex(string filename)
        {
            Console.Title = "RGB TO HEX";
            //C:(26,128,24)},  ==> {"C":"129,141,137"},

            var input = File.ReadAllText(filename);

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
            File.WriteAllText(filename, File.ReadAllText(filename).Replace(input, newstr));



            //remove any starting text crap

            input = File.ReadAllText(filename);

            if (input.Contains("["))
            {
                input = input.Substring(input.IndexOf('[') - 1);

            }
            input = input.Replace("[", "rgb='[{");
            input = input.Replace("}", "]");
            input = input.Replace("{" + '"' + "C" + '"' + ":", "[");
            input = input.Replace("{", "");
            File.WriteAllText(filename, input);
            File.WriteAllText("HEX" + filename, input);

            /*
                        File.WriteAllText(filename, File.ReadAllText(filename).Replace(input, newstr));

                        File.WriteAllText(filename, input.Replace("[", "rgb='[{"));

                        File.WriteAllText(filename, input.Replace("}", "]"));

                        File.WriteAllText(filename, input.Replace("{"+'"'+"C"+'"'+":", "["));
                        */

        }


        public void xyzReg(string filename)
        {

            string input = File.ReadAllText(filename);

            //   var newstr = decimal.Round(yourValue, 3 MidpointRounding.AwayFromZero);

            var newstr = Regex.Replace(input, @"\\D\\d*", m => m.ToString().Remove(7));
            File.WriteAllText(filename, File.ReadAllText(filename).Replace(input, newstr));

        }
        //change this to xyz(string filename, int DecimalPlace)
        public void Xyz(string filename)
        {
            Console.Title = "XYZ";
            Console.WriteLine(filename);

            string input = File.ReadAllText(filename);

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

                    Console.WriteLine($"[{newstr}] [{xyz}]");

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
                    //   File.WriteAllText("3" + filename, output.ToString());
                    Console.WriteLine(i++);
                    // input = input.Replace(m.ToString(), m.ToString().Remove(7));
                }

                catch (Exception)
                {

                }


            }
            output.Remove(output.Length - 1, 1).Append("]");
            File.WriteAllText(filename, output.ToString());
            File.WriteAllText(filename, File.ReadAllText(filename).Replace("\n", ""));
        }


        void NoLines(string filename)
        {
            File.WriteAllText(filename, File.ReadAllText(filename).Replace("\n", ""));

        }



    }
}
