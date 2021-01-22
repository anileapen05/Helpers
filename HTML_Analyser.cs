using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;
using System.Text;

using HtmlAgilityPack;
using Helpers_1_0;

using IO_1_0;

namespace NetWorking_1_0
{
    class HTML_Analyser
    {
        HtmlDocument doc;

        string address;

        File_Access file = new File_Access();

        Dictionary<string, List<HtmlNode>> nodes = new Dictionary<string,List<HtmlNode>>();

        void track_Node(HtmlNode node)
        { 
            if(nodes.Keys.Contains(node.Name))
            {
                nodes[node.Name].Add(node);
                
            }
            else
            {
                nodes.Add(node.Name, new List<HtmlNode>());
                nodes[node.Name].Add(node);
            }
        }

        void get_Nodes(HtmlNode node)
        {
            try
            {

                foreach (HtmlNode child in node.ChildNodes)
                {
                    track_Node(child);
                    get_Nodes(child);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("get_Nodes failed.." + e.Message);
            }
        }

        void create_Node_List()
        {
            try
            {
                nodes.Clear();
                track_Node(doc.DocumentNode);
                get_Nodes(doc.DocumentNode);
            }
            catch (Exception e)
            {
                Console.WriteLine("create_Node_List failed.." + e.Message);
            }
        }

        public void load_String(string html, string ext_address)
        {
            try
            {
                address = ext_address;

                doc = new HtmlDocument();

                doc.LoadHtml(html);

                create_Node_List();
            }
            catch (Exception e)
            {
                Console.WriteLine("load_String failed.." + e.Message);
            }
        }

        public void load_Page(string abs_file_name, string ext_address)
        {
            try
            {
                if (file.file_Exists(abs_file_name))
                {
                    address = ext_address;

                    doc = new HtmlDocument();

                    doc.Load(abs_file_name);

                    create_Node_List();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("load_Page failed.." + e.Message);
            }
        }

        public string get_Page_Text()
        {
            try
            {
                return doc.DocumentNode.InnerHtml;
            }
            catch(Exception e)
            {
                Console.WriteLine("get_Page_Text failed.." + e.Message);
                return null;
            }
        }

        public DataTable nodes_To_Table()
        {
            try
            {

                DataTable dt = new DataTable();

                dt.Columns.Add("Name");
                dt.Columns.Add("Parent");
                dt.Columns.Add("Path");
                dt.Columns.Add("Index");
                dt.Columns.Add("Attributes");

                foreach(string name in nodes.Keys)
                {
                    foreach (HtmlNode node in nodes[name])
                    {
                        string[] row_data;
                        string parent_name = "";

                        if (node.ParentNode != null)
                        {
                            parent_name = node.ParentNode.Name;
                        }

                        StringBuilder attributes = new StringBuilder();
                        foreach (HtmlAttribute attribute in node.Attributes)
                        {
                            attributes.Append(attribute.Name + " = " + attribute.Value+";");
                        }

                        row_data = new string[] { node.Name, parent_name, node.XPath, nodes[name].IndexOf(node).ToString(),attributes.ToString() };

                        dt.Rows.Add(row_data);
                    }
                }

                return dt;
            }
            catch(Exception e)
            {
                Console.WriteLine("nodes_To_Table.."+e.Message);
                return null;
            }


        }

        public List<HtmlNode> get_Nodes(string name)
        {
            try
            {

                if (nodes.Keys.Contains(name))
                {
                    return nodes[name];
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("get_Element.." + e.Message);
                return null;
            }


        }
        
        public HtmlNode get_Node(
            string name,
            long index)
        {
            try
            {
                if (index < nodes[name].Count)
                {
                    return nodes[name][(int)index];
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("get_Node failed.." + e.Message);
                return null;
            }

        }

        public bool is_Valid()
        {
            try
            {
                if(nodes.Count>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("is_Valid failed.." + e.Message);
                return false;
            }
        }

        public List<HtmlParseError> get_Errors()
        {
            try
            {
                return doc.ParseErrors.ToList();
            }
            catch(Exception e)
            {
                Console.WriteLine("get_Errors failed.."+e.Message);
                return null;
            }

        }

        public string get_Address()
        {
            try
            {
                return address;
            }
            catch(Exception e)
            {
                Console.WriteLine("get_Address failed.." + e.Message);
                return null;
            }
        }

        //untested
        public void save_Doc(string abs_dest_dir)
        {
            string full_path = Networking_Helpers.get_File_Path(new Uri(address), abs_dest_dir);

            if(full_path!=null)
            {
                if (Directory_Helpers.create_Path(full_path))
                {
                    doc.Save(full_path);
                }
            }
            
        }

        public int get_Nodes_Count()
        {
            try
            {
                return nodes.Count;
            }
            catch(Exception e)
            {
                Console.WriteLine("get_Nodes_Count failed.." + e.Message);
                return -1;
            }
        }

        public HtmlNode get_Top_Node()
        {
            try
            {
                return doc.DocumentNode;
            }
            catch(Exception e)
            {
                Console.WriteLine("get_Top_Node failed.." + e.Message);
                return null;
            }
        }
    }
}
