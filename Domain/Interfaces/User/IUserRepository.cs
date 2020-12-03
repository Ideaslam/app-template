using Domain.Entities;
using HelperClass;
using Login;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.User
{
  public  interface IUserRepository 
    {
      //  UserDb GetUserByUsername(string username);
      //  UserDb GetUserById(string user_id);
       // UserDb GetUserBySocialId(string socialId);
       // UserDb GetPhoneInfoByUsername(string username);
      //  UserDb GetPhoneInfoByPhoneNumber(string phoneNumber);
      //  UserDb GetUserByPhoneNumber(string phoneNumber);

      //  PersonDb GetPersonProfile(int user_id);
      //  WorkshopDb GetWarshaProfile(int user_id);

        int GetUserIdByAccessToken(string accessToken);
        int GetUserIdByUsername(string username);
        

        bool ComparePasswrod(string input_password , string db_password);
        bool CheckUsernameAvailability(string username);
        bool CheckphoneNumberAvailability(string phoneNumber);
        bool InsertRegistrationObject(RegisterCriteria registerCriteria);
        bool UpdateUserImageUrl(string image_url ,int user_id);
        bool DeleteImageByUrl(string image_url);
      //  bool UpdatePersonProfile(PersonDb person ,int  user_id);
     //   bool UpdateWarshaProfile(WorkshopDb workshop, int user_id);
        bool UpdatePasswordByPhone(string phoneNumber , string newPassword);
        bool UpdatePasswordByUsername(string username, string newPassword);
        bool CheckAccessToken(string AccessToken);
        bool InsertAccessToken(string accessToken, string username);

        string GenerateAccessToken(int length);
        double GetRaingByUserId(int user_id);
        bool SetRatingToUser(int RaterUser_id , int RatedUser_id , int starNo);


  

    }
}
