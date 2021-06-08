using BlazorEasyAuth.Models;

namespace BlazorEasyAuth.Example.Models
{
	/// <summary>
	/// This is an example user class. Normally, you'd store this user in a database.
	/// When storing in an EF database, it is recommended to store the roles as a JSON string,
	/// see "Entity Framework JSON Roles column example" below.
	/// Id column is purposefully left out of the IUser interface to allow for easily switching out the Id column type.
	/// GetId() simply needs to return the Id as a unique string value. In this case, we .ToString() the integer Id.
	/// Same can be done for Guid, or any other type.
	/// </summary>
	public class User : IUser
	{
		public int Id { get; set; }

		public string DisplayName { get; set; }
		
		public string Username { get; set; }
		
		public byte[] PasswordHash { get; set; }
		
		public string[] Roles { get; set; }
		
		public string GetId() => Id.ToString();
		
		// Entity Framework JSON Roles column example
		// [NotMapped]
		// public string[] Roles { get; set; } = new string[0];
		//
		// [Column(nameof(Roles))]
		// [EditorBrowsable(EditorBrowsableState.Never)]
		// public string RolesInternal
		// {
		// 	get => JsonConvert.SerializeObject(Roles ?? new string[0]);
		// 	set => Roles = JsonConvert.DeserializeObject<string[]>(value) ?? new string[0];
		// }
	}
}