using Domain.Entities;
using Domain.Entities.Login;
using Domain.Entities.Person;
using Domain.Entities.supplier;
using Domain.Enums;
using Domain.Exceptions;
using Login;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;


namespace DbQueries
{
    public class UserQuery : BaseQuery
    {
        string language = "en";

        public UserQuery(string language)
        {
            this.language = language;
        }


       
        public string GetSupplierIdByAccessToken(string accessToken)
        {
            string Query = "select  id from supplier where user_id in (select id from users where accessToken = '"+accessToken+"' ) ";
            return Query;
        }
        public string GetSupplierUserIdByOfferId(int  offer_id)
        {
            string Query = " select  user_id  from supplier  s join offer on s.id = offer.supplier_id where offer.id = "+ offer_id;
            return Query;
        }

        public string GetWorkshopIdByUser_id(int user_id)
        {
            string Query = "select  id from workshop where user_id ="+user_id;
            return Query;
        }


        public string GetWorkshopIdByAccessTokenView(string accessToken)
        {
            string Query = "select  id from workshop_v where accessToken ='" + accessToken + "'"; 
            return Query;
        }

        public string GetProfileByPhoneForPerson(string phoneNumber  )
        {
            string Query = "  select p.* , city_name  from profiles p left join city_trans_v ct on p.cityId = ct.id  where   phoneNumber = '"+ phoneNumber + "'  ";
            return Query;
        }


        public string GetProfileByPhoneForWorkshop(string phoneNumber, string lang)
        {
            string Query = "  select p.* , city_name  from profiles p left join city_trans_v ct on p.cityId = ct.id  where city_lang ='"+ lang + "' and phoneNumber = '"+ phoneNumber + "' ";
            return Query;
        }

        public string GetProfileByUserIdForPerson(string user_id)
        {
            string Query = "  select p.* , city_name  from profiles p left join city_trans_v ct on p.cityId = ct.id  where   user_id =  " + user_id + "   ";
            return Query;
        }


        public string GetProfileByUserIdForSupplier(string user_id, string lang)
        {
            string Query = "  select p.* , city_name ,suppliertype_name  from profiles p left join city_trans_v ct on p.cityId = ct.id   " +
                "  left join suppliertype_trans_v st on p.suppliertype_Id = st.id  where city_lang ='" + lang + "' and SUPPLIERTYPE_LANG='"+lang+"'  and user_id =  " + user_id + "  ";
            return Query;
        }

        public string GetWorkshopType(string lang)
        {
            string Query = "select * from city " +
                "join city_translation    on city.id = city_translation.city_non_trans_id " +
                "join language lang on city_translation.lang_id    = lang.id " +
                "where city.isactive = 1 and lang.name ='ar'";
            return Query;
        }

        public string GetUserTypeByAccessToken(string accessToken)
        {
            string Query = " select usertype from users_v where accessToken = '" + accessToken + "'   ";
            return Query;
        }
        public string GetUserTypeByUserId(string user_id)
        {
            string Query = " select usertype from users_v where user_id = '" + user_id + "'   ";
            return Query;
        }

        public string GetUserTypeByPhone(string phoneNumber)
        {
            string Query = " select usertype from users_v where phonenumber = '" + phoneNumber + "'   ";
            return Query;
        }


        public string GetAccessTokenByUsername(string username)
        {
            string Query = " select accesstoken from users_v where username = '" + username + "'  ";
            return Query;
        }

        public string GetAccessTokenByPhoneNumber(string phoneNumber)
        {
            string Query = " select accesstoken from users_v where phoneNumber = '" + phoneNumber + "'  ";
            return Query;
        }

        public string GetPasswordByPhoneNumber(string phoneNumber)
        {
            string Query = " select password from users  where phoneNumber = '" + phoneNumber + "'  ";
            return Query;
        }

   

        public string CheckAccessToken(string AccessToken)
        {
            string Query = "select  accessToken  from users where accessToken ='" + AccessToken + "' ";
            return Query;
        }

        public string CheckAccessTokenUser(string AccessToken)
        {
            string Query = "select  id  from users where accessToken ='" + AccessToken + "' ";
            return Query;
        }

        public string GetPersonIdOfferId(int  offer_id)
        {
            string Query = "select user_id ,ORDER_IDENTITY  from offer join orders on  offer.order_id = orders.id where offer.id = " + offer_id;
            return Query;
        }

        public string GetPersonIdOrderId(int offer_id)
        {
            string Query = "select user_id ,ORDER_IDENTITY  from orders  where id = " + offer_id;
            return Query;
        }

        public string GetPersonIdOfferId(string AccessToken)
        {
            string Query = "select  id  from users where accessToken ='" + AccessToken + "' ";
            return Query;
        }
        public string GetSuppliersByCity(int cityId)
        {
            string Query = "select  user_id  from supplier  where city_Id  = "+cityId;
            return Query;
        }



        public string GetPersonProfile(int user_id)
        {

            string Query = "select  *  from person where user_id =" + user_id + " ";
            return Query;


        }
        public string GetWarshaProfile(int user_id)
        {

            string Query = "select  *  from WORKSHOP where user_id =" + user_id + " ";
            return Query;


        }


        public string UpdateLastLogin(string user_id)
        {

            string Query = "update users set lastlogin =sysdate , log =1 where id =  " + user_id  ;
            return Query;


        }

        public string UpdateLastLoginPhone(string phone)
        {

            string Query = "update users set lastlogin =sysdate , log =1  where phonenumber =  '" + phone + "'";
            return Query;


        }




        public bool InsertImageUrl(int user_id, string imageUrl)
        {
            
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                new OracleParameter("@imageUrl",imageUrl),
                  new OracleParameter("@user_id",user_id),

                               };
                procConn.RunProc("InsertImageUrl_sp", param, 0);
                return true;
            }
            catch (Exception ex)
            {
                throw new InsertException(language, ex.Message);
            }



        }


        public bool DeleteImageUrl(  string imageUrl)
        {

            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                new OracleParameter("@pic_Url",imageUrl),
               

                               };
                procConn.RunProc("DeleteImageUrl_sp", param, 0);
                return true;
            }
            catch (Exception ex)
            {
                throw new DeleteException(language, ex.Message);
            }



        }



        public bool InsertAccessTokenByPhoneNumber(string accessToken, string phoneNumber)
        {

            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                new OracleParameter("@accessToken",accessToken),
                  new OracleParameter("@phoneNumber",phoneNumber),

                               };
                procConn.RunProc("InsertTokenByPhone_sp", param, 0);
                return true;
            }
            catch (Exception ex)
            {
                throw new InsertException(language, ex.Message);
            }

        }

        public bool InsertAccessToken(string accessToken, string username)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                new OracleParameter("@accessToken",accessToken),
                  new OracleParameter("@username",username),

                               };
                procConn.RunProc("InsertAccessToken_sp", param, 0);
                return true;
            }
            catch (Exception ex)
            {
                throw new InsertException(language, ex.Message);
            }


        }



        public bool UpdatePasswordByUsername(string username, string password)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                new OracleParameter("@password",password),
                  new OracleParameter("@username",username),

                               };
                procConn.RunProc("UpdatePasswordByUsername_sp", param, 0);
                return true;
            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }


        }



        public bool UpdatePasswordByPhone(string phoneNumber, string password)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                new OracleParameter("@password",password),
                  new OracleParameter("@phoneNumber",phoneNumber),

                               };
                procConn.RunProc("UpdatePasswordByPhone_sp", param, 0);
                return true;
            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }

        }


        public bool UpdateUserImage(string imageName, int user_id)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                new OracleParameter("@p_userImage",imageName),
                  new OracleParameter("@p_user_id",user_id),

                               };
                procConn.RunProc("UpdateUserImage_sp", param, 0);
                return true;
            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }

        }



        public bool UpdateIsActive(string username)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                new OracleParameter("@username",username),

                               };
                procConn.RunProc("UpdateIsActive_sp", param, 0);
                return true;
            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }

        }



        public bool DeleteDeviceId(string user_id , string device_id)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                new OracleParameter("@user_id",user_id),
                new OracleParameter("@device_id",device_id),
                               };
                procConn.RunProc("DeleteDeviceId_sp", param, 0);
                return true;
            }
            catch (Exception ex)
            {
                throw new DeleteException(language ,ex.Message);
            }

        }

        public bool DeleteAllUserDevices(string user_id)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                new OracleParameter("@user_id",user_id),
                               };
                procConn.RunProc("DeleteAllUserDevices_sp", param, 0);
                return true;
            }
            catch (Exception ex)
            {
                throw new DeleteException(language, ex.Message);
            }

        }


        public bool UpdateDeviceId(int user_id, string device_id, int phoneType)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
      new OracleParameter("@user_id",user_id),
      new OracleParameter("@device_id",device_id),
      new OracleParameter("@phoneType",phoneType),

                               };
                procConn.RunProc("UpdateDeviceId_sp", param, 0);
                return true;
            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }

        }




        public bool InsertRatingToUser(int raterId, int ratedId, int starNo)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
      new OracleParameter("@raterId",raterId),
      new OracleParameter("@ratedId",ratedId),
      new OracleParameter("@starNo",starNo),
      
                               };
                procConn.RunProc("InsertRaingToUser_sp", param, 0);
                return true;
            }
            catch (Exception ex)
            {
                throw new InsertException(language, ex.Message);
            }

        }

        public bool InsertRatingByOfferId(int offer_id ,int stars)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
      new OracleParameter("@offer_id",offer_id),
      new OracleParameter("@stars",stars),

                               };
                procConn.RunProc("InsertRaingByOffer_sp", param, 0);
                return true;
            }
            catch (Exception ex)
            {
                throw new InsertException(language, ex.Message);
            }

        }


        public bool InsertUserRegisterData(RegisterCriteria registerCriteria)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
      new OracleParameter("@username",registerCriteria.username),
      new OracleParameter("@password",registerCriteria.password),
      new OracleParameter("@phoneNumber",registerCriteria.phoneNumber),
      new OracleParameter("@countryCode",registerCriteria.countryCode),
      new OracleParameter("@isActive",registerCriteria.isActive),
      new OracleParameter("@user_type_id",registerCriteria.userType),
      new OracleParameter("@email",registerCriteria.email),
      new OracleParameter("@socialId",registerCriteria.socialId),
      new OracleParameter("@userImage",registerCriteria.userImage)
                               };
                procConn.RunProc("insertUserRegisterData_sp", param, 0);
                return true;
            }
            catch (Exception ex)
            {
                throw new InsertException(language, ex.Message);
            }

        }



        public bool InsertPerson(PersonRegister Person)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
      new OracleParameter("@firstName",Person.firstName),
      new OracleParameter("@lastName",Person.lastName),
      new OracleParameter("@phoneNumber",Person.phoneNumber),
      new OracleParameter("@country_code",Person.country_code),

                               };
                procConn.RunProc("insertPerson_sp", param, 0);
                return true;
            }
            catch (Exception ex)
            {
                throw new InsertException(language, ex.Message);
            }

        }


        public bool InsertSupplier(SupplierRegister supplier)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
         new OracleParameter("@phoneNumber",supplier.phoneNumber),
         new OracleParameter("@country_code",supplier.country_code),
         new OracleParameter("@password",supplier.password),
         new OracleParameter("@image",supplier.image),

         new OracleParameter("@supplierName",supplier.supplierName),
         new OracleParameter("@storeName",supplier.storeName),
         new OracleParameter("@storeNo",supplier.storeNo),
         new OracleParameter("@supplierType",supplier.supplierType),
         new OracleParameter("@city_id",supplier.city),
         new OracleParameter("@address",supplier.address),
         new OracleParameter("@locationX",supplier.locationX),
         new OracleParameter("@locationY",supplier.locationY),
        
 

                               };
                procConn.RunProc("insertSupplier_sp", param, 0);
                return true;
            }
            catch (Exception ex)
            {
                throw new InsertException(language, ex.Message);
            }

        }


        public bool DeleteUserByPhoneNumber(string phoneNumber)
        {
            try
            {
                int unconfirm = (int)Domain.Enums.Enums.orderStatus.UNCONFIRMED;
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                   new OracleParameter("@p_phoneNumber",phoneNumber)    ,
                   new OracleParameter("@p_unconfirm",unconfirm),
                   new OracleParameter("@P_confirm_id",(int)Domain.Enums.Enums.orderStatus.CONFIRMED),
                               };
                procConn.RunProc("DeleteUserByPhoneNumber_sp", param, 0);
                return true;
            }
            catch (Exception ex)
            {
                throw new DeleteException(language, ex.Message);
            }

        }

        public bool ResetUserPhoneNumber(string phoneNumber)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
      new OracleParameter("@p_phoneNumber",phoneNumber)

                               };
                procConn.RunProc("ResetUserPhoneNumber_sp", param, 0);
                return true;
            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }

        }

        public bool ChangeSupplierType(int user_id , int supplierType_id)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                new OracleParameter("@p_user_id",user_id) ,
                new OracleParameter("@p_supplierType_id",supplierType_id)

                               };
                procConn.RunProc("ChangeSupplierType_sp", param, 0);
                return true;
            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }

        }


        public bool DeleteUserByUsername(string username)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                new OracleParameter("@p_phoneNumber",username)

                               };
                procConn.RunProc("DELETEUSERBYUSERNAME_SP", param, 0);
                return true;
            }
            catch (Exception ex)
            {
                throw new DeleteException(language, ex.Message);
            }

        }



        public bool UpdateUserImageUrl(string imageUrl , int user_id )
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                    new OracleParameter("@p_user_id", user_id),
                  new OracleParameter("@p_pic_url", imageUrl),
                             };

                procConn.RunProc("UpdateUserPic_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }

        }


        public bool UpdateProfile(UserDTO userR , int user_id)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                  new OracleParameter("@u_user_id", user_id),
                  new OracleParameter("@u_firstName", userR.firstName),
                  new OracleParameter("@u_lastName", userR.lastName),
                  new OracleParameter("@u_phoneNumber", userR.phoneNumber),
                  new OracleParameter("@u_countryCode", userR.countryCode),
                  new OracleParameter("@u_image", userR.userImage),
                  new OracleParameter("@w_shopNumber", userR.shopNumber),
                  new OracleParameter("@w_shopName", userR.shopName),
                  new OracleParameter("@w_cityId", userR.cityId),
                  new OracleParameter("@w_address", userR.address),
                  new OracleParameter("@w_LocationX", userR.Lat),
                  new OracleParameter("@w_LocationY", userR.Lng),

                             };

                procConn.RunProc("updateProfile_sp", param);

                return true;
            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }

        }


        //public bool UpdatePersonProfile(PersonDb person , int user_id)
        //{
        //    try
        //    {
        //        CommanDB procConn = new CommanDB();
        //        OracleParameter[] param = {
        //          new OracleParameter("@p_user_id", user_id),
        //          new OracleParameter("@p_fullname", person.fullname),
        //          new OracleParameter("@p_IqammaNumber", person.IqammaNumber),
                  
        //                     };

        //        procConn.RunProc("UpdatePersonProfile_sp", param);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //}

        //public bool UpdateWarshaProfile(WorkshopDb workshop, int user_id)
        //{
        //    try
        //    {
        //        CommanDB procConn = new CommanDB();
        //          OracleParameter[] param = {
        //          new OracleParameter("@p_user_id", user_id),
        //          new OracleParameter("@p_fullName", workshop.fullName),
        //          new OracleParameter("@p_recordNumber", workshop.recordNumber),
        //          new OracleParameter("@p_expiryDate", workshop.expiryDate.ToString("yyyy-MM-dd")),
        //          new OracleParameter("@p_shopNumber", workshop.shopNumber),
        //           new OracleParameter("@p_shopName", workshop.shopName),
        //          new OracleParameter("@p_LocationX", workshop.LocationX),
        //          new OracleParameter("@p_LocationY", workshop.LocationY),
        //          new OracleParameter("@p_industrialArea_id", workshop.industrialAreaId),
               
        //                     };
        //        procConn.RunProc("UpdateWarshaProfile_sp", param);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //}

        

            
    }
}
