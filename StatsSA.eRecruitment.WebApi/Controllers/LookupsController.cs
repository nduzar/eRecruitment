using StatsSA.eRecruitment.IProvider.Genders;
using StatsSA.eRecruitment.IProvider.Races;
using StatsSA.eRecruitment.IProvider.Languages;
using StatsSA.eRecruitment.IProvider.LanguageProficiencies;
using StatsSA.eRecruitment.IProvider.QualificationTypes;
using StatsSA.eRecruitment.IProvider.ContactMethods;
using StatsSA.eRecruitment.WebApi.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;

namespace StatsSA.eRecruitment.WebApi.Controllers
{
    [RoutePrefix("api/lookups")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LookupsController : ApiController
    {
        private IRaceProvider _raceProvider;
        private IGenderProvider _genderProvider;
        private IContactMethodProvider _contactMethodProvider;
        private ILanguageProvider _languageProvicer;
        private ILanguageProficiencyProvider _languageProficiencyProvider;
        private IQualificationTypeProvider _qualificationTypeProvider;

        public LookupsController(IRaceProvider raceProvider, 
                                    IGenderProvider genderProvider, 
                                    IContactMethodProvider contactMethodProvider,
                                    ILanguageProvider languageProvicer,
                                    ILanguageProficiencyProvider languageProficiencyProvider,
                                    IQualificationTypeProvider qualificationTypeProvider)
        {
            _raceProvider = raceProvider;
            _genderProvider = genderProvider;
            _contactMethodProvider = contactMethodProvider;
            _languageProvicer = languageProvicer;
            _languageProficiencyProvider = languageProficiencyProvider;
            _qualificationTypeProvider = qualificationTypeProvider;
        }

        [HttpPost, Route("getraces")]
        public IHttpActionResult GetRaces()
        {
            var races = _raceProvider.GetAllRaces();
            return Ok(races);
        }
        [HttpPost, Route("getgenders")]
        public IHttpActionResult GetGenders()
        {
            var genders = _genderProvider.GetAllGenders();
            return Ok(genders);
        }
        [HttpPost, Route("getlanguages")]
        public IHttpActionResult GetLanguages()
        {
            var languages = _languageProvicer.GetAllLanguages();
            return Ok(languages);
        }
        [HttpPost, Route("getlanguageproficiencies")]
        public IHttpActionResult GetLanguageProficiencies()
        {
            var languageProficiencies = _languageProficiencyProvider.GetLanguageProficiencies();
            return Ok(languageProficiencies);
        }
        [HttpPost, Route("getcontactmetods")]
        public IHttpActionResult GetContactMethods()
        {
            var contactMethods = _contactMethodProvider.GetContactMethods();
            return Ok(contactMethods);
        }
        [HttpPost, Route("getqualificationtypes")]
        public IHttpActionResult GetQualificationTypes()
        {
            var qualificationTypes = _qualificationTypeProvider.GetAllQaulificationTypes();
            return Ok(qualificationTypes);
        }
    }
}
