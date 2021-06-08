# BlazorEasyAuth

## Setup

1. Set up your user repository/database. Create a user type that implements `BlazorEasyAuth.Models.IUser`
1. Create your roles  
   Creating roles is easy. The recommended way is to create a static class `static class Roles` and create `public static readonly Role` properties with the roles. Ex.:
   ```c#
   public static class Roles
   {
       public static readonly Role Superuser = new(1000);
       public static readonly Role Administrator = new(999);
       public static readonly Role ManageUsers = new();
       public static readonly Role MyRole = new();
   }
   ```
   The `Role` class uses the name of the caller to determine the name of the role. In the above example, we're creating roles with the names `Superuser, Administrator, ManageUsers, MyRole`. Another parameter is `priority`. This is an integer value that can be used to determine if a certain role is "above" another, for policy evaluation later on. (ex. a Superuser can manage administrators, but not the other way around, therefore the "Priority" is higher on the Superuser role).  
   You can also instantiate roles at runtime at any point simply by constructing them.
1. Create policies  
   Similar to roles, creating policies is very easy and is recommended to be done as static declarations. As there's often multiple policies per "resource" or area (like View/Create/Edit/Delete), policies will try to walk up the stack trace and use the declaring property's full name to create the policy. Ex.:
    ```c#
    public static class Policies
    {
        public static class Users
        {
            public static readonly Policy View = new(p => p.RequireRole(Roles.ManageUsers, Roles.Administrator, Roles.Superuser));
            
            public static readonly Policy Edit = new(p => p
                .RequireRole(Roles.ManageUsers, Roles.Administrator, Roles.Superuser)
                .AddRelativeRoleRequirement(RoleRequirement.Lesser));
            
            public static readonly Policy Delete = new(p => p
                .RequireRole(Roles.ManageUsers, Roles.Administrator, Roles.Superuser)
                .AddRelativeRoleRequirement(RoleRequirement.Lesser));
        }
   
        public static class MyResource
        {
            public static readonly Policy View = new(p => p.RequireRole(Roles.MyRole, Roles.Administrator, Roles.Superuser));
            public static readonly Policy Edit = new(p => p.RequireRole(Roles.MyRole, Roles.Administrator, Roles.Superuser));
            public static readonly Policy Create = new(p => p.RequireRole(Roles.MyRole, Roles.Administrator, Roles.Superuser));
        }
    }
    ```
   This example will create policies with names like `BlazorEasyAuth.Example.Models.Policies.Users.View/Edit/Delete`. You also have to specify the policy builder immediately.  
   If you want to create policies at runtime, you have to instantiate them before calling `services.AddBlazorEasyAuth<>()`
1. Create a `NotAuthorizedPage.razor` page (see example in this project)
1. Modify `App.razor`
    1. Wrap `<Router>` in `<CascadingAuthenticationState>` tags
    1. In `<Router><Found>`, replace `<RouteView>` with
        ```html
        <AuthorizeRouteView RouteData="routeData" DefaultLayout="typeof(MainLayout)">
            <NotAuthorized>
                <NotAuthorizedPage />
            </NotAuthorized>
        </AuthorizeRouteView>
       ```
1. Create `BlazorEasyAuth.Providers.Interfaces.IUserProvider` implementation
1. Modify `Startup.cs`
    1. Add services to `ConfigureServices(IServiceCollection services)`  
       Specify the type created in the previous step that implements `IUserProvider` as type argument
       ```c#
       services.AddBlazorEasyAuth<MyUserProviderImplementation>();
       ```
    1. In `void Configure(IApplicationBuilder app, IWebHostEnvironment env)` after `app.UseRouting();`, add:
       ```csharp
       app.UseCookiePolicy();
       app.UseAuthentication();
       ```
   1. Inside `app.UseEndpoints(...)`, make sure there's a `endpoints.MapControllers();` call
        ```c#
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
        });
        ```
1. Create a sign in page at the URL `@page "/authentication/signin/{ReturnUrl?}"`. See `SignIn.razor` as example.
    
## Roles/policies

The Role and Policy classes have some extra helper functions. For example, the Role class has several comparison operators that allow you to compare the priority of two roles to one another.

```c#
var superuser = new Role(100);
var administrator = new Role(50);
var equalToAdministratorPrio = new Role(50);

superuser > administrator
// true

administrator < superuser
// true

superuser <= administrator
// false

administrator >= superuser
// false

// Equality operator doesn't check the relative priority, but actually checks if the role is equal
administrator == equalToAdministratorPrio
// false

// <= and >= will in fact check relative priorities
administrator <= equalToAdministratorPrio
// true
administrator >= equalToAdministratorPrio
// true
```

Roles are unique by the name, and equality comparison happens based on that name. If you instantiate 2 roles in 2 different classes/properties with the same name, they will equal each other:
```c#
class Class1 {
    public static readonly Role MyRole = new Role();
}

class Class2 {
    public static readonly Role MyRole = new Role();
}

Class1.MyRole == Class2.MyRole
// true
```

Roles and policies also have an implicit string operator, allowing you to use it in methods that accept a string, or comparing a string to a role:
```c#
public static readonly Role MyRole = new Role();
// in file My/Name/Space/Policies.cs
public static readonly Policy MyPolicy = new Policy(...);

MyRole == "MyRole"
// true

MyRole == "SomethingElse"
// false

MyPolicy == "My.Name.Space.Policies.MyPolicy"
// true

MyPolicy == "Something.Else"
// false
```

Because roles and policies have implicit string operators, it is easy to use them in, for example, `<AuthorizeView>` tags:
```html
<AuthorizeView Policy="@Policies.User.Create" Roles="@string.Join(',', Roles.ManageUsers, Roles.Administrator, Roles.Superuser)">
    ...
</AuthorizeView>
```

If you need to enumerate all available roles, there's a property on the `Role` class that allows you to do this: `Role.AllRoles`

Similarly for policies: `Policy.AllPolicies`
