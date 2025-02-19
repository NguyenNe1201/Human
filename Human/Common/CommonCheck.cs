using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Ast;

namespace Human.Common
{
    public class CommonCheck
    {
        //----------------------------- Check EDIT function of user --------------------------------------
        public static bool Check_function_edit(List<Human.Models.Entities.Emp_Function_Entities> lst_fuction_emp, string functionName)
        {
            string btn_fun = "";
            string name_fun = "";
            foreach (var item_p in lst_fuction_emp)
            {
                if (item_p.BTN_FUNCTION == "edit/read" && item_p.NAME_FUNCTION == functionName)
                {
                    btn_fun = item_p.BTN_FUNCTION;
                    name_fun = item_p.NAME_FUNCTION;
                    break;
                }
            }
            return (lst_fuction_emp.Count > 0 && btn_fun == "edit/read" && name_fun == functionName);
        }
        //---------------------------------- Check READ function of user ----------------------------------
        public static bool Check_function_read(List<Human.Models.Entities.Emp_Function_Entities> lst_function_emp, string functionName)
        {
            string btn_fun = "";
            string name_fun = "";
            foreach (var item_p in lst_function_emp)
            {
                if ((item_p.BTN_FUNCTION == "edit/read" || item_p.BTN_FUNCTION == "read") && item_p.NAME_FUNCTION == functionName)
                {
                    btn_fun = item_p.BTN_FUNCTION;
                    name_fun = item_p.NAME_FUNCTION;
                    break;
                }
            }
            return (lst_function_emp.Count > 0 && (btn_fun == "edit/read" || btn_fun == "read") && name_fun == functionName);
        }
        //---------------------------------- Check quyền quản lý phòng ban của user -----------------------------
        public static int GetSectionIdFromList(List<Human.Models.Entities.PowerView_Entities> lstPowerByEmp, int? section_id)
        {
            int result = 0;
            foreach (var item in lstPowerByEmp)
            {
                if (item.SECTION_ID == section_id)
                {
                    result = item.SECTION_ID;
                    break;
                }
            }
            return result;
        }
        // ------------------------------------ Notifications ở view mỗi trang -------------------------------
        public static void SetAlert(Controller controller, string message, string alertType)
        {
            controller.TempData["AlertMessage"] = message;
            controller.TempData["AlertType"] = alertType;
        }
        // ------ chuyển đổi mã sha256 ------------------------
        public static string ComputeSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}