using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Domain.Entities;
using Domain.Entities.Order;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces.Order;
using Domain.Messages;
using HelperClass;
using Login;
using Repository.HelperRepository;
using Repository.ServicesRepository;
using Repository.UploadRepository;
using static Domain.Messages.Messages;

namespace Service.OrderService
{
    public class OrderService :  IOrderService
    {
        string language = "en"; 

        public OrderService(string language)
        {
            this.language = language;
        }


        public Response DeleteImage(   string image_url)
        {

            try
            {
                if (new OrderRepository.OrderRepository(language).DeleteImageByUrl(image_url))
                
                return new Response(true, Messages.GetMessage(language, TypeM.ACCIDENT, orderM.IMAGE_DELETED ));
                else
                    throw new DeleteException(language);
            }
            catch (DeleteException DeleteException)
            {
                return new Response(false, DeleteException.RespMessage, DeleteException.ErrorMessage );
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT,defaultM.UNEXPERROR) , ex.Message.ToString());
            }
        }

        public Response StoreOrderData( CarInfoOrder carInfoOrder  ,string user_id )
        {

            try
            {
               
                // Insert Order Data To DB 
                if (new OrderRepository.OrderRepository(language).InsertOrderData(carInfoOrder, user_id))
                {
                  
                    int order_id = new OrderRepository.OrderRepository(language).GetOrderIdByUserId(Convert.ToInt32( user_id));

                    // Insert order_details_assigns
                    foreach ( int   assign in carInfoOrder.orderDetailsIds)
                    {
                       new OrderRepository.OrderRepository(language).InsertOrderDetailAssign(order_id , assign );
                    }

                    //----------------------------



                    bool imageStatus = true;
                    Exception imageException =new Exception() ;



                    // Store Images 
                    int i = 0; 
                    foreach (string image in carInfoOrder.images)
                    {
                        string url = "";
                        i++;
                        string ImageName = "images/OrderImages/" + i + "_" + carInfoOrder.OrderIdentity + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".png";



                        url = new UploadRepository(language).StoreImage(image, ImageName);
                            
                            if(url != "")
                        {
                            try
                            {
                                new OrderRepository.OrderRepository(language).InsertOrderPictureUrl(ImageName, 1, order_id);
                            }
                            catch(InsertException ex )
                            {
                                imageStatus = false;
                                imageException  = ex ;
                            }
                        }
                           
                         
                        
                    }

                    //----------------


                   



                    // check If Image Was Inserted And Response with This Data 
                    if (imageStatus)
                        return new Response(imageStatus, Messages.GetMessage(language, TypeM.ACCIDENT, orderM.IMAGE_Order_STORE), order_id);
                    else
                        return new Response(imageStatus, Messages.GetMessage(language, TypeM.ACCIDENT, orderM.IMAGE_Order_ERROR), imageException, order_id);
                    ///-------------------------

                }
                else
                    throw new InsertException(language);


            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(false, EmptyViewException.RespMessage , EmptyViewException.ErrorMessage  );
            }
            catch (InsertException InsertException)
            {
                return new Response(false, InsertException.RespMessage, InsertException.ErrorMessage  );
            }
            catch (UpdateException UpdateException)
            {
                return new Response(false, UpdateException.RespMessage,   UpdateException.ErrorMessage );
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR)+ ex.Message.ToString(), ex.Message.ToString()+ "::: line NO:: "+ line);
            }

        }

        public Response UpdateAccidentData(CarInfoOrder carInfoOrder)
        {

            try
            {

                //Prepare date 
               

                if (new OrderRepository.OrderRepository(language).UpdateAccidentData(carInfoOrder))
                {

                    int accidentId = carInfoOrder.order_id;
                    bool imageStatus = true;

                    try
                    {
                        RequestCriteria requestCriteria = new RequestCriteria();
                        requestCriteria.accident_id = accidentId;
                        requestCriteria.city_id =(int) carInfoOrder.cityId;
                        new OfferService.OfferService(language).insertOrUpdatePermission(  requestCriteria);
                    }
                    catch (UpdateException  )
                    {
                        throw new UpdateException(language);
                    }



                    // Store Images 
                    int i = 0;
                    foreach (string image in carInfoOrder.images)
                    {
                        string url = "";
                        i++;
                        string ImageName = "images/OrderImages/" + i + "_" + accidentId + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".png";

                        url = new UploadRepository(language).StoreImage(image, ImageName);
                    }

                    if (imageStatus)
                        return new Response(imageStatus, Messages.GetMessage(language, TypeM.ACCIDENT, orderM.IMAGE_Order_ERROR));
                    else
                        return new Response(imageStatus, Messages.GetMessage(language, TypeM.ACCIDENT, orderM.IMAGE_Order_ERROR));

                }
                else
                    throw new InsertException(language);


            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(false, EmptyViewException.RespMessage,   EmptyViewException.ErrorMessage );
            }
            catch (InsertException InsertException)
            {
                return new Response(false, InsertException.RespMessage,  InsertException.ErrorMessage );
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR), ex.Message.ToString());
            }

        }


        public Response GetOrderLists(int orderType_id)
        {

            try
            {

                //Prepare date 
             
                List<InfoWindow> dropLists = new OrderRepository.OrderRepository(language).GetOrderListDetails(orderType_id);

            

                return new Response(true, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.DATAGOT), dropLists); 
                 
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true, EmptyViewException.RespMessage, EmptyViewException  ,new List<object>());
            }
            catch (InsertException InsertException)
            {
                return new Response(false, InsertException.RespMessage, InsertException , new List<object>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR), ex , new List<object>());
            }

        }

        public Response GetOrderDetails(string user_id, GetCriteria getCriteria)
        {
            try
            {

                // Get Order Data
                OrdersDetails orderDetails = new OrderRepository.OrderRepository(language).GetOrderDetails(Convert.ToInt32(user_id), getCriteria.order_id, getCriteria.lat, getCriteria.lang , getCriteria.offer_id);

                // Get Car Images 
                try
                {
                    List<string> carImageList = new VehicleRepository.VehicleRepository(language).GetCarImages(getCriteria.order_id, orderDetails.order.ORDERTYPE_ID);
                    orderDetails.media = carImageList;


                }
                catch (EmptyViewException)
                {
                    orderDetails.media = new List<string>();
                }
                catch (Exception ex)
                {
                    orderDetails.media = new List<string>();
                }

                return new Response(true, Messages.GetMessage(language, TypeM.ACCIDENT, orderM.Order_DATA_FOUND), orderDetails);

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



        public Response DeactiveRequest(  int accident_id)
        {
            try
            {
                if (new OrderRepository.OrderRepository(language).ResetReadyToFixFlag(accident_id))
                    return new Response(true, Messages.GetMessage(language , TypeM.ACCIDENT, orderM.REQUEST_DEACTIVE));
                else
                    throw new UpdateException(language);
            }
            catch (UpdateException UpdateException)
            {
                return new Response(false, UpdateException.RespMessage,  UpdateException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR), ex.Message.ToString());
            }
        }

        public Response DeleteRequest(int order_id)
        {
            try
            {
                bool ImgStatus = true; 
                List<string> imgUris = new OrderRepository.OrderRepository(language).GetOrderAllMedia(order_id);
                if (new OrderRepository.OrderRepository(language).DeleteRequestByOrderId(order_id))
                {
                    foreach (string imgUri in imgUris)
                    {
                        try
                        {
                            if(! new UploadRepository(language).DeleteImage(imgUri))
                            {
                                ImgStatus = false;
                            }
                           
                        }catch
                        {

                        }
                       
                    }

                    if (ImgStatus) 
                    return new Response(true, Messages.GetMessage(language, TypeM.ACCIDENT, orderM.REQUEST_DELETED));
                    else
                    return new Response(false, Messages.GetMessage(language, TypeM.ACCIDENT, orderM.REQUEST_NOT_DELETED));

                }
                    
                else
                    throw new DeleteException(language);
            }
            catch (DeleteException DeleteException)
            {
                return new Response(false, DeleteException.RespMessage);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(false, EmptyViewException.RespMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR), ex.Message.ToString());
            }
        }

        public Response GetAllRequests(string userId, int pageSize, int pageNumber)
        {
            try
            {
                int user = Convert.ToInt32(userId);
                List<UserRequestCriteria> requestCriterias = new OrderRepository.OrderRepository(language).GetAllRequests(user,   pageSize,   pageNumber);
                return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, defaultM.DATAGOT), requestCriterias);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true, EmptyViewException.RespMessage, EmptyViewException , new List<string>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), new List<string>());
            }
        }

        public Response GetPersonRequests(string user_id)
        {
            try
            {
                int userId = Convert.ToInt32(user_id);
                List<UserRequestCriteria> requestCriterias = new OrderRepository.OrderRepository(language).GetRequestsByUser_id(userId);
                return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, defaultM.DATAGOT), requestCriterias);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true, EmptyViewException.RespMessage, EmptyViewException , new List<string>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), new List<string>());
            }
        }


        public Response ActiveRequest( int accident_id)
        {
            try
            {
                if (new OrderRepository.OrderRepository(language).SetReadyToFixFlag(accident_id))
                    return new Response(true, Messages.GetMessage(language,   TypeM.ACCIDENT,  orderM.REQUEST_ACTIVE ));
                else
                    throw new UpdateException(language);
            }
            catch (UpdateException UpdateException)
            {
                return new Response(false, UpdateException.RespMessage, UpdateException.ErrorMessage );
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR), ex.Message.ToString());
            }
        }

        


        //public Response GetLastWorkshopLocation(string accessToken)
        //{
        //    try
        //    {
        //        int user_id = new UserRepository.UserRepository().GetUserIdByAccessToken(accessToken);
        //        WorkshopDb workshop = new AccidentRepository.AccidentRepository().GetLastWorkshopLocation(user_id);
        //        return new Response(true, Messages.DATAGOT_AR, Messages.DATAGOT_EN, workshop);
        //    }

        //    catch (EmptyViewException EmptyViewException)
        //    {
        //        return new Response(false, EmptyViewException.messageAr, EmptyViewException.messageEn, EmptyViewException.Message, new List<string>());
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response(false, Messages.UNEXPERROR_AR, Messages.UNEXPERROR_EN, ex.Message, new List<string>());
        //    }
        //}

        public Response GetDamagesType(  )
        {
            try
            {
                List<DamagesType> damagesTypes = new OrderRepository.OrderRepository(language).GetDamagesType();
                return new Response(true, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.DATAGOT), damagesTypes);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true, EmptyViewException.RespMessage, EmptyViewException  , new List<string>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR), ex.Message.ToString());
            }
        }

        public Response GetOrderType(string lang)
        {
            try
            {
                List<OrderType> orderTypes = new OrderRepository.OrderRepository(language).GetOrderType(lang);
                return new Response(true, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, defaultM.DATAGOT), orderTypes);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true, EmptyViewException.RespMessage, EmptyViewException , new List<string>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }
        }

    }
}
