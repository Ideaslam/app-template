using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.HelperClass;
using Domain.Entities.Login;
using HelperClass;



namespace Domain.Interfaces.User
{
   public interface IUserService 
    {
      //  Response Login(Login.Login login);
      //  Task<Response> Register(Login.RegisterCriteria registerCriteria);
        //Response SaveImage(string image, string imageType, string imageName, int user_id);
        Response CheckUserAvailability(Login.RegisterCriteria registerCriteria);

        Response DeleteUserByPhoneNumber(string phoneNumber);
        Response DeleteUserByUsername(string username);

        Response StoreUserImageUrl(string image_url , int user_id);
        Response DeleteImage(string image_url);

      //  Response EditProfile(UserR userDTO);
      //  Response EditPersonProfile(PersonDb person);
      //  Response EditWarshaProfile(WorkshopDb workshop);

      //  Response GetPersonProfile(string accessToken);
      //  Response GetProfile(string accessToken);
      //  Response GetProfileBySocialId(string socialId);
      //  Response GetProfileByPhone(string phoneNumber);
     //   Response GetWarshaProfile(string accessToken);


         
      //  Response ResetPassword(string username, string newPassword ,bool isMobile);
        //Response UpdatePasswordByPhone(string phoneNumer, string newPassword);
        //Response UpdatePasswordByUsername(string username, string newPassword);

        string GenerateAccessToken(string username);
        bool CheckAccessToken(string accessToken);


        Response GetRaingByUserId(Rating rating);
        Response SetRatingToUser(Rating rating);
    }
}
