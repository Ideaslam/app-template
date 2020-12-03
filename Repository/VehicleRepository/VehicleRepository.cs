using System;
using System.Collections.Generic;
using System.Text;
using DbQueries;
using Domain.Entities;
using Domain.Entities.Vechiles;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Messages;
using static Domain.Messages.Messages;

namespace VehicleRepository
{
    public class VehicleRepository : Domain.Interfaces.Vehicle.IVehicleRepository
    {
        Common conn_db = new Common();

        string language = "en"; 

        public VehicleRepository (string language)
        {
            this.language = language; 

        }


        public List<string> GetCarImages(int order_id ,int ordertype)
        {
            VehicleQuery vehicleQuery = new VehicleQuery(language);
            string modelImage = "";

          

            System.Data.DataTable dataTable = conn_db.ReadTable(vehicleQuery.GetObjectByColname("car_images_v", "order_id", order_id));
            if (dataTable.Rows.Count == 0)
            {
                if (ordertype == 6)
                {
                    modelImage = new Enums().APP_DIRECTORY + "images/OrderImages/satha.png";
                }
                else
                {
                    throw new EmptyViewException(language, Messages.GetMessage(language, TypeM.VEHICLE, vehicleM.IMAGE_NOT_FOUND));
                }
            }
            
                 
            List<string> listCarImages = new List<string>();
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                listCarImages.Add(row["Pic_url"].ToString());
            }

            if(listCarImages.Count == 0)
            {
                listCarImages.Add(modelImage);
            }
            return listCarImages;

        }

        public List<CarShort> GetWorkshopCars( int workshop_id)
        {
            VehicleQuery vehicleQuery = new VehicleQuery(language);

            System.Data.DataTable dataTable = conn_db.ReadTable(vehicleQuery.GetWorkshopCars(workshop_id));
            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language , Messages.GetMessage(language, TypeM.VEHICLE, vehicleM.VEHICLE_DATA_NOT_GOT));

            List<CarShort> listCars = new List<CarShort>();
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                 
                CarShort carShort = new CarShort();
                carShort.plateNumber = row["PLATENUMBER"].ToString() ;
                carShort.price =Convert.ToDouble( row["Price"].ToString());
                carShort.accident_id =Convert.ToInt32( row["ACCIDENT_ID"].ToString());
                listCars.Add(carShort);
            }
            return listCars;

        }


        public List<Brand> GetBrands(string lang)
        {
            VehicleQuery vehicleQuery = new VehicleQuery(language);

            System.Data.DataTable dataTable = conn_db.ReadTable(vehicleQuery.GetObjectByColname("car_brand","isactive",1));

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            Brand brand;
            List<Brand> Brands = new List<Brand>();
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                brand = new Brand();
                brand.brand_id = Convert.ToInt32(row["id"].ToString());
                if (lang == "ar")
                    brand.brandName = row["BRANDNAME_AR"].ToString();
                else
                    brand.brandName = row["BRANDNAME_EN"].ToString();
                

                brand.Logo = row["LOGO"].ToString();

                Brands.Add(brand);
            }
            return Brands;


        }



        public List<Model> GetModels(string lang ,int brandId)
        {
            VehicleQuery vehicleQuery = new VehicleQuery(language);

            System.Data.DataTable dataTable = conn_db.ReadTable(vehicleQuery.GetModelsByBrandId(brandId));

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            Model model;
            List<Model> models = new List<Model>();
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                model = new Model();
                model.model_id = Convert.ToInt32(row["id"].ToString());
                if (lang == "ar")
                    model.modelName = row["MODELNAME_AR"].ToString();
                else
                    model.modelName = row["MODELNAME_EN"].ToString();


                model.brand_id = row["BRAND_ID"] is DBNull ? 0 :  Convert.ToInt32( row["BRAND_ID"].ToString());

                models.Add(model);
            }
            return models;


        }

        public List<Color> GetColors(string lang )
        {
            VehicleQuery vehicleQuery = new VehicleQuery(language);

            System.Data.DataTable dataTable = conn_db.ReadTable(vehicleQuery.GetMasterTranslated("color",lang));

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            Color color;
            List<Color> models = new List<Color>();
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                color = new Color();
                color.color_id = Convert.ToInt32(row["id"].ToString());
                 color.colorName  = row["color_Name"].ToString();
               

                models.Add(color);
            }
            return models;


        }



        //public List<VehicleDTO> GetCarBasicAndAccident(int user_id)
        //{
        //    VehicleQuery vehicleQuery = new VehicleQuery();

        //    System.Data.DataTable dataTable = conn_db.ReadTable(vehicleQuery.GetCarBasicInformation(user_id));

        //    if (dataTable.Rows.Count == 0)
        //        throw new EmptyViewException();

        //    VehicleDTO vechileDTO;
        //    List<VehicleDTO> listVehicles = new List<VehicleDTO>();
        //    foreach (System.Data.DataRow row in dataTable.Rows)
        //    {
        //        vechileDTO = new VehicleDTO();
        //        vechileDTO.carId = Convert.ToInt32(row["id"].ToString());
        //        vechileDTO.plateNumber = row["PLATENUMBER"].ToString();

        //        vechileDTO.manufacturer = row["MANUFACTURER"].ToString();
        //        vechileDTO.model = row["MODEL"].ToString();
        //        vechileDTO.color = row["COLOR"].ToString();
        //        vechileDTO.readyToFix = Convert.ToInt32(row["READYTOFIX"].ToString());
        //        vechileDTO.foundDate = row["FOUNDDATE"].ToString();
        //        try
        //        {
        //            vechileDTO.accidentDate = Convert.ToDateTime(row["ACCIDENTDATE"].ToString()).ToString("dd-MM-yyyy h:mm tt");
        //        }
        //        catch (Exception ex)
        //        {
        //            vechileDTO.accidentDate = "";
        //        }
        //        //vechileDTO.registrationType =Convert.ToInt32( row["REGISTRATIONTYPE_ID"].ToString());
        //        vechileDTO.fixPaperId = Convert.ToInt32(row["paper_id"].ToString());
        //        vechileDTO.fixPaperNO = row["Paper_no"].ToString();
        //        vechileDTO.fixPaperStatus = Convert.ToInt32(row["paperStatus"].ToString());
        //        vechileDTO.accidentId = Convert.ToInt32(row["ACCIDENTID"].ToString());

        //        vechileDTO.STATUS = Convert.ToInt32(row["STATUS"].ToString());
        //        vechileDTO.STATUS_NAME_AR =  row["STATUS_NAME_AR"].ToString() ;
        //        vechileDTO.STATUS_NAME_EN =  row["STATUS_NAME_EN"].ToString() ;
        //        vechileDTO.SHOPNAME =  row["SHOPNAME"].ToString() ;
        //        vechileDTO.PRICE = Convert.ToDouble(row["PRICE"].ToString());
        //        vechileDTO.WORKDAYS = Convert.ToInt32(row["WORKDAYS"].ToString());

        //        listVehicles.Add(vechileDTO);
        //    }
        //    return listVehicles;


        //}


        public List<VehicleDTO> GetMyVechiles( string user_id , string lang)
        {
            VehicleQuery vehicleQuery = new VehicleQuery(language);

            System.Data.DataTable dataTable = conn_db.ReadTable(vehicleQuery.GetVehiclesByUserId(user_id));

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            VehicleDTO  vehicle;
            List<VehicleDTO> listVehicles = new List<VehicleDTO>();
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                vehicle = new VehicleDTO();
                vehicle.vechile_id =Convert.ToInt32( row["id"].ToString());
                vehicle.BrandLogo =  row["LOGO"].ToString() ;
                if(lang == Domain.Messages.Messages.language.ar.ToString())
                vehicle.modelName = row["MODELNAME_AR"].ToString();
                else
                vehicle.modelName = row["MODELNAME_EN"].ToString();


                if (lang == Domain.Messages.Messages.language.ar.ToString())
                    vehicle.brandName = row["brandname_ar"].ToString();
                else
                    vehicle.brandName = row["brandname_en"].ToString();

                vehicle.year = row["FOUNDDATE"].ToString();

                vehicle.color = row["COLOR_NAME"].ToString();




                listVehicles.Add(vehicle);
            }
            return listVehicles;


        }

        public List<FixPaperDTO> GetFixPaper(int user_id)
        {
            VehicleQuery vehicleQuery = new VehicleQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(vehicleQuery.GetObjectByColname("fixPaper_v", "user_id", user_id));
            FixPaperDTO fixPaper = new FixPaperDTO();
            List<FixPaperDTO> listPapers = new List<FixPaperDTO>();

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                fixPaper = new FixPaperDTO();
                fixPaper.id = Convert.ToInt32(row["ID"].ToString());
                fixPaper.paper_id = row["PAPER_ID"].ToString();
                fixPaper.issueDate = Convert.ToDateTime(row["ISSUEDATE"].ToString()).ToString("dd-MM-yyyy");
                fixPaper.expiryDate = Convert.ToDateTime(row["EXPIRYDATE"].ToString()).ToString("dd-MM-yyyy");
                fixPaper.status = Convert.ToInt32(row["status"].ToString());
                fixPaper.car_plateNumber = row["CAR_PLATENUMBER"].ToString();
              
                listPapers.Add(fixPaper);
            }


            return listPapers;
        }

        public bool InsertFixPaper(FixPaper fixPaper)
        {
            VehicleQuery vehicleQuery = new VehicleQuery(language);
            return vehicleQuery.InsertFixPaper(fixPaper, new UserRepository.UserRepository(language).GetUserIdByAccessToken(fixPaper.accessToken));

        }

        public bool CheckFixPaperByPlateNumber(string plateNumber)
        {
            VehicleQuery vehicleQuery = new VehicleQuery(language);

            return conn_db.ReadTable(vehicleQuery.CheckFixPaperByPlateNumber(plateNumber)).Rows.Count > 0 ? true : false;

        }


        public bool CheckFixPaperByAccidentId(int accident_id)
        {
            VehicleQuery vehicleQuery = new VehicleQuery(language);

            return conn_db.ReadTable(vehicleQuery.CheckFixPaperByAccidentId(accident_id)).Rows.Count > 0 ? true : false;

        }


        public bool InsertVehicleData(Vehicle vehicle ,string user_id )
        {
            VehicleQuery vehicleQuery = new VehicleQuery(language);

            return vehicleQuery.InsertVehicleData(vehicle , user_id);


        }
    }
}
