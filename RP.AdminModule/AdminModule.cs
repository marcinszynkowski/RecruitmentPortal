using Autofac;
using RP.AdminModule.Interfaces;
using RP.AdminModule.Services;

namespace RP.AdminModule
{
    public class AdminModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CityService>().As<ICityService>().InstancePerRequest();
            builder.RegisterType<CompanyService>().As<ICompanyService>().InstancePerRequest();
            builder.RegisterType<CourseKindService>().As<ICourseKindService>().InstancePerRequest();
            builder.RegisterType<CourseTypeService>().As<ICourseTypeService>().InstancePerRequest();
            builder.RegisterType<DrivingLicenseService>().As<IDrivingLicenseService>().InstancePerRequest();
            builder.RegisterType<ForeignLanguageLevelService>().As<IForeignLanguageLevelService>().InstancePerRequest();
            builder.RegisterType<ForeignLanguageService>().As<IForeignLanguageService>().InstancePerRequest();
            builder.RegisterType<FormOfEmploymentService>().As<IFormOfEmploymentService>().InstancePerRequest();
            builder.RegisterType<PaymentTypeService>().As<IPaymentTypeService>().InstancePerRequest();
            builder.RegisterType<PositionService>().As<IPositionService>().InstancePerRequest();
            builder.RegisterType<SurveyQuestionTypeService>().As<ISurveyQuestionTypeService>().InstancePerRequest();
            builder.RegisterType<XmlService>().As<IXmlService>().InstancePerRequest();
        }
    }
}