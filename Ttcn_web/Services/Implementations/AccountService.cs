using System;
using System.Linq;
using System.Web.Mvc;
using Ttcn_web.Models;
using Ttcn_web.Services.Abtractions;

namespace Ttcn_web.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private TtcnWebEntities db = new TtcnWebEntities();

        public string CreateUser(FormCollection signupForm, string userType)
        {
            string check = "";

            var userFromDB = db.ADUsers.ToList().Where(p => p.ADUserName == signupForm["username"]).FirstOrDefault();
            if (userFromDB != null)
            {
                check = "fail";
                return check;
            }
            var user = new ADUser();
            var lastUserFromDb = db.ADUsers.ToList().LastOrDefault();
            user.ADUserID = lastUserFromDb == null ? 1 : lastUserFromDb.ADUserID + 1;
            user.AAStatus = "Alive";
            user.AACreatedDate = DateTime.Now;
            user.ADUserName = signupForm["username"];
            user.ADPassword = Common.GetMD5(signupForm["password"]);

            if ("customer".Equals(userType))
            {
                var customerFromDb = db.ARCustomers.ToList().Where(x => x.ARCustomerContactPhone == signupForm["phone"] || x.ARCustomerContactEmail == signupForm["email"]).FirstOrDefault();
                if (customerFromDb != null)
                {
                    check = "fail";
                    return check;
                }
                user.FK_ADUserGroupID = 1; // Todo: 

                var customer = new ARCustomer();
                var lastCustomerFromDb = db.ARCustomers.ToList().LastOrDefault();
                customer.ARCustomerID = lastCustomerFromDb == null ? 1 : lastCustomerFromDb.ARCustomerID + 1;
                customer.FK_ADUserID = user.ADUserID;
                customer.AAStatus = "Alive";
                customer.AACreatedDate = DateTime.Now;
                customer.ARCustomerName = signupForm["name"];
                customer.ARCustomerNo = "KH" + customer.ARCustomerID.ToString();
                customer.ARCustomerBirthDay = signupForm["birthDay"] != null ? DateTime.Parse(signupForm["birthDay"]) : DateTime.Now;
                customer.ARCustomerSex = signupForm["gender"] == "male" ? true : false;
                customer.ARCustomerAddress = signupForm["address"];
                customer.ARCustomerContactPhone = signupForm["phone"];
                customer.ARCustomerContactEmail = signupForm["email"];

                db.ADUsers.Add(user);
                db.ARCustomers.Add(customer);
                db.SaveChanges();

                check = "success";
                return check;
            }
            else
            {
                var supplierFormDB = db.APSuppliers.ToList().Where(p => p.APSupplierContactPhone == signupForm["phone"].ToString() || p.APSupplierContactEmail == signupForm["email"].ToString()).FirstOrDefault();
                if (supplierFormDB != null)
                {
                    check = "fail";
                    return check;
                }
                var objUserGroup = db.ADUserGroups.FirstOrDefault(p => p.ADUserGroupName == "Supplier" && p.AAStatus == "Alive");
                user.FK_ADUserGroupID = objUserGroup != null ? objUserGroup.ADUserGroupID : 0;

                var supplier = new APSupplier();
                var lastSupplierFromDB = db.APSuppliers.ToList().LastOrDefault();
                supplier.APSupplierID = lastSupplierFromDB == null ? 1 : lastSupplierFromDB.APSupplierID + 1;
                supplier.FK_ADUserID = user.ADUserID;
                supplier.AAStatus = "Alive";
                supplier.AACreatedDate = DateTime.Now;
                supplier.APSupplierName = signupForm["name"];
                supplier.APSupplierNo = "NCC" + supplier.APSupplierID.ToString();
                supplier.APSupplierAddress = signupForm["address"];
                supplier.APSupplierContactPhone = signupForm["phone"];
                supplier.APSupplierContactEmail = signupForm["email"];

                db.ADUsers.Add(user);
                db.APSuppliers.Add(supplier);
                db.SaveChanges();
                check = "success";
                return check;
            }
        }

        public string GetToken(string userName, string password)
        {
            password = Common.GetMD5(password); 

            ADUser userFromDb = db.ADUsers.FirstOrDefault(x => x.ADUserName == userName && x.ADPassword == password);

            if (userFromDb == null)
            {
                return "";
            }

            string token = Common.GetToken();

            userFromDb.ADUserResetToken = token;

            db.SaveChanges();

            return token;
        }

        public string GetUserGroupNameByID(int userGroupID)
        {
            var objUserGroup = db.ADUserGroups.FirstOrDefault(p => p.ADUserGroupID == userGroupID && p.AAStatus == "Alive");
            if (objUserGroup == null)
                return string.Empty;
            return objUserGroup.ADUserGroupName;
        }
    }
}