using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Data;

using IO_1_0;

namespace Helpers_1_0
{
    public class Helper
    {

    }

    public class Language_Helper
    {
        //Windows 7 support
        public static DataTable language_table = null;

        public static bool load_Languages_Reference(string lang_file)
        {
            language_table = CSV_Helpers.csv_To_DataTable(lang_file, ",");

            if (languages != null && language_table.Rows.Count > 0)
            {

                return true;
            }
            return false;
        }

        public static string get_Language(string langid)
        {

            string hex = get_Language_As_Hex(langid);

            string lang = "";
            foreach (DataRow r in language_table.Rows)
            {
                if (hex == r.ItemArray[0].ToString())
                {
                    if (r.ItemArray[4].ToString() != "Empty")
                    {
                        lang = r.ItemArray[4].ToString();
                    }

                    if (r.ItemArray[1].ToString() != "Empty")
                    {
                        lang += " " + r.ItemArray[1].ToString();
                    }
                    break;
                }
            }

            return lang;
        }

        public static string get_Langauage_As_Locale_ID(string langid)
        {
            int i = Int32.Parse(langid);
            string hex = i.ToString("X");
            if (hex.Length == 3)
            {
                hex = "00000" + hex;
            }
            else
            {
                hex = "0000" + hex;
            }

            return hex;
        }

        public static string get_Language_As_Hex(string langid)
        {
            int i = Int32.Parse(langid);
            string hex = i.ToString("X");
            if (hex.Length == 3)
            {
                hex = "0x0" + hex;
            }
            else
            {
                hex = "0x" + hex;
            }

            return hex;
        }

        public static Dictionary<string, int> vk_keys = new Dictionary<string, int> 
                                                        {
                                                            /// <summary></summary>
                                                            {"LeftButton" , 0x01},
                                                            /// <summary></summary>
                                                            {"RightButton" , 0x02},
                                                            /// <summary></summary>
                                                            {"Cancel" , 0x03},
                                                            /// <summary></summary>
                                                            {"MiddleButton" , 0x04},
                                                            /// <summary></summary>
                                                            {"ExtraButton1" , 0x05},
                                                            /// <summary></summary>
                                                            {"ExtraButton2" , 0x06},
                                                            /// <summary></summary>
                                                            {"Back" , 0x08},
                                                            /// <summary></summary>
                                                            {"Tab" , 0x09},
                                                            /// <summary></summary>
                                                            {"Clear" , 0x0C},
                                                            /// <summary></summary>
                                                            {"Return" , 0x0D},
                                                            /// <summary></summary>
                                                            {"Shift" , 0x10},
                                                            /// <summary></summary>
                                                            {"Control" , 0x11},
                                                            /// <summary></summary>
                                                            {"Menu" , 0x12},
                                                            /// <summary></summary>
                                                            {"Pause" , 0x13},
                                                            /// <summary></summary>
                                                            {"CapsLock" , 0x14},
                                                            /// <summary></summary>
                                                            {"Kana" , 0x15},
                                                            /// <summary></summary>
                                                            {"Hangeul" , 0x15},
                                                            /// <summary></summary>
                                                            {"Hangul" , 0x15},
                                                            /// <summary></summary>
                                                            {"Junja" , 0x17},
                                                            /// <summary></summary>
                                                            {"Final" , 0x18},
                                                            /// <summary></summary>
                                                            {"Hanja" , 0x19},
                                                            /// <summary></summary>
                                                            {"Kanji" , 0x19},
                                                            /// <summary></summary>
                                                            {"Escape" , 0x1B},
                                                            /// <summary></summary>
                                                            {"Convert" , 0x1C},
                                                            /// <summary></summary>
                                                            {"NonConvert" , 0x1D},
                                                            /// <summary></summary>
                                                            {"Accept" , 0x1E},
                                                            /// <summary></summary>
                                                            {"ModeChange" , 0x1F},
                                                            /// <summary></summary>
                                                            {"Space" , 0x20},
                                                            /// <summary></summary>
                                                            {"Prior" , 0x21},
                                                            /// <summary></summary>
                                                            {"Next" , 0x22},
                                                            /// <summary></summary>
                                                            {"End" , 0x23},
                                                            /// <summary></summary>
                                                            {"Home" , 0x24},
                                                            /// <summary></summary>
                                                            {"Left" , 0x25},
                                                            /// <summary></summary>
                                                            {"Up" , 0x26},
                                                            /// <summary></summary>
                                                            {"Right" , 0x27},
                                                            /// <summary></summary>
                                                            {"Down" , 0x28},
                                                            /// <summary></summary>
                                                            {"Select" , 0x29},
                                                            /// <summary></summary>
                                                            {"Print" , 0x2A},
                                                            /// <summary></summary>
                                                            {"Execute" , 0x2B},
                                                            /// <summary></summary>
                                                            {"Snapshot" , 0x2C},
                                                            /// <summary></summary>
                                                            {"Insert" , 0x2D},
                                                            /// <summary></summary>
                                                            {"Delete" , 0x2E},
                                                            /// <summary></summary>
                                                            {"Help" , 0x2F},
                                                            /// <summary></summary>
                                                            {"N0" , 0x30},
                                                            /// <summary></summary>
                                                            {"N1" , 0x31},
                                                            /// <summary></summary>
                                                            {"N2" , 0x32},
                                                            /// <summary></summary>
                                                            {"N3" , 0x33},
                                                            /// <summary></summary>
                                                            {"N4" , 0x34},
                                                            /// <summary></summary>
                                                            {"N5" , 0x35},
                                                            /// <summary></summary>
                                                            {"N6" , 0x36},
                                                            /// <summary></summary>
                                                            {"N7" , 0x37},
                                                            /// <summary></summary>
                                                            {"N8" , 0x38},
                                                            /// <summary></summary>
                                                            {"N9" , 0x39},
                                                            /// <summary></summary>
                                                            {"A" , 0x41},
                                                            /// <summary></summary>
                                                            {"B" , 0x42},
                                                            /// <summary></summary>
                                                            {"C" , 0x43},
                                                            /// <summary></summary>
                                                            {"D" , 0x44},
                                                            /// <summary></summary>
                                                            {"E" , 0x45},
                                                            /// <summary></summary>
                                                            {"F" , 0x46},
                                                            /// <summary></summary>
                                                            {"G" , 0x47},
                                                            /// <summary></summary>
                                                            {"H" , 0x48},
                                                            /// <summary></summary>
                                                            {"I" , 0x49},
                                                            /// <summary></summary>
                                                            {"J" , 0x4A},
                                                            /// <summary></summary>
                                                            {"K" , 0x4B},
                                                            /// <summary></summary>
                                                            {"L" , 0x4C},
                                                            /// <summary></summary>
                                                            {"M" , 0x4D},
                                                            /// <summary></summary>
                                                            {"N" , 0x4E},
                                                            /// <summary></summary>
                                                            {"O" , 0x4F},
                                                            /// <summary></summary>
                                                            {"P" , 0x50},
                                                            /// <summary></summary>
                                                            {"Q" , 0x51},
                                                            /// <summary></summary>
                                                            {"R" , 0x52},
                                                            /// <summary></summary>
                                                            {"S" , 0x53},
                                                            /// <summary></summary>
                                                            {"T" , 0x54},
                                                            /// <summary></summary>
                                                            {"U" , 0x55},
                                                            /// <summary></summary>
                                                            {"V" , 0x56},
                                                            /// <summary></summary>
                                                            {"W" , 0x57},
                                                            /// <summary></summary>
                                                            {"X" , 0x58},
                                                            /// <summary></summary>
                                                            {"Y" , 0x59},
                                                            /// <summary></summary>
                                                            {"Z" , 0x5A},
                                                            /// <summary></summary>
                                                            {"LeftWindows" , 0x5B},
                                                            /// <summary></summary>
                                                            {"RightWindows" , 0x5C},
                                                            /// <summary></summary>
                                                            {"Application" , 0x5D},
                                                            /// <summary></summary>
                                                            {"Sleep" , 0x5F},
                                                            /// <summary></summary>
                                                            {"Numpad0" , 0x60},
                                                            /// <summary></summary>
                                                            {"Numpad1" , 0x61},
                                                            /// <summary></summary>
                                                            {"Numpad2" , 0x62},
                                                            /// <summary></summary>
                                                            {"Numpad3" , 0x63},
                                                            /// <summary></summary>
                                                            {"Numpad4" , 0x64},
                                                            /// <summary></summary>
                                                            {"Numpad5" , 0x65},
                                                            /// <summary></summary>
                                                            {"Numpad6" , 0x66},
                                                            /// <summary></summary>
                                                            {"Numpad7" , 0x67},
                                                            /// <summary></summary>
                                                            {"Numpad8" , 0x68},
                                                            /// <summary></summary>
                                                            {"Numpad9" , 0x69},
                                                            /// <summary></summary>
                                                            {"Multiply" , 0x6A},
                                                            /// <summary></summary>
                                                            {"Add" , 0x6B},
                                                            /// <summary></summary>
                                                            {"Separator" , 0x6C},
                                                            /// <summary></summary>
                                                            {"Subtract" , 0x6D},
                                                            /// <summary></summary>
                                                            {"Decimal" , 0x6E},
                                                            /// <summary></summary>
                                                            {"Divide" , 0x6F},
                                                            /// <summary></summary>
                                                            {"F1" , 0x70},
                                                            /// <summary></summary>
                                                            {"F2" , 0x71},
                                                            /// <summary></summary>
                                                            {"F3" , 0x72},
                                                            /// <summary></summary>
                                                            {"F4" , 0x73},
                                                            /// <summary></summary>
                                                            {"F5" , 0x74},
                                                            /// <summary></summary>
                                                            {"F6" , 0x75},
                                                            /// <summary></summary>
                                                            {"F7" , 0x76},
                                                            /// <summary></summary>
                                                            {"F8" , 0x77},
                                                            /// <summary></summary>
                                                            {"F9" , 0x78},
                                                            /// <summary></summary>
                                                            {"F10" , 0x79},
                                                            /// <summary></summary>
                                                            {"F11" , 0x7A},
                                                            /// <summary></summary>
                                                            {"F12" , 0x7B},
                                                            /// <summary></summary>
                                                            {"F13" , 0x7C},
                                                            /// <summary></summary>
                                                            {"F14" , 0x7D},
                                                            /// <summary></summary>
                                                            {"F15" , 0x7E},
                                                            /// <summary></summary>
                                                            {"F16" , 0x7F},
                                                            /// <summary></summary>
                                                            {"F17" , 0x80},
                                                            /// <summary></summary>
                                                            {"F18" , 0x81},
                                                            /// <summary></summary>
                                                            {"F19" , 0x82},
                                                            /// <summary></summary>
                                                            {"F20" , 0x83},
                                                            /// <summary></summary>
                                                            {"F21" , 0x84},
                                                            /// <summary></summary>
                                                            {"F22" , 0x85},
                                                            /// <summary></summary>
                                                            {"F23" , 0x86},
                                                            /// <summary></summary>
                                                            {"F24" , 0x87},
                                                            /// <summary></summary>
                                                            {"NumLock" , 0x90},
                                                            /// <summary></summary>
                                                            {"ScrollLock" , 0x91},
                                                            /// <summary></summary>
                                                            {"NEC_Equal" , 0x92},
                                                            /// <summary></summary>
                                                            {"Fujitsu_Jisho" , 0x92},
                                                            /// <summary></summary>
                                                            {"Fujitsu_Masshou" , 0x93},
                                                            /// <summary></summary>
                                                            {"Fujitsu_Touroku" , 0x94},
                                                            /// <summary></summary>
                                                            {"Fujitsu_Loya" , 0x95},
                                                            /// <summary></summary>
                                                            {"Fujitsu_Roya" , 0x96},
                                                            /// <summary></summary>
                                                            {"LeftShift" , 0xA0},
                                                            /// <summary></summary>
                                                            {"RightShift" , 0xA1},
                                                            /// <summary></summary>
                                                            {"LeftControl" , 0xA2},
                                                            /// <summary></summary>
                                                            {"RightControl" , 0xA3},
                                                            /// <summary></summary>
                                                            {"LeftMenu" , 0xA4},
                                                            /// <summary></summary>
                                                            {"RightMenu" , 0xA5},
                                                            /// <summary></summary>
                                                            {"BrowserBack" , 0xA6},
                                                            /// <summary></summary>
                                                            {"BrowserForward" , 0xA7},
                                                            /// <summary></summary>
                                                            {"BrowserRefresh" , 0xA8},
                                                            /// <summary></summary>
                                                            {"BrowserStop" , 0xA9},
                                                            /// <summary></summary>
                                                            {"BrowserSearch" , 0xAA},
                                                            /// <summary></summary>
                                                            {"BrowserFavorites" , 0xAB},
                                                            /// <summary></summary>
                                                            {"BrowserHome" , 0xAC},
                                                            /// <summary></summary>
                                                            {"VolumeMute" , 0xAD},
                                                            /// <summary></summary>
                                                            {"VolumeDown" , 0xAE},
                                                            /// <summary></summary>
                                                            {"VolumeUp" , 0xAF},
                                                            /// <summary></summary>
                                                            {"MediaNextTrack" , 0xB0},
                                                            /// <summary></summary>
                                                            {"MediaPrevTrack" , 0xB1},
                                                            /// <summary></summary>
                                                            {"MediaStop" , 0xB2},
                                                            /// <summary></summary>
                                                            {"MediaPlayPause" , 0xB3},
                                                            /// <summary></summary>
                                                            {"LaunchMail" , 0xB4},
                                                            /// <summary></summary>
                                                            {"LaunchMediaSelect" , 0xB5},
                                                            /// <summary></summary>
                                                            {"LaunchApplication1" , 0xB6},
                                                            /// <summary></summary>
                                                            {"LaunchApplication2" , 0xB7},
                                                            /// <summary></summary>
                                                            {"OEM1" , 0xBA},
                                                            /// <summary></summary>
                                                            {"OEMPlus" , 0xBB},
                                                            /// <summary></summary>
                                                            {"OEMComma" , 0xBC},
                                                            /// <summary></summary>
                                                            {"OEMMinus" , 0xBD},
                                                            /// <summary></summary>
                                                            {"OEMPeriod" , 0xBE},
                                                            /// <summary></summary>
                                                            {"OEM2" , 0xBF},
                                                            /// <summary></summary>
                                                            {"OEM3" , 0xC0},
                                                            /// <summary></summary>
                                                            {"OEM4" , 0xDB},
                                                            /// <summary></summary>
                                                            {"OEM5" , 0xDC},
                                                            /// <summary></summary>
                                                            {"OEM6" , 0xDD},
                                                            /// <summary></summary>
                                                            {"OEM7" , 0xDE},
                                                            /// <summary></summary>
                                                            {"OEM8" , 0xDF},
                                                            /// <summary></summary>
                                                            {"OEMAX" , 0xE1},
                                                            /// <summary></summary>
                                                            {"OEM102" , 0xE2},
                                                            /// <summary></summary>
                                                            {"ICOHelp" , 0xE3},
                                                            /// <summary></summary>
                                                            {"ICO00" , 0xE4},
                                                            /// <summary></summary>
                                                            {"ProcessKey" , 0xE5},
                                                            /// <summary></summary>
                                                            {"ICOClear" , 0xE6},
                                                            /// <summary></summary>
                                                            {"Packet" , 0xE7},
                                                            /// <summary></summary>
                                                            {"OEMReset" , 0xE9},
                                                            /// <summary></summary>
                                                            {"OEMJump" , 0xEA},
                                                            /// <summary></summary>
                                                            {"OEMPA1" , 0xEB},
                                                            /// <summary></summary>
                                                            {"OEMPA2" , 0xEC},
                                                            /// <summary></summary>
                                                            {"OEMPA3" , 0xED},
                                                            /// <summary></summary>
                                                            {"OEMWSCtrl" , 0xEE},
                                                            /// <summary></summary>
                                                            {"OEMCUSel" , 0xEF},
                                                            /// <summary></summary>
                                                            {"OEMATTN" , 0xF0},
                                                            /// <summary></summary>
                                                            {"OEMFinish" , 0xF1},
                                                            /// <summary></summary>
                                                            {"OEMCopy" , 0xF2},
                                                            /// <summary></summary>
                                                            {"OEMAuto" , 0xF3},
                                                            /// <summary></summary>
                                                            {"OEMENLW" , 0xF4},
                                                            /// <summary></summary>
                                                            {"OEMBackTab" , 0xF5},
                                                            /// <summary></summary>
                                                            {"ATTN" , 0xF6},
                                                            /// <summary></summary>
                                                            {"CRSel" , 0xF7},
                                                            /// <summary></summary>
                                                            {"EXSel" , 0xF8},
                                                            /// <summary></summary>
                                                            {"EREOF" , 0xF9},
                                                            /// <summary></summary>
                                                            {"Play" , 0xFA},
                                                            /// <summary></summary>
                                                            {"Zoom" , 0xFB},
                                                            /// <summary></summary>
                                                            {"Noname" , 0xFC},
                                                            /// <summary></summary>
                                                            {"PA1" , 0xFD},
                                                            /// <summary></summary>
                                                            {"OEMClear" , 0xFE}
                                                        };
        public static bool display_Keyboard(string langid)
        {
            string w = langid;

            IntPtr res = Win32_Helpers.LoadKeyboardLayout(w, (uint)Win32_Helpers.KLF.KLF_ACTIVATE);
            if (res == null)
            {
                return false;
            }

            StringBuilder s = new StringBuilder(100);
            Win32_Helpers.GetKeyboardLayoutName(s);

            int i = 0;
            while(i<255)
            {
                StringBuilder key = new StringBuilder(100);

                if (i % 5 == 0)
                {
                    Console.WriteLine();
                }

                int t = Win32_Helpers.GetKeyNameText(i << 16, key, key.Capacity);
                //if (t == 0)
                //{
                //    return false;
                //}

                Console.Write("\t " +key);

                i++;
            }

            bool b = Win32_Helpers.UnloadKeyboardLayout(res);
            if (!b)
            {
                Console.WriteLine(Marshal.GetLastWin32Error());
                return false;
            }

            return true;
        }

        public static IntPtr get_Current_Layout()
        {
            //int t_id = System.Threading.Thread.CurrentThread.ManagedThreadId;

            IntPtr cur = Win32_Helpers.GetKeyboardLayout(0);

            return cur;
        }

        public static StringBuilder get_Current_Layout_Name()
        { 
            StringBuilder s = new StringBuilder(100);
            Win32_Helpers.GetKeyboardLayoutName(s);

            return s;
        }

        public static IntPtr load_Layout(StringBuilder s,string langid)
        {
            IntPtr hkl = IntPtr.Zero;
            //need to switch to requested lang if it is differnt from current layout
            if (s.ToString() != langid)
            {
                hkl = Win32_Helpers.LoadKeyboardLayout(langid, (uint)Win32_Helpers.KLF.KLF_ACTIVATE);// | (uint)KLF.KLF_SETFORPROCESS);
                if (hkl == IntPtr.Zero)
                {
                    Console.WriteLine("Sorry, that keyboard does not seem to be valid.");
                }
            }
            else
            {
                Console.WriteLine("Requested Layout is current");
            }

            return hkl;
        }

        public static bool unload_Layout(StringBuilder s,string langid,IntPtr hkl)
        {
            bool b = false;
            if (s.ToString() != langid)
            {
                b = Win32_Helpers.UnloadKeyboardLayout(hkl);
                if (!b)
                {
                    Console.WriteLine(Marshal.GetLastWin32Error());
                }
            }
            
            return b;
        }

        public static string scancode_To_String(int vkey, int scanCode,IntPtr hkl)
        {
            byte[] keyStates = new byte[256];
            //wrong
            if (!Win32_Helpers.GetKeyboardState(keyStates))
                return string.Empty;

            StringBuilder key = new StringBuilder(10);
            int rc = Win32_Helpers.ToUnicodeEx((uint)vkey, (uint)scanCode, keyStates, key, key.Capacity, 0, hkl);

            if (rc == 0)
            {
                //value is scan code (left most 16 bits in lparam)
                key = new StringBuilder(10);
                int t = Win32_Helpers.GetKeyNameText(scanCode << 16, key, key.Capacity);
                if (t == 0)
                {
                    //Console.WriteLine("\n Error code: " + Marshal.GetLastWin32Error());
                    key.Append("[X]");
                }
            }

            return key.ToString();
        }

        public static string scancode_To_String(int vkey,int scanCode, string langid)
        {
            StringBuilder s = new StringBuilder(100);
            Win32_Helpers.GetKeyboardLayoutName(s);

            //current layout
            IntPtr hkl = Win32_Helpers.GetKeyboardLayout(0);

            //need to switch to requested lang if it is differnt from current layout
            if (s.ToString() != langid)
            {
                hkl = Win32_Helpers.LoadKeyboardLayout(langid, (uint)Win32_Helpers.KLF.KLF_ACTIVATE);
                if (hkl == IntPtr.Zero)
                {
                    Console.WriteLine("Sorry, that keyboard does not seem to be valid.");
                    return string.Empty;
                }
            }

            //wrong
            byte[] keyStates = new byte[256];
            if (!Win32_Helpers.GetKeyboardState(keyStates))
                return string.Empty;

            StringBuilder key = new StringBuilder(10);
            int rc = Win32_Helpers.ToUnicodeEx((uint)vkey, (uint)scanCode, keyStates, key, key.Capacity, 0, hkl);
            

            if (rc == 0)
            {
                //value is scan code (left most 16 bits in lparam)
                key = new StringBuilder(10);
                int t = Win32_Helpers.GetKeyNameText(scanCode << 16, key, key.Capacity);
                if (t == 0)
                {
                    //Console.WriteLine("\n Error code: " + Marshal.GetLastWin32Error());
                    key.Append("[X]");
                }
            }

            if (s.ToString() != langid)
            {
                bool b = Win32_Helpers.UnloadKeyboardLayout(hkl);
                if (!b)
                {
                    Console.WriteLine(Marshal.GetLastWin32Error());
                }
            }

            return key.ToString();
        }

        static Dictionary<string, List<string[]>> languages = new Dictionary<string, List<string[]>>();

        

        public static bool load_KBL_File(string kbl_file)
        {
            string lang = kbl_file.Replace(".txt", "");
            lang = lang.Replace(@"lang\", "");
            if(!languages.Keys.Contains(lang))
            {
                File_Access klc_file = new File_Access();
                klc_file.open_File(kbl_file);
                klc_file.attach_StreamReader();

                string[] split = klc_file.read_To_End().Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                List<string[]> kbl = new List<string[]>();

                int i = 0;
                foreach (string s in split)
                {
                    string[] r = s.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    kbl.Add(r);

                    //Console.Write("\n" + i + "->");

                    //foreach (string c in kbl[kbl.Count - 1])
                    //{
                    //    Console.Write(c + "|");
                    //}

                    i++;

                }

                klc_file.close_File();

                languages.Add(lang, kbl);

                return true;
            }

            return false;
        }

        public static int get_Char(string lang,int scan,int vkey,int caps,int shift,int ctrl,int alt,ref string scan_string)
        {

            List<string[]> kbl = languages[lang];

            //search in loaded kbl
            if(kbl.Count>0)
            {
                int i = 1;
                while(i<kbl.Count)
                {
                    string[] r = kbl[i];
                    if(r[0]=="LIGATURE")
                    {
                        //not supported as yet
                        break;
                    }
                    else
                    {
                        int sc = -1;
                        int.TryParse(r[0], System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out sc);
                        int vk = -1;
                        int.TryParse(r[1], System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out vk);
                        int cp = -1;
                        int.TryParse(r[2], System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out cp);

                        int ch      = -1;
                        int sh      = -1;
                        int cl      = -1;
                        int sh_cl   = -1;
                        int at      = -1;
                        int at_cl   = -1;

                        /*chars to choose from*/
                        if(r[3]!="-1")//char column
                        {
                            int.TryParse(r[3], System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture.NumberFormat,out ch);
                        }
                        if (r[4] != "-1")//shift column
                        {
                            int.TryParse(r[4], System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out sh);
                        }
                        if (r[5] != "-1")//ctrl column
                        {
                            int.TryParse(r[5], System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out cl);
                        }
                        if (r[6] != "-1")//shift+ctrl column
                        {
                            int.TryParse(r[6], System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out sh_cl);
                        }
                        if (r[7] != "-1")//alt column
                        {
                            int.TryParse(r[7], System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out at);
                        }
                        if (r[8] != "-1")//alt+ctrl column
                        {
                            int.TryParse(r[8], System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out at_cl);
                        }

                        //compare supplied values with current row
                        
                        
                            if (sc == scan && vk == vkey)
                            { 
                                //choose char based on flags
                            
                                if (alt == 1 && ctrl == 1)
                                {
                                    return at_cl;
                                }
                                else
                                    if(shift==1 && ctrl == 1)
                                    {
                                        return sh_cl;
                                    }
                                else
                                    if (ctrl == 1)
                                    {
                                        return cl;
                                    }
                                else
                                    if (shift==1)
                                    {
                                        if (caps == 1)//caps lock has precedence
                                        {
                                            int res = -1;
                                            if (int.TryParse(r[3], System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out res))
                                            {
                                                return res;
                                            }
                                        }

                                        return sh;
                                    }
                                else//not sure of place
                                    if (alt == 1)
                                    {
                                        return at;
                                    }
                                else//key names only
                                    {
                                        int res = -1;

                                        if (!int.TryParse(r[3], System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out res))
                                        { 
                                            scan_string = r[3]; //keyname
                                            return -2; 
                                        }
                                         
                                        return res; //regular char
                                        
                                    }
                            }
                            
                        
                        
                    }
                    i++;
                }
            }

            return -1;
        }
    }

    public class Text_Helper
    {
        

        public static Dictionary<string, string> formatted = new Dictionary<string, string>
                                                        {
                                                            { "[Enter]",        "\n"},
                                                            { "[Num *]",        "*"},
                                                            { "[Num 7]",        "7"},
                                                            { "[Num 8]",        "8"},
                                                            { "[Num 9]",        "9"},
                                                            { "[Num -]",        "-"},
                                                            { "[Num 4]",        "4"},
                                                            { "[Num 5]",        "5"},
                                                            { "[Num 6]",        "6"},
                                                            { "[Num +]",        "+"},
                                                            { "[Num 1]",        "1"},
                                                            { "[Num 2]",        "2"},
                                                            { "[Num 3]",        "3"},
                                                            { "[Num 0]",        "0"},
                                                            { "[Num Enter]",    "\n"},
                                                            { "[Num /]",         "/"},
                                                        };

        public static string replace_Cursor_Modifiers(string text)
        {
            string[] modifiers = {
                                    "[Insert]",
                                    "[Delete]",
                                    "[Num Del]",
                                    "[Bksp]",
                                 };
            //foreach (string m in modifiers)
            //{
            //    int i = 0;
            //    while( (i=text.IndexOf(m))>0 )
            //    {
            //        switch (m)
            //        {
            //            case "[Bksp]":
            //                {
            //                    //int index = text.IndexOf(c);
            //                    //text.Remove(index - 1, 2);
            //                }
            //            break;
            //        }
                    
            //    }
                
            //}

            return text;
        }

        //untested
        public static int[] find_All(string keyword,string source)
        {
            List<int> indices = new List<int>();
            

            int start_pos = 0;
            int index = 0;
            while((index = source.IndexOf(keyword,start_pos))!=-1)
            {
                indices.Add(index);
                start_pos = index + keyword.Length;
            }

            return indices.ToArray();
        }

        //untested
        public static string extract(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
                if (strSource.Contains(strStart) && strEnd=="")
                {
                    Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                    End = strSource.Length - Start;
                    return strSource.Substring(Start, End - Start);
                }
            else
            {
                return "";
            }
        }
    }

    public class HTML_Helper
    { 
        static public string get_HTML_String(string source)
        {
            string html = "";

            foreach(char c in source)
            {
                html+="&#"+(int)c+";";
            }

            return html;
        }
    }

    
}


