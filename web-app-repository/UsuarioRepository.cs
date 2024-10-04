using Dapper;
using MySqlConnector;
using web_app_domain;

namespace web_app_repository
{
    public class UsuarioRepository : IUsuarioRopository
    {
        private readonly MySqlConnection mySqulConnection;
        public UsuarioRepository()
        {
            string connectionString = "Server=localhost;Database=sys;User=root;Password=123;";
            mySqulConnection = new MySqlConnection(connectionString);
        }

        public async Task<IEnumerable<Usuario>> ListarUsuarios()
        {
            await mySqulConnection.OpenAsync();
            string query = "select Id, Nome, Email from usuarios;";
            var usuarios = await mySqulConnection.QueryAsync<Usuario>(query);
            return usuarios;
        }

        public async Task SalvarUsuario(Usuario usuario)
        {
            await mySqulConnection.OpenAsync();
            string sql = @"insert into usuarios(nome, email) 
                            values(@nome, @email);";
            await mySqulConnection.ExecuteAsync(sql, usuario);
            await mySqulConnection.CloseAsync();
        }

        public async Task AtualizarUsuario(Usuario usuario)
        {
            await mySqulConnection.OpenAsync();
            string sql = @"update usuarios 
                            set Nome = @nome, 
	                            Email = @email
                            where Id = @id;";

            await mySqulConnection.ExecuteAsync(sql, usuario);
            await mySqulConnection.CloseAsync();
        }

        public async Task RemoverUsuario(int id)
        {
            await mySqulConnection.OpenAsync();
            string sql = @"delete from usuarios where Id = @id;";
            await mySqulConnection.ExecuteAsync(sql, new { id });
            await mySqulConnection.CloseAsync();
        }

    }
}
