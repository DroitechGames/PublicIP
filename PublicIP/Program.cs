using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;

namespace PublicIP
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(600);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            //Console.WindowWidth = 60;
           // Console.WindowHeight = 30;
            Console.ForegroundColor = ConsoleColor.Black;
            
            Thread.Sleep(200);
            Console.Clear();
            Thread.Sleep(200);
            Console.WriteLine("HTTP PUBLIC IP PARSER. " + Environment.NewLine+ "Copyright © 2019 MASTER CASSIDY." + Environment.NewLine);
            Thread.Sleep(200);
            if (args.Count() < 1)
            {
                Thread.Sleep(200);
                Console.WriteLine("Error, missing command parameter.");
                Thread.Sleep(200);
                Console.WriteLine("To use this applet, you must insert the switch '-public'. " +Environment.NewLine + "This will obtain your Public IP in a parsable string.");
                Thread.Sleep(200);
                Console.WriteLine("I will now exit.");
                Thread.Sleep(200);
                //Console.Read();
            }
            else
            {
                if (args[0].Contains("-public"))
                {
                    Thread.Sleep(200);
                    Console.WriteLine("Your Public IP Address Is:");
                    Thread.Sleep(200);
                    Console.WriteLine("----------------------------------------------");
                    //Console.WriteLine(IPV4.GetPublicAddress());
                    //Console.Read();
                    Thread.Sleep(200);
                    IPV4.StorePublicIP(IPV4.GetPublicAddress(),"PublicIP.ini");
                    Thread.Sleep(200);
                    Console.WriteLine("----------------------------------------------");
                    Thread.Sleep(200);
                    //Console.Read();
                    Thread.Sleep(1250);
                }
            }
        }
    }
    public class IPV4
    {
        internal static string _ReturnedPublicIP = null;
        internal static string _IniFilename = "PublicIP.ini";
        //
        internal static string _IniNetworkSection = "[Network.PublicIP]";
        internal static string _IniNetworkSectionIPV4 = "$IPV4=";
        internal static string _iniNetworkSectionIP = "[$IP=]";
        //
        internal static void StorePublicIP(string _IP, string _FILE)
        {
            // read the file for the elements we need to edit.
            string _INF_IP_LINE;
            string _IPHEADER;
            string _IPV4;
            string newIP;
            //FileInfo _FileInfo = new FileInfo(_FILE);
            File.WriteAllText(_FILE, _IniNetworkSection + Environment.NewLine + _IniNetworkSectionIPV4 + _iniNetworkSectionIP);
            StreamReader _Reader = new StreamReader(_FILE);
            Console.Write("Detected IP: ");
            Thread.Sleep(50);
           
                while ((_INF_IP_LINE = _Reader.ReadLine()) != null)
                {

                    if (_INF_IP_LINE.Contains(_iniNetworkSectionIP))
                    {
                        _IPHEADER = _IniNetworkSection;
                        _IPV4 = _IniNetworkSectionIPV4;
                        // swap the IP for the ACTUAL IP
                        // edit the lines and replace the data we have now retrieved from the web.
                        newIP = _iniNetworkSectionIP.Replace(_iniNetworkSectionIP, _IP.ToString());
                        _iniNetworkSectionIP = newIP;
                        // Console.ResetColor();
                        Thread.Sleep(50);
                        Console.WriteLine(newIP);
                        break;
                    }

                 }
            Thread.Sleep(150);
            _Reader.Close();
            Thread.Sleep(50);
            File.WriteAllText(_FILE, _IniNetworkSection + Environment.NewLine +_IniNetworkSectionIPV4 + _iniNetworkSectionIP);

            // release anything in stored in memory..
          
        }
        public static string GetPublicAddress()
        {
            String _IPV4 = "";
            
            WebRequest _HTTP_REQUEST_DYNIP = WebRequest.Create("http://checkip.dyndns.org/");

            try
            {
                using (WebResponse _HTTP_RESPONCE_DYNIP = _HTTP_REQUEST_DYNIP.GetResponse())
                {
                    using (StreamReader _HTTP_RESPONCE_DATA = new StreamReader(_HTTP_RESPONCE_DYNIP.GetResponseStream()))
                    {
                        _IPV4 = _HTTP_RESPONCE_DATA.ReadToEnd();
                    }
                    int first = _IPV4.IndexOf("Address: ") + 9;
                    int last = _IPV4.LastIndexOf("</body>");
                    _IPV4 = _IPV4.Substring(first, last - first);
                    _ReturnedPublicIP = _IPV4.ToString();
                }
               
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return _IPV4;

        }
    }
}
