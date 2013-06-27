using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace ALG
{
    namespace DataAccessLayer
    {
        /// <summary>
        /// Класс подключений к БД
        /// </summary>
        public class Connection
        {
            #region .:Закрытые переменные:.

            //private ADODB.Connection _adoconnection;
            //private MSDASC.DataLinksClass _msdasc;

            private SqlConnection _connection;
            private string _strRusName;
            private int _intID;
            private string _strServerName;
            private string _strDataBaseName;
            private string _strUserName;
            private string _strPassword;
            private bool _isWinAuth;
            private bool _isActive;
            private string _role;

            public string Role
            {
                get { return _role; }
                set { _role = value; }
            }

            #endregion .:Закрытые переменные:.

            #region Свойства класса

            /// <summary>
            /// Наименование коннекта (по русски)
            /// </summary>
            public string RusName
            {
                get { return _strRusName; }
                set { _strRusName = value; }
            }

            /// <summary>
            /// Идентификатор коннекта
            /// </summary>
            public int ID
            {
                get { return _intID; }
                set { _intID = value; }
            }

            /// <summary>
            /// Имя сервера
            /// </summary>
            public string ServerName
            {
                get { return _strServerName; }
                set { _strServerName = value; }
            }

            /// <summary>
            /// Имя БД
            /// </summary>
            public string DataBaseName
            {
                get { return _strDataBaseName; }
                set { _strDataBaseName = value; }
            }

            /// <summary>
            /// Имя пользователя (имя входа)
            /// </summary>
            public string UserName
            {
                get { return _strUserName; }
                set {
                    if (_strUserName != value) {
                        SqlConnectionStringBuilder builder = 
                            new SqlConnectionStringBuilder(_connection.ConnectionString);
                        builder.UserID = value;
                        _connection.ConnectionString = builder.ConnectionString;
                        _strUserName = value;
                    }
                }
            }

            /// <summary>
            /// Пароль
            /// </summary>
            public string Password
            {
                get { return _strPassword; }
                set {
                    if (_strPassword != value) {
                        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(_connection.ConnectionString);
                        builder.Password = value;
                        _connection.ConnectionString = builder.ConnectionString;
                        _strPassword = value;
                    }
                }
            }

            /// <summary>
            /// Использовать Windows-проверку пользователя или нет
            /// </summary>
            public bool isWinAuth
            {
                get { return _isWinAuth; }
                set { _isWinAuth = value; }
            }

            /// <summary>
            /// Флаг активного коннекта
            /// </summary>
            public bool isActive
            {
                get { return _isActive; }
                set { _isActive = value; }
            }

            /// <summary>
            /// Коннект
            /// </summary>
            public SqlConnection sqlConnection
            {
                get { return _connection; }
            }


            #endregion Свойства класса

            //public Connection()
            //{
                //_msdasc = new MSDASC.DataLinksClass();
            //}

            /// <summary>
            /// Строитель коннекта
            /// </summary>
            /// <returns></returns>
            public bool BuildConnection()
            {
                _connection = new SqlConnection();
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = _strServerName;
                builder.InitialCatalog = _strDataBaseName;
                builder.IntegratedSecurity = _isWinAuth;
                builder.Password = _strPassword;
                builder.UserID = _strUserName;
                _connection.ConnectionString = builder.ConnectionString;
                return true;
            }

            public SqlConnectionStringBuilder GetConnectionStringBuilder(string ConnectionString) {
                SqlConnectionStringBuilder result = new SqlConnectionStringBuilder(ConnectionString);
                return result;
            }

            public bool InitConnection(string ConnectionString) {
                _connection = new SqlConnection();
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConnectionString);
                _connection.ConnectionString = ConnectionString;
                _strServerName = builder.DataSource;
                _strDataBaseName = builder.InitialCatalog;
                _isWinAuth = builder.IntegratedSecurity;
                _strPassword = builder.Password;
                _strUserName = builder.UserID;
                return true;
            }

            /*private void BuildConnection()
            {
                CheckResult cr = CheckConnection();
                switch (cr)
                {
                    case CheckResult.Empty:
                        _adoconnection = (ADODB.Connection)_msdasc.PromptNew();
                        _connection.ConnectionString = _adoconnection.ConnectionString.Substring(8);
                        BuildConnection();
                        break;

                    case CheckResult.Error:
                        break;

                    case CheckResult.Success:
                        break;

                    case CheckResult.Unsuccess:
                        _adoconnection.ConnectionString = _connection.ConnectionString;
                        object conn = new object();
                        conn = (object)_adoconnection;
                        _msdasc.PromptEdit(ref conn);
                        BuildConnection();
                        break;
                }

            }*/

        }
    }
}
