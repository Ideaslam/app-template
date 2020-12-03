using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Domain.Entities.Vechiles;
using Domain.Exceptions;
using Domain.Messages;
using HelperClass;
using UserRepository;
using static Domain.Messages.Messages;

namespace Service.VehicleService
{
    public class VehicleService : Domain.Interfaces.Vehicle.IVehicleService
    {
        //public Response GetCarInformations(string accessToken)
        //{
        //    try
        //    {
        //        int user_id = new UserRepository.UserRepository().GetUserIdByAccessToken(accessToken);
        //        List<VehicleDTO> vehicles = new VehicleRepository.VehicleRepository().GetCarBasicAndAccident(user_id);

        //        return new Response(true, Messages.DATAGOT_AR, Messages.DATAGOT_EN, vehicles);
        //    }
        //    catch (EmptyViewException EmptyViewException)
        //    {
        //        return new Response(false, EmptyViewException.messageAr, EmptyViewException.messageEn, EmptyViewException.Error_Message, new List<string>());
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response(false, Messages.UNEXPERROR_AR, Messages.UNEXPERROR_EN, ex.Message, new List<string>());
        //    }
        //}


        string language = "en"; 
        public VehicleService(string language)
        {
            this.language = language;
        }


        public Response GetMyVehicles( string user_id , string lang)
        {
            try
            {
                List<VehicleDTO> vehicles = new VehicleRepository.VehicleRepository(language).GetMyVechiles( user_id , lang);
                return new Response(true, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.DATAGOT), vehicles);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true,    EmptyViewException.RespMessage, EmptyViewException , new List<string>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language , TypeM.DEFAULT , defaultM.UNEXPERROR) , ex , new List<string>());
            }
        }


        public Response GetBrands(string lang)
        {
            try
            {
            
                List<Brand> brands = new VehicleRepository.VehicleRepository(language).GetBrands(lang);
                return new Response(true, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.DATAGOT), brands);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true,   EmptyViewException.RespMessage, EmptyViewException, new List<string>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR) , ex , new List<string>());
            }
        }

        public Response GetModels(string lang ,int brandId)
        {
            try
            {

               
                List<Model> models = new VehicleRepository.VehicleRepository(language).GetModels(lang ,brandId);

                return new Response(true, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.DATAGOT), models);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true,   EmptyViewException.RespMessage, EmptyViewException, new List<string>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR), ex  , new List<string>());
            }
        }


        public Response GetColors(string lang )
        {
            try
            {

                List<Color> colors = new VehicleRepository.VehicleRepository(language).GetColors(lang );
                return new Response(true, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.DATAGOT), colors);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true,  EmptyViewException.RespMessage, EmptyViewException , new List<string>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR), ex , new List<string>());
            }
        }


        public Response GetFixPaper(string accessToken)
        {
            try
            {
                int user_id = new UserRepository.UserRepository(language).GetUserIdByAccessToken(accessToken);
                List<FixPaperDTO> fixPapers = new VehicleRepository.VehicleRepository(language).GetFixPaper(user_id);


                return new Response(true, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.DATAGOT), fixPapers);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true,  EmptyViewException.RespMessage, EmptyViewException , new List<string>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR) , ex , new List<string>());
            }
        }

        public Response GetWorkshopCars(string accessToken)
        {
            try
            {
                int workshop_id = new UserRepository.UserRepository(language).GetSupplierIdByAccessToken(accessToken);
                List<CarShort> carShorts = new VehicleRepository.VehicleRepository(language).GetWorkshopCars(workshop_id);

                return new Response(true, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.DATAGOT), carShorts);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true, EmptyViewException.RespMessage, EmptyViewException , new List<string>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR), ex , new List<string>());
            }
        }



        public Response StoreFixPaper(FixPaper fixPaper)
        {
            try
            {
                if (new VehicleRepository.VehicleRepository(language).InsertFixPaper(fixPaper))
                    return new Response(true, Messages.GetMessage(language, TypeM.VEHICLE, vehicleM.FIXPAPER_STORE));
                else
                    throw new InsertException(language);

            }
            catch (InsertException InsertException)
            {
                return new Response(false,   InsertException.RespMessage, InsertException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR), ex.Message);
            }
        }
        public Response CheckFixPaperByPlateNumber(string plateNumber)
        {
            try
            {
                if (new VehicleRepository.VehicleRepository(language).CheckFixPaperByPlateNumber(plateNumber))
                    return new Response(true, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.DATAGOT));
                else
                    throw new EmptyViewException(language);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(false,   EmptyViewException.RespMessage, EmptyViewException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR), ex.Message);
            }
        }
        public Response CheckFixPaperByAccidentId(int accident_id)
        {
            try
            {
                if (new VehicleRepository.VehicleRepository(language).CheckFixPaperByAccidentId(accident_id))
                    return new Response(true, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.DATAGOT));
                else
                    throw new EmptyViewException(language);

            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(false,    EmptyViewException.RespMessage , EmptyViewException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR) , ex.Message);
            }
        }
        public Response StoreVehicleData(Vehicle vehicle ,string user_id )
        {
            try
            {
                if (new VehicleRepository.VehicleRepository(language).InsertVehicleData(vehicle , user_id))
                    return new Response(true, Messages.GetMessage(language, TypeM.VEHICLE, vehicleM.VEHICLE_STORE ));

                else
                    throw new InsertException(language);

            }
            catch (InsertException InsertException)
            {
                return new Response(false,   InsertException.RespMessage, InsertException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR), ex.Message);
            }

        }
    }
}
