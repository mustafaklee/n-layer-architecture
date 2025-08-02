namespace App.Repositories
{
    //OptionsPattern
    public class ConnectionStringOption
    {
        public const string Key = "ConnectionStrings";
        public string MySqlServer { get; set; }= default!;
    }
}
