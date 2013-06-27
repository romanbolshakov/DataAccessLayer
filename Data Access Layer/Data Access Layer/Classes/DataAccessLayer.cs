using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ALG
{
    namespace DataAccessLayer
    {
        /// <summary>
        /// Класс доступа к данным
        /// </summary>
        public class ClassDataAccessLayer
        {
            private List<Connection> _lstConnections;
            private int _intActiveIndex;
            private List<Query> _lstQueries;
            private DataTable _dtConnections;
            private bool _isTraceMode;
            private bool _hasMetaData;
            private ClassMetaData _metaData;

            #region Свойства класса

            /// <summary>
            /// Возвращает список зарегистрированных коннектов
            /// </summary>
            public List<Connection> GetConnections
            {
                get { return _lstConnections; }
            }

            /// <summary>
            /// Возвращает активный коннект
            /// </summary>
            public Connection GetActiveConnect
            {
                get { return _lstConnections[_intActiveIndex]; }
            }
            
            /// <summary>
            /// Возвращает объект класса с Мета Данными
            /// </summary>
            public ClassMetaData GetMetaData
            {
                get { return _metaData; }
            }

            /// <summary>
            /// Возвращает значение флага наличия Мета Данных
            /// </summary>
            public bool HasMetaData
            {
                get { return _hasMetaData; }
            }

            #endregion

            #region Конструкторы

            /// <summary>
            /// Констуктор класса.
            /// TraceMode = false;
            /// </summary>
            public ClassDataAccessLayer(DataTable TableConnections)
            {
                InitDataAccessLayer(TableConnections, false, false);
            }

            /// <summary>
            /// Констуктор класса.
            /// </summary>
            public ClassDataAccessLayer(DataTable TableConnections, bool isTraceMode)
            {
                InitDataAccessLayer(TableConnections, isTraceMode, false);
            }

            /// <summary>
            /// Констуктор класса.
            /// </summary>
            public ClassDataAccessLayer(DataTable TableConnections, bool isTraceMode, bool HasMetaData)
            {
                InitDataAccessLayer(TableConnections, isTraceMode, HasMetaData);
            }

            /// <summary>
            /// Конструктор. Читает XML 
            ///     isTraceMode = false
            /// </summary>
            /// <param name="PathToXML">Путь к файлу ХML</param>
            public ClassDataAccessLayer(string PathToXML)
            {
                DataTable dt = ReadXML(PathToXML);
                InitDataAccessLayer(dt, false, false);
            }

            /// <summary>
            /// Конструктор. Читает XML 
            /// </summary>
            /// <param name="PathToXML">Путь к файлу ХML</param>
            /// <param name="isTraceMode"></param>
            public ClassDataAccessLayer(string PathToXML, bool isTraceMode)
            {
                DataTable dt = ReadXML(PathToXML);
                InitDataAccessLayer(dt, isTraceMode, false);
            }

            /// <summary>
            /// Конструктор. Читает XML 
            /// </summary>
            /// <param name="PathToXML">Путь к файлу ХML</param>
            /// <param name="isTraceMode"></param>
            public ClassDataAccessLayer(string PathToXML, bool isTraceMode, bool HasMetaData)
            {
                DataTable dt = ReadXML(PathToXML);
                InitDataAccessLayer(dt, isTraceMode, HasMetaData);
            }

            /// <summary>
            /// Конструктор. Получает готовую строку подключения
            /// (Параметры умышленно поменяны местами для того чтобы отличаться 
            /// от аналогичного конструктора только с путем к файлу XML)
            /// </summary>
            /// <param name="isTraceMode"></param>
            /// <param name="ConnectionString"></param>
            public ClassDataAccessLayer(bool isTraceMode, string ConnectionString) {
                InitDataAccessLayer(ConnectionString, isTraceMode);
            }

            public ClassDataAccessLayer() {
                _lstConnections = new List<Connection>();
            }

            private DataTable ReadXML(string PathToXML) {
                DataSet ds = new DataSet();
                ds.ReadXml(PathToXML);
                return ds.Tables[0];
            }

            private void InitDataAccessLayer(DataTable TableConnections, bool isTraceMode, bool hasMetaData) {
                _dtConnections = TableConnections;
                _intActiveIndex = -1;
                InitConnections();
                _lstQueries = new List<Query>();
                _isTraceMode = isTraceMode;
                _hasMetaData = hasMetaData;
                if (hasMetaData) {
                    _metaData = new ClassMetaData(this);
                }
            }

            private void InitDataAccessLayer(string ConnectionString, bool isTraceMode) {
                _lstConnections = new List<Connection>();
                InitConnection(ConnectionString);
                _isTraceMode = isTraceMode;
            }

            #endregion

            #region Коннекты

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="InnerSender"></param>
            /// <param name="e"></param>
            public delegate void InfoMessageHandler(object sender, object InnerSender, System.Data.SqlClient.SqlInfoMessageEventArgs e);
            /// <summary>
            /// 
            /// </summary>
            public event InfoMessageHandler OnDBInfoMessage;

            /// <summary>
            /// Инициализация списка коннектов
            /// </summary>
            /// <returns></returns>
            private bool InitConnections()
            {
                _lstConnections = new List<Connection>();
                if (_dtConnections != null)
                {
                    Connection tempConnection;
                    foreach (DataRow dr in _dtConnections.Rows)
                    {
                        try
                        {
                            tempConnection = new Connection();
                            tempConnection.DataBaseName = dr["Base"].ToString();
                            tempConnection.ID = Convert.ToInt16(dr["ID"].ToString());
                            tempConnection.isWinAuth = Convert.ToBoolean(dr["WinAuth"].ToString());
                            tempConnection.Password = dr["Password"].ToString();
                            tempConnection.RusName = dr["RusName"].ToString();
                            tempConnection.ServerName = dr["Server"].ToString();
                            tempConnection.UserName = dr["User"].ToString();
                            tempConnection.isActive = Convert.ToBoolean(dr["isActive"].ToString());
                            if (_dtConnections.Columns.Contains("Role"))
                            {
                                tempConnection.Role = dr["Role"].ToString();
                            }
                            tempConnection.BuildConnection();
                            tempConnection.sqlConnection.InfoMessage += new System.Data.SqlClient.SqlInfoMessageEventHandler(sqlConnection_InfoMessage);
                            if ((_intActiveIndex == -1)
                                && (tempConnection.isActive)) 
                                _intActiveIndex = _lstConnections.Count;
                            _lstConnections.Add(tempConnection);
                        }
                        catch { }
                    }
                }
                return true;
            }

            private bool InitConnection(string ConnectionString) {
                AddConnection(ConnectionString);
                _intActiveIndex = 0;
                return true;
            }

            private void sqlConnection_InfoMessage(object sender, System.Data.SqlClient.SqlInfoMessageEventArgs e)
            {
                if (OnDBInfoMessage != null)
                {
                    OnDBInfoMessage(this, sender, e);
                }
            }

            /// <summary>
            /// Возвращает индекс коннекта в списке с указанным ID
            /// </summary>
            /// <param name="ID"></param>
            /// <returns></returns>
            private int GetConnectionIndexByID(int ID)
            {
                int result = -1;
                int i = -1;
                do
                {
                    i++;
                    if (_lstConnections[i].ID == ID)
                    {
                        result = i;
                    }
                }
                while (i < _lstConnections.Count && result == -1);
                return result;
            }

            /// <summary>
            /// Переключает активный коннект
            /// </summary>
            /// <param name="NewActiveID"></param>
            /// <returns></returns>
            public bool SwitchActiveConnect(int NewActiveID)
            {
                bool result;
                int newIndex = GetConnectionIndexByID(NewActiveID);
                if (newIndex != -1)
                {
                    _lstConnections[_intActiveIndex].isActive = false;
                    _intActiveIndex = newIndex;
                    _lstConnections[_intActiveIndex].isActive = true;
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;
            }

            /// <summary>
            /// Переключает активный коннект
            /// </summary>
            /// <param name="NewActiveIndex">Индекс нового активного коннекта</param>
            /// <returns></returns>
            public bool SwitchActiveConnectByIndex(int NewActiveIndex) {
                if (NewActiveIndex >= 0 && NewActiveIndex < _lstConnections.Count) {
                    _lstConnections[_intActiveIndex].isActive = false;
                    _intActiveIndex = NewActiveIndex;
                    _lstConnections[_intActiveIndex].isActive = true;
                    return true;
                }
                else return false;
            }

            /// <summary>
            /// Получает коннект по роли
            /// </summary>
            /// <param name="role"></param>
            /// <returns></returns>
            public Connection GetConnectionByRole(string role)
            {
                foreach (Connection con in _lstConnections)
                {
                    if (con.Role == role)
                    {
                        return con;
                    }
                }
                return null;
            }

            /// <summary>
            /// Добавляет новый коннект
            /// (по умолчанию этот коннект становится активным)
            /// </summary>
            /// <param name="ConnectionString">Полная строка подключения с Логином и Паролем</param>
            public void AddConnection(string ConnectionString) {
                Connection currentConnection = new Connection();
                currentConnection.InitConnection(ConnectionString);
                currentConnection.sqlConnection.InfoMessage += new System.Data.SqlClient.SqlInfoMessageEventHandler(sqlConnection_InfoMessage);
                _lstConnections.Add(currentConnection);
                _intActiveIndex = _lstConnections.Count - 1;
            }

            /// <summary>
            /// Добавляет новый коннект
            /// (по умолчанию этот коннект становится активным)
            /// </summary>
            /// <param name="CommonConnectionString">Полная строка подключения с Логином и Паролем</param>
            /// <param name="UserName">Имя пользователя для входа на сервер БД</param>
            /// <param name="Password">Пароль для входа на сервер БД</param>
            public void AddConnection(string CommonConnectionString, string UserName, string Password) {
                AddConnection(CommonConnectionString);
                _lstConnections[_lstConnections.Count - 1].UserName = UserName;
                _lstConnections[_lstConnections.Count - 1].Password = Password;
                _intActiveIndex = _lstConnections.Count - 1;
            }

            #endregion

            #region Комманды к БД

            /// <summary>
            /// Создание нового объекта для запроса к БД
            /// </summary>
            /// <param name="connection">Коннект</param>
            /// <returns>Объект запроса</returns>
            public Query CreateNewQuery(Connection connection)
            {
                Query tempQuery = new Query(connection, _isTraceMode);
                //_lstQueries.Add(tempQuery);
                return tempQuery;
            }



            /// <summary>
            /// Создание нового объекта для запроса к БД
            /// (использует активное подключение)
            /// </summary>
            /// <returns></returns>
            public Query CreateNewQuery()
            {
                return CreateNewQuery(_lstConnections[_intActiveIndex]);
            }

            #endregion

            #region Режим отладки

            /// <summary>
            /// Изменение состояния режима отладки
            /// </summary>
            /// <param name="NewState">Новое состояние:
            ///     1 - включить;
            ///     0 - выключить;</param>
            /// <returns></returns>
            public bool ChangeTraceMode(bool NewState)
            {
                _isTraceMode = NewState;
                return true;
            }

            #endregion Режим отладки
        }
    }
}
