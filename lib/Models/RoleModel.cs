using System.Drawing;
using System.Windows.Forms;

namespace Cloudsdale.lib.Models
{
    class RoleModel
    {
        public string Role_Text(string UserID)
        {
            return UserModel.Status(UserID);
        }
        public Color Role_Color(string role)
        {
            switch (role)
            {
                case "founder":
                    return Assets.FounderTag;
                    break;
                case "developer":
                    return Assets.DevTag;
                    break;
                case "admin":
                    return Assets.AdminTag;
                    break;
                case "associate":
                    return Assets.AssociateTag;
                    break;
                case "donator":
                    return Assets.DonatorTag;
                    break;
                case "legacy":
                    return Assets.LegacyTag;
                    break;
                case "verified":
                    return Assets.VerifiedTag;
                    break;
                default:
                    return Color.Transparent;
                    break;
            }
        }
        public void CreateRole(Label roleText, string UserID)
        {
            roleText.Text = Role_Text(UserID);
            roleText.BackColor = Role_Color(UserID);
            if (Role_Text(UserID) != "normal")
            {
                roleText.BorderStyle = BorderStyle.FixedSingle;
            }else
            {
                roleText.BorderStyle = BorderStyle.None;
            }
        }
    }
}
