using StatsSA.eRecruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsSA.eRecruitment.IProvider.VerApplicantProhibitions
{
    public interface IVerApplicantProhibitionProvider
    {
        VerApplicantPubServProhibition GetVerApplicantProhibition(int VerApplicantProfileId);

    }
}
