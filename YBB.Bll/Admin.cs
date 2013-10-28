using System.Data;
using YBB.Common;
namespace YBB.Bll
{
    public class Admin
    {
        public static int ActionInsertUpdateDelete(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6, string string_7)
        {
            return Ant.DAL.Admin.ActionInsertUpdateDelete(string_0, string_1, string_2, string_3, string_4, string_5, string_6, string_7);
        }

        public static int ActionMoneyInsertUpdateDelete(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6, string string_7)
        {
            return Ant.DAL.Admin.ActionMoneyInsertUpdateDelete(string_0, string_1, string_2, string_3, string_4, string_5, string_6, string_7);
        }

        public static int DeleteRose(string string_0)
        {
            return Ant.DAL.Admin.DeleteRose(string_0);
        }

        public static int DomainInsertUpdateDelete(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5)
        {
            return Ant.DAL.Admin.DomainInsertUpdateDelete(string_0, string_1, string_2, string_3, string_4, string_5);
        }

        public static DataTable GetAdminRoles()
        {
            AntCache cacheService = AntCache.GetCacheService();
            object obj2 = cacheService.RetrieveObject("/Ant/AdminRoles");
            if (obj2 == null)
            {
                obj2 = General.GetDataTable(0, "RoleId,AdminMenuId", "Ant_Roles", "", "");
                cacheService.AddObject("/Ant/AdminRoles", obj2);
            }
            return (DataTable)obj2;
        }

        public static DataTable GetList(int int_0, string string_0, string string_1, string string_2)
        {
            return Ant.DAL.Admin.GetList(int_0, string_0, string_1, string_2);
        }

        public static int InsertAdmin(string string_0, string string_1, string string_2, string string_3)
        {
            return Ant.DAL.Admin.InsertAdmin(string_0, string_1, string_2, string_3);
        }

        public static int InsertDingyue(string string_0, string string_1, string string_2, string string_3)
        {
            return Ant.DAL.Admin.InsertDingyue(string_0, string_1, string_2, string_3);
        }

        public static void InsertError(string string_0, string string_1, string string_2)
        {
            string_1 = string_1.Replace("'", "&#39");
            string_2 = string_2.Replace("'", "&#39");
            Ant.DAL.Admin.ErrorInsertUpdateDelete(string_0, string_1, string_2);
        }

        public static int InsertRose(string string_0, string string_1, string string_2)
        {
            return Ant.DAL.Admin.InsertRose(string_0, string_1, string_2);
        }

        public static int MessageInsert(int int_0, int int_1, string string_0, string string_1, int int_2, string string_2)
        {
            return Ant.DAL.Admin.MessageInsert(int_0, int_1, string_0, string_1, int_2, string_2);
        }

        public static int OrderInsertUpdateDelete(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6, string string_7, string string_8)
        {
            return Ant.DAL.Admin.OrderInsertUpdateDelete(string_0, string_1, string_2, string_3, string_4, string_5, string_6, string_7, string_8);
        }

        public static int PayCardInsertUpdateDelete(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6, string string_7, string string_8, string string_9)
        {
            return Ant.DAL.Admin.PayCardInsertUpdateDelete(string_0, string_1, string_2, string_3, string_4, string_5, string_6, string_7, string_8, string_9);
        }

        public static void RemoveAdminRoles()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/AdminRoles");
        }

        public static DataTable SelectAdminByuserid(string string_0, string string_1)
        {
            return Ant.DAL.Admin.SelectAdminByuserid(string_0, string_1);
        }

        public static DataTable SelectDingyue(string string_0, string string_1, string string_2)
        {
            return Ant.DAL.Admin.SelectDingyue(string_0, string_1, string_2);
        }

        public static DataTable SelectMenu(string string_0)
        {
            return Ant.DAL.Admin.SelectMenu(string_0);
        }

        public static DataTable ShowAdmin(string string_0)
        {
            return Admin.ShowAdmin(string_0);
        }

        public static DataTable ShowAdminMenu(int int_0)
        {
            return Ant.DAL.Admin.ShowAdminMenu(int_0);
        }

        public static int SmsCodeInsert(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5)
        {
            return Ant.DAL.Admin.SmsCodeInsert(string_0, string_1, string_2, string_3, string_4, string_5);
        }

        public static DataTable SmsCodeSelect(string string_0, string string_1)
        {
            return Ant.DAL.Admin.SmsCodeSelect(string_0, string_1);
        }

        public static DataTable SmsCodeSelectByCode(string string_0, string string_1, string string_2)
        {
            return Ant.DAL.Admin.SmsCodeSelectByCode(string_0, string_1, string_2);
        }

        public static DataTable SmsCodeSelectByIP(string string_0, string string_1)
        {
            return Ant.DAL.Admin.SmsCodeSelectByIP(string_0, string_1);
        }

        public static int SmsCodeUpdate(string string_0, string string_1)
        {
            return Ant.DAL.Admin.SmsCodeUpdate(string_0, string_1);
        }

        public static int SmsCodeUpdate(string string_0, int int_0, string string_1)
        {
            return Ant.DAL.Admin.SmsCodeUpdate(string_0, int_0, string_1);
        }

        public static int UpdateAdmin(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5)
        {
            return Ant.DAL.Admin.UpdateAdmin(string_0, string_1, string_2, string_3, string_4, string_5);
        }

        public static int UpdateBaseJifen(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6, string string_7, string string_8, string string_9, string string_10, string string_11, string string_12, string string_13, string string_14, string string_15, int int_0, int int_1, int int_2, int int_3, int int_4, string string_16, string string_17, string string_18, string string_19, string string_20, string string_21, int int_5, int int_6, int int_7, int int_8, int int_9, int int_10, int int_11, int int_12, int int_13, int int_14, int int_15, int int_16, int int_17, int int_18, int int_19, int int_20, int int_21, int int_22, int int_23)
        {
            return Ant.DAL.Admin.UpdateBaseJifen(string_0, string_1, string_2, string_3, string_4, string_5, string_6, string_7, string_8, string_9, string_10, string_11, string_12, string_13, string_14, string_15, int_0, int_1, int_2, int_3, int_4, string_16, string_17, string_18, string_19, string_20, string_21, int_5, int_6, int_7, int_8, int_9, int_10, int_11, int_12, int_13, int_14, int_15, int_16, int_17, int_18, int_19, int_20, int_21, int_22, int_23);
        }

        public static int UpdateDingyue(string string_0, string string_1)
        {
            return Ant.DAL.Admin.UpdateDingyue(string_0, string_1);
        }

        public static int UpdateRose(string string_0, string string_1, string string_2, string string_3)
        {
            return Ant.DAL.Admin.UpdateRose(string_0, string_1, string_2, string_3);
        }

        public static int UpdateTemplate(string string_0, int int_0, string string_1, string string_2)
        {
            return Ant.DAL.Admin.UpdateTemplate(string_0, int_0, string_1, string_2);
        }
    }

}
