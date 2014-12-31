using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using UPGL;
using UPTE;
using System.Diagnostics;// need these 3 lines to set the console window on top.
using System.Runtime.InteropServices;
using System.Threading;



namespace ModelCapabilities
{
    class Local_Functions
    {

        /// <summary>
        /// This function gets a model number from user and display what firmware it is in. One model can be on different firmwares.
        /// </summary>
        /// <param name="sUserInputModel"></param>
        /// <param name="allmodels_inFW"></param>
        /// <param name="Firmwaretype"></param>
        public void Find_WhatFw_a_model_belongs_to(string sUserInputModel, List<string> allmodels_inFW, string Firmwaretype)
        {

            foreach (string model in allmodels_inFW)
            {
                if (model.Contains(sUserInputModel))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n\n*****Model " + sUserInputModel + " is in: " + Firmwaretype);
                    Console.ForegroundColor = ConsoleColor.Gray;

                }
            }

        }

        /// <summary>
        /// This function asks user to enter a model capability and a firmware number, it return the model(s) that have this capability in the chosen firmware.
        /// If the model capability is not in the firmware user chooses, it will let user know.
        /// </summary>
        /// <param name="usercapability"></param>
        /// <param name="modelFw"></param>
        public void Find_A_Capability_In_A_Model(string usercapability, string sUserFirmware, Dictionary<string, string> Model_Capability)
        {
            int count = 0;
            string sModel = string.Empty;
            Console.WriteLine("\n\n*********Capability: " + usercapability + " is in model(s): \n\n");

            foreach (KeyValuePair<string, string> item in Model_Capability)
            {

                if (item.Key.Contains(sUserFirmware) && (item.Value.Contains(usercapability)))
                {
                    sModel = Regex.Replace(item.Key, @"_0x.*", "");

                    if (sModel.Contains("-00"))
                    {
                        sModel = sModel.Replace("-00", string.Empty);// for low voltage model, remove the "-00"
                    }
                    else if (sModel.Contains("-"))
                    {
                        sModel = sModel.Replace("-", string.Empty);// for high voltage model, remove the "-"

                    }
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(sModel);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    count = 1;
                }

            }



            if (count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n\n*********Capability: " + usercapability + " is not in any model in firmware: " + sUserFirmware);
                Console.ForegroundColor = ConsoleColor.Gray;
            }

        }

        /// <summary>
        /// This function lists model(s) in any firmwares that has the capability that user chooses. It list the model and the firmware extension
        /// since a model can be in multiple firmwares. It will let user know if the capability is not found in any model in any firmware
        /// </summary>
        /// <param name="usercapability"></param>
        /// <param name="Model_Capability"></param>
        public void Find_A_Capability_In_A_Model(string usercapability, Dictionary<string, string> Model_Capability)
        {
            int count = 0;
            string sModel = string.Empty;

            foreach (KeyValuePair<string, string> item in Model_Capability)
            {


                if (item.Value.Contains(usercapability))
                {
                    sModel = item.Key;// keep all the firmware and schema info of the model since a model can be on different firmware      
                    sModel = Strip_Schema_Version_After_Model_Number(sModel);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(sModel);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    count = 1;
                }

            }



            if (count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n\n*********Capability: " + usercapability + " is not in any model in any model in any firmware. You may have typo in " + usercapability);
                Console.ForegroundColor = ConsoleColor.Gray;
            }

        }

        /// <summary>
        /// This function list model(s) that have 2 capabilities  in a firmware that user choose.
        /// If there is no model that has all 2 capabilities, it will let user know.
        /// </summary>
        /// <param name="usercapability1"></param>
        /// <param name="usercapability2"></param>
        /// <param name="sUserFirmware"></param>
        /// <param name="Model_Capability"></param>
        public void Find_Capabilities_In_A_Model(string sUserCapability1, string sUserCapability2, string sUserFirmware, Dictionary<string, string> Model_Capability)
        {
            int count = 0;
            string sModel = string.Empty;
            Console.WriteLine("\n\n***** The Capabilities: " + sUserCapability1 + ", " + sUserCapability2 + ", " + "are in model(s): \n\n");

            foreach (KeyValuePair<string, string> item in Model_Capability)
            {

                if (item.Key.Contains(sUserFirmware) && (item.Value.Contains(sUserCapability1)) && (item.Value.Contains(sUserCapability2)))
                    
                {
                    sModel = Regex.Replace(item.Key, @"_0x.*", "");

                    if (sModel.Contains("-00"))
                    {
                        sModel = sModel.Replace("-00", string.Empty);// for low voltage model, remove the "-00"
                    }
                    else if (sModel.Contains("-"))
                    {
                        sModel = sModel.Replace("-", string.Empty);// for high voltage model, remove the "-"

                    }
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(sModel);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    count = 1;
                }

            }



            if (count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n\n***** The Capabilities: " + sUserCapability1 + ", " + sUserCapability2 + ", " + " are not in any model in firmware: " + sUserFirmware);
                Console.ForegroundColor = ConsoleColor.Gray;
            }

        }

        /// <summary>
        /// This function displays any model(s) in any firmware that has 2 capabilities that user chooses
        /// </summary>
        /// <param name="sUserCapability1"></param>
        /// <param name="sUserCapability2"></param>
        /// <param name="Model_Capability"></param>
        public void Find_Capabilities_In_A_Model(string sUserCapability1, string sUserCapability2, Dictionary<string, string> Model_Capability)
        {
            int count = 0;
            string sModel = string.Empty;
            Console.WriteLine("\n\n***** The Capabilities: " + sUserCapability1 + ", " + sUserCapability2 + ", " +  "are in model(s): \n\n");

            foreach (KeyValuePair<string, string> item in Model_Capability)
            {

                if ((item.Value.Contains(sUserCapability1)) && (item.Value.Contains(sUserCapability2)))
                {
                    sModel = Regex.Replace(item.Key, @"_0x.*", "");

                    if (sModel.Contains("-00"))
                    {
                        sModel = sModel.Replace("-00", string.Empty);// for low voltage model, remove the "-00"
                    }
                    else if (sModel.Contains("-"))
                    {
                        sModel = sModel.Replace("-", string.Empty);// for high voltage model, remove the "-"

                    }
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(sModel);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    count = 1;
                }

            }

            if (count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n\n***** The Capabilities: " + sUserCapability1 + ", " + sUserCapability2 + ", " + "are not in any model in any firmware: ");
                Console.ForegroundColor = ConsoleColor.Gray;
            }

        }


        /// <summary>
        /// This function list model(s) that have 3 capabilities  in a firmware that user choose.
        /// If there is no model that has all 3 capabilities, it will let user know.
        /// </summary>
        /// <param name="usercapability1"></param>
        /// <param name="usercapability2"></param>
        /// <param name="usercapability3"></param>
        /// <param name="sUserFirmware"></param>
        /// <param name="Model_Capability"></param>
        public void Find_Capabilities_In_A_Model(string sUserCapability1, string sUserCapability2, string sUserCapability3, string sUserFirmware, Dictionary<string, string> Model_Capability)
        {
            int count = 0;
            string sModel = string.Empty;
            Console.WriteLine("\n\n***** The Capabilities: " + sUserCapability1 + ", " + sUserCapability2 + ", " + sUserCapability3 + " are in model(s): \n\n");

            foreach (KeyValuePair<string, string> item in Model_Capability)
            {

                if (item.Key.Contains(sUserFirmware) && (item.Value.Contains(sUserCapability1)) && (item.Value.Contains(sUserCapability2))
                    && (item.Value.Contains(sUserCapability3)))
                {
                    sModel = Regex.Replace(item.Key, @"_0x.*", "");

                    if (sModel.Contains("-00"))
                    {
                        sModel = sModel.Replace("-00", string.Empty);// for low voltage model, remove the "-00"
                    }
                    else if (sModel.Contains("-"))
                    {
                        sModel = sModel.Replace("-", string.Empty);// for high voltage model, remove the "-"

                    }
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(sModel);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    count = 1;
                }

            }



            if (count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n\n***** The Capabilities: " + sUserCapability1 + ", " + sUserCapability2 + ", " + sUserCapability3 + " are not in any model in firmware: " + sUserFirmware);
                Console.ForegroundColor = ConsoleColor.Gray;
            }

        }

        /// <summary>
        /// This function displays any model(s) in any firmware that has 2 capabilities that user chooses
        /// </summary>
        /// <param name="sUserCapability1"></param>
        /// <param name="sUserCapability2"></param>
        /// <param name="Model_Capability"></param>
        public void Find_Three_Capabilities_In_A_Model(string sUserCapability1, string sUserCapability2, string sUserCapability3, Dictionary<string, string> Model_Capability)
        {
            int count = 0;
            string sModel = string.Empty;
            Console.WriteLine("\n\n***** The Capabilities: " + sUserCapability1 + ", " + sUserCapability2 + ", " + sUserCapability3 + ", " + "are in model(s): \n\n");

            foreach (KeyValuePair<string, string> item in Model_Capability)
            {

                if ((item.Value.Contains(sUserCapability1)) && (item.Value.Contains(sUserCapability2)) && (item.Value.Contains(sUserCapability3)))
                {
                    sModel = Regex.Replace(item.Key, @"_0x.*", "");

                    if (sModel.Contains("-00"))
                    {
                        sModel = sModel.Replace("-00", string.Empty);// for low voltage model, remove the "-00"
                    }
                    else if (sModel.Contains("-"))
                    {
                        sModel = sModel.Replace("-", string.Empty);// for high voltage model, remove the "-"

                    }
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(sModel);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    count = 1;
                }

            }

            if (count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n\n***** The Capabilities: " + sUserCapability1 + ", " + sUserCapability2 + ", " + sUserCapability3 + "are not in any model in any firmware: ");
                Console.ForegroundColor = ConsoleColor.Gray;
            }

        }


        /// <summary>
        /// This function asks user to pick the firmware
        /// </summary>
        /// <returns></returns>
        public string Ask_For_Which_FW_User_Want()
        {
            string sMessage = string.Empty;

            Console.WriteLine();
            Console.Write("A: Firmware 15 High Voltage models only          ");
            Console.WriteLine("H: Firmware B  Low Voltage models only       ");
            Console.Write("B: Firmware 14 Low Voltage models only           ");
            Console.WriteLine("I: Firmware A  High Voltage models only       ");
            Console.Write("C: Firmware 13 High Voltage models only          ");
            Console.WriteLine("J: Firmware E  High Voltage models only       ");
            Console.Write("D: Firmware 12 High Voltage models only          ");
            Console.WriteLine("K: Firmware 9  High Voltage models only          ");
            Console.Write("E: Firmware 11 High Voltage models only          ");
            Console.WriteLine("L: Firmware 8  High Voltage models only       ");
            Console.Write("F: Firmware 10 Low Voltage models only           ");
            Console.WriteLine("M: Firmware 7  High Voltage models only          ");
            Console.Write("G: Firmware C  High Voltage models only          ");
            Console.WriteLine("N: All the Firmwares");
            Console.Write("\n\n***** Pick a letter for the firmware: ");

            string sUserFirmware = Console.ReadLine();
            sUserFirmware = sUserFirmware.ToUpper();
            switch (sUserFirmware)
            {
                case "A": sUserFirmware = "_0x1502";
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n\n******You picked firmware 15");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case "B": sUserFirmware = "_0x1407";
                    sMessage = "\n\n******You picked firmware 14";
                    Display_User_Choice_In_Color(sMessage);
                    break;
                case "C": sUserFirmware = "_0x130c";
                    sMessage = "\n\n******You picked firmware 13";
                    Display_User_Choice_In_Color(sMessage);
                    break;
                case "D": sUserFirmware = "_0x120e";
                    sMessage = "\n\n******You picked firmware 12";
                    Display_User_Choice_In_Color(sMessage);
                    break;
                case "E": sUserFirmware = "_0x110e";
                    sMessage = "\n\n******You picked firmware 11";
                    Display_User_Choice_In_Color(sMessage);
                    break;
                case "F": sUserFirmware = "_0x1008";
                    sMessage = "\n\n******You picked firmware 10";
                    Display_User_Choice_In_Color(sMessage);
                    break;
                case "G": sUserFirmware = "_0xc0e";
                    sMessage = "\n\n******You picked firmware C";
                    Display_User_Choice_In_Color(sMessage);
                    break;
                case "H": sUserFirmware = "_0xb0e";
                    sMessage = "\n\n******You picked firmware B";
                    Display_User_Choice_In_Color(sMessage);
                    break;
                case "I": sUserFirmware = "_0xa0b";
                    sMessage = "\n\n******You picked firmware A";
                    Display_User_Choice_In_Color(sMessage);
                    break;
                case "J": sUserFirmware = "_0xe0";
                    sMessage = "\n\n******You picked firmware E";
                    Display_User_Choice_In_Color(sMessage);
                    break;
                case "K": sUserFirmware = "_0x90b";
                    sMessage = "\n\n******You picked firmware 9";
                    Display_User_Choice_In_Color(sMessage);
                    break;
                case "L": sUserFirmware = "_0x80b";
                    sMessage = "\n\n******You picked firmware 8";
                    Display_User_Choice_In_Color(sMessage);
                    break;
                case "M": sUserFirmware = "_0x700";
                    sMessage = "\n\n******You picked firmware 7";
                    Display_User_Choice_In_Color(sMessage);
                    break;
                case "N": sUserFirmware = "All Firmwares";
                    sMessage = "\n\n******You picked 'All Firmwares'";
                    Display_User_Choice_In_Color(sMessage);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You picked: " + sUserFirmware + ". It is an invalid Choice. Try again.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;

            }
            return sUserFirmware;

        }

        /// <summary>
        /// This function asks user to pick a device_config file; either us or .ous. Some models and capabilities are in .ous and not in us and vice versa.
        /// </summary>
        /// <returns></returns>
        public string   Ask_User_To_Pick_FileName()
        {
            Console.WriteLine("\n***** Pick the 'us' or 'ous', or 'jpn' device_config file.\n");
            Console.WriteLine("A: device_config.us");
            Console.WriteLine("B: device_config.ous");
            Console.WriteLine("C: device_config.jpn");
            Console.Write("\n***** Pick a letter: ");
            string sFilename = Console.ReadLine();
            sFilename = sFilename.ToUpper();
            Console.WriteLine("\n***** You picked: " + sFilename);

            return sFilename;

        }

        /// <summary>
        /// This funtion display the user choice in yellow color on command window.
        /// </summary>
        /// <param name="message"></param>
        public void Display_User_Choice_In_Color(string sMessage)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(sMessage);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// This function display all models in a firmware.
        /// </summary>
        /// <param name="sUserFirmware"></param>
        /// <param name="allF15models"></param>
        /// <param name="allF14models"></param>
        /// <param name="allF13models"></param>
        /// <param name="allF12models"></param>
        /// <param name="allF11models"></param>
        /// <param name="allF10models"></param>
        /// <param name="allF1Cmodels"></param>
        /// <param name="allFBmodels"></param>
        /// <param name="allFAmodels"></param>
        /// <param name="allF1Emodels"></param>
        /// <param name="allF9models"></param>
        /// <param name="allF8models"></param>
        /// <param name="allF7models"></param>
        public void Display_All_Models_In_A_Firmware(string sUserFirmware, List<string> allF15models, List<string> allF14models, List<string> allF13models, List<string> allF12models, List<string> allF11models, List<string> allF10models, List<string> allFEmodels, List<string> allFCmodels, List<string> allFBmodels, List<string> allFAmodels, List<string> allF9models, List<string> allF8models, List<string> allF7models, List<string> allmodels)
        {
            string message = "\n\n***** Below is a list of all the models:\n ";
            string sModelOnly = string.Empty;

            switch (sUserFirmware)
            {
                case "_0x1502":
                    Display_User_Choice_In_Color(message);

                    foreach (string sModel in allF15models)
                    {
                        sModelOnly = Strip_All_Characters_After_Model_Number(sModel);

                        Console.WriteLine(sModelOnly);
                    }
                    break;

                case "_0x1407":
                    Display_User_Choice_In_Color(message);

                    foreach (string sModel in allF14models)
                    {
                        sModelOnly = Strip_All_Characters_After_Model_Number(sModel);

                        Console.WriteLine(sModelOnly);
                    }
                    break;

                case "_0x130c":
                    Display_User_Choice_In_Color(message);

                    foreach (string sModel in allF13models)
                    {
                        sModelOnly = Strip_All_Characters_After_Model_Number(sModel);

                        Console.WriteLine(sModelOnly);
                    }
                    break;

                case "_0x120e":
                    Display_User_Choice_In_Color(message);

                    foreach (string sModel in allF12models)
                    {
                        sModelOnly = Strip_All_Characters_After_Model_Number(sModel);

                        Console.WriteLine(sModelOnly);
                    }
                    break;

                case "_0x110e":
                    Display_User_Choice_In_Color(message);

                    foreach (string sModel in allF11models)
                    {
                        sModelOnly = Strip_All_Characters_After_Model_Number(sModel);

                        Console.WriteLine(sModelOnly);
                    }
                    break;

                case "_0xc0e":
                    Display_User_Choice_In_Color(message);

                    foreach (string sModel in allFCmodels)
                    {
                        sModelOnly = Strip_All_Characters_After_Model_Number(sModel);

                        Console.WriteLine(sModelOnly);
                    }
                    break;


                case "_0xb0e":
                    Display_User_Choice_In_Color(message);

                    foreach (string sModel in allFBmodels)
                    {
                        sModelOnly = Strip_All_Characters_After_Model_Number(sModel);

                        Console.WriteLine(sModelOnly);
                    }
                    break;

                case "_0xa0b":
                    Display_User_Choice_In_Color(message);

                    foreach (string sModel in allFAmodels)
                    {
                        sModelOnly = Strip_All_Characters_After_Model_Number(sModel);

                        Console.WriteLine(sModelOnly);
                    }
                    break;

                case "_0x1008":
                    Display_User_Choice_In_Color(message);

                    foreach (string sModel in allF10models)
                    {
                        sModelOnly = Strip_All_Characters_After_Model_Number(sModel);
                        Console.WriteLine(sModelOnly);
                    }
                    break;

                case "_0xe0":
                    Display_User_Choice_In_Color(message);

                    foreach (string sModel in allFEmodels)
                    {
                        sModelOnly = Strip_All_Characters_After_Model_Number(sModel);
                        Console.WriteLine(sModelOnly);
                    }
                    break;

                case "_0x90b":
                    Display_User_Choice_In_Color(message);

                    foreach (string sModel in allF9models)
                    {
                        sModelOnly = Strip_All_Characters_After_Model_Number(sModel);
                        Console.WriteLine(sModelOnly);
                    }
                    break;

                case "_0x80b":
                    Display_User_Choice_In_Color(message);

                    foreach (string sModel in allF8models)
                    {
                        sModelOnly = Strip_All_Characters_After_Model_Number(sModel);
                        Console.WriteLine(sModelOnly);
                    }
                    break;

                case "_0x700":
                    Display_User_Choice_In_Color(message);

                    foreach (string sModel in allF7models)
                    {
                        sModelOnly = Strip_All_Characters_After_Model_Number(sModel);
                        Console.WriteLine(sModelOnly);
                    }
                    break;
                case "All Firmwares":
                    Display_User_Choice_In_Color(message);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n***** This will be a very long list, since some models are in several firmwares.");
                    Console.WriteLine("\n***** The model and its firmware are displayed in the following format: 'modelnumber_Firmware'.");
                    Console.WriteLine("\n***** Ex: 3269-40_0x120e");
                    Console.WriteLine("\n***** The '_0x120e' after the model number is the firmware version.\n");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    foreach (string sModel in allmodels)
                    {
                        string sModelFirmware = string.Empty;
                        sModelFirmware = Strip_Schema_Version_After_Model_Number(sModel);
                        Console.WriteLine(sModelFirmware);
                    }
                    break;

            }

        }

        /// <summary>
        /// This function strip all the characters after the models. For high voltage models, it will display: 326940
        /// For low voltage model it will display 3620. For Single Atrial Chamber, it will display: 1160A
        /// </summary>
        /// <param name="sModel"></param>
        /// <returns></returns>
        public string Strip_All_Characters_After_Model_Number(string sModel)
        {
            // Get only the model number
            string sModelOnly = Regex.Replace(sModel, @"_0x.*", "");
            if (sModelOnly.Contains("-00"))
            {
                sModelOnly = sModelOnly.Replace("-00", string.Empty);
            }
            else if (sModelOnly.Contains("-"))
            {
                sModelOnly = sModelOnly.Replace("-", string.Empty);// for high voltage model, remove the "-"

            }
            return sModelOnly;
        }

        /// <summary>
        /// This function strip all the characters after the models. For high voltage models, it will display: 326940
        /// For low voltage model it will display 3620. For Single Atrial Chamber, it will display: 1160A
        /// </summary>
        /// <param name="sModel"></param>
        /// <returns></returns>
        public string Strip_Schema_Version_After_Model_Number(string sModel)
        {
            // Get model number and firmware version
            //Rmove the last 5 characters which are the schema version
            string sModelFirware = sModel.Remove(sModel.Length - 5);

            if (sModelFirware.Contains("-00"))
            {
                sModelFirware = sModelFirware.Replace("-00", string.Empty);
            }
            else if (sModelFirware.Contains("-"))
            {
                sModelFirware = sModelFirware.Replace("-", string.Empty);// for high voltage model, remove the "-"

            }
            return sModelFirware;
        }

        /// <summary>
        /// This function retunr the latest file in a directory
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public static FileInfo GetNewestFile(DirectoryInfo directory)
        {
            return directory.GetFiles()
                .Union(directory.GetDirectories().Select(d => GetNewestFile(d)))
                .OrderByDescending(f => (f == null ? DateTime.MinValue : f.LastWriteTime))
                .FirstOrDefault();
        }

        /// <summary>
        /// This folder returns the lastest subdirectory in a directory
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public static DirectoryInfo GetNewestDir(DirectoryInfo directory)
        {
            return directory.GetDirectories()
                .Union(directory.GetDirectories().Select(d => GetNewestDir(d)))
                .OrderByDescending(f => (f == null ? DateTime.MinValue : f.LastWriteTime))
                .FirstOrDefault();
        }

        /// <summary>
        /// This function finds the latest IR folder in the network 
        /// and copy the Zip file from the latest IR to C:\DeviceConfig folder. Then extract only the device_config files
        /// from the Zip file to C:\DeviceConfig folder

        /// </summary>
        /// <param name="path"></param>
        /// <param name="sExtractLocation"></param>
        public void Get_Latest_Dir_From_Network_And_Copy_To_C_Drive(string path, string sExtractLocation)
        {

            //Find latest directory on net work
            DirectoryInfo newestDir = GetNewestDir(new DirectoryInfo(path));
            string sDirToExtract = newestDir.Parent.Name;

            //FileInfo newestFile = GetNewestFile(new DirectoryInfo(path));// to find latest file. It works
            #region Copy latest Zipfile from the network to the local drive and unzip the device_config files
            LoggerLib.Singleton.Initialize(new MockLogger(), null);// need this for the extract function to work.

            string source = path + @"\" + sDirToExtract + @"\" + sDirToExtract + ".zip";
            string destination = @"C:\DeviceConfig\" + sDirToExtract + ".zip";

            string[] localFileNamesAndPath = null;
            string destDirectory = Path.GetDirectoryName(destination);

            //If file exist in the source folder and not in the destination folder then copy the file other wise skip to next command
            if (File.Exists(source) && !File.Exists(destination))
            {
                Console.WriteLine("\n***** Copying " + source + " file");
                Console.WriteLine("\n*****  to " + destination + " folder");
                Console.WriteLine("\n***** Copying the Zip file, it will take several minutes.");
                Console.WriteLine("\n***** Please close any device_config files that are open.");

                File.Copy(source, destination);

                if (File.Exists(destination))
                {
                    Console.WriteLine("\n***** Successfully copied " + source + " to your " + sExtractLocation + " folder.");
                }
                else
                {
                    Console.WriteLine("\n***** Fail to copy " + source + " to your " + sExtractLocation + " folder.");
                }
            }
            else
            {

                Console.WriteLine("\n***** " + sDirToExtract + ".zip already exists in " + sExtractLocation + " folder.");
            }

            // extract device_config.us, device_config.ous, and device_config.jpn, and device_config.jpn.net to C:\DeviceConfig folder.
            Console.WriteLine("\n***** Extracting device_config files to " + sExtractLocation + " folder.");
            Console.WriteLine("\n***** Please close any device_config files that are open.");
            Console.WriteLine("\n***** It will overwrite existing device_config files.");
            eResult isExtracted = Upgl.UtilityLib.ExtractFileFromZipFile(destination, sExtractLocation, "device_config", out localFileNamesAndPath);

            if (isExtracted == eResult.SUCCESS)
            {
                Console.WriteLine("\n***** Successfully extract device_config files to your " + sExtractLocation + " folder");
            }
            else
            {
                Console.WriteLine("\n***** Fail to extract the device_config files to " + sExtractLocation + " folder.");
                Console.WriteLine("\n***** The zip file probably does not contain the device_config files");
                Console.WriteLine("\n***** Hit any key to exit program.");
                Console.ReadLine();
                Environment.Exit(0);

            }

            #endregion


        }

        /// <summary>
        /// This function copy the Zip file from the location that user choose, either network or local drive to C:\DeviceConfig folder
        /// </summary>
        /// <param name="path"></param>
        /// <param name="sExtractLocation"></param>
        /// <param name="sDirToExtract"></param>
        public void Get_Dir_From_Network_Or_Local_And_Copy_To_C_Drive(string source, string sExtractLocation, string sZipFileName)
        {

            #region Copy latest Zipfile from the network or local drive to C:\DeviceConfig folder and unzip the device_config files
            LoggerLib.Singleton.Initialize(new MockLogger(), null);// need this for the extract function to work.


            string sDestination = sExtractLocation + @"\" + sZipFileName;
            string[] localFileNamesAndPath = null;
            string destDirectory = Path.GetDirectoryName(sDestination);

            //If file exist in the source folder and not in the destination folder then copy the file other wise skip to next command
            if (File.Exists(source) && !File.Exists(sDestination))//&& !File.Exists(destination)
            {
                Console.WriteLine("\n***** Copying " + source + " file");
                Console.WriteLine("\n***** to " + sExtractLocation + " folder");
                Console.WriteLine("\n***** Copying the Zip file, it will take several minutes.");
                Console.WriteLine("\n***** You will be notified when it is done.");
                File.Copy(source, sDestination);

                if (File.Exists(sDestination))
                {
                    Console.WriteLine("\n***** Successfully copied " + source + " to your " + sExtractLocation + " folder.");
                }
                else
                {
                    Console.WriteLine("\n***** Fail to copy " + source + " to your " + sExtractLocation + " folder.");
                }
            }
            else
            {

                Console.WriteLine("\n***** " + sZipFileName + ".zip already exists in " + sExtractLocation + " folder.");
            }

            // extract device_config.us, device_config.ous, and device_config.jpn, and device_config.jpn.net to C:\DeviceConfig folder.
            Console.WriteLine("\n***** Extracting device_config files to " + sExtractLocation + " folder.");
            eResult isExtracted = Upgl.UtilityLib.ExtractFileFromZipFile(sDestination, sExtractLocation, "device_config", out localFileNamesAndPath);

            if (isExtracted == eResult.SUCCESS)
            {
                Console.WriteLine("\n***** Successfully extract device_config files to your " + sExtractLocation + " folder");
            }
            else
            {
                Console.WriteLine("\n***** Fail to extract the device_config files to " + sExtractLocation + " folder.");
                Console.WriteLine("\n***** The zip file probably does not contain the device_config files");
                Console.WriteLine("\n***** Hit any key to exit program.");
                Console.ReadLine();
                Environment.Exit(0);

            }

            #endregion


        }

        /// <summary>
        /// This function create a C:\DeviceConfig folder on user's local C drive so the IR and the device_config files can be copied to it.
        /// </summary>
        /// <param name="sExtractLocation"></param>
        public void Create_A_Dir_On_Local_Drive(string sExtractLocation)
        {
            // create a directory C:\DeviceConfig on user's local c drive if it is not already there.
            if (!Directory.Exists(sExtractLocation))
            {
                Directory.CreateDirectory(sExtractLocation);
            }
        }

        /// <summary>
        /// This function displays an explanation that a model can be in multiple firmwares.
        /// </summary>

        public void Display_Models_Explanation()
        {
            Console.WriteLine("\n\n *********** A model can be in several firmwares. ***********");
            Console.WriteLine("\n The model and its firmware are displayed in the following format: 'Modelnumber_Firmware'.");
            Console.WriteLine("\n Ex: 3269-40_0x120e");
            Console.WriteLine("\n The '_0x120e' after the model number is the firmware version.\n");

        }

        #region put console window on top of the all_capabilities.txt

        //The lines of codes below are used to put the concole window on top of the all_capabilities.txt file when this file is opened.
        // Can remove it when you make a a function to display capabilities choices.
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr zeroOnly, string lpWindowName);

        public void Set_Console_Window_On_Top()
        {
            string originalTitle = Console.Title;
            string uniqueTitle = Guid.NewGuid().ToString();
            Console.Title = uniqueTitle;
            Thread.Sleep(50);
            IntPtr handle = FindWindowByCaption(IntPtr.Zero, uniqueTitle);

            if (handle == IntPtr.Zero)
            {
                Console.WriteLine("Oops, can't find main window.");
                return;
            }
            Console.Title = originalTitle;

            //while (true)
            //{
            Thread.Sleep(300);// Need to have this line
            SetForegroundWindow(handle);
            //}
        }

        #endregion

        /// <summary>
        /// This functions displays a main menu of all the capabilities in alphabetical order
        /// </summary>

        public void Display_Capabilities_Choices()
        {
            Console.WriteLine("\n\n*****Pick capabilitie(s) from the list below: \n");
            Console.Write("1: All Capabilities begin with a number          ");
            Console.WriteLine("A: All Capabilities begin with letter 'A'       ");
            Console.Write("B: All Capabilities begin with letter 'B'           ");
            Console.WriteLine("  ");
            Console.Write("C: All Capabilities begin with letter 'C'          ");
            Console.WriteLine("  ");
            Console.Write("D: All Capabilities begin with letter 'D'          ");
            Console.WriteLine("E: All Capabilities begin with letter 'E'          ");
            Console.Write("F: All Capabilities begin with letter 'F'          ");
            Console.WriteLine("H: All Capabilities begin with letter 'H'       ");
            Console.Write("I: All Capabilities begin with letter 'I'           ");
            Console.WriteLine("L: All Capabilities begin with letter 'L'          ");
            Console.Write("M: All Capabilities begin with letter 'M'          ");
            Console.WriteLine("N: All Capabilities begin with letter N");
            Console.Write("P: All Capabilities begin with letter 'P'          ");
            Console.WriteLine("Q: All Capabilities begin with letter 'Q'          ");
            Console.Write("R: All Capabilities begin with letter 'R'          ");
            Console.WriteLine("S: All Capabilities begin with letter 'S'       ");
            Console.Write("T: All Capabilities begin with letter 'T'           ");
            Console.WriteLine("U: All Capabilities begin with letter 'U'          ");
            Console.Write("V: All Capabilities begin with letter 'V'          ");
            Console.WriteLine("Z: All Capabilities begin with letter Z");
            Console.Write("\n\n***** Pick a letter for the capability list: ");

        }
        /// <summary>
        /// This function displays a list of capabilities begin with a number
        /// </summary>
        public void Display_All_Capabilities_Begin_With_Number(string sUserCapability)
        {
            Console.WriteLine("\n\n***** Pick a number from the list below: \n");
            Console.WriteLine("1: 1.5T MRI           ");
            Console.WriteLine("2: 30 J Max Energy    ");
            Console.WriteLine("3: 36 J Max Energy    ");
            Console.WriteLine("4: 40 J Max Energy    ");
            Console.Write("\n\n***** Pick a number: ");
            sUserCapability = Console.ReadLine();
            sUserCapability = sUserCapability.ToUpper();
            bool bchoice = false;
            while (bchoice == false)
            {
                if (sUserCapability != "1" && sUserCapability != "2" && sUserCapability != "3" && sUserCapability != "4")
                {
                 
                    Console.Write("\n\n***** Pick a number: ");
                    sUserCapability = Console.ReadLine();
                    sUserCapability = sUserCapability.ToUpper();
                    Console.WriteLine("\n\n***** You picked number: " + sUserCapability);
                }
                else
                {
                    bchoice = true;
                }
            }

        }


        /// <summary>
        /// This function displays a list of capabilities begin with a letter "A"
        /// </summary>
        public void Display_All_Capabilities_Begin_With_Letter_A(string sUserCapability)
        {
            Console.WriteLine("\n\n***** Pick a number from the list below: \n");
            Console.WriteLine("1: ACA Debug           ");
            Console.WriteLine("2: Advanced Hysteresis Rate    ");
            Console.WriteLine("3: Alert Timestamps    ");
            Console.WriteLine("4: All In One Device    ");
            Console.WriteLine("5: AT/AF Notification          ");
            Console.WriteLine("6: ATP during Charging    ");
            Console.WriteLine("7: Atrial ASC    ");
            Console.WriteLine("8: Atrial AutoThreshold    ");
            Console.WriteLine("9: Atrial SR    ");
            Console.WriteLine("10: Auto Cap Assessment    ");
            Console.WriteLine("11: AV Optimization    ");
            sUserCapability = Console.ReadLine();
            sUserCapability = sUserCapability.ToUpper();
            bool bchoice = false;
            while (bchoice == false)
            {
                if (sUserCapability == "1" || sUserCapability == "2" || sUserCapability == "3" || sUserCapability == "4" || sUserCapability == "5"
                || sUserCapability == "6" || sUserCapability == "7" || sUserCapability == "8" || sUserCapability == "9" || sUserCapability == "10" || sUserCapability == "11")
                {

                    Console.Write("\n\n***** Pick a number: ");
                    sUserCapability = Console.ReadLine();
                    sUserCapability = sUserCapability.ToUpper();
                    Console.WriteLine("\n\n***** You picked number: " + sUserCapability);
                }
                else
                {
                    bchoice = true;
                }
            }

        }




        public bool Check_If_UserChoise_Is_Correct(string sUserChoice, List<string> choices, bool bchoice)
        {
            foreach (var choice in choices)
            {
                if (choice.Contains(sUserChoice))
                {

                    bchoice = true;
                }

            }
            if (bchoice == false)
            {
                Console.WriteLine("\n\n***** You entered: " + sUserChoice + " .That is not a valid choice. Try again");
            }
            return bchoice;
        }

       







    }//end class
}// end namespace
