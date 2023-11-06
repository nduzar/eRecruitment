using StatsSA.eRecruitment.IManager.UnitOfWork;
using StatsSA.eRecruitment.Manager;
using StatsSA.eRecruitment.Manager.UnitOfWork;
using StatsSA.SystemArchitecture.Resolver;
using System.ComponentModel.Composition;

namespace StatsSA.AuthServer.Manager.DIResolver
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IUnitOfWork, UnitOfWork>();
        }
    }
}
