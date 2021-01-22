using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Threading;

using System.Net.Sockets;

using NetWorking_1_0;
using IO_1_0;
using Symon_Data_Types_1_0;
using Helpers_1_0;

namespace Network_Monitor_1_0
{
    class Http_Get
    {
        public string server = "";
    };

    class Network_Monitor
    {
        public Network_Monitor()
        {
            message_count_symon = 0;
            symon = new Win_Socket_Server();
            // Incoming message from symon
            message_symon = null;
            // Response message to  symon
            response_symon = null;
            ip4_address_symon = null;
            port_symon = null;

            message_count_l_server_clients = new List<uint>();
            server = new Win_Socket_Server_2();
            // Incoming message from symon
            message_l_server_clients = new List<string>();
            // Response message to  symon
            response_l_server_clients = new List<string>();
            ip4_address_server = null;
            port_server = null;
            
            settings_ie = new File_Access();
            b_exit = false;
            b_pause = false;



        }

        ~Network_Monitor() { }

        Win_Socket_Server symon;
        string ip4_address_symon;
        string port_symon;
        // Incoming message from symon
        public byte[] message_symon;
        // Response message to  symon
        public byte[] response_symon;
        uint message_count_symon;

        Win_Socket_Server_2 server;
        string ip4_address_server;
        string port_server;
        // Request message from client
        public List<string> message_l_server_clients;
        // Response message to client
        public List<string> response_l_server_clients;

        List<uint> message_count_l_server_clients;

        bool b_exit;
        bool b_pause;

        File_Access settings_ie;
        string settings_file = "settings_nm";

        string user = "";
        string computer = "";
        string workgroup = "";
        string domain = "";

        Mutex m_lock_received_message = new Mutex();
        Mutex m_lock_disconnection = new Mutex();
        Mutex m_lock_connection = new Mutex();

        bool load_QS_Settings()
        {
            try
            {
                //read settings
                int length = 0;
                byte[] temp;

                //symon ip_address
                temp = new byte[sizeof(int)];
                settings_ie.read_File_To_Byte_Array(temp, 0, temp.Length);
                length = BitConverter.ToInt32(temp, 0);
                temp = new byte[length];
                settings_ie.read_File_To_Byte_Array(temp, 0, length);
                ip4_address_symon = System.Text.Encoding.UTF8.GetString(temp);

                //symon port
                temp = new byte[sizeof(int)];
                settings_ie.read_File_To_Byte_Array(temp, 0, temp.Length);
                length = BitConverter.ToInt32(temp, 0);
                temp = new byte[length];
                settings_ie.read_File_To_Byte_Array(temp, 0, length);
                port_symon = System.Text.Encoding.UTF8.GetString(temp);

                //server ip_address
                temp = new byte[sizeof(int)];
                settings_ie.read_File_To_Byte_Array(temp, 0, temp.Length);
                length = BitConverter.ToInt32(temp, 0);
                temp = new byte[length];
                settings_ie.read_File_To_Byte_Array(temp, 0, length);
                ip4_address_server = System.Text.Encoding.UTF8.GetString(temp);

                //symon port
                temp = new byte[sizeof(int)];
                settings_ie.read_File_To_Byte_Array(temp, 0, temp.Length);
                length = BitConverter.ToInt32(temp, 0);
                temp = new byte[length];
                settings_ie.read_File_To_Byte_Array(temp, 0, length);
                port_server = System.Text.Encoding.UTF8.GetString(temp);

                

                return true;
            }
            catch (Exception e)
            {
                Console.Write("\nload_QS_Settings failed.." + e.Message);
                return false;
            }


        }

        public void start()
        {
            SelectQuery query = new SelectQuery("Win32_ComputerSystem");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                foreach (ManagementObject mo in searcher.Get())
                {
                    if (mo["partofdomain"] != null)
                    {
                        domain = mo["domain"].ToString();
                    }

                    if (mo["workgroup"] != null)
                    {
                        workgroup = mo["workgroup"].ToString();
                    }
                }
            }

            if (domain == workgroup)
            {
                domain = "";
            }

            //object result = computer_system["Workgroup"];
            user = Environment.UserName;
            computer = Environment.MachineName;

            //we want to get 1 connections
            //symon
            symon.set_Max_Backlog(1);
            {
                settings_ie.update_File(settings_file);
                {
                    //settings_ie.close_File();
                    //settings_ie.create_File(settings_file);
                    ////local symon
                    //int length = "127.0.0.1".Length;
                    //settings_ie.write_Bytes(BitConverter.GetBytes(length), 0, sizeof(int));
                    //settings_ie.write_Bytes(System.Text.Encoding.UTF8.GetBytes("127.0.0.1"), 0, length);
                    //length = "10000".Length;
                    //settings_ie.write_Bytes(BitConverter.GetBytes(length), 0, sizeof(int));
                    //settings_ie.write_Bytes(System.Text.Encoding.UTF8.GetBytes("10000"), 0, length);

                    ////server
                    //length = "127.0.0.1".Length;
                    //settings_ie.write_Bytes(BitConverter.GetBytes(length), 0, sizeof(int));
                    //settings_ie.write_Bytes(System.Text.Encoding.UTF8.GetBytes("127.0.0.1"), 0, length);
                    //length = "11000".Length;
                    //settings_ie.write_Bytes(BitConverter.GetBytes(length), 0, sizeof(int));
                    //settings_ie.write_Bytes(System.Text.Encoding.UTF8.GetBytes("11000"), 0, length);




                    //settings_ie.close_File();
                    //settings_ie.update_File(settings_file);

                }

                load_QS_Settings();

                ////async listen for symon
                //symon.eprm = new Win_Socket.ext_Process_Received_Message(process_Received_Message_Async_Symon);
                //symon.set_Send_Buffer(1024);
                //symon.set_Receive_Buffer(1024);
                //symon.listen_For_Client_IPV4_Async(ip4_address_symon, port_symon);

                //async listen for symon
                //server.set_Max_Backlog(10);
                server.set_Max_Backlog(1);
                server.eprm = new Win_Socket.ext_Process_Received_Message(process_Received_Message_Async_Server);
                //server.epdiscon = new Win_Socket.ext_Process_Disconnection(process_Disconnection_Server);
                //server.epsm = new Win_Socket.ext_Process_Send_Message(process_Send_Message_Async_Server);
                server.epcon = new Win_Socket.ext_Process_Connection(process_Connection_Server);
                server.set_Send_Buffer(1024);
                server.set_Receive_Buffer(1024);
                server.listen_For_Client_IPV4_Async(ip4_address_server, port_server);


            }

        }

        public void run()
        {
            while (!b_exit)//exit when symon says bye(shutdown) or connect retries to symon fail
            {
                //if (!symon.is_Alive())
                //{
                //    symon.set_Send_Buffer(1024);
                //    symon.set_Receive_Buffer(1024);
                //    symon.listen_For_Client_IPV4_Async(ip4_address_symon, port_symon);
                //}
                //else
                //    if (!symon.connection_Attempt())
                //    {
                //        if (!symon.connection_Attempt())
                //        {
                //            if (!b_pause)
                //            {
                                

                //                //screen k = new screen();
                //                //k.type = Data_Types.SCREEN_SHOT;
                //                //if (image != null)
                //                //{
                //                //    SYSTEMTIME st = new SYSTEMTIME();
                //                //    st.collect(DateTime.Now);
                //                //    k.collect(image, image.Length,
                //                //                        Encoding.UTF8.GetBytes(user),
                //                //                        Encoding.UTF8.GetBytes(computer),
                //                //                        Encoding.UTF8.GetBytes(domain),
                //                //                        Encoding.UTF8.GetBytes(workgroup),
                //                //                        st);
                //                //    byte[] packet = k.byte_packet();
                //                //    //todo send back to symon for writing to qdata
                //                //    ie_packet p = new ie_packet();
                //                //    p.collect("SYMON_IE_DATA", packet);
                //                //    symon.send_Bytes_Message_Async(p.byte_packet());
                //                //    image = null;
                //                //    packet = null;
                //                //}
                //                //k.destroy();
                //            }
                //        }
                //    }

                //foreach(Win_Socket in 


                Thread.Sleep(10);
                

                //Console.WriteLine("\npress any key to continue..");
                //Console.ReadLine();

            }//while
        }

        public void stop()
        {


            settings_ie.close_File();

            //save_New_Data_File();

            if (symon != null)
            {
                //symon.send_Message("IE_SYMON_BYE");
                symon.shutdown_Socket();
            }

            if (server != null)
            {
                server.shutdown();
            }

            

            b_exit = true;
        }

        /*symon*/
        public void process_Received_Message_Async_Symon(Win_Socket client)
        {

            message_symon = client.received_Message();
            if (message_symon != null)
            {
                process_Symon_Packet(message_symon);
            }
        }

        void process_Symon_Packet(byte[] packet)
        {
            //byte[] packet_byte = packet;
            //int i = 0;
            //while (i < packet_byte.Length)
            //{
            //    byte[] command_length;
            //    //byte[] command;
            //    byte[] data_length;
            //    //byte[] data;
            //    int length = 0;

            //    byte[] remaining;

            //    if ((packet_byte.Length - 1) - i >= sizeof(int))
            //    {

            //        //command_length
            //        command_length = new byte[sizeof(int)];
            //        Buffer.BlockCopy(packet_byte, i, command_length, 0, command_length.Length);

            //        //command
            //        i += command_length.Length;
            //        //command = new byte[BitConverter.ToInt32(command_length,0)];
            //        //Buffer.BlockCopy(packet_byte, i, command, 0, command.Length );

            //        //data_length
            //        i += BitConverter.ToInt32(command_length, 0);
            //        //i+=command.Length;
            //        data_length = new byte[sizeof(int)];
            //        Buffer.BlockCopy(packet_byte, i, data_length, 0, data_length.Length);

            //        //data
            //        i += data_length.Length;
            //        //data = new byte[BitConverter.ToInt32(data_length, 0)];
            //        //Buffer.BlockCopy(packet_byte, i, data, 0, data.Length );

            //        i += BitConverter.ToInt32(data_length, 0);
            //        //i += data.Length;

            //        length = i;

            //        remaining = new byte[length];
            //        Buffer.BlockCopy(packet_byte, i - length, remaining, 0, length);
            //        pop_Data(remaining);

            //    }
            //}

            //pop_Data(packet);
            Console.WriteLine(System.Text.Encoding.UTF8.GetString(packet));
        }

        bool pop_Data(byte[] data)
        {
            ie_packet iep = new ie_packet();

            if (iep.unpack(data))
            {
                command_Match_Symon_Async(iep);
                return true;
            }

            return false;

        }

        public void command_Match_Symon_Async(ie_packet iep)
        {
            string msg = Encoding.UTF8.GetString(iep.command);
            switch (msg)
            {
                case "SYMON_IE_PAUSE":
                    {
                        b_pause = true;
                    }
                    break;
                case "SYMON_IE_CONTINUE":
                    {
                        b_pause = false;
                        //send ack to symon ?
                    }
                    break;
                case "SYMON_IE_KILL":
                    {
                        stop();
                        //send ack to symon ?
                    }
                    break;
                case "IE_DATA_ACK":
                    {
                        Console.Write("\n" + msg);
                    }
                    break;
                default:
                    {
                        Console.Write("\nUnknown message: " + msg);
                    }
                    break;
            }

            message_count_symon++;
            Console.Write(" symon_count= " + message_count_symon);
        }

        bool pop_Data_Symon(byte[] packet)
        {

            int type;

            byte[] temp = new byte[sizeof(int)];
            Buffer.BlockCopy(packet, 0, temp, 0, temp.Length);
            type = BitConverter.ToInt32(temp, 0);

            byte[] remaining = new byte[packet.Length - temp.Length];
            Buffer.BlockCopy(packet, temp.Length, remaining, 0, remaining.Length);

            switch (type)
            {
                //case Data_Types.SCREEN_SHOT:
                //    {
                //        screen k = new screen();
                //        k.type = Data_Types.SCREEN_SHOT;
                //        k.unpack(remaining);
                //        //encode screen_shot from bmp to jpeg
                //        byte[] image = null;
                //        Graphics_Helpers.encode_BMP_To_JPEG(ref k.screen_shot, ref image);
                //        if (image != null)
                //        {
                //            k.screen_shot = image;
                //            k.screen_shot_length = image.Length;
                //            //packet = k.byte_packet();
                //            //todo send back to symon for writing to qdata
                //            ie_packet p = new ie_packet();
                //            p.collect("SYMON_IE_DATA", packet);
                //            symon.send_Bytes_Message_Async(p.byte_packet());
                //            image = null;
                //        }

                //        k.destroy();
                //    }
                //    return true;
                default:
                    {
                        Console.Write("\nno match for type: " + type);
                        return false;
                    }
            }



        }

        /*Server*/
        public void process_Connection_Server(Win_Socket socket)
        {
            //lock
            //if (m_lock_connection.WaitOne(Timeout.Infinite))
            {
                //Console.WriteLine("\nentering con: " + Thread.CurrentThread.ManagedThreadId);
                int index = server.clients.FindIndex(item => item == socket);

                message_l_server_clients.Add("");

                Console.WriteLine("\nclients: " + server.clients.Count);
                //Console.WriteLine("leaving con: " + Thread.CurrentThread.ManagedThreadId);
               // m_lock_connection.ReleaseMutex();
               // Thread.Sleep(1000);
            }

        }
        public void process_Disconnection_Server(Win_Socket socket)
        {
            //lock
            //if(m_lock_disconnection.WaitOne(Timeout.Infinite))
            {
                //Console.WriteLine("\nentering discon: " + Thread.CurrentThread.ManagedThreadId);
                int index = server.clients.FindIndex(item => item == socket);

                server.remove_Client(socket);
                message_l_server_clients.RemoveAt(index);
                
                Console.WriteLine("\nclients: " + server.clients.Count);
                //Console.WriteLine("leaving discon: " + Thread.CurrentThread.ManagedThreadId);
                //m_lock_disconnection.ReleaseMutex();
                //Thread.Sleep(1000);
            }
            
        }
        public void process_Send_Message_Async_Server(Win_Socket socket)
        {
            process_Disconnection_Server(socket);
        }
        public void process_Received_Message_Async_Server(Win_Socket socket)
        {

            //if (m_lock_received_message.WaitOne(Timeout.Infinite))
            {
                //Console.WriteLine("entering recieve: " + Thread.CurrentThread.ManagedThreadId);
                Win_Socket client = socket as Win_Socket;
                int index = server.clients.FindIndex(item => item == socket);


                byte[] message = client.received_Message();
                if (message != null)
                {
                    //if (index == message_l_server_clients.Count)//add new message store for new client
                    {
                        //message_l_server_clients.Add(Encoding.UTF8.GetString(message));
                        //Console.Write(Encoding.UTF8.GetString(message));
                    }
                    //else
                    {
                        message_l_server_clients[index] += Encoding.UTF8.GetString(message);
                        //Console.Write(Encoding.UTF8.GetString(message));
                    }

                }

                if (!client.is_Data_Available())
                {
                    //Console.WriteLine("left?:" + socket.handler.Available);
                    //Console.WriteLine("\n"+message_l_server_clients[index]);
                    process_Client_Packet(message_l_server_clients[index], index);

                    message_l_server_clients[index] = "";
                }

                //Thread.Sleep(1000);
            }

        }
        Http_Get extract_Get_Header(string message)
        {
            //lock
            Http_Get get = new Http_Get();
            
            string[] parts = message.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            get.server = parts[0].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[1];

            return get;
            
        }
        void process_Client_Packet(string message, int index)
        {
            
            Console.WriteLine("\nclient:" + index);

            string ip = "";

            Http_Get get = new Http_Get();

            if (message.ToLower().Contains("get"))
            {
                get = extract_Get_Header(message);

                ip = get.server;
            }


            Web_Client wbc = new Web_Client();
            wbc.set_Address(ip);
            byte[] data = wbc.download_Data();
            //Console.WriteLine(Encoding.UTF8.GetString(data));
            if(data!=null && data.Length>0)
            {
                server.clients[index].send_Bytes_Message_Async(data);
            }

            //m_lock_received_message.ReleaseMutex();
            //Console.WriteLine("leaving recieve: " + Thread.CurrentThread.ManagedThreadId);

            
        }
        


    }//class QStorage_Client
}
