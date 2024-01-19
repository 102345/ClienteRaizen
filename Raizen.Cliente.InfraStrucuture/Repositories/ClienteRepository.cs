using Dapper;
using Raizen.Cliente.Common.InfraStructure.DbConnectionFactories;
using Raizen.Cliente.Domain.Repositories;
using System.Text;

namespace Raizen.Cliente.InfraStrucuture.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        public IDbConnectionFactory DbConnectionFactory { get; }
        public ClienteRepository(IDbConnectionFactory dbConnectionFactory)
        {
            DbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory));
        }

        public async Task<bool> Insert(Domain.Entities.Cliente cliente)
        {
            using (var conn = DbConnectionFactory.GetReadConnection())
            {
                var command = @"INSERT INTO dbo.Cliente
                           (Nome
                           ,DataNascimento
                           ,Email
                           ,Logradouro
                           ,Complemento
                           ,Bairro
                           ,Cidade
                           ,UF
                           ,CEP) VALUES
                           ( @Nome,
                             @DataNascimento,
                             @Email,
                             @Logradouro,
                             @Complemento,
                             @Bairro,
                             @Cidade,
                             @UF,
                             @CEP
                            )";

                var ret = await conn.ExecuteAsync(command, new
                                                            {
                                                                cliente.Nome,
                                                                cliente.DataNascimento,
                                                                cliente.Email,
                                                                cliente.Logradouro,
                                                                cliente.Complemento,
                                                                cliente.Bairro,
                                                                cliente.Cidade,
                                                                cliente.UF,
                                                                cliente.CEP
                                                            });

                return ret == 1;
            }

        }

        public async Task<bool> Update(Domain.Entities.Cliente cliente)
        {
            using (var conn = DbConnectionFactory.GetReadConnection())
            {
                const string command = @"UPDATE dbo.Cliente SET
                                             Nome = @Nome,
                                             DataNascimento = CONVERT(DATE,@DataNascimento),
                                             Email =  @Email,
                                             Logradouro = @Logradouro,
                                             Complemento = @Complemento,
                                             Bairro = @Bairro,
                                             Cidade = @Cidade,
                                             UF = @UF,
                                             CEP = @CEP
                                             WHERE Id = @Id";

                var ret = 0;

                try
                {
                    ret = await conn.ExecuteAsync(command, new
                    {
                        cliente.Nome,
                        cliente.DataNascimento,
                        cliente.Email,
                        cliente.Logradouro,
                        cliente.Complemento,
                        cliente.Bairro,
                        cliente.Cidade,
                        cliente.UF,
                        cliente.CEP,
                        cliente.Id

                    });
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    throw;
                }
              

                return ret == 1;
            }
        }

        public async Task<Domain.Entities.Cliente> GetById(int id)
        {
            var sql = @"SELECT Id
                              ,Nome
                              ,FORMAT(DataNascimento, 'dd/MM/yyyy') as DataNascimento
                              ,Email
                              ,Logradouro
                              ,Complemento
                              ,Bairro
                              ,Cidade
                              ,UF
                              ,CEP
                          FROM ClienteRaizen.dbo.Cliente WHERE Id = @Id";

            using (var conn = DbConnectionFactory.GetReadConnection())
            {
                var result = await conn.QueryFirstOrDefaultAsync<Domain.Entities.Cliente>(sql, new { id });

                return result;
            }

        }

        public async Task<IEnumerable<Domain.Entities.Cliente>> GetAll()
        {
            var sql = @"SELECT Id
                              ,Nome
                              ,DataNascimento
                              ,Email
                              ,Logradouro
                              ,Complemento
                              ,Bairro
                              ,Cidade
                              ,UF
                              ,CEP
                          FROM ClienteRaizen.dbo.Cliente ORDER By Nome ";

            using (var conn = DbConnectionFactory.GetReadConnection())
            {
                var result = await conn.QueryAsync<Domain.Entities.Cliente>(sql);

                return result;
            }
        }

        public async Task<IEnumerable<Domain.Entities.Cliente>> GetAll(Domain.Entities.Cliente cliente)
        {
            var sqlQuery = new StringBuilder();

             sqlQuery.Append(@"SELECT Id
                              ,Nome
                              ,DataNascimento
                              ,Email
                              ,Logradouro
                              ,Complemento
                              ,Bairro
                              ,Cidade
                              ,UF
                              ,CEP
                          FROM ClienteRaizen.dbo.Cliente WHERE 1 = 1");

            if(cliente.Nome != null)
            {
                sqlQuery.AppendLine(" AND UPPER(Nome) LIKE UPPER('%" + cliente.Nome + "%')"); 
            };

            if (cliente.Email != null)
            {
                sqlQuery.AppendLine(" AND UPPER(Email) LIKE UPPER('%" + cliente.Email + "%')");
            };

            if (cliente.Cidade != null)
            {
                sqlQuery.AppendLine(" AND UPPER(Cidade) LIKE UPPER('%" + cliente.Cidade + "%')");
            };

            sqlQuery.AppendLine(" ORDER BY Nome");

            var sql = sqlQuery.ToString();

            using (var conn = DbConnectionFactory.GetReadConnection())
            {
                var result = await conn.QueryAsync<Domain.Entities.Cliente>(sql);

                return result;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (var conn = DbConnectionFactory.GetReadConnection())
            {
                var command = @"DELETE FROM dbo.Cliente WHERE Id = @id";

                var ret = await conn.ExecuteAsync(command, new
                {
                    id
                });

                return ret == 1;
            }
        }
    }
}
