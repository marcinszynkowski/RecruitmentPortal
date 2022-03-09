using Autofac;
using RP.RecruitmentModule.Interfaces;
using RP.RecruitmentModule.Services;

namespace RP.RecruitmentModule
{
    public class RecruitmentModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProcessStatusService>().As<IProcessStatusService>().InstancePerRequest();
            builder.RegisterType<RecruitmentProcessService>().As<IRecruitmentProcessService>().InstancePerRequest();
            builder.RegisterType<SurveyAnswerService>().As<ISurveyAnswerService>().InstancePerRequest();
            builder.RegisterType<SurveyQuestionService>().As<ISurveyQuestionService>().InstancePerRequest();
        }
    }
}