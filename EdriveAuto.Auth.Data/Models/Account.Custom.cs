
using EDriveAuto.Auth.Data.Models.Interfaces;

namespace EDriveAuto.Auth.Data.Models
{
    public partial class Account : BaseModel, IAccount
    {
	    public override string ToString()
        {
            return UniqueKey.ToString();
        }
    }
}
