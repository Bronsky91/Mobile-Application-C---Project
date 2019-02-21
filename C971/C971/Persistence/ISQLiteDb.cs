using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace C971
{
    public interface ISQLiteDb
    {
       SQLiteAsyncConnection GetConnection();
    }
}
