﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:2.0.50727.3053
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ALG.DataAccessLayer.Properties {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ALG.DataAccessLayer.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на MessageCaption.
        /// </summary>
        public static string Mes_CaptionColumnName {
            get {
                return ResourceManager.GetString("Mes_CaptionColumnName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на MessageCode.
        /// </summary>
        public static string Mes_CodeColumnName {
            get {
                return ResourceManager.GetString("Mes_CodeColumnName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на MessageComment.
        /// </summary>
        public static string Mes_CommentColumnName {
            get {
                return ResourceManager.GetString("Mes_CommentColumnName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на MessageCode =.
        /// </summary>
        public static string Mes_FilterExpression_part1 {
            get {
                return ResourceManager.GetString("Mes_FilterExpression_part1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Messages.
        /// </summary>
        public static string Mes_MetaTableName {
            get {
                return ResourceManager.GetString("Mes_MetaTableName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на MessageText.
        /// </summary>
        public static string Mes_TextColumnName {
            get {
                return ResourceManager.GetString("Mes_TextColumnName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на MessageType.
        /// </summary>
        public static string Mes_TypeColumnName {
            get {
                return ResourceManager.GetString("Mes_TypeColumnName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Meta.sp_MessagesSelectAll.
        /// </summary>
        public static string Meta_Messages {
            get {
                return ResourceManager.GetString("Meta_Messages", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Meta.sp_StoredProceduresSelect.
        /// </summary>
        public static string Meta_SPNames {
            get {
                return ResourceManager.GetString("Meta_SPNames", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на ExecuteCode =.
        /// </summary>
        public static string SP_FilterExpression_part1 {
            get {
                return ResourceManager.GetString("SP_FilterExpression_part1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на  and isActive = 1.
        /// </summary>
        public static string SP_FilterExpression_part2 {
            get {
                return ResourceManager.GetString("SP_FilterExpression_part2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на StoredProcedures.
        /// </summary>
        public static string SP_MetaTableName {
            get {
                return ResourceManager.GetString("SP_MetaTableName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Name.
        /// </summary>
        public static string SP_NameColumnName {
            get {
                return ResourceManager.GetString("SP_NameColumnName", resourceCulture);
            }
        }
    }
}
