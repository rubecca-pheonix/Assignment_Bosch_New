using System;
namespace Assignment_Bosch.Integration
{
	public class Roles
	{
		public Roles()
		{
			UserInfos = new HashSet<UserInfo>();
		}
		public int RoleId { get; set; }
		public required string RoleName { get; set; }
		public virtual ICollection<UserInfo> UserInfos { get; set; }
	}
}

