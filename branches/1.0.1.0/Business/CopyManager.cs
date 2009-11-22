using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Test.SqlCopy.Objects;

namespace Test.SqlCopy.Data
{
    public class CopyManager
    {
        private IDbData Db;
        private CopyObject Settings {get; set;}

        public CopyManager(CopyObject settings, IDbData db)
        {
            this.Settings = settings;
            this.Db = db;
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
