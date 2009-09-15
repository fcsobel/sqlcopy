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

        public IDataReader List()
        {
            return this.Db.List();
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
