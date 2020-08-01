using MySql.Data.MySqlClient;
using System;
using System.Data;
using UnityEngine;

public class MySQLManager
{
    public static MySqlConnection SqlConnection;//连接类对象

    //private static string host = "219.235.6.205";     //IP地址。如果只是在本地的话，写localhost就可以。
    //private static string userid = "_03Learnjava";       //用户名。
    //private static string pwd = "!QAZ2wsx";      //密码。
    //private static string dataBase = "_04chen_mysql"; //数据库名称。

    private static string host = "rm-uf655z4n9sg2kmrz93o.mysql.rds.aliyuncs.com";     //IP地址。如果只是在本地的话，写localhost就可以。
    private static string userid = "_10chen_mysql";       //用户名。
    private static string pwd = "_10chen_mysql12345";      //密码。
    private static string dataBase = "_10chen_mysql"; //数据库名称。


    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="_host">IP地址</param>
    /// <param name="_id">用户名</param>
    /// <param name="_pwd">密码</param>
    /// <param name="_dataBase">数据库名称</param>
    public MySQLManager()
    {

        //OpenDatabase();
    }


    /// <summary>
    /// 测试是否链接上数据库
    /// </summary>
    /// <returns></returns>
    public bool TestConnection()
    {
        string SqlString = string.Format("Database={0};Data Source={1};User Id={2};Password={3};", dataBase, host, userid, pwd, "3306");

        bool isConnected = true;
        //发送数据库连接字段 创建连接通道
        using (MySqlConnection connection = new MySqlConnection(SqlString))
        {
            try
            {
                //打开连接通道
                connection.Open();
            }
            catch (MySqlException E)
            {
                //如果有异常 则连接失败
                isConnected = false;
                throw new Exception(E.Message);
            }
            finally
            {
                //关闭连接通道
                connection.Close();
            }
        }

        return isConnected;
    }

    /// <summary>  
    /// 打开数据库  
    /// </summary>  
    public void OpenDatabase()
    {
        string SqlString = string.Format("Database={0};Data Source={1};User Id={2};Password={3};charset=gbk", dataBase, host, userid, pwd, "3306");

        try
        {
            SqlConnection = new MySqlConnection(SqlString);
            SqlConnection.Open();


            Debug_Log.Call_WriteLog("打开数据库");

        }
        catch (Exception e)
        {
            Debug_Log.Call_WriteLog(e, "服务器连接失败，请重新检查是否打开MySql服务");
            Debug_Log.Call_WriteLog(SqlString, "服务器连接失败，请重新检查是否打开MySql服务");
            //InputIP.ReadIP();
        }
    }


    /// <summary>
    /// 通过SQL语句查询是否数据库存在某表
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public bool ExitTable(string sql)
    {

        OpenDatabase();
        MySqlCommand cmd = new MySqlCommand(sql, SqlConnection);
        MySqlDataReader reader = cmd.ExecuteReader();
        if (reader.HasRows == false)
        {
            Debug_Log.Call_WriteLog("该表不存在");
            CloseDataBase();
            return false;
        }
        else
        {
            Debug_Log.Call_WriteLog("表存在");
            CloseDataBase();
            return true;
        }

    }
    /// <summary>  
    /// 创建表  
    /// </summary>  
    /// <param name="name">表名</param>  
    /// <param name="colName">属性列</param>  
    /// <param name="colType">属性类型</param>  
    /// <returns></returns>  
    public DataSet CreateTable(string name, string[] colName, string[] colType)
    {
        if (colName.Length != colType.Length)
        {
            Debug_Log.Call_WriteLog("输入不正确：" + "columns.Length != colType.Length");
        }

        string query = "CREATE TABLE  " + name + "(" + colName[0] + " " + colType[0];
        for (int i = 1; i < colName.Length; i++)
        {
            query += "," + colName[i] + " " + colType[i];
        }
        query += ")";
        return QuerySet(query);
    }

    /// <summary>  
    /// 创建具有id自增的表  
    /// </summary>  
    /// <param name="name">表名</param>  
    /// <param name="col">属性列</param>  
    /// <param name="colType">属性列类型</param>  
    /// <returns></returns>  
    public bool CreateTableAutoID(string name, string[] col, string[] colType)
    {
        if (col.Length != colType.Length)
        {
            //  throw new Exception("columns.Length != colType.Length");
            Debug_Log.Call_WriteLog("输入不正确：" + "columns.Length != colType.Length");
        }
        string query = "CREATE TABLE  " + name + " (" + col[0] + " " + colType[0] + " NOT NULL AUTO_INCREMENT";
        for (int i = 1; i < col.Length; ++i)
        {
            query += ", " + col[i] + " " + colType[i];
        }
        query += ", PRIMARY KEY (" + col[0] + ")" + ")";

        if (ExecuteSqlCmd(query))
        {
            return true;
        }
        else
        {
            return false;
        }


    }


    /// <summary>  
    /// 插入部分ID  
    /// </summary>  
    /// <param name="tableName">表名</param>  
    /// <param name="col">属性列</param>  
    /// <param name="values">属性值</param>  
    /// <returns></returns>  
    public DataSet InsertInto(string tableName, string[] col, string[] values)
    {
        if (col.Length != values.Length)
        {
            // throw new Exception("columns.Length != colType.Length");
            Debug_Log.Call_WriteLog("输入不正确：" + "columns.Length != colType.Length");
        }
        string query = "INSERT INTO " + tableName + " (" + col[0];
        for (int i = 1; i < col.Length; ++i)
        {
            query += ", " + col[i];
        }
        query += ") VALUES (" + "'" + values[0] + "'";
        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + "'" + values[i] + "'";
        }
        query += ")";
        return QuerySet(query);
    }

    /// <summary>  
    /// 查询表数据 
    /// </summary>  
    /// <param name="tableName">表名</param>  
    /// <param name="items">需要查询的列</param>  
    /// <param name="whereColName">查询的条件列</param>  
    /// <param name="operation">条件操作符</param>  
    /// <param name="value">条件的值</param>  
    /// <returns></returns>  
    public DataSet Select(string tableName, string[] items, string[] whereColName, string[] operation, string[] value)
    {
        if (whereColName.Length != operation.Length || operation.Length != value.Length)
        {
            // throw new Exception("输入不正确：" + "col.Length != operation.Length != values.Length");
            Debug_Log.Call_WriteLog("输入不正确：" + "col.Length != operation.Length != values.Length");
        }
        string query = "SELECT " + items[0];
        for (int i = 1; i < items.Length; i++)
        {
            query += "," + items[i];
        }
        query += "  FROM  " + tableName + "  WHERE " + " " + whereColName[0] + operation[0] + " '" + value[0] + "'";
        for (int i = 1; i < whereColName.Length; i++)
        {
            query += " AND " + whereColName[i] + operation[i] + "' " + value[i] + "'";
        }
        return QuerySet(query);
    }
    /// <summary>
    /// 读取数据  读取数据库指定表中的指定数据
    /// </summary>
    /// <param name="column_name">所要查询数据的列名称或以*代替</param>
    /// <param name="index">列索引号</param>
    /// <param name="table_name">表名称</param>
    /// <param name="ConditonVar">依据条件列名称</param>>
    /// <param name="Condtionvalue">依据条件列的值 </param>>
    /// <returns></returns>
    public string GetValueByRead(string column_name, int index, string table_name, string ConditonVar, string Condtionvalue)
    {
        OpenDatabase();
        string _value = "";
        MySqlCommand cmd = new MySqlCommand();
        cmd.CommandText = "select " + column_name + " from " + table_name + " where " + ConditonVar + " = '" + Condtionvalue + "'";
        cmd.Connection = SqlConnection;
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            _value = reader[index].ToString();
        }
        reader.Close();
        //  Close();
        return _value;
    }

    /// <summary>  
    /// 更新表数据 
    /// </summary>  
    /// <param name="tableName">表名</param>  
    /// <param name="cols">更新列</param>  
    /// <param name="colsvalues">更新的值</param>  
    /// <param name="selectkey">条件：列</param>  
    /// <param name="selectvalue">条件：值</param>  
    /// <returns></returns>  
    public DataSet UpdateInto(string tableName, string[] cols, string[] colsvalues, string selectkey, string selectvalue)
    {
        string query = "UPDATE " + tableName + " SET " + cols[0] + " = " + "'" + colsvalues[0] + "'";
        for (int i = 1; i < colsvalues.Length; ++i)
        {
            query += ", " + cols[i] + " =" + "'" + colsvalues[i] + "'";
        }
        query += " WHERE " + selectkey + " = " + "'" + selectvalue + "'" + " ";
        return QuerySet(query);
    }

    /// <summary>  
    /// 删除表数据  
    /// </summary>  
    /// <param name="tableName">表名</param>  
    /// <param name="cols">条件：删除列</param>  
    /// <param name="colsvalues">删除该列属性值所在得行</param>  
    /// <returns></returns>  
    public DataSet Delete(string tableName, string[] cols, string[] colsvalues)
    {
        string query = "DELETE  FROM " + tableName + " WHERE " + cols[0] + " = " + colsvalues[0];
        for (int i = 1; i < colsvalues.Length; ++i)
        {
            query += " or " + cols[i] + " = " + colsvalues[i];
        }
        return QuerySet(query);
    }
    /// <summary>
    /// 按照表名删除数据库中的表
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public bool DeleteTable(string tableName)
    {
        string query = "DROP TABLE " + tableName;
        return ExecuteSqlCmd(query);
    }

    /// <summary>
    /// 释放
    /// </summary>
    public void CloseDataBase()
    {
        if (SqlConnection != null)
        {
            SqlConnection.Close();
            SqlConnection.Dispose();
            SqlConnection = null;
        }
        Debug_Log.Call_WriteLog("关闭数据库");
    }

    /// <summary>    
    /// 执行Sql语句  
    /// </summary>  
    /// <param name="sqlString">sql语句</param>  
    /// <returns></returns>  
    public DataSet QuerySet(string sqlString)
    {
        OpenDatabase();
        if (SqlConnection.State == ConnectionState.Open)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(sqlString, SqlConnection);
                mySqlDataAdapter.Fill(ds);
            }
            catch (Exception e)
            {
                // throw new Exception("SQL:" + sqlString + "/n" + e.Message.ToString());
                string strMessage = "SQL:" + sqlString + "/n" + e.Message.ToString();
                Debug_Log.Call_WriteLog(strMessage);
                Debug_Log.Call_WriteLog(sqlString, "sqlString", "Unity");
                Debug_Log.Call_WriteLog(e, "QuerySet", "Unity");
            }
            finally
            {
                CloseDataBase();
            }
            //Debug_Log.Call_WriteLog(Newtonsoft.Json.JsonConvert.SerializeObject(ds), "执行Sql语句ds  ", "Unity");
            return ds;
        }
        return null;
    }
    /// <summary>
    /// 通过sql语句返回查询行数
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public int SqlRecordCount(string sql)
    {
        OpenDatabase();
        MySqlCommand cmd = new MySqlCommand();
        cmd.CommandText = sql;
        cmd.Connection = SqlConnection;
        MySqlDataReader dr;
        dr = cmd.ExecuteReader();
        int RecordCount = 0;
        while (dr.Read())
        {
            RecordCount = RecordCount + 1;
        }
        CloseDataBase();
        return RecordCount;
    }
    /// <summary>
    /// 获取表中指定条件下，某字段的行数
    /// </summary>
    /// <param name="column_name"></param>
    /// <param name="table_name"></param>
    /// <param name="condition"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public int GetDataLineCount(string column_name, string table_name, string condition, string value)
    {
        int _Num = 0;
        OpenDatabase();
        MySqlCommand cmd = new MySqlCommand();
        cmd.CommandText = "select " + column_name + " from " + table_name + " where find_in_set(" + value + "," + condition + ")"; //find_in_set(1,position)
        Debug_Log.Call_WriteLog(cmd.CommandText);
        cmd.Connection = SqlConnection;
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            _Num = _Num + 1;
        }
        reader.Close();
        CloseDataBase();
        return _Num;
    }
    /// <summary>
    /// 执行sql语句并返回是否执行完成
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public bool ExecuteSqlCmd(string sql)
    {
        Debug_Log.Call_WriteLog(sql);
        OpenDatabase();
        if (SqlConnection.State == ConnectionState.Open)
        {
            MySqlCommand cmd = new MySqlCommand(sql, SqlConnection);
            return cmd.ExecuteNonQuery() > 0;
        }
        CloseDataBase();
        return false;
    }



}

