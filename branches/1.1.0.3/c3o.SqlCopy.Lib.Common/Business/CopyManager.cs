using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using c3o.SqlCopy.Objects;
using System.IO;

namespace c3o.SqlCopy.Data
{
    public class CopyManager
    {
        private IDbData Db;
        private CopyObject Settings {get; set;}

        public CopyManager(CopyObject settings)
        {
            this.Settings = settings;

            switch (settings.SourceType)
            {
                case DBMS.Oracle:
                    this.Db = new OracleData(settings);
                    break;
                default:
                    this.Db = new SqlData(settings);
                    break;
            }

            //this.Db = db;
        }


        public static void RunCopyJobs()
        {
            string path = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).FullName;

            // Get jobs
            List<CopyObject> list = SerializationHelper.Deserialize<List<CopyObject>>(path + @"\config\list.xml");

            foreach (CopyObject obj in list)
            {
                if (obj.Selected)
                {
                    // Clear status
                    foreach (TableObject table in obj.Tables)
                    {
                        table.Status = "";
                    }

                    CopyManager manager = new CopyManager(obj);

                    // Run copy job
                    manager.Copy();

                    // Log the copy
                    manager.Log();

                    // Save Results
                    SerializationHelper.Serialize<List<CopyObject>>(list, path + @"\config\list.xml");
                }
            }
        }


        public void Log()
        {
            string file = String.Format(@"{0}\log\{1}.xml", new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).FullName, this.Settings.Name);

            List<CopyObject> list = null;

            // Get log
            if (File.Exists(file))
            {
                list = SerializationHelper.Deserialize<List<CopyObject>>(file);
            }
            else
            {
                list = new List<CopyObject>();
            }

            list.Insert(0, this.Settings);

            if (this.Settings.LogLimit > 0 && list.Count > this.Settings.LogLimit)
            {
                list.RemoveRange(this.Settings.LogLimit, list.Count - this.Settings.LogLimit);
            }

            // Save Results
            SerializationHelper.Serialize<List<CopyObject>>(list, file);
        }


        public void Copy()
        {
            try
            {
                this.PreCopy();
            }
            catch (Exception er)
            {
                this.Settings.PreCopyStatus = er.Message;
                return;
            }
            
            foreach (TableObject obj in this.Settings.Tables)
            {
                try
                {
                    if (obj.Selected)
                    {
                        this.Copy(obj.Name);
                        obj.Status = "Success";
                    }
                }
                catch (Exception er)
                {
                    obj.Status = er.Message;
                }
            }

            try
            {
                this.PostCopy();
            }
            catch (Exception er)
            {
                this.Settings.PostCopyStatus = er.Message;
            }
        }

        public void Copy(string table)
        {
            this.Db.Copy(table);
        }

        public void Delete(string table)
        {
            this.Db.Delete(table);
        }

        public List<TableObject> List()
        {
            //return this.Db.List();

            List<TableObject> list = new List<TableObject>();

            using (IDataReader dr = this.Db.List())
            {
                while (dr.Read())
                {
                    TableObject obj = new TableObject();

                    obj.Name = dr["table_name"] as string;
                    obj.Selected = false;
                    obj.Status = "";

                    list.Add(obj);
                    //this.dataGridView1.Rows.Add(true, dr["table_name"].ToString(), "");
                }
            }

            return list;

        }

        public void PostCopy()
        {
            this.Db.PostCopy();
        }

        public void PreCopy()
        {
            this.Db.PreCopy();
        }
    }
}
