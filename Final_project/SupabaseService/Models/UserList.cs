using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Final_project_wpf.SupabaseService.Models;

[Table("UserList")]
public class UserList : BaseModel
{
    [PrimaryKey("id", false)]
    [Column("id")]
    public int Id { get; set; }
    [Column("username")]
    public  string Username { get; set; }
    [Column("password")]
    public  string Password { get; set; }

}
