using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DbQueries;
using Domain.Entities;
using Domain.Entities.Login;
using Domain.Entities.Order;
using Domain.Entities.Person;
using Domain.Entities.supplier;
using Twilio;
using Twilio.Rest.Verify.V2.Service;
using Domain.Enums;
using Domain.Exceptions;
using HelperClass;
using Login;
using Newtonsoft.Json;
using Domain.Common;

namespace UserRepository
{
    public class UserRepository : Domain.Interfaces.User.IUserRepository
    {
        public static string testc;
        Common conn_db = new Common();
        string language = "en";


        public UserRepository(string language)
        {
            this.language = language;
        }




        public async Task<bool> SendCode(string phoneNumber, string countryCode)
        {
            try
            {
               await StartPhoneVerificationAsync(phoneNumber, countryCode);
            
              return true;
                

            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public string checkPhoneValidity(string phoneNumber)
        {
            try
            {
                if (phoneNumber == "")
                {
                    return phoneNumber;
                }

                if (phoneNumber.Substring(0, 1) == "0")
                    return phoneNumber.Substring(1, phoneNumber.Length - 1);
                else
                    return phoneNumber;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public async Task<bool> VerifyPhone(string phoneNumber, string countryCode, string num)
        {
            try
            {
                await VerifyPhoneAsync(phoneNumber, countryCode, num); 
                return true; 

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async Task<string> StartPhoneVerificationAsync(string phoneNumber, string countryCode)
        {

            if (Env.Production)
            {

                string accountSid = TwilioConfig.AccountSid;
                string authToken = TwilioConfig.AuthToken;
                string pathServiceID = TwilioConfig.ServiceSid;

                TwilioClient.Init(accountSid, authToken);

                var verification = VerificationResource.Create(
                    to: "+" + countryCode + phoneNumber,
                    channel: "sms",
                    pathServiceSid: pathServiceID
                );

                return "success";
            }
            else 
                return "success"; 
        }




        public static async Task<string> VerifyPhoneAsync(string phoneNumber, string countryCode, string num)
        {

            if (Env.Production)
            {


                string accountSid = TwilioConfig.AccountSid;
                string authToken = TwilioConfig.AuthToken;
                string pathServiceID = TwilioConfig.ServiceSid;

                TwilioClient.Init(accountSid, authToken);

                var verificationCheck = VerificationCheckResource.Create(
                   to: "+" + countryCode + phoneNumber,
                code: num,
                pathServiceSid: pathServiceID
                );

                if (verificationCheck.Valid == true)
                    return verificationCheck.Status;
                else
                    throw new Exception(verificationCheck.Status);

            }else
            {
                return "success";
            }
        }


        public bool ComparePasswrod(string input_password, string db_password)
        {
            if (input_password == db_password)
                return true;
            else
                return false;

        }



        public int GetUserUserTypeByAccessToken(string accessToken)
        {

            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetUserTypeByAccessToken(accessToken));
            int UserType = 1;

            if (dataTable.Rows.Count > 0)
            {
                UserType = Convert.ToInt16(dataTable.Rows[0][0].ToString());
            }
            else
                return 1;


            return UserType;
        }

        public List<string> GetUserDevices(int user_id)
        {

            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetObjectsByColName("user_device", "DEVICE_ID", "user_id", user_id.ToString()));

            List<string> devices = new List<string>();

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);


            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                string device = row["device_id"].ToString();
                devices.Add(device);
            }


            return devices;
        }

        public List<OrderUser> GetPersonIdOfferId(int offer_id)
        {

            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetPersonIdOfferId(offer_id));

            List<OrderUser> users = new List<OrderUser>();

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);


            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                OrderUser user = new OrderUser();
                user.userId = row["user_id"] is DBNull ? 0 : Convert.ToInt32(row["user_id"]);
                user.OrderIdentity = row["ORDER_IDENTITY"].ToString();
                users.Add(user);
            }


            return users;
        }

        public List<OrderUser> GetPersonIdOrderId(int offer_id)
        {

            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetPersonIdOrderId(offer_id));

            List<OrderUser> users = new List<OrderUser>();

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);


            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                OrderUser user = new OrderUser();
                user.userId = row["user_id"] is DBNull ? 0 : Convert.ToInt32(row["user_id"]);
                user.OrderIdentity = row["ORDER_IDENTITY"].ToString();
                users.Add(user);
            }


            return users;
        }



        public List<int> GetSupplierUsersIds(int cityId)
        {

            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetSuppliersByCity(cityId));

            List<int> users = new List<int>();

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);


            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                int user = row["user_id"] is DBNull ? 0 : Convert.ToInt32(row["user_id"]);
                users.Add(user);
            }

            return users;
        }

        public List<int> GetAllAdminIds()
        {

            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetObjectsByColName("users", "id", "isAdmin", "1"));

            List<int> users = new List<int>();

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);


            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                int user = row["id"] is DBNull ? 0 : Convert.ToInt32(row["id"]);
                users.Add(user);
            }

            return users;
        }

        public List<int> GetUsersIds()
        {

            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetObjectsByColName("users", "id", "user_type_id", "1"));

            List<int> users = new List<int>();

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);


            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                int user = row["id"] is DBNull ? 0 : Convert.ToInt32(row["id"]);
                users.Add(user);
            }

            return users;
        }



        public int GetUserUserTypeByUserId(string user_id)
        {

            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetUserTypeByUserId(user_id));
            int UserType = 1;

            if (dataTable.Rows.Count > 0)
            {
                UserType = Convert.ToInt16(dataTable.Rows[0][0].ToString());
            }
            else
                return 1;


            return UserType;
        }

        public int GetUserUserTypeByPhone(string phoneNumber)
        {

            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetUserTypeByPhone(phoneNumber));
            int UserType = 1;

            if (dataTable.Rows.Count > 0)
            {
                UserType = Convert.ToInt16(dataTable.Rows[0][0].ToString());
            }
            else
                return 1;


            return UserType;
        }


        public UserDTO GetProfileByPhone(string phoneNumber, string lang, int userType)
        {

            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable;

            if (userType == 1)
                dataTable = conn_db.ReadTable(userQuery.GetProfileByPhoneForPerson(phoneNumber));
            else
                dataTable = conn_db.ReadTable(userQuery.GetProfileByPhoneForWorkshop(phoneNumber, lang));

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            UserDTO profile = new UserDTO();


            profile.user_id = dataTable.Rows[0]["USER_ID"] is DBNull ? 0 : Convert.ToInt32(dataTable.Rows[0]["USER_ID"]);
            profile.accessToken = dataTable.Rows[0]["ACCESSTOKEN"].ToString();
            profile.countryCode = dataTable.Rows[0]["COUNTRYCODE"] is DBNull ? 0 : Convert.ToInt32(dataTable.Rows[0]["COUNTRYCODE"]);
            profile.phoneNumber = dataTable.Rows[0]["PHONENUMBER"].ToString();
            profile.userType = dataTable.Rows[0]["USERTYPE"] is DBNull ? 0 : Convert.ToInt32(dataTable.Rows[0]["USERTYPE"]);
            profile.userImage = dataTable.Rows[0]["USERIMAGE"].ToString();

            profile.rating = dataTable.Rows[0]["RATING"] is DBNull ? 0 : Convert.ToDouble(dataTable.Rows[0]["RATING"]);
            profile.firstName = dataTable.Rows[0]["FIRSTNAME"].ToString();
            profile.lastName = dataTable.Rows[0]["LASTNAME"].ToString();


            profile.shopNumber = dataTable.Rows[0]["STORE_NUMBER"] is DBNull ? 0 : Convert.ToInt32(dataTable.Rows[0]["STORE_NUMBER"]);
            profile.shopName = dataTable.Rows[0]["STORE_NAME"].ToString();
            profile.cityId = dataTable.Rows[0]["CITYID"] is DBNull ? 0 : Convert.ToInt32(dataTable.Rows[0]["CITYID"]);
            profile.cityName = dataTable.Rows[0]["CITY_NAME"].ToString();
            profile.address = dataTable.Rows[0]["ADDRESS"].ToString();
            profile.Lat = dataTable.Rows[0]["LAT"] is DBNull ? 0 : Convert.ToDouble(dataTable.Rows[0]["LAT"]);
            profile.Lng = dataTable.Rows[0]["LNG"] is DBNull ? 0 : Convert.ToDouble(dataTable.Rows[0]["LNG"]);



            return profile;
        }


        public LoginDTO LoginControl(string phoneNumber, string password)
        {

            UserQuery userQuery = new UserQuery(language);
            var status = false;
            System.Data.DataTable dataTable;


            dataTable = conn_db.ReadTable(userQuery.GetObjectByColname("users", "phoneNumber", phoneNumber));


            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);





            if (PasswordHash.ValidatePassword(password, dataTable.Rows[0]["PASSWORD"].ToString()))
                status = true;




            LoginDTO user = new LoginDTO
            {

                user_id = dataTable.Rows[0]["id"].ToString(),
                accessToken = dataTable.Rows[0]["accessToken"].ToString(),
                username = dataTable.Rows[0]["username"].ToString(),
                userType = dataTable.Rows[0]["USER_TYPE_ID"] is DBNull ? 0 : Convert.ToInt32(dataTable.Rows[0]["USER_TYPE_ID"])
            };


            if (status)
                return user;
            else
                return null;


        }


        public UserDTO GetProfileByUserId(string user_id, string lang, int userType)
        {

            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = new System.Data.DataTable();



            if (userType == (int)Enums.UserType.person)
                dataTable = conn_db.ReadTable(userQuery.GetProfileByUserIdForPerson(user_id));
            else if (userType == (int)Enums.UserType.workshop)
                dataTable = conn_db.ReadTable(userQuery.GetProfileByUserIdForSupplier(user_id, lang));


            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            UserDTO profile = new UserDTO();


            profile.user_id = dataTable.Rows[0]["USER_ID"] is DBNull ? 0 : Convert.ToInt32(dataTable.Rows[0]["USER_ID"]);
            profile.accessToken = dataTable.Rows[0]["ACCESSTOKEN"].ToString();
            profile.countryCode = dataTable.Rows[0]["COUNTRYCODE"] is DBNull ? 0 : Convert.ToInt32(dataTable.Rows[0]["COUNTRYCODE"]);
            profile.phoneNumber = dataTable.Rows[0]["PHONENUMBER"].ToString();
            profile.userType = dataTable.Rows[0]["USERTYPE"] is DBNull ? 0 : Convert.ToInt32(dataTable.Rows[0]["USERTYPE"]);
            profile.userImage = dataTable.Rows[0]["USERIMAGE"].ToString();

            profile.rating = dataTable.Rows[0]["RATING"] is DBNull ? 0 : Convert.ToDouble(dataTable.Rows[0]["RATING"]);
            profile.rater_no = dataTable.Rows[0]["RATER_NO"] is DBNull ? 0 : Convert.ToInt32(dataTable.Rows[0]["RATER_NO"]);
            profile.firstName = dataTable.Rows[0]["FIRSTNAME"].ToString();
            profile.lastName = dataTable.Rows[0]["LASTNAME"].ToString();


            profile.shopNumber = dataTable.Rows[0]["STORE_NUMBER"] is DBNull ? 0 : Convert.ToInt32(dataTable.Rows[0]["STORE_NUMBER"]);

            if (userType == (int)Enums.UserType.person)
                profile.supplierType = "";
            else if (userType == (int)Enums.UserType.workshop)
                profile.supplierType = dataTable.Rows[0]["suppliertype_name"] is DBNull ? "" : dataTable.Rows[0]["suppliertype_name"].ToString();

            profile.shopName = dataTable.Rows[0]["STORE_NAME"].ToString();
            profile.cityId = dataTable.Rows[0]["CITYID"] is DBNull ? 0 : Convert.ToInt32(dataTable.Rows[0]["CITYID"]);
            profile.cityName = dataTable.Rows[0]["CITY_NAME"].ToString();
            profile.address = dataTable.Rows[0]["ADDRESS"].ToString();
            profile.Lat = dataTable.Rows[0]["LAT"] is DBNull ? 0 : Convert.ToDouble(dataTable.Rows[0]["LAT"]);
            profile.Lng = dataTable.Rows[0]["LNG"] is DBNull ? 0 : Convert.ToDouble(dataTable.Rows[0]["LNG"]);
            profile.isActive = dataTable.Rows[0]["isActive"] is DBNull ? 0 : Convert.ToInt32(dataTable.Rows[0]["isActive"]);

            return profile;
        }




        public string GetPasswordByPhone(string phoneNumber)
        {

            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetPasswordByPhoneNumber(phoneNumber));
            string password = "";

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);


            password = dataTable.Rows[0][0].ToString();



            return password;
        }

        public string GetAccessTokenByUsername(string username)
        {

            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetAccessTokenByUsername(username));
            string accessToken = "";

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            accessToken = dataTable.Rows[0][0].ToString();

            return accessToken;
        }
        public bool GetIsOlderFlag(string phonenumber)
        {

            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetObjectByColname<string>("users", "phonenumber", phonenumber));
            int flag = 0;

            if (dataTable.Rows.Count == 0)
                return false;

            flag = dataTable.Rows[0]["ISOLDUSER"] is DBNull ? 0 : Convert.ToInt32(dataTable.Rows[0]["ISOLDUSER"].ToString());

            if (flag == 0)
                return false;
            else
                return true;


        }


        public UserDTO mappingUserObject(UserDb user, PersonDb person, WorkshopDb workshop)
        {

            UserDTO userData = new UserDTO();

            userData.user_id = user.userId;
            userData.accessToken = user.accessToken;
            userData.countryCode = Convert.ToInt32(user.countryCode);
            userData.phoneNumber = user.phoneNumber;
            userData.userType = user.userType;
            userData.userImage = user.userImage;


            userData.firstName = person.firstName;
            userData.lastName = person.lastName;


            userData.shopNumber = Convert.ToInt32(workshop.shopNumber);
            userData.shopName = workshop.shopName;
            userData.cityId = workshop.cityId;
            userData.cityName = workshop.cityName;
            userData.address = workshop.address;
            userData.Lat = workshop.lat;
            userData.Lng = workshop.lng;



            return userData;
        }




        public string GetAccessTokenByPhoneNumber(string phoneNumber)
        {

            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetAccessTokenByPhoneNumber(phoneNumber));
            string accessToken = "";

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);


            accessToken = dataTable.Rows[0][0].ToString();


            return accessToken;
        }

        //public UserDb GetUserByPhoneNumber(string phoneNumber)
        //{

        //    UserQuery userQuery = new UserQuery();
        //    System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetObjectByColname<string>("users_v", "phoneNumber", phoneNumber));
        //    UserDb userdb = new UserDb();
        //    if (dataTable.Rows.Count > 0)
        //    {
        //        userdb.userId = Convert.ToInt32(dataTable.Rows[0]["USER_ID"].ToString());
        //        userdb.username = dataTable.Rows[0]["USERNAME"].ToString();
        //        userdb.fullname = dataTable.Rows[0]["FULLNAME"].ToString();
        //        userdb.email = dataTable.Rows[0]["EMAIL"].ToString();
        //        userdb.password = dataTable.Rows[0]["PASSWORD"].ToString();
        //        userdb.phoneNumber = dataTable.Rows[0]["PHONENUMBER"].ToString();
        //        userdb.countryCode = dataTable.Rows[0]["countryCode"].ToString();
        //        userdb.isActive =Convert.ToInt32( dataTable.Rows[0]["isActive"].ToString());
        //        userdb.rating = Convert.ToDouble(dataTable.Rows[0]["RATING"].ToString());
        //        userdb.userType = Convert.ToInt32(dataTable.Rows[0]["USERTYPE"].ToString());
        //        userdb.accessToken = dataTable.Rows[0]["ACCESSTOKEN"].ToString();
        //        userdb.img = dataTable.Rows[0]["IMG"].ToString();
        //        userdb.socialId = dataTable.Rows[0]["SOCIAL_ID"].ToString();
        //    }
        //    else
        //        return null;


        //    return userdb;
        //}

        //public UserDb GetUserById(string user_id)
        //{


        //    UserQuery userQuery = new UserQuery();
        //    System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetObjectByColname<string>("users_v", "user_id", user_id));
        //    UserDb userdb = new UserDb();
        //    if (dataTable.Rows.Count > 0)
        //    {

        //        userdb.userId = Convert.ToInt32(dataTable.Rows[0]["USER_ID"].ToString());
        //        userdb.username = dataTable.Rows[0]["USERNAME"].ToString();
        //        userdb.fullname = dataTable.Rows[0]["FULLNAME"].ToString();
        //        userdb.email = dataTable.Rows[0]["EMAIL"].ToString();
        //        userdb.password = dataTable.Rows[0]["PASSWORD"].ToString();
        //        userdb.phoneNumber = dataTable.Rows[0]["PHONENUMBER"].ToString();
        //        userdb.countryCode = dataTable.Rows[0]["countryCode"].ToString();
        //        userdb.isActive = Convert.ToInt32(dataTable.Rows[0]["isActive"].ToString());
        //        userdb.rating = Convert.ToDouble(dataTable.Rows[0]["RATING"].ToString());
        //        userdb.userType = Convert.ToInt32(dataTable.Rows[0]["USERTYPE"].ToString());
        //        userdb.accessToken = dataTable.Rows[0]["ACCESSTOKEN"].ToString();
        //        userdb.img = dataTable.Rows[0]["IMG"].ToString();
        //        userdb.socialId = dataTable.Rows[0]["SOCIAL_ID"].ToString();

        //    }
        //    else
        //        return null;

        //    return userdb;
        //}

        public UserDb GetPhoneInfoByUsername(string username)
        {
            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetObjectByColname<string>("users_v", "username", username));
            UserDb userdb = new UserDb();

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);



            userdb.phoneNumber = dataTable.Rows[0]["PHONENUMBER"].ToString();
            userdb.countryCode = dataTable.Rows[0]["countryCode"].ToString();


            return userdb;
        }


        public UserDb GetPhoneInfoByUser_id(string user_id)
        {
            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetObjectByColname<string>("users_v", "user_id", user_id));
            UserDb userdb = new UserDb();

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            userdb.phoneNumber = dataTable.Rows[0]["PHONENUMBER"].ToString();
            userdb.countryCode = dataTable.Rows[0]["countryCode"].ToString();


            return userdb;
        }

        public UserDb GetPhoneInfoByPhoneNumber(string phoneNumber)
        {
            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetObjectByColname<string>("users_v", "phoneNumber", phoneNumber));
            UserDb userdb = new UserDb();
            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            userdb.phoneNumber = dataTable.Rows[0]["PHONENUMBER"].ToString();
            userdb.countryCode = dataTable.Rows[0]["countryCode"].ToString();


            return userdb;
        }

        //public UserDb GetUserBySocialId(string socialId)
        //{


        //    UserQuery userQuery = new UserQuery();
        //    System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetObjectByColname<string>("users_v", "social_id", socialId));
        //    UserDb userdb = new UserDb();
        //    if (dataTable.Rows.Count > 0)
        //    {

        //        userdb.userId = Convert.ToInt32(dataTable.Rows[0]["USER_ID"].ToString());
        //        userdb.username = dataTable.Rows[0]["USERNAME"].ToString();
        //        userdb.fullname = dataTable.Rows[0]["FULLNAME"].ToString();
        //        userdb.email = dataTable.Rows[0]["EMAIL"].ToString();
        //        userdb.password = dataTable.Rows[0]["PASSWORD"].ToString();
        //        userdb.phoneNumber = dataTable.Rows[0]["PHONENUMBER"].ToString();
        //        userdb.countryCode = dataTable.Rows[0]["countryCode"].ToString();
        //        userdb.isActive = Convert.ToInt32(dataTable.Rows[0]["isActive"].ToString());
        //        userdb.rating = Convert.ToDouble(dataTable.Rows[0]["RATING"].ToString());
        //        userdb.userType = Convert.ToInt32(dataTable.Rows[0]["USERTYPE"].ToString());
        //        userdb.accessToken = dataTable.Rows[0]["ACCESSTOKEN"].ToString();
        //        userdb.img = dataTable.Rows[0]["IMG"].ToString();
        //        userdb.socialId = dataTable.Rows[0]["SOCIAL_ID"].ToString();

        //    }
        //    else
        //        return null;

        //    return userdb;
        //}

        public string GetUsernameByUserId(int user_id)
        {


            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetObjectsByColName("users", "username", "id", user_id.ToString()));
            UserDb userdb = new UserDb();

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            return dataTable.Rows[0]["username"].ToString();



        }

        //public UserDb GetUserByUsername(string username)
        //{


        //    UserQuery userQuery = new UserQuery();
        //    System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetObjectByColname<string>("users_v", "username", username));
        //    UserDb userdb = new UserDb();
        //    if (dataTable.Rows.Count > 0)
        //    {

        //        userdb.userId =Convert.ToInt32( dataTable.Rows[0]["USER_ID"].ToString());
        //        userdb.username = dataTable.Rows[0]["USERNAME"].ToString();
        //        userdb.fullname = dataTable.Rows[0]["FULLNAME"].ToString();
        //        userdb.email = dataTable.Rows[0]["EMAIL"].ToString();
        //        userdb.password = dataTable.Rows[0]["PASSWORD"].ToString();
        //        userdb.phoneNumber = dataTable.Rows[0]["PHONENUMBER"].ToString();
        //        userdb.countryCode = dataTable.Rows[0]["countryCode"].ToString();
        //        userdb.isActive = Convert.ToInt32(dataTable.Rows[0]["isActive"].ToString());
        //        userdb.rating = Convert.ToDouble(dataTable.Rows[0]["RATING"].ToString());
        //        userdb.userType = Convert.ToInt32(dataTable.Rows[0]["USERTYPE"].ToString());
        //        userdb.accessToken = dataTable.Rows[0]["ACCESSTOKEN"].ToString();
        //        userdb.img = dataTable.Rows[0]["IMG"].ToString();
        //        userdb.socialId = dataTable.Rows[0]["SOCIAL_ID"].ToString();


        //    }
        //    else
        //        return null;


        //    return userdb;
        //}

        public string GetWarshaImageByUser_id(int user_id)
        {
            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetObjectByColname<string>("users_v", "user_id", user_id.ToString()));
            UserDb userdb = new UserDb();

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);


            return dataTable.Rows[0]["IMG"].ToString();

        }

        public bool InsertAccessToken(string accessToken, string username)
        {

            return new UserQuery(language).InsertAccessTokenByPhoneNumber(accessToken, username);

        }


        public bool InsertAccessTokenByPhoneNumber(string accessToken, string phoneNumber)
        {

            return new UserQuery(language).InsertAccessTokenByPhoneNumber(accessToken, phoneNumber);

        }

        public bool InsertImageUrl(int user_id, string imageUrl)
        {

            return new UserQuery(language).InsertImageUrl(user_id, imageUrl);

        }

        public string GenerateAccessToken(int length)
        {
            Random random = new Random();
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
            var builder = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                var c = pool[random.Next(0, pool.Length)];
                builder.Append(c);
            }
            return builder.ToString();
        }

        public bool CheckUsernameAvailability(string username)
        {
            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetObjectByColname("users", "username", username));
            if (dataTable.Rows.Count > 0) // if exist return false  , do not insert 
                return true;
            else
                return false;
        }

        public bool CheckphoneNumberAvailability(string phoneNumber)
        {

            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetObjectByColname("users", "phoneNumber", phoneNumber));
            if (dataTable.Rows.Count > 0) // if exist return false  , do not insert 
                return true;
            else
                return false;
        }

        public bool InsertRegistrationObject(RegisterCriteria registerCriteria)
        {
            return new UserQuery(language).InsertUserRegisterData(registerCriteria);
        }
        public bool InsertPerson(PersonRegister person)
        {

            return new UserQuery(language).InsertPerson(person);
        }

        public bool UpdateLastLogin(string user_id)
        {

            try
            {
                UserQuery userQuery = new UserQuery(language);
                string resp = conn_db.ExecuteSql(new UserQuery(language).UpdateLastLogin(user_id));

            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }


            return true;
        }

        public bool UpdateLastLoginPhone(string phone)
        {

            try
            {
                UserQuery userQuery = new UserQuery(language);
                string resp = conn_db.ExecuteSql(new UserQuery(language).UpdateLastLoginPhone(phone));

            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }


            return true;
        }
        public bool InsertSupplier(SupplierRegister supplier)
        {

            return new UserQuery(language).InsertSupplier(supplier);
        }


        public bool DeleteUserByPhoneNumber(string phoneNumber)
        {

            return new UserQuery(language).DeleteUserByPhoneNumber(phoneNumber);
        }
        public bool ResetUserPhoneNumber(string phoneNumber)
        {

            return new UserQuery(language).ResetUserPhoneNumber(phoneNumber);
        }

        public bool ChangeSupplierType(int user_id, int supplierType_id)
        {

            return new UserQuery(language).ChangeSupplierType(user_id, supplierType_id);
        }

        public bool DeleteUserByUsername(string username)
        {
            UserQuery userQuery = new UserQuery(language);
            return userQuery.DeleteUserByUsername(username);
        }

        public int GetUserIdByAccessToken(string accessToken)
        {
            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetObjectByColname("users", "accessToken", accessToken));
            if (dataTable.Rows.Count > 0)
            {
                return Convert.ToInt32(dataTable.Rows[0]["id"].ToString());
            }
            else
                return -1;
        }

        public int GetUserIdByWarshaId(int supplier_id)
        {
            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetObjectByColname("supplier", "id", supplier_id));
            if (dataTable.Rows.Count > 0)
            {
                return Convert.ToInt32(dataTable.Rows[0]["user_id"].ToString());
            }
            else
                return -1;
        }

        public int GetSupplierIdByAccessToken(string accessToken)
        {
            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetSupplierIdByAccessToken(accessToken));
            if (dataTable.Rows.Count > 0)
            {
                return Convert.ToInt32(dataTable.Rows[0]["id"].ToString());
            }
            else
                return -1;
        }
        public int GetSupplierUserIdByOfferId(int offer_id)
        {
            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetSupplierUserIdByOfferId(offer_id));
            if (dataTable.Rows.Count > 0)
            {
                return Convert.ToInt32(dataTable.Rows[0]["user_id"].ToString());
            }
            else
                return -1;
        }


        public int GetWorkshopIdByUser_id(int user_id)
        {
            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetWorkshopIdByUser_id(user_id));
            if (dataTable.Rows.Count > 0)
            {
                return Convert.ToInt32(dataTable.Rows[0]["id"].ToString());
            }
            else
                return -1;
        }


        public int GetWorkshopIdByAccessTokenView(string accessToken)
        {
            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetWorkshopIdByAccessTokenView(accessToken));

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            return Convert.ToInt32(dataTable.Rows[0]["id"].ToString());
        }


        public int GetUserIdByUsername(string username)
        {
            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetObjectByColname("users", "username", username));
            if (dataTable.Rows.Count > 0)
            {
                return Convert.ToInt32(dataTable.Rows[0]["id"].ToString());
            }
            else
                return -1;
        }

        public int GetUserIdByPhoneNumber(string phoneNumber)
        {
            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetObjectByColname("users", "PHONENUMBER", phoneNumber));
            if (dataTable.Rows.Count > 0)
            {
                return Convert.ToInt32(dataTable.Rows[0]["id"].ToString());
            }
            else
                return -1;
        }

        public bool CheckAccessToken(string AccessToken)
        {
            UserQuery UserQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(UserQuery.CheckAccessToken(AccessToken));

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            return true;
        }

        public string CheckAccessTokenUser(string AccessToken)
        {
            System.Data.DataTable dataTable;
            try
            {
                UserQuery UserQuery = new UserQuery(language);
                dataTable = conn_db.ReadTable(UserQuery.CheckAccessTokenUser(AccessToken));
            }

            catch
            {
                throw new EmptyViewException(language);
            }

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            return dataTable.Rows[0]["ID"].ToString();
        }


        public bool UpdateUserImageUrl(string image_url, int user_id)
        {
            return new UserQuery(language).UpdateUserImageUrl(image_url, user_id);
        }

        public bool DeleteImageByUrl(string image_url)
        {
            return new UserQuery(language).DeleteImageUrl(image_url);
        }

        public bool UpdatePasswordByPhone(string phoneNumber, string newPassword)
        {

            return new UserQuery(language).UpdatePasswordByPhone(phoneNumber, newPassword);

        }

        //public bool UpdateUserImage(string imageName, string user_id)
        //{

        //    return new UserQuery(language).UpdateUserImage(imageName, Convert.ToInt32(user_id));

        //}

        public bool UpdatePasswordByUsername(string username, string newPassword)
        {

            return new UserQuery(language).UpdatePasswordByUsername(username, newPassword);

        }

        public bool UpdateProfile(UserDTO userR, int user_id)
        {

            return new UserQuery(language).UpdateProfile(userR, user_id);

        }

        //public bool UpdatePersonProfile(PersonDb person, int user_id)
        //{
        //    UserQuery userQuery = new UserQuery();
        //    try
        //    {
        //        return userQuery.UpdatePersonProfile(person, user_id);
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        //public bool UpdateWarshaProfile(WorkshopDb workshop, int user_id)
        //{
        //    UserQuery userQuery = new UserQuery();
        //    try
        //    {
        //        return userQuery.UpdateWarshaProfile(workshop, user_id);
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        //public PersonDb GetPersonProfile(int user_id)
        //{
        //    UserQuery userQuery = new UserQuery();
        //    System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetObjectByColname("persons_v", "user_id", user_id));
        //    PersonDb person = new PersonDb();
        //    if (dataTable.Rows.Count > 0)
        //    {

        //        person.id = Convert.ToInt32(dataTable.Rows[0]["ID"].ToString());
        //        person.fullname = dataTable.Rows[0]["FULLNAME"].ToString();
        //        person.IqammaNumber = Convert.ToInt32(dataTable.Rows[0]["IQAMMANUMBER"].ToString());
        //        person.rating = Convert.ToDouble(dataTable.Rows[0]["RATING"].ToString());

        //    }
        //    else
        //        return null;

        //    return person;
        //}

        //public WorkshopDb GetWarshaProfile(int user_id)
        //{
        //    UserQuery userQuery = new UserQuery();
        //    System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetObjectByColname("workshop_v", "user_id", user_id));
        //    WorkshopDb workshop = new WorkshopDb();
        //    if (dataTable.Rows.Count > 0)
        //    {

        //        workshop.id = Convert.ToInt32(dataTable.Rows[0]["ID"].ToString());
        //        workshop.fullName = dataTable.Rows[0]["FULLNAME"].ToString();
        //        workshop.recordNumber = dataTable.Rows[0]["RECORDNUMBER"].ToString();

        //        try
        //        {workshop.expiryDate = Convert.ToDateTime(dataTable.Rows[0]["EXPIRYDATE"].ToString());}
        //        catch
        //        {workshop.expiryDate = DateTime.Now;}

        //        workshop.shopNumber = dataTable.Rows[0]["SHOPNUMBER"].ToString();
        //        workshop.shopName = dataTable.Rows[0]["SHOPNAME"].ToString();

        //        try
        //        { workshop.expiryDate = Convert.ToDateTime(dataTable.Rows[0]["EXPIRYDATE"].ToString()); }
        //        catch
        //        { workshop.expiryDate = DateTime.Now; }

        //        try
        //        { workshop.LocationX = Convert.ToDouble(dataTable.Rows[0]["LOCATIONX"].ToString()); }
        //        catch
        //        { workshop.LocationX = 0; }

        //        try
        //        { workshop.LocationY = Convert.ToDouble(dataTable.Rows[0]["LOCATIONY"].ToString()); }
        //        catch
        //        { workshop.LocationY = 0; }

        //        try
        //        { workshop.industrialAreaId = Convert.ToInt32(dataTable.Rows[0]["INDUSTRIALAREA_ID"].ToString()); }
        //        catch
        //        { workshop.industrialAreaId = 0; }

        //        try
        //        { workshop.rating = Convert.ToDouble(dataTable.Rows[0]["RATING"].ToString()); }
        //        catch
        //        { workshop.rating = 0; }

        //        workshop.industrialAreaName = dataTable.Rows[0]["AREANAME"].ToString();
        //        workshop.cityId =Convert.ToInt32(dataTable.Rows[0]["CITY_ID"].ToString());
        //        workshop.cityName =  dataTable.Rows[0]["CITYNAME"].ToString();




        //    }
        //    else
        //        return null;

        //    return workshop;
        //}

        public double GetRaingByUserId(int user_id)
        {
            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetObjectByColname("rating", "user_id", user_id));
            double rating = new Rating().ratingRatio;

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);


            rating = Convert.ToDouble(dataTable.Rows[0]["ID"].ToString());


            return rating;
        }

        public List<List<string>> GetUsers(GetCriteria getCriteria)
        {
            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable;


            string where = " where 1=1 ";

            if (getCriteria.user_id != 0)
                where += " and user_id =" + getCriteria.user_id;

            if (getCriteria.userType != 0)
                where += " and user_type_id =" + getCriteria.userType;

            if (getCriteria.lastLogin != "")
                where += " and trunc(lastlogin) = to_date('" + getCriteria.lastLogin + "','yyyy-mm-dd')";


            if (getCriteria.log != 0)
                where += " and log =" + getCriteria.log;


            dataTable = conn_db.ReadTable("select *  from users" + where);



            List<string> userDTO;
            List<List<string>> userDTOs = new List<List<string>>();

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                //userDTO = new UserDTO();
                //userDTO.user_id = Convert.ToInt32(row["ID"]);
                //userDTO.phoneNumber =  row["phoneNumber"].ToString()  ;
                //userDTO.userType = Convert.ToInt32(row["user_type_id"]);
                //userDTO.registeredDate = row["REGISTERED_DATE"].ToString();
                //userDTO.countryCode = Convert.ToInt32(row["COUNTRY_CODE"]) ;

                userDTO = new List<string>();
                userDTO.Add(row["ID"].ToString());
                userDTO.Add(row["COUNTRY_CODE"].ToString());
                userDTO.Add(row["phoneNumber"].ToString());
                userDTO.Add(row["user_type_id"].ToString());
                userDTO.Add(row["REGISTERED_DATE"].ToString());


                userDTOs.Add(userDTO);
            }



            return userDTOs;
        }

        public object GetUsersCount(GetCriteria getCriteria)
        {
            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable;


            string where = " where 1=1 ";

            if (getCriteria.user_id != 0)
                where += " and user_id =" + getCriteria.user_id;

            if (getCriteria.userType != 0)
                where += " and user_type_id =" + getCriteria.userType;

            if (getCriteria.lastLogin != "")
                where += " and trunc(lastlogin) = to_date('" + getCriteria.lastLogin + "','yyyy-mm-dd')";


            if (getCriteria.log != 0)
                where += " and log =" + getCriteria.log;


            dataTable = conn_db.ReadTable("select count(id) from users" + where);




            int usersCount = 0;
            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            foreach (System.Data.DataRow row in dataTable.Rows)
            {

                usersCount = Convert.ToInt32(row[0]);

            }



            return new { usersCount = usersCount };
        }

        public UserPage GetUserPage(int user_id)
        {
            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(userQuery.GetObjectsByColName("users_v", "URL", "user_id", user_id.ToString()));
            UserPage userPage = new UserPage();

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            userPage.URL = dataTable.Rows[0]["URL"].ToString();
            return userPage;


        }

        public List<SupplierType> GetSupplierType(string lang)
        {
            UserQuery userQuery = new UserQuery(language);
            System.Data.DataTable dataTable;



            SupplierType type;
            dataTable = conn_db.ReadTable(userQuery.GetMasterTranslated("suppliertype", lang));

            List<SupplierType> workshopTypes = new List<SupplierType>();

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);


            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                type = new SupplierType();
                type.id = Convert.ToInt32(row["ID"].ToString());
                type.typeName = row["suppliertype_name"].ToString();
                workshopTypes.Add(type);
            }


            return workshopTypes;


        }

        public bool SetRatingToUser(int RaterUser_id, int RatedUser_id, int starNo)
        {

            return new UserQuery(language).InsertRatingToUser(RaterUser_id, RatedUser_id, starNo);


        }
        public bool SetRatingByOffer(int offer_id, int starNo)
        {

            return new UserQuery(language).InsertRatingByOfferId(offer_id, starNo);


        }

        public bool UpdateDeviceId(int user_id, string device_id, int phoneType)
        {
            return new UserQuery(language).UpdateDeviceId(user_id, device_id, phoneType);
        }

        public bool DeleteDeviceId(string user_id, string device_id)
        {

            return new UserQuery(language).DeleteDeviceId(user_id, device_id);

        }

        public bool DeleteAllUserDevices(string user_id)
        {

            return new UserQuery(language).DeleteAllUserDevices(user_id);

        }


        public bool SetIsActiveTrue(string username)
        {

            return new UserQuery(language).UpdateIsActive(username);

        }

    }
}
