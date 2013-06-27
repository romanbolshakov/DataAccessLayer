using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ALG.DataAccessLayer
{
    /// <summary>
    /// Класс для работы с мета данными
    /// </summary>
    public class ClassMetaData
    {
        private DataSet _dsMetaData;
        private ALG.DataAccessLayer.ClassDataAccessLayer _dal;
        private bool isDataLoaded = false;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="DAL">Data Access Layer</param>
        public ClassMetaData(ClassDataAccessLayer DAL)
        {
            _dsMetaData = new DataSet("MetaData");
            _dal = DAL;
            isDataLoaded = LoadMetaData();
        }

        private bool LoadMetaData()
        {
            _dsMetaData.Tables.Add(GetMeta_SPNames());
            _dsMetaData.Tables.Add(GetMeta_Messages());
            return true;
        }

        private DataTable GetMeta_SPNames()
        {
            string metaSP = Properties.Resources.Meta_SPNames;
            string tableName = Properties.Resources.SP_MetaTableName;
            DataTable dtMetaSP = new DataTable(tableName);
            Query aQuery = _dal.CreateNewQuery();
            CommandResult cr = aQuery.ExecuteQuery(metaSP, CommandResultTypes.Table, CommandType.StoredProcedure);
            if (cr.isGood)
            {
                dtMetaSP = (DataTable)cr.ResultObject;
                dtMetaSP.TableName = tableName;
            }
            return dtMetaSP;
        }

        private DataTable GetMeta_Messages()
        {
            string metaSP = Properties.Resources.Meta_Messages;
            string tableName = Properties.Resources.Mes_MetaTableName;
            DataTable dtMetaMes = new DataTable(tableName);
            Query aQuery = _dal.CreateNewQuery();
            CommandResult cr = aQuery.ExecuteQuery(metaSP, CommandResultTypes.Table, CommandType.StoredProcedure);
            if (cr.isGood)
            {
                dtMetaMes = (DataTable)cr.ResultObject;
                dtMetaMes.TableName = tableName;
            }
            return dtMetaMes;
        }

        /// <summary>
        /// Возвращает имя хранимой процедуры по коду запуска
        /// </summary>
        /// <param name="ExecuteCode">Код запуска хранимой процедуры</param>
        /// <returns>Имя хранимой процедуры</returns>
        public string GetSPNameByCode(int ExecuteCode)
        {
            return GetSPNameByCode(ExecuteCode.ToString());
        }

        /// <summary>
        /// Возвращает имя хранимой процедуры по коду запуска
        /// </summary>
        /// <param name="ExecuteCode">Код запуска хранимой процедуры</param>
        /// <returns>Имя хранимой процедуры</returns>
        public string GetSPNameByCode(string ExecuteCode)
        {
            string result = "";
            if (isDataLoaded)
            {
                string tableName = Properties.Resources.SP_MetaTableName;
                string filter_p1 = Properties.Resources.SP_FilterExpression_part1;
                string filter_p2 = Properties.Resources.SP_FilterExpression_part2;
                string columnName = Properties.Resources.SP_NameColumnName;
                DataRow[] drs = _dsMetaData.Tables[tableName].Select(filter_p1 + "\'" + ExecuteCode + "\'" + filter_p2);
                if (drs.Length >= 1)
                {
                    result = drs[0][columnName].ToString();
                }
            }
            return result;
        }

        /// <summary>
        /// Возвращает компоненты сообщения по коду
        /// </summary>
        /// <param name="MessageCode">Код сообщения</param>
        /// <returns></returns>
        public MetaMessage GetMessageByCode(int MessageCode)
        {
            return GetMessageByCode(MessageCode.ToString());
        }

        /// <summary>
        /// Возвращает компоненты сообщения по коду
        /// </summary>
        /// <param name="MessageCode">Код сообщения</param>
        /// <returns></returns>
        public MetaMessage GetMessageByCode(string MessageCode)
        {
            MetaMessage mm = new MetaMessage();
            if (isDataLoaded)
            {
                string tableName = Properties.Resources.Mes_MetaTableName;
                string filter_p1 = Properties.Resources.Mes_FilterExpression_part1;
                string cnMessageType = Properties.Resources.Mes_TypeColumnName;
                string cnMessageCaption = Properties.Resources.Mes_CaptionColumnName;
                string cnMessageText = Properties.Resources.Mes_TextColumnName;
                string cnMessageComment = Properties.Resources.Mes_CommentColumnName;
                string cnMessageCode = Properties.Resources.Mes_CodeColumnName;
                DataRow[] drs = _dsMetaData.Tables[tableName].Select(filter_p1 + "\'" + MessageCode + "\'");
                if (drs.Length >= 1)
                {
                    mm.MessageType = drs[0][cnMessageType].ToString();
                    mm.MessageCaption = drs[0][cnMessageCaption].ToString();
                    mm.MessageText = drs[0][cnMessageText].ToString();
                    mm.MessageComment = drs[0][cnMessageComment].ToString();
                    mm.MessageCode = drs[0][cnMessageCode].ToString();
                }
            }
            return mm;
        }
    }

    public struct MetaMessage
    {
        public string MessageType;
        public string MessageCaption;
        public string MessageText;
        public string MessageComment;
        public string MessageCode;
    }
}
