using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ALG
{
    namespace DataAccessLayer
    {
        /// <summary>
        /// Класс параметров для хранимых процедур
        /// </summary>
        public class Parameters
        {
            List<SqlParameter> _lstParameters;
            
            /// <summary>
            /// Конструктор. Инициализирует лист SqlParameter
            /// </summary>
            public Parameters()
            {
                _lstParameters = new List<SqlParameter>();
            }
            
            /// <summary>
            /// Возвращает лист SqlParameter
            /// </summary>
            public List<SqlParameter> GetParameters
            {
                get { return _lstParameters; }
            }

            /// <summary>
            /// Добавляет новый параметр в лист SqlParameter
            /// </summary>
            /// <param name="Name">Имя параметра</param>
            /// <param name="Value">Значение параметра</param>
            /// <param name="Type">Тип параметра</param>
            public void AddParameter(string Name, string Value, SqlDbType Type)
            {
                _lstParameters.Add(CreateParameter(Name, Value, Type, 1, ParameterDirection.Input, true));
            }

            /// <summary>
            /// Добавляет новый параметр в лист SqlParameter
            /// </summary>
            /// <param name="Name">Имя параметра</param>
            /// <param name="Value">Значение параметра</param>
            /// <param name="Type">Тип параметра</param>
            /// <param name="Direction">"Направление" параметра (input, output, ...)</param>
            public void AddParameter(string Name, string Value, SqlDbType Type, ParameterDirection Direction) {
                _lstParameters.Add(CreateParameter(Name, Value, Type, 1, Direction, true));
            }

            public void AddParameter(string Name, string Value, SqlDbType Type, int Size, ParameterDirection Direction) {
                _lstParameters.Add(CreateParameter(Name, Value, Type, Size, Direction, true));
            }

            /// <summary>
            /// Добавляет новый параметр в лист SqlParameter
            /// </summary>
            /// <param name="Name">Имя параметра</param>
            /// <param name="Value">Значение параметра</param>
            /// <param name="Type">Тип параметра</param>
            /// <param name="Direction">"Направление" параметра (input, output, ...)</param>
            /// <param name="IsNullable">Флаг возможности значения "null"</param>
            public void AddParameter(string Name, string Value, SqlDbType Type, ParameterDirection Direction, bool IsNullable)
            {
                _lstParameters.Add(CreateParameter(Name, Value, Type, 1, Direction, IsNullable));
            }

            /// <summary>
            /// Добавляет новый параметр в лист SqlParameter
            /// </summary>
            /// <param name="sqlParameter">Готовый параметр</param>
            public void AddParameter(SqlParameter sqlParameter)
            {
                _lstParameters.Add(sqlParameter);
            }

            /// <summary>
            /// Создает новый SqlParameter
            /// </summary>
            /// <param name="Name"></param>
            /// <param name="Value"></param>
            /// <param name="Type"></param>
            /// <returns></returns>
            private SqlParameter CreateParameter(string Name, string Value, SqlDbType Type, int Size , ParameterDirection Direction, bool IsNullable)
            {
                SqlParameter sp = new SqlParameter();
                sp.Direction = Direction;
                sp.IsNullable = IsNullable;
                sp.SqlDbType = Type;
                sp.Size = Size;
                sp.ParameterName = Name;
                sp.SqlValue = Value;
                return sp;
            }

            /// <summary>
            /// Очистка листа параметров
            /// </summary>
            public void ClearParameters()
            {
                _lstParameters.Clear();
            }

            /// <summary>
            /// Возвращает параметр по имени
            /// </summary>
            /// <param name="Name">Имя параметра</param>
            /// <returns></returns>
            public SqlParameter GetParameterByName(string Name) {
                SqlParameter result = null;
                foreach(SqlParameter p in _lstParameters){
                    if (p.ParameterName == Name){
                        result = p;
                        break;
                    }
                }
                return result;
            }
        }
    }
}
