using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ALG
{
    namespace DataAccessLayer
    {
        /// <summary>
        /// Класс запросов
        /// </summary>
        public class Query
        {
            #region Закрытые переменные

            private Parameters _parameters;
            private Connection _connection;
            private SqlDataAdapter _adapter;
            private bool _isTraceMode;

            #endregion Закрытые переменные

            #region Свойства класса


            #endregion Свойства класса

            #region Конструкторы

            /// <summary>
            /// Инициализирует Parameters.
            /// Присваивает Connection
            /// </summary>
            /// <param name="Connection">Объкт подключения к БД</param>
            /// <param name="isTraceMode">Флаг отладки</param>
            public Query(Connection Connection, bool isTraceMode)
            {
                _connection = Connection;
                _parameters = new Parameters();
                _isTraceMode = isTraceMode;
            }

            #endregion Конструкторы

            public Parameters Parameters
            {
                get
                {
                   return  _parameters;
                }
            }

            #region Методы
            
            /// <summary>
            /// Функция запуска хранимых процедур или текстовых запросов.
            /// </summary>
            /// <param name="CommandText">Текст команды или имя процедуры</param>
            /// <param name="CommandResultType">Тип возвращаемого результата</param>
            /// <param name="CommandType">Тип команды</param>
            /// <param name="ResultColumnName">Имя колонки с результатом</param>
            /// <param name="CommandTimeOut">Таймаут для команды (в секундах)</param>
            /// <returns>Структура с описанием результата</returns>
            public CommandResult ExecuteQuery(string CommandText, CommandResultTypes CommandResultType, CommandType CommandType, string ResultColumnName, int CommandTimeOut)
            {
                CommandResult result = new CommandResult();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = _connection.sqlConnection;
                    cmd.CommandType = CommandType;
                    cmd.CommandText = CommandText;
                    cmd.CommandTimeout = CommandTimeOut;

                    if (_parameters.GetParameters != null)
                    {
                        foreach (SqlParameter sp in _parameters.GetParameters)
                        {
                            cmd.Parameters.Add(sp);
                        }
                    }

                    switch (CommandResultType)
                    {
                        case CommandResultTypes.None:
                            {
                                if (cmd.Connection.State == ConnectionState.Closed)
                                    cmd.Connection.Open();
                                cmd.ExecuteNonQuery();
                                cmd.Connection.Close();
                            }
                            break;
                        case CommandResultTypes.Table:
                            {
                                DataTable dtResult = new DataTable();
                                _adapter = new SqlDataAdapter(cmd);
                                _adapter.Fill(dtResult);
                                result.ResultObject = dtResult;
                            }
                            break;
                        case CommandResultTypes.DataSet:
                            {
                                DataSet dsResult = new DataSet();
                                _adapter = new SqlDataAdapter(cmd);
                                _adapter.Fill(dsResult);
                                result.ResultObject = dsResult;
                            }
                            break;
                        case CommandResultTypes.SingleValue:
                            {
                                DataTable dtResult = new DataTable();
                                _adapter = new SqlDataAdapter(cmd);
                                _adapter.Fill(dtResult);
                                result.ResultObject = dtResult.Rows[0][ResultColumnName].ToString();
                            }
                            break;

                        case CommandResultTypes.Adapter:
                            {
                                _adapter = new SqlDataAdapter(cmd);
                                result.ResultObject = _adapter;
                            }
                            break;
                    }
                    result.isGood = true;
                }
                catch (Exception ex)
                {
                    result.isGood = false;
                    throw new Exception("Ошибка при выполнении команды БД", ex);
                }

                return result;
            }

            /// <summary>
            /// Функция запуска хранимых процедур или текстовых запросов.
            ///     
            ///     CommandResultType.None;
            ///     CommandType.Text;
            ///     ResultColumnName = ""
            ///     CommandTimeout = 30;
            /// </summary>
            /// <param name="CommandText">Текст команды или имя процедуры</param>
            /// <returns></returns>
            public CommandResult ExecuteQuery(string CommandText)
            {
                return ExecuteQuery(CommandText, CommandResultTypes.None, CommandType.Text, "", 30);
            }

            /// <summary>
            /// Функция запуска хранимых процедур или текстовых запросов.
            ///     
            ///     CommandType.Text;
            ///     ResultColumnName = ""
            ///     CommandTimeout = 30;
            /// </summary>
            /// <param name="CommandText">Текст команды или имя процедуры</param>
            /// <param name="CommandResultType">Тип возвращаемого результата</param>
            /// <returns></returns>
            public CommandResult ExecuteQuery(string CommandText, CommandResultTypes CommandResultType)
            {
                return ExecuteQuery(CommandText, CommandResultType, CommandType.Text, "", 30);

            }

            /// <summary>
            /// Функция запуска хранимых процедур или текстовых запросов.
            ///     
            ///     ResultColumnName = ""
            ///     CommandTimeout = 30;
            /// </summary>
            /// <param name="CommandText">Текст команды или имя процедуры</param>
            /// <param name="CommandResultType">Тип возвращаемого результата</param>
            /// <param name="CommandType">Тип команды</param>
            /// <returns></returns>
            public CommandResult ExecuteQuery(string CommandText, CommandResultTypes CommandResultType, CommandType CommandType)
            {
                return ExecuteQuery(CommandText, CommandResultType, CommandType, "", 30);
            }

            /// <summary>
            /// Функция запуска хранимых процедур или текстовых запросов.
            ///     
            ///     CommandResultType.SingleValue;
            ///     CommandTimeout = 30;
            /// </summary>
            /// <param name="CommandText">Текст команды или имя процедуры</param>
            /// <param name="CommandType">Тип команды</param>
            /// <param name="ResultColumnName">Имя колонки с результатом</param>
            /// <returns></returns>
            public CommandResult ExecuteQuery(string CommandText, CommandType CommandType, string ResultColumnName)
            {
                return ExecuteQuery(CommandText, CommandResultTypes.SingleValue, CommandType, ResultColumnName, 30); 
            }

            /// <summary>
            /// Функция запуска хранимых процедур или текстовых запросов.
            ///     
            ///     CommandResultType.None;
            ///     CommandType.Text;
            ///     ResultColumnName = ""
            /// </summary>
            /// <param name="CommandText">Текст команды или имя процедуры</param>
            /// <param name="CommandTimeOut">Таймаут для команды (в секундах)</param>
            /// <returns></returns>
            public CommandResult ExecuteQuery(string CommandText, int CommandTimeOut)
            {
                return ExecuteQuery(CommandText, CommandResultTypes.None, CommandType.Text, "", CommandTimeOut);
            }

            /// <summary>
            /// Функция запуска хранимых процедур или текстовых запросов.
            ///     
            ///     CommandType.Text;
            ///     ResultColumnName = ""
            /// </summary>
            /// <param name="CommandText">Текст команды или имя процедуры</param>
            /// <param name="CommandResultType">Тип возвращаемого результата</param>
            /// <param name="CommandTimeOut">Таймаут для команды (в секундах)</param>
            /// <returns></returns>
            public CommandResult ExecuteQuery(string CommandText, CommandResultTypes CommandResultType, int CommandTimeOut)
            {
                return ExecuteQuery(CommandText, CommandResultType, CommandType.Text, "", CommandTimeOut);
            }

            /// <summary>
            /// Функция запуска хранимых процедур или текстовых запросов.
            ///     
            ///     ResultColumnName = ""
            /// </summary>
            /// <param name="CommandText">Текст команды или имя процедуры</param>
            /// <param name="CommandResultType">Тип возвращаемого результата</param>
            /// <param name="CommandType">Тип команды</param>
            /// <param name="CommandTimeOut">Таймаут для команды (в секундах)</param>
            /// <returns></returns>
            public CommandResult ExecuteQuery(string CommandText, CommandResultTypes CommandResultType, CommandType CommandType, int CommandTimeOut)
            {
                return ExecuteQuery(CommandText, CommandResultType, CommandType, "", CommandTimeOut);
            }

            /// <summary>
            /// Функция запуска хранимых процедур или текстовых запросов.
            ///     
            ///     CommandResultType.SingleValue
            /// </summary>
            /// <param name="CommandText">Текст команды или имя процедуры</param>
            /// <param name="ResultColumnName">Имя колонки с результатом</param>
            /// <param name="CommandType">Тип команды</param>
            /// <param name="CommandTimeOut">Таймаут для команды (в секундах)</param>
            /// <returns></returns>
            public CommandResult ExecuteQuery(string CommandText, CommandType CommandType, string ResultColumnName, int CommandTimeOut)
            {
                return ExecuteQuery(CommandText, CommandResultTypes.SingleValue, CommandType, ResultColumnName, CommandTimeOut);
            }


            #endregion

        }

        /// <summary>
        /// Возможные выходы Комманд
        /// </summary>
        public enum CommandResultTypes
        {
            /// <summary>
            /// Нет результата
            /// </summary>
            None,
            /// <summary>
            /// На выходе объект DataTable
            /// </summary>
            Table,
            /// <summary>
            /// На выходе объект класса DataSet
            /// </summary>
            DataSet,
            /// <summary>
            /// На выходе одно значение
            /// </summary>
            SingleValue,

            /// <summary>
            /// На выходе объект Adapter
            /// </summary>
            Adapter
        };

        /// <summary>
        /// Структура, которая описывает выходной результат выполнения комманды
        /// </summary>
        public struct CommandResult
        {
            /// <summary>
            /// Тип результата
            /// </summary>
            public CommandResultTypes ResultType;
            /// <summary>
            /// Выходной объект. Зависит от Типа результата
            /// </summary>
            public object ResultObject;
            /// <summary>
            /// Флаг успешного выполнения команды
            /// </summary>
            public bool isGood;
        }

    }

}
