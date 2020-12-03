using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.HelperClass;
using Domain.Entities.Login;
using Domain.Entities.Person;
using Domain.Entities.supplier;
 
using Domain.Exceptions;
using Domain.Messages;
using HelperClass;
using Login;
using Repository.HelperRepository;
using Repository.UploadRepository;
using UserRepository;
using static Domain.Enums.Enums;
using static Domain.Messages.Messages;

namespace Service.UserService
{
    public class UserService : Domain.Interfaces.User.IUserService
    {
        string language = "en";
        public UserService(string language)
        {
            this.language = language;
        }

       

       


 

        public Response LoginPerson(string phoneNumber, string lang)
        {


            try
            {

                UserDTO user = new UserRepository.UserRepository(language).GetProfileByPhone(phoneNumber, lang, 1);
                user.accessToken = GenerateAccessTokenByPhoneNumber(phoneNumber);

                if (user.userType == (int)UserType.workshop)
                    return new Response(false, Messages.GetMessage(language, Messages.TypeM.USER, userM.PHONE_NOT_PERSON));


                return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, defaultM.DATAGOT), user);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(false, EmptyViewException.RespMessage );
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }

        }

        public Response LoginControl(string phoneNumber ,string password)
        {


            try
            {

                LoginDTO loginObject = new UserRepository.UserRepository(language).LoginControl(phoneNumber, password);
                string accessToken = ""; 
                if (loginObject !=null)
                {
                    accessToken = GenerateAccessTokenByPhoneNumber(phoneNumber);
                    return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, defaultM.DATAGOT), loginObject);
                }
                else
                {
                    return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, defaultM.DATAGOT), "NO");
                }
                  

               


              
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(false, EmptyViewException.RespMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }

        }


        public Response RegisterPerson(PersonRegister person, string lang)
        {
            try
            {

                if (new UserRepository.UserRepository(language).InsertPerson(person))
                {
                    // Return Profile 
                    UserDTO user = new UserRepository.UserRepository(language).GetProfileByPhone(person.phoneNumber, lang, 1);
                    user.accessToken = GenerateAccessTokenByPhoneNumber(user.phoneNumber);
                    return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.INSERT_CORRECT), user);
                }

                else
                    throw new InsertException(language);
            }
            catch (InsertException InsertException)
            {
                return new Response(false, InsertException.RespMessage, InsertException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }

        }


        public Response SetLastLogin(string user_id)
        {
            try
            {
                if (new UserRepository.UserRepository(language).UpdateLastLogin(user_id))
                    return new Response(true, GetMessage(language, TypeM.DEFAULT, defaultM.UPDATE_CORRECT));
                else
                    throw new DeleteException(language);
            }

            catch (UpdateException UpdateException)
            {
                return new Response(false, UpdateException.RespMessage, UpdateException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }

        }

        public Response SetLastLoginPhone(string phone)
        {
            try
            {
                if (new UserRepository.UserRepository(language).UpdateLastLoginPhone(phone))
                    return new Response(true, GetMessage(language, TypeM.DEFAULT, defaultM.UPDATE_CORRECT));
                else
                    throw new DeleteException(language);
            }

            catch (UpdateException UpdateException)
            {
                return new Response(false, UpdateException.RespMessage, UpdateException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }

        }

        public Response RegisterSupplier(SupplierRegister supplier, string lang)
        {
            try
            {


                // Store Image 
                string url = "";
                if (supplier.image != "")
                {
                    string ImageName = "images/UserImages/" + supplier.phoneNumber + ".png";
                    url = new UploadRepository(language).StoreImage(supplier.image, ImageName);

                    if (url != "")
                    {
                        supplier.image = "images/UserImages/" + supplier.phoneNumber + ".png";
                    }
                    else
                    {
                        supplier.image = "";
                    }

                }


                // hashing Password 
                string hashedPassword = PasswordHash.CreateHash(supplier.password);
                supplier.password = hashedPassword;


                    // insert Supplier Data
                    new UserRepository.UserRepository(language).InsertSupplier(supplier);

                    // Return Profile 
                    UserDTO user = new UserRepository.UserRepository(language).GetProfileByPhone(supplier.phoneNumber, lang, 2);
                    user.accessToken = GenerateAccessTokenByPhoneNumber(user.phoneNumber);
                    return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.INSERT_CORRECT), user);
                 

              
            }
            catch (InsertException InsertException)
            {
                return new Response(false, InsertException.RespMessage, InsertException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }

        }


        public Response LoginSupplier(GetCriteria getCriteria, string lang)
        {
            try
            {
                int userType = new UserRepository.UserRepository(language).GetUserUserTypeByPhone(getCriteria.phoneNumber);
                if (userType == (int)UserType.person)
                    return new Response(false, Messages.GetMessage(language, Messages.TypeM.USER, userM.PHONE_NOT_WORKSHOP));

                string correctHash = new UserRepository.UserRepository(language).GetPasswordByPhone(getCriteria.phoneNumber);

                if (correctHash == null || correctHash =="" )
                    return new Response(false, Messages.GetMessage(language , TypeM.DEFAULT,defaultM.PASS_WRONG));

                // hashing Password 
                if (PasswordHash.ValidatePassword(getCriteria.password, correctHash))
                {
                    // Return Profile 
                    UserDTO user = new UserRepository.UserRepository(language).GetProfileByPhone(getCriteria.phoneNumber, lang, 2);
                    user.accessToken = GenerateAccessTokenByPhoneNumber(user.phoneNumber);
                    return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.PASS_CORRECT), user);
                }

                else
                    return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.PASS_WRONG));
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(false, EmptyViewException.RespMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }

        }



        public Response UpdateUserImage(string  image ,string user_id )
        {
            try
            {
                // Store Image 
                int userType = new UserRepository.UserRepository(language).GetUserUserTypeByUserId(user_id);
                UserDTO userdto = new UserRepository.UserRepository(language).GetProfileByUserId(user_id ,language , userType);
                string phoneNumber = userdto.phoneNumber;

                string url = "";
                if (image != "")
                {
                    string ImageName = "images/UserImages/" +  phoneNumber + ".png";
                    url = new UploadRepository(language).StoreImage( image, ImageName);

                    if (url != "")
                    {
                         image = "images/UserImages/" + phoneNumber + ".png";
                    }
                    else
                    {
                         image = "";
                    }

                }

                if (image == "")
                {
                    return new Response(false, GetMessage(language, TypeM.USER, userM.PICTURE_NOT_UPDATED));
                }

                if (new UserRepository.UserRepository(language).UpdateUserImageUrl(image, Convert.ToInt32(user_id)))
                    return new Response(true, GetMessage(language, TypeM.USER, userM.PICTURE_UPDATED), userdto);
                else
                    throw new UpdateException(language);
            }
            catch (UpdateException UpdateException)
            {
                return new Response(false, UpdateException.RespMessage, UpdateException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }

        }



 
        



        // Delete Data
        public Response DeleteUserByPhoneNumber(string phoneNumber)
        {
            try
            {
                if (new UserRepository.UserRepository(language).DeleteUserByPhoneNumber(phoneNumber))
                    return new Response(true, GetMessage(language, TypeM.USER, userM.USER_DELETED));
                else
                    throw new DeleteException(language);
            }

            catch (DeleteException DeleteException)
            {
                return new Response(false, DeleteException.RespMessage, DeleteException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }
        }

        public Response ResetUserPhoneNumber(string phoneNumber)
        {
            try
            {
                if (new UserRepository.UserRepository(language).ResetUserPhoneNumber(phoneNumber))
                    return new Response(true, GetMessage(language, TypeM.DEFAULT, defaultM.PHONE_CHANGED));
                else
                    throw new UpdateException(language); 

            }

            catch (UpdateException UpdateException)
            {
                return new Response(false, UpdateException.RespMessage, UpdateException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }
        }


        public Response ChangeSupplierType(int user_id  , int  supplierType_id)
        {
            try
            {
                if (new UserRepository.UserRepository(language).ChangeSupplierType(user_id  , supplierType_id))
                    return new Response(true, GetMessage(language, TypeM.USER, userM.SUPPLIER_TYPE_CHANGED));
                else
                    throw new UpdateException(language);

            }

            catch (UpdateException UpdateException)
            {
                return new Response(false, UpdateException.RespMessage, UpdateException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }
        }

        public Response DeleteUserByUsername(string username)
        {

            try
            {
                if (new UserRepository.UserRepository(language).DeleteUserByUsername(username))
                    return new Response(true, GetMessage(language, TypeM.USER, userM.USER_DELETED));
                else
                  throw new DeleteException(language);
            }
            catch (DeleteException DeleteException)
            {
                return new Response(false, DeleteException.RespMessage, DeleteException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }

        


        }

        public Response CheckUserAvailability(Login.RegisterCriteria registerCriteria)
        {
            try
            {
                //check first time Availability of User Data 
                if (!new UserRepository.UserRepository(language).CheckUsernameAvailability(registerCriteria.username.ToLower()))
                {
                    if (!new UserRepository.UserRepository(language).CheckphoneNumberAvailability(registerCriteria.phoneNumber))
                        return new Response(true, GetMessage(language, TypeM.USER, userM.USER_PASS_ABLE));
                    else
                        return new Response(false, GetMessage(language, TypeM.DEFAULT, defaultM.PHONE_EXISTS));
                }
                else
                    return new Response(false, GetMessage(language, TypeM.DEFAULT, defaultM.PHONE_EXISTS));
            }

            catch (EmptyViewException EmptyViewException)
            {
                return new Response(false, EmptyViewException.RespMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }
          
        }


        public Response CheckPhoneAvailability(string phoneNumber)
        {
            if (new UserRepository.UserRepository(language).CheckphoneNumberAvailability(phoneNumber))
                return new Response(true, GetMessage(language, TypeM.DEFAULT, defaultM.PHONE_EXISTS));
            else
                return new Response(false, GetMessage(language, TypeM.DEFAULT, defaultM.PHONE_NOT_EXISTS));
        }

  
        //-----------------
        public string GenerateAccessToken(string username)
        {
            
                UserRepository.UserRepository user = new UserRepository.UserRepository(language);
                SetupRepository.SetupRepository setup = new SetupRepository.SetupRepository(language);


                // Check If Exist 
                string accessToken = user.GetAccessTokenByUsername(username);
                if (accessToken != "")
                {
                    return accessToken;
                }
                else
                {
                    accessToken = user.GenerateAccessToken(setup.GetAllSetups().accessTokenLength);
                    user.InsertAccessToken(accessToken, username);
                }


                return accessToken;
          
        }

        public string GenerateAccessTokenByPhoneNumber(string phoneNumber)
        {
            UserRepository.UserRepository user = new UserRepository.UserRepository(language);
            SetupRepository.SetupRepository setup = new SetupRepository.SetupRepository(language);


            // Check If Exist 
            string accessToken = user.GetAccessTokenByPhoneNumber(phoneNumber);
            if (accessToken != "")
            {
                return accessToken;
            }
            else
            {
                accessToken = user.GenerateAccessToken(setup.GetAllSetups().accessTokenLength);
                user.InsertAccessTokenByPhoneNumber(accessToken, phoneNumber);
            }


            return accessToken;
        }

        //----------------
        public string GenerateAccessToken()
        {
            UserRepository.UserRepository user = new UserRepository.UserRepository(language);
            SetupRepository.SetupRepository setup = new SetupRepository.SetupRepository(language);
            return user.GenerateAccessToken(setup.GetAllSetups().accessTokenLength);

        }

        public bool CheckAccessToken(string accessToken)
        {
            try
            {
                UserRepository.UserRepository userRepository = new UserRepository.UserRepository(language);
                return userRepository.CheckAccessToken(accessToken);
            }
            catch (Domain.Exceptions.EmptyViewException)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public string CheckAccessTokenUser(string accessToken)
        {
            try
            {
                UserRepository.UserRepository userRepository = new UserRepository.UserRepository(language);
                return userRepository.CheckAccessTokenUser(accessToken);
            }
            catch (Domain.Exceptions.EmptyViewException)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Response StoreUserImageUrl(string image_url, int user_id)
        {
            try
            {
                if (new UserRepository.UserRepository(language).UpdateUserImageUrl(image_url, user_id))

                    return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.INSERT_CORRECT));

                else
                    throw new InsertException(language);

            }
            catch (InsertException InsertException)
            {
                return new Response(false, InsertException.RespMessage, InsertException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }
        }

        public Response DeleteImage(string image_url)
        {
            try
            {
                if (new UserRepository.UserRepository(language).DeleteImageByUrl(image_url))

                    return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.INSERT_CORRECT));

                else
                    throw new DeleteException(language);

            }
            catch (DeleteException DeleteException)
            {
                return new Response(false, DeleteException.RespMessage, DeleteException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }
        }

       
 
 
        public Response GetProfileByPhone(string phoneNumber, int userType, string lang)
        {
            try
            {
                UserDTO user = new UserRepository.UserRepository(language).GetProfileByPhone(phoneNumber, lang, userType);
                return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, defaultM.DATAGOT), user);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(false, EmptyViewException.RespMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), ex.Message);
            }
        }


        public Response GetProfileByUserId(string user_id, int userType, string lang)
        {
            try
            {
                UserDTO user = new UserRepository.UserRepository(language).GetProfileByUserId(user_id, lang, userType);
                return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, defaultM.DATAGOT), user);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(false, EmptyViewException.RespMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), ex.Message);
            }
        }

        public Response GetUsersCount(GetCriteria getCriteria)
        {
            try
            {
               
              object usersCount= new UserRepository.UserRepository(language).GetUsersCount(getCriteria);
                return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.DATAGOT), usersCount);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(false, EmptyViewException.RespMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), ex.Message);
            }
        }


        public Response GetUsers(GetCriteria getCriteria)
        {
            try
            {

                List<List<string>> users = new UserRepository.UserRepository(language).GetUsers(getCriteria);
                return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.DATAGOT), users);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(false, EmptyViewException.RespMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), ex.Message);
            }
        }


        public Response GetRaingByUserId(Rating rating)
        {
            try
            {
                int user_id = new UserRepository.UserRepository(language).GetUserIdByAccessToken(rating.accessToken);
                double ratingRatio = new UserRepository.UserRepository(language).GetRaingByUserId(user_id);
                return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.DATAGOT), ratingRatio);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(false, EmptyViewException.RespMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), ex.Message);
            }
        }


        public Response GetUserPage(string accessToken)
        {
            try
            {
                int user_id = new UserRepository.UserRepository(language).GetUserIdByAccessToken(accessToken);
                UserPage userPage = new UserRepository.UserRepository(language).GetUserPage(user_id);
                return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, defaultM.DATAGOT), userPage);

            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(false, EmptyViewException.RespMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), ex.Message);
            }
        }


        public Response GetSupplierType(string lang)
        {
            try
            {
                List<SupplierType> workshopTypes = new UserRepository.UserRepository(language).GetSupplierType(lang);
                return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, defaultM.DATAGOT), workshopTypes);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(false, EmptyViewException.RespMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), ex.Message);
            }
        }

        public Response SetRatingToUser(Rating rating)
        {
            try
            {
                rating.raterId = new UserRepository.UserRepository(language).GetUserIdByAccessToken(rating.accessToken);
                rating.ratedId = new UserRepository.UserRepository(language).GetUserIdByWarshaId(rating.workshop_id);

                if (new UserRepository.UserRepository(language).SetRatingToUser(rating.raterId, rating.ratedId, rating.starNo))
                    return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.INSERT_CORRECT));
                else
                    throw new InsertException(language);

            }
            catch (InsertException InsertException)
            {
                return new Response(false, InsertException.RespMessage, InsertException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR),ex.Message);
            }
        }


        public Response Logout(string user_id, string device_id)
        {
            try
            {
                if (new UserRepository.UserRepository(language).DeleteDeviceId(user_id, device_id))
                    return new Response(true, Messages.GetMessage(language, Messages.TypeM.USER, Messages.userM.LOGOUT_CORRECT));
                else
                    throw new DeleteException(language);

            }
            catch (DeleteException DeleteException)
            {
                return new Response(false, DeleteException.RespMessage, DeleteException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), ex.Message);
            }
        }


        public Response LogoutAllSessions(string user_id)
        {
            try
            {



                if (new UserRepository.UserRepository(language).DeleteAllUserDevices(user_id))
                    return new Response(true, Messages.GetMessage(language, Messages.TypeM.USER, Messages.userM.LOGOUT_CORRECT));
                else
                    throw new DeleteException(language);

            }
            catch (DeleteException DeleteException)
            {
                return new Response(false, DeleteException.RespMessage, DeleteException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), ex.Message);
            }
        }



        public Response PushDeviceId(UserDevice userDevice, string user_id)
        {
            try
            {
                if (new UserRepository.UserRepository(language).UpdateDeviceId(Convert.ToInt32(user_id), userDevice.device_id, userDevice.phoneType))
                    return new Response(true, Messages.GetMessage(language, Messages.TypeM.USER, Messages.userM.DEVICEID_UPDATED));
                else
                    throw new UpdateException(language);

            }
            catch (UpdateException UpdateException)
            {
                return new Response(false, UpdateException.RespMessage, UpdateException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), ex.Message);
            }
        }

        public Response ResetPassword(string phonenumber ,string password )
        {
            try
            {


                if (new UserRepository.UserRepository(language).UpdatePasswordByPhone(phonenumber, PasswordHash.CreateHash(password)))
                {
                    // And Get The profile Data By PhoneNumber

                    return new Response(true, Messages.GetMessage(language, Messages.TypeM.SERVICE, Messages.serviceM.SERVICE_CHANGE_PASSWORD));
                }
                else
                    return new Response(true, Messages.GetMessage(language, Messages.TypeM.SERVICE, Messages.serviceM.SERVICE_CHANGE_PASSWORD));

            }
            catch (UpdateException UpdateException)
            {
                return new Response(false, UpdateException.RespMessage, UpdateException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), ex.Message);
            }
        }


       


    }
}
