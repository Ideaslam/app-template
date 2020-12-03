
using Domain.Entities.supplier;
using Domain.Exceptions;
using Domain.Interfaces.Stat;
using Domain.Messages;
using HelperClass;
using Repository.StatRepository;
using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Messages.Messages;

namespace Service.StatService
{
  public  class StatService : IStatService 
    {

        string language = "en";
        public StatService(string language )
        {
            this.language = language;
        }

        public Response GetWorkshopBills(  string accessToken)
        {
            try
            {
                int workshop_id =  new UserRepository.UserRepository(language).GetSupplierIdByAccessToken(accessToken);
                List<Bill> wrokshopStats = new StatRepository(language).GetWorkshopBills(workshop_id);
                return new Response(true,  Messages.GetMessage(language,   TypeM.DEFAULT , defaultM.DATAGOT) , wrokshopStats);
            }
            catch (EmptyViewException  EmptyViewException)
            {
                return new Response(false,   EmptyViewException.RespMessage, EmptyViewException.ErrorMessage );
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR), ex.Message.ToString());
            }
        }



    }
}
