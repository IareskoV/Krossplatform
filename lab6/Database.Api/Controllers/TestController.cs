using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Database.Api.Db;
using Database.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Database.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private AbstractContext dbContext;
        public TestController(AbstractContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet("gettext")]
        [Authorize]
        public string GetText()
        {
            return "amazing";
        }
        [HttpGet("init")]
        public void Initialize()
        {
            dbContext.Database.EnsureCreated();
        }

        [HttpGet("fill")]
        public void Fill()
        {

            Ref_Asset_Categories engRefAssetCategory = new Ref_Asset_Categories
            {
                Asset_Category_Code = "code_eng",
                Asset_Category_Description = "english description",
                Domestic = Ref_Asset_Categories.DomesticEnum.Domestic
            };
            Ref_Asset_Categories rusRefAssetCategory = new Ref_Asset_Categories
            {
                Asset_Category_Code = "code_rus",
                Asset_Category_Description = "описание",
                Domestic = Ref_Asset_Categories.DomesticEnum.Domestic
            };

            Ref_Asset_Supertypes engRefAssetSupertype = new Ref_Asset_Supertypes
            {
                Ref_Asset_Categories = engRefAssetCategory,
                Asset_Supertype_Code = "eng_super_code",
                Cutlery = Ref_Asset_Supertypes.CutleryEnum.Cutlery,
                Asset_Supertype_Description = "eng_supertype_description"
            };
            Ref_Asset_Supertypes rusRefAssetSupertype = new Ref_Asset_Supertypes
            {
                Ref_Asset_Categories = rusRefAssetCategory,
                Asset_Supertype_Code = "рус_super_code",
                Cutlery = Ref_Asset_Supertypes.CutleryEnum.Cutlery,
                Asset_Supertype_Description = "описание"
            };

            Ref_Asset_Types engRefAssetTypes = new Ref_Asset_Types
            {
                Asset_Type_Code = "eng_type_code",
                Ref_Asset_Supertypes = engRefAssetSupertype,
                Asset_Type_Description = "eng_desc",
                Spoon = Ref_Asset_Types.SpoonEnum.Spoon
            };
            Ref_Asset_Types rusRefAssetTypes = new Ref_Asset_Types
            {
                Asset_Type_Code = "рус_код",
                Ref_Asset_Supertypes = rusRefAssetSupertype,
                Asset_Type_Description = "описание",
                Spoon = Ref_Asset_Types.SpoonEnum.Spoon
            };

            Ref_Sizes engSize = new Ref_Sizes
            {
                Size_Code = "eng_code",
                Size = Ref_Sizes.Sizes.Medium,
                Size_Description = "eng_description"
            };
            Ref_Sizes rusSize = new Ref_Sizes
            {
                Size_Code = "рус_code",
                Size = Ref_Sizes.Sizes.Medium,
                Size_Description = "описание"
            };

            Asset engAsset = new Asset
            {
               // Asset_ID = 1,
                Asset_Name = "asset_eng",
                Ref_Asset_Types = engRefAssetTypes,
                Ref_Sizes = engSize,
                Other_Details = "details_eng"
            };
            Asset rusAsset = new Asset
            {
                //Asset_ID = 2,
                Asset_Name = "asset_рус",
                Ref_Asset_Types = rusRefAssetTypes,
                Ref_Sizes = rusSize,
                Other_Details = "details_рус"
            };

            Life_Cycle_Phases engPhases = new Life_Cycle_Phases
            {
                Life_Cycle_Code = "eng_code",
                Life_Cycle_Name = "eng_name",
                Life_Cycle_Description = "description"
            };
            Life_Cycle_Phases rusPhases = new Life_Cycle_Phases
            {
                Life_Cycle_Code = "рус_код",
                Life_Cycle_Name = "рус_имя",
                Life_Cycle_Description = "описание"
            };

            Location engLocation = new Location
            {
                //Location_ID = 1,
                Location_Details = "eng_details"
            };
            Location rusLocation = new Location
            {
                //Location_ID = 2,
                Location_Details = "детали"
            };

            Ref_Status engStatus = new Ref_Status
            {
                Status_Code = "eng_code",
                Status_Description = "description"
            };
            Ref_Status rusStatus = new Ref_Status
            {
                Status_Code = "рус_код",
                Status_Description = "описание"
            };

            Responsible_Party engResponsibleParty = new Responsible_Party
            {
                //Party_ID = 1,
                Party_Details = "eng_details"
            };
            Responsible_Party rusResponsibleParty = new Responsible_Party
            {
                //Party_ID = 2,
                Party_Details = "рус_детали"
            };
            Assets_Life_Cycle_Events engEvent = new Assets_Life_Cycle_Events
            {
                Asset = engAsset,
                Life_Cycle_Phases = engPhases,
                Location = engLocation,
                Responsible_Party = engResponsibleParty,
                Ref_Status = engStatus,
                Date_From = DateTimeOffset.Now,
                Date_To = DateTimeOffset.Now.AddHours(5)
            };
            Assets_Life_Cycle_Events rusEvent = new Assets_Life_Cycle_Events
            {
                Asset = rusAsset,
                Life_Cycle_Phases = rusPhases,
                Location = rusLocation,
                Responsible_Party = rusResponsibleParty,
                Ref_Status = rusStatus,
                Date_From = DateTimeOffset.Now,
                Date_To = DateTimeOffset.Now.AddHours(5)
            };

            dbContext.Ref_Asset_Categories.AddRange(new List<Ref_Asset_Categories>{engRefAssetCategory, rusRefAssetCategory});
            dbContext.Ref_Asset_Supertypes.AddRange(new List<Ref_Asset_Supertypes>{engRefAssetSupertype, rusRefAssetSupertype});
            dbContext.Ref_Asset_Types.AddRange(new List<Ref_Asset_Types> {rusRefAssetTypes, engRefAssetTypes});
            dbContext.Ref_Sizes.AddRange(new List<Ref_Sizes>{engSize, rusSize});
            dbContext.Assets.AddRange(new List<Asset> { engAsset, rusAsset });
            dbContext.Life_Cycle_Phases.AddRange(new List<Life_Cycle_Phases>{engPhases, rusPhases});
            dbContext.Locations.AddRange(new List<Location> {engLocation, rusLocation});
            dbContext.Responsible_Party.AddRange(new List<Responsible_Party>{engResponsibleParty, rusResponsibleParty});
            dbContext.Ref_Status.AddRange(new List<Ref_Status> {engStatus, rusStatus});
            dbContext.Assets_Life_Cycle_Events.AddRange(new List<Assets_Life_Cycle_Events>{engEvent, rusEvent});

            dbContext.SaveChanges();
        }

        [HttpGet("assets_events")]
        public List<Assets_Life_Cycle_Events> GetCycleEvents()
        {
            dbContext.Locations.Load();
            dbContext.Ref_Asset_Types.Load();
            dbContext.Ref_Sizes.Load();
            dbContext.Ref_Asset_Categories.Load();
            dbContext.Life_Cycle_Phases.Load();
            dbContext.Assets.Load();
            dbContext.Responsible_Party.Load();
            dbContext.Ref_Status.Load();
            return dbContext.Assets_Life_Cycle_Events.ToList();
        }

        [HttpGet("deleteDb")]
        public void DeleteDb()
        {
            dbContext.DeleteDb();
        }

    }
}
