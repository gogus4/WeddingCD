using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WeddingCD.DAL.Context;

namespace Thermibox.DAL.Context
{
    /// <summary>
    /// Class that represents the database context.
    /// </summary>
    public partial class WeddingCDDbContext : WeddingCDModel, IDisposable
    {
        /// <summary>
        /// Initializes static members of the <see cref="WeddingCDDbContext"/> class.
        /// </summary>
        /// <exception cref="System.Exception">Do not remove, ensures static reference to System.Data.Entity.SqlServer</exception>
        static WeddingCDDbContext()
        {
            // This call ensures the System.Data.Entity.SqlServer assembly will be copied to the bin directory
            // Without it, there actually isn't any actual call to the SQL Server provider (which is only referenced
            // in app/web.config).
            var ensureWeLoadTheProvider = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="WeddingCDDbContext"/> class.
        ///// </summary>
        ///// <param name="configuration">The configuration instance.</param>
        //public WeddingCDDbContext(IConfiguration configuration)
        //    : this(configuration.GetString(ConfigurationKey.DatabaseConnectionString))
        //{
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="WeddingCDDbContext" /> class.
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
        internal WeddingCDDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            this.ConfigureContext();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeddingCDDbContext"/> class.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        protected WeddingCDDbContext(DbConnection connection)
            : base(connection)
        {
            this.ConfigureContext();
        }

        /// <summary>
        /// Creates a context from a database connection.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns>The context.</returns>
        public static WeddingCDDbContext CreateFromDbConnection(DbConnection connection)
        {
            return new WeddingCDDbContext(connection);
        }

        #region Protected methods

        /// <summary>
        /// Construit une DataTable pour une procécure stockée de type Table Valued Parameter.
        /// </summary>
        /// <typeparam name="T">Type de l'entité source</typeparam>
        /// <param name="values">Liste des valeurs.</param>
        /// <param name="parameterTypeName">Nom du type Table utilisé.</param>
        /// <param name="columnName">Nom de la colonne.</param>
        /// <returns>
        /// L'instance générée
        /// </returns>
        /// <remarks>
        /// Copie via reflection les propriétés primitives et non exclues dans une dataTable, avec
        /// autant de rows que d'entités spécifiées
        /// </remarks>
        protected DataTable BuildDataTable<T>(IEnumerable<T> values, string parameterTypeName, string columnName)
        {
            DataTable table = new DataTable(parameterTypeName);
            table.Columns.Add(columnName);

            foreach (var value in values)
            {
                table.Rows.Add(value);
            }

            return table;
        }

        /// <summary>
        /// Construit une DataTable pour une procécure stockée de type Table Valued Parameter.
        /// </summary>
        /// <typeparam name="TEntity">Type de l'entité source</typeparam>
        /// <param name="entities">Liste des entités.</param>
        /// <param name="parameterTypeName">Nom du type Table utilisé.</param>
        /// <param name="excludedProperties">Noms des propriétés éventuelles de l'entité qui n'appartiennent pas au Table type.</param>
        /// <returns>L'instance générée</returns>
        /// <remarks>Copie via reflection les propriétés primitives et non exclues dans une dataTable, avec
        /// autant de rows que d'entités spécifiées</remarks>
        protected DataTable BuildDataTable<TEntity>(IEnumerable<TEntity> entities, string parameterTypeName, IEnumerable<string> excludedProperties)
        {
            DataTable table = new DataTable(parameterTypeName);

            List<string> columnNames = new List<string>();
            foreach (var property in typeof(TEntity).GetProperties(BindingFlags.Instance | BindingFlags.Public).OrderBy(z => z.Name))
            {
                if (!excludedProperties.Contains(property.Name))
                {
                    // Ajoute les colonnes des types primitifs dans la table par reflection sur les entities
                    var t = property.PropertyType;
                    if (t.IsPrimitive || t == typeof(decimal) || t == typeof(string) || t == typeof(DateTime) || t == typeof(TimeSpan) || t == typeof(DateTimeOffset))
                    {
                        columnNames.Add(property.Name);
                        table.Columns.Add(property.Name, property.PropertyType);
                    }
                }
            }

            // Maintenant les valeurs
            foreach (var entitiy in entities)
            {
                DataRow dr = table.NewRow();
                foreach (var col in columnNames)
                {
                    dr[col] = entitiy.GetType().GetProperty(col).GetValue(entitiy, null);
                }

                table.Rows.Add(dr);
            }

            return table;
        }

        /// <summary>
        /// Exécute une procédure stockée.
        /// </summary>
        /// <param name="methodName">Nom de la procédure.</param>
        /// <param name="parameters">Liste des paramètres.</param>
        /// <returns>The Task to be awaited. Le résultat est le nombre de rows impactées</returns>
        /// <remarks>
        /// Les paramètres sont des paires Nom/Valeur. La valeur doit être de type
        /// DataTable pour un Table Valued Parameter
        /// </remarks>
        protected async Task<int> ExecuteStoredProcedureAsync(string methodName, IDictionary<string, object> parameters)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            foreach (var param in parameters)
            {
                if (param.Value is DataTable)
                {
                    sqlParams.Add(new SqlParameter
                    {
                        ParameterName = param.Key,
                        SqlDbType = System.Data.SqlDbType.Structured,
                        Value = param.Value ?? DBNull.Value,
                        TypeName = ((DataTable)param.Value).TableName
                    });
                }
                else
                {
                    sqlParams.Add(new SqlParameter(param.Key, param.Value ?? DBNull.Value));
                }
            }

            return await this.ExecuteStoredProcedureAsync(methodName, sqlParams);
        }

        /// <summary>
        /// Exécute une procédure stockée.
        /// </summary>
        /// <param name="methodName">Nom de la méthode.</param>
        /// <param name="sqlParams">Liste des paramètres déjà formés.</param>
        /// <returns>The Task to be awaited. Le résultat est le nombre de rows impactées</returns>
        protected async Task<int> ExecuteStoredProcedureAsync(string methodName, IList<SqlParameter> sqlParams)
        {
            string query = methodName;
            if (sqlParams.Count > 0)
            {
                query += " " + string.Join(", ", sqlParams.Select(z => string.Format("@{0}", z.ParameterName)));
            }

            return await this.Database.ExecuteSqlCommandAsync(query, sqlParams.ToArray());
        }

        /// <summary>
        /// Exécute une procédure stockée.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// The Task to be awaited. Le résultat contient le retour de la procsto
        /// </returns>
        protected async Task<IEnumerable<TResult>> ExecuteStoredProcedureAsync<TResult>(string query, params object[] parameters)
        {
            return await this.Database.SqlQuery<TResult>(query, parameters).ToListAsync();
        }

        /// <summary>
        /// Exécute une fonction scalaire.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The Task to be awaited. Le résultat contient le retour de la fonction.</returns>
        protected async Task<TResult> ExecuteScalarFunctionAsync<TResult>(string functionName, IList<SqlParameter> parameters)
        {
            string query = "SELECT " + functionName + " (";
            if (parameters.Count > 0)
            {
                query += " " + string.Join(", ", parameters.Select(z => string.Format("@{0}", z.ParameterName)));
            }

            query += " )";

            return await this.Database.SqlQuery<TResult>(query, parameters.ToArray()).SingleAsync();
        }

        /// <summary>
        /// Execute une procédure stockée et renvoie le résultat sous forme scalaire.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The Task to be awaited. Le résultat contient la valeur scalaire</returns>
        protected async Task<TResult> ExecuteStoredProcedureScalarAsync<TResult>(string methodName, params object[] parameters)
        {
            var dbCommand = this.Database.Connection.CreateCommand();
            dbCommand.Parameters.AddRange(parameters);
            dbCommand.CommandText = methodName;
            dbCommand.CommandType = CommandType.StoredProcedure;

            if (dbCommand.Connection.State == ConnectionState.Closed)
            {
                await dbCommand.Connection.OpenAsync();
            }

            var res = await dbCommand.ExecuteScalarAsync();
            return (TResult)res;
        }

        #endregion

        /// <summary>
        /// Configures the context.
        /// </summary>
        private void ConfigureContext()
        {
#if DEBUG
            this.Database.Log = z => { System.Diagnostics.Trace.Write(z); };
#endif
        }
    }
}