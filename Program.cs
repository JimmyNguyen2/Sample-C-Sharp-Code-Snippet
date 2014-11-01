using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using UPGL;
using UPTE;
using System.Reflection;
//using Microsoft.Office.Interop.Excel;



namespace ModelCapabilities
{

    //This program asks user to enter a location for the device_config files.
    //It then provide a menu of choice for user to pick from: pick a model to find its firmware or capability.
    // Or pick a firmware to find all the models in that firmware. Or list all the capabilities of a firmware.

    class Program : Local_Functions
    {

        static void Main(string[] args)
        {
            Local_Functions ll = new Local_Functions();

            #region Declarations
            string path = string.Empty;
            string sExtractLocation = @"C:\DeviceConfig";
            string sMessage = string.Empty;
            string sFileName = string.Empty;
            string sProject = string.Empty;
            string sUserChoice = string.Empty;
            string sZipFileName = string.Empty;
            Dictionary<string, string> Model_Capability = new Dictionary<string, string>();
            Dictionary<string, string> SupersetCapability = new Dictionary<string, string>();
            List<string> ModelNumber = new List<string>();
            string sFirmwareList = "_0x1502, _0x1407, _0x130c, _0x120e, _0x110e, _0xc0e, _0xb0e, _0xa0b, _0x1008, _0xe0, _0x90b, _0x80b, _0x700, All Firmwares";
            string sModel = string.Empty;
            string sLine = string.Empty;
            string sUsermodel = "";
            string sUserchoice = string.Empty;
            string sUserFirmware = string.Empty;
            int index = 0;
            int counter = 0;
            List<string> listCapabilities = new List<string>();
            List<string> listModels = new List<string>();
            List<string> listCapabilitiesChoices = new List<string>();
            List<string> listCapabilitiesA = new List<string>();
           // bool bchoice = false;

            #endregion

            #region initialize capabilities list
            // initialize "listCapabilitiesChoices"
            listCapabilitiesChoices.Add("1");
            listCapabilitiesChoices.Add("A");
            listCapabilitiesChoices.Add("B");
            listCapabilitiesChoices.Add("C");
            listCapabilitiesChoices.Add("D");
            listCapabilitiesChoices.Add("E");
            listCapabilitiesChoices.Add("F");
            listCapabilitiesChoices.Add("H");
            listCapabilitiesChoices.Add("I");
            listCapabilitiesChoices.Add("L");
            listCapabilitiesChoices.Add("M");
            listCapabilitiesChoices.Add("N");
            listCapabilitiesChoices.Add("P");
            listCapabilitiesChoices.Add("Q");
            listCapabilitiesChoices.Add("R");
            listCapabilitiesChoices.Add("S");
            listCapabilitiesChoices.Add("T");
            listCapabilitiesChoices.Add("U");
            listCapabilitiesChoices.Add("V");
            listCapabilitiesChoices.Add("Z");



            // initialize listCapabilitiesA
            listCapabilitiesA.Add("1");
            listCapabilitiesA.Add("2");
            listCapabilitiesA.Add("3");
            listCapabilitiesA.Add("4");
            listCapabilitiesA.Add("5");
            listCapabilitiesA.Add("6");
            listCapabilitiesA.Add("7");
            listCapabilitiesA.Add("8");
            listCapabilitiesA.Add("9");
            listCapabilitiesA.Add("10");
            listCapabilitiesA.Add("11");

            #endregion

            #region Show user how to use the program
            Console.SetWindowSize(90, 40);
            Console.Title = "Model Capability Utility by Jimmy Nguyen. The utility is in O:\\Software Development\\Jimmy\\ModelCapability folder";
            Console.WriteLine("\n***** This program helps you find what firmware a model belong to.");
            Console.WriteLine("\n***** Which model has a particular capability.");
            Console.WriteLine("\n***** List all models that are in a particular Firmware.");
            Console.WriteLine("\n***** It uses the info from the device_config file '.us, .ous. or .jpn'");
            Console.WriteLine("\n***** The device_config files '.us, .ous. or .jpn'are inside the zip file of the IR.");
            Console.WriteLine("\n***** This program will create a 'C:\\DeviceConfig' folder on your C drive.");
            Console.WriteLine("\n***** So it can extract the device_config file to this folder.");
            Console.WriteLine("\n***** When you run it the second time, you can choose choice 'C'");

            // Create a "C:\DeviceConfig folder
            ll.Create_A_Dir_On_Local_Drive(sExtractLocation);

            #endregion

            #region Get location of the DeviceConfig file from users
            while (true) // Loop indefinitely
            {
                ll.Display_User_Choice_In_Color("\n***** Where do you want to get the device_config files from?\n");
                Console.WriteLine("A: Latest IR from Project 2.6.7");
                Console.WriteLine("B: Latest IR from Project 2.6.5");
                Console.WriteLine("C: Get the existing device_config file from C:\\DeviceConfig folder.");// if user already ran the program once, there is no need to extract the files again.
                Console.WriteLine("D: Get the device_config file from the another project or another IR on local drive");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("E: Exit program");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("\n***** Pick a letter: ");
                sUserchoice = Console.ReadLine();
                sUserchoice = sUserchoice.ToUpper();
                ll.Display_User_Choice_In_Color("\n*****You picked: " + sUserchoice);

                // check if user enters correct choice
                while (sUserchoice != "A" && sUserchoice != "B" && sUserchoice != "C" && sUserchoice != "D" && sUserchoice != "E")
                {
                    Console.WriteLine("\n\n***** Please pick only one of these letters: 'A', or 'B', or 'C', or 'D', 'E'");
                    Console.Write("\n***** Pick a letter: ");
                    sUserchoice = Console.ReadLine();
                    sUserchoice = sUserchoice.ToUpper();

                }

                switch (sUserchoice)
                {
                    case "A":
                        path = @"\\Nasyfs02\unitybuilds$\IR2.6.7";
                        ll.Get_Latest_Dir_From_Network_And_Copy_To_C_Drive(path, sExtractLocation);
                        break;
                    case "B":
                        path = @"\\Nasyfs02\unitybuilds$\IRU2.6.5";
                        ll.Get_Latest_Dir_From_Network_And_Copy_To_C_Drive(path, sExtractLocation);
                        break;
                    case "C":
                        // No need to do anything program will continue with the function that asks user to pick file name below
                        break;
                    case "D":
                        Console.Write("\n**** Enter the location of the Zip file. Ex: " + @"\\nasyfs02\unitybuilds$\IR2.3\IR2.3_12.08.02.1");
                        Console.Write("\n**** Enter the location: ");
                        path = Console.ReadLine();
                        Console.Write("\n**** Enter the name of the Zip file. Ex: 'IR2.3_12.08.02.1.zip'");
                        Console.Write("\n**** Enter the Zipfile name: ");
                        sZipFileName = Console.ReadLine();
                        sExtractLocation = @"C:\DeviceConfig";

                        string source = path + @"\" + sZipFileName;
                        // call function to copy the Zipfile from network.
                        //If file exist in the source folder and not in the destination folder then copy the file other wise skip to next command
                        ll.Get_Dir_From_Network_Or_Local_And_Copy_To_C_Drive(source, sExtractLocation, sZipFileName);
                        break;
                    case "E": //exit program
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Pick either choice 'A', 'B', 'C' ,or 'D' only.");
                        break;

                }

                // Now ask user to choose which device_config files to use. There are 3 different types
                sFileName = ll.Ask_User_To_Pick_FileName();
                // check if user picks the correct choice
                while (sFileName != "A" && sFileName != "B" && sFileName != "C")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong choice. Pick either 'A' or 'B', or 'C' only.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    sFileName = ll.Ask_User_To_Pick_FileName();


                }// end while

                if (sFileName == "A")
                {
                    sFileName = "device_config.us";

                }
                else if (sFileName == "B")
                {
                    sFileName = "device_config.ous";
                }
                else if (sFileName == "C")
                {
                    sFileName = "device_config.jpn";
                }

                string sCheckFileExist = sExtractLocation + "\\" + sFileName;
                sMessage = "\n*****The location of the file is: ";
                ll.Display_User_Choice_In_Color(sMessage + sCheckFileExist);

                #region Commented code
                //Check if device_config file exist.
                //sFideExist = File.Exists(@sCheckFileExist);
                //while (!sFideExist)// May not need this while loop since the directory has been created and the device_config files have been extracted there.
                //{
                //    Console.ForegroundColor = ConsoleColor.Red;
                //    Console.WriteLine("\n***** Either the directory or the filename does not exist.");
                //    Console.ForegroundColor = ConsoleColor.Gray;
                //    Console.WriteLine("\n***** Enter the location of the DeviceConfig file again: ");
                //    sFileLocation = Console.ReadLine();
                //    sMessage = "\n***** You entered file location: ";
                //    ll.Display_User_Choice_In_Color(sMessage + sFileLocation);
                //    sFileName = ll.Ask_User_To_Pick_FileName();
                //    if (sFileName == "A")
                //    {
                //        sFileName = "device_config.us";

                //    }
                //    else if (sFileName == "B")
                //    {
                //        sFileName = "device_config.ous";
                //    }
                //    else if (sFileName == "C")
                //    {
                //        sFileName = "device_config.jpn";
                //    }
                //    sCheckFileExist = sFileLocation + "\\" + sFileName;
                //    sFideExist = File.Exists(@sCheckFileExist);
                //}

                #endregion

            #endregion

                #region Put all supersets of capabilities of each firmware in another file to use later

                //This file will only have the lines start with "FFFF_" which list all the capabilities that a firmware can have.
                //This line also has a line starts with "9999_" which lists all the capabilities that are available in the device_config file.

                using (StreamReader reader = new StreamReader(sCheckFileExist))
                {
                    using (StreamWriter writer = new StreamWriter(sExtractLocation + "\\device_config_capabilities.txt"))
                    {
                        while ((sLine = reader.ReadLine()) != null)// Save only the lines that contain the superset of capabilities.
                        {
                            if (sLine.Contains("FFFF_") || sLine.Contains("9999_"))

                                writer.WriteLine(sLine);
                        }
                        writer.Close();
                    }
                    reader.Close();
                }


                #endregion Save all available capabilities to a file for use later//4/30/2013

                #region Put all available capabilities in a list
                using (StreamReader reader = new StreamReader(sCheckFileExist))
                {
                    while ((sLine = reader.ReadLine()) != null)// Save only the line that contains all capabilities in the device_config file.
                    {
                        if (sLine.Contains("9999_"))
                        {
                            index = sLine.IndexOf(':');
                            sLine = sLine.Remove(0, index + 1);// Remove from the beginning of the line to after the ":" to get only the capabilities
                            listCapabilities = sLine.Split(',').ToList();
                        }
                    }
                    reader.Close();
                }

                #endregion

                #region Write all available capabilites to a file

                using (StreamWriter writer = new StreamWriter(sExtractLocation + "\\all_capabilities.txt"))
                {
                    foreach (string cap in listCapabilities)
                    {

                        writer.WriteLine(cap);
                    }
                    writer.Close();
                }

                #endregion

                #region Save the superset of capabilities for each firmware to a dictionary

                // Read the file and save the models and its capabilities
                StreamReader file = new StreamReader(sExtractLocation + "\\device_config_capabilities.txt");
                while ((sLine = file.ReadLine()) != null)
                {

                    index = sLine.IndexOf(':');
                    sModel = sLine.Remove(index); //Since there more than 1 firmware for each model, keept the FW extension and Schema so it can be added to the dictionary
                    string sCapability = sLine.Remove(0, index + 1);// Remove from the beginning of the line to after the ":" to get only the capabilities

                    //Add all capabilities to a dictionary
                    SupersetCapability.Add(sModel, sCapability);


                }
                file.Close();


                #endregion

                #region Remove unwanted lines from DeviceConfig file and save to a new file

                using (StreamReader reader = new StreamReader(sCheckFileExist))
                {
                    using (StreamWriter writer = new StreamWriter(sExtractLocation + "\\device_config_formatted.txt"))// @"C:\DeviceConfig\device_config_formatted.txt"
                    {
                        while ((sLine = reader.ReadLine()) != null)// Format the file to remove unwanted lines.
                        {
                            if (sLine.Contains("ExportTimestamp") || sLine.Contains("ReleaseVersion") || sLine.Contains("LastEditTimestamp") || sLine.Contains("FileName:device_config")
                                || sLine.Contains("ConfigGeneratorVersion") || sLine.Contains("#--") || sLine.Contains("FFFF_") || sLine.Contains("9999_") || sLine.Contains("8888_"))
                                continue;

                            writer.WriteLine(sLine);
                        }
                        writer.Close();
                    }
                    reader.Close();
                }

                #endregion

                #region Read from device configfile and save the models and its capabilities to a dictionary

                // Read the file and save the models and its capabilities
                file = new StreamReader(sExtractLocation + "\\device_config_formatted.txt");
                while ((sLine = file.ReadLine()) != null)
                {

                    index = sLine.IndexOf(':');
                    sModel = sLine.Remove(index); //Since there more than 1 firmware for each model, keept the FW extension and Schema so it can be added to the dictionary
                    string sCapability = sLine.Remove(0, index + 1);// Remove from the beginning of the line to after the ":" to get only the capabilities

                    //Add all capabilities to a dictionary
                    Model_Capability.Add(sModel, sCapability);

                }

                file.Close();
                //   System.Console.WriteLine("There were {0} lines.", counter);// for debugging

                // convert from dictionary keys to list

                listModels = Model_Capability.Keys.ToList();



                #endregion

                #region Save all models in each firmware

                // Create an empty list of all firmwares 
                List<string> allF15models = new List<string>();
                List<string> allF14models = new List<string>();
                List<string> allF13models = new List<string>();
                List<string> allF12models = new List<string>();
                List<string> allF11models = new List<string>();
                List<string> allF10models = new List<string>();
                List<string> allFCmodels = new List<string>();
                List<string> allFBmodels = new List<string>();
                List<string> allFAmodels = new List<string>();
                List<string> allFEmodels = new List<string>();
                List<string> allF9models = new List<string>();
                List<string> allF8models = new List<string>();
                List<string> allF7models = new List<string>();
                List<string> allmodels = new List<string>();

                //store each FirmWare with its models.
                foreach (string model in Model_Capability.Keys)
                {

                    if (model.Contains("0x1502"))//FW15
                        allF15models.Add(model);
                    else if (model.Contains("_0x1407"))//FW14
                        allF14models.Add(model);
                    else if (model.Contains("_0x130c"))//FW13
                        allF13models.Add(model);
                    else if (model.Contains("_0x120e"))//FW12
                        allF12models.Add(model);
                    else if (model.Contains("_0x110e"))//FW11
                        allF11models.Add(model);
                    else if (model.Contains("_0xc0e"))//FWC
                        allFCmodels.Add(model);
                    else if (model.Contains("_0xb0e"))//FW B
                        allFBmodels.Add(model);
                    else if (model.Contains("_0xa0b"))//FW A
                        allFAmodels.Add(model);
                    else if (model.Contains("_0x1008")) // FW10
                        allF10models.Add(model);
                    else if (model.Contains("_0xe0"))// FWE
                        allFEmodels.Add(model);
                    else if (model.Contains("_0x90b"))// FW9
                        allF9models.Add(model);
                    else if (model.Contains("_0x80b"))// FW8
                        allF8models.Add(model);
                    else if (model.Contains("_0x700"))// FW7
                        allF7models.Add(model);
                }
                foreach (string model in Model_Capability.Keys)
                {
                    allmodels.Add(model);
                }
                #endregion





                #region Ask user to pick a choice

                //Set Console window size
                Console.SetWindowSize(90, 45);// Important: Depend on window resolution. If you set the width > the max allow for that resolution, it will crash.
                //Console.SetBufferSize(90, 350);
                bool loop = true;
                while (loop) // Loop indefinitely until user chooses to exit
                {

                    Console.WriteLine("\n\n***** Select a choice below:");
                    Console.WriteLine();
                    Console.WriteLine("A: Enter a model number to findout what firmware it belongs to");
                    Console.WriteLine("B: Enter a capability to find out what models have it");
                    Console.WriteLine("C: List all models in a Firmware");
                    Console.WriteLine("D: List all capabilities of a model");
                    Console.WriteLine("E: List all capabilities in a Firmware");
                    Console.WriteLine("F: Go back to main menu");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("G: Exit program");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("\n***** Pick a letter: ");

                    sUserchoice = Console.ReadLine();
                    sUserchoice = sUserchoice.ToUpper();
                    if (sUserchoice == "G") // Quit program when user chooses "Exit".
                    {
                        Environment.Exit(0);
                    } 
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n\n****You picked: " + sUserchoice);
                    Console.ForegroundColor = ConsoleColor.Gray;

                    switch (sUserchoice)
                    {
                        #region Case A
                        case "A"://Enter a model number to findout what firmware it belongs to
                            Console.WriteLine("\n\n***** For high voltage models enter the '-' between the model and the energy.");
                            Console.WriteLine("\n***** Example: '3269-40', or '2227-36', or '1107-30'.");
                            Console.WriteLine("\n***** For low voltage model: add '-00' after model."); 
                            Console.WriteLine("\n***** Example:'3262-00', or '2126-00', or '1210-00'"); 
                            Console.WriteLine("\n***** For Single Atrial Chamber model like 1160A, enter '1160-00-A'. Use upper case A.");
                            Console.Write("\n***** Enter a model number: ");

                            sUsermodel = Console.ReadLine();

                            //5/2/2013: Need to update to get the model number without the dash and if it has only 4 numbers then it is low voltage models. Verify that is is a low voltage model.
                            //make sure model contain a "-"
                            if (!sUsermodel.Contains("-"))
                            {
                                Console.WriteLine("Please enter model with the '-'");
                                Console.WriteLine("Enter model number: ");
                                sUsermodel = Console.ReadLine();

                            }

                            // check to see if user enter a valid model
                            counter = 0;
                            foreach (KeyValuePair<string, string> item in Model_Capability)
                            {
                                if (item.Key.Contains(sUsermodel))
                                {
                                    counter = 1;
                                }
                            }
                            if (counter == 0)
                            {
                                Console.WriteLine("\n\n***** Model " + sUsermodel + " is not in the device_config file.");
                                Console.WriteLine("\n\n***** Either you entered an in valid model or the device_config file is not updated yet.");
                                break;
                            }

                            if (sUsermodel.Contains("1160") || sUsermodel.Contains("1170") || sUsermodel.Contains("1240")
                                || sUsermodel.Contains("1250") || sUsermodel.Contains("1260") || sUsermodel.Contains("1270")) //These models are only in FW14
                            {

                                ll.Display_User_Choice_In_Color("\n\n*****Model " + sUsermodel + " is in Firmware 14");

                            }
                            else
                            {
                                //Check which firmware model belong to                            
                                ll.Find_WhatFw_a_model_belongs_to(sUsermodel, allF15models, "Firmware 15");
                                ll.Find_WhatFw_a_model_belongs_to(sUsermodel, allF14models, "Firmware 14");
                                ll.Find_WhatFw_a_model_belongs_to(sUsermodel, allF13models, "Firmware 13");
                                ll.Find_WhatFw_a_model_belongs_to(sUsermodel, allF12models, "Firmware 12");
                                ll.Find_WhatFw_a_model_belongs_to(sUsermodel, allF11models, "Firmware 11");
                                ll.Find_WhatFw_a_model_belongs_to(sUsermodel, allF10models, "Firmware 10");
                                ll.Find_WhatFw_a_model_belongs_to(sUsermodel, allFCmodels, "Firmware C");
                                ll.Find_WhatFw_a_model_belongs_to(sUsermodel, allFBmodels, "Firmware B");
                                ll.Find_WhatFw_a_model_belongs_to(sUsermodel, allFAmodels, "Firmware A");
                                ll.Find_WhatFw_a_model_belongs_to(sUsermodel, allFEmodels, "Firmware E");
                                ll.Find_WhatFw_a_model_belongs_to(sUsermodel, allF9models, "Firmware 9");
                                ll.Find_WhatFw_a_model_belongs_to(sUsermodel, allF8models, "Firmware 8");
                                ll.Find_WhatFw_a_model_belongs_to(sUsermodel, allF7models, "Firmware 7");

                            }
                            break;
                        #endregion case A

                        #region Case B
                        //Need to update this section to allow user to enter multiple capabilities. Need to provide a list of available capabilities
                        case "B"://Enter a capability to find out what models have it
                            string sUsercapability = string.Empty;
                            string sUserCapability1 = string.Empty;
                            string sUserCapability2 = string.Empty;
                            string sUserCapability3 = string.Empty;


                            //Dispaly all_capabilities.txt file that has all the capabilities
                            System.Diagnostics.Process.Start(sExtractLocation + "\\all_capabilities.txt");

                            // Put the console window on top of the all_capabilities file
                            ll.Set_Console_Window_On_Top();
                            Console.WriteLine("\n\n***** An 'all_capabilities.txt' file was just opened for you using notepad.");
                            Console.WriteLine("\n\n***** It lists all the available capabilities in aphabetical order.");
                            Console.WriteLine("\n\n***** You can copy and paste a capability you want from the 'all_capabilities.txt' file to the console window");
                            Console.WriteLine("\n\n***** You can enter up to 3 capabilities");
                            Console.Write("\n***** Enter either a number '1', or '2', or '3': ");
                            sUsercapability = Console.ReadLine();

                            // check if user enters a correct value of 1, 2, or 3
                            while (sUsercapability != "1" && sUsercapability != "2" && sUsercapability != "3")
                            {
                                Console.WriteLine("\n\n***** You can enter only a number '1', or '2', or '3'.");
                                Console.Write("\n***** Enter either a number '1', or '2', or '3': ");
                                sUsercapability = Console.ReadLine();

                            }
                            int choice = Convert.ToInt32(sUsercapability);

                            if (choice == 1)
                            {
                                Console.Write("\n\n***** Enter capability: ");
                                sUserCapability1 = Console.ReadLine();
                                ll.Display_User_Choice_In_Color("\n\n******You entered capability: " + sUserCapability1);


                                sUserFirmware = ll.Ask_For_Which_FW_User_Want();
                                if (!sFirmwareList.Contains(sUserFirmware))
                                {
                                    Console.Write("\n***** Your choice is not valid.");
                                    sUserFirmware = ll.Ask_For_Which_FW_User_Want();// if users enter wrong choice, ask them to pick another choice
                                }

                                if (sUserFirmware == "All Firmwares")
                                {
                                    Console.Write("\n\n***** List of all models with this capability: " + sUserCapability1 + "\n\n");
                                    ll.Display_Models_Explanation();
                                    ll.Find_A_Capability_In_A_Model(sUserCapability1, Model_Capability);
                                }
                                else
                                {
                                    ll.Find_A_Capability_In_A_Model(sUserCapability1, sUserFirmware, Model_Capability);
                                }
                            }// end if of sUsercapability == "1"
                            else if (choice == 2)
                            {

                                Console.Write("\n\n***** Enter first capability: ");
                                sUserCapability1 = Console.ReadLine();
                                Console.Write("\n\n***** Enter 2nd capability: ");
                                sUserCapability2 = Console.ReadLine();

                                ll.Display_User_Choice_In_Color("\n\n******You entered capabilities: " + sUserCapability1 + "," + sUserCapability2 + "\n\n");


                                sUserFirmware = ll.Ask_For_Which_FW_User_Want();
                                if (!sFirmwareList.Contains(sUserFirmware))
                                {
                                    Console.Write("\n***** Your choice is not valid.");
                                    sUserFirmware = ll.Ask_For_Which_FW_User_Want();// if users enter wrong choice, ask them to pick another choice
                                }

                                if (sUserFirmware == "All Firmwares")
                                {
                                    Console.Write("\n\n***** List of all models in firmwares with these capabilities: " + sUserCapability1 + "," + sUserCapability2 + "," + sUserCapability3 + "\n\n");
                                    ll.Display_Models_Explanation();
                                    ll.Find_Capabilities_In_A_Model(sUserCapability1, sUserCapability2, Model_Capability);
                                }
                                else
                                {
                                    ll.Find_Capabilities_In_A_Model(sUserCapability1, sUserCapability2, sUserFirmware, Model_Capability);
                                }



                            }// end else if of (sUsercapability == "2")
                            else if (choice == 3)
                            {

                                Console.Write("\n\n***** Enter first capability: ");
                                sUserCapability1 = Console.ReadLine();
                                Console.Write("\n\n***** Enter 2nd capability: ");
                                sUserCapability2 = Console.ReadLine();
                                Console.Write("\n\n***** Enter 3rd capability: ");
                                sUserCapability3 = Console.ReadLine();


                                ll.Display_User_Choice_In_Color("\n\n******You entered capability: " + sUserCapability1 + "," + sUserCapability2 + "," + sUserCapability3 + "\n\n");


                                sUserFirmware = ll.Ask_For_Which_FW_User_Want();
                                if (!sFirmwareList.Contains(sUserFirmware))
                                {
                                    Console.Write("\n***** Your choice is not valid.");
                                    sUserFirmware = ll.Ask_For_Which_FW_User_Want();// if users enter wrong choice, ask them to pick another choice
                                }

                                if (sUserFirmware == "All Firmwares")
                                {
                                    Console.Write("\n\n***** List of all models with these capabilities: " + sUserCapability1 + "," + sUserCapability2 + "," + sUserCapability3 + "\n\n");
                                    ll.Display_Models_Explanation();
                                    ll.Find_Three_Capabilities_In_A_Model(sUserCapability1, sUserCapability2, sUserCapability3, Model_Capability);
                                }
                                else
                                {
                                    ll.Find_Capabilities_In_A_Model(sUserCapability1, sUserCapability2, sUserCapability3, sUserFirmware, Model_Capability);
                                }



                            }// end else if of (sUsercapability == "3")

                            break;
                        #endregion Case B

                        #region Case C
                        case "C"://List all models in a Firmware
                            Console.WriteLine("Pick a firmware from the list below:");
                            sUserFirmware = ll.Ask_For_Which_FW_User_Want();
                            ll.Display_All_Models_In_A_Firmware(sUserFirmware, allF15models, allF14models, allF13models, allF12models, allF11models, allF10models, allFEmodels, allFCmodels, allFBmodels, allFAmodels, allF9models, allF8models, allF7models, allmodels);

                            break;
                        #endregion Case C

                        #region Case D
                        case "D"://List all capabilities of a model
                           Console.WriteLine("\n\n***** For high voltage models enter the '-' between the model and the energy.");
                            Console.WriteLine("\n***** Example: '3269-40', or '2227-36', or '1107-30'.");
                            Console.WriteLine("\n***** For low voltage model: add '-00' after model."); 
                            Console.WriteLine("\n***** Example:'3262-00', or '2126-00', or '1210-00'"); 
                            Console.WriteLine("\n***** For Single Atrial Chamber model like 1160A, enter '1160-00-A'. Use upper case A.");
                            Console.Write("\n***** Enter a model number: ");
                            sUsermodel = Console.ReadLine();
                            //5/2/2013: Need to add a function to verify if the model is valid first.
                            counter = 0;
                            Console.WriteLine("\n\n *********** A model can be in several firmwares. ***********");
                            Console.WriteLine("\n The model and its firmware are displayed in the following format:");
                            Console.WriteLine("\n 'Modelnumber_Firmware'.Ex: 3269-40_0x120e");
                            Console.WriteLine("\n The '_0x120e' after the model number is the firmware version.");
                            foreach (KeyValuePair<string, string> item in Model_Capability)
                            {
                                if (item.Key.Contains(sUsermodel))
                                {
                                    string s = string.Empty;
                                    string sModelFirware = item.Key;
                                    //Remove the last 5 characters which are the schema version
                                    sModelFirware = sModelFirware.Remove(sModelFirware.Length - 5);
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("\n\n***** Model: " + sModelFirware + " has the following capabilities:\n ");
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    s = item.Value;
                                    List<string> result = s.Split(',').ToList();
                                    foreach (string value in result)
                                    {
                                        Console.WriteLine(value);
                                    }



                                    counter = 1;
                                }
                            }
                            Console.ForegroundColor = ConsoleColor.Gray;
                            if (counter == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("The model you entered: " + sUsermodel + " is not a valid model. Try again.");
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                            break;
                        #endregion Case D

                        #region Case E
                        case "E": //List all capabilities in a Firmware
                            sUserFirmware = ll.Ask_For_Which_FW_User_Want();
                            foreach (KeyValuePair<string, string> item in SupersetCapability)
                            {
                                if (item.Key.Contains(sUserFirmware))
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("\n\n***** It has the following capabilities: ");
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    string s = item.Value;
                                    List<string> result = s.Split(',').ToList();
                                    foreach (string value in result)
                                    {
                                        Console.WriteLine(value);
                                    }

                                    Console.ForegroundColor = ConsoleColor.Gray;
                                }
                            }
                            if (sUserFirmware == "All Firmwares")
                            {
                                foreach (KeyValuePair<string, string> item in SupersetCapability)
                                {
                                    if (item.Key.Contains("9999_"))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.WriteLine("\n\n***** All the available capabilities in the device_config are: ");
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        string s = item.Value;
                                        List<string> result = s.Split(',').ToList();
                                        foreach (string value in result)
                                        {
                                            Console.WriteLine(value);
                                        }

                                        Console.ForegroundColor = ConsoleColor.Gray;
                                    }
                                }

                            }
                            break;
                        #endregion Case E
                        case "F": // go back to main program
                            loop = false;
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("You picked: " + sUserchoice + ". It is an invalid Choice. Try again.");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;

                    }
                }//end while for Ask user to pick a choice

                // Clean-up: Re-initialize all the variables for next user's choice.
                path = string.Empty;
                sExtractLocation = @"C:\DeviceConfig";
                sMessage = string.Empty;
                sFileName = string.Empty;
                sProject = string.Empty;
                sUserChoice = string.Empty;
                sZipFileName = string.Empty;
                Model_Capability = new Dictionary<string, string>();
                SupersetCapability = new Dictionary<string, string>();
                ModelNumber = new List<string>();
                sFirmwareList = "_0x1502, _0x1407, _0x130c, _0x120e, _0x110e, _0xc0e, _0xb0e, _0xa0b, _0x1008, _0xe0, _0x90b, _0x80b, _0x700, All Firmwares";
                sModel = string.Empty;
                sLine = string.Empty;
                sUsermodel = "";
                sUserchoice = string.Empty;
                sUserFirmware = string.Empty;
                index = 0;
                counter = 0;
                listCapabilities = new List<string>();
                listModels = new List<string>();

            }//end while for Get location of the DeviceConfig file from users
                #endregion


        }//end main




    }// end class
}// end namespace
